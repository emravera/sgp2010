using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class StockEnum
    {
        public enum TipoFecha { FechaPrevista, FechaReal };
        public enum ContenidoUbicacion { Cocina = 1, Conjunto = 2, Subconjunto = 3, Pieza = 4, MateriaPrima = 5, Repuesto = 6, Intermedio = 7 };
    }
}
