using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio.Locacao.ObjetosValor
{
    public class ChecklistDevolucao: EntidadeValidacao
    {
        public ChecklistDevolucao(bool carroLimpo,
                                bool tanqueCheio,
                                bool semAmassados,
                                bool semArranhoes,
                                DateTime dataRealizacaoChecklist)
        {
            CarroLimpo = carroLimpo;
            TanqueCheio = tanqueCheio;
            SemAmassados = semAmassados;
            SemArranhoes = semArranhoes;
            DataRealizacaoChecklist = dataRealizacaoChecklist;
            // nada para validar por enquanto
        }

        public bool CarroLimpo { get; private set; }
        public bool TanqueCheio { get; private set; }
        public bool SemAmassados { get; private set; }
        public bool SemArranhoes { get; private set; }
        public DateTime DataRealizacaoChecklist { get; private set; }
    }
}
