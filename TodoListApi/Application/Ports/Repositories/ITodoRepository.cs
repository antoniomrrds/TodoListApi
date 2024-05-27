using TodoListApi.Domain.Entity;

namespace TodoListApi.Application.Ports.Repositories;

public interface ITodoRepository
{
    Task<int> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<int> UpdateAsync(Todo todo);
    Task<int> DeleteAsync(int id);
}