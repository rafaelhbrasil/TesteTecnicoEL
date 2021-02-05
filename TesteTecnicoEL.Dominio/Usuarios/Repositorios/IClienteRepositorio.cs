using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Usuarios.Repositorios
{
    public interface IClienteRepositorio : IRepositorioBase<Cliente>
    {
        Task<Cliente> ObterPorCpf(string cpf);
    }
}
