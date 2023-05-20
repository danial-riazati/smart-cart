using cart.services.sms_service.Models;
using Ghasedak;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var SMSToken = builder.Configuration["SMSToken"];
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();



        app.MapPost("/sendSms", async ([FromBody] SmsDTO dto) =>
        {
            try
            {
                var sms = new Ghasedak.Core.Api(SMSToken);

                var result = await sms.VerifyAsync(1, "radino", new string[] { dto.phoneNumber }, dto.code.ToString());
                if (result.Result.Code == 200)
                {
                    return Results.Ok();
                }
                else
                    return Results.Problem();

            }
            catch (Exception e)
            {
                return Results.Problem();
            }

        }
        );


        app.Run();
    }
}

