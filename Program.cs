using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PraticeSessionDemo.Data;
using PraticeSessionDemo.Middleware;
using PraticeSessionDemo.Services.StudentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(option =>
                    option.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<LogTimeTakingByAPI>();
app.MapControllers();

app.Run();
