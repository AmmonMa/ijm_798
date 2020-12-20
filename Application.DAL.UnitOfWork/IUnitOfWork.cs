using Application.DAL.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
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
        Task RollbackAsync();
        Task BeginTransactionAsync();
        void Dispose();
    }
}
