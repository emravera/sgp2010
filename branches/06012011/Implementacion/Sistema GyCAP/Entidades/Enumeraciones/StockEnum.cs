using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class StockEnum
    {
        public enum TipoFecha { FechaPrevista, FechaReal };
        
        public enum ContenidoUbicacion { Cocina = 1, MateriaPrima = 5, Repuesto = 6, Intermedio = 7, Parte = 8 };

        public enum TipoUbicacion { Vista = 1, Proveedor = 2, Interna = 3, Logística = 4 };

        public enum EstadoMovimientoStock { Planificado = 1, Finalizado = 2, Cancelado = 3, EnProceso = 4 };

        public enum CodigoMovimiento { Manual, Pedido, DetallePedido, Mantenimiento, OrdenProduccion, OrdenTrabajo };

    }
}
