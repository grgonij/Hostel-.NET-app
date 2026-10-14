using Microsoft.AspNetCore.Mvc;
using Hostel.WebAPI.DTOs;
using Hostel.WebAPI.Models;
using Hostel.WebAPI.Services;
namespace Hostel.WebAPI.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class RezervacijeController : ControllerBase
    {
        private readonly IRezervacijaService _service;
        public RezervacijeController(IRezervacijaService service) => _service = service;

        /// <summary>Dohvati sve rezervacije</summary>
        [HttpGet] public async Task<ActionResult<IEnumerable<RezervacijaDto>>> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>Dohvati rezervaciju po ID-u</summary>
        [HttpGet("{id}")] public async Task<ActionResult<RezervacijaDto>> GetById(int id)
        { var r=await _service.GetByIdAsync(id); if(r==null)return NotFound(new{poruka=$"Rezervacija ID={id} nije pronađena."}); return Ok(r); }

        /// <summary>Kreiraj novu rezervaciju</summary>
        [HttpPost][ProducesResponseType(StatusCodes.Status201Created)][ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<RezervacijaDto>> Create([FromBody] CreateRezervacijaDto dto)
        {
            if(!ModelState.IsValid)return BadRequest(ModelState);
            if(dto.DatumOdjave<=dto.DatumPrijave)return UnprocessableEntity(new{poruka="Datum odjave mora biti nakon datuma prijave."});
            var r=await _service.CreateAsync(dto);
            if(r==null)return UnprocessableEntity(new{poruka=$"Ležaj ID={dto.LezajId} ne postoji."});
            return CreatedAtAction(nameof(GetById),new{id=r.Id},r);
        }

        /// <summary>Ažuriraj rezervaciju</summary>
        [HttpPut("{id}")] public async Task<ActionResult<RezervacijaDto>> Update(int id,[FromBody] UpdateRezervacijaDto dto)
        {
            if(!ModelState.IsValid)return BadRequest(ModelState);
            if(dto.DatumOdjave<=dto.DatumPrijave)return UnprocessableEntity(new{poruka="Datum odjave mora biti nakon datuma prijave."});
            var r=await _service.UpdateAsync(id,dto); if(r==null)return NotFound(new{poruka=$"Rezervacija ID={id} nije pronađena."}); return Ok(r);
        }

        /// <summary>Obriši rezervaciju</summary>
        [HttpDelete("{id}")][ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        { var ok=await _service.DeleteAsync(id); if(!ok)return NotFound(new{poruka=$"Rezervacija ID={id} nije pronađena."}); return NoContent(); }

        /// <summary>Filtriraj rezervacije po statusu</summary>
        [HttpGet("filter")] public async Task<ActionResult<IEnumerable<RezervacijaDto>>> FilterByStatus([FromQuery] StatusRezervacije status=StatusRezervacije.Aktivna)
        => Ok(await _service.GetByStatusuAsync(status));

        /// <summary>Rezervacije za određeni ležaj</summary>
        [HttpGet("po-lezaju/{lezajId}")] public async Task<ActionResult<IEnumerable<RezervacijaDto>>> GetByLezaj(int lezajId)
        => Ok(await _service.GetByLezajuAsync(lezajId));
    }
}
