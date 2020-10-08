using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoCore3.Api.Models;

namespace todoCore3.Api.Controllers
{
  [Authorize]
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class TodoItemsController : ControllerBase
  {
    private readonly TodoContext _context;

    public TodoItemsController(TodoContext context)
    {
      _context = context;
    }

    private bool TodoItemExists(long id) => _context.TodoItems.Any(e => e.Id == id);

    private static TodoItemDto ItemToDTO(TodoItem todoItem) => new TodoItemDto
    {
      Id = todoItem.Id,
      Name = todoItem.Name,
      Description = todoItem.Description,
      IsComplete = todoItem.IsCompleted,
      FlowId = todoItem.FlowId,
      CreatedAt = todoItem.CreatedAt,
      UpdatedAt = todoItem.UpdatedAt
    };

    /// <summary>
    /// 모든 Todo Item 을 불러옵니다.
    /// </summary>
    /// <returns></returns>
    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
      return await _context.TodoItems.Select(x => ItemToDTO(x)).ToListAsync();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
      var todoItem = await _context.TodoItems.FindAsync(id);

      if (todoItem == null)
      {
        return NotFound();
      }

      return ItemToDTO(todoItem);
    }

    // PUT: api/TodoItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDTO)
    {
      if (id != todoItemDTO.Id)
      {
        return BadRequest();
      }

      var todoItem = await _context.TodoItems.FindAsync(id);
      if (todoItem == null)
      {
        return NotFound();
      }

      todoItem.Name = todoItemDTO.Name;
      todoItem.Description = todoItemDTO.Description;
      todoItem.IsCompleted = todoItemDTO.IsComplete;
      todoItem.FlowId = todoItemDTO.FlowId;
      todoItem.UpdatedAt = DateTime.Now;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    /// <summary>
    /// Todo item 을 생성합니다.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///		POST api/TodoItems
    ///		{
    ///			"name": "Item no 1",
    ///			"isCompleted": false
    ///		}
    ///
    /// </remarks>
    /// <param name="todoItemDTO"></param>
    /// <returns>생성된 Todo item</returns>
    /// <response code="201">생성된 Todo item</response>
    /// <response code="400">todo item 이 null 일 경우</response>
    // POST: api/TodoItems
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItemDto todoItemDTO)
    {
      var todoItem = new TodoItem
      {
        IsCompleted = todoItemDTO.IsComplete,
        Name = todoItemDTO.Name,
        Description = todoItemDTO.Description,
        FlowId = todoItemDTO.FlowId,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
      };

      _context.TodoItems.Add(todoItem);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
      var todoItem = await _context.TodoItems.FindAsync(id);
      if (todoItem == null)
      {
        return NotFound();
      }

      _context.TodoItems.Remove(todoItem);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
