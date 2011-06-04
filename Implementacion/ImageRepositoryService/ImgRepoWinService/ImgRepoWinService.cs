using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;

namespace ImgRepoWinService
{
    public partial class ImgRepoWinService : ServiceBase
    {
        ServiceHost winServiceHost;
        public ImgRepoWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            winServiceHost = new ServiceHost(typeof(ImageRepositoryService.Service));
            winServiceHost.Open();
        }

        protected override void OnStop()
        {
            winServiceHost.Close();
        }
    }
}
