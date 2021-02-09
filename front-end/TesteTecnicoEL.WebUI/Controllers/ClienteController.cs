using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
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

        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Criar()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(ClienteDto cliente)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Acessar()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            catch (ValidacaoException ex)
            {
                ViewBag.Erro = ex.Mensagens;
            }
            catch(UnauthorizedAccessException ex)
            {
                ViewBag.Erro = new[] { "UnauthorizedAccessException" };
            }
            catch
            {
            }
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
