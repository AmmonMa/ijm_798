using Application.DAL.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEscolaRepository Escolas { get; }
    }
}
