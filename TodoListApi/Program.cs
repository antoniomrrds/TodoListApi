using FluentValidation.AspNetCore;
using TodoListApi.Application.Mappings;
using TodoListApi.Application.Ports.Repositories;
using TodoListApi.Application.Validators;
using TodoListApi.Infrastructure.database;
using TodoListApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddAutoMapper(typeof(TodoProfile));
builder.Services.AddFluentValidation(fv => 
    fv.RegisterValidatorsFromAssemblyContaining<CreateTodoDtoValidator>());builder.Services.AddSingleton(servicesProvider =>
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