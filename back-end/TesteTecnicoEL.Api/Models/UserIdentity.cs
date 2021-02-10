using System.Security.Claims;
using TesteTecnicoEL.Dominio.Usuarios;

namespace TesteTecncicoEL.Api.Models
{
    public class UserIdentity : ClaimsIdentity
    {
        public Cliente Cliente { get; internal set; }
        public Operador Operador { get; internal set; }

        public bool EhOperador => Operador != null;

        public void ArmazenarUsuario(Cliente cliente)
        {
            Cliente = cliente;
        }

        public void ArmazenarUsuario(Operador operador)
        {
            Operador = operador;
        }

    }
}
