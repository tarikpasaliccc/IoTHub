using IoTHub.Domain.Sensors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHub.Infrastructure.EntityConfigurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Type).IsRequired();
        builder.Property(s => s.Unit).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Precision);
        builder.Property(s => s.MinThreshold);
        builder.Property(s => s.MaxThreshold);
        builder.Property(s => s.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(s => s.CreatedAt).IsRequired().HasDefaultValue("NOW()");
        builder.Property(s => s.UpdatedAt);
        
        builder.HasOne(s => s.Device)
               .WithMany(d => d.Sensors)
               .HasForeignKey(s => s.DeviceId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(s => s.DeviceId);
        builder.HasIndex(s => new {s.DeviceId, s.IsActive});
        builder.HasIndex(s => new {s.DeviceId, s.Type}).IsUnique();
    }
}