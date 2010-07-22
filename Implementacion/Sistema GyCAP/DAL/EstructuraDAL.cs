using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstructuraDAL
    {
        public static void Eliminar(int codigoEstructura)
        {
            string sql = "DELETE FROM ESTRUCTURAS WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object legResponsable, object activoSiNo, Data.dsEstructura ds)
        {
            string sql = @"SELECT estr_codigo, estr_nombre, coc_codigo, pno_codigo, estr_descripcion, estr_activo, estr_fecha_alta, estr_fecha_modificacion, e_legajo 
                          FROM CONJUNTOS WHERE 1=1";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[6];
            //Empecemos a armar la consulta, revisemos que filtros aplican - NOMBRE
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND estr_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            
            //PLANO - Revisamos si pasó algun valor y si es un integer
            if (codPlano != null && codPlano.GetType() == cantidadParametros.GetType())
            {
                sql += " AND pno_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codPlano);
                cantidadParametros++;
            }
            
            //FECHA CREACION
            if (fechaCreacion != null && fechaCreacion.GetType() == DateTime.Today.GetType())
            {
                sql += " AND estr_fecha_creacion = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaCreacion.ToString();
                cantidadParametros++;
            }

            //COCINA
            if (codCocina != null && codCocina.GetType() == cantidadParametros.GetType())
            {
                sql += " AND coc_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codCocina);
                cantidadParametros++;
            }

            //RESPONSABLE
            if (legResponsable != null && legResponsable.GetType() == cantidadParametros.GetType())
            {
                sql += " AND e_legajo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codPlano);
                cantidadParametros++;
            }

            //ACTIVO
            if (activoSiNo != null && activoSiNo.ToString() != string.Empty)
            {
                sql += " AND estr_activo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(activoSiNo);
                cantidadParametros++;
            }
            
            if (cantidadParametros > 0)
            {
                //Buscamos con filtro, armemos el array de los valores de los parametros
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                DB.FillDataSet(ds, "ESTRUCTURAS", sql, valorParametros);
            }
            else
            {
                //Buscamos sin filtro
                DB.FillDataSet(ds, "ESTRUCTURAS", sql, null);
            }
        }
        
        public static bool PuedeEliminarse(int codigoEstructura)
        {
            return false;
        }
    }
}
