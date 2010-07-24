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
