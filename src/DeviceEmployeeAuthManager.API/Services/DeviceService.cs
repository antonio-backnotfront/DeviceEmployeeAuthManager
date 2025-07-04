﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DAL;
using DeviceEmployeeAuthManager.API.DTO;
using DeviceEmployeeAuthManager.API.Exceptions;
using DeviceEmployeeAuthManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceEmployeeAuthManager.API.Services;

public class DeviceService : IDeviceService
{
    private readonly DeviceEmployeeDbContext _context;

    public DeviceService(DeviceEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetDevicesDto>> GetAllDevices(CancellationToken cancellationToken)
    {
        List<Device> devices = await _context.Devices.ToListAsync(cancellationToken);
        return devices.Select(d => new GetDevicesDto(d.Id, d.Name)).ToList();
    }

    public async Task<List<GetDeviceTypesDto>> GetDeviceTypesDto(CancellationToken cancellationToken)
    {
        List<DeviceType> deviceTypes = await _context.DeviceTypes.ToListAsync(cancellationToken);
        return deviceTypes.Select(d => new GetDeviceTypesDto { Name = d.Name, Id = d.Id }).ToList();
    }

    public async Task<GetDeviceDto?> GetDeviceById(int id, CancellationToken cancellationToken)
    {
        var device = await _context.Devices
            .Include(d => d.DeviceType)
            .Include(d => d.DeviceEmployees)
            .ThenInclude(de => de.Employee)
            .ThenInclude(e => e.Person)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        ;
        if (device is null) return null;
        var dto = new GetDeviceDto
        {
            Name = device.Name,
            Type = device.DeviceType.Name,
            AdditionalProperties = JsonDocument.Parse(device.AdditionalProperties ?? "").RootElement,
            IsEnabled = device.IsEnabled
        };


        return dto;
    }

    public async Task<bool> CreateDevice(CreateDeviceDto dto, CancellationToken cancellationToken)
    {
        var deviceType = await _context.DeviceTypes.FirstOrDefaultAsync(d => d.Id == dto.TypeId, cancellationToken);
        if (deviceType == null)
            throw new InvalidDeviceTypeException();

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Invalid device name");


        var device = new Device
        {
            Name = dto.Name,
            DeviceTypeId = deviceType.Id,
            IsEnabled = dto.IsEnabled,
            AdditionalProperties = dto.AdditionalProperties.GetRawText()
        };
        await _context.Devices.AddAsync(device, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        dto.Id = device.Id;
        return true;
    }

    public async Task<bool> UpdateDevice(int id, UpdateDeviceDto dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.DeviceType))
            throw new ArgumentException("Invalid device type");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Invalid device name");

        var existingDevice = await _context.Devices.FindAsync(id, cancellationToken)
                             ?? throw new KeyNotFoundException("Device not found");

        var deviceType = await GetDeviceTypeByName(dto.DeviceType, cancellationToken)
                         ?? throw new InvalidDeviceTypeException();

        if (existingDevice == null)
            throw new KeyNotFoundException($"Device with ID {id} not found.");

        existingDevice.Name = dto.Name;
        existingDevice.IsEnabled = dto.IsEnabled;
        existingDevice.DeviceType = deviceType;
        existingDevice.AdditionalProperties = dto.AdditionalProperties.GetRawText();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteDevice(int id, CancellationToken cancellationToken)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (device == null)
            throw new KeyNotFoundException($"Device with ID {id} not found.");

        var deviceEmployees = await _context.DeviceEmployees
            .Where(de => de.DeviceId == id)
            .ToListAsync(cancellationToken);

        if (deviceEmployees.Any())
            _context.DeviceEmployees.RemoveRange(deviceEmployees);

        _context.Devices.Remove(device);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<int>> GetDeviceIdsByEmployeeId(int id, CancellationToken cancellationToken)
    {
        var deviceIds = await _context.DeviceEmployees.Where(d => d.EmployeeId == id).Select(d => d.DeviceId)
            .ToListAsync(cancellationToken);
        return deviceIds;
    }

    public async Task<DeviceType?> GetDeviceTypeByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.DeviceTypes
                .FirstOrDefaultAsync(dt => dt.Name == name, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Failed to retrieve device type '{name}'.", ex);
        }
    }
}