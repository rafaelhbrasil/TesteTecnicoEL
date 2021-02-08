using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Locacao
{
    public class AluguelRepositorio : RepositorioMemoriaBase<Aluguel>, IAluguelRepositorio
    {
        private readonly IVeiculoRepositorio _veiculos;

        public AluguelRepositorio(IVeiculoRepositorio veiculos)
        {
            _veiculos = veiculos;
        }
        public override async Task Inserir(Aluguel obj)
        {
            var veiculo = await _veiculos.ObterPorId(obj.IdVeiculo);
            if (veiculo == null) throw new ArgumentException("violação de FK: veiculo");
            obj.SetVeiculo(veiculo);
            await base.Inserir(obj);
        }

        public Task Atualizar(Aluguel aluguel)
        {
            // não faz nada por enquanto. Hoje já está tudo em memória.
            // Se tivesse em banco, seria necessário fazer o update devidamente.
            return Task.CompletedTask;
        }

        public Task<List<Aluguel>> ListarPorUsuario(long idUsuario)
        {
            return Task.FromResult(Itens.Where(a => a.IdUsuario == idUsuario).ToList());
        }

        public Task<Aluguel> ObterPorVeiculo(long idVeiculo)
        {
            return Task.FromResult(Itens.FirstOrDefault(a => a.IdVeiculo == idVeiculo));
        }

    }
}
