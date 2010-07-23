using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using System.Windows;


namespace GyCAP.UI.RecursosFabricacion
{
    public class BaseReporte
    {
        private static CrystalDecisions.Shared.ConnectionInfo loginfo;
        public static bool exportar_mail;
        public static string rutaRpt;
        public static bool stillOpen;
        public static string custTitle;

        public static void conectar(string servidor, string daseDatos, string usuario, string password) 
        {
            loginfo = new ConnectionInfo();
            loginfo.ServerName = servidor;
            loginfo.DatabaseName = daseDatos;
            loginfo.UserID = usuario;
            loginfo.Password = password;
        }

        private static ParameterField generarParametros()
        {
            string p1, p2;
            int i;
            ParameterFields parametros = new ParameterFields();

            /*for (int c = 0; c <= matriz.Length - 1; c++)
            {
                //l = Strings.InStr(matriz[c].IndexOf(, ";");
                //i = matriz[c].IndexOf(";");

                if (i > 0)
                {
                    //p1 = Strings.Mid(matriz[c], 1, l - 1);
                    //p1 = matriz[c].Substring(1, 1 - 1);

                    //p2 = Strings.Mid(matriz[c], l + 1, Strings.Len(matriz[c]) - l);
                    //p2 = matriz[c].Substring(1 + 1, matriz[c].Length - 1);

                    ParameterField parametro = new ParameterField();

                    ParameterDiscreteValue dVal = new ParameterDiscreteValue();

                    parametro.ParameterFieldName = p1;

                    dVal.Value = p2;

                    parametro.CurrentValues.Add(dVal);

                    parametros.Add(parametro);

                }*/

            

            return parametros[0];

            //Private Shared Function genpar ( ByVal ParamArray matriz() As String) As ParameterFields
            //    Dim c As Long, p1, p2 As String, l As Integer
            //    Dim parametros As New ParameterFields
            //    For c = 0 To matriz.Length - 1
            //          l = InStr (matriz(c), ";")
            //          If l > 0 Then
            //                p1 = Mid (matriz(c), 1, l - 1)
            //                p2 = Mid (matriz(c), l + 1, Len(matriz(c)) - l)
            //                Dim parametro As New ParameterField
            //                Dim dVal As New ParameterDiscreteValue
            //                parametro.ParameterFieldName = p1
            //                dVal.Value = p2
            //                parametro.CurrentValues.Add ( dVal )
            //                parametros.Add (parametro)
            //          End If
            //    Next
            //    Return (parametros)
            //End Function
        }

        private static void logonrpt(ReportDocument reporte) 
        {
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

        public static void printRpt(string nombreReporte, string[] par)
        {
            frmVisorInformes visor = new frmVisorInformes();
            ReportDocument rpt = new ReportDocument();

            //Public Overloads Shared Sub printrpt ( ByVal nombrereporte As String, ByVal ParamArray par() As String)
            //Dim forma As New frmprint
            //Dim rpt As New ReportDocument
            //With forma.CrystalReportViewer1

            //  If par.Length > 0 Then
            //        .ParameterFieldInfo = genpar (par)
            //  End If

            //  If rutarpt.Trim.Length = 0 Then
            //        rpt.Load (nombrereporte, OpenReportMethod.OpenReportByDefault)
            //  ElseIf Mid(rutarpt.Trim, rutarpt.Trim.Length, 1) = "\" Then
            //        rpt.Load (rutarpt & nombrereporte, OpenReportMethod.OpenReportByDefault)
            //  Else
            //        rpt.Load (rutarpt & "\" & nombrereporte, OpenReportMethod.OpenReportByDefault)
            //  End If

            //logonrpt (rpt)

            //  'Configurar aquí cualquier opción de exportación
            //    Dim opt As New ExportOptions
            //   opt = rpt.ExportOptions

            //  'Configurar aquí cualquier opción de impresión
            //   Dim prn As PrintOptions
            //    prn = rpt.PrintOptions

            //   .ReportSource = rpt
            //  'Visualizar el reporte en una ventana nueva
            //    forma.Text = custTitle
            //    forma.Show ()

            //    End With
            //End Sub

        }

        public static void printRpt(string nombreReporte) 
        {
            frmVisorInformes visor = new frmVisorInformes();
            ReportDocument rpt = new ReportDocument();

            if (rutaRpt.Trim().Length == 0) 
                rpt.Load(nombreReporte, OpenReportMethod.OpenReportByDefault);
            else
                rpt.Load(rutaRpt + "\\" + nombreReporte, OpenReportMethod.OpenReportByDefault);

            logonrpt(rpt);
            
            //Configurar aquí cualquier opción de exportación
            //ExportOptions opt = new ExportOptions();
            //opt = rpt.PrintOptions;  // opt = rpt.ExportOptions

            //Configurar aquí cualquier opción de impresión
            //PrintOptions prn = new PrintOptions();
            //prn = rpt.PrintOptions;

            visor.crvReporte.ReportSource = rpt;
            visor.Show();


            //Public Overloads Shared Sub printrpt(ByVal nombrereporte As String)
            //        Dim forma As New frmprint
            //        Dim rpt As New ReportDocument
            //        With forma.CrystalReportViewer1
            //            If rutarpt.Trim.Length = 0 Then
            //                rpt.Load(nombrereporte, OpenReportMethod.OpenReportByDefault)
            //            ElseIf Mid(rutarpt.Trim, rutarpt.Trim.Length, 1) = "\" Then
            //                rpt.Load(rutarpt & nombrereporte, OpenReportMethod.OpenReportByDefault)
            //            Else
            //                rpt.Load(rutarpt & "\" & nombrereporte, OpenReportMethod.OpenReportByDefault)
            //            End If
            //            logonrpt(rpt)
            //            'Configurar aquí cualquier opción de exportación
            //            Dim opt As New ExportOptions
            //            opt = rpt.ExportOptions
            //            'Configurar aquí cualquier opción de impresión
            //            Dim prn As PrintOptions
            //            prn = rpt.PrintOptions
            //            .ReportSource = rpt
            //            forma.Show()
            //        End With
            //    End Sub
            //End Class

        }

    }
}

//'La forma de utilizarla es la siguiente:

//'1)     Invocar la rutina conectar utilizando los parámetros de esta forma:  CR.conectar(SERVIDOR, BASEDATOS, USUARIO, PASSWORD)
//'2)     Especificar la ruta a utilizar para abrir el reporte fijando CR.RPT = Ruta de los reportes
//'3)     Llamar a la rutina CR.printrpt, de cualquiera de las dos formas siguientes:
//'a)     Si el reporte no utiliza parámetros, solamente debe llamarse CR.printrpt(“reporte.rpt”)
//'b)     Si el reporte utiliza parámetros, llamarlo de esta forma: CR.printrpt (“reporte.rpt”, “par1;valor”, “par2;valor”, “parN;valor”……)
//'Por ejemplo: CR.printrpt (“reporte.rpt”, “year;2004”, “mes;10”)
//'Esto abrirá el reporte y le pasará los parámetros con los valores respectivos.