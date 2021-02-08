using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecncicoEL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly ServicoAutenticacao _servicoAutenticacao;

        public AutenticacaoController(IClienteRepositorio clienteRepositorio,
            IOperadorRepositorio operadorRepositorio,
            ServicoAutenticacao servicoAutenticacao)
        {
            this._clienteRepositorio = clienteRepositorio;
            this._operadorRepositorio = operadorRepositorio;
            this._servicoAutenticacao = servicoAutenticacao;
        }

        [HttpGet("autenticar")]
        public async Task<ActionResult<Cliente>> Autenticar(string login, string senha)
        {
            var usuario = await _servicoAutenticacao.Autenticar(login, senha);
            if (usuario == null)
                return Unauthorized();
            return Ok(usuario);
        }

    }
}
