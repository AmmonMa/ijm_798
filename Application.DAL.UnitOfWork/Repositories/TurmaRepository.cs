using Application.DAL.UnitOfWork.DTO;
using Application.DAL.UnitOfWork.Entities;
using AutoMapper;

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
