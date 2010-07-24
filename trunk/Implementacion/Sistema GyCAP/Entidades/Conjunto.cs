﻿using System;
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
        private int cantidadStock;
        private int codigoEstado;
        private int codigoPlano;

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