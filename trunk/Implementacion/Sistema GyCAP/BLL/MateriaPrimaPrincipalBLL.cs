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

    }
}
