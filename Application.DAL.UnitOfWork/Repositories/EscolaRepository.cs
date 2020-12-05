using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public class EscolaRepository : IEscolaRepository
    {
        private readonly IMapper Mapper;
        protected AppContext Context;

        public EscolaRepository(AppContext context)
        {
            Context = context;
        }
        public EscolaRepository(AppContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual async Task<IList<ViewEscolaDTO>> ListAllAsync()
        {
            return Mapper.Map<IList<Escola>, IList<ViewEscolaDTO>>(await Context.Escolas.OrderByDescending(x => x.Id).ToListAsync());
        }
    }
}
