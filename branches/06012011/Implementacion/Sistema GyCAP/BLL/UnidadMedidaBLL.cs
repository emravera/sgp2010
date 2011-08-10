using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{

    public class UnidadMedidaBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, int idTipo, Data.dsPlanMP ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(nombre, idTipo, ds);
        }

        //Obtiene una unidad de medida a partir de su codigo
        public static string ObtenerUnidad(int codigoUnidad)
        {
            return DAL.UnidadMedidaDAL.ObtenerUnidad(codigoUnidad);
        }
                
        //Metodo para buscar todos los valores pasandole el datatable
        public static void ObtenerTodos(DataTable dtUnidadMedida)
        {
            DAL.UnidadMedidaDAL.ObtenerTodos(dtUnidadMedida);
        }
        
        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.UnidadMedidaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.UnidadMedidaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        //Guardado de Datos
        public static int Insertar(Entidades.UnidadMedida unidadMedida)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsUnidadMedida(unidadMedida)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.UnidadMedidaDAL.Insertar(unidadMedida);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsUnidadMedida(Entidades.UnidadMedida unidadMedida)
        {
            return DAL.UnidadMedidaDAL.esUnidadMedida(unidadMedida);
        }

        //Actualización de los datos
        public static void Actualizar(Entidades.UnidadMedida unidadMedida)
        {
            if (EsUnidadMedida(unidadMedida)) throw new Entidades.Excepciones.ElementoExistenteException();
            DAL.UnidadMedidaDAL.Actualizar(unidadMedida);
        }

        public static Entidades.UnidadMedida AsUnidadMedidaEntity(Data.dsEstructuraProducto.UNIDADES_MEDIDARow row)
        {
            Entidades.UnidadMedida umed = new GyCAP.Entidades.UnidadMedida()
            {
                Codigo = Convert.ToInt32(row.UMED_CODIGO),
                Nombre = row.UMED_ABREVIATURA,
                Abreviatura = row.UMED_ABREVIATURA,
                Tipo = BLL.TipoUnidadMedidaBLL.AsTipoUnidadMedidaEntity(row.TIPOS_UNIDADES_MEDIDARow)
            };

            return umed;
        }
    }
}
