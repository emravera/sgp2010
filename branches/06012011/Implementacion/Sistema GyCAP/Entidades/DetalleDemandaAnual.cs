using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetalleDemandaAnual
    {
        int codigo, cantidadmes;

        public int Cantidadmes
        {
            get { return cantidadmes; }
            set { cantidadmes = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        string mes;

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        DemandaAnual demanda;

        public DemandaAnual Demanda
        {
            get { return demanda; }
            set { demanda = value; }
        }

    }
}
