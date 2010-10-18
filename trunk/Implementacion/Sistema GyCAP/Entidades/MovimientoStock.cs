using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MovimientoStock
    {
        private int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private DateTime fechaAlta;

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }
        private DateTime fechaPrevista;

        public DateTime FechaPrevista
        {
            get { return fechaPrevista; }
            set { fechaPrevista = value; }
        }
        private DateTime fechaReal;

        public DateTime FechaReal
        {
            get { return fechaReal; }
            set { fechaReal = value; }
        }
        private UbicacionStock origen;

        public UbicacionStock Origen
        {
            get { return origen; }
            set { origen = value; }
        }
        private UbicacionStock destino;

        public UbicacionStock Destino
        {
            get { return destino; }
            set { destino = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private UnidadMedida unidadMedida;

        public UnidadMedida UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        private EstadoMovimientoStock estado;

        public EstadoMovimientoStock Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
