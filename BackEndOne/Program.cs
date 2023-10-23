using AirStat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

List<AirStatistics> WeatherStats = new List<AirStatistics>()
{
    new AirStatistics()
    {
        Id = 1,
        Date = DateTime.Today.AddDays(15),
        City = "Traverse City",
        weather = Weather.Balmy,
        Temperature = 71,
        Celcius = false

    },
    new AirStatistics()
    {
        Id = 2,
        Date = DateTime.Today.AddDays(40),
        City = "Port Huron",
        weather = Weather.Bracing,
        Temperature = 41,
        Celcius = false
    },
    new AirStatistics()
    {
        Id = 3,
        Date = DateTime.Today.AddDays(10),
        City = "Detroit",
        weather = Weather.Hot,
        Temperature = 95,
        Celcius = false
    },
    new AirStatistics()
    {
        Id = 4,
        Date = DateTime.Today.AddDays(15),
        City = "Mackinaw City",
        weather = Weather.Mild,
        Temperature = 55,
        Celcius = false

    },
    new AirStatistics()
    {
        Id = 5,
        Date = DateTime.Today.AddDays(150),
        City = "Novi",
        weather = Weather.Cool,
        Temperature = 56,
        Celcius = false
    }


};

app.MapGet("/Weather", async () =>
{

    await Task.Delay(2000);
    return WeatherStats;

});
app.MapPost("/Weather", async (AirStatistics l) =>
{
    await Task.Delay(1000);
    WeatherStats.Add(l);
});

app.MapPut("/Weather/{id}", async (int id, AirStatistics a) =>
{
    await Task.Delay(100);
    var air = WeatherStats.Where(a => a.Id == id).FirstOrDefault();

    if (air != null)
    {
        if (a.City != null)
            air.City = a.City;

        if (a.Celcius != null)
            air.Celcius = a.Celcius;

        if (a.Temperature != null)
            air.Temperature = a.Temperature;

        if (a.Date != null)
            air.Date = a.Date;

        if (a.weather != null)
            air.weather = a.weather;
    }
});

app.Run();
