using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using cart.services.product_service.DataProvide;
using cart.services.product_service.Repos;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddDbContext<ProductDBContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("MYSQLCONNECTION")));
builder.Services.AddScoped<IProductRepo, ProductRepo>();
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
app.MapControllers();
app.Run();

