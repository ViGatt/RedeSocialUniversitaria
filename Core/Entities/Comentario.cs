namespace Core.Entities;

public class Comentario
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public DateTime DataHora { get; set; } = DateTime.Now;

    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }

    public int PostagemId { get; set; }
    public virtual Postagem Postagem { get; set; }
}