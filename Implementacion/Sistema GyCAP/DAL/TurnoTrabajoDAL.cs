using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class TurnoTrabajoDAL
    {
        public static int Insertar(Entidades.TurnoTrabajo turno)
        {
            string sql = "INSERT INTO [TURNOS_TRABAJO] ([tur_nombre], [tur_horainicio], [tur_horafin]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
            object[] valorParametros = { turno.Nombre, turno.HoraInicio, turno.HoraFin };
            try
            {
                turno.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                return turno.Codigo;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.TurnoTrabajo turno)
        {
            string sql = "UPDATE TURNOS_TRABAJO SET tur_nombre = @p0, tur_horainicio = @p1, tur_horafin = @p2 WHERE tur_codigo = @p3";
            object[] valorParametros = { turno.Nombre, turno.HoraInicio, turno.HoraFin, turno.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int codigoTurno)
        {
            string sql = "DELETE FROM TURNOS_TRABAJO WHERE tur_codigo = @p0";
            object[] valorParametros = { codigoTurno };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerTurnos(DataTable dtTurnos)
        {
            string sql = "SELECT tur_codigo, tur_nombre, tur_horainicio, tur_horafin FROM TURNOS_TRABAJO";
            try
            {
                DB.FillDataTable(dtTurnos, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerTurnosPorCentros(DataTable dtTurnosPorCentros)
        {
            string sql = "SELECT txct_codigo, tur_codigo, cto_codigo FROM TURNOSXCENTROTRABAJO";
            try
            {
                DB.FillDataTable(dtTurnosPorCentros, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(tur_codigo) FROM TURNOSXCENTROTRABAJO WHERE tur_codigo = @p0";
            object[] valorParametros = { codigo };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerTurno(int codigoTurno, Data.dsHojaRuta ds)
        {
            string sql = "SELECT tur_codigo, tur_nombre, tur_horainicio, tur_horafin FROM TURNOS_TRABAJO WHERE tur_codigo = @p0";
            object[] valoresParametros = { codigoTurno };
            try
            {
                DB.FillDataTable(ds.TURNOS_TRABAJO, sql, valoresParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsTurno(Entidades.TurnoTrabajo turno)
        {
            string sql = "SELECT count(tur_codigo) FROM TURNOS_TRABAJO WHERE tur_nombre = @p0 AND tur_codigo <> @p1";
            object[] parametros = { turno.Nombre, turno.Codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
