using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;

namespace GyCAP.UI.RecursosFabricacion
{
    

    public class BaseReporte2
    {

        private static CrystalDecisions.Shared.ConnectionInfo loginfo;
        public static bool exportar_mail;
        public static string rutaRpt;
        public static bool stillOpen;

        public static string custTitle;

        public static void conectar(string servidor, string basedatos, string usuario, string password)
        {
            loginfo = new CrystalDecisions.Shared.ConnectionInfo();
            loginfo.ServerName = servidor;
            loginfo.DatabaseName = basedatos;
            loginfo.UserID = usuario;
            loginfo.Password = password;

        }




        private static ParameterFields genpar(params string[] matriz)
        {
            long c = 0;
            string p1 = null;
            string p2 = null;
            int i = 0;
            ParameterFields parametros = new ParameterFields();


            for (c = 0; c <= matriz.Length - 1; c++)
            {
                //l = Strings.InStr(matriz[c].IndexOf(, ";");
                i = matriz[c].IndexOf(";");

                if (i > 0)
                {
                    //p1 = Strings.Mid(matriz[c], 1, l - 1);
                    p1 = matriz[c].Substring(1, 1 - 1);
                    
                    //p2 = Strings.Mid(matriz[c], l + 1, Strings.Len(matriz[c]) - l);
                    p2 = matriz[c].Substring(1 + 1, matriz[c].Length - 1);

                    ParameterField parametro = new ParameterField();

                    ParameterDiscreteValue dVal = new ParameterDiscreteValue();

                    parametro.ParameterFieldName = p1;

                    dVal.Value = p2;

                    parametro.CurrentValues.Add(dVal);

                    parametros.Add(parametro);

                }

            }

            return (parametros);

        }




        private static void logonrpt(ref ReportDocument reporte)
	{
		
		/*TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
		ConnectionInfo crConnectionInfo = new ConnectionInfo();
		Tables CrTables = default(Tables);
		//Table CrTable = default(Table);

		crConnectionInfo = loginfo;
		CrTables = reporte.Database.Tables;


        foreach (Tables crTable in CrTables)
        {
            crtableLogoninfo = crTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
            crTable.ApplyLogOnInfo(crtableLogoninfo);
        }*/

        //TableLogOnInfos crtableLogoninfos;
        TableLogOnInfo crtableLogoninfo;
        ConnectionInfo crConnectionInfo;
        Tables CrTables;

        crConnectionInfo = loginfo;
        CrTables = reporte.Database.Tables;

        foreach (Table CrTable in CrTables)
        {
            crtableLogoninfo = CrTable.LogOnInfo;
            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        }

		

	}




        public static void printrpt(string nombrereporte, params string[] par)
        {
            frmVisorInformes forma = new frmVisorInformes();
            ReportDocument rpt = new ReportDocument();


            {

                if (par.Length > 0)
                {
                    forma.crvReporte.ParameterFieldInfo = genpar(par);

                }


                if (rutaRpt.Trim().Length == 0)
                {
                    rpt.Load(nombrereporte, OpenReportMethod.OpenReportByDefault);


                }
                else if (rutaRpt.Trim().Substring(rutaRpt.Trim().Length, 1) == "\\")
                {
                    rpt.Load(rutaRpt + nombrereporte, OpenReportMethod.OpenReportByDefault);


                }
                else
                {
                    rpt.Load(rutaRpt + "\\" + nombrereporte, OpenReportMethod.OpenReportByDefault);

                }

                logonrpt(ref rpt);

                //Configurar aquí cualquier opción de exportación

                ExportOptions opt = new ExportOptions();

                opt = rpt.ExportOptions;

                //Configurar aquí cualquier opción de impresión

                PrintOptions prn = default(PrintOptions);

                prn = rpt.PrintOptions;

                forma.crvReporte.ReportSource = rpt;

                //Visualizar el reporte en una ventana nueva

                forma.Text = custTitle;

                forma.Show();

            }

        }




        public static void printrpt(string nombrereporte)
        {
            frmVisorInformes forma = new frmVisorInformes();
            ReportDocument rpt = new ReportDocument();


            {

                if (rutaRpt.Trim().Length == 0)
                {
                    rpt.Load(nombrereporte, OpenReportMethod.OpenReportByDefault);


                }
                else if (rutaRpt.Trim().Substring(rutaRpt.Trim().Length, 1) == "\\")
                {
                    rpt.Load(rutaRpt + nombrereporte, OpenReportMethod.OpenReportByDefault);


                }
                else
                {
                    rpt.Load(rutaRpt + "\\" + nombrereporte, OpenReportMethod.OpenReportByDefault);

                }

                logonrpt(ref rpt);

                //Configurar aquí cualquier opción de exportación

                ExportOptions opt = new ExportOptions();

                opt = rpt.ExportOptions;

                //Configurar aquí cualquier opción de impresión

                PrintOptions prn = default(PrintOptions);

                prn = rpt.PrintOptions;

                forma.crvReporte.ReportSource = rpt;

                forma.Show();

            }

        }

    }

}
