using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Veiculos
{
    public class ModeloRepositorio : RepositorioMemoriaBase<Modelo>, IModeloRepositorio
    {
        private readonly IMarcaRepositorio _marcas;

        public ModeloRepositorio(IMarcaRepositorio marcas)
        {
            _marcas = marcas;
        }
        public override async Task Inserir(Modelo obj)
        {
            var marca = await _marcas.ObterPorId(obj.IdMarca);
            if (marca == null) throw new ArgumentException("violação de FK: marca");
            obj.SetMarca(marca);
            await base.Inserir(obj);
        }
        public Task<List<Modelo>> ListarPorMarca(long idMarca)
        {
            return Task.FromResult(Itens.Where(m => m.Marca.Id == idMarca).ToList());
        }
    }
}
