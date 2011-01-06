using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DemandaAnual
    {
        int anio, codigo;
        DateTime fechaCreacion;
        string nombre;
        decimal parametroCrecimiento;

        public decimal ParametroCrecimiento
        {
            get { return parametroCrecimiento; }
            set { parametroCrecimiento = value; }
        }

       

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }




    }
}
