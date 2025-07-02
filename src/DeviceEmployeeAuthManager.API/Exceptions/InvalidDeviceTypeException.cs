using System;

namespace DeviceEmployeeAuthManager.API.Exceptions;

public class InvalidDeviceTypeException : Exception
{
    public InvalidDeviceTypeException() : base("Invalid device type")
    {
    }
}