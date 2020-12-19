
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
        private IDbContextTransaction transaction;
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
        public virtual IDbContextTransaction Transaction { 
            get
            {
                transaction ??= Context.Database.BeginTransaction();
                return transaction;
            }
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
            await Context.SaveChangesAsync();
            if (Transaction != null)
            {
                await Transaction.CommitAsync();
            }
        }
        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }

            if (Context != null)
            {
                Context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
