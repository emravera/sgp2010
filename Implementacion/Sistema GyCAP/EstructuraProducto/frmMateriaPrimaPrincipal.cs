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
        private static frmMateriaPrimaPrincipal _frmMateriaPrima = null;
        private Data.dsEstructuraProducto dsMateriaPrima = new GyCAP.Data.dsEstructuraProducto();
        private enum estadoUI { inicio, agregar, modificar };
        private DataView dvListaBusqueda, dvCbUnidadMedida, dvCbTipoUnMedida, dvCbUbicacionStock;
        private static estadoUI estadoActual;

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
            dgvLista.Columns["MPPR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MPPR_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Habilito resize
            dgvLista.Columns["MPPR_CODIGO"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MP_CODIGO"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MPPR_CANTIDAD"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["UMED_CODIGO"].Resizable = DataGridViewTriState.True;

            //Alineo la columna
            dgvLista.Columns["MPPR_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Oculto el Codigo
            dgvLista.Columns[0].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MPPR_CODIGO"].DataPropertyName = "MPPR_CODIGO";
            dgvLista.Columns["MP_CODIGO"].DataPropertyName = "MP_CODIGO";
            dgvLista.Columns["MPPR_CANTIDAD"].DataPropertyName = "MPPR_CANTIDAD";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";

            //Seteo el Dataview de la Lista
            dvListaBusqueda = new DataView(dsMateriaPrima.MATERIAS_PRIMAS);
            dgvLista.DataSource = dvListaBusqueda;

            //LLeno cada uno de los datatable con los datos
            
            //Lleno el DataTable con las Unidades de Medida
            BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.UNIDADES_MEDIDA);

            //Lleno el DataTable con los Tipos de Unidades de Medida
            BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.TIPOS_UNIDADES_MEDIDA);

            //Lleno el Datatable de las Ubicaciones de Stock (El valor 5 esta en la tabla de ubicaciones que tienen MP)
            BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsMateriaPrima.UBICACIONES_STOCK, Convert.ToInt16(5));

            //CARGA DE COMBOS
            //Combo de Datos
            //Creamos el Dataview y se lo asignamos al combo
            dvCbUnidadMedida = new DataView(dsMateriaPrima.UNIDADES_MEDIDA);
            cbUnidadMedida.DataSource = dvCbUnidadMedida;
            cbUnidadMedida.SetDatos(dvCbUnidadMedida, "cli_codigo", "cli_razonsocial", "-Seleccionar-", false);



            //Seteo la propiedad del Incremento de la cantidad
            numCantidad.Increment=Convert.ToDecimal(0.01);
           
            
        }

        public static frmMateriaPrimaPrincipal Instancia
        {
            get
            {
                if (_frmMateriaPrima == null || _frmMateriaPrima.IsDisposed)
                {
                    _frmMateriaPrima = new frmMateriaPrimaPrincipal();
                }
                else
                {
                    _frmMateriaPrima.BringToFront();
                }
                return _frmMateriaPrima;
            }
            set
            {
                _frmMateriaPrima = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void frmMateriaPrimaPrincipal_Activated(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.agregar);
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }
        private void SetInterface(estadoUI estado)
        {
            //Completar
        }
               
        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }
        
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        
    }
        
        
}
