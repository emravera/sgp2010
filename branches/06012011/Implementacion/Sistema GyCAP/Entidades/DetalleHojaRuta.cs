using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetalleHojaRuta
    {
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private CentroTrabajo centroTrabajo;

        public CentroTrabajo CentroTrabajo
        {
            get { return centroTrabajo; }
            set { centroTrabajo = value; }
        }
        private int secuencia;

        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }
        private OperacionFabricacion operacion;

        public OperacionFabricacion Operacion
        {
            get { return operacion; }
            set { operacion = value; }
        }
        private UbicacionStock stockOrigen;

        public UbicacionStock StockOrigen
        {
            get { return stockOrigen; }
            set { stockOrigen = value; }
        }
        private UbicacionStock stockDestino;

        public UbicacionStock StockDestino
        {
            get { return stockDestino; }
            set { stockDestino = value; }
        }
    }
}
