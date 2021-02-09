using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecncicoEL.Api.Models
{
    public class ModeloDto
    {
        public string Nome { get; set; }
        public long IdMarca { get; set; }
        public Combustivel Combustivel { get; private set; }
    }
}
