namespace MottuApi.Models
{
    public class Filial
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public ICollection<Moto> Motos { get; private set; }

        public Filial(string nome)
        {
            SetNome(nome);
            Motos = new List<Moto>();
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da filial é obrigatório");

            Nome = nome;
        }
    }
}
