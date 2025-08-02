using Microsoft.EntityFrameworkCore;
using Neo_Watcher;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Регистрируем свои сервисы
builder.Services.AddHttpClient<NeoSyncService>();
builder.Services.AddHostedService<NeoSyncJob>();

//Зарегистрируй DbContext в Program.cs
builder.Services.AddDbContext<NeoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Конфигурируем middleware (если нужно)
app.Run();

