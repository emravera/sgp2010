using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Entidad
    {        
        private int codigo;
        private string nombre;
        private TipoEntidad tipoEntidad;
        private object entidadExterna;

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

        public TipoEntidad TipoEntidad
        {
            get { return tipoEntidad; }
            set { tipoEntidad = value; }
        }        

        public object EntidadExterna
        {
            get { return entidadExterna; }
            set { entidadExterna = value; }
        }
    }
}
