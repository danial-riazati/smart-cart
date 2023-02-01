using cart.services.long_polling;
using cart.services.long_polling.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/addPoll", async (PollingDTO dto) =>
{
    try
    {
        if (string.IsNullOrEmpty(dto.name))
            return Results.Problem();


        LongPolling lp = new(dto.name);
        var message = await lp.WaitAsync();

        if (message == null)
        {
            return Results.StatusCode(502);
        }
        return Results.Ok(message);
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
});


app.MapGet("/releasePoll/{pollingName}", (string pollingName) =>
{
    if (string.IsNullOrEmpty(pollingName))
        return Results.Problem();

    try
    {
        var message = pollingName + " Unlocked";
        var res = LongPolling.Publish(pollingName, message);
        if (res)
            return Results.Ok(message);
        return Results.NotFound();
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }


});


app.Run();




