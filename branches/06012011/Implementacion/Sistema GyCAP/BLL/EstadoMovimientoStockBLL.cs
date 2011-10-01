using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;

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

        public static void ObtenerEstadosMovimiento(Data.dsStock.ESTADO_MOVIMIENTOS_STOCKDataTable dtEstadosMovimientoStock)
        {
            DAL.EstadoMovimientoStockDAL.ObtenerEstados(dtEstadosMovimientoStock);
        }

        public static Entidades.EstadoMovimientoStock GetEstadoEntity(string nombreEstado)
        {
            return DAL.EstadoMovimientoStockDAL.GetEstadoEntity(nombreEstado);
        }

        public static Entidades.EstadoMovimientoStock GetEstadoEntity(int codigoEstado)
        {
            return DAL.EstadoMovimientoStockDAL.GetEstadoEntity(codigoEstado);
        }

        public static SortableBindingList<EstadoMovimientoStock> GetAll()
        {
            Data.dsStock.ESTADO_MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.ESTADO_MOVIMIENTOS_STOCKDataTable();
            SortableBindingList<EstadoMovimientoStock> lista = new SortableBindingList<EstadoMovimientoStock>();
            ObtenerEstadosMovimiento(dt);

            foreach (Data.dsStock.ESTADO_MOVIMIENTOS_STOCKRow row in dt.Rows)
            {
                lista.Add(new EstadoMovimientoStock()
                {
                    Codigo = Convert.ToInt32(row.EMVTO_CODIGO),
                    Nombre = row.EMVTO_NOMBRE,
                    Descripcion = row.EMVTO_DESCRIPCION
                });
            }

            return lista;
        }

    }
}
