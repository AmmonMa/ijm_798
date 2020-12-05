using Application.CrossCutting.ViewModels.Escolas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.UnitOfWork.Repositories
{
    public interface IEscolaRepository
    {
        Task<IList<ViewEscolaDTO>> ListAllAsync();

    }
}
