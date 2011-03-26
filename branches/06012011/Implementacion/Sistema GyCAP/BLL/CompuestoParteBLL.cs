using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CompuestoParteBLL
    {
        public static readonly int HijoEsParte = 1;
        public static readonly int HijoEsMP = 2;
        
        public static void ObtenerCompuestosEstructura(int codigoEstructura, DataTable dtCompuestos_Partes)
        {
            DAL.CompuestoParteDAL.ObtenerCompuestosEstructura(codigoEstructura, dtCompuestos_Partes);
        }
    }
}
