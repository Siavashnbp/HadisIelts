using FluentValidation.AspNetCore;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Server.Services.Email;
using HadisIelts.Server.Services.Files;
using HadisIelts.Server.Services.Payment;
using HadisIelts.Server.Services.Telegram;
using HadisIelts.Server.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("WebsiteSqlServer") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    {
        options.IdentityResources["openid"].UserClaims.Add("role");
        options.ApiResources.Single().UserClaims.Add("role");
    });
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministrtorRole",
        policy => policy.RequireRole("Administrator"));
});

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllers().AddFluentValidation(fv =>
fv.RegisterValidatorsFromAssembly(Assembly.Load("HadisIelts.Shared")));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    //Pasword settings
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

    //User Settings
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.SignIn.RequireConfirmedEmail = true;
});

//Email configuration
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
//Telegram configuration
var telegramConfig = builder.Configuration.GetSection("TelegramConfigurations").Get<TelegramConfiguration>();

builder.Services.AddScoped<IUserServices, UserServicesProvider>();
builder.Services.AddTransient(typeof(ICustomRepositoryServices<,>), typeof(RepositoryServiceProvider<,>));
builder.Services.AddScoped<IWordFileServices, WordFileServiceProvider>();
builder.Services.AddScoped<IWritingCorrectionPayment, WritingCorrectionPaymentServiceProvider>();
builder.Services.AddSingleton(emailConfig!);
builder.Services.AddScoped<IEmailServices, EmailServiceProvider>();
builder.Services.AddSingleton(telegramConfig!);
builder.Services.AddScoped<ITelegramServices, TelegramServiceProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
