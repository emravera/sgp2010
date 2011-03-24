using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ParteBLL
    {
        public static readonly int CostoFijoChecked = 1;
        public static readonly int CostoFijoUnChecked = 0;
        
        public static void ObtenerPartes(object nombre, object codigo, object terminacion, object tipo, object estado, object plano, DataTable dtPartes)
        {
            if (Convert.ToInt32(terminacion) <= 0) { terminacion = null; }
            if (Convert.ToInt32(tipo) <= 0) { tipo = null; }
            if (Convert.ToInt32(estado) <= 0) { estado = null; }
            if (Convert.ToInt32(plano) <= 0) { plano = null; }
            DAL.ParteDAL.ObtenerPartes(nombre, codigo, terminacion, tipo, estado, plano, dtPartes);
        }

        public static void Insertar(Data.dsEstructuraProducto dsParte)
        {
            DAL.ParteDAL.Insertar(dsParte);
        }

        public static void Actualizar(Data.dsEstructuraProducto dsParte)
        {
            DAL.ParteDAL.Actualizar(dsParte);
        }

        public static void Eliminar(int numeroParte)
        {
            if (!DAL.ParteDAL.PuedeEliminarse(numeroParte)) { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
            DAL.ParteDAL.Eliminar(numeroParte);
        }
    }
}
