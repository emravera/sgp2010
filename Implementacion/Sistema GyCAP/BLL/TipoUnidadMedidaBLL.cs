using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class TipoUnidadMedidaBLL
    {
        public static int Insertar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            //Si existe lanzamos la excepción correspondiente
            if (DAL.TipoUnidadMedidaDAL.EsTipoUnidadMedidaNuevo(tipoUnidadMedida)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.TipoUnidadMedidaDAL.Insertar(tipoUnidadMedida);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.TipoUnidadMedidaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.TipoUnidadMedidaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            if (DAL.TipoUnidadMedidaDAL.EsTipoUnidadMedidaActualizar(tipoUnidadMedida)) throw new Entidades.Excepciones.ElementoExistenteException();
            else DAL.TipoUnidadMedidaDAL.Actualizar(tipoUnidadMedida);
        }

        public static void ObtenerTodos(Data.dsPlanMP ds)
        {
            DAL.TipoUnidadMedidaDAL.ObtenerTipoUnidadMedida(ds);
        }

        public static void ObtenerTodos(string nombre, Data.dsPlanMP ds)
        {
            DAL.TipoUnidadMedidaDAL.ObtenerTipoUnidadMedida(nombre, ds);
        }

        public static void ObtenerTodos(System.Data.DataTable dtTipoUnidad)
        {
            DAL.TipoUnidadMedidaDAL.ObtenerTipoUnidadMedida(dtTipoUnidad);
        }

        public static Entidades.TipoUnidadMedida AsTipoUnidadMedidaEntity(Data.dsEstructuraProducto.TIPOS_UNIDADES_MEDIDARow row)
        {
            Entidades.TipoUnidadMedida tumed = new GyCAP.Entidades.TipoUnidadMedida()
            {
                Codigo = Convert.ToInt32(row.TUMED_CODIGO),
                Nombre = row.TUMED_NOMBRE
            };

            return tumed;
        }

        public static Entidades.TipoUnidadMedida AsTipoUnidadMedidaEntity(Data.dsStock.TIPOS_UNIDADES_MEDIDARow row)
        {
            Entidades.TipoUnidadMedida tumed = new GyCAP.Entidades.TipoUnidadMedida()
            {
                Codigo = Convert.ToInt32(row.TUMED_CODIGO),
                Nombre = row.TUMED_NOMBRE
            };

            return tumed;
        }

        public static Entidades.TipoUnidadMedida GetTipoUnidadMedida(int codigo)
        {
            Data.dsStock.TIPOS_UNIDADES_MEDIDADataTable dt = DAL.TipoUnidadMedidaDAL.GetTipoUnidadMedida(codigo);
            Entidades.TipoUnidadMedida tipo = new GyCAP.Entidades.TipoUnidadMedida();

            if (dt.Rows.Count > 0)
            {
                tipo.Codigo = codigo;
                tipo.Nombre = dt.Rows[0]["tumed_nombre"].ToString();
            }

            return tipo;
        }
    }
}
