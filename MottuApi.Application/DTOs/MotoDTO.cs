using System;

namespace MottuApi.Application.DTOs
{
    public class MotoDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public bool Disponivel { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public int FilialId { get; set; }
        public string FilialNome { get; set; }
    }

    public class CreateMotoDTO
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public int FilialId { get; set; }
    }

    public class UpdateMotoDTO
    {
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
    }
} 