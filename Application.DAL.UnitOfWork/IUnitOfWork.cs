using Application.DAL.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEscolaRepository Escolas { get; }
        ITurmaRepository Turmas { get; }
        Task CommitAsync();
        void Dispose();
    }
}
