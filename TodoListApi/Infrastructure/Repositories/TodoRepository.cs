using System.Text;
using Dapper;
using TodoListApi.Application.Ports.Repositories;
using TodoListApi.Domain.Entity;
using TodoListApi.Infrastructure.database;

namespace TodoListApi.Infrastructure.Repositories;

public class TodoRepository: ITodoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;
    
    public TodoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<int> CreateAsync(Todo todo)
    {
        todo.CreatedAt = DateTime.Now;
        todo.UpdatedAt = DateTime.Now;
        var sql = new StringBuilder();
        sql.AppendLine("INSERT INTO tbl_todo(");
        sql.AppendLine("       Title,");
        sql.AppendLine("       Description,");
        sql.AppendLine("       IsCompleted,");
        sql.AppendLine("       CreatedAt,");
        sql.AppendLine("       UpdatedAt");
        sql.AppendLine(") VALUES (");
        sql.AppendLine("       @Title,");
        sql.AppendLine("       @Description,");
        sql.AppendLine("       @IsCompleted,");
        sql.AppendLine("       @CreatedAt,");
        sql.AppendLine("       @UpdatedAt");
        sql.AppendLine(");");
        sql.AppendLine("SELECT LAST_INSERT_ID();");
        var connection = _connectionFactory.Create();
        return connection.ExecuteAsync(sql.ToString(), todo);
    }

    public Task<IEnumerable<Todo>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Todo> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}