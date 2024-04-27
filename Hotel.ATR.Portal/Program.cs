using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Hotel.ATR.Portal.Models;
using Hotel.ATR.Portal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/*builder.Host.ConfigureLogging(logging =>
{

    string connectionString = builder.Configuration.GetConnectionString("DemoSeriLogDB");

    logging.ClearProviders();
    logging
    .AddDebug()
    .AddConsole()
    .AddEventLog()
    .AddSeq();

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Seq("http://localhost:5341/")
   *//*     .WriteTo.File("Logs/logs.txt",
        rollingInterval: RollingInterval.Day)

        .WriteTo.MSSqlServer
        (connectionString, sinkOptions: new
        MSSqlServerSink
        {
            TableName = "log"
        },
        null,
        null,
        LogEventLevel.Information, null,
        null,
        null,
        null)*//*
        .CreateLogger();

    Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq(" http://localhost:5341/")
    .WriteTo.File("Logs/logs.txt",
    rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }, null, null,
    LogEventLevel.Information, null, null, null, null)
    .CreateLogger();



});
*/
// Add services to the container.
void HeandlMapOpen(IApplicationBuilder appMap)
{
    appMap.Run(async context =>
    {
        await context.Response.WriteAsync("hello");
    });
}

string connectionStringDC = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HotelATRContext>(
    options => options.UseSqlServer(connectionStringDC));

builder.Services.Configure<APIEndpoint>(
builder.Configuration.GetSection("APIEndpoint"));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    /*  .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }, null, null,
          LogEventLevel.Information, null, null, null, null)*/
    .CreateLogger();


builder.Services.AddControllersWithViews();


builder.Services.AddMvc().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new[]
    {
        new CultureInfo("ru-Ru"),
        new CultureInfo("kz-Kz"),
        new CultureInfo("en-Us")
    };

    options.DefaultRequestCulture = new RequestCulture("ru-Ru", "ru-Ru");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;

});

//builder.Services.AddTransient<IRepository, Repository>();

builder.Services.AddLocalization(options =>
options.ResourcesPath = "Resources");


builder.Host.UseSerilog(Log.Logger);

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookieTempDataProviderOptions>
    (options =>
    {
        options.Cookie.IsEssential = true;
        options.Cookie.Domain = "localhost:56281";
        options.Cookie.Expiration =
        TimeSpan.FromSeconds(160);
    });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<TimeElapsed>();
builder.Services.AddScoped<CatchError>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = ".HotelATR.Session";
});

builder.Services.AddCors(cors =>
    {
        cors.AddPolicy("Policy_1",
        builder => builder.WithOrigins("http://localhost:56281/").WithMethods("GET"));
    }
);

builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
    });
//builder.Services.AddTransient<IRepository, Repository>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseCors("Policy_1");

 //app.UseMiddleware<ContentMiddleware>();

/*app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("Before invoke app.Use\n");
        await next();

        await context.Response.WriteAsync("After invoke app.Use\n");

    }
) ; 
app.Map("/m2", HeandlMapOpen);

app.Map("/m1", appMap =>
    appMap.Run(async context =>
    {
        await context.Response.WriteAsync("hello");
    })
);
*/

var localOptios = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

/*var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();*/

app.UseRequestLocalization(localOptios.Value);
/*app.Map("/Index", Index);*/

app.UseAuthentication();

app.UseAuthorization();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/Hello", async context =>
    {
        await context.Response
    });
});*/

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
