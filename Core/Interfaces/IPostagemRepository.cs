using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPostagemRepository  
    {
        Task<Postagem> GetByIdAsync(int id);
        Task<IReadOnlyList<Postagem>> ListAllAsync();
        Task<Postagem> AddAsync(Postagem entity);
        Task UpdateAsync(Postagem entity);
        Task DeleteAsync(Postagem entity);

        
        Task<IReadOnlyList<Postagem>> GetPostagensPorUsuario(int usuarioId);
        Task AdicionarCurtida(int postagemId, int usuarioId);
        Task RemoverCurtida(int postagemId, int usuarioId);
        Task AdicionarComentario(Comentario comentario);
        Task RemoverComentario(int comentarioId);
    }
}