using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;

namespace GyCAP.BLL
{
    public class EstadoOrdenTrabajoBLL
    {        
        public static void ObtenerEstadosOrden(DataTable dtEstadosOrden)
        {
            DAL.EstadoOrdenTrabajoDAL.ObtenerEstados(dtEstadosOrden);
        }

        public static int ObtenerEstadoGenerada()
        {
            return DAL.EstadoOrdenTrabajoDAL.ObtenerEstadoGenerada();
        }

        public static EstadoOrdenTrabajo GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum estado)
        {
            return DAL.EstadoOrdenTrabajoDAL.GetEstado(estado);
        }

        public static SortableBindingList<EstadoOrdenTrabajo> GetAll()
        {
            Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJODataTable dt = new GyCAP.Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJODataTable();
            SortableBindingList<EstadoOrdenTrabajo> lista = new SortableBindingList<EstadoOrdenTrabajo>();
            ObtenerEstadosOrden(dt);
            foreach (Data.dsOrdenTrabajo.ESTADO_ORDENES_TRABAJORow row in dt.Rows)
            {
                lista.Add(new EstadoOrdenTrabajo()
                {
                    Codigo = Convert.ToInt32(row.EORD_CODIGO),
                    Nombre = row.EORD_NOMBRE,
                    Descripcion = row.EORD_DESCRIPCION
                });
            }

            return lista;
        }
    }
}
