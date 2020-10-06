using System;
using System.Linq;
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
  public class FlowController : ControllerBase
  {
    private readonly TodoContext _context;
    private IMapper _mapper;

    public FlowController(TodoContext context, IMapper mapper)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private bool FlowExists(long id) => _context.Flows.Any(f => f.Id == id);

    private static FlowDTO FlowToDTO(Flow flow) => new FlowDTO
    {
      Id = flow.Id,
      Name = flow.Name,
      CategoryId = flow.CategoryId,
      Pos = flow.Pos,
      CreatedAt = flow.CreatedAt,
      UpdatedAt = flow.UpdatedAt,
    };

    [HttpGet("{id}")]
    public async Task<ActionResult<FlowDTO>> GetFlow(long id)
    {
      var flow = await _context.Flows.FindAsync(id);

      if (flow == null)
      {
        return NotFound();
      }

      return FlowToDTO(flow);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlow(long id, FlowDTO flowDTO)
    {
      if (id != flowDTO.Id)
      {
        return BadRequest();
      }

      var flow = await _context.Flows.FindAsync(id);
      if (flow == null)
      {
        return NotFound();
      }

      flow.Name = flowDTO.Name;
      flow.Pos = flowDTO.Pos;
      flow.CategoryId = flowDTO.CategoryId;
      flow.UpdatedAt = DateTime.Now;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!FlowExists(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Flow>> CreateFlow(FlowDTO flowDTO)
    {
      var flow = new Flow
      {
        Name = flowDTO.Name,
        CategoryId = flowDTO.CategoryId,
        Pos = flowDTO.Pos,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
      };

      _context.Flows.Add(flow);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetFlow), new { id = flow.Id }, FlowToDTO(flow));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlow(long id)
    {
      var flow = await _context.Flows.FindAsync(id);

      if (flow == null)
      {
        return NotFound();
      }

      _context.Remove(flow);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
