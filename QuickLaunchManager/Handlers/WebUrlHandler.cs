using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickLaunchManager.Models;

namespace QuickLaunchManager.Handlers
{
    public class WebUrlHandler : BaseHandler
    {
        public override string HandlerKey => "Web Handler";
        public override string handlerIcon => "Handlers\\wwwIcon.bmp";
        public override bool Handle(QuickLaunchItem item)
        {
            Process.Start(item.URI);
            return true;
        }
    }
}
