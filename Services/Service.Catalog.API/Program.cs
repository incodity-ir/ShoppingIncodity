using Microsoft.EntityFrameworkCore;
using Service.Catalog.API.Infrustructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//? Register Service

builder.Services.AddDbContextPool<SqlServerApplicationDB>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogCon"));
},poolSize:16);

var app = builder.Build();

app.MapGet("/", () => "CatalogAPI");

app.Run();


