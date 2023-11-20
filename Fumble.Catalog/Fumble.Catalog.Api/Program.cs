using Fumble.Catalog.Database;
using Fumble.Catalog.Database.Repositories;
using Fumble.Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<FumbleDbContext>(options =>
{
    string dbHost = builder.Configuration["CATALOG_DB_HOST"];
    string user = builder.Configuration["CATALOG_DB_USER"];
    string password = builder.Configuration["CATALOG_DB_PASSWORD"];
    string dbName = builder.Configuration["CATALOG_DB_NAME"];

options.UseNpgsql(@$"Host={dbHost};Username={user};Password={password};Database={dbName}",
        x => x.MigrationsAssembly("Fumble.Catalog.Database.Migrations"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();