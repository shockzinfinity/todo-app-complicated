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
    public class CategoryController : ControllerBase
    {
      private readonly TodoContext _context;
      public CategoryController(TodoContext context)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
      }

    private bool CategoryExists(long id) => _context.Categories.Any(c => c.Id == id);

    private static CategoryDTO CategoryToDTO(Category category) => new CategoryDTO
    {
      Id = category.Id,
      Name = category.Name
    };

    /// <summary>
    /// 모든 카테고리를 불러옵니다.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategorys()
    {
      return await _context.Categories.Select(x => CategoryToDTO(x)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(long id)
    {
      var category = await _context.Categories.FindAsync(id);

      if(category == null)
      {
        return NotFound();
      }

      return CategoryToDTO(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(long id, CategoryDTO categoryDTO)
    {
      if(id != categoryDTO.Id)
      {
        return BadRequest();
      }

      var category = await _context.Categories.FindAsync(id);
      if(category == null)
      {
        return NotFound();
      }

      category.Name = categoryDTO.Name;

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
        Name = categoryDTO.Name
      };

      _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, CategoryToDTO(category));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
      var category = await _context.Categories.FindAsync(id);

      if(category == null)
      {
        return NotFound();
      }

      _context.Remove(category);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}