using Urbamais.Infra.Config;
using Urbamais.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Bootstrap.AddService(builder.Services, builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();