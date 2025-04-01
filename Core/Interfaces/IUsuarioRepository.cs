using Core.Entities;

namespace Core.Interfaces;

public interface IUsuarioRepository  
{
    Task<Usuario> GetByIdAsync(int id);
    Task<IReadOnlyList<Usuario>> ListAllAsync();
    Task<Usuario> AddAsync(Usuario entity);
    Task UpdateAsync(Usuario entity);
    Task DeleteAsync(Usuario entity);

    Task SeguirUsuario(int seguidorId, int seguidoId);
    Task DeixarDeSeguir(int seguidorId, int seguidoId);
    Task<IReadOnlyList<Usuario>> GetSeguidores(int usuarioId);
    Task<IReadOnlyList<Usuario>> GetSeguindo(int usuarioId);
}