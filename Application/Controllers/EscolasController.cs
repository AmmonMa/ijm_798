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
        private readonly ILogger<EscolasController> Logger;
        private readonly IUnitOfWork UnitOfWork;

        public EscolasController(
            ILogger<EscolasController> logger,
            IUnitOfWork unitOfWork)
        {
            Logger = logger;
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
                var id = UnitOfWork.Escolas.Add(dto).Id;
                await UnitOfWork.CommitAsync();
                return id;
            }
            Logger.LogError("Erro de Cadastro de Escola", dto);
            throw new Exception("Problema encontrado na inserção");
        }
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] SaveEscolaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await UnitOfWork.Escolas.UpdateAsync(id, dto);
                await UnitOfWork.CommitAsync();
                return id;
            }
            Logger.LogError("Erro de Atualização de Escola", dto);
            throw new Exception("Problema encontrado na atualização");
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await UnitOfWork.Escolas.DeleteAsync(id);
            await UnitOfWork.CommitAsync();
        }
    }
}
