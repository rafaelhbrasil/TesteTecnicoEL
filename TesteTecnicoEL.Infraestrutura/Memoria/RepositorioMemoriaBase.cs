using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio;

namespace TesteTecnicoEL.Infraestrutura.Memoria
{
    public abstract class RepositorioMemoriaBase<T> : IRepositorioBase<T>
    {
        public Task<T> ObterPorId(long id)
        {
            throw new NotImplementedException();
        }
    }
}
