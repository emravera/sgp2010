using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Empleado : Usuario
    {
        private long codigo;
        private string legajo;
        private String nombre;
        private String apellido;
        private DateTime? fechaNacimiento;
        private String telefono;
        private DateTime fechaAlta;
        private DateTime fechaBaja;

        private EstadoEmpleado estado;
        private SectorTrabajo sector;
        
        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Legajo
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
        
        public DateTime? FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }
        
        public String Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }

        public DateTime FechaBaja
        {
            get { return fechaBaja; }
            set { fechaBaja = value; }
        }

        public EstadoEmpleado Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
        public SectorTrabajo Sector
        {
            get { return sector; }
            set { sector = value; }
        }
        
    }
}
