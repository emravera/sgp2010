using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class RepuestoDAL
    {
        public static int Insertar(Entidades.Repuesto repuesto)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO REPUESTOS (TREP_CODIGO,
                                                    REP_NOMBRE, 
                                                    REP_DESCRIPCION,
                                                    REP_CANTIDADSTOCK,
                                                    REP_COSTO) VALUES (@p0,@p1,@p2,@p3,@p4) SELECT @@Identity";
            
            object[] valorParametros = {repuesto.Tipo.Codigo, repuesto.Nombre, repuesto.Descripcion,
                                        repuesto.CantidadStock,repuesto.Costo };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM REPUESTOS WHERE REP_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Repuesto repuesto)
        {
            string sql = @"UPDATE REPUESTOS SET TREP_CODIGO = @p1, 
                                                REP_NOMBRE = @p2, 
                                                REP_DESCRIPCION = @p3,
                                                REP_CANTIDADSTOCK = @p4, 
                                                REP_COSTO = @p5 
                                                WHERE TREP_CODIGO = @p0";

            object[] valorParametros = { repuesto.Codigo, repuesto.Tipo.Codigo, repuesto.Nombre, repuesto.Descripcion,
                                         repuesto.CantidadStock, repuesto.Costo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsRepuesto(Entidades.Repuesto repuesto)
        {
            string sql = "SELECT count(REP_CODIGO) FROM REPUESTOS WHERE REP_NOMBRE = @p0";
            object[] valorParametros = { repuesto.Nombre };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerRepuesto(string nombre, int tipoRepuesto, Data.dsMantenimiento ds)
        {
            string sql = @"SELECT * FROM REPUESTOS WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican

            // NOMBRE
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND REP_NOMBRE LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }

            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (tipoRepuesto != -1)
            {
                sql += " AND TREP_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(tipoRepuesto);
                cantidadParametros++;
            }

            try
            {
                if (cantidadParametros > 0)
                {
                    //Buscamos con filtro, armemos el array de los valores de los parametros
                    object[] valorParametros = new object[cantidadParametros];
                    for (int i = 0; i < cantidadParametros; i++)
                    {
                        valorParametros[i] = valoresFiltros[i];
                    }
                    DB.FillDataSet(ds, "REPUESTOS", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "REPUESTOS", sql, null);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerRepuesto(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT * FROM REPUESTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "REPUESTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //        public static void ObtenerTipoRepuesto(Data.dsCliente ds)
        //        {
        //            string sql = @"SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION
        //                           FROM TIPOS_REPUESTOS";
        //            try
        //            {
        //                //Se llena el Dataset
        //                DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
        //            }
        //            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        //        }

        public static void ObtenerRepuesto(DataTable dtRepuesto)
        {
            string sql = @"SELECT * FROM REPUESTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtRepuesto, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sqlRRMAN = "SELECT count(RRMAN_CODIGO) FROM REPUESTOS_REGISTRO_MANTENIMIENTO  WHERE rep_codigo = @p0";
            string sqlRDPMAN = "SELECT count(RDPMAN_CODIGO) FROM REPUESTOS_DETALLE_PLAN_MAN WHERE rep_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoRRMAN = Convert.ToInt32(DB.executeScalar(sqlRRMAN, valorParametros, null));
                int resultadoRDPMAN = Convert.ToInt32(DB.executeScalar(sqlRDPMAN, valorParametros, null));
                if (resultadoRRMAN == 0 && resultadoRDPMAN == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
