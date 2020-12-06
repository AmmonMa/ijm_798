using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public interface ITurmaRepository
    {
        Task<IList<Turma>> ListAllAsync();
        Task<int> CreateAsync(SaveTurmaDTO obj);
        Task UpdateAsync(int id, SaveTurmaDTO obj);
        Task<Turma> FindByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
