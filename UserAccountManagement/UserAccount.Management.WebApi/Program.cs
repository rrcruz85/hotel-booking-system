using Hotel.Booking.Common.WebApi.Middleware;
using UserAccount.Management.DataAccess;
using UserAccount.Management.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddEnvironmentVariables().AddJsonFile("appsettings.json", optional: true);
var configuration = configBuilder.Build();
builder.Services.AddSingleton(configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserAccountManagementContext>();
builder.Services.AddServices();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseUnhandledExceptionHandler();
//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

app.Run();
