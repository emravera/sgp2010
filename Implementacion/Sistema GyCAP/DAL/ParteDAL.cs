using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class ParteDAL
    {
        public static readonly int CostoFijoChecked = 1;
        public static readonly int CostoFijoUnChecked = 0;
        
        public static void Insertar(Data.dsEstructuraProducto dsEstructura)
        {
            string sql = @"INSERT INTO [PARTES] 
                       ([part_nombre],
                        [part_descripcion],
                        [part_codigo],
                        [pno_codigo],
                        [part_costo],
                        [par_codigo],
                        [tpar_codigo],
                        [te_codigo],
                        [hr_codigo],
                        [umed_codigo],
                        [prove_codigo]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10) SELECT @@Identity";

            object plano = DBNull.Value, terminacion = DBNull.Value, hojaRuta = DBNull.Value, proveedor = DBNull.Value;
            Data.dsEstructuraProducto.PARTESRow rowParte = dsEstructura.PARTES.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructuraProducto.PARTESRow;
            if (!rowParte.IsPNO_CODIGONull()) { plano = rowParte.PNO_CODIGO; }
            if (!rowParte.IsTE_CODIGONull()) { terminacion = rowParte.TE_CODIGO; }
            if (!rowParte.IsHR_CODIGONull()) { hojaRuta = rowParte.HR_CODIGO; }
            if (!rowParte.IsPROVE_CODIGONull()) { proveedor = rowParte.PROVE_CODIGO; }

            object[] parametros = { rowParte.PART_NOMBRE,
                                    rowParte.PART_DESCRIPCION,
                                    rowParte.PART_CODIGO,
                                    plano,
                                    rowParte.PART_COSTO,
                                    rowParte.PAR_CODIGO,
                                    rowParte.TPAR_CODIGO,
                                    terminacion,
                                    hojaRuta,
                                    rowParte.UMED_CODIGO,
                                    proveedor };
            
            try
            {
                rowParte.BeginEdit();
                rowParte.PART_NUMERO = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
                rowParte.EndEdit();                
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Data.dsEstructuraProducto dsEstructura)
        {
            string sql = @"UPDATE PARTES SET
                            part_nombre = @p0,
                            part_descripcion = @p1,
                            part_codigo = @p2,
                            pno_codigo = @p3,
                            part_costo = @p4,
                            par_codigo = @p5,
                            tpar_codigo = @p6,
                            te_codigo = @p7,
                            hr_codigo = @p8,
                            umed_codigo = @p9,
                            prove_codigo = @p10 
                            WHERE part_numero = @p11";

            object plano = DBNull.Value, terminacion = DBNull.Value, hojaRuta = DBNull.Value, proveedor = DBNull.Value;
            Data.dsEstructuraProducto.PARTESRow rowParte = dsEstructura.PARTES.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructuraProducto.PARTESRow;
            if (!rowParte.IsPNO_CODIGONull()) { plano = rowParte.PNO_CODIGO; }
            if (!rowParte.IsTE_CODIGONull()) { terminacion = rowParte.TE_CODIGO; }
            if (!rowParte.IsHR_CODIGONull()) { hojaRuta = rowParte.HR_CODIGO; }
            if (!rowParte.IsPROVE_CODIGONull()) { proveedor = rowParte.PROVE_CODIGO; }

            object[] parametros = { rowParte.PART_NOMBRE,
                                    rowParte.PART_DESCRIPCION,
                                    rowParte.PART_CODIGO,
                                    plano,
                                    rowParte.PART_COSTO,
                                    rowParte.PAR_CODIGO,
                                    rowParte.TPAR_CODIGO,
                                    terminacion,
                                    hojaRuta,
                                    rowParte.UMED_CODIGO,
                                    proveedor,
                                    rowParte.PART_NUMERO };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool PuedeEliminarse(int numeroParte)
        {
            string sql1 = "SELECT count(comp_codigo) FROM COMPUESTOS_PARTES WHERE part_numero_padre = @p0 OR part_numero_hijo = @p0";
            object[] parametros = { numeroParte };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql1, parametros, null)) == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int numeroParte)
        {
            string sql = "DELETE FROM PARTES WHERE part_numero = @p0";
            object[] parametros = { numeroParte };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerPartes(object nombre, object codigo, object terminacion, object tipo, object estado, object plano, DataTable dtPartes)
        {
            string sql = @"SELECT part_numero, part_nombre, part_descripcion, part_codigo, pno_codigo, part_costo, 
                            par_codigo, tpar_codigo, te_codigo, hr_codigo, umed_codigo, prove_codigo 
                           FROM PARTES WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[6];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND part_nombre LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            if (codigo != null && codigo.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND part_codigo LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                codigo = "%" + codigo + "%";
                valoresFiltros[cantidadParametros] = codigo;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (terminacion != null && terminacion.GetType() == cantidadParametros.GetType())
            {
                sql += " AND te_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(terminacion);
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (tipo != null && tipo.GetType() == cantidadParametros.GetType())
            {
                sql += " AND tpar_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(tipo);
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND par_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (plano != null && plano.GetType() == cantidadParametros.GetType())
            {
                sql += " AND pno_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(plano);
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
                try
                {
                    DB.FillDataTable(dtPartes, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(dtPartes, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static bool EsParte(string nombre, string codigo, int numero)
        {
            string sql = "SELECT count(part_numero) FROM PARTES WHERE part_nombre = @p0 AND part_codigo = @p1 AND part_numero <> @p2";
            object[] parametros = { nombre, codigo, numero };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
