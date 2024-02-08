using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Catalog.API.Application.Contracts;
using Service.Catalog.API.Application.Mapper;
using Service.Catalog.API.Application.Services;
using Service.Catalog.API.Infrustructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

//? Register Service


//? Register IProductService
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddControllers();

//? Register AutoMapper
//IMapper mapper = MapConfiguration.RegisterMap().CreateMapper();
//builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContextPool<SqlServerApplicationDB>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogCon"));
},poolSize:16);

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:6002";
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false
    };
    options.RequireHttpsMetadata = false;

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "incodity");
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping InCodity - Catalog API" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' and your token'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                In = ParameterLocation.Header
            }, new List<string>()
        }
        
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
//app.MapGet("/", () => "CatalogAPI");
app.MapControllers();


app.Run();


