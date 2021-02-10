using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.AcessoDados.Extensions;
using TesteTecnicoEL.Dominio.Locacao;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.AcessoDados
{
    public class AluguelRepositorio : IAluguelRepositorio
    {
        private readonly IHttpRequest _httpRequest;

        public AluguelRepositorio(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }

        public Task<List<Aluguel>> ListarHistoricoDoCliente()
        {
            return _httpRequest.GetAsync<List<Aluguel>>($"alugueis/usuario");
        }

        public Task<Aluguel> RealizarDevolucao(long id, ParametrosLocacaoDto parametrosLocacao)
        {
            return _httpRequest.PostAsync<Aluguel>($"alugueis/devolucao/{id}", parametrosLocacao);
        }

        public Task<Aluguel> RealizarLocacao(ParametrosLocacaoDto parametrosLocacao)
        {
            return _httpRequest.PostAsync<Aluguel>($"alugueis", parametrosLocacao);
        }

        public Task<Aluguel> Simular(ParametrosLocacaoDto parametrosLocacao)
        {
            return _httpRequest.PostAsync<Aluguel>($"alugueis/simular", parametrosLocacao);
        }
    }
}
