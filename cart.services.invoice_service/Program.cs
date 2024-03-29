﻿using cart.services.invoice_service.DataProvide;
using cart.services.invoice_service.Model;
using cart.services.invoice_service.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<InvoiceDBContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("MYSQLCONNECTION")));
builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();

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

app.MapGet("GetInvoice",async (IInvoiceRepo repo , [FromQuery] int id) =>
{

    try
    {
        if (id == 0)
        {
            return Results.NotFound();
        }
        var invoice = await repo.GetInvoice(id);
        if (invoice == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(invoice);
    }
    catch(Exception e)
    {
        return Results.Problem();
    }
   

});
app.MapPost("SaveInvoice", async (IInvoiceRepo repo, [FromBody] InvoiceDTO dto) =>
{

    try
    {

        if (dto == null)
        {
            return Results.NotFound();
        }
        await repo.SaveInvoice(dto);
      
        return Results.Ok();
    }
    catch (Exception e)
    {
        return Results.Problem();
    }


});

app.Run();

