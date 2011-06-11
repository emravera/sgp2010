using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class UbicacionStock
    {
        public UbicacionStock() { }

        public UbicacionStock(int numeroUbicacionStock)
        {
            numero = numeroUbicacionStock;
        }
        
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
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private string ubicacionFisica;

        public string UbicacionFisica
        {
            get { return ubicacionFisica; }
            set { ubicacionFisica = value; }
        }
        private decimal cantidadReal;

        public decimal CantidadReal
        {
            get { return cantidadReal; }
            set { cantidadReal = value; }
        }
        private decimal cantidadVirtual;

        public decimal CantidadVirtual
        {
            get { return cantidadVirtual; }
            set { cantidadVirtual = value; }
        }
        private UnidadMedida unidadMedida;

        public UnidadMedida UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        private UbicacionStock ubicacionPadre;

        public UbicacionStock UbicacionPadre
        {
            get { return ubicacionPadre; }
            set { ubicacionPadre = value; }
        }
        private int activo;

        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        private TipoUbicacionStock tipoUbicacion;

        public TipoUbicacionStock TipoUbicacion
        {
            get { return tipoUbicacion; }
            set { tipoUbicacion = value; }
        }

        private ContenidoUbicacionStock contenido;

        public ContenidoUbicacionStock Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }
        
    }
}
