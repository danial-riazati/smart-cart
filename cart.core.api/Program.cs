var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
var server = new TcpServer(1302, async client =>
{
    // Handle client connection
    using var stream = client.GetStream();
    var buffer = new byte[1024];
    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
    var request = Encoding.ASCII.GetString(buffer, 0, bytesRead);

    // Process request
    var response = $"Hello, {request}!";
    var outputBuffer = Encoding.ASCII.GetBytes(response);
    await stream.WriteAsync(outputBuffer, 0, outputBuffer.Length);
});
await server.Start();
