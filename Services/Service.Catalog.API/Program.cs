using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.API.Application.Mapper;
using Service.Catalog.API.Infrustructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

//? Register Service


//? Register AutoMapper
// IMapper mapper = MapConfiguration.RegisterMap().CreateMapper();
// builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContextPool<SqlServerApplicationDB>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogCon"));
},poolSize:16);

var app = builder.Build();

app.MapGet("/", () => "CatalogAPI");

app.Run();


