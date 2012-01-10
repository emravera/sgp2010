using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class DBBLL
    {
        public static readonly int tipoLocal = 0;
        public static readonly int tipoRemota = 1;
        public static readonly int tipoInterna = 2;
        public static readonly int tipoFabrica = 3;

        public static void SetTipoConexion(int tipo)
        {
            DAL.DB.SetTipoConexion(tipo);
        }

        public static DateTime GetFechaServidor()
        {
            return DAL.DB.GetFechaServidor();
        }
    }
}
