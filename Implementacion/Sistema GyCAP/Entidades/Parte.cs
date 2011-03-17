using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Parte
    {
        int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        Plano plano;

        public Plano Plano
        {
            get { return plano; }
            set { plano = value; }
        }
        decimal costo;

        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }
        int costoFijo;

        public int CostoFijo
        {
            get { return costoFijo; }
            set { costoFijo = value; }
        }
        EstadoParte estado;

        public EstadoParte Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        TipoParte tipo;

        public TipoParte Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        Terminacion terminacion;

        public Terminacion Terminacion
        {
            get { return terminacion; }
            set { terminacion = value; }
        }
        HojaRuta hojaRuta;

        public HojaRuta HojaRuta
        {
            get { return hojaRuta; }
            set { hojaRuta = value; }
        }
    }
}
