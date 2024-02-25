using Fumble.Basket.Api.Repositories;
using Fumble.Basket.Api.Services;
using Fumble.Basket.Domain.Repositories;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddSingleton<IDiscountServiceClient, DiscountServiceClient>(_ =>
{
    string discountServiceUrl = builder.Configuration["DISCOUNT_SERVICE_URL"]!;
    return new DiscountServiceClient(GrpcChannel.ForAddress(discountServiceUrl));
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["REDIS_CONNECTIONSTRING"];
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();