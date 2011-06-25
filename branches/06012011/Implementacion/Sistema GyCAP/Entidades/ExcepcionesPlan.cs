using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class ExcepcionesPlan
    {
        public enum TipoExcepcion { MateriaPrima, Capacidad, Fecha };

        int codigo;
        string nombre;
        TipoExcepcion tipo;
        string descripcion;
                
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

        public TipoExcepcion Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
