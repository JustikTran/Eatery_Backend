using CloudinaryDotNet;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.VNPay;
using Eatery_API.Infrastructures.Data;
using Eatery_API.Infrastructures.Providers;
using Eatery_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:EateryContext"] ?? throw new InvalidOperationException("Connection string 'EateryContext' not found.")));

//Configuration Cloudinary  
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton<Cloudinary>(serviceProvider =>
{
    var cloudinarySettings = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<CloudinarySettings>>().Value;
    return new Cloudinary(new Account(
        cloudinarySettings.CloudName,
        cloudinarySettings.ApiKey,
        cloudinarySettings.ApiSecret));
});

// Register OData model builder
var odataBuilder = new ODataConventionModelBuilder();

odataBuilder.EntitySet<ResponseAccount>("account");
odataBuilder.EntitySet<ResponseCart>("cart");
odataBuilder.EntitySet<ResponseCartTopping>("cart-topping");
odataBuilder.EntitySet<ResponseOrder>("order");
odataBuilder.EntitySet<ResponseOrderItem>("order-item");
odataBuilder.EntitySet<ResponseOrderTopping>("order-topping");
odataBuilder.EntitySet<ResponseDish>("dish");
odataBuilder.EntitySet<ResponseTopping>("topping");
odataBuilder.EntitySet<ResponsePaymentMethod>("payment-method");
odataBuilder.EntitySet<ResponseUserAddress>("user-address");
odataBuilder.EntitySet<ResponseUser>("user");

builder.Services.AddControllers()
    .AddOData(options => options
        .SetMaxTop(100)
        .Filter()
        .OrderBy()
        .Count()
        .Expand()
        .Select()
        .AddRouteComponents("odata", odataBuilder.GetEdmModel())
        );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountProvider, AccountProvider>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IUserAddressProvider, UserAddressProvider>();
builder.Services.AddScoped<IDishProvider, DishProvider>();
builder.Services.AddScoped<IToppingProvider, ToppingProvider>();
builder.Services.AddScoped<ICartProvider, CartProvider>();
builder.Services.AddScoped<ICartToppingProvider, CartToppingProvider>();
builder.Services.AddScoped<IOrderProvider, OrderProvider>();
builder.Services.AddScoped<IOrderItemProvider, OrderItemProvider>();
builder.Services.AddScoped<IOrderToppingProvider, OrderToppingProvider>();
builder.Services.AddScoped<IPaymentMethodProvider, PaymentMethodProvider>();
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<IVNPayHelper, VNPayHelper>();
builder.Services.AddScoped<ElasticEmailService>();

// Confiure cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
         .AllowCredentials();
    });
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Config JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero
    };
});

// Add Authorization
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
    //context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseODataRouteDebug();

app.UseCors("AllowFrontend");
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
