using Shipment_Api;
using Shipment_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IShipmentService, ShipmentService>();

builder.Services.AddControllers();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var allowedOrigins = builder.Configuration
                    .GetSection("Cors:AllowedOrigins")
                    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
   options.AddPolicy("shipment-client", policy =>
   {
       policy
        .WithOrigins(allowedOrigins)
        .WithHeaders("Content-Type", "Authorization")
        .WithMethods("GET");
   });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// For unhandled exceptions
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("shipment-client");
app.UseAuthorization();
app.MapControllers();
app.Run();
