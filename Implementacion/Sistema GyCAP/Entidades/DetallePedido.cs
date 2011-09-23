using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePedido
    {
        private long codigo;
        private Pedido pedido;
        private EstadoDetallePedido estado;
        private Cocina cocina;
        private int cantidad;
        private DateTime fechaCancelacion;
        private DateTime fechaEntregaPrevista;
        private DateTime fechaEntregaReal;
        private string codigoNemonico;
        
        public string CodigoNemonico
        {
            get { return CodigoNemonico; }
            set { CodigoNemonico = value; }
        }

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public Pedido Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        public EstadoDetallePedido Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Cocina Cocina
        {
            get { return cocina; }
            set { cocina = value; }
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

        public DateTime FechaEntregaReal
        {
            get { return fechaEntregaReal; }
            set { fechaEntregaReal = value; }
        }

        public DateTime FechaEntregaPrevista
        {
            get { return fechaEntregaPrevista; }
            set { fechaEntregaPrevista = value; }
        }
    }
}
