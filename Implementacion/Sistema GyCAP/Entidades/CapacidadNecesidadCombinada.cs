using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class CapacidadNecesidadCombinadaItem
    {
        private decimal tiempoOperacion;

        public decimal TiempoOperacion
        {
            get { return tiempoOperacion; }
            set { tiempoOperacion = value; }
        }
        private decimal capacidadCentroTrabajo;

        public decimal CapacidadCentroTrabajo
        {
            get { return capacidadCentroTrabajo; }
            set { capacidadCentroTrabajo = value; }
        }
        private int resultado;

        public int Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }
    }
    
    public class CapacidadNecesidadCombinada
    {
        private Parte parte;

        public Parte Parte
        {
            get { return parte; }
            set { parte = value; }
        }
        private decimal necesidad;

        public decimal Necesidad
        {
            get { return necesidad; }
            set { necesidad = value; }
        }
        private IList<CapacidadNecesidadCombinadaItem> listaOperacionesCentros;

        public IList<CapacidadNecesidadCombinadaItem> ListaOperacionesCentros
        {
            get { return listaOperacionesCentros; }
            set { listaOperacionesCentros = value; }
        }
    }
}
