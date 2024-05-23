namespace TodoListApi.Application.DTOs;

public record CreateTodoDto(
    string Title,
    string Description,
    bool IsCompleted = false);