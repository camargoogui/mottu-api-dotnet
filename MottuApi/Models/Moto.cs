namespace MottuApi.Models
{
    public class Moto
    {
        public int Id { get; private set; }
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public int FilialId { get; private set; }
        public Filial Filial { get; private set; }

        public Moto(string placa, string modelo, int filialId)
        {
            SetPlaca(placa);
            SetModelo(modelo);
            SetFilial(filialId);
        }

        public void SetPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa) || placa.Length < 7)
                throw new ArgumentException("Placa inválida");
            Placa = placa.ToUpper();
        }

        public void SetModelo(string modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo))
                throw new ArgumentException("Modelo é obrigatório");
            Modelo = modelo;
        }

        public void SetFilial(int filialId)
        {
            if (filialId <= 0)
                throw new ArgumentException("FilialId inválido");
            FilialId = filialId;
        }
    }
}
