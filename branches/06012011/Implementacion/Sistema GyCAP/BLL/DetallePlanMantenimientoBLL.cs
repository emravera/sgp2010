using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.BLL
{
    public class DetallePlanMantenimientoBLL
    {
        public static void Insertar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
        {
            DAL.DetallePlanMantenimientoDAL.Insertar(detalle, transaccion);
        }

        public static void Eliminar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
        {
            DAL.DetallePlanMantenimientoDAL.Eliminar(detalle, transaccion);
        }

        public static void Actualizar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
        {
            DAL.DetallePlanMantenimientoDAL.Actualizar(detalle, transaccion);
        }

        public static void EliminarDetallePlanMantenimiento(long codigoPlanMantenimiento, SqlTransaction transaccion)
        {
            DAL.DetallePlanMantenimientoDAL.EliminarDetallePlanMantenimiento(codigoPlanMantenimiento, transaccion);
        }

        public static void ObtenerDetallePlanMantenimiento(DataTable dtDetallePlanMantenimiento, long codigoPlanMantenimiento)
        {
            DAL.DetallePlanMantenimientoDAL.ObtenerDetallePlanMantenimiento(dtDetallePlanMantenimiento, codigoPlanMantenimiento);
        }
        
    }
}
