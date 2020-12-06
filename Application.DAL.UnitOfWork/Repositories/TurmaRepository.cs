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
    public class TurmaRepository : BaseRepository<SaveTurmaDTO, Turma>, ITurmaRepository
    {
        public TurmaRepository(AppContext context)
        {
            Context = context;
            DbSet = Context.Turmas;
        }
        public TurmaRepository(AppContext context, IMapper mapper) : this(context)
        {
            Mapper = mapper;
        }
    }
}
