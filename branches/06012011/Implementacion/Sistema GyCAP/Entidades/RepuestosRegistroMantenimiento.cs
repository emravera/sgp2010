using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class RepuestosRegistroMantenimiento
    {
        private long codigo;
        private RegistrosMantenimientos registroMantenimiento;
        private Repuesto repuesto;
        private decimal cantidad;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public RegistrosMantenimientos RegistroMantenimiento
        {
            get { return registroMantenimiento; }
            set { registroMantenimiento = value; }
        }
        
        public Repuesto Repuesto
        {
            get { return repuesto; }
            set { repuesto = value; }
        }
        
        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

    }
}
