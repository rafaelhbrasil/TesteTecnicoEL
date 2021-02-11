using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TesteTecnicoEL.Api.Models;
using TesteTecnicoEL.AcessoDados;
using TesteTecnicoEL.AcessoDados.DTOs;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.WebUI.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ClienteController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // GET: UsuarioController/Create
        public ActionResult Criar()
        {
            return View();
        }

        public ActionResult Acessar()
        {
            return View();
        }

        public ActionResult CarregarHeaderUsuario()
        {
            return PartialView("_HeaderDadosUsuario", ClienteAutenticado);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        public async Task<ActionResult> Acessar(string cpf, string senha)
        {
            try
            {
                var usuario = await _usuarioRepositorio.Autenticar(cpf, senha);
                if (usuario != null)
                {
                    if (usuario is Cliente)
                        await SalvarCookieDeAutenticacao(usuario as Cliente);
                    else
                        throw new NotImplementedException("Autenticação de Operador não está no escopo do teste.");
                    return NoContent();
                }
            }
            catch (ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
            }
            catch (UnauthorizedAccessException)
            {
                ViewBag.Erro = new[] { nameof(UnauthorizedAccessException) };
            }
            return PartialView("_FormAcesso", new ClienteDto{ CPF = cpf, Senha = senha});
        }

        [HttpPost]
        public async Task<ActionResult> Criar(ClienteDto cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _usuarioRepositorio.CadastrarCliente(cliente);
                    if (usuario != null)
                    {
                        await SalvarCookieDeAutenticacao(usuario);
                        return NoContent();
                    }
                }
            }
            catch (ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
            }
            return PartialView("_FormCadastro", cliente);
        }

        [Authorize]
        public async Task<ActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
