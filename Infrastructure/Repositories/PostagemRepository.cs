using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly AppDbContext _context;

        public PostagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Postagem> AddAsync(Postagem entity)
        {
            await _context.Postagens.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Postagem> GetByIdAsync(int id)
        {
            return await _context.Postagens
                .Include(p => p.Autor)
                .Include(p => p.Curtidas)
                .Include(p => p.Comentarios)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Postagem>> ListAllAsync()
        {
            return await _context.Postagens
                .Include(p => p.Autor)
                .ToListAsync();
        }

        public async Task UpdateAsync(Postagem entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Postagem entity)
        {
            _context.Postagens.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Postagem>> GetPostagensPorUsuario(int usuarioId)
        {
            return await _context.Postagens
                .Where(p => p.AutorId == usuarioId)
                .Include(p => p.Autor)
                .ToListAsync();
        }

        public async Task AdicionarCurtida(int postagemId, int usuarioId)
        {
            var postagem = await _context.Postagens.FindAsync(postagemId);
            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (postagem != null && usuario != null)
            {
                var curtida = new Curtida
                {
                    PostagemId = postagemId,
                    UsuarioId = usuarioId,
                    DataHora = DateTime.Now
                };

                await _context.Curtidas.AddAsync(curtida);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoverCurtida(int postagemId, int usuarioId)
        {
            var curtida = await _context.Curtidas
                .FirstOrDefaultAsync(c => c.PostagemId == postagemId && c.UsuarioId == usuarioId);

            if (curtida != null)
            {
                _context.Curtidas.Remove(curtida);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AdicionarComentario(Comentario comentario)
        {
            await _context.Comentarios.AddAsync(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverComentario(int comentarioId)
        {
            var comentario = await _context.Comentarios.FindAsync(comentarioId);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
                await _context.SaveChangesAsync();
            }
        }
    }
}