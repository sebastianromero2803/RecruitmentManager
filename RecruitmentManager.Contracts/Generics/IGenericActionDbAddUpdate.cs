using System;
using System.Threading.Tasks;

namespace RecruitmentManager.Contracts.Generics
{
    public interface IGenericActionDbAddUpdate<T> where T : class
    {
        Task<Tuple<T, bool>> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
