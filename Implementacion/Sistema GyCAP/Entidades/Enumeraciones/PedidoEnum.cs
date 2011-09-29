using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class PedidoEnum
    {
        public enum EstadoDetallePedidoEnum { Pendiente = 1, EnCurso = 2, Cancelado = 3, EntregaStock = 4, Finalizado = 5, Entregado = 6 };
    }
}
