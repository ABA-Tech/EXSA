using Application.Auth;
using Application.Services;
using Domain.Common;
using Domain.Models;
using Domain.Services;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories;
using Infrastructure.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IDashboardStore, DashboardStore>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


const string CorsPolicyName = "ErpFrontend";
// Gestion du cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy
            .WithOrigins(
                "https://erp.mondomaine.com",
                "http://localhost:4200",
                "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "__Host-erp_xsrf";
    options.Cookie.HttpOnly = false;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "__Host-erp_auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        // En local Angular : localhost:4200 -> localhost:7118 = cross-origin.
        // Donc SameSite=None + Secure obligatoire.

        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);

        // Tu peux garder ces chemins, mais ils ne doivent pas provoquer de redirect API.
        options.LoginPath = "/api/auth/login";
        options.AccessDeniedPath = "/api/auth/access-denied";

        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            },

            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }
        };
    });

//builder.Services
//    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.Events.OnRedirectToLogin = context =>
//        {
//            context.Response.StatusCode = 401;
//            return Task.CompletedTask;
//        };

//        options.Events.OnRedirectToAccessDenied = context =>
//        {
//            context.Response.StatusCode = 403;
//            return Task.CompletedTask;
//        };

//        options.Cookie.Name = "__Host-erp_auth";
//        options.Cookie.HttpOnly = true;
//        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

//        // Si frontend et API sont sur le même site : Strict ou Lax.
//        // Si frontend et API sont sur des domaines différents : SameSite=None + HTTPS obligatoire.
//        options.Cookie.SameSite = SameSiteMode.Strict;

//        options.SlidingExpiration = true;
//        options.ExpireTimeSpan = TimeSpan.FromHours(8);

//        options.LoginPath = "/api/auth/login";
//        options.LogoutPath = "/api/auth/logout";
//        options.AccessDeniedPath = "/api/auth/access-denied";

//        // Très important pour une API : ne pas rediriger vers HTML.
//        options.Events = new CookieAuthenticationEvents
//        {
//            OnRedirectToLogin = context =>
//            {
//                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                return Task.CompletedTask;
//            },
//            OnRedirectToAccessDenied = context =>
//            {
//                context.Response.StatusCode = StatusCodes.Status403Forbidden;
//                return Task.CompletedTask;
//            }
//        };

//        options.Cookie.SameSite = SameSiteMode.Strict;
//        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//        options.Cookie.HttpOnly = true;

//        options.Events = new CookieAuthenticationEvents
//        {
//            OnValidatePrincipal = async context =>
//            {
//                var userIdValue = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

//                if (!Guid.TryParse(userIdValue, out var userId))
//                {
//                    context.RejectPrincipal();
//                    await context.HttpContext.SignOutAsync();
//                    return;
//                }

//                var repository = context.HttpContext.RequestServices
//                    .GetRequiredService<IUtilisateurRepository>();

//                var user = await repository.GetByIdAsync(
//                    userId,
//                    context.HttpContext.RequestAborted);

//                if (user is null || user.EstSupprime || user.EstActif != true)
//                {
//                    context.RejectPrincipal();
//                    await context.HttpContext.SignOutAsync();
//                }
//            }
//        };
//    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("ADMIN"));

    options.AddPolicy("Gestion", policy =>
        policy.RequireRole("ADMIN", "GESTIONNAIRE"));

    options.AddPolicy("Comptabilite", policy =>
        policy.RequireRole("ADMIN", "COMPTABLE"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors(CorsPolicyName);


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
