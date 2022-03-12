using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.API.Application.Queries;
using ShoppingList.Domain.AggregatesModel;
using ShoppingList.Infrastructure;
using ShoppingList.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShoppingListContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IShoppingListQueries, ShoppingListQueries>(s =>
{
    return new ShoppingListQueries(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ShoppingListContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
