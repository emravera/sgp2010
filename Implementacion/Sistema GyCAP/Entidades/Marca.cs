using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Marca
    {
        private int codigo;
        private String nombre;
        private Cliente cliente;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
 
        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
    }
}
