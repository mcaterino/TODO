using Microsoft.Extensions.Options;
using ToDoAPI.Data;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Contracts;
using ToDoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var dbSettings = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value;
    options.UseSqlServer(dbSettings.ConnectionString);
});

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

AppDbInitializer.Seed(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


