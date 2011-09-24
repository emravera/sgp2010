using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.ArbolOrdenesTrabajo
{
    public class ArbolProduccion
    {
        private OrdenProduccion ordenProduccion;
        private int codigoOrden = 0;

        public OrdenProduccion OrdenProduccion
        {
            get { return ordenProduccion; }
            set { ordenProduccion = value; }
        }
        private IList<NodoOrdenTrabajo> ordenesTrabajo;

        public IList<NodoOrdenTrabajo> OrdenesTrabajo
        {
            get { return ordenesTrabajo; }
            set { ordenesTrabajo = value; }
        }

        public int GetNextCodigoOrden()
        {
            codigoOrden--;
            return codigoOrden;
        }
    }
}
