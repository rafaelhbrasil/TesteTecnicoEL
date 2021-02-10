using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio;

namespace TesteTecnicoEL.Infraestrutura.Memoria
{
    public abstract class RepositorioMemoriaBase<T> : IRepositorioBase<T> where T : Entidade
    {
        public static List<T> Itens { get; } = new List<T>();
        public virtual Task<T> ObterPorId(long id)
        {
            return Task.FromResult(Itens.Find(i => i.Id == id));
        }

        public virtual Task Inserir(T obj)
        {
            if (obj != null)
            {
                obj.Id = Itens.Any() ? (Itens.Max(i => i.Id) + 1) : 1;
                Itens.Add(obj);
            }
            return Task.CompletedTask;
        }

        public virtual Task Alterar(T obj)
        {
            var indiceParaAlterar = Itens.FindIndex(i => i.Id == obj.Id);
            if (indiceParaAlterar >= 0)
            {
                Itens.RemoveAt(indiceParaAlterar);
                Itens.Insert(indiceParaAlterar, obj);
            }
            return Task.CompletedTask;
        }

        public virtual Task Excluir(long id)
        {
            Itens.RemoveAll(i => i.Id == id);
            return Task.CompletedTask;
        }
    }
}
