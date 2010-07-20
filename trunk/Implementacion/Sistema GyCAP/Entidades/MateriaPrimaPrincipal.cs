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
        decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        
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
        
       

    }
}
