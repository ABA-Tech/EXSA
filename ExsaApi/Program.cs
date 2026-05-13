using Application.Services;
using Domain.Models;
using Domain.Services;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Stores;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ExsaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


// Injection des dépendances de services
builder.Services.AddScoped<IGenericService<Locataire>, LocataireService>();
builder.Services.AddScoped<IGenericService<Employe>, EmployeService>();
builder.Services.AddScoped<IAppService<Intervention>, AppService<Intervention>>();
builder.Services.AddScoped<IInterventionService, InterventionService>();
builder.Services.AddScoped<IAppService<Utilisateur>, AppService<Utilisateur>>();
builder.Services.AddScoped<IReferentielService, ReferentielService>();
builder.Services.AddScoped<IArticleStockService, ArticleStockService>();
builder.Services.AddScoped<IMouvementStockService, MouvementStockService>();

// Injection des dépendances de stores
builder.Services.AddScoped<IRepository<Intervention>, InterventionStore>();
builder.Services.AddScoped<IRepository<Utilisateur>, UtilisateurStore>();
builder.Services.AddScoped<ILocataireStore, LocataireStore>();
builder.Services.AddScoped<IEmployeStore, EmployeStore>();
builder.Services.AddScoped<IInterventionStore, InterventionStore>();
builder.Services.AddScoped<IReferentielStore, ReferentielStore>();
builder.Services.AddScoped<IArticleStockStore, ArticleStockStore>();
builder.Services.AddScoped<IMouvementStockStore, MouvementStockStore>();

builder.Services.AddScoped<IVehiculeStore, VehiculeStore>();
builder.Services.AddScoped<IDepenseVehiculeStore, DepenseVehiculeStore>();
builder.Services.AddScoped<IEntretienVehiculeStore, EntretienVehiculeStore>();

builder.Services.AddScoped<IVehiculeService, VehiculeService>();
builder.Services.AddScoped<IDepenseVehiculeService, DepenseVehiculeService>();
builder.Services.AddScoped<IEntretienVehiculeService, EntretienVehiculeService>();

builder.Services.AddScoped<IVehiculeDashboardStore, VehiculeDashboardStore>();
builder.Services.AddScoped<IDepenseParVehiculeStore, DepenseParVehiculeStore>();
builder.Services.AddScoped<IAlerteVehiculeStore, AlerteVehiculeStore>();

builder.Services.AddScoped<IVehiculeDashboardService, VehiculeDashboardService>();
builder.Services.AddScoped<IDepenseParVehiculeService, DepenseParVehiculeService>();
builder.Services.AddScoped<IAlerteVehiculeService, AlerteVehiculeService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Gestion du cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200") // URL de ton front Angular
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
