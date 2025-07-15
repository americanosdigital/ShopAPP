using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using ShopAPP.Application.Interfaces.Account;
using ShopAPP.Application.Interfaces.Customers;
using ShopAPP.Application.Interfaces.Email;
using ShopAPP.Application.Interfaces.Orders;
using ShopAPP.Application.Interfaces.ProductCategories;
using ShopAPP.Application.Interfaces.Products;
using ShopAPP.Application.Mappings;
using ShopAPP.Application.Mappings.Products;
using ShopAPP.Application.Services.Account;
using ShopAPP.Application.Services.Customers;
using ShopAPP.Application.Services.Email;
using ShopAPP.Application.Services.Orders;
using ShopAPP.Application.Services.ProductCategories;
using ShopAPP.Application.Services.Products;
using ShopAPP.Domain.Interfaces;
using ShopAPP.Infrastructure.Data.DataContext;
using ShopAPP.Infrastructure.Identity.Models;
using ShopAPP.Infrastructure.Identity.Seed;
using ShopAPP.Infrastructure.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ============================
// Services Configuration
// ============================

builder.Services.AddControllers();

builder.Services.AddDbContext<ShopAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopAppDbContext>()
    .AddDefaultTokenProviders();

// ✅ JWT
var jwtKey = builder.Configuration["JwtSettings:SecretKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
    };
});

// ✅ Swagger + Scalar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShopAPP API",
        Version = "v1",
        Description = "API RESTful do ShopAPP em ASP.NET Core 9",
        Contact = new OpenApiContact
        {
            Name = "Wellington Americano",
            Email = "americanosdigital@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT no campo abaixo:\n\nExemplo: **Bearer eyJhbGciOiJIUzI1NiIs...**"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddOpenApi();

// ✅ AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ✅ Application Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:5137")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ============================
// Criação automática de pastas de uploads
// ============================
var wwwrootPath = app.Environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
var uploadBasePath = Path.Combine(wwwrootPath, "uploads");

var uploadFolders = new[] { "products", "users", "customers" };

foreach (var folder in uploadFolders)
{
    var fullPath = Path.Combine(uploadBasePath, folder);
    if (!Directory.Exists(fullPath))
    {
        Directory.CreateDirectory(fullPath);
    }
}

// ============================
// Seeder das Roles e Usuários
// ============================
try
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await IdentitySeeder.SeedRolesAsync(roleManager);
    await IdentitySeeder.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao rodar Seed: {ex.Message}");
    throw;
}

// ============================
// App Pipeline
// ============================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapScalarApiReference(options => options.WithTheme(ScalarTheme.DeepSpace));

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
