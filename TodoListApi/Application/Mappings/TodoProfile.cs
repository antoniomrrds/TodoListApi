using AutoMapper;
using TodoListApi.Application.DTOs;
using TodoListApi.Domain.Entity;

namespace TodoListApi.Application.Mappings;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<CreateTodoDto, Todo>();
    }
}