using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEventoRepository  
    {
        Task<Evento> GetByIdAsync(int id);
        Task<IReadOnlyList<Evento>> ListAllAsync();
        Task<Evento> AddAsync(Evento entity);
        Task UpdateAsync(Evento entity);
        Task DeleteAsync(Evento entity);

        Task InscreverUsuario(int eventoId, int usuarioId);
        Task CancelarInscricao(int eventoId, int usuarioId);
        Task<IReadOnlyList<Usuario>> GetParticipantes(int eventoId);
    }
}