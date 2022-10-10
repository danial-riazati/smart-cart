using cart.services.invoice_service.DataProvide;
using cart.services.invoice_service.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var mySQLBuilder = new MySqlConnectionStringBuilder();
mySQLBuilder.ConnectionString = builder.Configuration.GetConnectionString("MYSQLCONNECTION");
mySQLBuilder.UserID = builder.Configuration["UserId"];
mySQLBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<InvoiceDBContext>(opt => opt.UseMySQL(mySQLBuilder.ConnectionString));
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

app.MapGet("invoice",async (IInvoiceRepo repo , [FromQuery] int id) =>
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

app.Run();

