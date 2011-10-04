using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.BindingEntity;

namespace GyCAP.BLL
{
    public class CentroTrabajoBLL
    {
        public static readonly int TipoHombre = 1;
        public static readonly int TipoMaquina = 2;
        public static readonly int TipoProveedor = 3;
        public static readonly int CentroInactivo = 0;
        public static readonly int CentroActivo = 1;
        
        public static int Insertar(Data.dsHojaRuta ds)
        {
            Entidades.CentroTrabajo centro = new GyCAP.Entidades.CentroTrabajo();
            Data.dsHojaRuta.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsHojaRuta.CENTROS_TRABAJOSRow;
            centro.Nombre = row.CTO_NOMBRE;
            centro.Sector = new GyCAP.Entidades.SectorTrabajo();
            centro.Sector.Codigo = Convert.ToInt32(row.SEC_CODIGO);
            if (DAL.CentroTrabajoDAL.EsCentroTrabajo(centro)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            return DAL.CentroTrabajoDAL.Insertar(ds);
        }

        public static void Actualizar(Data.dsHojaRuta ds)
        {
            Entidades.CentroTrabajo centro = new GyCAP.Entidades.CentroTrabajo();
            Data.dsHojaRuta.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsHojaRuta.CENTROS_TRABAJOSRow;
            centro.Codigo = Convert.ToInt32(row.CTO_CODIGO);
            centro.Nombre = row.CTO_NOMBRE;
            centro.Sector = new GyCAP.Entidades.SectorTrabajo();
            centro.Sector.Codigo = Convert.ToInt32(row.SEC_CODIGO);
            if (DAL.CentroTrabajoDAL.EsCentroTrabajo(centro)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
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

        public static void ObetenerCentrosTrabajo(object nombre, object tipo, object sector, object estado, Data.dsHojaRuta ds)
        {
            if (tipo != null && Convert.ToInt32(tipo.ToString()) <= 0) { tipo = null; }
            if (sector != null && Convert.ToInt32(sector.ToString()) <= 0) { sector = null; }
            if (estado != null && Convert.ToInt32(estado.ToString()) < 0) { estado = null; }
            DAL.CentroTrabajoDAL.ObetenerCentrosTrabajo(nombre, tipo, sector, estado, ds);
        }

        public static Entidades.CentroTrabajo AsCentroTrabajoEntity(int codigo, Data.dsHojaRuta ds)
        {
            Data.dsHojaRuta.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigo);

            Entidades.CentroTrabajo centro = new GyCAP.Entidades.CentroTrabajo()
            {
                Activo = Convert.ToInt32(row.CTO_ACTIVO),
                CapacidadCiclo = row.CTO_CAPACIDADCICLO,
                CapacidadUnidadHora = row.CTO_CAPACIDADUNIDADHORA,
                Codigo = Convert.ToInt32(row.CTO_CODIGO),
                CostoCiclo = row.CTO_COSTOCICLO,
                CostoHora = row.CTO_COSTOHORA,
                Descripcion = row.CTO_DESCRIPCION,
                Eficiencia = row.CTO_EFICIENCIA,
                HorasCiclo = row.CTO_HORASCICLO,
                HorasTrabajoExtendido = row.CTO_HORASTRABAJOEXTENDIDO,
                HorasTrabajoNormal = row.CTO_HORASTRABAJONORMAL,
                Nombre = row.CTO_NOMBRE,
                Sector = new GyCAP.Entidades.SectorTrabajo() { Codigo = Convert.ToInt32(row.SEC_CODIGO) },
                TiempoAntes = row.CTO_TIEMPOANTES,
                TiempoDespues = row.CTO_TIEMPODESPUES,
                Tipo = TipoCentroTrabajoBLL.AsTipoEntity(Convert.ToInt32(row.CT_TIPO), ds),
                TurnosTrabajo = null
            };

            return centro;
        }

        public static SortableBindingList<CentroTrabajo> GetAll()
        {
            Data.dsHojaRuta.CENTROS_TRABAJOSDataTable dt = new GyCAP.Data.dsHojaRuta.CENTROS_TRABAJOSDataTable();
            ObetenerCentrosTrabajo(null, null, null, null, dt);
            SortableBindingList<CentroTrabajo> lista = new SortableBindingList<CentroTrabajo>();
            SortableBindingList<TipoCentroTrabajo> tipos = TipoCentroTrabajoBLL.GetAll();

            foreach (Data.dsHojaRuta.CENTROS_TRABAJOSRow row in dt.Rows)
            {
                lista.Add(new CentroTrabajo()
                {
                    Activo = Convert.ToInt32(row.CTO_ACTIVO),
                    CapacidadCiclo = row.CTO_CAPACIDADCICLO,
                    CapacidadUnidadHora = row.CTO_CAPACIDADUNIDADHORA,
                    Codigo = Convert.ToInt32(row.CTO_CODIGO),
                    CostoCiclo = row.CTO_COSTOCICLO,
                    CostoHora = row.CTO_COSTOHORA,
                    Descripcion = row.CTO_DESCRIPCION,
                    Eficiencia = row.CTO_EFICIENCIA,
                    HorasCiclo = row.CTO_HORASCICLO,
                    HorasTrabajoExtendido = row.CTO_HORASTRABAJOEXTENDIDO,
                    HorasTrabajoNormal = row.CTO_HORASTRABAJONORMAL,
                    Nombre = row.CTO_NOMBRE,
                    Sector = new GyCAP.Entidades.SectorTrabajo() { Codigo = Convert.ToInt32(row.SEC_CODIGO) },
                    TiempoAntes = row.CTO_TIEMPOANTES,
                    TiempoDespues = row.CTO_TIEMPODESPUES,
                    Tipo = tipos.Where(p => p.Codigo == Convert.ToInt32(row.CT_TIPO)).Single(),
                    TurnosTrabajo = null
                });
            }

            return lista;
        }
    }
}
