using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WmsPlus.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddDbContext<WarehouseDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("WarehouseConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("WarehouseConnection"))
    )
);

// JWT 认证配置
var jwtKey = builder.Configuration["Jwt:Key"] ?? "WmsPlus_SecretKey_2024!@#$%^&*()_+QwErTyUiOp";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "WmsPlus.Api";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "WmsPlus";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        var origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>()
            ?? new[] { "http://localhost:5196", "http://localhost:5193", "http://localhost:5192" };
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 启用静态文件服务（用于测试和调试）
app.UseStaticFiles();

app.UseCors("AllowBlazorApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
