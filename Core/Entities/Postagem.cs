namespace Core.Entities;

public class Postagem
{
    public int Id { get; set; }
    public string Conteudo { get; set; }
    public DateTime DataHora { get; set; } = DateTime.Now;

    public int AutorId { get; set; }
    public virtual Usuario Autor { get; set; }
    public virtual ICollection<Curtida> Curtidas { get; set; } = new List<Curtida>();
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}