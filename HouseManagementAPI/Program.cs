using FluentValidation.AspNetCore;
using HouseManagementAPI.Caching;
using HouseManagementAPI.Notifications;
using HouseManagementAPI.Repositories;
using HouseManagementAPI.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Add memory cache
builder.Services.AddScoped<IHouseCacheService, InMemoryCacheService>();
builder.Services.AddScoped<AddHouseCommandHandler>();
builder.Services.AddScoped<GetHousesQueryHandler>();
builder.Services.AddScoped<HouseNotificationService>();

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
