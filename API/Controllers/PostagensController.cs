using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PostagensController : ControllerBase
{
    private readonly IPostagemRepository _postagemRepository;

    public PostagensController(IPostagemRepository postagemRepository)
    {
        _postagemRepository = postagemRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Postagem>>> GetPostagens()
    {
        return Ok(await _postagemRepository.ListAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Postagem>> GetPostagem(int id)
    {
        var postagem = await _postagemRepository.GetByIdAsync(id);
        if (postagem == null) return NotFound();
        return Ok(postagem);
    }

    [HttpPost]
    public async Task<ActionResult<Postagem>> CreatePostagem(Postagem postagem)
    {
        var novaPostagem = await _postagemRepository.AddAsync(postagem);
        return CreatedAtAction(nameof(GetPostagem), new { id = novaPostagem.Id }, novaPostagem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePostagem(int id, Postagem postagem)
    {
        if (id != postagem.Id) return BadRequest();
        await _postagemRepository.UpdateAsync(postagem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostagem(int id)
    {
        var postagem = await _postagemRepository.GetByIdAsync(id);
        if (postagem == null) return NotFound();
        await _postagemRepository.DeleteAsync(postagem);
        return NoContent();
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IReadOnlyList<Postagem>>> GetPostagensPorUsuario(int usuarioId)
    {
        return Ok(await _postagemRepository.GetPostagensPorUsuario(usuarioId));
    }

    [HttpPost("{postagemId}/curtir/{usuarioId}")]
    public async Task<IActionResult> CurtirPostagem(int postagemId, int usuarioId)
    {
        await _postagemRepository.AdicionarCurtida(postagemId, usuarioId);
        return NoContent();
    }

    [HttpPost("{postagemId}/descurtir/{usuarioId}")]
    public async Task<IActionResult> DescurtirPostagem(int postagemId, int usuarioId)
    {
        await _postagemRepository.RemoverCurtida(postagemId, usuarioId);
        return NoContent();
    }

    [HttpPost("{postagemId}/comentar")]
    public async Task<ActionResult<Comentario>> AdicionarComentario(int postagemId, Comentario comentario)
    {
        comentario.PostagemId = postagemId;
        await _postagemRepository.AdicionarComentario(comentario);
        return Ok(comentario);
    }

    [HttpDelete("comentario/{comentarioId}")]
    public async Task<IActionResult> RemoverComentario(int comentarioId)
    {
        await _postagemRepository.RemoverComentario(comentarioId);
        return NoContent();
    }
}