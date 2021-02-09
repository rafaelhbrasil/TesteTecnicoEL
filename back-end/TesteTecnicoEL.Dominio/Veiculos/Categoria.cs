namespace TesteTecnicoEL.Dominio.Veiculos
{
    public class Categoria : Entidade
    {
        public Categoria(string codigo, string nome)
        {
            Codigo = codigo?.Trim();
            Nome = nome?.Trim();

            if (string.IsNullOrWhiteSpace(Codigo))
                AdicionarMensagemErro($"{nameof(Codigo)} é de preenchimento obrigatório");
            if (string.IsNullOrWhiteSpace(Nome))
                AdicionarMensagemErro($"{nameof(Nome)} é de preenchimento obrigatório");
        }

        public string Codigo { get; }
        public string Nome { get; }
    }
}
