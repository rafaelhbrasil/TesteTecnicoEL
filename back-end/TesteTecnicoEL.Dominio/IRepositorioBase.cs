using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio
{
    public interface IRepositorioBase<T>
    {
        Task<T> ObterPorId(long id);
        Task Inserir(T obj);
    }
}
