namespace Core.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Curso { get; set; }

    public virtual ICollection<Usuario> Seguindo { get; set; } = new List<Usuario>();
    public virtual ICollection<Usuario> Seguidores { get; set; } = new List<Usuario>();
    public virtual ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();
    public virtual ICollection<Evento> EventosInscritos { get; set; } = new List<Evento>();
    public virtual ICollection<Curtida> Curtidas { get; set; } = new List<Curtida>();
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}