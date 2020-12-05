
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
    }
}
