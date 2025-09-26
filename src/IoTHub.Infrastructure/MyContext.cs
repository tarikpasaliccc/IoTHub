using IoTHub.Domain.Devices;
using IoTHub.Domain.Readings;
using IoTHub.Domain.Sensors;
using IoTHub.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IoTHub.Infrastructure;

public class MyContext(DbContextOptions<MyContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Reading> Readings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new DeviceConfiguration().Configure(modelBuilder.Entity<Device>());
        new SensorConfiguration().Configure(modelBuilder.Entity<Sensor>());
        new ReadingConfiguration().Configure(modelBuilder.Entity<Reading>());
    }
}