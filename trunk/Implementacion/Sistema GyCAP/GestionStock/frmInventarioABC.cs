using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionStock
{
    public partial class frmInventarioABC : Form
    {
        private static frmInventarioABC _frmInventarioABC = null;
        private enum estadoUI { inicio, CargaDetalle };
        private DataView dvComboAño, dvComboAñoHistorico, dvComboCocinas, dvListaModelos, dvListaMP;
        private static estadoUI estadoActual;
        private Data.dsInventarioABC dsInventarioABC = new GyCAP.Data.dsInventarioABC();

        public frmInventarioABC()
        {
            InitializeComponent();

            //Ponemos que las columnas no se autogeneren
            dgvModelos.AutoGenerateColumns = false;
            dgvMP.AutoGenerateColumns = false;

            //*********************************CREACION GRILLAS *********************************
            //Grilla de Modelos
            //Agregamos la columnas
            dgvModelos.Columns.Add("CODIGO_MODELO_PRODUCIDO", "Código");
            dgvModelos.Columns.Add("CODIGO_MODELO", "Modelo");
            dgvModelos.Columns.Add("MODELO_PORCENTAJE", "Porcentaje");
            dgvModelos.Columns.Add("MODELO_CANTIDAD", "Cantidad");

            //Seteamos el modo de tamaño de las columnas
            dgvModelos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvModelos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvModelos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvModelos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvModelos.Columns["CODIGO_MODELO_PRODUCIDO"].DataPropertyName = "CODIGO_MODELO_PRODUCIDO";
            dgvModelos.Columns["CODIGO_MODELO"].DataPropertyName = "CODIGO_MODELO";
            dgvModelos.Columns["MODELO_PORCENTAJE"].DataPropertyName = "MODELO_PORCENTAJE";
            dgvModelos.Columns["MODELO_CANTIDAD"].DataPropertyName = "MODELO_CANTIDAD";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaModelos = new DataView(dsInventarioABC.MODELOS_PRODUCIDOS);
            dgvModelos.DataSource = dvListaModelos;

            //Grilla de Materias Primas ABC
            //Agregamos la columnas
            dgvMP.Columns.Add("CODIGO_MATERIA_PRIMA_ABC", "Código");
            dgvMP.Columns.Add("CODIGO_MATERIA_PRIMA", "Materia Prima");
            dgvMP.Columns.Add("CANTIDAD_ANUAL", "Cant.Anual");
            dgvMP.Columns.Add("PRECIO_UNIDAD", "Precio");
            dgvMP.Columns.Add("PORCENTAJE_INVERSION", "% Inversion");
            dgvMP.Columns.Add("CATEGORIA_ABC", "Categoria");

            //Seteamos el modo de tamaño de las columnas
            dgvMP.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvMP.Columns["CODIGO_MATERIA_PRIMA_ABC"].DataPropertyName = "CODIGO_MATERIA_PRIMA_ABC";
            dgvMP.Columns["CODIGO_MATERIA_PRIMA"].DataPropertyName = "CODIGO_MATERIA_PRIMA";
            dgvMP.Columns["CANTIDAD_ANUAL"].DataPropertyName = "CANTIDAD_ANUAL";
            dgvMP.Columns["PRECIO_UNIDAD"].DataPropertyName = "PRECIO_UNIDAD";
            dgvMP.Columns["PORCENTAJE_INVERSION"].DataPropertyName = "PORCENTAJE_INVERSION";
            dgvMP.Columns["CATEGORIA_ABC"].DataPropertyName = "CATEGORIA_ABC";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaMP = new DataView(dsInventarioABC.MATERIAS_PRIMAS_ABC);
            dgvMP.DataSource = dvListaMP;


            //Traemos los datos para llenar Datatables 
            //Traigo los datos de MP
            BLL.MateriaPrimaBLL.ObtenerMP(dsInventarioABC.MATERIAS_PRIMAS);

            //Traigo los datos de Modelos Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsInventarioABC.COCINAS);

            //Planes Anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsInventarioABC.PLANES_ANUALES);


            //CARGA DE COMBOS
            //Cargo el combo de datos del plan anual
            dvComboAño = new DataView(dsInventarioABC.PLANES_ANUALES);
            cbAñoInventario.SetDatos(dvComboAño, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el combo del plan anual historico
            dvComboAñoHistorico = new DataView(dsInventarioABC.PLANES_ANUALES);
            cbAñoHistorico.SetDatos(dvComboAñoHistorico, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el Combo de Cocinas
            dvComboCocinas = new DataView(dsInventarioABC.COCINAS);
            cbCocinas.SetDatos(dvComboCocinas, "coc_codigo", "coc_codigo_producto", "Seleccione", false);
            
            //Seteamos la interface
            SetInterface(estadoUI.inicio);
        }


        #region Servicios
        
        public static frmInventarioABC Instancia
        {
            get
            {
                if (_frmInventarioABC == null || _frmInventarioABC.IsDisposed)
                {
                    _frmInventarioABC = new frmInventarioABC();
                }
                else
                {
                    _frmInventarioABC.BringToFront();
                }
                return _frmInventarioABC;
            }
            set
            {
                _frmInventarioABC = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    gbDatosPrincipales.Visible = true;
                    gbDatosCocinas.Visible = false;
                    gbMateriasPrimas.Visible = false;

                    rbNuevo.Checked = true;

                    cbAñoHistorico.Visible = false;
                    break;
                case estadoUI.CargaDetalle:
                    break;

            }
        }
        private void rbNuevo_CheckedChanged(object sender, EventArgs e)
        {
            cbAñoHistorico.Visible = false;
            cbAñoHistorico.SetSelectedIndex(-1);
        }

        private void rbHistorico_CheckedChanged(object sender, EventArgs e)
        {
            cbAñoHistorico.Visible = true;
        }

        #endregion

       



    }
}
