using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.BLL
{
    public class EstadoMovimientoStockBLL
    {        
        public static void ObtenerEstadosMovimiento(DataTable dtEstadosMovimientoStock)
        {
            DAL.EstadoMovimientoStockDAL.ObtenerEstados(dtEstadosMovimientoStock);
        }

        public static void ObtenerEstadosMovimiento(Data.dsStock.ESTADO_MOVIMIENTOS_STOCKDataTable dtEstadosMovimientoStock)
        {
            DAL.EstadoMovimientoStockDAL.ObtenerEstados(dtEstadosMovimientoStock);
        }

        public static EstadoMovimientoStock GetEstadoEntity(StockEnum.EstadoMovimientoStock estado)
        {
            return GetAll().Where(p => p.Codigo == (int)estado).Single();
        }

        public static EstadoMovimientoStock GetEstadoEntity(int estado)
        {
            return GetAll().Where(p => p.Codigo == estado).Single();
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
