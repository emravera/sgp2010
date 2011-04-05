using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Domicilio
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        Entidades.Localidad localidad;

        public Entidades.Localidad Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }
        Entidades.Proveedor proveedor;

        public Entidades.Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        string calle;

        public string Calle
        {
            get { return calle; }
            set { calle = value; }
        }
        int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        int piso;

        public int Piso
        {
            get { return piso; }
            set { piso = value; }
        }
        string departamento;

        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        
    }
}
