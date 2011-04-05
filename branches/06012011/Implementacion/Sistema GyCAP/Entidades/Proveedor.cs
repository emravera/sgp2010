using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Proveedor
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        Entidades.SectorTrabajo sector;

        public Entidades.SectorTrabajo Sector
        {
            get { return sector; }
            set { sector = value; }
        }
        string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }
        string telPrincipal;

        public string TelPrincipal
        {
            get { return telPrincipal; }
            set { telPrincipal = value; }
        }
        string telSecundario;

        public string TelSecundario
        {
            get { return telSecundario; }
            set { telSecundario = value; }
        }


    }
}
