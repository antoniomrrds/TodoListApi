using FluentValidation;
using FluentValidation.AspNetCore;
using TodoListApi.Application.Ports.Repositories;
using TodoListApi.Infrastructure.database;
using TodoListApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddFluentValidationAutoValidation(config =>
{
  config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddSingleton(servicesProvider =>
{
    var configuration = servicesProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new ApplicationException("Connection string 'DefaultConnection' not found.");;
    return new SqlConnectionFactory(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();