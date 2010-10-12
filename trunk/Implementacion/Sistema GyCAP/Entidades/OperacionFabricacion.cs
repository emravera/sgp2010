using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class OperacionFabricacion
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        string codificacion;

        public string Codificacion
        {
            get { return codificacion; }
            set { codificacion = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        decimal horasRequeridas;

        public decimal HorasRequeridas
        {
            get { return horasRequeridas; }
            set { horasRequeridas = value; }
        }

        private UbicacionStock ubicacionStockOrigen;

        public UbicacionStock UbicacionStockOrigen
        {
            get { return ubicacionStockOrigen; }
            set { ubicacionStockOrigen = value; }
        }
        private UbicacionStock ubicacionStockDestino;

        public UbicacionStock UbicacionStockDestino
        {
            get { return ubicacionStockDestino; }
            set { ubicacionStockDestino = value; }
        }

        
    }
}
