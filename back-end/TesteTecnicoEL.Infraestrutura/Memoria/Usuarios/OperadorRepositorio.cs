using System.Linq;
using System.Threading.Tasks;
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

        public Task<Operador> ObterPorChave(string chave)
        {
            return Task.FromResult(Itens.FirstOrDefault(c => c.ChaveAutenticacao == chave));
        }
    }
}
