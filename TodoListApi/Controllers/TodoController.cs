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
    private readonly IValidator<TodoDto> _validator;

    public TodoController(ITodoRepository todoRepository, IMapper mapper, IValidator<TodoDto> validator)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
      _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TodoDto createTodoDto)
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
    public async Task<IActionResult> GetTodosAsync()
    {
      var todos = await _todoRepository.GetAllAsync();
      var model = _mapper.Map<IEnumerable<Todo>>(todos);
      return Ok(model);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTodoByIdAsync([FromRoute] int id)
    {
      var todo = await _todoRepository.GetByIdAsync(id);
      if (todo == null)
      {
        return NotFound();
      }
      var model = _mapper.Map<Todo>(todo);

      return Ok(model);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutAsync([FromBody] TodoDto updateTodoDto, [FromRoute] int id)
    {
      var todo = await _todoRepository.GetByIdAsync(id);
      if (todo == null)
      {
        return NotFound();
      }

      var validationResult = _validator.Validate(updateTodoDto);

      if (!validationResult.IsValid)
      {
        return BadRequest(validationResult);
      }

      _mapper.Map(updateTodoDto, todo); 
      var updatedTodo = await _todoRepository.UpdateAsync(todo);

      return Ok(updatedTodo);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
      var todo = await _todoRepository.GetByIdAsync(id);
      if (todo == null)
      {
        return NotFound("Todo não encontrado");
      }

      var deletedTodo = await _todoRepository.DeleteAsync(id);

      return Ok(deletedTodo);
    }
  }
}
