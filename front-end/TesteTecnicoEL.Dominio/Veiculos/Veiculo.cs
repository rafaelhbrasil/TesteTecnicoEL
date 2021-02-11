using TesteTecnicoEL.Dominio.Extensoes;

namespace TesteTecnicoEL.Dominio.Veiculos
{
    public class Veiculo : Entidade
    {
        public string Placa { get; set; }
        public long IdModelo { get; set; }
        public Modelo Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int CapacidadePortaMalaLitros { get; set; }
        public float ValorHora { get; set; }
        public long IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public bool Disponivel { get; set; }

        public override string ToString()
        {
            var combustiveis = Modelo.Combustivel.ObterFlagsIndividuais();
            return $"{Modelo.Marca.Nome} {Modelo.Nome} {AnoFabricacao} {string.Join("/", combustiveis)}";
        }
    }

}
