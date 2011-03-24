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
    public partial class frmEstructuraCocina : Form
    {
        private static frmEstructuraCocina _frmEstructuraCocina = null;
        private Data.dsEstructuraProducto dsEstructura = new GyCAP.Data.dsEstructuraProducto();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        //Nombre objetos: CD=ConjuntosDisponibles - CE=ConjuntosEstructura - PD=PiezasDisponibles - PE=PiezasEstructura
        private DataView dvCocinaBuscar, dvCocina, dvResponsableBuscar, dvResponsable, dvPlanoBuscar, dvPlano;
        private DataView dvEstructuras, dvPartes, dvCD, dvCE, dvPD, dvPE;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private int cxe = -1, pxe = -1; //Variables para el manejo de inserciones en los dataset con códigos unique

        #region Inicio

        public frmEstructuraCocina()
        {
            InitializeComponent();

            SetGrillasCombosVistas();
            SetInterface(estadoUI.inicio);
        }

        public static frmEstructuraCocina Instancia
        {
            get
            {
                if (_frmEstructuraCocina == null || _frmEstructuraCocina.IsDisposed)
                {
                    _frmEstructuraCocina = new frmEstructuraCocina();
                }
                else
                {
                    _frmEstructuraCocina.BringToFront();
                }
                return _frmEstructuraCocina;
            }
            set
            {
                _frmEstructuraCocina = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

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
            if (dgvEstructuras.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Estructura seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //obtenemos el código
                        int codigo = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.EstructuraBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigo).Delete();
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Modelo de Cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion Inicio

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {                
                dsEstructura.PARTES.Clear();                
                dsEstructura.COMPUESTOS_PARTES.Clear();
                dsEstructura.ESTRUCTURAS.Clear();
                BLL.EstructuraBLL.ObtenerEstructuras(txtNombreBuscar.Text, cbPlanoBuscar.GetSelectedValue(), dtpFechaAltaBuscar.GetFecha(), cbCocinaBuscar.GetSelectedValue(), cbResponsableBuscar.GetSelectedValue(), ((Sistema.Item)cbActivoBuscar.SelectedItem).Value, dsEstructura);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvEstructuras.Table = dsEstructura.ESTRUCTURAS;
                if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Estructuras con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Estructuras - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisemos que completó todos los datos obligatorios          
            string datosFaltantes = string.Empty;
            if (txtNombre.Text == string.Empty) { datosFaltantes += "* Nombre\n"; }
            if (cbPlano.GetSelectedIndex() == -1) { datosFaltantes += "* Plano\n"; }
            if (cbCocina.GetSelectedIndex() == -1) { datosFaltantes += "* Cocina\n"; }
            //if (cbResponsable.GetSelectedIndex() == -1) { datosOK = false; datosFaltantes += "\\n* Responsable"; } Por ahora opcional
            //if (dtpFechaAlta.IsValueNull()) { dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor()); } Opcional por ahora
            if (dgvPartes.Rows.Count == 0) { datosFaltantes += "* El detalle de la estructura\n"; } //que al menos haya cargado 1 parte
            if (cbEstado.GetSelectedIndex() == -1) { datosFaltantes += "* Estado\n"; }
            if (datosFaltantes == string.Empty)
            {
                //Datos OK, revisemos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Creando uno nuevo
                    try
                    {
                        Data.dsEstructuraProducto.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.NewESTRUCTURASRow();
                        rowEstructura.BeginEdit();
                        rowEstructura.ESTR_CODIGO = -1;
                        rowEstructura.ESTR_NOMBRE = txtNombre.Text;
                        rowEstructura.PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        rowEstructura.ESTR_ACTIVO = cbEstado.GetSelectedValueInt(); ;
                        if (dtpFechaAlta.IsValueNull()) { rowEstructura.ESTR_FECHA_ALTA = BLL.DBBLL.GetFechaServidor(); }
                        else { rowEstructura.ESTR_FECHA_ALTA = (DateTime)dtpFechaAlta.GetFecha(); }
                        rowEstructura.COC_CODIGO = cbCocina.GetSelectedValueInt();
                        if (cbResponsable.GetSelectedIndex() == -1) rowEstructura.SetE_CODIGONull();
                        else { rowEstructura.E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { rowEstructura.SetESTR_FECHA_MODIFICACIONNull(); }
                        else { rowEstructura.ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        rowEstructura.ESTR_COSTO = nudcosto.Value;
                        rowEstructura.ESTR_DESCRIPCION = txtDescripcion.Text;
                        rowEstructura.ESTR_COSTOFIJO = (chkFijo.Checked) ? 1 : 0;
                        rowEstructura.EndEdit();
                        dsEstructura.ESTRUCTURAS.AddESTRUCTURASRow(rowEstructura);
                        BLL.EstructuraBLL.Insertar(dsEstructura);
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                        
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
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de estructuras ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.ESTRUCTURAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Modificando
                    try
                    {
                        int codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_NOMBRE = txtNombre.Text;
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_ACTIVO = cbEstado.GetSelectedValueInt();
                        if (dtpFechaAlta.IsValueNull()) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_ALTA = BLL.DBBLL.GetFechaServidor(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_ALTA = (DateTime)dtpFechaAlta.GetFecha(); }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).COC_CODIGO = cbCocina.GetSelectedValueInt();
                        if (cbResponsable.GetSelectedIndex() == -1) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).SetE_CODIGONull(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).SetESTR_FECHA_MODIFICACIONNull(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_DESCRIPCION = txtDescripcion.Text;
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_COSTO = nudcosto.Value;
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_COSTOFIJO = (chkFijo.Checked) ? 1 : 0;
                        BLL.EstructuraBLL.Actualizar(dsEstructura);
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de estructuras ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.ESTRUCTURAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //le faltan completar datos, avisemos
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsEstructura.COMPUESTOS_PARTES.RejectChanges();
            dsEstructura.PARTES.RejectChanges();
            dsEstructura.ESTRUCTURAS.RejectChanges();
            //dsEstructura.LISTA_PARTES.Clear();
            SetInterface(estadoUI.inicio);
        }        

        #endregion    

        private void btnDatos_Click(object sender, EventArgs e)
        {
            
        }

        private void btnConjuntos_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPiezas_Click(object sender, EventArgs e)
        {
            
        }

        private void SetBotones(int orientacion, Button boton)
        {
            switch (orientacion)
            {
                case -1:
                    boton.Image = Properties.Resources.izquierda1_15;
                    break;
                case 0:
                    boton.Image = Properties.Resources.arriba1_15;
                    break;
                case 1:
                    boton.Image = Properties.Resources.derecha1_15;
                    break;
                default:
                    break;
            }
            boton.TextImageRelation = TextImageRelation.TextBeforeImage;
        }
        
        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
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
                    tcEstructuraCocina.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SelectedIndex = -1;
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    dtpFechaAlta.Enabled = true;
                    dtpFechaAlta.SetFechaNull();
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    dtpFechaModificacion.SetFechaNull();
                    txtDescripcion.ReadOnly = false;
                    nudcosto.Value = 0;
                    nudcosto.Enabled = true;
                    chkFijo.Enabled = true;
                    chkFijo.Checked = false;
                    txtDescripcion.Clear();
                    //dsEstructura.LISTA_PARTES.Clear();
                    dvCE.RowFilter = "ESTR_CODIGO = -1";
                    dvPE.RowFilter = "ESTR_CODIGO = -1";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    //CargarCSCPMP();
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SelectedIndex = -1;
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    dtpFechaAlta.Enabled = true;
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    dtpFechaModificacion.SetFechaNull();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    nudcosto.Enabled = true;
                    nudcosto.Value = 0;
                    chkFijo.Enabled = true;
                    chkFijo.Checked = false;
                    //dsEstructura.LISTA_PARTES.Clear();
                    dvCE.RowFilter = "ESTR_CODIGO = -1";
                    dvPE.RowFilter = "ESTR_CODIGO = -1";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    //CargarCSCPMP();
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbPlano.Enabled = false;
                    cbEstado.Enabled = false;
                    dtpFechaAlta.Enabled = false;
                    cbCocina.Enabled = false;
                    cbResponsable.Enabled = false;
                    dtpFechaModificacion.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    nudcosto.Enabled = false;
                    chkFijo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    //CargarCSCPMP();
                    //CargarListaPartes();
                    btnDatos.PerformClick();
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    cbPlano.Enabled = true;
                    cbEstado.Enabled = true;
                    dtpFechaAlta.Enabled = true;
                    cbCocina.Enabled = true;
                    cbResponsable.Enabled = true;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    nudcosto.Enabled = true;
                    chkFijo.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    //CargarCSCPMP();
                    //CargarListaPartes();
                    btnDatos.PerformClick();
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            //if (sender.GetType().Equals(nudC.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void SetGrillasCombosVistas()
        {
            //Obtenemos los datos iniciales necesarios: terminaciones, empleados, cocinas
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.CocinaBLL.ObtenerCocinas(dsCocina.COCINAS);
                BLL.EmpleadoBLL.ObtenerEmpleados(dsEmpleado.EMPLEADOS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region Buscar
            //Grilla Listado Estructuras
            dgvEstructuras.AutoGenerateColumns = false;
            dgvEstructuras.Columns.Add("ESTR_NOMBRE", "Nombre");
            dgvEstructuras.Columns.Add("COC_CODIGO", "Cocina");
            dgvEstructuras.Columns.Add("PNO_CODIGO", "Plano");
            dgvEstructuras.Columns.Add("E_CODIGO", "Responsable");
            dgvEstructuras.Columns.Add("ESTR_ACTIVO", "Activo");
            dgvEstructuras.Columns.Add("ESTR_FECHA_ALTA", "Fecha creación");
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstructuras.Columns["ESTR_ACTIVO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstructuras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].MinimumWidth = 110;
            dgvEstructuras.Columns["ESTR_NOMBRE"].DataPropertyName = "ESTR_NOMBRE";
            dgvEstructuras.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvEstructuras.Columns["PNO_CODIGO"].DataPropertyName = "PNO_CODIGO";
            dgvEstructuras.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvEstructuras.Columns["ESTR_ACTIVO"].DataPropertyName = "ESTR_ACTIVO";
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DataPropertyName = "ESTR_FECHA_ALTA";

            //Dataviews
            dvEstructuras = new DataView(dsEstructura.ESTRUCTURAS);
            dvEstructuras.Sort = "ESTR_NOMBRE ASC";
            dgvEstructuras.DataSource = dvEstructuras;
            dvCocinaBuscar = new DataView(dsCocina.COCINAS);
            dvResponsableBuscar = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsableBuscar.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            dvPlanoBuscar = new DataView(dsEstructura.PLANOS);

            //ComboBoxs
            cbCocinaBuscar.SetDatos(dvCocinaBuscar, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "--TODOS--", true);
            string[] displaymember = { "E_APELLIDO", "E_NOMBRE" };
            cbResponsableBuscar.SetDatos(dvResponsableBuscar, "E_CODIGO", displaymember, ", ", "--TODOS--", true);
            cbPlanoBuscar.SetDatos(dvPlanoBuscar, "PNO_CODIGO", "PNO_NOMBRE", "--TODOS--", true);
            cbActivoBuscar.Items.Add(new Sistema.Item("--TODOS--", -1));
            cbActivoBuscar.Items.Add(new Sistema.Item("SI", 1));
            cbActivoBuscar.Items.Add(new Sistema.Item("NO", 0));
            cbActivoBuscar.DisplayMember = "Name";
            cbActivoBuscar.ValueMember = "Value";
            cbActivoBuscar.SelectedIndex = 0;

            #endregion Buscar

            #region Datos
            //Grilla Listado Partes
            dgvPartes.AutoGenerateColumns = false;
            dgvPartes.Columns.Add("PAR_TIPO", "Tipo");
            dgvPartes.Columns.Add("PAR_CODIGO", "Código");
            dgvPartes.Columns.Add("PAR_NOMBRE", "Nombre");
            dgvPartes.Columns.Add("PAR_TERMINACION", "Terminación");
            dgvPartes.Columns.Add("PAR_CANTIDAD", "Cantidad");
            dgvPartes.Columns.Add("PAR_UMED", "Unidad Medida");
            dgvPartes.Columns["PAR_TIPO"].DataPropertyName = "PAR_TIPO";
            dgvPartes.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvPartes.Columns["PAR_NOMBRE"].DataPropertyName = "PAR_NOMBRE";
            dgvPartes.Columns["PAR_TERMINACION"].DataPropertyName = "PAR_TERMINACION";
            dgvPartes.Columns["PAR_CANTIDAD"].DataPropertyName = "PAR_CANTIDAD";
            dgvPartes.Columns["PAR_UMED"].DataPropertyName = "PAR_UMED";
            dgvPartes.Columns["PAR_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //Dataviews
            dvCocina = new DataView(dsCocina.COCINAS);
            dvCocina.Sort = "COC_CODIGO_PRODUCTO ASC";
            dvResponsable = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsable.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            dvPlano = new DataView(dsEstructura.PLANOS);
            dvPlano.Sort = "PNO_NOMBRE ASC";
            //dvPartes = new DataView(dsEstructura.LISTA_PARTES);
            dgvPartes.DataSource = dvPartes;

            //ComboBoxs
            cbCocina.SetDatos(dvCocina, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione", false);
            cbResponsable.SetDatos(dvResponsable, "E_CODIGO", displaymember, ", ", "Seleccione", false);
            cbPlano.SetDatos(dvPlano, "PNO_CODIGO", "PNO_NOMBRE", "Seleccione", false);
            string[] nombres = { "Activa", "Inactiva" };
            int[] valores = { BLL.EstructuraBLL.EstructuraActiva, BLL.EstructuraBLL.EstructuraInactiva };
            cbEstado.SetDatos(nombres, valores, "Seleccione", false);

            #endregion Datos

      
        }

        /*private void CargarListaPartes()
        {
            dsEstructura.LISTA_PARTES.Clear();
            int codigoEstructura = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
            else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }

            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in
                (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.ESTR_CODIGO = codigoEstructura.ToString();
                rowParte.PAR_TIPO = "Conjunto";
                rowParte.PAR_CODIGO = row.CONJUNTOSRow.CONJ_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.CONJUNTOSRow.CONJ_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.CXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in
                (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.ESTR_CODIGO = codigoEstructura.ToString();
                rowParte.PAR_TIPO = "Pieza";
                rowParte.PAR_CODIGO = row.PIEZASRow.PZA_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.PIEZASRow.PZA_NOMBRE;
                rowParte.PAR_TERMINACION = dsEstructura.TERMINACIONES.FindByTE_CODIGO(row.PIEZASRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.PXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            dvPartes.Table = dsEstructura.LISTA_PARTES;
        }*/

        private void frmEstructuraCocina_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled) { txtNombreBuscar.Focus(); }
        }
        
        private decimal CalcularCosto()
        {
            decimal costo = 0;
            int codigoEstructura = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
            else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }

            /*foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in
                (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                costo += (row.CONJUNTOSRow.CONJ_COSTO * row.CXE_CANTIDAD);
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in
                (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                costo += (row.PIEZASRow.PZA_COSTO * row.PXE_CANTIDAD);
            }*/

            return costo;
        }

        private void chkFijo_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFijo.Checked) { nudcosto.Value = CalcularCosto(); }
            else { nudcosto.Value = 0; }
        }

        #endregion Servicios

        #region Cell_Formatting y RowEnter

        private void dgvEstructuras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty; //tira error despues de guardar - gonzalo
                switch (dgvEstructuras.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsCocina.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "PNO_CODIGO":
                        nombre = dsEstructura.PLANOS.FindByPNO_CODIGO(Convert.ToInt32(e.Value)).PNO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "E_CODIGO":
                        nombre = dsEmpleado.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_APELLIDO;
                        nombre += ", " + dsEmpleado.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "ESTR_ACTIVO":
                        nombre = "No";
                        if (Convert.ToInt32(e.Value) == 1) { nombre = "Si"; }
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvEstructuras_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codEstructura = Convert.ToInt32(dvEstructuras[e.RowIndex]["estr_codigo"]);
            txtNombre.Text = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_NOMBRE;
            cbPlano.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).PLANOSRow.PNO_CODIGO));
            cbEstado.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_ACTIVO));
            dtpFechaAlta.SetFecha(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_FECHA_ALTA);
            cbCocina.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).COC_CODIGO));
            if (!dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).IsE_CODIGONull())
            {
                cbResponsable.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).E_CODIGO));
            }
            else { cbResponsable.SetSelectedIndex(-1); }
            if (!dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).IsESTR_FECHA_MODIFICACIONNull())
            {
                dtpFechaModificacion.SetFecha(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_FECHA_MODIFICACION);
            }
            else { dtpFechaModificacion.SetFechaNull(); }
            txtDescripcion.Text = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_DESCRIPCION;
            nudcosto.Value = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_COSTO;
            if (dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_COSTOFIJO == 0) { chkFijo.Checked = false; }
            else { chkFijo.Checked = true; }            
        }

        #endregion


    }
}

