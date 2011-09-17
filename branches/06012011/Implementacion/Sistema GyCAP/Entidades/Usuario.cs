using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Usuario
    {
        private int codigo;
        private String login;
        private String password;
        private EstadoUsuario estado;
        private String nombre;
        private String mail;
        private Rol rol;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public String Login
        {
            get { return login; }
            set { login = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public EstadoUsuario Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public Rol Rol
        {
            get { return rol; }
            set { rol = value; }
        }
       
    }
}
