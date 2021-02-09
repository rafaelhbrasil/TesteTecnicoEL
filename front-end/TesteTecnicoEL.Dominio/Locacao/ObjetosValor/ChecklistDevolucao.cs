using System;

namespace TesteTecnicoEL.Dominio.Locacao
{
    public class ChecklistDevolucao
    {
        public bool CarroLimpo { get; set; }
        public bool TanqueCheio { get; set; }
        public bool SemAmassados { get; set; }
        public bool SemArranhoes { get; set; }
        public DateTime DataRealizacaoChecklist { get; set; }
    }
}
