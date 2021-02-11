using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTecnicoEL.AcessoDados;

namespace TesteTecnicoEL.WebUI.Controllers
{
    public class VeiculoController : BaseController
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculoController(IVeiculoRepositorio veiculoRepositorio)
        {
            this._veiculoRepositorio = veiculoRepositorio;
        }
        // GET: VeiculoController
        public async Task<ActionResult> Index()
        {
            var veiculos = await _veiculoRepositorio.ListarVeiculos();
            //veiculos = veiculos.OrderBy(v => v.Categoria.Nome)
            //                   .ThenBy(v => v.Placa)
            //                   .ToList();
            return View(veiculos);
        }

        // GET: VeiculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VeiculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VeiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: VeiculoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VeiculoController/Edit/5
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

        // GET: VeiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VeiculoController/Delete/5
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
