using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MenuUsuario
    {
        private int codigo;
        private Menu menu;
        private Usuario usuario;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public Menu Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }


    }
}
