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

        public virtual async Task<ViewEscolaDTO> FindByIdAsync(int id)
        {
            return Mapper.Map<Escola, ViewEscolaDTO>(await Context.Escolas.Where(x => x.Id == id).FirstOrDefaultAsync());
        }

        public virtual async Task<int> CreateAsync(SaveEscolaDTO obj)
        {
            var entity = Mapper.Map<SaveEscolaDTO, Escola>(obj);
            Context.Escolas.Add(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task UpdateAsync(int id, SaveEscolaDTO obj)
        {
            var entity = await Context.Escolas.Where(x => x.Id == id).FirstOrDefaultAsync();
            Context.Entry(entity).CurrentValues.SetValues(obj);

            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await Context.Escolas.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new Exception("Não existe este id");
            }
            Context.Escolas.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
