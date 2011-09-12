using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class OperacionBLL
    {
        //METODOS DE BUSQUEDA
        //Metodo que trae todas las operaciones
        public static void ObetenerOperaciones(DataTable dtOperaciones)
        {
            DAL.OperacionDAL.ObtenerOperaciones(dtOperaciones);
        }
        //Metodo de busqueda con filtro
        public static void ObetenerOperaciones(Data.dsHojaRuta dsOperaciones, string nombre, string codificacion)
        {
            DAL.OperacionDAL.buscarOperacion(dsOperaciones, nombre, codificacion);
        }
        //METODO INSERCION
        public static void InsertarOperacion(Entidades.OperacionFabricacion operacion)
        {
            if (DAL.OperacionDAL.EsOperacion(operacion.Codigo, operacion.Codificacion)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.OperacionDAL.Insertar(operacion);
        }
        //METODO MODIFICACIÓN
        public static void ModificarOperacion(Entidades.OperacionFabricacion operacion)
        {
            if (DAL.OperacionDAL.EsOperacion(operacion.Codigo, operacion.Codificacion)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.OperacionDAL.ModificarOperacion(operacion);
        }
        //METODO ELIMINACIÓN
        public static void EliminarOperacion(int codigoOperacion)
        {
            if (!DAL.OperacionDAL.PuedeEliminarse(codigoOperacion)) { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
            DAL.OperacionDAL.EliminarOperacion(codigoOperacion);
        }

        public static Entidades.OperacionFabricacion AsOperacionFabricacionEntity(Data.dsHojaRuta.OPERACIONESRow row)
        {
            Entidades.OperacionFabricacion operacion = new GyCAP.Entidades.OperacionFabricacion()
            {
                Codigo = Convert.ToInt32(row.OPR_NUMERO),
                Codificacion = row.OPR_CODIGO,
                Descripcion = row.OPR_DESCRIPCION,
                HorasRequeridas = row.OPR_HORASREQUERIDA,
                Nombre = row.OPR_NOMBRE
            };

            return operacion;
        }
    }
}
