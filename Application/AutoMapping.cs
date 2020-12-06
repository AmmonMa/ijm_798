using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
