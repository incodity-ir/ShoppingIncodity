var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SqlServerApplicationDb>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IDPConnection"));
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<SqlServerApplicationDb>().AddDefaultTokenProviders();

//! Identity Server Configuration

var IDP = builder.Services.AddIdentityServer(options=>
{
   options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseFailureEvents = true;
}).AddInMemoryApiScopes(SD.ApiScopes).AddInMemoryClients(SD.Clients).AddInMemoryIdentityResources(SD.IdentityResources);

IDP.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDbInitializer,DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();
app.UseIdentityServer();
//DbRunInitializer.dbInitializer.Initialize();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
