using Application.DAL.UnitOfWork.DTO;
using Application.DAL.UnitOfWork.Entities;

namespace Application.DAL.UnitOfWork.Repositories
{
    public interface IEscolaRepository : IBaseRepository<SaveEscolaDTO, Escola>
    {
    }
}
