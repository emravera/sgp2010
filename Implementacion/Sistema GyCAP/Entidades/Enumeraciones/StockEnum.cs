using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class StockEnum
    {
        public enum TipoFecha { FechaPrevista, FechaReal };

        // Enumeracion Codigo de Movimiento
        public enum CodigoMovimiento { Manual, Pedido }; 

        public enum ContenidoUbicacion { Cocina = 1, Conjunto = 2, Subconjunto = 3, Pieza = 4, MateriaPrima = 5, Repuesto = 6, Intermedio = 7 };

        public enum TipoUbicacion { Vista = 1, Proveedor = 2, Interna = 3, Logística = 4 };

        public enum EstadoMovimientoStock { Planificado = 1, Finalizado = 2, Cancelado = 3 };

    }
}
