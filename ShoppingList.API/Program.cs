using Microsoft.EntityFrameworkCore;
using ShoppingList.Infrastructure;

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

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ShoppingListContext>();
    context.Database.Migrate();
    var shoppingList = new ShoppingList.Domain.AggregatesModel.ShoppingList();
    shoppingList.AddItem("Teste", 2);
    shoppingList.AddItem("Teste Dois", 4);
    context.Add(shoppingList);
    context.SaveChanges();
    Console.WriteLine($"Items in DB {context.ShoppingLists.Count()}");
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
