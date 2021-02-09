using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace TesteTecnicoEL.Dominio
{
    public abstract class EntidadeValidacao
    {
        private List<string> _mensagens = new List<string>();
        [JsonIgnore]
        public IReadOnlyList<string> Mensagens { get { return _mensagens; } }
        public bool EhValido() => !Mensagens.Any();

        protected void AdicionarMensagemErro(string mensagem) => _mensagens.Add(mensagem);
        protected void LimparMensagensErro() => _mensagens.Clear();
    }
}
