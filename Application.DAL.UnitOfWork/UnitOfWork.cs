
using Application.DAL.UnitOfWork.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IEscolaRepository escolas;
        private ITurmaRepository turmas;

        private readonly AppContext Context;
        private IDbContextTransaction Transaction;
        private readonly IMapper Mapper;

        public UnitOfWork(AppContext context)
        {
            Context = context;
        }

        public UnitOfWork(AppContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual IEscolaRepository Escolas
        {
            get
            {
                escolas ??= new EscolaRepository(Context, Mapper);

                return escolas;
            }
        }

        public virtual ITurmaRepository Turmas
        {
            get
            {
                turmas ??= new TurmaRepository(Context, Mapper);

                return turmas;
            }
        }
        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
        }
        public async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
        }

        public async Task BeginTransactionAsync()
        {
            Transaction = await Context.Database.BeginTransactionAsync(); 
        }
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
