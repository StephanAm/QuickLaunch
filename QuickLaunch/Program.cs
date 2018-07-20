using QuickLaunchManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLaunch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NLog.Logger logger;
            var factory = new ApiFactory();
            var api = factory.GetApi();
            logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(api));
            }
            catch (Exception x)
            {
                logger.Error(x);
                throw x;
            }
            finally
            {
                logger.Debug("Exit");
            }
        }
    }
}
