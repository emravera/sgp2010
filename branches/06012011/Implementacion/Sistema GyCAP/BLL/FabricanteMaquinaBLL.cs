using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class FabricanteMaquinaBLL
    {
        public static long Insertar(Entidades.FabricanteMaquina fabricanteMaquina)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsFabricanteMaquina(fabricanteMaquina)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.FabricanteMaquinaDAL.Insertar(fabricanteMaquina);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.FabricanteMaquinaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.FabricanteMaquinaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.FabricanteMaquina fabricanteMaquina)
        {
            DAL.FabricanteMaquinaDAL.Actualizar(fabricanteMaquina);
        }

        public static bool EsFabricanteMaquina(Entidades.FabricanteMaquina fabricanteMaquina)
        {
            return DAL.FabricanteMaquinaDAL.EsFabricanteMaquina(fabricanteMaquina);
        }

        public static void ObtenerTodos(string nombre, Data.dsFabricanteMaquina dsFabricanteMaquina)
        {
            DAL.FabricanteMaquinaDAL.ObtenerFabricanteMaquina(nombre, dsFabricanteMaquina);
        }

        public static void ObtenerTodos(Data.dsFabricanteMaquina dsFabricanteMaquina)
        {
            DAL.FabricanteMaquinaDAL.ObtenerFabricanteMaquina(dsFabricanteMaquina);
        }

        public static void ObtenerTodos(Data.dsMaquina dsMaquina)
        {
            DAL.FabricanteMaquinaDAL.ObtenerFabricanteMaquina(dsMaquina);
        }
    }
}
