using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Seguidores)
            .Include(u => u.Seguindo)
            .Include(u => u.Postagens)
            .Include(u => u.EventosInscritos)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IReadOnlyList<Usuario>> ListAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> AddAsync(Usuario entity)
    {
        await _context.Usuarios.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Usuario entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Usuario entity)
    {
        _context.Usuarios.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SeguirUsuario(int seguidorId, int seguidoId)
    {
        var seguidor = await _context.Usuarios.FindAsync(seguidorId);
        var seguido = await _context.Usuarios.FindAsync(seguidoId);

        if (seguidor != null && seguido != null)
        {
            seguido.Seguidores.Add(seguidor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeixarDeSeguir(int seguidorId, int seguidoId)
    {
        var seguidor = await _context.Usuarios.FindAsync(seguidorId);
        var seguido = await _context.Usuarios
            .Include(u => u.Seguidores)
            .FirstOrDefaultAsync(u => u.Id == seguidoId);

        if (seguidor != null && seguido != null)
        {
            seguido.Seguidores.Remove(seguidor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<Usuario>> GetSeguidores(int usuarioId)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Seguidores)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);

        return usuario?.Seguidores.ToList() ?? new List<Usuario>();
    }

    public async Task<IReadOnlyList<Usuario>> GetSeguindo(int usuarioId)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Seguindo)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);

        return usuario?.Seguindo.ToList() ?? new List<Usuario>();
    }
}