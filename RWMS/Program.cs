using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RWMS.Data;
using RWMS.Extensions;
using RWMS.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Identity ──────────────────────────────────────────────────────────────────
// Using AddIdentity (not AddDefaultIdentity) to get full RoleManager<ApplicationRole> support
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password policy
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    // Lockout policy
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User policy
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ── Authorization Policies ────────────────────────────────────────────────────
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("OwnerAccess", policy => policy.RequireRole("Owner"))
    .AddPolicy("ManagerAccess", policy => policy.RequireRole("Owner", "Manager"))
    .AddPolicy("OrderAccess", policy => policy.RequireRole("Owner", "Manager", "Client"));

// ── Application Services ──────────────────────────────────────────────────────
builder.Services.AddApplicationServices();

// ── MVC ───────────────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Required for Identity UI scaffolding

// ── QuestPDF License ──────────────────────────────────────────────────────────
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

var app = builder.Build();

// ── Seed Roles ────────────────────────────────────────────────────────────────
await SeedRolesAsync(app);

// ── HTTP Pipeline ─────────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages(); // Required for Identity UI

app.Run();

// ── Role Seeding ──────────────────────────────────────────────────────────────
static async Task SeedRolesAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    var roles = new[]
    {
        new ApplicationRole { Name = "Owner",   Description = "Restaurant owner — full access including financial reports" },
        new ApplicationRole { Name = "Manager", Description = "Restaurant manager — products, orders, and supply management" },
        new ApplicationRole { Name = "Client",  Description = "Wholesale client — browse products and manage own orders" },
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role.Name!))
        {
            await roleManager.CreateAsync(role);
        }
    }
}
