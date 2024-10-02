using FluentValidation.AspNetCore;
using HouseManagementAPI.Caching;
using HouseManagementAPI.Notifications;
using HouseManagementAPI.Repositories;
using HouseManagementAPI.Validation;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Add memory cache
builder.Services.AddScoped<IHouseCacheService, InMemoryCacheService>();
builder.Services.AddScoped<AddHouseCommandHandler>();
builder.Services.AddScoped<GetHousesQueryHandler>();
builder.Services.AddScoped<HouseNotificationService>();

// Add rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("HouseLimiter", opt =>
    {
        opt.PermitLimit = 10; // Allow 5 requests
        opt.Window = TimeSpan.FromMinutes(1); // per 1 minute
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2; // Allow 2 queued requests
    });
});

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<HouseModelValidator>());


var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
