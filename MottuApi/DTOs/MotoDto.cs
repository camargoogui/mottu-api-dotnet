public class MotoDto
{
    public int Id { get; set; }
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public int FilialId { get; set; }
    public string FilialNome { get; set; } // Para exibição no GET
}
