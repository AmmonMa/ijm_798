using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DAL.UnitOfWork.Repositories
{
    public class EscolaRepository : IEscolaRepository
    {
        protected readonly IAppContext Context;
        public EscolaRepository(IAppContext context )
        {
            Context = context;
        }
    }
}
