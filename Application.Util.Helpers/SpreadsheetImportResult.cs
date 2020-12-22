using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Util.Helpers
{
    public enum ResultStatus
    {
        Succeeded,
        Error
    }
    public struct SpreadsheetImportResult
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
    }
}
