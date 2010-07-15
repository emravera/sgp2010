using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class MarcaBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, Data.dsMarca ds)
        {
            DAL.MarcaDAL.ObtenerMarca(nombre, ds);
        }
        public static void ObtenerTodos(int idCliente, Data.dsMarca ds)
        {
            DAL.MarcaDAL.ObtenerMarca(idCliente, ds);
        }
        public static void ObtenerTodos(Data.dsMarca ds)
        {
            DAL.MarcaDAL.ObtenerMarca(ds);
        }
        //Metodo para usar desde el formulario de Designaciones (otro Dataset)
        public static void ObtenerTodos(Data.dsDesignacion ds)
        {
            DAL.MarcaDAL.ObtenerMarca(ds);
        }


        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.MarcaDAL.PuedeEliminarse(codigo))
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
        public static int Insertar(Entidades.Marca marca)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsMarca(marca)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.MarcaDAL.Insertar(marca);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsMarca(Entidades.Marca marca)
        {
            return DAL.MarcaDAL.esMarca(marca);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.Marca marca)
        {
            DAL.MarcaDAL.Actualizar(marca);
        }

    }
}
