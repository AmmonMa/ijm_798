using Application.DAL.UnitOfWork.DTO;
using Application.DAL.UnitOfWork.Entities;
using AutoMapper;

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

    }
}
