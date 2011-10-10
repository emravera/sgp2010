using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class CierreParcialOrdenTrabajo
    {
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private OrdenTrabajo ordenTrabajo;

        public OrdenTrabajo OrdenTrabajo
        {
            get { return ordenTrabajo; }
            set { ordenTrabajo = value; }
        }
        private Empleado empleado;

        public Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        private Maquina maquina;

        public Maquina Maquina
        {
            get { return maquina; }
            set { maquina = value; }
        }
        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private DateTime? fecha;

        public DateTime? Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        private int operacionesFallidas;

        public int OperacionesFallidas
        {
            get { return operacionesFallidas; }
            set { operacionesFallidas = value; }
        }
    }
}
