using GAB.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB
var cs = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

// CORS (allow frontend)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowFrontend", p => p
        .WithOrigins(
            "http://localhost:5173",
            "http://localhost:5174",
            "http://localhost:5175"
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ❌ REMOVE THESE (Render will break)
// app.Urls.Add("http://localhost:5272");
// app.Urls.Add("https://localhost:7225");

// Render: always use HTTP only
// app.UseHttpsRedirection();  // ❌ Do NOT use on Render

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health check
app.MapGet("/", () => Results.Ok(new { ok = true, msg = "GAB API running" }));
app.MapGet("/health", () => "ok");

app.MapControllers();

// Required for Render (port provided by environment)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
