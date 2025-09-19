using IoTHub.Features.Device;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDeviceEndpoints();
app.UseHttpsRedirection();
app.MapHealthChecks("/api/health");
app.Run();
