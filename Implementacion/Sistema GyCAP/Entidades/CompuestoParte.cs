using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;

namespace GyCAP.Entidades
{
    public class CompuestoParte
    {
        int codigo;
        
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        Parte parte;

        public Parte Parte
        {
            get { return parte; }
            set { parte = value; }
        }
        MateriaPrima materiaPrima;

        public MateriaPrima MateriaPrima
        {
            get { return materiaPrima; }
            set { materiaPrima = value; }
        }
        decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        UnidadMedida unidadMedida;

        public UnidadMedida UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }

        private Estructura estructura;

        public Estructura Estructura
        {
            get { return estructura; }
            set { estructura = value; }
        }

        private CompuestoParte compuestoPadre;

        public CompuestoParte CompuestoPadre
        {
            get { return compuestoPadre; }
            set { compuestoPadre = value; }
        }
    }
}
