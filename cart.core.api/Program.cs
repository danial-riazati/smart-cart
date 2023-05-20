using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using cart.core.api.Services;
using System.Threading.Tasks;
using System.Text;
using cart.core.api.Repos;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var logger = NLog.LogManager.Setup()
         .LoadConfigurationFromAppSettings()
         .GetCurrentClassLogger();

logger.Info("Starting the application");
builder.Services.AddSingleton<CameraTcpServer>(new CameraTcpServer(1302, async client =>
{
   
    using var stream = client.GetStream();
    var buffer = new byte[1024];
    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
    var request = Encoding.ASCII.GetString(buffer, 0, bytesRead);


    var response = $"Hello, {request}!";
    var outputBuffer = Encoding.ASCII.GetBytes(response);
    await stream.WriteAsync(outputBuffer, 0, outputBuffer.Length);
}));
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
}
);


builder.Services.AddControllers();

builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<ICameraTcpServer, CameraTcpServer>();
builder.Services.AddScoped<IWeightRepo, WeightRepo>();
builder.Services.AddScoped<IBarcodeRepo, BarcodeRepo>();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var tcpServer = app.Services.GetRequiredService<CameraTcpServer>();
tcpServer.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
