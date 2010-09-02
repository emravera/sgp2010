using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class CentroTrabajoDAL
    {
        public static readonly int TipoHombre = 1;
        public static readonly int TipoMaquina = 2;
        public static readonly int CentroInactivo = 0;
        public static readonly int CentroActivo = 1;
        
        public static void Insertar(Data.dsCentroTrabajo ds)
        {
            string sql = @"INSERT INTO [CENTROS_TRABAJOS] 
                           ([cto_nombre], 
                            [sec_codigo], 
                            [cto_tipo], 
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
                            [cto_costociclo])
                            VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13) SELECT @@Identity";

            Data.dsCentroTrabajo.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsCentroTrabajo.CENTROS_TRABAJOSRow;
            object[] valoresParametros = { row.CTO_NOMBRE,
                                           row.SEC_CODIGO,
                                           row.CTO_TIPO,
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
                                           row.CTO_COSTOCICLO };

            string sqlTurnos = "INSERT INTO [TURNOSXCENTROTRABAJO] ([tur_codigo], [cto_codigo]) VALUES(@p0, @p1) SELECT @@Identity";
            
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                row.BeginEdit();
                row.CTO_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                row.EndEdit();
                foreach (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow rows in (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select(null, null, DataViewRowState.Added))
                {
                    valoresParametros = new object[] { rows.TUR_CODIGO, row.CTO_CODIGO };
                    rows.BeginEdit();
                    rows.TXCT_CODIGO = Convert.ToInt32(DB.executeScalar(sqlTurnos, valoresParametros, transaccion));
                    rows.CTO_CODIGO = row.CTO_CODIGO;
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

        public static void Actualizar(Data.dsCentroTrabajo ds)
        {
            string sqlCentro = @"UPDATE CENTROS_TRABAJOS SET 
                                cto_nombre = @p0, 
                                sec_codigo = @p1, 
                                cto_tipo = @p2, 
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
                                cto_costociclo = @p13 
                                WHERE cto_codigo = @p14";

            Data.dsCentroTrabajo.CENTROS_TRABAJOSRow row = ds.CENTROS_TRABAJOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsCentroTrabajo.CENTROS_TRABAJOSRow;
            object[] valorParametros = { row.CTO_NOMBRE, 
                                           row.SEC_CODIGO,
                                           row.CTO_TIPO,
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
                                           row.CTO_CODIGO };

            string sqlITurnos = "INSERT INTO [TURNOSXCENTROTRABAJO] ([tur_codigo], [cto_codigo]) VALUES(@p0, @p1) SELECT @@Identity";
            string sqlDTurnos = "DELETE FROM TURNOSXCENTROTRABAJO WHERE txct_codigo = @p0";

            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                DB.executeNonQuery(sqlCentro, valorParametros, transaccion);                

                foreach (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow rows in (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select(null, null, DataViewRowState.Deleted))
                {
                    valorParametros = new object[] { Convert.ToInt32(rows["txct_codigo", System.Data.DataRowVersion.Original]) };
                    DB.executeNonQuery(sqlDTurnos, valorParametros, transaccion);
                }

                foreach (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow rows in (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow[])ds.TURNOSXCENTROTRABAJO.Select(null, null, DataViewRowState.Added))
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
            //ver condiciones - gonzalo
            return false;
        }

        //Determikna si existe un centro de trabajo dado su nombre y sector
        public static bool EsCentroTrabajo(Entidades.CentroTrabajo centro)
        {
            string sql = "SELECT count(cto_codigo) FROM CENTROS_TRABAJOS WHERE cto_nombre = @p0 AND sec_codigo = @p1";
            object[] valoresParametros = { centro.Nombre, centro.Sector.Codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObetenerCentrosTrabajo(object nombre, object tipo, object sector, object estado, DataTable dtCentrosTrabajo)
        {
            string sql = @"SELECT cto_codigo, cto_nombre, sec_codigo, cto_tipo, cto_horastrabajonormal, cto_horastrabajoextendido,
                                  cto_activo, cto_descripcion, cto_capacidadciclo, cto_horasciclo, cto_tiempoantes, cto_tiempodespues, 
                                  cto_eficiencia, cto_costohora, cto_costociclo FROM CENTROS_TRABAJOS WHERE 1=1 ";

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
                sql += " AND cto_tipo = @p" + cantidadParametros;
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
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static void ObetenerCentrosTrabajo(object nombre, object tipo, object sector, object estado, Data.dsCentroTrabajo ds)
        {
            string sql = @"SELECT cto_codigo, cto_nombre, sec_codigo, cto_tipo, cto_horastrabajonormal, cto_horastrabajoextendido,
                                  cto_activo, cto_descripcion, cto_capacidadciclo, cto_horasciclo, cto_tiempoantes, cto_tiempodespues, 
                                  cto_eficiencia, cto_costohora, cto_costociclo FROM CENTROS_TRABAJOS WHERE 1=1 ";

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
                sql += " AND cto_tipo = @p" + cantidadParametros;
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
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static void ObtenerCentroTrabajo(int codigoCentro, bool turnos, Data.dsHojaRuta ds)
        {
            string sql = @"SELECT cto_codigo, cto_nombre, sec_codigo, cto_tipo, cto_horastrabajonormal, cto_horastrabajoextendido,
                                  cto_activo, cto_descripcion, cto_capacidadciclo, cto_horasciclo, cto_tiempoantes, cto_tiempodespues, 
                                  cto_eficiencia, cto_costohora, cto_costociclo FROM CENTROS_TRABAJOS WHERE cto_codigo = @p0";

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
        
        private static void ObtenerTurnosCentro(Data.dsCentroTrabajo ds)
        {
            string sql = "SELECT txct_codigo, tur_codigo, cto_codigo FROM TURNOSXCENTROTRABAJO WHERE cto_codigo = @p0";
            object[] valorParametros;
            foreach (Data.dsCentroTrabajo.CENTROS_TRABAJOSRow row in ds.CENTROS_TRABAJOS)
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
