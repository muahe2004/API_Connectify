using TestChatAPI.BLL.Interfaces;
using TestChatAPI.BLL;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.DAL;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CORS
builder.Services.AddCors(options =>
{
	// Cho phép tất cả các nguồn
	options.AddPolicy("AllowAllOrigins", policy =>
	{
		policy.AllowAnyOrigin()           // Cho phép tất cả các nguồn
			  .AllowAnyMethod()           // Cho phép tất cả các phương thức HTTP (GET, POST, PUT, DELETE, ...)
			  .AllowAnyHeader();          // Cho phép tất cả các header
	});

	// Hoặc nếu muốn chỉ cho phép các nguồn cụ thể
	/*options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://your-frontend-url.com", "http://localhost:3000")  // Các domain được phép
              .AllowAnyMethod()  // Cho phép tất cả phương thức HTTP
              .AllowAnyHeader();  // Cho phép tất cả headers
    });*/
});

// Thêm các dịch vụ vào container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm các dịch vụ cần thiết cho ứng dụng
builder.Services.AddScoped<IUser_Repo, User_Repo>();
builder.Services.AddScoped<IUser_Services, User_Services>();
builder.Services.AddScoped<IPosts_Repo, Posts_Repo>();
builder.Services.AddScoped<IPosts_Services, Posts_Services>();
builder.Services.AddScoped<IMessages_Repo, Messages_Repo>();
builder.Services.AddScoped<IMessages_Services, Messages_Services>();

var app = builder.Build();

// Áp dụng CORS
app.UseCors("AllowAllOrigins");  // Sử dụng chính sách "AllowAllOrigins"

// Cấu hình pipeline HTTP
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
