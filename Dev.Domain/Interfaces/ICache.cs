using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Domain.Interfaces
{
    public interface ICache
    {
        Task<bool> Store<T>(string key, T value);
        Task<T> Get<T>(string key);
        Task<IDictionary<string, T>> GetAll<T>(string key);
        Task<bool> Delete(string key);
    }
}
