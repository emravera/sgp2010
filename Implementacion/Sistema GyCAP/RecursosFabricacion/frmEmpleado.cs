using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmEmpleado : Form
    {
        private static frmEmpleado _frmEmpleado = null;
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado(); 
        private DataView dvEmpleado, dvComboEstadoEmpleado, dvListaSectores;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmEmpleado()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Agregamos las columnas
            dgvLista.Columns.Add("E_CODIGO", "Código");
            dgvLista.Columns.Add("E_LEGAJO", "Legajo");
            dgvLista.Columns.Add("E_APELLIDO", "Apellido");
            dgvLista.Columns.Add("E_NOMBRE", "Nombre");
            dgvLista.Columns.Add("SEC_CODIGO", "Sector");
            dgvLista.Columns.Add("EE_CODIGO", "Estado");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvLista.Columns["E_LEGAJO"].DataPropertyName = "E_LEGAJO";
            dgvLista.Columns["E_APELLIDO"].DataPropertyName = "E_APELLIDO";
            dgvLista.Columns["E_NOMBRE"].DataPropertyName = "E_NOMBRE";
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["EE_CODIGO"].DataPropertyName = "EE_CODIGO";

            //Oculta la columna que contiene los encabezados
            dgvLista.RowHeadersVisible = false;

            //Setemaos las columnas
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvLista.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Creamos el dataview y lo asignamos a la grilla
            //dvEmpleado = new DataView(dsEmpleado.);
            dgvLista.DataSource = dvEmpleado;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            //txtDescripcion.MaxLength = 250;
            //txtNombre.MaxLength = 80;

            //Seteamos el estado de la interfaz
            //SetInterface(estadoUI.inicio);
        }
    }
}
