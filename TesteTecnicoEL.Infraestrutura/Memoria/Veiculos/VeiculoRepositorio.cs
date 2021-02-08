using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Veiculos
{
    public class VeiculoRepositorio : RepositorioMemoriaBase<Veiculo>, IVeiculoRepositorio
    {
        static List<Categoria> _categorias = new List<Categoria>();
        private readonly IModeloRepositorio _modelos;

        public VeiculoRepositorio(IModeloRepositorio modelos)
        {
            _modelos = modelos;
        }

        //override para simular a inserção da FK no banco
        public override async Task Inserir(Veiculo obj)
        {
            var modelo = await _modelos.ObterPorId(obj.IdModelo);
            if (modelo == null) throw new ArgumentException("violação de FK: modelo");
            obj.SetModelo(modelo);

            var categoria = _categorias.FirstOrDefault(c => c.Id == obj.IdCategoria);
            if (categoria == null) throw new ArgumentException("violação de FK: categoria");
            obj.SetCategoria(categoria);

            await base.Inserir(obj);
        }
        public Task InserirCategoria(Categoria obj)
        {
            if (obj != null)
            {
                obj.Id = _categorias.Any() ? (_categorias.Max(i => i.Id) + 1) : 1;
                _categorias.Add(obj);
            }
            return Task.CompletedTask;
        }
        public Task<List<Categoria>> ListarCategorias()
        {
            return Task.FromResult(_categorias);
        }

        public Task<List<Veiculo>> ListarPorCategoria(long idCategoria)
        {
            return Task.FromResult(Itens.Where(v => v.Categoria.Id == idCategoria).ToList());
        }

        public Task<List<Veiculo>> ListarPorModelo(long idModelo)
        {
            return Task.FromResult(Itens.Where(v => v.Modelo.Id == idModelo).ToList());
        }
    }
}
