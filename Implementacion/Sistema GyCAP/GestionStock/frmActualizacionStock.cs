using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;


namespace GyCAP.UI.GestionStock
{
    public partial class frmActualizacionStock : Form
    {
        private static frmActualizacionStock _frmActualizacionStock = null;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, eliminar, update };
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();
        DataView dvUbicaciones, dvTipoBuscar, dvContenidoBuscar, dvStockOrigen, dvStockDestino;

        #region Inicio
        
        public frmActualizacionStock()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmActualizacionStock Instancia
        {
            get
            {
                if (_frmActualizacionStock == null || _frmActualizacionStock.IsDisposed)
                {
                    _frmActualizacionStock = new frmActualizacionStock();
                }
                else
                {
                    _frmActualizacionStock.BringToFront();
                }
                return _frmActualizacionStock;
            }
            set
            {
                _frmActualizacionStock = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        #endregion

        #region Botones menu y buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {            
            string filtro = string.Empty;
            if (!string.IsNullOrEmpty(txtNombreBuscar.Text))
            {
                filtro = "USTCK_NOMBRE LIKE '%" + txtNombreBuscar.Text + "%'";
            }

            if (cboEstadoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "USTCK_ACTIVO = " + cboEstadoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND USTCK_ACTIVO = " + cboEstadoBuscar.GetSelectedValueInt(); }
            }

            if(!string.IsNullOrEmpty(txtCodigoBuscar.Text))
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "USTCK_NOMBRE LIKE '%" + txtCodigoBuscar.Text + "%'"; }
                else { filtro += " AND USTCK_NOMBRE LIKE '%" + txtCodigoBuscar.Text + "%'"; }
            }

            if (cboTipoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "TUS_CODIGO = " + cboTipoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND TUS_CODIGO = " + cboTipoBuscar.GetSelectedValueInt(); }
            }

            if (cboContenidoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "CON_CODIGO = " + cboContenidoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND CON_CODIGO = " + cboContenidoBuscar.GetSelectedValueInt(); }
            }
            
            dvUbicaciones.RowFilter = filtro;

            if (dvUbicaciones.Count == 0) { MensajesABM.MsjBuscarNoEncontrado("Actualización de Stock", this.Text); }           

