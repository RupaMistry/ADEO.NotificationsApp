using ADEO.NotificationsApp.DAL.Core;
using ADEO.NotificationsApp.DAL.Models;
using ADEO.NotificationsApp.Web.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database services
var dbConnection = builder.Configuration.GetConnectionString("NotificationsAppDBConnection");
builder.Services.SetupDatabase(dbConnection);

// Repository and ApplicationServices 
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserMessageRepository<UserMessage>, UserMessageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notifications}/{action=Index}/{id?}");

app.Run();