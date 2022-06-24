using BusinessAccessLayer.ProductInterface;
using BusinessAccessLayer.ProductService;
//using DairyForm.Services.BizObject;
using DataAccessLayer.DataStore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.EntityModels;
using Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextPool<DairyManDbContext>
        (Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"), 
                optionsBuilder => optionsBuilder.MigrationsAssembly("DataAccessLayer")));
builder.Services.AddIdentity<DairyMan, IdentityRole>() 
       .AddEntityFrameworkStores<DairyManDbContext>()
       .AddDefaultTokenProviders();
builder.Services.AddTransient<IDairyMan, DairyManService>();
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.AddControllersWithViews();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DairyMan}/{action=Welcome}/{id?}");

app.Run();

//builder.Services.AddTransient<DairyForm.Services.BizObject.IEmailService, EmailService>();
//builder.Services.AddHostedService<IDairyMan>();
//builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));