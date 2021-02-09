using System;

namespace TesteTecnicoEL.Dominio.Veiculos
{
    public class Modelo : Entidade
    {
        public string Nome { get; set; }
        public long IdMarca { get; set; }
        public Marca Marca { get; set; }
        public Combustivel Combustivel { get; set; }
    }

    [Flags]
    public enum Combustivel
    {
        Gasolina = 1,
        Alcool = 2,
        Diesel = 4,
    }
}
