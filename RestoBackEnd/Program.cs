using Microsoft.EntityFrameworkCore;
using RestoBackEnd.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext with SQL Server
builder.Services.AddDbContext<RestoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS to allow Frontend to communicate
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin for development (includes file://)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register Services
builder.Services.AddScoped<RestoBackEnd.Services.IProductService, RestoBackEnd.Services.ProductService>();
builder.Services.AddScoped<RestoBackEnd.Services.ITableService, RestoBackEnd.Services.TableService>();
builder.Services.AddScoped<RestoBackEnd.Services.IOrderService, RestoBackEnd.Services.OrderService>();
builder.Services.AddScoped<RestoBackEnd.Services.IReportService, RestoBackEnd.Services.ReportService>();

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

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
