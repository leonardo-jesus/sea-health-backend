using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeaTemperatureDashboard.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conex�o
var connectionString = builder.Configuration.GetConnectionString("SeaTemperatureDB");
builder.Services.AddDbContext<SeaTemperatureContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Adicionar servi�os de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<SeaTemperatureContext>()
    .AddDefaultTokenProviders();

// Configurar pol�ticas de autoriza��o
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Adicionar servi�os de Razor Pages e MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();


// Middleware de diagn�stico para verificar autentica��o
app.Use(async (context, next) =>
{
    var user = context.User;
    if (user?.Identity?.IsAuthenticated == true)
    {
        Console.WriteLine($"Authenticated user: {user.Identity.Name}");
    }
    else
    {
        Console.WriteLine("User is not authenticated");

    }
    await next.Invoke();
});

// Mapear Razor Pages
app.MapRazorPages();

// Mapear rotas padr�o para MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
