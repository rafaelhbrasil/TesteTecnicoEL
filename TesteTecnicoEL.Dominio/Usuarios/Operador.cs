using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;

namespace TesteTecnicoEL.Dominio.Usuarios
{
    public class Operador: Entidade
    {
        public Operador(string matricula, string nome)
        {
            Matricula = matricula?.Trim();
            Nome = nome?.Trim();

            if (string.IsNullOrWhiteSpace(Matricula))
                AdicionarMensagemErro($"{nameof(Matricula)} é de preenchimento obrigatório");
            else if(Matricula.Length != 6)
                AdicionarMensagemErro($"{nameof(Matricula)} deve ser composta por 6 dígitos");
            if (string.IsNullOrWhiteSpace(Nome))
                AdicionarMensagemErro($"{nameof(Nome)} é de preenchimento obrigatório");
        }

        /// <summary>
        /// 6 dígitos, padrão da empresa
        /// </summary>
        public string Matricula { get; private set; }
        public string Nome { get; private set; }
        public string ChaveAutenticacao { get; private set; }
        public string Senha { get; private set; }

        // alterar a senha também troca a chave de autenticação
        public void SetSenha(string senha)
        {
            Senha = ServicoAutenticacao.CalcularSHA256(senha);
            ChaveAutenticacao = ServicoAutenticacao.CalcularSHA256(Matricula + Senha);
        }
    }
}
