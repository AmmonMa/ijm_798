using Application.DAL.UnitOfWork.DTO;
using Application.DAL.UnitOfWork.Entities;
using AutoMapper;

namespace Application
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SaveEscolaDTO, Escola>().ForMember(x => x.Turmas, act => act.Ignore());
            CreateMap<SaveTurmaDTO, Turma>();
        }
    }
}
