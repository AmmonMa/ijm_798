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

        [HttpPost]
        public async Task<int> Post(SaveEscolaDTO dto)
        {
            if(ModelState.IsValid)
            {
                var id = await UnitOfWork.Escolas.CreateAsync(dto);
                return id;
            }
            Logger.LogError("Erro de Cadastro de Escola", dto);
            throw new Exception("Problema encontrado na inserção");
        }
        [HttpPut("{id}")]
        public async Task<int> Put(int id, SaveEscolaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await UnitOfWork.Escolas.UpdateAsync(id, dto);
                return id;
            }
            Logger.LogError("Erro de Atualização de Escola", dto);
            throw new Exception("Problema encontrado na atualização");
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await UnitOfWork.Escolas.DeleteAsync(id);
        }
    }
}
