using Microsoft.AspNetCore.Mvc;
using Hostel.WebAPI.DTOs;
using Hostel.WebAPI.Models;
using Hostel.WebAPI.Services;
namespace Hostel.WebAPI.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class LezajiController : ControllerBase
    {
        private readonly ILezajService _service;
        public LezajiController(ILezajService service) => _service = service;

        /// <summary>Dohvati sve ležajeve</summary>
        [HttpGet] public async Task<ActionResult<IEnumerable<LezajDto>>> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>Dohvati ležaj po ID-u</summary>
        [HttpGet("{id}")] public async Task<ActionResult<LezajDto>> GetById(int id)
        { var l=await _service.GetByIdAsync(id); if(l==null)return NotFound(new{poruka=$"Ležaj ID={id} nije pronađen."}); return Ok(l); }

        /// <summary>Kreiraj novi ležaj</summary>
        [HttpPost][ProducesResponseType(StatusCodes.Status201Created)][ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LezajDto>> Create([FromBody] CreateLezajDto dto)
        { if(!ModelState.IsValid)return BadRequest(ModelState); var l=await _service.CreateAsync(dto); return CreatedAtAction(nameof(GetById),new{id=l.Id},l); }

        /// <summary>Ažuriraj ležaj</summary>
        [HttpPut("{id}")] public async Task<ActionResult<LezajDto>> Update(int id,[FromBody] UpdateLezajDto dto)
        { if(!ModelState.IsValid)return BadRequest(ModelState); var l=await _service.UpdateAsync(id,dto); if(l==null)return NotFound(new{poruka=$"Ležaj ID={id} nije pronađen."}); return Ok(l); }

        /// <summary>Obriši ležaj</summary>
        [HttpDelete("{id}")][ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        { var ok=await _service.DeleteAsync(id); if(!ok)return NotFound(new{poruka=$"Ležaj ID={id} nije pronađen."}); return NoContent(); }

        /// <summary>Dohvati samo dostupne ležajeve</summary>
        [HttpGet("dostupni")] public async Task<ActionResult<IEnumerable<LezajDto>>> GetDostupni() => Ok(await _service.GetDostupniAsync());

        /// <summary>Filtriraj ležajeve (tip, cijena, dostupnost)</summary>
        [HttpGet("filter")] public async Task<ActionResult<IEnumerable<LezajDto>>> Filter([FromQuery] TipSobe? tip=null,[FromQuery] decimal? cijenaOd=null,[FromQuery] decimal? cijenaDo=null,[FromQuery] bool? dostupan=null)
        => Ok(await _service.FilterAsync(tip,cijenaOd,cijenaDo,dostupan));
    }
}
