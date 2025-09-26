using IoTHub.Features.Device;
using IoTHub.Infrastructure;
using IoTHub.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
    {
    options.AddPolicy(name: "IoTCorsPolicy",
        policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
var api = app.MapGroup("/api");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("IoTCorsPolicy");
api.MapHealthChecks("/health");
api.MapDeviceEndpoints();
app.Run();
