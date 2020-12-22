using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Util.Helpers
{
    public class SpreadsheetHelper : ISpreadsheetHelper
    {
        private ExcelWorksheet xlWorksheet;

        public async Task<SpreadsheetImportResult> Import(IFormFile file, Func<int, Task> rowAction)
        {
            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage xlPackage = new ExcelPackage(stream);

                xlWorksheet = xlPackage.Workbook.Worksheets.First();
                var rows = xlWorksheet.Dimension.End.Row;

                for (var rowNum = 2; rowNum <= rows; rowNum++)
                {
                    await rowAction(rowNum);
                }

                return new SpreadsheetImportResult { Status = ResultStatus.Succeeded, Message = "Arquivo Importado com Sucesso!" };
            }
            catch(Exception e)
            {
                return new SpreadsheetImportResult { Status = ResultStatus.Error, Message = e.Message };
            }
        }


        public string GetTextCell(int row, int col)
        {
            return xlWorksheet.Cells[row, col].Text;
        }
    }
}
