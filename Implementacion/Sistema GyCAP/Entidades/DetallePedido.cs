using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePedido
    {
        private long codigo;
        private Pedido codigoPedido;
        private EstadoDetallePedido estado;
        private Cocina codigoCocina;
        private int cantidad;
        private DateTime fechaCancelacion;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public Pedido CodigoPedido
        {
            get { return codigoPedido; }
            set { codigoPedido = value; }
        }

        public EstadoDetallePedido Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Cocina CodigoCocina
        {
            get { return codigoCocina; }
            set { codigoCocina = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public DateTime FechaCancelacion
        {
            get { return fechaCancelacion; }
            set { fechaCancelacion = value; }
        }

    }
}
