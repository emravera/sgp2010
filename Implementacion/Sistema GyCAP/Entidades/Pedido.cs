﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Pedido
    {
        private int codigo;
        private string numero;  
        private Cliente cliente;
        private EstadoPedido estadoPedido;
        private DateTime fechaEntregaReal;
        private DateTime fechaAlta;
        private string observaciones;

        public int Codigo
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

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
    }
}
