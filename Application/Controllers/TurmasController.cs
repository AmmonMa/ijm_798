using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.DTO;
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
        public async Task<ActionResult<IList<Turma>>> Get()
        {
            try
            {
                return Ok(await UnitOfWork.Turmas.ListAllAsync());
            }
            catch(Exception e)
            {
                Logger.LogError(e.Message, e);
                return BadRequest("Problema desconhecido ao retornar turmas");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> Get(int id)
        {
            try
            {
                return Ok(await UnitOfWork.Turmas.FindByIdAsync(id));
            }
            catch(Exception e)
            {
                Logger.LogError(e.Message, e, id);
                return BadRequest($"Problema desconhecido ao retornar a turma de id {id}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(SaveTurmaDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Informações de inserção inválidas");
            }
            try
            {
                var entity = await UnitOfWork.Turmas.CreateAsync(dto);
                return Ok(entity.Id);
            }
            catch(Exception e)
            {
                Logger.LogError(e.Message, e, dto);
                return BadRequest("Problema desconhecido encontrado na inserção");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, SaveTurmaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações de atualização inválidas");
            }
            try
            {
                await UnitOfWork.Turmas.UpdateAsync(id ,dto);
                return Ok(id);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e, dto);
                return BadRequest("Problema desconhecido encontrado na atualização");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await UnitOfWork.Turmas.DeleteAsync(id);
                return Ok();
            }
            catch(Exception e)
            {
                Logger.LogError(e.Message, e, id);
                return BadRequest("Problema desconhecido encontrado na exclusão");
            }
        }
    }
}
