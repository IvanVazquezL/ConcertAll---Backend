using ConcertAll.Persistence;
using ConcertAll.Repositories;
using ConcertAll.Services.Implementation;
using ConcertAll.Services.Interface;
using ConcertAll.Services.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register or configure my context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//  Setting context
builder.Services.AddHttpContextAccessor();

//  Identity
//  builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddIdentity<ConcertAllUserIdentity, IdentityRole>(policies =>
{
    policies.Password.RequireDigit = true;
    policies.Password.RequiredLength = 6;
    policies.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"] ??
        throw new InvalidOperationException("JWT key not configured"));
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(corsConfiguration);

app.MapControllers();

app.Run();
