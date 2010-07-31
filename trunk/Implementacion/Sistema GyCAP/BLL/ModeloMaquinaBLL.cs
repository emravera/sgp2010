using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class ModeloMaquinaBLL
    {
        public static int Insertar(Entidades.ModeloMaquina modeloMaquina)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsModeloMaquina(modeloMaquina)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.ModeloMaquinaDAL.Insertar(modeloMaquina);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.CapacidadEmpleadoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.CapacidadEmpleadoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.ModeloMaquina modeloMaquina)
        {
            DAL.ModeloMaquinaDAL.Actualizar(modeloMaquina);
        }

        public static bool EsModeloMaquina(Entidades.ModeloMaquina modeloMaquina)
        {
            return DAL.ModeloMaquinaDAL.EsModeloMaquina(modeloMaquina);
        }

        public static void ObtenerTodos(string nombre, Data.dsModeloMaquina dsModeloMaquina)
        {
            DAL.ModeloMaquinaDAL.ObtenerModeloMaquina(nombre, dsModeloMaquina);
        }

        public static void ObtenerTodos(Data.dsMaquina dsMaquina)
        {
            DAL.ModeloMaquinaDAL.ObtenerModeloMaquina(dsMaquina);
        }

    }
}
