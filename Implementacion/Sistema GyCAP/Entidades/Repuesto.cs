using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Repuesto
    {
        private int codigo;
        private string nombre;
        private string descripcion;
        private decimal costo;
        private decimal cantidadStock;
        private TipoRepuesto tipo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
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

        public decimal CantidadStock
        {
            get { return cantidadStock; }
            set { cantidadStock = value; }
        }

        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public TipoRepuesto Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

    }
}
