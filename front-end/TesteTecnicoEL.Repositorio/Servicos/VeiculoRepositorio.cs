using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.AcessoDados.Extensions;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Veiculos;

namespace TesteTecnicoEL.AcessoDados
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        private readonly IHttpRequest _httpRequest;

        public VeiculoRepositorio(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }

        public Task<List<Veiculo>> ListarVeiculos()
        {
            return _httpRequest.GetAsync<List<Veiculo>>($"veiculos");
        }

        public Task<Veiculo> ObterPorId(long id)
        {
            return _httpRequest.GetAsync<Veiculo>($"veiculos/{id}");
        }
    }
}
