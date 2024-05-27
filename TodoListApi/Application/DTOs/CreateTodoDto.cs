namespace TodoListApi.Application.DTOs;

public record TodoDto(
    string Title,
    string Description,
    bool IsCompleted = false);