using _net_angular_demo.Data;
using _net_angular_demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add db context

builder.Services.AddDbContext<Entities>(options =>
 options.UseInMemoryDatabase(databaseName: "_net_angular_demo"),
 ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen( c =>
    { c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer{
        Description = "Development Server",
        Url = "https://localhost:7138"
    });
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
    }
);

// adding entity singleton
builder.Services.AddSingleton<Entities>();

var app = builder.Build();

// seeding data

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var rand = new Random();
Flight[] flightsSeed = new Flight[]{
            new (Guid.NewGuid(),
                "American Airlines",
                rand.Next(90,5000).ToString(),
                new TimePlace("Los Angeles", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Istanbul", DateTime.Now.AddHours(rand.Next(4,10))),
                2
            ),
            new (Guid.NewGuid(),
                "Deutsche BA",
                rand.Next(90,5000).ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Schiphol", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "British Airways",
                rand.Next(90,5000).ToString(),
                new TimePlace("London", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Rome", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Basiq Air",
                rand.Next(90,5000).ToString(),
                new TimePlace("Amsterdam", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Glasgow", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "BB Heliag",
                rand.Next(90,5000).ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Baku", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
            new (Guid.NewGuid(),
                "Adria Airways",
                rand.Next(90,5000).ToString(),
                new TimePlace("Ljubljana", DateTime.Now.AddHours(rand.Next(1,3))),
                new TimePlace("Warsaw", DateTime.Now.AddHours(rand.Next(4,10))),
                rand.Next(1,853)
            ),
    };

entities?.flights.AddRange(flightsSeed);

entities?.SaveChanges();

// setting up cors
app.UseCors(bulder => bulder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
