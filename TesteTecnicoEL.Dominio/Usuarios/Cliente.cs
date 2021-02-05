using System;
using System.Collections.Generic;
using System.Text;

namespace TesteTecnicoEL.Dominio.Usuarios
{
    public class Cliente: Entidade
    {
    }

    [Flags]
    public enum Combustivel
    {
        Gasolina = 1,
        Alcool = 2,
        Diesel = 4,
    }
}
