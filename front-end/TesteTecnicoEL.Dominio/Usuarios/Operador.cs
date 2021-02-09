
namespace TesteTecnicoEL.Dominio.Usuarios
{
    public class Operador : Entidade
    {
        /// <summary>
        /// 6 dígitos, padrão da empresa
        /// </summary>
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string ChaveAutenticacao { get; set; }
        public string Senha { get; set; }
    }
}
