using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public interface IEscolaRepository
    {
        Task<IList<Escola>> ListAllAsync();
        Task<int> CreateAsync(SaveEscolaDTO obj);
        Task UpdateAsync(int id, SaveEscolaDTO obj);
        Task<Escola> FindByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
