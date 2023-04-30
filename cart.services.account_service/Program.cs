using cart.services.account_service.Repos;
using MySql.Data.MySqlClient;
using cart.services.account_service.DataProvide;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using CipherHelper;
using cart.services.account_service.Models;
using System.Text.RegularExpressions;

using NLog;
using NLog.Web;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var mySQLBuilder = new MySqlConnectionStringBuilder();
    mySQLBuilder.ConnectionString = builder.Configuration.GetConnectionString("MYSQLCONNECTION");
    logger.Info(mySQLBuilder.ConnectionString);
    builder.Services.AddDbContext<AccountDBContext>(opt => opt.UseMySQL(mySQLBuilder.ConnectionString));
    builder.Services.AddScoped<IAccountRepo, AccountRepo>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
    }
    );


    var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());

    app.MapPost("/checkin", async (IAccountRepo repo, [FromBody] CheckinDTO dto) =>
    {
        logger.Info("init main");
        try
        {

            var model = new Checkin { CartId = dto.CartId, PhoneNumber = dto.PhoneNumber };
            var id = await repo.CheckIn(model);
            return Results.Ok(new { id = id });
        }
        catch (Exception e)
        {

            return Results.Problem();
        }
    });




    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
