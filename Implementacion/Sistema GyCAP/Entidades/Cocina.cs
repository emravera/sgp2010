using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Cocina
    {
        private int codigoCocina;        
        private string codigoProducto;
        private Color color;
        private Designacion designacion;
        private EstadoCocina estado;
        private Marca marca;
        private ModeloCocina modelo;
        private Terminacion terminacionHorno;
        private int activo;        
        private int hasImage;

        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }       
        
        public int HasImage
        {
            get { return hasImage; }
            set { hasImage = value; }
        }

        public int CodigoCocina
        {
            get { return codigoCocina; }
            set { codigoCocina = value; }
        }
        
        public string CodigoProducto
        {
            get { return codigoProducto; }
            set { codigoProducto = value; }
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
