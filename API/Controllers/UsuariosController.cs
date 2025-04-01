using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuariosController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Usuario>>> GetUsuarios()
    {
        return Ok(await _usuarioRepository.ListAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
    {
        var novoUsuario = await _usuarioRepository.AddAsync(usuario);
        return CreatedAtAction(nameof(GetUsuario), new { id = novoUsuario.Id }, novoUsuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
    {
        if (id != usuario.Id) return BadRequest();
        await _usuarioRepository.UpdateAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        await _usuarioRepository.DeleteAsync(usuario);
        return NoContent();
    }

    [HttpPost("{seguidorId}/seguir/{seguidoId}")]
    public async Task<IActionResult> SeguirUsuario(int seguidorId, int seguidoId)
    {
        await _usuarioRepository.SeguirUsuario(seguidorId, seguidoId);
        return NoContent();
    }

    [HttpPost("{seguidorId}/deixardeseguir/{seguidoId}")]
    public async Task<IActionResult> DeixarDeSeguir(int seguidorId, int seguidoId)
    {
        await _usuarioRepository.DeixarDeSeguir(seguidorId, seguidoId);
        return NoContent();
    }

    [HttpGet("{id}/seguidores")]
    public async Task<ActionResult<IReadOnlyList<Usuario>>> GetSeguidores(int id)
    {
        return Ok(await _usuarioRepository.GetSeguidores(id));
    }

    [HttpGet("{id}/seguindo")]
    public async Task<ActionResult<IReadOnlyList<Usuario>>> GetSeguindo(int id)
    {
        return Ok(await _usuarioRepository.GetSeguindo(id));
    }
}