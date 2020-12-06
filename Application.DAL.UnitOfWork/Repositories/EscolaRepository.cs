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
    public class EscolaRepository : BaseRepository<SaveEscolaDTO, Escola>, IEscolaRepository
    {
        public EscolaRepository(AppContext context)
        {
            Context = context;
            DbSet = Context.Escolas;
        }
        public EscolaRepository(AppContext context, IMapper mapper) : this(context)
        {
            Mapper = mapper;
        }

        public override async Task<Escola> FindByIdAsync(int id)
        {
            return await Context.Escolas.Include(x => x.Turmas).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
