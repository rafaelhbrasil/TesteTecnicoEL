using System.Collections.Generic;
using System.Threading.Tasks;
using TesteTecnicoEL.Api.Models;
using TesteTecnicoEL.Dominio.Locacao;

namespace TesteTecnicoEL.AcessoDados
{
    public interface IAluguelRepositorio
    {
        Task<Aluguel> Simular(ParametrosLocacaoDto parametrosLocacao);
        Task<Aluguel> RealizarLocacao(ParametrosLocacaoDto parametrosLocacao);
        Task<Aluguel> RealizarDevolucao(long id, ParametrosLocacaoDto parametrosLocacao);
        Task<List<Aluguel>> ListarHistoricoDoCliente();
    }
}
