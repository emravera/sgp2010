using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstadoMaquinaBLL
    {
        public static long Insertar(Entidades.EstadoMaquina estadoMaquina)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoMaquina(estadoMaquina)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoMaquinaDAL.Insertar(estadoMaquina);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoMaquinaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoMaquinaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.EstadoMaquina estadoMaquina)
        {
            DAL.EstadoMaquinaDAL.Actualizar(estadoMaquina);
        }

        public static bool EsEstadoMaquina(Entidades.EstadoMaquina estadoMaquina)
        {
            return DAL.EstadoMaquinaDAL.EsEstadoMaquina(estadoMaquina);
        }

        public static void ObtenerTodos(string nombre, Data.dsEstadoMaquina dsEstadoMaquina)
        {
            DAL.EstadoMaquinaDAL.ObtenerEstadosMaquina(nombre, dsEstadoMaquina);
        }

        public static void ObtenerTodos(Data.dsEstadoMaquina dsEstadoMaquina)
        {
            DAL.EstadoMaquinaDAL.ObtenerEstadosMaquina(dsEstadoMaquina);
        }

        public static void ObtenerTodos(Data.dsMaquina dsMaquina)
        {
            DAL.EstadoMaquinaDAL.ObtenerEstadosMaquina(dsMaquina);
        }
    }
}
