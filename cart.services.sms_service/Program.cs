using cart.services.sms_service.Models;
using Ghasedak;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var SMSToken = builder.Configuration["SMSToken"];
var LineNumber = builder.Configuration["LineNumber"];
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapPost("/sendSms", async ([FromBody] SmsDTO dto) =>
{
try {
    var sms = new Ghasedak.Core.Api(SMSToken);

    var result = await sms.SendSMSAsync($"message\n code:{dto.OtpCode}", dto.MobileNumber, linenumber: LineNumber);
    if (result.Result.Code == 200)
    {
        return Results.Ok();
    }
    else
        return Results.Problem();

} catch (Exception e)
    {
        return Results.Problem();
    }

}
);


app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
