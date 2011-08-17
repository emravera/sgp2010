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
        private string codigo;
        private string nombre;
        private string descripcion;
        private string ubicacionFisica;
        private decimal cantidadReal;
        private decimal cantidadVirtual;
        private UnidadMedida unidadMedida;
        private UbicacionStock ubicacionPadre;
        private int activo;
        private TipoUbicacionStock tipoUbicacion;
        private ContenidoUbicacionStock contenido;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }        

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }        

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }        

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }        

        public string UbicacionFisica
        {
            get { return ubicacionFisica; }
            set { ubicacionFisica = value; }
        }        

        public decimal CantidadReal
        {
            get { return cantidadReal; }
            set { cantidadReal = value; }
        }        

        public decimal CantidadVirtual
        {
            get { return cantidadVirtual; }
            set { cantidadVirtual = value; }
        }        

        public UnidadMedida UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }        

        public UbicacionStock UbicacionPadre
        {
            get { return ubicacionPadre; }
            set { ubicacionPadre = value; }
        }        

        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }        

        public TipoUbicacionStock TipoUbicacion
        {
            get { return tipoUbicacion; }
            set { tipoUbicacion = value; }
        }        

        public ContenidoUbicacionStock Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }        
    }
}
