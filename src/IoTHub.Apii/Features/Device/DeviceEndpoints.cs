using IoTHub.Infrastructure;

namespace IoTHub.Features.Device;

public static class DeviceEndpoints
{
    public static IEndpointRouteBuilder MapDeviceEndpoints(this IEndpointRouteBuilder group)
    {
        //group.MapGet("/devices", ...).WithName("GetDevices");
        return group;
    }
}