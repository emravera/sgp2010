using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.BLL
{
    public class TurnoTrabajoBLL
    {
        public static int Insertar(Entidades.TurnoTrabajo turno)
        {
            if (DAL.TurnoTrabajoDAL.EsTurno(turno)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            return DAL.TurnoTrabajoDAL.Insertar(turno);
        }

        public static void Actualizar(Entidades.TurnoTrabajo turno)
        {
            if (DAL.TurnoTrabajoDAL.EsTurno(turno)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
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

        public static void ObtenerTurnosPorCentros(DataTable dtTurnosPorCentros)
        {
            DAL.TurnoTrabajoDAL.ObtenerTurnosPorCentros(dtTurnosPorCentros);
        }

        public static IList<TurnoTrabajo> AsTurnosTrabajoEntity(int codigoCentro, Data.dsHojaRuta ds)
        {
            IList<TurnoTrabajo> turnos = new List<TurnoTrabajo>();

            foreach (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow row in (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select("cto_codigo = " + codigoCentro))
            {
                turnos.Add(new TurnoTrabajo()
                {
                    Codigo = Convert.ToInt32(row.TUR_CODIGO),
                    Nombre = ds.TURNOS_TRABAJO.FindByTUR_CODIGO(row.TUR_CODIGO).TUR_NOMBRE,
                    HoraInicio = ds.TURNOS_TRABAJO.FindByTUR_CODIGO(row.TUR_CODIGO).TUR_HORAINICIO,
                    HoraFin = ds.TURNOS_TRABAJO.FindByTUR_CODIGO(row.TUR_CODIGO).TUR_HORAFIN
                });
            }

            return turnos;
        }
    }
}