            SetInterface(estadoUI.inicio);
        }                

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) 
            { 
                SetInterface(estadoUI.update);
                txtUbicacionStockSelected.Text = (dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_nombre"]).ToString();
            }
            else { MensajesABM.MsjSinSeleccion("Actualización de Stock", MensajesABM.Generos.Femenino, this.Text); }
        }       

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { dgvLista.SelectedRows[0].Selected = false; }
            SetInterface(estadoUI.inicio);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                try
                {
                    Entidades.UbicacionStock ubicacion = new Entidades.UbicacionStock(Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]));
                    Entidades.MovimientoStock movimiento = new Entidades.MovimientoStock();
                    movimiento.Numero = 0;
                    movimiento.Codigo = "codigo";
                    movimiento.Descripcion = txtDescripcion.Text;
                    movimiento.FechaAlta = BLL.DBBLL.GetFechaServidor();
                    /*movimiento.FechaReal = DateTime.Parse(dtpFechaUpdate.GetFecha());
                    if (cboAccionUpdate.GetSelectedValueInt() == 1)
                    {
                        movimiento.Origen = new Entidades.UbicacionStock(cboUbicacionOrigenDestinoUpdate.GetSelectedValueInt());
                    }
                    else
                    {
                        movimiento.Destino = new Entidades.UbicacionStock(cboUbicacionOrigenDestinoUpdate.GetSelectedValueInt());
                    }*/

                    
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                }
            }
        }

        private void rbOrigen_CheckedChanged(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);
            if (rbOrigen.Checked)
            {
                cboStockOrigen.SetSelectedValue(numero);
                cboStockDestino.SetSelectedValue(-1);
                txtUnidadMedidaOrigen.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).UNIDADES_MEDIDARow.UMED_NOMBRE;
                txtUnidadMedidaDestino.Clear();
                nudVirtualActualOrigen.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADVIRTUAL;
                nudVirtualActualDestino.Value = 0;
                nudRealActualOrigen.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL;
                nudRealActualDestino.Value = 0;
                nudVirtualNuevaOrigen.Value = 0;
                nudVirtualNuevaDestino.Value = 0;
                cboStockOrigen.Enabled = false;
                cboStockDestino.Enabled = true;
            }
            else
            {
                cboStockOrigen.SetSelectedValue(-1);
                cboStockDestino.SetSelectedValue(numero);
                txtUnidadMedidaDestino.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).UNIDADES_MEDIDARow.UMED_NOMBRE;
                txtUnidadMedidaOrigen.Clear();
                nudVirtualActualOrigen.Value = 0;
                nudVirtualActualDestino.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADVIRTUAL;
                nudRealActualOrigen.Value = 0;
                nudRealActualDestino.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL;
                cboStockOrigen.Enabled = true;
                cboStockDestino.Enabled = false;
            }

            nudVirtualNuevaOrigen.Value = 0;
            nudVirtualNuevaDestino.Value = 0;
            nudCantidadOrigen.Value = 0;
            nudCantidadDestino.Value = 0;
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dvUbicaciones.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnActualizar.Enabled = hayDatos;
                    tcUbicacionStock.SelectedTab = tpBuscar;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtNombreBuscar.Focus();
                    break;                
                case estadoUI.update:
                    rbOrigen.Checked = false;
                    rbDestino.Checked = false;
                    txtDescripcion.Clear();
                    cboStockOrigen.SetSelectedValue(-1);
                    cboStockDestino.SetSelectedValue(-1);
                    txtUnidadMedidaDestino.Clear();
                    txtUnidadMedidaOrigen.Clear();
                    nudVirtualActualOrigen.Value = 0;
                    nudVirtualActualDestino.Value = 0;
                    nudRealActualOrigen.Value = 0;
                    nudRealActualDestino.Value = 0;
                    nudVirtualNuevaOrigen.Value = 0;
                    nudVirtualNuevaDestino.Value = 0;
                    nudCantidadOrigen.Value = 0;
                    nudCantidadDestino.Value = 0;
                    nudRealNuevaOrigen.Value = 0;
                    nudRealNuevaDestino.Value = 0;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnActualizar.Enabled = false;
                    tcUbicacionStock.SelectedTab = tpUpdate;                    
                    break;
                default:
                    break;
            }
        }

        private void Inicializar()
        {
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("USTCK_CODIGO", "Código");
            dgvLista.Columns.Add("USTCK_NOMBRE", "Nombre");
            dgvLista.Columns.Add("TUS_CODIGO", "Tipo");            
            dgvLista.Columns.Add("USTCK_CANTIDADREAL", "Cantidad real");
            dgvLista.Columns["USTCK_CANTIDADREAL"].DefaultCellStyle.Format = "N3";
            dgvLista.Columns.Add("USTCK_CANTIDADVIRTUAL", "Cantidad virtual");
            dgvLista.Columns["USTCK_CANTIDADVIRTUAL"].DefaultCellStyle.Format = "N3";
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad medida");
            dgvLista.Columns.Add("USTCK_ACTIVO", "Estado");
            dgvLista.Columns.Add("CON_CODIGO", "Contenido");
            dgvLista.Columns["USTCK_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["USTCK_CANTIDADVIRTUAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;            
            dgvLista.Columns["USTCK_CODIGO"].DataPropertyName = "USTCK_CODIGO";
            dgvLista.Columns["USTCK_NOMBRE"].DataPropertyName = "USTCK_NOMBRE";
            dgvLista.Columns["TUS_CODIGO"].DataPropertyName = "TUS_CODIGO";
            dgvLista.Columns["USTCK_CANTIDADVIRTUAL"].DataPropertyName = "USTCK_CANTIDADVIRTUAL";
            dgvLista.Columns["USTCK_CANTIDADREAL"].DataPropertyName = "USTCK_CANTIDADREAL";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["USTCK_ACTIVO"].DataPropertyName = "USTCK_ACTIVO";
            dgvLista.Columns["CON_CODIGO"].DataPropertyName = "CON_CODIGO";

            try
            {
                BLL.UnidadMedidaBLL.ObtenerTodos(dsStock.UNIDADES_MEDIDA);
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
                BLL.TipoUbicacionStockBLL.ObtenerTiposUbicacionStock(dsStock.TIPOS_UBICACIONES_STOCK);
                BLL.ContenidoUbicacionStockBLL.ObtenerContenidosUbicacionStock(dsStock.CONTENIDO_UBICACION_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvUbicaciones = new DataView(dsStock.UBICACIONES_STOCK);
            dvUbicaciones.Sort = "USTCK_NOMBRE ASC";
            dvUbicaciones.RowFilter = "USTCK_NUMERO < 0";
            dgvLista.DataSource = dvUbicaciones;            
            
            string[] nombres = { "Activo", "Inactivo" };
            int[] valores = { BLL.UbicacionStockBLL.Activo, BLL.UbicacionStockBLL.Inactivo };
            cboEstadoBuscar.SetDatos(nombres, valores, "--TODOS--", true);            
            dvTipoBuscar = new DataView(dsStock.TIPOS_UBICACIONES_STOCK);
            cboTipoBuscar.SetDatos(dvTipoBuscar, "TUS_CODIGO", "TUS_NOMBRE", "--TODOS--", true);
            dvContenidoBuscar = new DataView(dsStock.CONTENIDO_UBICACION_STOCK);            
            cboContenidoBuscar.SetDatos(dvContenidoBuscar, "CON_CODIGO", "CON_NOMBRE", "--TODOS--", true);

            dvStockOrigen = new DataView(dsStock.UBICACIONES_STOCK);
            dvStockDestino = new DataView(dsStock.UBICACIONES_STOCK);
            cboStockOrigen.SetDatos(dvStockOrigen, "USTCK_NUMERO", "USTCK_NOMBRE", "--Sin especificar--", true);
            cboStockDestino.SetDatos(dvStockDestino, "USTCK_NUMERO", "USTCK_NOMBRE", "--Sin especificar--", true);

            SetInterface(estadoUI.inicio);
        }        

        private void ActualizarCantidadPadre(decimal numeroStockPadre, decimal cantidadReal, decimal cantidadVirtual)
        {
            while (numeroStockPadre != -1)
            {
                dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADREAL += cantidadReal;
                dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADVIRTUAL += cantidadVirtual;
                try
                {
                    BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(numeroStockPadre), cantidadReal, cantidadVirtual);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado); }
                                
                //Acomodamos los valores para que no queden negativos
                if (dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADREAL < 0)
                { dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADREAL = 0; }
                if(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADVIRTUAL < 0)
                { dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADVIRTUAL = 0; }
               
                if(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).IsUSTCK_PADRENull()) { numeroStockPadre = -1; }
                else { numeroStockPadre = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_PADRE; }
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(typeof(TextBox))) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(typeof(NumericUpDown))) { (sender as NumericUpDown).Select(0, 20); }
        }        

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "USTCK_PADRE":
                        nombre = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "UMED_CODIGO":
                        nombre = dsStock.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "USTCK_ACTIVO":
                        if (Convert.ToInt32(e.Value) == BLL.UbicacionStockBLL.Activo) { nombre = "Activo"; }
                        if (Convert.ToInt32(e.Value) == BLL.UbicacionStockBLL.Inactivo) { nombre = "Inactivo"; }
                        e.Value = nombre;
                        break;
                    case "TUS_CODIGO":
                        nombre = dsStock.TIPOS_UBICACIONES_STOCK.FindByTUS_CODIGO(Convert.ToInt32(e.Value)).TUS_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CON_CODIGO":
                        nombre = dsStock.CONTENIDO_UBICACION_STOCK.FindByCON_CODIGO(Convert.ToInt32(e.Value)).CON_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion

        

                
    }
}
