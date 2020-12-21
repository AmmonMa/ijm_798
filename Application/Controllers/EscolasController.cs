using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EscolasController : ControllerBase
    {
        private readonly IUnitOfWork UnitOfWork;

        public EscolasController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IList<Escola>> Get()
        {
            return await UnitOfWork.Escolas.ListAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<Escola> Get(int id)
        {
            return await UnitOfWork.Escolas.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<int> Post(SaveEscolaDTO dto)
        {
            if(ModelState.IsValid)
            {
                var entity = await UnitOfWork.Escolas.CreateAsync(dto);
                return entity.Id;
            }
            throw new Exception("Problema encontrado na inserção");
        }
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] SaveEscolaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await UnitOfWork.Escolas.UpdateAsync(id, dto);
                return id;
            }
            throw new Exception("Problema encontrado na atualização");
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await UnitOfWork.Escolas.DeleteAsync(id);
        }
    }
}
