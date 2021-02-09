using System;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.Dominio.Usuarios.ObjetosValor;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Veiculos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;

namespace TesteTecncicoEL.Api
{
    public class SeedInicial
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly IMarcaRepositorio _marcaRepositorio;
        private readonly IModeloRepositorio _modeloRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public SeedInicial(IClienteRepositorio clienteRepositorio,
                            IOperadorRepositorio operadorRepositorio,
                            IMarcaRepositorio marcaRepositorio,
                            IModeloRepositorio modeloRepositorio,
                            IVeiculoRepositorio veiculoRepositorio)
        {
            this._clienteRepositorio = clienteRepositorio;
            this._operadorRepositorio = operadorRepositorio;
            this._marcaRepositorio = marcaRepositorio;
            this._modeloRepositorio = modeloRepositorio;
            this._veiculoRepositorio = veiculoRepositorio;
        }

        /// <summary>
        /// Inicializa dados para simular um banco já existente com dados simulados
        /// </summary>
        public void Inicializar()
        {
            var endereco = new Endereco("Rua alulolara", 200, null, "Belo Horizonte", "MG");
            var cliente = new Cliente("Rafael Brasil",
                                        "99999999999",
                                        new DateTime(2000, 1, 1),
                                        endereco);
            cliente.SetSenha("123");
            _clienteRepositorio.Inserir(cliente);
            var operador = new Operador("999999", "Rafael Brasil");
            operador.SetSenha("123");
            _operadorRepositorio.Inserir(operador);

            _marcaRepositorio.Inserir(new Marca("Ford")); // 1
            _marcaRepositorio.Inserir(new Marca("Fiat")); // 2

            _modeloRepositorio.Inserir(new Modelo("Ka Hatch 1.0", 1, Combustivel.Alcool | Combustivel.Gasolina)); // 1
            _modeloRepositorio.Inserir(new Modelo("Ka+ Sedan 1.6", 1, Combustivel.Alcool | Combustivel.Gasolina)); // 2
            _modeloRepositorio.Inserir(new Modelo("Argo Drive 1.0", 2, Combustivel.Alcool | Combustivel.Gasolina)); // 3
            _modeloRepositorio.Inserir(new Modelo("Cronos 1.4", 2, Combustivel.Alcool | Combustivel.Gasolina)); // 4

            _veiculoRepositorio.InserirCategoria(new Categoria("C", "Básico")); // 1
            _veiculoRepositorio.InserirCategoria(new Categoria("FS", "Intermediário")); // 2
            _veiculoRepositorio.InserirCategoria(new Categoria("L", "Luxo")); // 3

            _veiculoRepositorio.Inserir(new Veiculo("ABC1111", 1, 2020, 15, 1, 200)); // 1
            _veiculoRepositorio.Inserir(new Veiculo("ABC2222", 2, 2020, 15, 2, 300)); // 2
            _veiculoRepositorio.Inserir(new Veiculo("ABC3333", 3, 2020, 15, 1, 200)); // 3
            var veiculo4 = new Veiculo("ABC4444", 4, 2020, 15, 2, 300);
            veiculo4.MarcarComoIndisponivel();
            _veiculoRepositorio.Inserir(veiculo4); // 4

        }
    }
}
