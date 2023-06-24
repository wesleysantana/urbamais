using Autofac;
using Autofac.Extensions.DependencyInjection;
using Urbamais.CrossCutting.Autofac;
using Urbamais.WebApi;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            builder.RegisterModule(new AutofacModule());
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//Bootstrap.AddService(builder.Services, builder.Configuration);

//builder.Services.AddControllers();

//builder.Services.AddRouting(options => options.LowercaseUrls = true);

//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (builder.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Metamais v1"));
//}

//app.UseHttpsRedirection();

//app.UseRouting();

////app.UseCors(builder => builder
////    .SetIsOriginAllowed(orign => true)
////    .AllowAnyMethod()
////    .AllowAnyHeader()
////    .AllowCredentials());

//app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();