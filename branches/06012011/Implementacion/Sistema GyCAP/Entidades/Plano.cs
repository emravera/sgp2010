using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Plano
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string numero;

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        int habilitado;

        public int Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }
        DateTime fechaHabilitado;

        public DateTime FechaHabilitado
        {
            get { return fechaHabilitado; }
            set { fechaHabilitado = value; }
        }
        DateTime fechaDeshabilitado;

        public DateTime FechaDeshabilitado
        {
            get { return fechaDeshabilitado; }
            set { fechaDeshabilitado = value; }
        }
        DateTime fechaModificacion;

        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }
        string letraCambio;

        public string LetraCambio
        {
            get { return letraCambio; }
            set { letraCambio = value; }
        }

        
    }
}
