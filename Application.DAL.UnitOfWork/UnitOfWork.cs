
using Application.DAL.UnitOfWork.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEscolaRepository escolas;
        private ITurmaRepository turmas;

        private readonly IMapper Mapper;
        private readonly AppContext Context;

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
                if (escolas == null)
                {
                    escolas = new EscolaRepository(Context, Mapper);
                }

                return escolas;
            }
        }

        public virtual ITurmaRepository Turmas
        {
            get
            {
                if (turmas == null)
                {
                    turmas = new TurmaRepository(Context, Mapper);
                }

                return turmas;
            }
        }
    }
}
