namespace TesteTecncicoEL.Api.Models
{
    public class EnderecoDto
    {
        public string Logradouro { get; set; }
        /// <summary>
        /// Nulo para S/N (sem número)
        /// </summary>
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
