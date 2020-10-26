using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoCore3.Api.Models;
using todoCore3.Api.Models.kte;

namespace todoCore3.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KtePartnerController : ControllerBase
  {
    private readonly TodoContext _context;

    public KtePartnerController(TodoContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private static KtePartnerResponse ItemToDTO(KtePartner partner) => new KtePartnerResponse
    {
      WPUserId = partner.WPUserId,
      UserLogin = partner.UserLogin,
      UserNickName = partner.UserNickName,
      UserEmail = partner.UserEmail,
      PartnerName = partner.PartnerName
    };

    [HttpGet]
    public async Task<ActionResult<IEnumerable<KtePartnerResponse>>> Get()
    {
      return await _context.KtePartners.Select(x => ItemToDTO(x)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KtePartnerResponse>> GetBy(int id)
    {
      return await _context.KtePartners.Where(x => x.WPUserId == id).Select(x => ItemToDTO(x)).FirstOrDefaultAsync();
    }
  }
}