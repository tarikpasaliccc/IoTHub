namespace IoTHub.Features.Device;

public static class DeviceEndpoints
{
    public static void MapDeviceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/device").WithTags("Device");

        
    }
}