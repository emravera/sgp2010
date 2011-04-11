using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Averia
    {
        private long codigo;
        private string descripcion;
        private Maquina maquina;
        private NivelCriticidad nivel;
        private DateTime fechaAlta;
        private long codUsuario;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
        public Maquina Maquina
        {
            get { return maquina; }
            set { maquina = value; }
        }

        public NivelCriticidad Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }

        public long CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }

    }
}
