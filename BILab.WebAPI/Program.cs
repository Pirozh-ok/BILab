using BILab.Domain.Settings;
using BILab.Web.Extensions;
using BILab.WebAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtBearerSettings"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Add services to the container.
//builder.Services.AddControllersWithViews()
// .AddJsonOptions(options =>
// options.JsonSerializerOptions.DefaultIgnoreCondition = .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddIdentitySettings();
builder.Services.AddAutoMapper();
builder.Services.AddUserServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerOptions();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllOrigins",
    builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddOptions();

var jwtSection = builder.Configuration.GetSection("JwtBearerSettings");
builder.Services.AddJwtAuth(jwtSection);
builder.Services.Configure<DataProtectionTokenProviderOptions>(options => {
    options.TokenLifespan = TimeSpan.FromDays(int.Parse(builder.Configuration["SmtpSettings:ConfirmEmailTokenValidityInDay"]));
});

var app = builder.Build();

// Seed necessary data
await builder.Services.SeedDataAsync();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();