using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CierreParcialOrdenTrabajoBLL
    {
        public static void Actualizar(Entidades.CierreParcialOrdenTrabajo cierreOrdenTrabajo)
        {
            DAL.CierreParcialOrdenTrabajoDAL.Actualizar(cierreOrdenTrabajo);
        }

        public static void Eliminar(int codigoCierreOrdenTrabajo)
        {
            DAL.CierreParcialOrdenTrabajoDAL.Eliminar(codigoCierreOrdenTrabajo);
        }

        public static void ObtenerCierresParcialesOrdenTrabajo(int numeroOrdenTrabajo, DataTable dtCierreParcialOrdenTrabajo)
        {
            DAL.CierreParcialOrdenTrabajoDAL.ObtenerCierresParcialesOrdenTrabajo(numeroOrdenTrabajo, dtCierreParcialOrdenTrabajo);
        }
    }
}
