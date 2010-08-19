using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class TurnoTrabajoDAL
    {
        public static void Insertar(Entidades.TurnoTrabajo turno)
        {
        }

        public static void Actualizar(Entidades.TurnoTrabajo turno)
        {
        }

        public static void Eliminar(int codigoTurno)
        {
        }

        public static void ObtenerTurnos(DataTable dtTurnos)
        {
            string sql = "SELECT tur_codigo, tur_nombre, tur_horainicio, tur_horafin FROM TURNOS_TRABAJO";
            try
            {
                DB.FillDataTable(dtTurnos, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
