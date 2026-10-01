using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using OneStreamAssessment;
using OneStreamAssessment.Authentication;

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
builder.Services.AddCors();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()  
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
