using System;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.Helpers;
using CityApp_ANET.Services.CityService;
using CityApp_ANET.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("CityApp"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAllowAll",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin();
            corsPolicyBuilder.AllowAnyHeader();
            corsPolicyBuilder.AllowAnyMethod();
        });
});

var app = builder.Build();

SetupAppData(app, builder.Environment, builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsAllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
{
    using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();

    using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

    var logger = serviceScope.ServiceProvider.GetService<ILogger<Program>>();

    if (context == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize ApplicationDbContext");
    }

    if (logger == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize logger");
    }

    AppDataInit.SeedData(context, logger);
}

