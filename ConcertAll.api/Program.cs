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
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//  Setting context
builder.Services.AddHttpContextAccessor();

// Registering my services
builder.Services.AddTransient<IGenreRepository,GenreRepository>();
builder.Services.AddTransient<IConcertRepository,ConcertRepository>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ISaleRepository, SaleRepository>();

builder.Services.AddTransient<IConcertService, ConcertService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<ISaleService, SaleService>();

builder.Services.AddAutoMapper(config =>
{
    //  Configuring the mapping profiles
    config.AddProfile<ConcertProfile>();
    config.AddProfile<GenreProfile>();
    config.AddProfile<SaleProfile>();
});

// CORS
var corsConfiguration = "ConcertAllCors";
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader().WithExposedHeaders(new string[] { "TotalRecordsQuantity" });
        policy.AllowAnyMethod();

    });
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
