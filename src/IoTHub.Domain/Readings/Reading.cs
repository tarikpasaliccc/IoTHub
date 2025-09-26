using IoTHub.Domain.Common.Enums;

namespace IoTHub.Domain.Readings;

public class Reading
{
    public long Id { get; set; }
    public Guid DeviceId { get; set; }
    public Guid SensorId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = null!;
    public ReadingQuality Quality { get; set; }
    public DateTimeOffset IngestionAt { get; set; }
}   