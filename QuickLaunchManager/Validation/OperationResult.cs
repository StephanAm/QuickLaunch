using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLaunchManager.Validation
{
    public enum Severity { Undefine,Info,Warning,Error};
    public class OperationResult
    {
        public Severity Severity { get; set; }
        public string Message { get; set; }
        public string Reference { get; set; }
        public OperationResult(string reference, Severity severity = Severity.Undefine, string message = null)
        {
            Reference = reference;
            Severity = severity;
            Message = message;
        }
    }
}
