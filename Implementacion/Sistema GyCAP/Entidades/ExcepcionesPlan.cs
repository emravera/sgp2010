using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class ExcepcionesPlan
    {
        public enum TipoExcepcion 
        { 
            MateriaPrima, 
            Capacidad, 
            Fecha, 
            SinCocinaBase, 
            ParteSinHojaRuta, 
            CocinaSinEstructuraActiva,
            CentroTrabajoInactivo,
            SinUbicacionStock
        };

        public enum TipoElemento
        {
            DetalleHojaRuta,
            OrdenTrababjo,
            OrdenProduccion,
            MateriaPrima,
            Parte
        };

        string nombre;
        TipoExcepcion tipo;
        string descripcion;
       
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }        

        public TipoExcepcion Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        
    }
}
