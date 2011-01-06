using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class MateriaPrimaPrincipalBLL
    {

        public static int Insertar(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
           if (DAL.MateriaPrimaPrincipalDAL.EsMateriaPrima(materiaPrima)) throw new Entidades.Excepciones.ElementoExistenteException();
           //Si no existe la inserta en la BAse de datos
           return DAL.MateriaPrimaPrincipalDAL.Insertar(materiaPrima);
       }

        public static void ObtenerTodos(Data.dsMateriaPrima ds)
        {
            DAL.MateriaPrimaPrincipalDAL.ObtenerTodos(ds);
        }
        //MEtodo que se llama desde plan MP Anual
        public static void ObtenerMPPrincipales(Data.dsPlanMateriasPrimas ds)
        {
            DAL.MateriaPrimaPrincipalDAL.ObtenerMPPrincipales(ds);
        }


        public static void Eliminar(int codigo)
        {
            DAL.MateriaPrimaPrincipalDAL.Eliminar(codigo);
        }
        public static void Actualizar(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
            if (DAL.MateriaPrimaPrincipalDAL.ModificarMateriaPrima(materiaPrima)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Si no existe la modifica en la Base de datos
            else DAL.MateriaPrimaPrincipalDAL.Actualizar(materiaPrima);
        }

    }
}
