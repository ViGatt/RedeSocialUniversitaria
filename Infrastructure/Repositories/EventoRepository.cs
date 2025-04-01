using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Evento> GetByIdAsync(int id)
        {
            return await _context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<Evento>> ListAllAsync()
        {
            return await _context.Eventos
                .Include(e => e.Participantes)
                .ToListAsync();
        }

        public async Task<Evento> AddAsync(Evento entity)
        {
            await _context.Eventos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Evento entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Evento entity)
        {
            _context.Eventos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InscreverUsuario(int eventoId, int usuarioId)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.Id == eventoId);

            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (evento != null && usuario != null)
            {
                evento.Participantes.Add(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CancelarInscricao(int eventoId, int usuarioId)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.Id == eventoId);

            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (evento != null && usuario != null)
            {
                evento.Participantes.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<Usuario>> GetParticipantes(int eventoId)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.Id == eventoId);

            return evento?.Participantes.ToList() ?? new List<Usuario>();
        }
    }
}