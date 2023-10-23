using LakeStat.Models;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
List<LakeStatistics> LakeStats = new List<LakeStatistics>()
{
    new LakeStatistics()
    {
        Id = 1,
        Name = "Lake Huron",
        Celcius = false,
        Temperature = 92,
        WeatherDate = DateTime.Today.AddDays(-20),
        Waves = Wave.Calm

    },
    new LakeStatistics()
    {
        Id = 2,
        Name = "Lake Ontario",
        Celcius = false,
        Temperature = 72,
        WeatherDate = DateTime.Today.AddDays(-30),
        Waves = Wave.Rough
    },
    new LakeStatistics()
    {
        Id = 3,
        Name = "Lake Michigan",
        Celcius = false,
        Temperature = 81,
        WeatherDate = DateTime.Today.AddDays(-10),
        Waves = Wave.Choppy
    },
    new LakeStatistics()
    {   
        Id = 4,
        Name = "Lake Erie",
        Celcius = false,
        Temperature = 73,
        WeatherDate = DateTime.Today.AddDays(-7),
        Waves = Wave.Rough

    },
        new LakeStatistics()
    {        
        Id = 5,
        Name = "Lake Superior",
        Celcius = false,
        Temperature = 4,
        WeatherDate = DateTime.Today.AddDays(-20),
        Waves = Wave.Tidal

    }


};

app.MapGet("/Lake", async () => {
    await Task.Delay(1000);
    return LakeStats;
    });


app.MapPost("/Lake", async (LakeStatistics l) => {
    await Task.Delay(2000);
    LakeStats.Add(l);
    });   

app.MapPut("/Lake/{id}", async  (int id, LakeStatistics l) =>
{
    await Task.Delay(1000);
    var lake = LakeStats.Where(lake => lake.Id == id).FirstOrDefault();
    
    if (lake != null)
    {
        if(l.Name != null)
            lake.Name = l.Name;

        if (l.Celcius != null)
            lake.Celcius = l.Celcius;

        if (l.Temperature != null)
            lake.Temperature = l.Temperature;

        if (l.WeatherDate != null)
            lake.WeatherDate = l.WeatherDate;

        if (l.Waves != null)
            lake.Waves = l.Waves;   
    }
});

app.Run();


