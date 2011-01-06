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
        public static void ObetenerOperaciones(Data.dsOperacionesFabricacion dsOperaciones, string nombre, string codificacion)
        {
            DAL.OperacionDAL.buscarOperacion(dsOperaciones,nombre,codificacion);
        }
        //METODO INSERCION
        public static void InsertarOperacion(Entidades.OperacionFabricacion operacion)
        {
            DAL.OperacionDAL.Insertar(operacion);
        }
        //METODO MODIFICACIÓN
        public static void ModificarOperacion(Entidades.OperacionFabricacion operacion)
        {
            DAL.OperacionDAL.ModificarOperacion(operacion);
        }
        //METODO ELIMINACIÓN
        public static void EliminarOperacion(int codigoOperacion)
        {
            DAL.OperacionDAL.EliminarOperacion(codigoOperacion);
        }
    }
}
