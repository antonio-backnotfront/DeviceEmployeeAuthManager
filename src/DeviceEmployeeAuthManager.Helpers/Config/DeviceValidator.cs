using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Helpers.Config;

public class DeviceValidator
{
    public static async Task<List<string>> ValidateAdditionalProperties(string deviceJson)
    {
        List<string> errors = new List<string>();
        Console.WriteLine("Entered validator");

        var validationJson = await File.ReadAllTextAsync("DeviceEmployeeAuthManager.Helpers/Config/validation.json");
        var validationRoot = JsonDocument.Parse(validationJson).RootElement;

        Console.WriteLine($"device: {deviceJson}");
        var deviceDto = JsonSerializer.Deserialize<CreateDeviceDto>(
            deviceJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        Console.WriteLine($"device: {deviceDto?.DeviceType}");
        if (deviceDto == null || string.IsNullOrEmpty(deviceDto.DeviceType))
        {
            errors.Add("Invalid device type");
            return errors;
        }

        using JsonDocument deviceDoc = JsonDocument.Parse(deviceJson);
        JsonElement rootElement = deviceDoc.RootElement;

        JsonElement additionalProps = deviceDto.AdditionalProperties;

        // used reflection here because we may not know the type of the element on which this method can be called on
        // creates a dynamic custom method that may be invoked on JsonElement class
        MethodInfo getPropertyMethod = typeof(JsonElement).GetMethod("GetProperty", new Type[] { typeof(string) })!;
        
        // tries to get the property 'propName' from JsonElement called element
        // true: if found and sets propValue = element('propName')
        // false: if not found
        // propValue: output object with the value of property 'propName', initially set as default 
        bool TryGetProperty(JsonElement element, string propName, out JsonElement propValue)
        {
            propValue = default;
            try
            {
                propValue = (JsonElement)getPropertyMethod.Invoke(element, new object[] { propName })!;
                // could simply use this ⌄ instead of this ^
                // propValue = element.GetProperty(propName);
                return true;
            }
            catch (TargetInvocationException tie) when (tie.InnerException is KeyNotFoundException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }

        foreach (var item in validationRoot.GetProperty("validations").EnumerateArray())
        {
            string type = item.GetProperty("type").GetString()!;
            Console.WriteLine($"Validation type: {type}");
            if (!string.Equals(type, deviceDto.DeviceType, StringComparison.OrdinalIgnoreCase))
                continue;
            Console.WriteLine($"Passed if");
            string preRequestName = item.GetProperty("preRequestName").GetString()!;
            string expectedPreRequestValue = item.GetProperty("preRequestValue").GetString()!;
            Console.WriteLine($"param: {preRequestName}");
            Console.WriteLine($"param: {expectedPreRequestValue}");

            if (!TryGetProperty(rootElement, preRequestName, out JsonElement actualValueElement) ||
                actualValueElement.GetString()?.ToLower() != expectedPreRequestValue.ToLower())
                continue;

            Console.WriteLine("Passed preRequest check");

            foreach (var rule in item.GetProperty("rules").EnumerateArray())
            {
                string paramName = rule.GetProperty("paramName").GetString()!;

                if (!TryGetProperty(additionalProps, paramName, out JsonElement valueElement))
                {
                    errors.Add($"Missing parameter: {paramName}");
                    continue;
                }

                string actualValue = valueElement.GetString()!;

                var regexProperty = rule.GetProperty("regex");
                Console.WriteLine($"regex {actualValue} with  {regexProperty}");
                if (regexProperty.ValueKind == JsonValueKind.Array)
                {
                    bool match = regexProperty.EnumerateArray()
                        .Any(opt => string.Equals(opt.GetString(), actualValue, StringComparison.OrdinalIgnoreCase));

                    if (!match)
                    {
                        errors.Add($"Value '{actualValue}' not in allowed list for {paramName}");
                    }
                    Console.WriteLine($"matched {actualValue} with list {regexProperty.EnumerateArray().ToList()}");
                }
                else
                {
                    string regexPattern = regexProperty.GetString()!;
                    if (!Regex.IsMatch(actualValue, regexPattern))
                    {
                        errors.Add($"Regex '{regexPattern}' failed for {paramName} = '{actualValue}'");
                    }
                    Console.WriteLine($"matched {actualValue} with {regexProperty.ValueKind}");
                }
            }
        }

        return errors;
    }
}
