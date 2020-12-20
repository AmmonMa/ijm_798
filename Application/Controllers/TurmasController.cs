

using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly ILogger<TurmasController> Logger;
        private readonly IUnitOfWork UnitOfWork;

        public TurmasController(
            ILogger<TurmasController> logger,
            IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IList<Turma>> Get()
        {
            return await UnitOfWork.Turmas.ListAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Turma> Get(int id)
        {
            return await UnitOfWork.Turmas.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<int> Post(SaveTurmaDTO dto)
        {
            if (ModelState.IsValid)
            {
                var entity = await UnitOfWork.Turmas.CreateAsync(dto);
                return entity.Id;
            }
            Logger.LogError("Erro de Cadastro de Turma", dto);
            throw new Exception("Problema encontrado na inserção");
        }
        [HttpPut("{id}")]
        public async Task<int> Put(int id, SaveTurmaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await UnitOfWork.Turmas.UpdateAsync(id, dto);
                return id;
            }
            Logger.LogError("Erro de Atualização de Turma", dto);
            throw new Exception("Problema encontrado na atualização");
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await UnitOfWork.Turmas.DeleteAsync(id);
        }
    }
}
