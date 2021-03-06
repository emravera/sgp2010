﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class MateriaPrimaBLL
    {
        /// <summary>
        /// Obitne una materia prima por su código.
        /// </summary>
        /// <param name="codigoMateriaPrima">El código de la materia prima deseada.</param>
        /// <returns>El objeto materiaPrima con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la materia prima.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.MateriaPrima ObtenerMateriaPrima(int codigoMateriaPrima)
        {
            return DAL.MateriaPrimaDAL.ObtenerMateriaPrima(codigoMateriaPrima);
        }
        
        public static void ObtenerTodos(Data.dsMateriaPrima ds)
        {
            DAL.MateriaPrimaDAL.ObtenerTodos(ds);
        }
        //Metodo que se llama desde el formulario planificacion materias primas
        public static void ObtenerMP(Data.dsPlanMateriasPrimas ds)
        {
            DAL.MateriaPrimaDAL.ObtenerMP(ds);
        }
        public static void ObtenerTodos(System.Data.DataTable dtMateriaPrima)
        {
            DAL.MateriaPrimaDAL.ObtenerTodos(dtMateriaPrima);
        }
        //Metodo que acepta el Datatable
        public static void ObtenerMP(DataTable dtMateriaPrima)
        {
            DAL.MateriaPrimaDAL.ObtenerMP(dtMateriaPrima);
        }
        //Metodo que devuelve el precio de una materia prima
        public static decimal ObtenerPrecioMP(decimal codigoMP)
        {
            return DAL.MateriaPrimaDAL.ObtenerPrecioMP(codigoMP);
        }
    }
}
