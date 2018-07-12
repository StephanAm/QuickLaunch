using QuickLaunchManager.Models;
using QuickLaunchManager.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLaunchManager.Handlers
{
    public abstract class BaseHandler
    {
        public abstract string handlerIcon { get; }
        public abstract string HandlerKey { get; }
        
        public abstract bool Handle(QuickLaunchItem item);
        public virtual OperationResult Validate(QuickLaunchItem item)
        {
            if (string.IsNullOrWhiteSpace(item.URI))
            {
                return new OperationResult(nameof(item.URI), Severity.Error, "URI can not be blank");
            }
            if (string.IsNullOrWhiteSpace(item.DisplayName))
            {
                return new OperationResult(nameof(item.DisplayName), Severity.Error, "DisplayName can not be blank");
            }
            return null;
        }
    }
}
