using Microsoft.EntityFrameworkCore;
using Neo_Watcher;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//������������ ���� �������
builder.Services.AddHttpClient<NeoSyncService>();
builder.Services.AddHostedService<NeoSyncJob>();

//������������� DbContext � Program.cs
builder.Services.AddDbContext<NeoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ������������� middleware (���� �����)
app.Run();

