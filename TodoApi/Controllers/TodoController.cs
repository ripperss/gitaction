using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetAll()
    {
        return Ok(_todoService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<Todo> Create(Todo todo)
    {
        var createdTodo = _todoService.Create(todo);
        return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Todo todo)
    {
        var updatedTodo = _todoService.Update(id, todo);
        if (updatedTodo == null)
            return NotFound();

        return Ok(updatedTodo);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_todoService.Delete(id))
            return NotFound();

        return NoContent();
    }
} 