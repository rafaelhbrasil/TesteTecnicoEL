using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecnicoEL.Dominio.Locacao.Servicos
{
    public interface IServicoAluguel
    {
        Task<Simulacao> SimularAluguel(Simulacao aluguel);

        Task<Aluguel> RealizarAluguel(Aluguel aluguel);

        Task<Aluguel> RealizarDevolucao(long idAluguel, ChecklistDevolucao checklistDevolucao);

        Task AtualizarDados(Aluguel aluguel);
    }
}
