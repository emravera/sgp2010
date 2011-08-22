using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoMovimientoStockBLL
    {
        public const string Planificado = DAL.EstadoMovimientoStockDAL.Planificado;
        public const string Finalizado = DAL.EstadoMovimientoStockDAL.Finalizado;
        public const string Cancelado = DAL.EstadoMovimientoStockDAL.Cancelado;
        
        public static void ObtenerEstadosMovimiento(DataTable dtEstadosMovimientoStock)
        {
            DAL.EstadoMovimientoStockDAL.ObtenerEstados(dtEstadosMovimientoStock);
        }

        public static Entidades.EstadoMovimientoStock GetEstadoEntity(string nombreEstado)
        {
            return DAL.EstadoMovimientoStockDAL.GetEstadoEntity(nombreEstado);
        }
    }
}
