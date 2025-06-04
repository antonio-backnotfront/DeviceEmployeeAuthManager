# DeviceEmployeeManager

## appsettings.json Structure

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultDatabase": "Server=your_server;Database=your_db;User=placeholder;Password=placeholder;"
  }
}
```

## Why I chose not to split the solution into multiple projects
I went for this approach because on my local machine it's not very convenient to work on multiple projects at the same time. Additionally, it's much more comfortable in terms of importing external libraries as I don't need to manually import a single library into multiple projects, I only need to do it once. Also, referencing my classes and methods from other files is faster during the development process. 