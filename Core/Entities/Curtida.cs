namespace Core.Entities;

public class Curtida
{
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }

    public int PostagemId { get; set; }
    public virtual Postagem Postagem { get; set; }

    public DateTime DataHora { get; set; } = DateTime.Now;
}