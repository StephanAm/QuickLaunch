using QuickLaunchManager.Models;
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
    }
}
