using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class ClienteBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda (se debe sobrecargar)
        public static void ObtenerTodos(Data.dsCliente ds)
        {
            DAL.ClienteDAL.ObtenerCliente(ds);
        }

        public static void ObtenerTodos(System.Data.DataTable dt)
        {
            DAL.ClienteDAL.ObtenerCliente(dt);
        }

        public static void ObtenerTodos(string nombre, string estadoCliente, Data.dsCliente dsCliente)
        {
            DAL.ClienteDAL.ObtenerCliente(nombre, estadoCliente, dsCliente);
        }

        public static long Insertar(Entidades.Cliente cliente)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsCliente(cliente)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.ClienteDAL.Insertar(cliente);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.ClienteDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.ClienteDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Cliente cliente)
        {
            DAL.ClienteDAL.Actualizar(cliente);
        }

        public static bool EsCliente(Entidades.Cliente cliente)
        {
            return DAL.ClienteDAL.esCliente(cliente);
        }
        
    }
}
