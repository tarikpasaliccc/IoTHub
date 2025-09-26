using IoTHub.Domain.Common.Enums;
using IoTHub.Domain.Devices;
using IoTHub.Domain.Readings;
using IoTHub.Domain.Sensors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHub.Infrastructure.EntityConfigurations;

public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
{
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Timestamp).IsRequired();
        builder.Property(r => r.Value).IsRequired();
        builder.Property(r => r.Unit).IsRequired().HasMaxLength(50);
        builder.Property(r => r.Quality).IsRequired().HasDefaultValue(ReadingQuality.Good);
        builder.Property(r => r.IngestionAt).IsRequired().HasDefaultValueSql("NOW()");

        builder.HasOne<Device>()
               .WithMany()
               .HasForeignKey(r => r.DeviceId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Sensor>()
            .WithMany(s => s.Readings)
            .HasForeignKey(r => r.SensorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(r => new { r.DeviceId, r.Timestamp });
        builder.HasIndex(r => new { r.SensorId, r.Timestamp });
    }
}