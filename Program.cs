using Microsoft.EntityFrameworkCore;
using EReaderNow.Interfaces;
using EReaderNow.mocks;
using EReaderNow.Data.AddDBMS;
using EReaderNow;
using EReaderNow.Data.Repository;
using EReaderNow.Data.Repository.EntytiFramework;
using EReaderNow.Data.Service;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Подключаем json конфиг
AddStartup.AppConfiguration.Bind("Project", new Config());

// Add Services to the container.

builder.Services.AddMvc();
// Связываем интерфейсы и реализацию
builder.Services.AddTransient<IBooksCategory, MockCategory>();
builder.Services.AddTransient<IAllDescriptionBooks, EFAllDescriptionBooks>();
builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
builder.Services.AddTransient<IRepositoryCategory, EFBooksItem>();
builder.Services.AddTransient<DataManager>();
// Add Bd
builder.Services.AddDbContext<AddDB>(x => x.UseSqlServer(Config.ConnectionBDStrings));
// identity system
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

}).AddEntityFrameworkStores<AddDB>().AddDefaultTokenProviders();
//настраиваем authentication cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "myCompanyAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/accounl/accessdenied";
  
    options.SlidingExpiration = true;
});
//добавляем сервисы для контроллеров и представлений (MVC)
builder.Services.AddControllersWithViews(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
});
           
//настраиваем политику авторизации для Admin area
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
    app.UseHttpsRedirection();
    // Подключаем поддержку статичных файлов
    app.UseStaticFiles();
//Поддержка маршрутизацми
    app.UseRouting();
    //Авторизация
    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        Console.WriteLine();
        endpoints.MapRazorPages();
    });

app.UseEndpoints(endpoints =>
{
    //Маршрутизация
    endpoints.MapControllerRoute(
       "admin",
       "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
    name: "PageIndex",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Home}/{id?}");
    endpoints.MapControllerRoute(
    name: "",
    pattern: "{controller=UseBook}/{action=Book}/{id?}");
    endpoints.MapControllerRoute(
   name: "",
   pattern: "{controller=Books}/{action=genre}/{id?}");
});
app.Run();
