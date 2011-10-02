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
    public class TipoCentroTrabajoBLL
    {
        public static void GetAll(DataTable dt)
        {
            DAL.TipoCentroTrabajoDAL.GetAll(dt);
        }

        public static SortableBindingList<TipoCentroTrabajo> GetAll()
        {
            Data.dsHojaRuta.TIPOS_CENTRO_TRABAJODataTable dt = new GyCAP.Data.dsHojaRuta.TIPOS_CENTRO_TRABAJODataTable();
            DAL.TipoCentroTrabajoDAL.GetAll(dt);
            SortableBindingList<TipoCentroTrabajo> lista = new SortableBindingList<TipoCentroTrabajo>();

            foreach (Data.dsHojaRuta.TIPOS_CENTRO_TRABAJORow row in dt.Rows)
            {
                lista.Add(new TipoCentroTrabajo()
                {
                    Codigo = Convert.ToInt32(row.TC_CODIGO),
                    Nombre = row.TC_NOMBRE
                });
            }

            return lista;
        }

        public static TipoCentroTrabajo GetTipo(RecursosFabricacionEnum.TipoCentroTrabajo tipo)
        {
            return DAL.TipoCentroTrabajoDAL.GetTipo(tipo);
        }

        public static TipoCentroTrabajo GetTipo(int codigo)
        {
            return DAL.TipoCentroTrabajoDAL.GetTipo(codigo);
        }

        public static TipoCentroTrabajo AsTipoEntity(int codigo, Data.dsHojaRuta ds)
        {
            return new TipoCentroTrabajo()
            {
                Codigo = Convert.ToInt32(ds.TIPOS_CENTRO_TRABAJO.FindByTC_CODIGO(codigo).TC_CODIGO),
                Nombre = ds.TIPOS_CENTRO_TRABAJO.FindByTC_CODIGO(codigo).TC_NOMBRE
            };
        }
    }
}
