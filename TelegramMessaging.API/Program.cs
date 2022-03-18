using TelegramMessaging.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TelegramMessaging.API.Consumer;
using MediatR;
using System.Reflection;
using TelegramMessaging.API.Application.Queries;
using TelegramMessaging.Domain.AggregatesModel;
using TelegramMessaging.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MessagesContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IMessageQueries,MessageQueries>(s =>
{
    return new MessageQueries(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSingleton<IHostedService, MessageConsumer>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MessagesContext>();
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
