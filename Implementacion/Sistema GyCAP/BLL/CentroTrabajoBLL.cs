using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CentroTrabajoBLL
    {
        public static readonly int TipoHombre = 1;
        public static readonly int TipoMaquina = 2;
        public static readonly int CentroInactivo = 0;
        public static readonly int CentroActivo = 1;
        
        public static void Insertar(Data.dsCentroTrabajo ds)
        {
            Entidades.CentroTrabajo centro = new GyCAP.Entidades.CentroTrabajo();
            Data.dsCentroTrabajo.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsCentroTrabajo.CENTROS_TRABAJOSRow;
            centro.Nombre = row.CTO_NOMBRE;
            centro.Sector = new GyCAP.Entidades.SectorTrabajo();
            centro.Sector.Codigo = Convert.ToInt32(row.SEC_CODIGO);
            if (DAL.CentroTrabajoDAL.EsCentroTrabajo(centro)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.CentroTrabajoDAL.Insertar(ds);
        }

        public static void Actualizar(Data.dsCentroTrabajo ds)
        {
            DAL.CentroTrabajoDAL.Actualizar(ds);
        }

        public static void Eliminar(int codigoCentroTrabajo)
        {
            if (!DAL.CentroTrabajoDAL.PuedeEliminarse(codigoCentroTrabajo)) throw new Entidades.Excepciones.ElementoEnTransaccionException();
            DAL.CentroTrabajoDAL.Eliminar(codigoCentroTrabajo);
        }

        public static void ObetenerCentrosTrabajo(object nombre, object tipo, object sector, object estado, DataTable dtCentrosTrabajo)
        {
            if (tipo != null && Convert.ToInt32(tipo.ToString()) <= 0) { tipo = null; }
            if (sector != null && Convert.ToInt32(sector.ToString()) <= 0) { sector = null; }
            if (estado != null && Convert.ToInt32(estado.ToString()) < 0) { estado = null; }
            DAL.CentroTrabajoDAL.ObetenerCentrosTrabajo(nombre, tipo, sector, estado, dtCentrosTrabajo);
        }
    }
}
