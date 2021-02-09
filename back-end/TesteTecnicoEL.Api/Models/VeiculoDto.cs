namespace TesteTecncicoEL.Api.Models
{
    public class VeiculoDto
    {
        public string Placa { get; set; }
        public long IdModelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int CapacidadePortaMalaLitros { get; set; }
        public float ValorHora { get; set; }
        public long IdCategoria { get; set; }
    }
}
