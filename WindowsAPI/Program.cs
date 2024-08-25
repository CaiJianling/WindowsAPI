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

// ������´�����ָ��������ַ
var ipAddress = "0.0.0.0"; // ������������ӿ�
var port = 5010; // �����ѡ���κο��õĶ˿�
app.Urls.Add($"http://{ipAddress}:{port}");

app.Run();
