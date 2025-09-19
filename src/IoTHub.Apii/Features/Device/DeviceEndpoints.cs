namespace IoTHub.Features.Device;

public static class DeviceEndpoints
{
    public static void MapDeviceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/devices").WithTags("Devices");

        group.MapGet("/", () => Results.Ok("Get all devices")).WithName("GetAllDevices");
        group.MapGet("/{id}", (string id) => Results.Ok($"Get device with ID: {id}")).WithName("GetDeviceById");
        group.MapPost("/", () => Results.Ok("Create a new device")).WithName("CreateDevice");
        group.MapPut("/{id}", (string id) => Results.Ok($"Update device with ID: {id}")).WithName("UpdateDevice");
        group.MapDelete("/{id}", (string id) => Results.Ok($"Delete device with ID: {id}")).WithName("DeleteDevice");
    }
}