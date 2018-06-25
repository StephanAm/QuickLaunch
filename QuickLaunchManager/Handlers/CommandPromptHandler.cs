using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickLaunchManager.Models;

namespace QuickLaunchManager.Handlers
{
    public class CommandPromptHandler : BaseHandler
    {
        private readonly string cmdPath;
        public override string handlerIcon => "Handlers\\cmdIcon.bmp";
        public override string HandlerKey => "Command Prompt";

        public CommandPromptHandler()
        {
            var windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            cmdPath = Path.Combine(windowsPath, "system32\\cmd.exe");
        }

        public override bool Handle(QuickLaunchItem item)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = cmdPath,
                WorkingDirectory = item.URI
            };
            Process.Start(startInfo);
            return true;
        }
    }
}
