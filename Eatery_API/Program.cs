using Eatery_API.Domain.Interfaces;
using Eatery_API.Infrastructures.Data;
using Eatery_API.Infrastructures.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:EateryContext"] ?? throw new InvalidOperationException("Connection string 'EateryContext' not found.")));
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
