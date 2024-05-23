using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Application.DTOs;
using TodoListApi.Application.Ports.Repositories;
using TodoListApi.Domain.Entity;

namespace TodoListApi.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public TodoController(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] CreateTodoDto createTodoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var todo = _mapper.Map<Todo>(createTodoDto);
        var result = await _todoRepository.CreateAsync(todo);
        return Ok(result);
    }
}