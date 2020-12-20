using Application.CrossCutting.ViewModels.Escolas;
using Application.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<ImportController> Logger;
        private readonly IUnitOfWork UnitOfWork;

        public ImportController(
            ILogger<ImportController> logger,
            IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("escolas")]
        public async Task<IActionResult> Import(IFormFile file)
        {

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage xlPackage = new ExcelPackage(stream);
                
                var xlWorksheet = xlPackage.Workbook.Worksheets.First();
                var rows = xlWorksheet.Dimension.End.Row;

                await UnitOfWork.BeginTransactionAsync();
                for(var rowNum = 2; rowNum <= rows; rowNum++)
                {
                    var dto = new SaveEscolaDTO
                    {
                        Nome = xlWorksheet.Cells[rowNum, 1].Text,
                        RazaoSocial = xlWorksheet.Cells[rowNum, 2].Text,
                        Cnpj = xlWorksheet.Cells[rowNum, 3].Text,
                        Telefone = xlWorksheet.Cells[rowNum, 4].Text,
                        Email = xlWorksheet.Cells[rowNum, 5].Text,
                        Site = xlWorksheet.Cells[rowNum, 6].Text
                    };
                    if(!TryValidateModel(dto, nameof(dto)))
                    {
                        await UnitOfWork.RollbackAsync();
                        Logger.LogError("Inclusão em massa realizada com erros", dto);
                        throw new Exception($"Erro na importação, cheque os dados da linha {rowNum}");
                    }
                    await UnitOfWork.Escolas.CreateAsync(dto);
                }

            }
            await UnitOfWork.CommitAsync();
            return Ok();
        }
    }
}
