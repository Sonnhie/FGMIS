using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Model
{
    public class ErrorModel
    {
        public class ScanError
        {
            public string ErrorId { get; set; }
            public string Message { get; set; }
            public string StackTrace { get; set; }
            public DateTime Date { get; set; }
            public DateTime Time { get; set; }
        }
    }
}
