using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.DTO;
using Application.Util.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly ISpreadsheetHelper xlHelper;

        public ImportController(
            IUnitOfWork unitOfWork,
            ISpreadsheetHelper xl)
        {
            UnitOfWork = unitOfWork;
            xlHelper = xl;
        }

        [HttpPost]
        [Route("escolas")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            await UnitOfWork.BeginTransactionAsync();

            var importResult = await xlHelper.Import(file, async row =>
            {
                var dto = new SaveEscolaDTO
                {   
                    Nome = xlHelper.GetTextCell(row, 1),
                    RazaoSocial = xlHelper.GetTextCell(row, 2),
                    Cnpj = xlHelper.GetTextCell(row, 3),
                    Telefone = xlHelper.GetTextCell(row, 4),
                    Email = xlHelper.GetTextCell(row, 5),
                    Site = xlHelper.GetTextCell(row, 6)
                };
                if (!TryValidateModel(dto, nameof(dto)))
                {
                    await UnitOfWork.RollbackAsync();
                    throw new Exception($"Erro na importação, cheque os dados da linha {row}");
                }
                await UnitOfWork.Escolas.CreateAsync(dto);
            });

            if(!importResult.Status.Equals(ResultStatus.Succeeded))
            {
                return BadRequest(new { message = importResult.Message });
            }

            await UnitOfWork.CommitAsync();
            return Ok(new { message = "Arquivo Importado com Sucesso" });
        }
    }
}
