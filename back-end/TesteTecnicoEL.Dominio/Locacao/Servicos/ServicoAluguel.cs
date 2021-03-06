﻿using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Locacao.ObjetosValor;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecnicoEL.Dominio.Locacao.Servicos
{
    public class ServicoAluguel: IServicoAluguel
    {
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public ServicoAluguel(IAluguelRepositorio aluguelRepositorio, IVeiculoRepositorio veiculoRepositorio)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task<Simulacao> SimularAluguel(Simulacao aluguel)
        {
            var veiculo = await _veiculoRepositorio.ObterPorId(aluguel.IdVeiculo);
            aluguel.ValidarELancarErroSeInvalido();
            aluguel.SetVeiculo(veiculo);
            aluguel.CalcularValorPrevistoAluguel();
            return aluguel;
        }

        public async Task<Aluguel> RealizarAluguel(Aluguel aluguel)
        {
            aluguel = await SimularAluguel(aluguel) as Aluguel;
            aluguel.ConfirmarAluguel();
            await _aluguelRepositorio.Inserir(aluguel);
            return aluguel;
        }

        public async Task<Aluguel> RealizarDevolucao(long idAluguel, ChecklistDevolucao checklistDevolucao)
        {
            var aluguel = await _aluguelRepositorio.ObterPorId(idAluguel);
            if (aluguel == null || aluguel.DataDevolucaoReal.HasValue)
            {
                aluguel.AdicionarMensagemErro("Veículo já havia sido devolvido e está disponível.");
                aluguel.ValidarELancarErroSeInvalido();
            }

            aluguel.RealizarDevolucao(checklistDevolucao);
            await _aluguelRepositorio.Atualizar(aluguel);
            return aluguel;
        }

        public async Task AtualizarDados(Aluguel aluguel)
        {
            var aluguelBd = await _aluguelRepositorio.ObterPorId(aluguel.Id);
            if (aluguelBd == null)
            {
                aluguel.AdicionarMensagemErro("Aluguel não encontrado.");
                aluguel.ValidarELancarErroSeInvalido();
            }
            else if (aluguelBd.ChecklistDevolucao != null || aluguel.ChecklistDevolucao != null)
            {
                aluguel.AdicionarMensagemErro("Não é permitido alterar um aluguel encerrado.");
                aluguel.ValidarELancarErroSeInvalido();
            }
            else
            {
                aluguel = await SimularAluguel(aluguel) as Aluguel;
                aluguel.ConfirmarAluguel();
                await _aluguelRepositorio.Atualizar(aluguel);
            }
        }
    }
}
