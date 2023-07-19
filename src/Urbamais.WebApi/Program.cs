using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Urbamais.WebApi;
using Urbamais.WebApi.Swagger;

var version = 1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Bootstrap.AddService(builder.Services, builder.Configuration);

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddApiProblemDetails();

builder.Services.AddControllers();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddApiVersioning(x =>
{
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.DefaultApiVersion = new ApiVersion(version, 0);
    x.ApiVersionReader = new HeaderApiVersionReader("version");
});

builder.Services.AddVersionedApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVV";
    x.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerDefaultValues>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Informe o token de autenticação JWT obtido no endpoint 'Login'.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
});

//builder.Services.AddSwaggerGen(c =>
//{
//    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Urbamais", Version = "v1" });

//    // Configuração para autenticação JWT no Swagger UI
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme",
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference = new OpenApiReference
//                    {
//                        Type = ReferenceType.SecurityScheme,
//                        Id = "Bearer"
//                    }
//                },
//                new string[] {}
//            }
//        });
//});

var app = builder.Build();

var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"Urbamais - {description.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseProblemDetails();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();