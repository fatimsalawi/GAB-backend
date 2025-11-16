using GAB.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB
var cs = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

// CORS (allow frontend Vite port)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowFrontend", p => p
        .WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175") // ✅ Add 5175 here
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Urls.Add("http://localhost:5272");
app.Urls.Add("https://localhost:7225");

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new { ok = true, msg = "GAB API running" }));
app.MapGet("/health", () => "ok");

app.MapControllers();
app.Run();
