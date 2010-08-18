using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanMensualDAL
    {
        //METODO DE BUSQUEDA
        //BUSQUEDA
        public static void ObtenerTodos(int anio, string mes, Data.dsPlanMensual ds)
        {
             //Consulta para mes solo
            string sql = @"SELECT pm.pmes_codigo, pm.pan_codigo, pa.pan_anio, pm.pmes_mes, pm.pmes_fechacreacion
                        FROM PLANES_MENSUALES pm, PLANES_ANUALES as pa 
                        WHERE pm.pan_codigo=pa.pan_codigo";

            object[] valorParametros = { null };
            object[] valoresPram = { null, null };

            //Si busca solo por el nombre
            if (mes != String.Empty && anio == 0)
            {
                //Agrego la busqueda por nombre
                sql = sql + " and pm.pmes_mes LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                mes = "%" + mes + "%";
                valorParametros.SetValue(mes, 0);
            }
            else if (mes == string.Empty && anio != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " and pa.pan_anio=@p0";
                valorParametros.SetValue(anio, 0);
            }
            else if (mes != string.Empty && anio != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " and pa.pan_anio=@p0 and pm.pmes LIKE @p1";
                mes = "%" + anio + "%";
                //Le doy valores a la estructura
                valoresPram.SetValue(anio, 0);
                valoresPram.SetValue(mes, 1);
            }

            //Ejecuto el comando a la BD
            try
            {
                if (valorParametros.GetValue(0) == null && valoresPram.GetValue(0) == null)
                {
                    //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                    DB.FillDataSet(ds, "PLANES_MENSUALES", sql, null);
                }
                else
                {
                    if (valoresPram.GetValue(0) == null)
                    {
                        DB.FillDataSet(ds, "PLANES_MENSUALES", sql, valorParametros);
                    }
                    else
                    {
                        DB.FillDataSet(ds, "PLANES_MENSUALES", sql, valoresPram);
                    }
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
