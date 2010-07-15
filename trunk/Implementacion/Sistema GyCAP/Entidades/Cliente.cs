using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Cliente
    {
        private int codigo;
        private string razonSocial;
        private string telefono;
        private DateTime fechaAlta;
        private DateTime fechaBaja;
        private string motivoBaja;

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
       

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        
        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }
        
        public DateTime FechaBaja
        {
            get { return fechaBaja; }
            set { fechaBaja = value; }
        }
       
        public string MotivoBaja
        {
            get { return motivoBaja; }
            set { motivoBaja = value; }
        }


    }
}
