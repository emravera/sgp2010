using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Cocina
    {
        private String codigo;
        private Color color;
        private Designacion designacion;
        private EstadoCocina estado;
        private Estructura estructura;
        private Marca marca;
        private ModeloCocina modelo;
        private Terminacion terminacionHorno;

        public String Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        
        public Designacion Designacion
        {
            get { return designacion; }
            set { designacion = value; }
        }
        
        public EstadoCocina Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
        public Estructura Estructura
        {
            get { return estructura; }
            set { estructura = value; }
        }
        
        public Marca Marca
        {
            get { return marca; }
            set { marca = value; }
        }
        
        public ModeloCocina Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }
 
        public Terminacion TerminacionHorno
        {
            get { return terminacionHorno; }
            set { terminacionHorno = value; }
            
        }
    }
}
