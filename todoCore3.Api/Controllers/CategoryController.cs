using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
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
  public class CategoryController : ControllerBase
  {
    private readonly TodoContext _context;
    private IMapper _mapper;

    public CategoryController(TodoContext context, IMapper mapper)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private bool CategoryExists(long id) => _context.Categories.Any(c => c.Id == id);

    private static CategoryDTO CategoryToDTO(Category category) => new CategoryDTO
    {
      Id = category.Id,
      Name = category.Name,
      BgColor = category.BgColor,
      UserId = category.UserId,
      CreatedAt = category.CreatedAt,
      UpdatedAt = category.UpdatedAt
    };

    /// <summary>
    /// 모든 카테고리를 불러옵니다.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
      //var list = _context.Categories
      //  .GroupJoin(_context.TodoItems, category => category.Id, todoItem => todoItem.CategoryId, (x, y) => new { Category = x, TodoItems = y })
      //  .SelectMany(x => x.TodoItems.DefaultIfEmpty(), (x, y) => new { Category = x.Category, TodoItems = y });

      //var categoryList = _context.Categories.Where(c => c.UserId == 1).ToList();
      //var items = _context.TodoItems.Where(x => categoryList.Select(c => c.Id).ToList().Contains(x.CategoryId)).ToList();

      //return await categoryList.Select(c => new
      //{
      //  Id = c.Id,
      //  Name = c.Id,
      //  BgColor = c.BgColor,
      //  UserId = c.UserId,
      //  CreatedAt = c.CreatedAt,
      //  UpdatedAt = c.UpdatedAt,
      //  TodoItems = items.Where(t => t.CategoryId == c.Id)
      //}).ToList();

      var groupJoin = _context.Categories.Where(x => x.UserId == 1).ToList().GroupJoin(_context.TodoItems, c => c.Id, t => t.CategoryId, (c, t) => new { Category = c, Items = t });

      return await _context.Categories.Select(x => CategoryToDTO(x)).ToListAsync();
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<CategoryDTO>> GetCategory(long id)
    //{
    //  var category = await _context.Categories.FindAsync(id);

    //  if (category == null)
    //  {
    //    return NotFound();
    //  }

    //  return CategoryToDTO(category);
    //}

    //[Route("GetCategoryBy")]
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryWithItems>> GetCategory(long id)
    {
      var category = await _context.Categories.FindAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      var items = await _context.TodoItems.Where(x => x.CategoryId == id).ToListAsync();

      return new CategoryWithItems
      {
        Id = category.Id,
        Name = category.Name,
        BgColor = category.BgColor,
        UserId = category.UserId,
        CreatedAt = category.CreatedAt,
        UpdatedAt = category.UpdatedAt,
        TodoItems = _mapper.Map<IEnumerable<TodoItemDTO>>(items)
      };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(long id, CategoryDTO categoryDTO)
    {
      if (id != categoryDTO.Id)
      {
        return BadRequest();
      }

      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }

      category.Name = categoryDTO.Name;
      category.BgColor = categoryDTO.BgColor;
      category.UserId = categoryDTO.UserId;
      category.UpdatedAt = DateTime.Now;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!CategoryExists(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    /// <summary>
    /// Category 를 생성합니다.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///   POST api/Category
    ///   {
    ///     name: "Category 1"
    ///   }
    ///   
    /// </remarks>
    /// <param name="categoryDTO"></param>
    /// <returns>생성된 Category</returns>
    /// <response code="201">생성된 Category</response>
    /// <response code="400">category 가 null 일 경우</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> CreateCategory(CategoryDTO categoryDTO)
    {
      var category = new Category
      {
        Name = categoryDTO.Name,
        BgColor = categoryDTO.BgColor,
        UserId = categoryDTO.UserId,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
      };

      _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, CategoryToDTO(category));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
      var category = await _context.Categories.FindAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      _context.Remove(category);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
