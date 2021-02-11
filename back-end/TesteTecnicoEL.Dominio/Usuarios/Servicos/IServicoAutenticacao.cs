using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Dominio.Usuarios.Servicos
{
    public interface IServicoAutenticacao
    {
        Task<object> Autenticar(string nomeUsuario, string senha);

        Task<object> ObterPorChave(string chave);
    }
}
