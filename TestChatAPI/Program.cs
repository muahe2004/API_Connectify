using TestChatAPI.BLL.Interfaces;
using TestChatAPI.BLL;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// User
builder.Services.AddScoped<IUser_Repo, User_Repo>();
builder.Services.AddScoped<IUser_Services, User_Services>();

// Post
builder.Services.AddScoped<IPosts_Repo, Posts_Repo>();
builder.Services.AddScoped<IPosts_Services, Posts_Services>();

var app = builder.Build();

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
