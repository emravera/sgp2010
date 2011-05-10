using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MateriaPrima
    {
        private int codigoMateriaPrima;
        private int codigoUnidadMedida;
        private string nombre;
        private string descripcion;
        private int cantidadStock;
        private decimal costo;
        private UbicacionStock ubicacionStock;
        private int esPrincipal;
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        
        public int EsPrincipal
        {
            get { return esPrincipal; }
            set { esPrincipal = value; }
        }
        
        public UbicacionStock UbicacionStock
        {
            get { return ubicacionStock; }
            set { ubicacionStock = value; }
        }

        public int CodigoMateriaPrima
        {
            get { return codigoMateriaPrima; }
            set { codigoMateriaPrima = value; }
        }
        
        public int CodigoUnidadMedida
        {
            get { return codigoUnidadMedida; }
            set { codigoUnidadMedida = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }       
  
        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }
    }
}
