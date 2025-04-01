using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventosController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Evento>>> GetEventos()
    {
        return Ok(await _eventoRepository.ListAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Evento>> GetEvento(int id)
    {
        var evento = await _eventoRepository.GetByIdAsync(id);
        if (evento == null) return NotFound();
        return Ok(evento);
    }

    [HttpPost]
    public async Task<ActionResult<Evento>> CreateEvento(Evento evento)
    {
        var novoEvento = await _eventoRepository.AddAsync(evento);
        return CreatedAtAction(nameof(GetEvento), new { id = novoEvento.Id }, novoEvento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvento(int id, Evento evento)
    {
        if (id != evento.Id) return BadRequest();
        await _eventoRepository.UpdateAsync(evento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvento(int id)
    {
        var evento = await _eventoRepository.GetByIdAsync(id);
        if (evento == null) return NotFound();
        await _eventoRepository.DeleteAsync(evento);
        return NoContent();
    }

    [HttpPost("{eventoId}/inscrever/{usuarioId}")]
    public async Task<IActionResult> InscreverUsuario(int eventoId, int usuarioId)
    {
        await _eventoRepository.InscreverUsuario(eventoId, usuarioId);
        return NoContent();
    }

    [HttpPost("{eventoId}/cancelarinscricao/{usuarioId}")]
    public async Task<IActionResult> CancelarInscricao(int eventoId, int usuarioId)
    {
        await _eventoRepository.CancelarInscricao(eventoId, usuarioId);
        return NoContent();
    }

    [HttpGet("{id}/participantes")]
    public async Task<ActionResult<IReadOnlyList<Usuario>>> GetParticipantes(int id)
    {
        return Ok(await _eventoRepository.GetParticipantes(id));
    }
}