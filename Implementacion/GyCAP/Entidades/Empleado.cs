using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Empleado : Usuario
    {
        private int legajo;
        private String nombre;
        private String apellido;
        private DateTime fechaNacimiento;
        private String telefono;
        private EstadoEmpleado estado;
        private Sector sector;
        
        public int Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        
        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }
        
        public String Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        
        public EstadoEmpleado Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
        public Sector Sector
        {
            get { return sector; }
            set { sector = value; }
        }
        
    }
}
