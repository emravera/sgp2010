using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class ConfiguracionesSistema
    {
        int codigo;
        int valor;
        string nombre;
        
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

        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}
