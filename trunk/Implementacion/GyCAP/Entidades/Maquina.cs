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
        private ModeloMaquina modelo;
        private FabricanteMaquina fabricante;
        private Marca marca;

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
        
        public Marca Marca
        {
            get { return marca; }
            set { marca = value; }
        }
    }
}
