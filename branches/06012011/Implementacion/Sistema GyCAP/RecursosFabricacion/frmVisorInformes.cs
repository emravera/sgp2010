using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine; 

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmVisorInformes : Form
    {
        public frmVisorInformes()
        {
            
            InitializeComponent();
            //ReportDocument r = new ReportDocument();
            //r.FileName = "Reportes/rptEmpleados.rpt";
            //crvReporte.ReportSource = r;


        }

        //public frmVisorInformes(Report rpt, string condicion)
        //{
        //    InitializeComponent();
        //    crvReporte.ReportSource = rpt;
        //    if (condicion != string.Empty)
        //    {
        //        crvReporte.SelectionFormula = "where";
        //    }

        //}
    }
}
