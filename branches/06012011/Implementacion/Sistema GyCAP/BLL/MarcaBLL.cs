using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class MarcaBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, int idCliente, DataTable dt)
        {
            DAL.MarcaDAL.ObtenerMarca(nombre, idCliente, dt);
        }

        public static void ObtenerTodos(DataTable dtMarca)
        {
            DAL.MarcaDAL.ObtenerMarca(dtMarca);
        }
       
        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.MarcaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.MarcaDAL.Eliminar(codigo);
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
