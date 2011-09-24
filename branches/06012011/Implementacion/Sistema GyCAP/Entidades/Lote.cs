using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Lote
    {
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string numeroSerieDesde;

        public string NumeroSerieDesde
        {
            get { return numeroSerieDesde; }
            set { numeroSerieDesde = value; }
        }
        private string numeroSerieHasta;

        public string NumeroSerieHasta
        {
            get { return numeroSerieHasta; }
            set { numeroSerieHasta = value; }
        }
    }
}
