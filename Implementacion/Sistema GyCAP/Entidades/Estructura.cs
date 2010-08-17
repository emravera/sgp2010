using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Estructura
    {
        private int codigoEstructura;
        private string nombre;
        private int codigoCocina;
        private int codigoPlano;
        private string descripcion;
        private int activo; //0-NO , 1-SI
        private DateTime fechaAlta;
        private DateTime fechaModificacion;
        private int codigoEmpleado;
        private decimal costo;

        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public int CodigoEstructura
        {
            get { return codigoEstructura; }
            set { codigoEstructura = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        public int CodigoCocina
        {
            get { return codigoCocina; }
            set { codigoCocina = value; }
        }
        
        public int CodigoPlano
        {
            get { return codigoPlano; }
            set { codigoPlano = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        
        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }
        
        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }        

        public int CodigoEmpleado
        {
            get { return codigoEmpleado; }
            set { codigoEmpleado = value; }
        }
    }
}
