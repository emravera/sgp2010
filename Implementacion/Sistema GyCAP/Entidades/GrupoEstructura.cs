using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class GrupoEstructura
    {
        private int codigoGrupo;

        public int CodigoGrupo
        {
            get { return codigoGrupo; }
            set { codigoGrupo = value; }
        }
        private int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private int codigoEstructura;

        public int CodigoEstructura
        {
            get { return codigoEstructura; }
            set { codigoEstructura = value; }
        }
        private int codigoPadre;

        public int CodigoPadre
        {
            get { return codigoPadre; }
            set { codigoPadre = value; }
        }
        private string nombreGrupo;

        public string NombreGrupo
        {
            get { return nombreGrupo; }
            set { nombreGrupo = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private int concreto;

        public int Concreto
        {
            get { return concreto; }
            set { concreto = value; }
        }
    }
}
