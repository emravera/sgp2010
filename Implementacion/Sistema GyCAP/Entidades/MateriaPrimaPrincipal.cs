using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MateriaPrimaPrincipal
    {
        int codigo;
        Entidades.MateriaPrima materiaPrima;
        double cantidad;
        
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        

        public Entidades.MateriaPrima MateriaPrima
        {
            get { return materiaPrima; }
            set { materiaPrima = value; }
        }
        
        public double Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }


    }
}
