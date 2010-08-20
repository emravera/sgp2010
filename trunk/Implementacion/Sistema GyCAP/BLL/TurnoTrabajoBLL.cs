using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TurnoTrabajoBLL
    {
        public static int Insertar(Entidades.TurnoTrabajo turno)
        {
            return DAL.TurnoTrabajoDAL.Insertar(turno);
        }

        public static void Actualizar(Entidades.TurnoTrabajo turno)
        {
            DAL.TurnoTrabajoDAL.Actualizar(turno);
        }

        public static void Eliminar(int codigoTurno)
        {
            if (!DAL.TurnoTrabajoDAL.PuedeEliminarse(codigoTurno)) throw new Entidades.Excepciones.ElementoEnTransaccionException();
            DAL.TurnoTrabajoDAL.Eliminar(codigoTurno);
        }

        public static void ObtenerTurnos(DataTable dtTurnos)
        {
            DAL.TurnoTrabajoDAL.ObtenerTurnos(dtTurnos);
        }
    }
}
