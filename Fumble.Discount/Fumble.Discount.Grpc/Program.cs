using Fumble.Discount.Database.DataContext;
using Fumble.Discount.Database.Repositories;
using Fumble.Discount.Domain.Repositories;
using Fumble.Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddSingleton(new CouponDatabaseConfigurations(
        builder.Configuration["DISCOUNT_DB_CONNECTION_STRING"]!,
        builder.Configuration["DISCOUNT_DB_NAME"]!,
        builder.Configuration["DISCOUNT_DB_COUPONS_COLLECTION"]!
    ));
builder.Services.AddSingleton<ICouponDbContext, CouponDbContext>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();
app.MapGrpcService<DiscountService>();


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();