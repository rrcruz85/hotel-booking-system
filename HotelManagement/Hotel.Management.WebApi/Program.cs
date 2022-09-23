using Hotel.Booking.Common.WebApi.Middleware;
using Hotel.Management.DataAccess;
using Hotel.Management.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddEnvironmentVariables().AddJsonFile("appsettings.json", optional: true);
var configuration = configBuilder.Build();
builder.Services.AddSingleton(configuration);
 
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelManagementContext>();
builder.Services.AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseUnhandledExceptionHandler();
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();
