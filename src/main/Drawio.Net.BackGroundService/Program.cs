using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drawio.Net.BackGroundService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            AutofacBuilder.init();
#if DEBUG
            var host = new BGServiceHost();
            host.StartService();
            try
            {
                while (Console.ReadLine() != "exit")
                {
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                host.UnRegisterService();
                host.Dispose();
            }
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BGService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
