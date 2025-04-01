using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Seguindo)
                .WithMany(u => u.Seguidores)
                .UsingEntity(j => j.ToTable("UsuarioSeguidores"));

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Participantes)
                .WithMany(u => u.EventosInscritos)
                .UsingEntity(j => j.ToTable("EventoParticipantes"));

            modelBuilder.Entity<Curtida>()
                .HasKey(c => new { c.UsuarioId, c.PostagemId });

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull); 

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Postagem)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.PostagemId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.Autor)
                .WithMany(u => u.Postagens)
                .HasForeignKey(p => p.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Curtida>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Curtidas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}