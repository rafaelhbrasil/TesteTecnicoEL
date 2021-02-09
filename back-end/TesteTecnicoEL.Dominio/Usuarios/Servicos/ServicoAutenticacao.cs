using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;

namespace TesteTecnicoEL.Dominio.Usuarios.Servicos
{
    public class ServicoAutenticacao
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private static SHA256 _cryptoTransformSHA256 = SHA256.Create();

        public ServicoAutenticacao(IClienteRepositorio clienteRepositorio,
                                    IOperadorRepositorio operadorRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _operadorRepositorio = operadorRepositorio;
        }

        public async Task<object> Autenticar(string nomeUsuario, string senha)
        {
            nomeUsuario = nomeUsuario?.Trim();
            if (string.IsNullOrEmpty(nomeUsuario) || string.IsNullOrEmpty(senha)) return null;
            if (nomeUsuario.Length == 11)
            {
                var cliente = await _clienteRepositorio.ObterPorCpf(nomeUsuario);
                if (cliente != null && cliente.Senha == CalcularSHA256(senha))
                    return cliente;
            }
            else
            {
                var operador = await _operadorRepositorio.ObterPorMatricula(nomeUsuario);
                if (operador != null && operador.Senha == CalcularSHA256(senha))
                    return operador;
            }
            return null;
        }

        const string _salt = "LOCALIZA2021";
        public static string CalcularSHA256(string text)
        {
            //return text;
            byte[] buffer = Encoding.Default.GetBytes(text + _salt);
            return BitConverter.ToString(_cryptoTransformSHA256.ComputeHash(buffer)).Replace("-", "");
        }
    }
}
