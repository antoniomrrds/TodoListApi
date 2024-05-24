using System.Reflection;
using FluentValidation;
using TodoListApi.Application.Ports.Repositories;
using TodoListApi.Infrastructure.database;
using TodoListApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

builder.Services.AddSingleton(servicesProvider =>
{
    var configuration = servicesProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new ApplicationException("Connection string 'DefaultConnection' not found.");;
    return new SqlConnectionFactory(connectionString);
});

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