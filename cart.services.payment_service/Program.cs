using cart.services.payment_service.DataProvide;
using cart.services.payment_service.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<PaymentDBContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("PaymentSQLCONNECTION")));
builder.Services.AddDbContext<InvoiceDBContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("InvoiceSQLCONNECTION")));
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
}
);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("GetPaymentUrl", async (IPaymentRepo repo, [FromQuery] int InvoiceId) =>
{

    try
    {
        if (InvoiceId == 0)
        {
            return Results.NotFound();
        }
        var paymentUrl = await repo.GetPaymentUrl(InvoiceId);
        if (paymentUrl == null || paymentUrl == "")
        {
            return Results.NotFound();
        }
        return Results.Ok(paymentUrl);
    }
    catch (Exception e)
    {
        return Results.Problem();
    }
});





app.Run();

