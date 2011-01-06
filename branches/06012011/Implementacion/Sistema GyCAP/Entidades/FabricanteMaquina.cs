using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class FabricanteMaquina
    {
        private int codigo;
        private string razonSocial;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

    }
}
