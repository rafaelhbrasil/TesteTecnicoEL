using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Infraestrutura.Memoria.Usuarios
{
    public class ClienteRepositorio : RepositorioMemoriaBase<Cliente>, IClienteRepositorio
    {
        public override Task Inserir(Cliente obj)
        {
            return base.Inserir(obj);
        }
        public Task<Cliente> ObterPorCpf(string cpf)
        {
            return Task.FromResult(Itens.FirstOrDefault(c => c.CPF == cpf));
        }

        public Task<Cliente> ObterPorChave(string chave)
        {
            return Task.FromResult(Itens.FirstOrDefault(c => c.ChaveAutenticacao == chave));
        }

        public override Task Alterar(Cliente obj)
        {
            var indiceParaAlterar = Itens.FindIndex(i => i.Id == obj.Id);
            if (indiceParaAlterar >= 0)
            {
                var itemRemovido = Itens[indiceParaAlterar];
                obj.SetSenhaEChaveCifradas(itemRemovido.Senha, itemRemovido.ChaveAutenticacao);
                Itens.RemoveAt(indiceParaAlterar);
                Itens.Insert(indiceParaAlterar, obj);
            }
            return Task.CompletedTask;
        }
    }
}
