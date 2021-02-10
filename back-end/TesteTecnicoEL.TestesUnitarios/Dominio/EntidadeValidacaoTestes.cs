using System;
using System.Collections.Generic;
using System.Text;
using TesteTecnicoEL.Dominio;
using Xunit;

namespace TesteTecnicoEL.TestesUnitarios.Dominio
{
    public class EntidadeValidacaoTestes
    {
        [Fact]
        public void TesteEhValido_RetornaSePossuiErros()
        {
            var obj = new Concreta();
            Assert.True(obj.EhValido());
            Assert.Equal(0, obj.Mensagens.Count);
            obj.AdicionarMensagemErro("teste");
            Assert.False(obj.EhValido());
            Assert.Equal(1, obj.Mensagens.Count);
            obj.AdicionarMensagemErro("teste2");
            Assert.Equal(2, obj.Mensagens.Count);
            Assert.False(obj.EhValido());
            obj.LimparMensagensErro();
            Assert.True(obj.EhValido());
            Assert.Equal(0, obj.Mensagens.Count);
        }

        [Fact]
        public void TesteValidarELancarErroSeInvalido_LanchaExcecaoSePossuiErros()
        {
            var obj = new Concreta();
            obj.ValidarELancarErroSeInvalido();
            
            obj.AdicionarMensagemErro("teste");
            Assert.Throws<ValidacaoException>(() =>
                obj.ValidarELancarErroSeInvalido()
            );

            obj.AdicionarMensagemErro("teste2");
            Assert.Throws<ValidacaoException>(() =>
                obj.ValidarELancarErroSeInvalido()
            );

            obj.LimparMensagensErro();
            obj.ValidarELancarErroSeInvalido();
        }

        private class Concreta: EntidadeValidacao
        {
        }
    }
}
