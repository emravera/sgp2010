﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class ExcepcionesPlanBLL
    {
        //Implementar método Add que se le pase la MP y cantidad faltante y devuelva
        //el objeto ExcepcionPlan formateado para la lista
        //FALTA mp_nombre
        //DESC: Cant. Faltante:XX
        //TIPO: Enumeracion

        //Metodo que crea un objeto estandar con el formato como sigue
        public static Entidades.ExcepcionesPlan Add_ExcepcionMP(decimal cantidad, Entidades.MateriaPrima materiaPrima)
        {
            //Creo el objeto excepcion 
            Entidades.ExcepcionesPlan excepcion = new GyCAP.Entidades.ExcepcionesPlan();
            cantidad = cantidad * -1;
            cantidad = decimal.Round(cantidad, 3);

            excepcion.Nombre = "FALTA: " + materiaPrima.Nombre.ToString();
            excepcion.Tipo = Entidades.ExcepcionesPlan.TipoExcepcion.MateriaPrima;
            string unidadMedida = BLL.UnidadMedidaBLL.ObtenerUnidad(Convert.ToInt32(materiaPrima.CodigoUnidadMedida));
            excepcion.Descripcion = "Faltante: " + cantidad.ToString() + " " + unidadMedida;

            return excepcion;          
        }


        
    }
}
