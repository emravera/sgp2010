using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class SistemaBLL
    {
        private static string workingPath;

        public static string WorkingPath
        {
            get { return SistemaBLL.workingPath; }
            set { SistemaBLL.workingPath = value.Replace("Principal\\bin\\Debug", ""); }
        }

        
    }
}
