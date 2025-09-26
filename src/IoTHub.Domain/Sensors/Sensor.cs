using IoTHub.Domain.Common.Enums;
using IoTHub.Domain.Devices;
using IoTHub.Domain.Readings;

namespace IoTHub.Domain.Sensors;

public class Sensor
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public required string Name { get; set; }
    public SensorType Type { get; set; }
    public required string Unit { get; set; }
    public int? Precision { get; set; }
    public double? MinThreshold { get; set; }
    public double? MaxThreshold { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Device Device { get; set; } = null!;
    public ICollection<Reading> Readings { get; set; } = new List<Reading>();
}