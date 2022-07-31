using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.BackGroundService
{
    public partial class BGService : ServiceBase
    {
        BGServiceHost host = null;
        public BGService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            host = new BGServiceHost();
            host.StartService();
        }

        protected override void OnStop()
        {
            host.UnRegisterService();
            host.Dispose();
        }
    }
}
