using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.DAL
{
    public class CentroTrabajoDAL
    {        
        public static int Insertar(Data.dsHojaRuta ds)
        {
            string sql = @"INSERT INTO [CENTROS_TRABAJOS] 
                           ([cto_nombre], 
                            [sec_codigo], 
                            [ct_tipo], 
                            [cto_horastrabajonormal], 
                            [cto_horastrabajoextendido], 
                            [cto_activo], 
                            [cto_descripcion], 
                            [cto_capacidadciclo], 
                            [cto_horasciclo], 
                            [cto_tiempoantes], 
                            [cto_tiempodespues], 
                            [cto_eficiencia], 
                            [cto_costohora], 
                            [cto_costociclo],
                            [cto_capacidadunidadhora])
                            VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14) SELECT @@Identity";

            Data.dsHojaRuta.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsHojaRuta.CENTROS_TRABAJOSRow;
            object[] valoresParametros = { row.CTO_NOMBRE,
                                           row.SEC_CODIGO,
                                           row.CT_TIPO,
                                           row.CTO_HORASTRABAJONORMAL,
                                           row.CTO_HORASTRABAJOEXTENDIDO,
                                           row.CTO_ACTIVO,
                                           row.CTO_DESCRIPCION,
                                           row.CTO_CAPACIDADCICLO,
                                           row.CTO_HORASCICLO,
                                           row.CTO_TIEMPOANTES,
                                           row.CTO_TIEMPODESPUES,
                                           row.CTO_EFICIENCIA,
                                           row.CTO_COSTOHORA,
                                           row.CTO_COSTOCICLO,
                                           row.CTO_CAPACIDADUNIDADHORA };

            string sqlTurnos = "INSERT INTO [TURNOSXCENTROTRABAJO] ([tur_codigo], [cto_codigo]) VALUES(@p0, @p1) SELECT @@Identity";
            
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                
                int codigo = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                
                foreach (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow rows in (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select(null, null, DataViewRowState.Added))
                {
                    valoresParametros = new object[] { rows.TUR_CODIGO, codigo };
                    rows.BeginEdit();
                    rows.TXCT_CODIGO = Convert.ToInt32(DB.executeScalar(sqlTurnos, valoresParametros, transaccion));
                    rows.CTO_CODIGO = codigo;
                    rows.EndEdit();
                }
                transaccion.Commit();
                return codigo;
            }
            catch (SqlException ex) 
            { 
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); 
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
        }

        public static void Actualizar(Data.dsHojaRuta ds)
        {
            string sqlCentro = @"UPDATE CENTROS_TRABAJOS SET 
                                cto_nombre = @p0, 
                                sec_codigo = @p1, 
                                ct_tipo = @p2, 
                                cto_horastrabajonormal = @p3, 
                                cto_horastrabajoextendido = @p4, 
                                cto_activo = @p5, 
                                cto_descripcion = @p6, 
                                cto_capacidadciclo = @p7, 
                                cto_horasciclo = @p8, 
                                cto_tiempoantes = @p9, 
                                cto_tiempodespues = @p10, 
                                cto_eficiencia = @p11, 
                                cto_costohora = @p12, 
                                cto_costociclo = @p13,
                                cto_capacidadunidadhora = @p14  
                                WHERE cto_codigo = @p15";

            Data.dsHojaRuta.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsHojaRuta.CENTROS_TRABAJOSRow;
            object[] valorParametros = { row.CTO_NOMBRE, 
                                           row.SEC_CODIGO,
                                           row.CT_TIPO,
                                           row.CTO_HORASTRABAJONORMAL,
                                           row.CTO_HORASTRABAJOEXTENDIDO,
                                           row.CTO_ACTIVO,
                                           row.CTO_DESCRIPCION,
                                           row.CTO_CAPACIDADCICLO,
                                           row.CTO_HORASCICLO,
                                           row.CTO_TIEMPOANTES,
                                           row.CTO_TIEMPODESPUES,
                                           row.CTO_EFICIENCIA,
                                           row.CTO_COSTOHORA,
                                           row.CTO_COSTOCICLO,
                                           row.CTO_CAPACIDADUNIDADHORA,
                                           row.CTO_CODIGO };

            string sqlITurnos = "INSERT INTO [TURNOSXCENTROTRABAJO] ([tur_codigo], [cto_codigo]) VALUES(@p0, @p1) SELECT @@Identity";
            string sqlDTurnos = "DELETE FROM TURNOSXCENTROTRABAJO WHERE txct_codigo = @p0";

            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                DB.executeNonQuery(sqlCentro, valorParametros, transaccion);                

                foreach (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow rows in (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select(null, null, DataViewRowState.Deleted))
                {
                    valorParametros = new object[] { Convert.ToInt32(rows["txct_codigo", System.Data.DataRowVersion.Original]) };
                    DB.executeNonQuery(sqlDTurnos, valorParametros, transaccion);
                }

                foreach (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow rows in (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select(null, null, DataViewRowState.Added))
                {
                    valorParametros = new object[] { rows.TUR_CODIGO, rows.CTO_CODIGO };
                    rows.BeginEdit();
                    rows.TXCT_CODIGO = Convert.ToInt32(DB.executeScalar(sqlITurnos, valorParametros, transaccion));
                    rows.EndEdit();
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
        }

        public static void Eliminar(int codigoCentroTrabajo)
        {
            string sqlPadre = "DELETE FROM CENTROS_TRABAJOS WHERE cto_codigo = @p0";
            string sqlHijos = "DELETE FROM TURNOSXCENTROTRABAJO WHERE cto_codigo = @p0";
            object[] valoresParametros = { codigoCentroTrabajo };
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                DB.executeNonQuery(sqlHijos, valoresParametros, transaccion);
                DB.executeNonQuery(sqlPadre, valoresParametros, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }

        }

        public static bool PuedeEliminarse(int codigoCentro)
        {
            string sql1 = "SELECT count(cto_codigo) FROM DETALLE_HOJARUTA WHERE cto_codigo = @p0";
            string sql3 = "SELECT count(cto_codigo) FROM ORDENES_TRABAJO WHERE cto_codigo = @p0";
            object[] parametros = { codigoCentro };

            try
            {
                int r1 = Convert.ToInt32(DB.executeScalar(sql1, parametros, null));                
                int r2 = Convert.ToInt32(DB.executeScalar(sql3, parametros, null));

                if (r1 + r2 == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //Determina si existe un centro de trabajo dado su nombre y sector
        public static bool EsCentroTrabajo(Entidades.CentroTrabajo centro)
        {
            string sql = "SELECT count(cto_codigo) FROM CENTROS_TRABAJOS WHERE cto_nombre = @p0 AND sec_codigo = @p1 AND cto_codigo <> @p2";
            object[] valoresParametros = { centro.Nombre, centro.Sector.Codigo, centro.Codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObetenerCentrosTrabajo(object nombre, object tipo, object sector, object estado, DataTable dtCentrosTrabajo)
        {
            string sql = @"SELECT cto_codigo, cto_nombre, sec_codigo, ct_tipo, cto_horastrabajonormal, cto_horastrabajoextendido,
                                  cto_activo, cto_descripcion, cto_capacidadciclo, cto_horasciclo, cto_tiempoantes, cto_tiempodespues, 
                                  cto_eficiencia, cto_costohora, cto_costociclo, cto_capacidadunidadhora FROM CENTROS_TRABAJOS WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[4];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND cto_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }

            if (tipo != null && tipo.GetType() == cantidadParametros.GetType())
            {
                //Si aplica el filtro lo usamos
                sql += " AND ct_tipo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = tipo;
                cantidadParametros++;
            }

            if (sector != null && sector.GetType() == cantidadParametros.GetType())
            {
                //Si aplica el filtro lo usamos
                sql += " AND sec_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = sector;
                cantidadParametros++;
            }

            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                //Si aplica el filtro lo usamos
                sql += " AND cto_activo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = estado;
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
                    DB.FillDataTable(dtCentrosTrabajo, sql, valorParametros);
                    
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(dtCentrosTrabajo, sql, null);                    
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static void ObetenerCentrosTrabajo(object nombre, object tipo, object sector, object estado, Data.dsHojaRuta ds)
        {
            string sql = @"SELECT cto_codigo, cto_nombre, sec_codigo, ct_tipo, cto_horastrabajonormal, cto_horastrabajoextendido,
                                  cto_activo, cto_descripcion, cto_capacidadciclo, cto_horasciclo, cto_tiempoantes, cto_tiempodespues, 
                                  cto_eficiencia, cto_costohora, cto_costociclo, cto_capacidadunidadhora FROM CENTROS_TRABAJOS WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[4];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND cto_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }

            if (tipo != null && tipo.GetType() == cantidadParametros.GetType())
            {
                //Si aplica el filtro lo usamos
                sql += " AND ct_tipo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = tipo;
                cantidadParametros++;
            }

            if (sector != null && sector.GetType() == cantidadParametros.GetType())
            {
                //Si aplica el filtro lo usamos
                sql += " AND sec_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = sector;
                cantidadParametros++;
            }

            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                //Si aplica el filtro lo usamos
                sql += " AND cto_activo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = estado;
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
                    DB.FillDataSet(ds, "CENTROS_TRABAJOS", sql, valorParametros);
                    ObtenerTurnosCentro(ds);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataSet(ds, "CENTROS_TRABAJOS", sql, null);
                    ObtenerTurnosCentro(ds);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static void ObtenerCentroTrabajo(int codigoCentro, bool turnos, Data.dsHojaRuta ds)
        {
            string sql = @"SELECT cto_codigo, cto_nombre, sec_codigo, ct_tipo, cto_horastrabajonormal, cto_horastrabajoextendido,
                                  cto_activo, cto_descripcion, cto_capacidadciclo, cto_horasciclo, cto_tiempoantes, cto_tiempodespues, 
                                  cto_eficiencia, cto_costohora, cto_costociclo, cto_capacidadunidadhora FROM CENTROS_TRABAJOS WHERE cto_codigo = @p0";

            object[] valoresParametros = { codigoCentro };

            try
            {
                DB.FillDataTable(ds.CENTROS_TRABAJOS, sql, valoresParametros);
                if (turnos)
                {

                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        private static void ObtenerTurnosCentro(Data.dsHojaRuta ds)
        {
            string sql = "SELECT txct_codigo, tur_codigo, cto_codigo FROM TURNOSXCENTROTRABAJO WHERE cto_codigo = @p0";
            object[] valorParametros;
            foreach (Data.dsHojaRuta.CENTROS_TRABAJOSRow row in ds.CENTROS_TRABAJOS)
            {
                valorParametros = new object[] { row.CTO_CODIGO };
                DB.FillDataTable(ds.TURNOSXCENTROTRABAJO, sql, valorParametros);
            }
        }

        private static void ObtenerTurnosCentro(int codigoCentro, Data.dsHojaRuta ds)
        {
            string sql = "SELECT txct_codigo, tur_codigo, cto_codigo FROM TURNOSXCENTROTRABAJO WHERE cto_codigo = @p0";
            object[] valorParametros = { codigoCentro };
            DB.FillDataTable(ds.TURNOSXCENTROTRABAJO, sql, valorParametros);

            foreach (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow rowTxCTO in (Data.dsHojaRuta.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select("cto_codigo = " + codigoCentro))
            {
                TurnoTrabajoDAL.ObtenerTurno(Convert.ToInt32(rowTxCTO.TUR_CODIGO), ds);
            }
        }
    }
}
