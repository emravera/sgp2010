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
    public partial class frmUbicacionStock : Form
    {
        private static frmUbicacionStock _frmUbicacionStock = null;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();
        DataView dvUbicaciones, dvUnidadMedida, dvUbicacionPadre;

        #region Inicio
        public frmUbicacionStock()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmUbicacionStock Instancia
        {
            get
            {
                if (_frmUbicacionStock == null || _frmUbicacionStock.IsDisposed)
                {
                    _frmUbicacionStock = new frmUbicacionStock();
                }
                else
                {
                    _frmUbicacionStock.BringToFront();
                }
                return _frmUbicacionStock;
            }
            set
            {
                _frmUbicacionStock = value;
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
            try
            {
                string filtro = string.Empty;
                if (!string.IsNullOrEmpty(txtNombreBuscar.Text))
                {
                    filtro = "USTCK_NOMBRE LIKE %'" + txtNombreBuscar.Text + "'%";
                }

                if (cboEstadoBuscar.GetSelectedIndex() != -1)
                {
                    if (string.IsNullOrEmpty(txtNombreBuscar.Text)) { filtro = "USTCK_ACTIVO = " + cboEstadoBuscar.GetSelectedValueInt(); }
                    else { filtro += " AND USTCK_ACTIVO = " + cboEstadoBuscar.GetSelectedValueInt(); }
                }

                dvUbicaciones.RowFilter = filtro;

                if (dvUbicaciones.Count == 0)
                {
                    MessageBox.Show("No se encontraron Ubicaciones de stock con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Ubicación de stock seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        int numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);
                        //Lo eliminamos de la DB
                        BLL.UbicacionStockBLL.Eliminar(numero);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).Delete();
                        dsStock.UBICACIONES_STOCK.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Ubicación stock - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Ubicación stock - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Ubicación de stock de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        #endregion

        #region Boton Guardar

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (txtCodigo.Text == string.Empty) { mensaje = "* Código\n"; }
            if (txtCodigo.Text == string.Empty) { mensaje = "* Nombre\n"; }
            if (cboUnidadMedida.GetSelectedIndex() == -1) { mensaje = "* Unidad de medida\n"; }
            if (cboEstado.GetSelectedIndex() == -1) { mensaje = "* Estado\n"; }

            if (mensaje == string.Empty)
            {
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();
                        ubicacion.Numero = -1;
                        ubicacion.Codigo = txtCodigo.Text;
                        ubicacion.Descripcion = txtDescripcion.Text;
                        ubicacion.UbicacionFisica = txtUbicacionFisica.Text;
                        ubicacion.CantidadReal = nudCantidadReal.Value;
                        ubicacion.CantidadVirtual = nudCantidadVirtual.Value;
                        if (cboPadre.GetSelectedIndex() != -1) { ubicacion.UbicacionPadre = new GyCAP.Entidades.UbicacionStock(cboPadre.GetSelectedValueInt()); }
                        ubicacion.UnidadMedida = new GyCAP.Entidades.UnidadMedida(cboUnidadMedida.GetSelectedValueInt());
                        ubicacion.Activo = cboEstado.GetSelectedValueInt();

                        Data.dsStock.UBICACIONES_STOCKRow row = dsStock.UBICACIONES_STOCK.NewUBICACIONES_STOCKRow();
                        row.BeginEdit();
                        row.USTCK_NUMERO = ubicacion.Numero;
                        row.USTCK_CODIGO = ubicacion.Codigo;
                        row.USTCK_NOMBRE = ubicacion.Nombre;
                        row.USTCK_DESCRIPCION = ubicacion.Descripcion;
                        row.USTCK_UBICACIONFISICA = ubicacion.UbicacionFisica;
                        if (ubicacion.UbicacionPadre == null) { row.SetUSTCK_PADRENull(); }
                        else { row.USTCK_PADRE = ubicacion.UbicacionPadre.Numero; }
                        row.UMED_CODIGO = ubicacion.UnidadMedida.Codigo;
                        row.USTCK_CANTIDADREAL = ubicacion.CantidadReal;
                        row.USTCK_CANTIDADVIRTUAL = ubicacion.CantidadVirtual;
                        row.USTCK_ACTIVO = ubicacion.Activo;
                        row.EndEdit();
                        dsStock.UBICACIONES_STOCK.AddUBICACIONES_STOCKRow(row);
                        dsStock.UBICACIONES_STOCK.AcceptChanges();
                        dgvLista.Refresh();

                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsStock.UBICACIONES_STOCK.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsStock.UBICACIONES_STOCK.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //falta terminar - gonzalo
                }
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + mensaje, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsStock.UBICACIONES_STOCK.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcUbicacionStock.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    txtUbicacionFisica.ReadOnly = false;
                    txtUbicacionFisica.Clear();
                    cboPadre.SetSelectedIndex(-1);
                    cboPadre.Enabled = true;
                    cboUnidadMedida.SetSelectedIndex(-1);
                    cboUnidadMedida.Enabled = true;
                    nudCantidadReal.Value = 0;
                    nudCantidadReal.Enabled = true;
                    nudCantidadVirtual.Value = 0;
                    nudCantidadVirtual.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    txtUbicacionFisica.ReadOnly = false;
                    txtUbicacionFisica.Clear();
                    cboPadre.Enabled = true;
                    cboUnidadMedida.SetSelectedIndex(-1);
                    cboUnidadMedida.Enabled = true;
                    nudCantidadReal.Value = 0;
                    nudCantidadReal.Enabled = true;
                    nudCantidadVirtual.Value = 0;
                    nudCantidadVirtual.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtUbicacionFisica.ReadOnly = true;
                    cboUnidadMedida.Enabled = false;
                    cboPadre.Enabled = false;
                    cboEstado.Enabled = false;
                    cboUnidadMedida.Enabled = false;
                    nudCantidadReal.Enabled = false;
                    nudCantidadVirtual.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtUbicacionFisica.ReadOnly = false;
                    cboUnidadMedida.Enabled = true;
                    cboPadre.Enabled = true;
                    cboEstado.Enabled = true;
                    cboUnidadMedida.Enabled = true;
                    nudCantidadReal.Enabled = false;
                    nudCantidadVirtual.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
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
            dgvLista.Columns.Add("USTCK_PADRE", "Padre");
            dgvLista.Columns.Add("USTCK_CANTIDADREAL", "Cant. real");
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad medida");
            dgvLista.Columns.Add("USTCK_ACTIVO", "Estado");
            dgvLista.Columns["USTCK_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["USTCK_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["USTCK_PADRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["USTCK_CANTIDADREAL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["USTCK_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["USTCK_ACTIVO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["USTCK_CODIGO"].DataPropertyName = "USTCK_CODIGO";
            dgvLista.Columns["USTCK_NOMBRE"].DataPropertyName = "USTCK_NOMBRE";
            dgvLista.Columns["USTCK_PADRE"].DataPropertyName = "USTCK_PADRE";
            dgvLista.Columns["USTCK_CANTIDADREAL"].DataPropertyName = "USTCK_CANTIDADREAL";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["USTCK_ACTIVO"].DataPropertyName = "USTCK_CODIGO";

            dvUbicaciones = new DataView(dsStock.UBICACIONES_STOCK);
            dvUbicaciones.Sort = "USTCK_NOMBRE ASC";
            dgvLista.DataSource = dvUbicaciones;

            try
            {
                BLL.UnidadMedidaBLL.ObtenerTodos(dsStock.UNIDADES_MEDIDA);
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dvUbicaciones = new DataView(dsStock.UBICACIONES_STOCK);
            dvUbicaciones.Sort = "USTCK_NOMBRE ASC";
            dvUbicaciones.RowFilter = "USTCK_NUMERO < 0";
            dvUbicacionPadre = new DataView(dsStock.UBICACIONES_STOCK);
            dvUbicacionPadre.Sort = "USTCK_NOMBRE ASC";

            dvUnidadMedida = new DataView(dsStock.UNIDADES_MEDIDA);
            cboUnidadMedida.SetDatos(dvUnidadMedida, "UMED_CODIGO", "UMED_NOMBRE", "Seleccione", false);
            string[] nombres = { "Activo", "Inactivo" };
            int[] valores = { BLL.UbicacionStockBLL.Activo, BLL.UbicacionStockBLL.Inactivo };
            cboEstadoBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            cboEstado.SetDatos(nombres, valores, "Seleccione", false);
            dvUbicacionPadre = new DataView(dsStock.UBICACIONES_STOCK);
            cboPadre.SetDatos(dvUbicacionPadre, "USTCK_NUMERO", "USTCK_NOMBRE", "Sin especificar", false);
        }
        
        private void frmUbicacionStock_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled == true)
            {
                txtNombreBuscar.Focus();
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCantidadReal.GetType())) { (sender as NumericUpDown).Select(0, 20); }
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
                        nombre = dsStock.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_ABREVIATURA;
                        e.Value = nombre;
                        break;
                    case "USTCK_ACTIVO":
                        if (Convert.ToInt32(e.Value) == BLL.UbicacionStockBLL.Activo) { nombre = "Activo"; }
                        if (Convert.ToInt32(e.Value) == BLL.UbicacionStockBLL.Inactivo) { nombre = "Inactivo"; }
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }        

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                int numero = Convert.ToInt32(dvUbicaciones[e.RowIndex]["ustck_numero"]);
                txtCodigo.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CODIGO;
                txtNombre.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_NOMBRE;
                txtDescripcion.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_DESCRIPCION;
                txtUbicacionFisica.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_UBICACIONFISICA;
                if (!dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).IsUSTCK_PADRENull())
                { cboPadre.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_PADRE)); }
                nudCantidadReal.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL;
                nudCantidadVirtual.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADVIRTUAL;
                cboUnidadMedida.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).UMED_CODIGO));
            }
        }

        #endregion

                
    }
}
