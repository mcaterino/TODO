using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Contracts;

namespace ToDoAPI.Controllers
{

    [ApiController]
  [Route("api/[controller]")]
  
  public class TodoController : ControllerBase
  {
    private readonly ITodoService _todoService;
    

    public TodoController(ITodoService todoService)
    {
      _todoService = todoService;
    }

    // Get all Todo Items
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
      try
      {
        var todos = await _todoService.GetAllAsync();
        if  (todos == null || !todos.Any())
        {
          return Ok(new {message = "Not Todo Items found"});
          
        }
        return Ok(new { message = "Successfully retrieved all Todos", data = todos });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while retrieving all Todos", error = ex.Message });
      }
    }

    // Get a Todo By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
      try
      {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo == null)
        {
          return NotFound(new { message = $"Todo with id {id} not found" });
        }
        return Ok(new { message = "Successfully retrieved todo item", data = todo });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while retrieving todo item", error = ex.Message });
      }
    }

    // Create a Todo Item
    [HttpPost]
    public async Task<IActionResult> CreateTodoAsync([FromBody] CreateTodoRequest request)
    {

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await _todoService.CreateTodoAsync(request);
        return Ok(new { message = "Successfully created todo item" });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while creating todo item", error = ex.Message });
      }
    }

    // Update a Todo Item
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoAsync(Guid id, [FromBody] UpdateTodoRequest request)
    {
      try
      {
        var todo = await _todoService.UpdateTodoAsync(id, request);
        if (todo == null)
        {
          return NotFound(new { message = $"Todo with id {id} not found" });
        }
        return Ok(new { message = "Successfully updated todo item", data = todo });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while updating todo item", error = ex.Message });
      }
    }

    // Delete a Todo Item
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoAsync(Guid id)
    {
      try
      {
        var isDeleted = await _todoService.DeleteTodoAsync(id);
        if (!isDeleted)
        {
          return NotFound(new { message = $"Todo with id {id} not found" });
        }
        return Ok(new { message = "Successfully deleted todo item" });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while deleting todo item", error = ex.Message });
      }
    }
  }
}