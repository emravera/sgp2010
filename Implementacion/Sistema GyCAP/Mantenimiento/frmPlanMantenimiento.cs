using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Mantenimiento
{
    public partial class frmPlanMantenimiento : Form
    {
        private static frmPlanMantenimiento _frmPlanMantenimiento = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvMaquina, dvEstadoMaquina, dvEstadoMaquinaBuscar,
                         dvLista;
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        public frmPlanMantenimiento()
        {
            InitializeComponent();
        }



    }
}
