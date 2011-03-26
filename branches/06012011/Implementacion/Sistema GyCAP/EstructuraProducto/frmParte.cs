using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmParte : Form
    {
        private static frmParte _frmParte = null;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        Data.dsEstructuraProducto dsParte = new GyCAP.Data.dsEstructuraProducto();
        DataView dvPartes, dvTipoPartes, dvTerminacion, dvHojaRuta, dvEstado, dvPlano, dvUnidadMedida;
        DataView dvTipoPartesBuscar, dvTerminacionBuscar, dvHojaRutaBuscar, dvEstadoBuscar, dvPlanoBuscar;
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();

        #region Inicio

        public frmParte()
        {
            InitializeComponent();

            SetearDatosIniciales();
            SetInterface(estadoUI.inicio);
        }

        public static frmParte Instancia
        {
            get
            {
                if (_frmParte == null || _frmParte.IsDisposed)
                {
                    _frmParte = new frmParte();
                }
                else
                {
                    _frmParte.BringToFront();
                }
                return _frmParte;
            }
            set
            {
                _frmParte = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        #endregion

        #region Botones menu

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MensajesABM.MsjConfirmaEliminarDatos("Parte", MensajesABM.Generos.Femenino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //obtenemos el código
                        int numero = Convert.ToInt32(dvPartes[dgvLista.SelectedRows[0].Index]["part_numero"]);
                        //Lo eliminamos de la DB
                        BLL.ParteBLL.Eliminar(numero);
                        //Lo eliminamos del dataset
                        dsParte.PARTES.FindByPART_NUMERO(numero).Delete();
                        dsParte.PARTES.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Parte", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsParte.PARTES.Clear();

                BLL.ParteBLL.ObtenerPartes(txtNombreBuscar.Text, txtCodigoBuscar.Text, cboTerminacionBuscar.GetSelectedValue(), cboTipoBuscar.GetSelectedValue(), cboEstadoBuscar.GetSelectedValue(), cboPlanoBuscar.GetSelectedValue(), dsParte.PARTES);
                if (dsParte.PARTES.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Partes", this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
            }
            finally
            {
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Datos

        private void btnVolver_Click(object sender, EventArgs e)
        {
            dsParte.PARTES.RejectChanges();
            //Seteamos la interfaz
            SetInterface(estadoUI.inicio);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<string> validacion = new List<string>();
            if (string.IsNullOrEmpty(txtNombre.Text)) { validacion.Add("Nombre"); }
            if (string.IsNullOrEmpty(txtCodigo.Text)) { validacion.Add("Código"); }
            if (cboEstado.GetSelectedIndex() == -1) { validacion.Add("Estado"); }
            if (cboTipo.GetSelectedIndex() == -1) { validacion.Add("Tipo"); }
            if (cboUnidadMedida.GetSelectedIndex() == -1) { validacion.Add("Unidad de medida"); }
            if (validacion.Count == 0)
            {
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    try
                    {
                        
                        Data.dsEstructuraProducto.PARTESRow row = dsParte.PARTES.NewPARTESRow();
                        row.BeginEdit();
                        row.PART_NOMBRE = txtNombre.Text;
                        row.PART_CODIGO = txtCodigo.Text;
                        row.PART_DESCRIPCION = txtDescripcion.Text;
                        row.TPAR_CODIGO = cboTipo.GetSelectedValueInt();
                        row.PAR_CODIGO = cboEstado.GetSelectedValueInt();
                        if (cboHojaRuta.GetSelectedIndex() != -1) { row.HR_CODIGO = cboHojaRuta.GetSelectedValueInt(); }
                        else { row.SetHR_CODIGONull(); }
                        if (cboPlano.GetSelectedIndex() != -1) { row.PNO_CODIGO = cboPlano.GetSelectedValueInt(); }
                        else { row.SetPNO_CODIGONull(); }
                        if (cboTerminacion.GetSelectedIndex() != -1) { row.TE_CODIGO = cboTerminacion.GetSelectedValueInt(); }
                        else { row.SetTE_CODIGONull(); }
                        row.PART_COSTO = nudCosto.Value;
                        if (chkCostoFijo.Checked) { row.PART_COSTOFIJO = BLL.ParteBLL.CostoFijoChecked; }
                        else { row.PART_COSTOFIJO = BLL.ParteBLL.CostoFijoUnChecked; }
                        row.UMED_CODIGO = cboUnidadMedida.GetSelectedValueInt();
                        row.EndEdit();
                        dsParte.PARTES.AddPARTESRow(row);
                        BLL.ParteBLL.Insertar(dsParte);
                        dsParte.PARTES.AcceptChanges();

                        //Falta guardar la imagen - gonzalo

                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                            SetInterface(estadoUI.inicio);
                        }
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    int numero = Convert.ToInt32(dvPartes[dgvLista.SelectedRows[0].Index]["part_numero"]);

                    try
                    {                        
                        dsParte.PARTES.FindByPART_NUMERO(numero).BeginEdit();
                        dsParte.PARTES.FindByPART_NUMERO(numero).PART_NOMBRE = txtNombre.Text;
                        dsParte.PARTES.FindByPART_NUMERO(numero).PART_DESCRIPCION = txtDescripcion.Text;
                        dsParte.PARTES.FindByPART_NUMERO(numero).PART_CODIGO = txtCodigo.Text;
                        dsParte.PARTES.FindByPART_NUMERO(numero).PAR_CODIGO = cboEstado.GetSelectedValueInt();
                        dsParte.PARTES.FindByPART_NUMERO(numero).TPAR_CODIGO = cboTipo.GetSelectedValueInt();
                        dsParte.PARTES.FindByPART_NUMERO(numero).PART_COSTO = nudCosto.Value;
                        if (cboHojaRuta.GetSelectedIndex() != -1) { dsParte.PARTES.FindByPART_NUMERO(numero).HR_CODIGO = cboHojaRuta.GetSelectedValueInt(); }
                        else { dsParte.PARTES.FindByPART_NUMERO(numero).SetHR_CODIGONull(); }
                        if (cboPlano.GetSelectedIndex() != -1) { dsParte.PARTES.FindByPART_NUMERO(numero).PNO_CODIGO = cboPlano.GetSelectedValueInt(); }
                        else { dsParte.PARTES.FindByPART_NUMERO(numero).SetPNO_CODIGONull(); }
                        if (cboTerminacion.GetSelectedIndex() != -1) { dsParte.PARTES.FindByPART_NUMERO(numero).TE_CODIGO = cboTerminacion.GetSelectedValueInt(); }
                        else { dsParte.PARTES.FindByPART_NUMERO(numero).SetTE_CODIGONull(); }
                        if (chkCostoFijo.Checked) { dsParte.PARTES.FindByPART_NUMERO(numero).PART_COSTOFIJO = BLL.ParteBLL.CostoFijoChecked; }
                        else { dsParte.PARTES.FindByPART_NUMERO(numero).PART_COSTOFIJO = BLL.ParteBLL.CostoFijoUnChecked; }
                        dsParte.PARTES.FindByPART_NUMERO(numero).UMED_CODIGO = cboUnidadMedida.GetSelectedValueInt();
                        dsParte.PARTES.FindByPART_NUMERO(numero).EndEdit();
                        BLL.ParteBLL.Actualizar(dsParte);
                        dsParte.PARTES.AcceptChanges();
                        MensajesABM.MsjConfirmaGuardar("Parte", this.Text, MensajesABM.Operaciones.Modificación);
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoInexistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(MensajesABM.Validaciones.CompletarDatos, validacion), this.Text);
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

                    if (dsParte.PARTES.Rows.Count == 0)
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
                    btnZoomOut.PerformClick();
                    estadoInterface = estadoUI.inicio;
                    tcParte.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    cboPlano.SetTexto("Seleccione");
                    cboPlano.Enabled = true;
                    cboTipo.SetTexto("Seleccione");
                    cboTipo.Enabled = true;
                    cboEstado.SetTexto("Seleccione");
                    cboEstado.Enabled = true;
                    cboTerminacion.SetTexto("Seleccione");
                    cboTerminacion.Enabled = true;
                    cboUnidadMedida.Enabled = true;
                    cboUnidadMedida.SetTexto("Seleccione");
                    cboHojaRuta.SetTexto("Seleccione");
                    cboHojaRuta.Enabled = true;
                    nudCosto.Value = 0;
                    nudCosto.Enabled = true;
                    chkCostoFijo.Checked = false;
                    chkCostoFijo.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    btnZoomOut.PerformClick();
                    estadoInterface = estadoUI.nuevo;
                    tcParte.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    cboPlano.SetTexto("Seleccione");
                    cboPlano.Enabled = true;
                    cboTipo.SetTexto("Seleccione");
                    cboTipo.Enabled = true;
                    cboEstado.SetTexto("Seleccione");
                    cboEstado.Enabled = true;
                    cboTerminacion.SetTexto("Seleccione");
                    cboTerminacion.Enabled = true;
                    cboUnidadMedida.Enabled = true;
                    cboUnidadMedida.SetTexto("Seleccione");
                    cboHojaRuta.SetTexto("Seleccione");
                    cboHojaRuta.Enabled = true;
                    nudCosto.Value = 0;
                    nudCosto.Enabled = true;
                    chkCostoFijo.Checked = false;
                    chkCostoFijo.Enabled = true;                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    btnZoomOut.PerformClick();
                    estadoInterface = estadoUI.nuevoExterno;
                    tcParte.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtCodigo.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    cboPlano.Enabled = false;
                    cboTipo.Enabled = false;
                    cboEstado.Enabled = false;
                    cboTerminacion.Enabled = false;
                    cboUnidadMedida.Enabled = false;
                    cboHojaRuta.Enabled = false;
                    nudCosto.Enabled = false;
                    chkCostoFijo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    btnAbrirImagen.Enabled = false;
                    btnQuitarImagen.Enabled = false;
                    btnZoomOut.PerformClick();
                    estadoInterface = estadoUI.consultar;
                    tcParte.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtCodigo.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    cboPlano.Enabled = true;
                    cboTipo.Enabled = true;
                    cboEstado.Enabled = true;
                    cboTerminacion.Enabled = true;
                    cboUnidadMedida.Enabled = true;
                    cboHojaRuta.Enabled = true;
                    nudCosto.Enabled = true;
                    chkCostoFijo.Enabled = true;                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    btnZoomOut.PerformClick();
                    estadoInterface = estadoUI.modificar;
                    tcParte.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                default:
                    break;
            }
        }

        private void SetearDatosIniciales()
        {
            //Cargo los datos
            try
            {
                BLL.TipoParteBLL.ObtenerTodos(dsParte.TIPOS_PARTES);
                BLL.EstadoParteBLL.ObtenerTodos(dsParte.ESTADO_PARTES);
                BLL.PlanoBLL.ObtenerTodos(dsParte.PLANOS);
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsParte.TERMINACIONES);
                BLL.HojaRutaBLL.ObtenerHojasRuta(dsParte.HOJAS_RUTA);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsParte.UNIDADES_MEDIDA);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            //Grilla 
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("PART_NOMBRE", "Nombre");
            dgvLista.Columns.Add("PART_CODIGO", "Código");
            dgvLista.Columns.Add("PNO_CODIGO", "Plano");
            dgvLista.Columns.Add("PAR_CODIGO", "Estado");
            dgvLista.Columns.Add("TPAR_CODIGO", "Tipo de parte");
            dgvLista.Columns.Add("TE_CODIGO", "Terminación");
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad medida");
            dgvLista.Columns.Add("HR_CODIGO", "Hoja de ruta");
            dgvLista.Columns["PART_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PART_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PNO_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PAR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["HR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PART_NOMBRE"].DataPropertyName = "PART_NOMBRE";
            dgvLista.Columns["PART_CODIGO"].DataPropertyName = "PART_CODIGO";
            dgvLista.Columns["PNO_CODIGO"].DataPropertyName = "PNO_CODIGO";
            dgvLista.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvLista.Columns["TPAR_CODIGO"].DataPropertyName = "TPAR_CODIGO";
            dgvLista.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["HR_CODIGO"].DataPropertyName = "HR_CODIGO";

            //Dataviews y combos
            dvPartes = new DataView(dsParte.PARTES);
            dvPartes.Sort = "PART_NOMBRE ASC";
            dgvLista.DataSource = dvPartes;
            dvTipoPartes = new DataView(dsParte.TIPOS_PARTES);
            dvEstado = new DataView(dsParte.ESTADO_PARTES);
            dvPlano = new DataView(dsParte.PLANOS);
            dvTerminacion = new DataView(dsParte.TERMINACIONES);
            dvHojaRuta = new DataView(dsParte.HOJAS_RUTA);
            dvTipoPartesBuscar = new DataView(dsParte.TIPOS_PARTES);
            dvEstadoBuscar = new DataView(dsParte.ESTADO_PARTES);
            dvPlanoBuscar = new DataView(dsParte.PLANOS);
            dvTerminacionBuscar = new DataView(dsParte.TERMINACIONES);
            dvUnidadMedida = new DataView(dsParte.UNIDADES_MEDIDA);
            dvHojaRutaBuscar = new DataView(dsParte.HOJAS_RUTA);
            
            cboTipoBuscar.SetDatos(dvTipoPartesBuscar, "TPAR_CODIGO", "TPAR_NOMBRE", "TPAR_NOMBRE ASC", "TODOS", true);
            cboEstadoBuscar.SetDatos(dvEstadoBuscar, "PAR_CODIGO", "PAR_NOMBRE", "PAR_NOMBRE ASC", "TODOS", true);
            cboPlanoBuscar.SetDatos(dvPlanoBuscar, "PNO_CODIGO", "PNO_NOMBRE", "PNO_NOMBRE ASC", "TODOS", true);
            cboTerminacionBuscar.SetDatos(dvTerminacionBuscar, "TE_CODIGO", "TE_NOMBRE", "TE_NOMBRE ASC", "TODOS", true);
            cboTipo.SetDatos(dvTipoPartes, "TPAR_CODIGO", "TPAR_NOMBRE", "TPAR_NOMBRE ASC", "Seleccione", false);
            cboEstado.SetDatos(dvEstado, "PAR_CODIGO", "PAR_NOMBRE", "PAR_NOMBRE ASC", "Seleccione", false);
            cboPlano.SetDatos(dvPlano, "PNO_CODIGO", "PNO_NOMBRE", "PNO_NOMBRE ASC", "Seleccione", false);
            cboTerminacion.SetDatos(dvTerminacion, "TE_CODIGO", "TE_NOMBRE", "TE_NOMBRE ASC", "Seleccione", false);
            cboUnidadMedida.SetDatos(dvUnidadMedida, "UMED_CODIGO", "UMED_NOMBRE", "UMED_NOMBRE ASC", "Seleccione", false);
            cboHojaRuta.SetDatos(dvHojaRuta, "HR_CODIGO", "HR_NOMBRE", "HR_NOMBRE ASC", "Seleccione", false);
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCosto.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {                   
                    case "PNO_CODIGO":
                        nombre = dsParte.PLANOS.FindByPNO_CODIGO(Convert.ToInt32(e.Value)).PNO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "PAR_CODIGO":
                        nombre = dsParte.ESTADO_PARTES.FindByPAR_CODIGO(Convert.ToInt32(e.Value)).PAR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TPAR_CODIGO":
                        nombre = dsParte.TIPOS_PARTES.FindByTPAR_CODIGO(Convert.ToInt32(e.Value)).TPAR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "HR_CODIGO":
                        nombre = dsParte.HOJAS_RUTA.FindByHR_CODIGO(Convert.ToInt32(e.Value)).HR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TE_CODIGO":
                        nombre = dsParte.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value)).TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "UMED_CODIGO":
                        nombre = dsParte.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int numero = Convert.ToInt32(dvPartes[e.RowIndex]["part_numero"]);
            txtNombre.Text = dsParte.PARTES.FindByPART_NUMERO(numero).PART_NOMBRE;
            txtCodigo.Text = dsParte.PARTES.FindByPART_NUMERO(numero).PART_CODIGO;
            txtDescripcion.Text = dsParte.PARTES.FindByPART_NUMERO(numero).PART_DESCRIPCION;
            cboEstado.SetSelectedValue(Convert.ToInt32(dsParte.PARTES.FindByPART_NUMERO(numero).PAR_CODIGO));
            if (dsParte.PARTES.FindByPART_NUMERO(numero).IsHR_CODIGONull()) { cboHojaRuta.SetTexto(string.Empty); }
            else { cboHojaRuta.SetSelectedValue(Convert.ToInt32(dsParte.PARTES.FindByPART_NUMERO(numero).HR_CODIGO)); }
            if (dsParte.PARTES.FindByPART_NUMERO(numero).IsPNO_CODIGONull()) { cboPlano.SetTexto(string.Empty); }
            else { cboPlano.SetSelectedValue(Convert.ToInt32(dsParte.PARTES.FindByPART_NUMERO(numero).PNO_CODIGO)); }
            if (dsParte.PARTES.FindByPART_NUMERO(numero).IsTE_CODIGONull()) { cboTerminacion.SetTexto(string.Empty); }
            else { cboTerminacion.SetSelectedValue(Convert.ToInt32(dsParte.PARTES.FindByPART_NUMERO(numero).TE_CODIGO)); }
            cboTipo.SetSelectedValue(Convert.ToInt32(dsParte.PARTES.FindByPART_NUMERO(numero).TPAR_CODIGO));
            nudCosto.Value = dsParte.PARTES.FindByPART_NUMERO(numero).PART_COSTO;
            if (dsParte.PARTES.FindByPART_NUMERO(numero).PART_COSTOFIJO == 0) { chkCostoFijo.Checked = false; }
            else { chkCostoFijo.Checked = true; }
            cboUnidadMedida.SetSelectedValue(Convert.ToInt32(dsParte.PARTES.FindByPART_NUMERO(numero).UMED_CODIGO));
            //Falta agregar la imagen de la parte - gonzalo
        }

        #endregion

        #region Imagen y look & feel

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
                (sender as Button).Location = punto;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
                (sender as Button).Location = punto;
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            Sistema.frmImagenZoom.Instancia.SetImagen(pbImagen.Image, "Imagen de la Parte");
            animador.SetFormulario(Sistema.frmImagenZoom.Instancia, this, Sistema.ControlesUsuarios.AnimadorFormulario.animacionDerecha, 300, true);
            animador.MostrarFormulario();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            animador.CerrarFormulario();
        }

        private void ActualizarImagen()
        {
            if (animador.EsVisible())
            {
                (animador.GetForm() as Sistema.frmImagenZoom).SetImagen(pbImagen.Image, "Imagen de la Parte");
            }
        }

        private void pbImagen_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ActualizarImagen();
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            ofdImagen.ShowDialog();
        }

        private void ofdImagen_FileOk(object sender, CancelEventArgs e)
        {
            pbImagen.ImageLocation = ofdImagen.FileName;
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            pbImagen.Image = EstructuraProducto.Properties.Resources.sinimagen;
            ActualizarImagen();
        }

        #endregion
    }
}
