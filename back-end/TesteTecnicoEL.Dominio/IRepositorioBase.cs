﻿using System.Threading.Tasks;

namespace TesteTecnicoEL.Dominio
{
    public interface IRepositorioBase<T>
    {
        Task<T> ObterPorId(long id);
        Task Inserir(T obj);
    }
}
