using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Util.Helpers
{
    public interface ISpreadsheetHelper
    {
        Task<SpreadsheetImportResult> Import(IFormFile file, Func<int, Task> rowAction);
        string GetTextCell(int row, int col);
    }
}
