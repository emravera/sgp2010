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
        private string descripcion;
        private int codigoEstado;
        private int codigoPlano;
        private string codigoParte;
        private decimal costo;
        private int codigoHojaRuta;
        private int costoFijo;

        public int CostoFijo
        {
            get { return costoFijo; }
            set { costoFijo = value; }
        }

        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }        

        public int CodigoHojaRuta
        {
            get { return codigoHojaRuta; }
            set { codigoHojaRuta = value; }
        }

        public string CodigoParte
        {
            get { return codigoParte; }
            set { codigoParte = value; }
        }

        public int CodigoPlano
        {
            get { return codigoPlano; }
            set { codigoPlano = value; }
        }

        public int CodigoEstado
        {
            get { return codigoEstado; }
            set { codigoEstado = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
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
        
    }
}
