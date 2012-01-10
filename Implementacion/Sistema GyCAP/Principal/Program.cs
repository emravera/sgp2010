using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GyCAP.UI.Principal;
using GyCAP.BLL;

namespace Principal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                        
            DBBLL.SetTipoConexion(DBBLL.tipoFabrica);
            
            Application.Run(new frmLogin());
            //Application.Run(new frmPrincipal());
            
        }
    }
}
