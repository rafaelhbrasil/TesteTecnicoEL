using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Usuarios.ObjetosValor;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecnicoEL.Dominio.Usuarios
{
    public class Cliente: Entidade
    {
        public Cliente(string nome, string cPF, DateTime nascimento, Endereco endereco)
        {
            Nome = nome?.Trim();
            CPF = cPF?.Trim();
            Nascimento = nascimento;
            Endereco = endereco;

            if (string.IsNullOrWhiteSpace(Nome))
                AdicionarMensagemErro($"{nameof(Nome)} é de preenchimento obrigatório");
            if (string.IsNullOrWhiteSpace(CPF))
                AdicionarMensagemErro($"{nameof(CPF)} é de preenchimento obrigatório");
            else if (CPF.Length != 11)
                AdicionarMensagemErro($"{nameof(CPF)} é inválido");
            if (Nascimento == default)
                AdicionarMensagemErro($"{nameof(Nascimento)} é de preenchimento obrigatório");
            else if(Nascimento > DateTime.Today)
                AdicionarMensagemErro($"{nameof(Nascimento)} não pode ser data futura");
            if (Endereco == null)
                AdicionarMensagemErro($"{nameof(Endereco)} é de preenchimento obrigatório");
            else if (!Endereco.EhValido())
                AdicionarMensagemErro($"{nameof(Endereco)} é inválido");
        }

        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime Nascimento { get; private set; }
        public Endereco Endereco { get; private set; }
        public string ChaveAutenticacao { get; private set; }
        public string Senha { get; private set; }

        // alterar a senha também troca a chave de autenticação
        public void SetSenha(string senha)
        {
            Senha = ServicoAutenticacao.CalcularSHA256(senha);
            ChaveAutenticacao = ServicoAutenticacao.CalcularSHA256(CPF + Senha);
        }
    }
}
