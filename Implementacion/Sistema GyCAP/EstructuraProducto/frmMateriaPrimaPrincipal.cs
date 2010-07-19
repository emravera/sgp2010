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
    public partial class frmMateriaPrimaPrincipal : Form
    {
        private static frmMateriaPrimaPrincipal _frmMateriaPrimaPrincipal = null;
        private Data.dsMateriaPrima dsMateriaPrimaPrincipal = new GyCAP.Data.dsMateriaPrima();

        public frmMateriaPrimaPrincipal()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MPPR_CODIGO", "Código");
            dgvLista.Columns.Add("MP_CODIGO", "Materia Prima");
            dgvLista.Columns.Add("MPPR_CANTIDAD", "Cantidad");
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad de Medida");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MPPR_CODIGO"].DataPropertyName = "MPPR_CODIGO";
            dgvLista.Columns["MP_CODIGO"].DataPropertyName = "MP_CODIGO";
            dgvLista.Columns["MPPR_CANTIDAD"].DataPropertyName = "MPPR_CANTIDAD";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";

            /*/Seteo el arranque de la pantalla
            //Cambio la posicion y escondo la que agrega
            gbLista.Location.X = 3;
            gbLista.Location.Y = 3;
            gbAgregar.Visible = false;
            //Cambio los tamaños de la lista y el groupbox
            gbLista.Size.Height = 300;
            dgvLista.Size.Height = 280;*/

        }

        public static frmMateriaPrimaPrincipal Instancia
        {
            get
            {
                if (_frmMateriaPrimaPrincipal == null || _frmMateriaPrimaPrincipal.IsDisposed)
                {
                    _frmMateriaPrimaPrincipal = new frmMateriaPrimaPrincipal();
                }
                else
                {
                    _frmMateriaPrimaPrincipal.BringToFront();
                }
                return _frmMateriaPrimaPrincipal;
            }
            set
            {
                _frmMateriaPrimaPrincipal = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        

       
    }
}
