using System;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecnicoEL.Dominio.Locacao
{
    public class Aluguel : Entidade
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }
        public long IdVeiculo { get; set; }
        public Veiculo Veiculo { get; set; }
        public long IdUsuario { get; set; }
        public float ValorAluguel { get; set; }
        public float ValorCobradoDevolucao { get; set; }

        public ChecklistDevolucao ChecklistDevolucao { get; set; }
    }
}
