using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GyCAP.Entidades
{
    public class Conjunto
    {
        private int codigoConjunto;
        private string nombre;
        private int codigoTerminacion;
        private string descripcion;
        private Image imagen;

        public Image Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }        

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private int cantidadStock;        

        public int CodigoConjunto
        {
            get { return codigoConjunto; }
            set { codigoConjunto = value; }
        }
 
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        public int CodigoTerminacion
        {
            get { return codigoTerminacion; }
            set { codigoTerminacion = value; }
        }
        
        public int CantidadStock
        {
            get { return cantidadStock; }
            set { cantidadStock = value; }
        }
    }
}
