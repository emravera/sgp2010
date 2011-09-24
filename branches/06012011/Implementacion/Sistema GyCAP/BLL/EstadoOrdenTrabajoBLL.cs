using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades;

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
    }
}
