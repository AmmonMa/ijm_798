using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public interface IBaseRepository<T, E>
    {
        Task<IList<E>> ListAllAsync();
        Task<E> CreateAsync(T obj);
        Task UpdateAsync(int id, T obj);
        Task<E> FindByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
