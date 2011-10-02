using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Enumeraciones
{
    public class EntidadEnum
    {
        public enum TipoEntidadEnum 
        { 
            Manual = 1, 
            Pedido = 2,
            DetallePedido = 3, 
            OrdenProduccion = 4, 
            OrdenTrabajo = 5, 
            Mantenimiento = 6, 
            UbicacionStock = 7 
        };

        public static string GetFriendlyName(TipoEntidadEnum tipo)
        {
            switch (tipo)
            {
                case TipoEntidadEnum.Manual:
                    return "Manual";
                    break;
                case TipoEntidadEnum.Pedido:
                    return "Pedido";
                    break;
                case TipoEntidadEnum.DetallePedido:
                    return "Detalle de Pedido";
                    break;
                case TipoEntidadEnum.OrdenProduccion:
                    return "Orden de Producción";
                    break;
                case TipoEntidadEnum.OrdenTrabajo:
                    return "Orden de Trabajo";
                    break;
                case TipoEntidadEnum.Mantenimiento:
                    return "Mantenimiento";
                    break;
                case TipoEntidadEnum.UbicacionStock:
                    return "Ubicación de Stock";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        public static string GetFriendlyName(int tipo)
        {
            switch (tipo)
            {
                case (int)TipoEntidadEnum.Manual:
                    return "Manual";
                    break;
                case (int)TipoEntidadEnum.Pedido:
                    return "Pedido";
                    break;
                case (int)TipoEntidadEnum.DetallePedido:
                    return "Detalle de Pedido";
                    break;
                case (int)TipoEntidadEnum.OrdenProduccion:
                    return "Orden de Producción";
                    break;
                case (int)TipoEntidadEnum.OrdenTrabajo:
                    return "Orden de Trabajo";
                    break;
                case (int)TipoEntidadEnum.Mantenimiento:
                    return "Mantenimiento";
                    break;
                case (int)TipoEntidadEnum.UbicacionStock:
                    return "Ubicación de Stock";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
    }
}
