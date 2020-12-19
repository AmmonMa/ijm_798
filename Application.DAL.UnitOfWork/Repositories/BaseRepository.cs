

using Application.DAL.UnitOfWork.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public abstract class BaseRepository<T,E> : IBaseRepository<T, E> where E: BaseEntity
    {
        protected IMapper Mapper;
        protected AppContext Context;
        protected DbSet<E> DbSet;

  
        public virtual async Task<IList<E>> ListAllAsync()
        {
            return await DbSet.OrderByDescending(x => x.Id).ToListAsync();
        }

        public virtual async Task<E> FindByIdAsync(int id)
        {
            return await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual E Add(T obj)
        {
            var entity = Mapper.Map<T, E>(obj);
            DbSet.Add(entity);
            return entity;
        }

        public virtual async Task UpdateAsync(int id, T obj)
        {
            var entity = await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            Context.Entry(entity).CurrentValues.SetValues(obj);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Não existe este id");
            }
            DbSet.Remove(entity);
        }
    }
}