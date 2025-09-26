using IoTHub.Domain.Common.Enums;
using IoTHub.Domain.Sensors;

namespace IoTHub.Domain.Devices;

public class Device
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime LastActive { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
}