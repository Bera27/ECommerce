using System.Text;
using System.Text.Json.Serialization;
using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.Services;
using Ecommerce.Application.UseCases.Customers;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureControllers(builder);
ConfigureAuthentication(builder);
ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

void ConfigureControllers(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    string? jwtKeyConfig = builder.Configuration["Jwt:Key"];

    if(string.IsNullOrEmpty(jwtKeyConfig))
        throw new InvalidOperationException("A chave JWT não está configurada.");

    var key = Encoding.ASCII.GetBytes(jwtKeyConfig);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

    builder.Services.AddAuthorization();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<EcommerceDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICartRepository, CartRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();

    builder.Services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();
    builder.Services.AddTransient<ITokenService, TokenService>();

    builder.Services.AddScoped<RegisterCustomer>();
    builder.Services.AddScoped<Login>();
}