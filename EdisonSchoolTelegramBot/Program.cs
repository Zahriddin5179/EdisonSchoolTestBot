using EdisonSchoolTelegramBot.Abstractions;
using EdisonSchoolTelegramBot.Interfaces;
using EdisonSchoolTelegramBot.Services;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Telegram Bot
var botToken = builder.Configuration["TelegramBot:Token"]
    ?? throw new Exception("Telegram bot token topilmadi");

builder.Services.AddSingleton<ITelegramBotClient>(
    new TelegramBotClient(botToken)
);


// 🔹 PostgreSQL + EF Core
builder.Services.AddDbContext<BotDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")
    )
);

builder.Services.AddScoped<IUserService, UserService>();


// 🔹 Controllers
builder.Services.AddControllers();

// 🔹 Swagger (ixtiyoriy, qoldiramiz)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
