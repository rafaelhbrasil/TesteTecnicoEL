﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio.Veiculos.Repositorios
{
    public interface IVeiculoRepositorio : IRepositorioBase<Veiculo>
    {
        Task<List<Veiculo>> Listar();
        Task<List<Veiculo>> ListarPorCategoria(long idCategoria);
        Task<List<Veiculo>> ListarPorModelo(long idModelo);

        Task InserirCategoria(Categoria obj);
        Task<List<Categoria>> ListarCategorias();
    }
}
