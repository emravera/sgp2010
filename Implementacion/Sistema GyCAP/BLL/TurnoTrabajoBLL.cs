using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TurnoTrabajoBLL
    {
        public static void Insertar(Entidades.TurnoTrabajo turno)
        {
            DAL.TurnoTrabajoDAL.Insertar(turno);
        }

        public static void Actualizar(Entidades.TurnoTrabajo turno)
        {
            DAL.TurnoTrabajoDAL.Actualizar(turno);
        }

        public static void Eliminar(int codigoTurno)
        {
            DAL.TurnoTrabajoDAL.Eliminar(codigoTurno);
        }

        public static void ObtenerTurnos(DataTable dtTurnos)
        {
            DAL.TurnoTrabajoDAL.ObtenerTurnos(dtTurnos);
        }
    }
}
