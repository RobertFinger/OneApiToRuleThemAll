using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OneStreamAssessment;
using OneStreamAssessment.Authentication;
using System;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( x => {
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme {
        Description = "Api key for api keying.",
        Name= "X-Api-Key",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme {
        Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header,
    };

    var requirement = new OpenApiSecurityRequirement
    {
        {scheme, new List<string>() }
    };

    x.AddSecurityRequirement(requirement);

});

builder.Services.AddHttpClient("AirApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7108");
});

builder.Services.AddHttpClient("LakeApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7246");
});

builder.Services.AddScoped<DataManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
