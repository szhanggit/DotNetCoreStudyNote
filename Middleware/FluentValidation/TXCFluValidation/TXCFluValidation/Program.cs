#region Startup
using Microsoft.AspNetCore.Mvc;
using TXCFluValidation.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Configure AppConfigurations
Console.WriteLine("Starting..." + builder.Environment.EnvironmentName);
builder.Configuration.AddEnvironmentVariables();
#endregion




#region Configure Services
var services = builder.Services;
var configuration = builder.Configuration;
builder.Services.AddControllers();
services.AddFluentValidation(configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
#endregion




#region Configure
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
#endregion


app.Run();
