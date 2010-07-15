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

            //Llena el Dataset con las marcas
            BLL.MarcaBLL.ObtenerTodos(dsDesignacion);
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDesignacion = new DataView(dsDesignacion.DESIGNACIONES);
            dgvLista.DataSource = dvListaMarca;
            
            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvComboDesignacion = new DataView(dsDesignacion.MARCAS);
            cbMarcaBuscar.DataSource = dvComboMarca;
            cbMarcaBuscar.DisplayMember = "MCA_NOMBRE";
            cbClienteBuscar.ValueMember = "MCA_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMarcaBuscar.SelectedIndex = -1;
            cbMarcaBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            cbMarcaDatos.DataSource = dvComboMarca;
            cbMarcaDatos.DisplayMember = "MCA_NOMBRE";
            cbMarcaDatos.ValueMember = "MCA_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMarcaDatos.SelectedIndex = -1;
            cbMarcaDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Selecciono por defecto buscar por nombre
            rbNombre.Checked = true;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
           
        }
    }
}
