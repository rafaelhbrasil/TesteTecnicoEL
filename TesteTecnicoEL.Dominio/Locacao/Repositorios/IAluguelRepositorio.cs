using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Locacao.Repositorios
{
    public interface IAluguelRepositorio : IRepositorioBase<Aluguel>
    {
        Task<Aluguel> ObterPorVeiculo(long idVeiculo);
        Task<List<Aluguel>> ListarPorUsuario(long idUsuario);
        Task Atualizar(Aluguel aluguel);
    }
}
