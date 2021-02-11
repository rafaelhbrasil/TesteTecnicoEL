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

    }
}
