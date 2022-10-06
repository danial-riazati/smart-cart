using cart.services.account_service.Repos;
using MySql.Data.MySqlClient;
using cart.services.account_service.DataProvide;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using CipherHelper;
using LongPolling;
using cart.services.account_service.Models;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySQLBuilder = new MySqlConnectionStringBuilder();
mySQLBuilder.ConnectionString = builder.Configuration.GetConnectionString("MYSQLCONNECTION");
mySQLBuilder.UserID = builder.Configuration["UserId"];
mySQLBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CartDBContext>(opt => opt.UseMySQL(mySQLBuilder.ConnectionString));
builder.Services.AddScoped<IAccountRepo, AccountRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
}
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.MapPost("/checkin",   async (IAccountRepo repo, [FromBody]CheckinDTO dto) =>
{
    try
    {
      
        var model = new CheckInOut { CartId = dto.CartId, PhoneNumber = dto.PhoneNumber };
        await repo.CheckIn(model);
        return Results.Ok();
    }catch(Exception e)
    {
        return Results.Problem();
    }
});

//app.MapPost("/otp", async ([FromBody] OtpDTO dto) =>
//{
//    var x = 2;
//    try
//    {
//        if (dto.MobileNumber == null || !Regex.IsMatch(dto.MobileNumber, "^[0-9]{11}$", RegexOptions.Compiled)) 
//        {
//            return Results.BadRequest("mobile number is incorrect");
//        }
//        return Results.Ok
//            (new
//            {
//                code = dto.MobileNumber.Substring(dto.MobileNumber.Length - 6)
//            });
//    }catch(Exception e)
//    {
//        return Results.BadRequest($"mobile number is incorrect + {e}");
//    }
//});


app.Run();
