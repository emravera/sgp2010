using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.BLL
{
    public class TipoEntidadBLL
    {        
        public static void ObtenerTodos(Data.dsStock.TIPOS_ENTIDADDataTable table)
        {
            DAL.TipoEntidadDAL.ObtenerTodos(table);
        }

        public static IList<TipoEntidad> ObtenerTodos()
        {
            Data.dsStock.TIPOS_ENTIDADDataTable dt = new GyCAP.Data.dsStock.TIPOS_ENTIDADDataTable();

            DAL.TipoEntidadDAL.ObtenerTodos(dt);

            IList<TipoEntidad> lista = new List<TipoEntidad>();

            foreach (Data.dsStock.TIPOS_ENTIDADRow row in dt.Rows)
            {
                lista.Add(AsTipoEntidad(row));
            }

            return lista;
        }

        private static TipoEntidad AsTipoEntidad(Data.dsStock.TIPOS_ENTIDADRow row)
        {
            return new TipoEntidad()
            {
                Codigo = Convert.ToInt32(row.TENTD_CODIGO),
                Nombre = row.TENTD_NOMBRE,
                Descripcion = row.TENTD_DESCRIPCION
            };
        }

        public static TipoEntidad GetTipoEntidadEntity(EntidadEnum.TipoEntidadEnum tipo)
        {
            return GetTipoEntidadEntity((int)tipo);
        }

        public static TipoEntidad GetTipoEntidadEntity(int codigo)
        {
            return ObtenerTodos().Where(p => p.Codigo == codigo).Single();
        }
    }
}
