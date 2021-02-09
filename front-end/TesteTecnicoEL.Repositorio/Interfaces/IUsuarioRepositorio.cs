using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteTecncicoEL.Api.Models;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecnicoEL.AcessoDados
{
    public interface IUsuarioRepositorio
    {
        Task<object> Autenticar(string login, string senha);
        Task<Cliente> CadastrarCliente(ClienteDto cliente);
    }
}
