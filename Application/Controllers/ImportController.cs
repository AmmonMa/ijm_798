using Application.DAL.UnitOfWork;
using Application.DAL.UnitOfWork.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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

        public ImportController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("escolas")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
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
                        return BadRequest(new { message = $"Erro na importação, cheque os dados da linha {rowNum}" });
                    }
                    await UnitOfWork.Escolas.CreateAsync(dto);
                }

            }
            await UnitOfWork.CommitAsync();
            return Ok(new { message = "Arquivo Importado com Sucesso" });
        }
    }
}
