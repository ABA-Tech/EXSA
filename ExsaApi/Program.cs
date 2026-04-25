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
builder.Services.AddScoped<IAppService<Utilisateur>, AppService<Utilisateur>>();
builder.Services.AddScoped<IReferentielService, ReferentielService>();

// Injection des dépendances de stores
builder.Services.AddScoped<IRepository<Utilisateur>, UtilisateurStore>();
builder.Services.AddScoped<ILocataireStore, LocataireStore>();
builder.Services.AddScoped<IEmployeStore, EmployeStore>();
builder.Services.AddScoped<IReferentielStore, ReferentielStore>();


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

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
