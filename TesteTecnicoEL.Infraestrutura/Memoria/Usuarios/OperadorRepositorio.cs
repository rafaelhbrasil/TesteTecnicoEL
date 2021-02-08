using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Usuarios
{
    public class OperadorRepositorio : RepositorioMemoriaBase<Operador>, IOperadorRepositorio
    {
        public Task<Operador> ObterPorMatricula(string matricula)
        {
            return Task.FromResult(Itens.FirstOrDefault(o => o.Matricula == matricula));
        }
    }
}
