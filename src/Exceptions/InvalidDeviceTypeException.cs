namespace src.DeviceEmployeeAuthManager.Exceptions;

public class InvalidDeviceTypeException : Exception
{
    public InvalidDeviceTypeException() : base("Invalid device type")
    {
    }
}