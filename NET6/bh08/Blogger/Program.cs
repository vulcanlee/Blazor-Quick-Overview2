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

#region �ŧi BlogDbContext �|�Ψ쪺�A��
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogDbContext>(
    options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);
#endregion

#region �[�J�Ȼs�ƪA�Ȫ`�J�ŧi
builder.Services.AddScoped<BlogViewModel>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IMyUserAuthenticationService, MyUserAuthenticationService>();
builder.Services.AddScoped<IMyUserService, MyUserService>();
#endregion

#region �[�J�ϥ� Cookie & JWT �{�һݭn���ŧi
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
#endregion

#region �ϥ� HttpContext
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

#region ���w�n�ϥΨϥΪ̻{�Ҫ������n��
app.UseAuthentication();
#endregion

#region ���w�ϥα��v�ˬd�������n��
app.UseAuthorization();
#endregion

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
