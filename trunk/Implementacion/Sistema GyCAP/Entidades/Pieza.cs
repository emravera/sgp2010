using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Pieza
    {
        private int codigoPieza;
        private string nombre;
        private int codigoTerminacion;
        private int cantidadStock;
        private string descripcion;
        private int codigoEstado;
        private int codigoPlano;
        private string codigoParte;
        private decimal costo;
        private int codigoHojaRuta;

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

        public int CodigoEstado
        {
            get { return codigoEstado; }
            set { codigoEstado = value; }
        }        

        public int CodigoPlano
        {
            get { return codigoPlano; }
            set { codigoPlano = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int CodigoPieza
        {
            get { return codigoPieza; }
            set { codigoPieza = value; }
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
