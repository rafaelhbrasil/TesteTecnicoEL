using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Usuarios.Repositorios
{
    public interface IOperadorRepositorio: IRepositorioBase<Operador>
    {
        Task<Operador> ObterPorMatricula(string matricula);
    }
}
