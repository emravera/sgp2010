using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class CentroTrabajo
    {
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private SectorTrabajo sector;

        public SectorTrabajo Sector
        {
            get { return sector; }
            set { sector = value; }
        }
        private TipoCentroTrabajo tipo;

        public TipoCentroTrabajo Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private decimal horasTrabajoNormal;

        public decimal HorasTrabajoNormal
        {
            get { return horasTrabajoNormal; }
            set { horasTrabajoNormal = value; }
        }
        private decimal horasTrabajoExtendido;

        public decimal HorasTrabajoExtendido
        {
            get { return horasTrabajoExtendido; }
            set { horasTrabajoExtendido = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private decimal capacidadCiclo;

        public decimal CapacidadCiclo
        {
            get { return capacidadCiclo; }
            set { capacidadCiclo = value; }
        }
        private decimal capacidadUnidadHora;

        public decimal CapacidadUnidadHora
        {
            get { return capacidadUnidadHora; }
            set { capacidadUnidadHora = value; }
        }
        
        private decimal horasCiclo;

        public decimal HorasCiclo
        {
            get { return horasCiclo; }
            set { horasCiclo = value; }
        }
        private int activo;

        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        private decimal tiempoAntes;

        public decimal TiempoAntes
        {
            get { return tiempoAntes; }
            set { tiempoAntes = value; }
        }
        private decimal tiempoDespues;

        public decimal TiempoDespues
        {
            get { return tiempoDespues; }
            set { tiempoDespues = value; }
        }
        private decimal eficiencia;

        public decimal Eficiencia
        {
            get { return eficiencia; }
            set { eficiencia = value; }
        }
        private decimal costoHora;

        public decimal CostoHora
        {
            get { return costoHora; }
            set { costoHora = value; }
        }
        private decimal costoCiclo;

        public decimal CostoCiclo
        {
            get { return costoCiclo; }
            set { costoCiclo = value; }
        }

        private IList<TurnoTrabajo> turnosTrabajo = new List<TurnoTrabajo>();

        public IList<TurnoTrabajo> TurnosTrabajo
        {
            get { return turnosTrabajo; }
            set { turnosTrabajo = value; }
        }
    }
}
