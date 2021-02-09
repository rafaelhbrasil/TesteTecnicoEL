using System;

namespace TesteTecnicoEL.Dominio.Usuarios
{
    public class Cliente : Entidade
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public Endereco Endereco { get; set; }
        public string ChaveAutenticacao { get; set; }
        public string Senha { get; set; }
    }
}
