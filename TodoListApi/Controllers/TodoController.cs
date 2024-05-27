using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Application.DTOs;
using TodoListApi.Application.Ports.Repositories;
using TodoListApi.Domain.Entity;

namespace TodoListApi.Controllers
{
  [ApiController]
  [Route("api/todo")]
  public class TodoController : ControllerBase
  {
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTodoDto> _validator;

    public TodoController(ITodoRepository todoRepository, IMapper mapper, IValidator<CreateTodoDto> validator)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
      _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateTodoDto createTodoDto)
    {
      var validationResult = _validator.Validate(createTodoDto);

      if (!validationResult.IsValid)
      {
        return BadRequest(validationResult);
      }

      var todo = _mapper.Map<Todo>(createTodoDto);
      var createTodo = await _todoRepository.CreateAsync(todo);

      return Ok(createTodo);
    }

    [HttpGet]
    public async Task<IEnumerable<Todo>> GetTodosAsync()
    {
      return await _todoRepository.GetAllAsync();
    }

  }
}
