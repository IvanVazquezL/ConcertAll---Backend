using ConcertAll.Persistence;
using ConcertAll.Repositories;
using ConcertAll.Services.Implementation;
using ConcertAll.Services.Interface;
using ConcertAll.Services.Profiles;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register or configure my context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"), builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

// Registering my services
builder.Services.AddTransient<IGenreRepository,GenreRepository>();
builder.Services.AddTransient<IConcertRepository,ConcertRepository>();
builder.Services.AddTransient<IConcertService, ConcertService>();

builder.Services.AddAutoMapper(config =>
{
    //  Configuring the mapping profiles
    config.AddProfile<ConcertProfile>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//  Swagger configuration
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "ConcertAll API",
        Description = "This is the documentation for the ConcertAll API"
    });
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json","ConcertAll API Swagger");
        config.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
