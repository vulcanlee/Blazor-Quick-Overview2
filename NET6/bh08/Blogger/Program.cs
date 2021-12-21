using Blogger.Data;
using Blogger.Models;
using Blogger.Services;
using Blogger.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

#region 宣告 BlogDbContext 會用到的服務
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogDbContext>(
    options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);
#endregion

#region 加入客製化服務注入宣告
builder.Services.AddScoped<BlogViewModel>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IMyUserAuthenticationService, MyUserAuthenticationService>();
builder.Services.AddScoped<IMyUserService, MyUserService>();
#endregion

#region 加入使用 Cookie & JWT 認證需要的宣告
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
#endregion

#region 使用 HttpContext
builder.Services.AddHttpContextAccessor();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

#region 指定要使用使用者認證的中介軟體
app.UseAuthentication();
#endregion

#region 指定使用授權檢查的中介軟體
app.UseAuthorization();
#endregion

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
