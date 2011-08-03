﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class HojaRutaBLL
    {
        public static readonly int hojaRutaActiva = 1;
        public static readonly int hojaRutaInactiva = 0;

        public static void ObtenerHojasRuta(object nombre, object activa, Data.dsHojaRuta dsHojaRuta, bool obtenerDetalle)
        {
            object estado = null;
            if (activa != null && Convert.ToInt32(activa.ToString()) == hojaRutaActiva || Convert.ToInt32(activa.ToString()) == hojaRutaInactiva) { estado = activa; };
            DAL.HojaRutaDAL.ObtenerHojasRuta(nombre, estado, dsHojaRuta, obtenerDetalle);
        }

        public static void ObtenerHojasRuta(DataTable dtHojasRuta)
        {
            DAL.HojaRutaDAL.ObtenerHojasRuta(dtHojasRuta);
        }

        public static int Insertar(Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsHojaRuta.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            if (DAL.HojaRutaDAL.EsHojaRuta(rowhoja.HR_NOMBRE, Convert.ToInt32(rowhoja.HR_CODIGO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            return DAL.HojaRutaDAL.Insertar(dsHojaRuta);
        }
        
        public static void Actualizar(Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsHojaRuta.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            if (DAL.HojaRutaDAL.EsHojaRuta(rowhoja.HR_NOMBRE, Convert.ToInt32(rowhoja.HR_CODIGO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.HojaRutaDAL.Actualizar(dsHojaRuta);
        }

        public static void Eliminar(int codigoHoja)
        {
            if (!DAL.HojaRutaDAL.PuedeEliminarse(codigoHoja)) throw new Entidades.Excepciones.ElementoEnTransaccionException();
            DAL.HojaRutaDAL.Eliminar(codigoHoja);
        }
    }
}
