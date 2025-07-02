using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace DeviceEmployeeAuthManager.API.Helpers.Config;

public class DeviceValidator : IDeviceValidator
{
    public async Task<List<string>> ValidateAdditionalProperties(string deviceJson,
        DeviceEmployeeDbContext context)
    {
        List<string> errors = new List<string>();


        var validationJson = await File.ReadAllTextAsync("Helpers/Config/validation.json");
        var validationRoot = JsonDocument.Parse(validationJson).RootElement;

        using var deviceDoc = JsonDocument.Parse(deviceJson);
        var rootElement = deviceDoc.RootElement;

        // Get type identifier (support both CreateDeviceDto.TypeId and UpdateDeviceDto.DeviceType)
        // If the device type is unusual(empty) - there's no point in continuing the validation, it will always be true, therefore, return empty list
        string? deviceType = null;
        if (rootElement.TryGetProperty("typeId", out var typeIdElement))
            deviceType = (await context.DeviceTypes.FirstOrDefaultAsync(e => e.Id == typeIdElement.GetInt32())).Name;

        else if (rootElement.TryGetProperty("deviceType", out var deviceTypeElement))
            deviceType = (await context.DeviceTypes.FirstOrDefaultAsync(e => e.Name == deviceTypeElement.GetString()))
                .Name;

        if (string.IsNullOrEmpty(deviceType))
        {
            errors.Add("Valid device type is required.");
            return errors;
        }

        // Get additionalProps
        if (!rootElement.TryGetProperty("additionalProperties", out var additionalProps))
        {
            errors.Add("Missing 'AdditionalProperties'");
            return errors;
        }

        // used reflection here because we may not know the type of the element on which this method can be called on
        // creates a dynamic custom method that may be invoked on JsonElement class
        var getPropertyMethod = typeof(JsonElement).GetMethod("GetProperty", new Type[] { typeof(string) })!;

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
            var type = item.GetProperty("type").GetString()!;

            if (!string.Equals(type, deviceType, StringComparison.OrdinalIgnoreCase))
                continue;

            var preRequestName = item.GetProperty("preRequestName").GetString()!;
            var expectedPreRequestValue = item.GetProperty("preRequestValue").GetString()!;


            if (!TryGetProperty(rootElement, preRequestName, out var actualValueElement) ||
                actualValueElement.ToString().ToLower() != expectedPreRequestValue.ToLower())
                continue;


            foreach (var rule in item.GetProperty("rules").EnumerateArray())
            {
                var paramName = rule.GetProperty("paramName").GetString()!;

                if (!TryGetProperty(additionalProps, paramName, out var valueElement))
                {
                    errors.Add($"Missing parameter: {paramName}");
                    continue;
                }

                var actualValue = valueElement.GetString()!;

                var regexProperty = rule.GetProperty("regex");

                if (regexProperty.ValueKind == JsonValueKind.Array)
                {
                    bool match = regexProperty.EnumerateArray()
                        .Any(opt => string.Equals(opt.GetString(), actualValue, StringComparison.OrdinalIgnoreCase));

                    if (!match) errors.Add($"Value '{actualValue}' not in allowed list for {paramName}");
                }
                else
                {
                    var regexPattern = regexProperty.GetString()!;
                    if (!Regex.IsMatch(actualValue, regexPattern))
                        errors.Add($"Regex '{regexPattern}' failed for {paramName} = '{actualValue}'");
                }
            }
        }

        return errors;
    }
}