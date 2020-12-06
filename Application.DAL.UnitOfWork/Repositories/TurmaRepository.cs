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
    public class TurmaRepository : ITurmaRepository
    {
        private readonly IMapper Mapper;
        protected AppContext Context;

        public TurmaRepository(AppContext context)
        {
            Context = context;
        }
        public TurmaRepository(AppContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual async Task<IList<Turma>> ListAllAsync()
        {
            return await Context.Turmas.OrderByDescending(x => x.Id).ToListAsync();
        }

        public virtual async Task<Turma> FindByIdAsync(int id)
        {
            return await Context.Turmas.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<int> CreateAsync(SaveTurmaDTO obj)
        {
            var entity = Mapper.Map<SaveTurmaDTO, Turma>(obj);
            Context.Turmas.Add(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task UpdateAsync(int id, SaveTurmaDTO obj)
        {
            var entity = await Context.Turmas.Where(x => x.Id == id).FirstOrDefaultAsync();
            Context.Entry(entity).CurrentValues.SetValues(obj);

            await Context.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Não existe este id");
            }
            Context.Turmas.Remove(entity);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
