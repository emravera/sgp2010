using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Pedido
    {
        private long codigo;
        private string numero;  
        private Cliente cliente;
        private EstadoPedido estadoPedido;
        private DateTime fechaEntregaPrevista;
        private DateTime fechaEntregaReal;
        private DateTime fechaAlta;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public EstadoPedido EstadoPedido
        {
            get { return estadoPedido; }
            set { estadoPedido = value; }
        }

        public DateTime FechaEntregaPrevista
        {
            get { return fechaEntregaPrevista; }
            set { fechaEntregaPrevista = value; }
        }

        public DateTime FechaEntregaReal
        {
            get { return fechaEntregaReal; }
            set { fechaEntregaReal = value; }
        }

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }


    }
}
