using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public abstract class Usuario
    {
        private int codigo;
        
        private String usuario;
        private String password;

        public String Usuario1
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
