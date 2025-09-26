using IoTHub.Domain.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHub.Infrastructure.EntityConfigurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Location).HasMaxLength(200);
        builder.Property(d => d.Description).HasMaxLength(500);
        builder.Property(d => d.Status).IsRequired();
        builder.Property(d => d.LastActive);
        builder.Property(d => d.IsActive).IsRequired();
        builder.Property(d => d.CreatedAt).IsRequired().HasDefaultValue("NOW()");
        builder.Property(d => d.UpdatedAt);

        builder.HasMany(d => d.Sensors)
               .WithOne(s => s.Device)
               .HasForeignKey("DeviceId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => d.Name );
        builder.HasIndex(d => d.IsActive );
    }
}