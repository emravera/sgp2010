using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Maquina
    {
        private int codigo;
        private String nombre;
        private string numeroSerie;
        private DateTime fechaAlta;
        private ModeloMaquina modelo;
        private FabricanteMaquina fabricante;
        private string marca;
        private EstadoMaquina estado;
        private string esCritica;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set { numeroSerie = value; }
        }

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }

        public ModeloMaquina Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }
       
        public FabricanteMaquina Fabricante
        {
            get { return fabricante; }
            set { fabricante = value; }
        }

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public EstadoMaquina Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string EsCritica
        {
            get { return esCritica; }
            set { esCritica = value; }
        }
    }
}
