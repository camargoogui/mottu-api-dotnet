// Models/Filial.cs
public class Filial
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Moto> Motos { get; set; }
}
