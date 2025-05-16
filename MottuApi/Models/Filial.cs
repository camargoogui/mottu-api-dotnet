// Models/Filial.cs
namespace MottuApi.Models
{
    public class Filial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Moto> Motos { get; set; }
    }
}
