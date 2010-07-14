using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmDesignacion : Form
    {
        private static frmDesignacion _frmDesignacion = null;
        private Data.dsDesignacion dsDesignacion = new GyCAP.Data.dsDesignacion();
        private DataView dvListaDesignacion, dvComboDesignacion;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmDesignacion()
        {
            InitializeComponent();


            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("DESIG_CODIGO", "Código");
            dgvLista.Columns.Add("MCA_CODIGO", "Marca");
            dgvLista.Columns.Add("DESIG_NOMBRE", "Nombre");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["DESIG_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["MCA_CODIGO"].DataPropertyName = "TUMED_CODIGO";
            dgvLista.Columns["DESIG_NOMBRE"].DataPropertyName = "UMED_NOMBRE";
            
        }
    }
}
