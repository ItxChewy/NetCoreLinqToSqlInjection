using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services
//    .AddTransient<IRepositoryDoctores, RepositoryDoctoresOracle>();

builder.Services
    .AddTransient<IRepositoryDoctores, RepositoryDoctoresSQLServer>();

//builder.Services.AddTransient<Coche>();
//builder.Services.AddSingleton<Coche>();
builder.Services.AddSingleton<ICoche,Deportivo>();
//Coche car = new Coche();
//car.Marca = "DACIA";
//car.Modelo = "SANDERO";
//car.Imagen = "suv.jpg";
//car.Velocidad = 0;
//car.VelocidadMaxima = 180;
//builder.Services.AddSingleton<ICoche, Coche>(i => car);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Doctores}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
