using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoEL.AcessoDados
{
    public interface IHttpRequest
    {
        Task<TOut> PostAsync<TOut>(string relativePath, object objToPost);
        Task PostAsync(string relativePath, object objToPost);
        Task<T> PutAsync<T>(string relativePath, object objToPost = null);
        Task PutAsync(string relativePath, object objToPost);
        Task DeleteAsync(string relativePath);
        Task<T> GetAsync<T>(string relativePath);
        Task<string> GetAsync(string relativePath);
    }
}
