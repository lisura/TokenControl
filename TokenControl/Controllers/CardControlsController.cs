using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokenControl.Models;
using TokenControl.Services;

namespace TokenControl.Controllers
{
    [Route("api/CardControls")]
    [ApiController]
    public class CardControlsController : ControllerBase
    {
        private readonly TokenControlContext _context;

        private readonly ICardControlsService _cardControlsService;
        private readonly ITokenService _tokenService;

        public CardControlsController(TokenControlContext context)
        {
            _context = context;
            if( _cardControlsService == null)
            {
                _cardControlsService = new CardControlsService();
            }
            if(_tokenService == null)
            {
                _tokenService = new TokenService();
            }
        }

        // GET: api/CardControls
        [HttpGet("GetCards")]
        public async Task<ActionResult<IEnumerable<CardControl>>> GetCardControl()
        {
            return await _context.CardControl.ToListAsync();
        }

        // Token Control
        // POST: api/CardControls
        [HttpPost("CardControl")]
        public async Task<ActionResult<CardControlDTO>> PostCardControl(CardControl cardControl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CardControlDTO returnCardinformation = await _cardControlsService.ProcessCustomerCard(_context, cardControl);

            return CreatedAtAction(nameof(GetCardControl), new { id = cardControl.Id }, returnCardinformation);
        }

        // Token Control
        // POST: api/ValidateToken
        [HttpPost("ValidateToken")]
        public async Task<ActionResult<bool>> PostValidateToken(TokenRequest cardControl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnTokenInformation = await _tokenService.ValidateToken(_context, cardControl);
            return returnTokenInformation;
        }
    }
}
