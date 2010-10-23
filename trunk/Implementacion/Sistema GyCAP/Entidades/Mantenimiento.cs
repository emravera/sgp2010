using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Mantenimiento
    {
        private long codigo;
        private TipoMantenimiento tipo;
        private string descripcion;
        private CapacidadEmpleado capacidadEmpleado;
        private string observacion;
        private string requierePararPlanta;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public TipoMantenimiento Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public CapacidadEmpleado CapacidadEmpleado
        {
            get { return capacidadEmpleado; }
            set { capacidadEmpleado = value; }
        }
        
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        public string RequierePararPlanta
        {
            get { return requierePararPlanta; }
            set { requierePararPlanta = value; }
        }

    }
}
