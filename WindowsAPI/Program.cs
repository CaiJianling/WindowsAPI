var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// 添加以下代码来指定监听地址
var ipAddress = "0.0.0.0"; // 监听所有网络接口
var port = 5010; // 你可以选择任何可用的端口
app.Urls.Add($"http://{ipAddress}:{port}");

app.Run();
