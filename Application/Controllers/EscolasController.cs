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
        public async Task<ActionResult<IList<Escola>>> Get()
        {
            try
            {
                return Ok(await UnitOfWork.Escolas.ListAllAsync());
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                return BadRequest("Problema desconhecido ao retornar escolas");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Escola>> Get(int id)
        {
            try
            {
                return Ok(await UnitOfWork.Escolas.FindByIdAsync(id));
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e, id);
                return BadRequest($"Problema desconhecido ao retornar a escola de id {id}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(SaveEscolaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações de inserção inválidas");
            }
            try
            {
                var entity = await UnitOfWork.Escolas.CreateAsync(dto);
                return Ok(entity.Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e, dto);
                return BadRequest("Problema desconhecido encontrado na inserção");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] SaveEscolaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações de atualização inválidas");
            }
            try
            {
                await UnitOfWork.Escolas.UpdateAsync(id, dto);
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
                await UnitOfWork.Escolas.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e, id);
                return BadRequest("Problema desconhecido encontrado na exclusão");
            }
        }
    }
}
