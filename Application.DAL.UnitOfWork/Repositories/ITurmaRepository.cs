using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public interface ITurmaRepository : IBaseRepository<SaveTurmaDTO, Turma>
    {
    }
}
