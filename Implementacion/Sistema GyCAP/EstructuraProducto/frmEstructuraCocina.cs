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
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        //Nombre objetos: CD=ConjuntosDisponibles - CE=ConjuntosEstructura - SCD=SubconjuntosDisponibles - SCE=SubconjuntosEstructura
        //PD=PiezasDisponibles - PE=PiezasEstructura - MPD=MateriaPrimaDisponibles - MPE=MateriaPrimaEstructura
        private DataView dvCocinaBuscar, dvCocina, dvResponsableBuscar, dvResponsable, dvPlanoBuscar, dvPlano;
        private DataView dvEstructuras, dvPartes, dvCD, dvCE, dvSCD, dvSCE, dvPD, dvPE, dvMPD, dvMPE;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private int slideActual = 0; //0-Datos, 1-Conjuntos, 2-Subconjuntos, 3-Piezas, 4-MateriaPrima, 5-Árbol
        private int cxe = 0, scxe = 0, pxe = 0, mpxe = 0; //Variables para el manejo de inserciones en los dataset con códigos unique

        #region Inicio

        public frmEstructuraCocina()
        {
            InitializeComponent();

            SetGrillasCombosVistas();
            SetSlide();
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
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Estructura seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                dsEstructura.Clear();                
                BLL.EstructuraBLL.ObtenerEstructuras(txtNombreBuscar.Text, cbPlanoBuscar.GetSelectedValue(), dtpFechaAltaBuscar.GetFecha(), cbCocinaBuscar.GetSelectedValue(), cbResponsableBuscar.GetSelectedValue(), cbActivoBuscar.SelectedItem, dsEstructura);
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
            if (datosFaltantes == string.Empty)
            {
                //Datos OK, revisemos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Creando uno nuevo
                    try
                    {
                        Data.dsEstructura.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.NewESTRUCTURASRow();
                        rowEstructura.BeginEdit();
                        rowEstructura.ESTR_CODIGO = -1;
                        rowEstructura.ESTR_NOMBRE = txtNombre.Text;
                        rowEstructura.PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        rowEstructura.ESTR_ACTIVO = (chkActivo.Checked) ? BLL.EstructuraBLL.EstructuraActiva : BLL.EstructuraBLL.EstructuraInactiva;
                        if (dtpFechaAlta.IsValueNull()) { rowEstructura.ESTR_FECHA_ALTA = BLL.DBBLL.GetFechaServidor(); }
                        else { rowEstructura.ESTR_FECHA_ALTA = (DateTime)dtpFechaAlta.GetFecha(); }
                        rowEstructura.COC_CODIGO = cbCocina.GetSelectedValueInt();
                        if (cbResponsable.GetSelectedIndex() == -1) rowEstructura.SetE_CODIGONull();
                        else { rowEstructura.E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { rowEstructura.SetESTR_FECHA_MODIFICACIONNull(); }
                        else { rowEstructura.ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        rowEstructura.ESTR_DESCRIPCION = txtDescripcion.Text;
                        rowEstructura.EndEdit();
                        dsEstructura.ESTRUCTURAS.AddESTRUCTURASRow(rowEstructura);
                        BLL.EstructuraBLL.Insertar(dsEstructura);
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                        dsEstructura.GRUPOS_ESTRUCTURA.AcceptChanges();
                        dsEstructura.CONJUNTOSXESTRUCTURA.AcceptChanges();
                        dsEstructura.SUBCONJUNTOSXESTRUCTURA.AcceptChanges();
                        dsEstructura.PIEZASXESTRUCTURA.AcceptChanges();
                        dsEstructura.MATERIASPRIMASXESTRUCTURA.AcceptChanges();

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
                        if (chkActivo.Checked) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_ACTIVO = BLL.EstructuraBLL.EstructuraActiva; }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_ACTIVO = BLL.EstructuraBLL.EstructuraInactiva; }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_ALTA = (DateTime)dtpFechaAlta.GetFecha();
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).COC_CODIGO = cbCocina.GetSelectedValueInt();
                        if (cbResponsable.GetSelectedIndex() == -1) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).SetE_CODIGONull(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).SetESTR_FECHA_MODIFICACIONNull(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_DESCRIPCION = txtDescripcion.Text;
                        BLL.EstructuraBLL.Actualizar(dsEstructura);
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dsEstructura.MATERIASPRIMASXESTRUCTURA.RejectChanges();
            dsEstructura.PIEZASXESTRUCTURA.RejectChanges();
            dsEstructura.SUBCONJUNTOSXESTRUCTURA.RejectChanges();
            dsEstructura.CONJUNTOSXESTRUCTURA.RejectChanges();
            dsEstructura.GRUPOS_ESTRUCTURA.RejectChanges();
            dsEstructura.ESTRUCTURAS.RejectChanges();
            dsEstructura.LISTA_PARTES.Clear();
            SetInterface(estadoUI.inicio);
        }

        private void clbVer_SelectedValueChanged(object sender, EventArgs e)
        {
            string filtro = string.Empty;
            if (clbVer.GetItemChecked(0)) { filtro = "PAR_TIPO <> Conjunto "; }
            else { filtro = "PAR_TIPO = Conjuntos"; }

            if (clbVer.GetItemChecked(1)) { filtro += " AND PAR_TIPO <> Subconjunto"; }
            else { filtro += " AND PAR_TIPO = Subconjunto"; }

            if (clbVer.GetItemChecked(2)) { filtro += " AND PAR_TIPO <> Pieza"; }
            else { filtro += " AND PAR_TIPO = Subconjunto"; }

            if (clbVer.GetItemChecked(3)) { filtro += " AND PAR_TIPO <> Materia Prima"; }
            else { filtro += " AND PAR_TIPO = Subconjunto"; }

            dvPartes.RowFilter = filtro;
        }

        #endregion

        #region Conjuntos
        #endregion

        #region Subconjuntos
        #endregion

        #region Piezas
        #endregion

        #region Materia Prima
        #endregion

        #region Árbol
        #endregion

        #region SlideControl

        private void btnDatos_Click(object sender, EventArgs e)
        {
            if (slideActual != 0)
            {
                slideControl.BackwardTo("slideDatos");
                SetTextBotones("Datos ^","Conjuntos >","Subconjuntos >","Piezas >","Materia Prima >");
                slideActual = 0;
            }
        }        
        
        private void btnConjuntos_Click(object sender, EventArgs e)
        {
            if (slideActual != 1)
            {
                if (slideActual > 1) { slideControl.BackwardTo("slideConjuntos"); }
                else { slideControl.ForwardTo("slideConjuntos"); }
                SetTextBotones("< Datos", "Conjuntos ^", "Subconjuntos >", "Piezas >", "Materia Prima >");
                slideActual = 1;
            }
        }

        private void btnSubconjuntos_Click(object sender, EventArgs e)
        {
            if (slideActual != 2)
            {
                if (slideActual > 2) { slideControl.BackwardTo("slideSubconjuntos"); }
                else { slideControl.ForwardTo("slideSubconjuntos"); }
                SetTextBotones("< Datos", "< Conjuntos", "Subconjuntos ^", "Piezas >", "Materia Prima >");
                slideActual = 2;
            }
        }

        private void btnPiezas_Click(object sender, EventArgs e)
        {
            if (slideActual != 3)
            {
                if (slideActual > 3) { slideControl.BackwardTo("slidePiezas"); }
                else { slideControl.ForwardTo("slidePiezas"); }
                SetTextBotones("< Datos", "< Conjuntos", "< Subconjuntos", "Piezas ^", "Materia Prima >");
                slideActual = 3;
            }
        }

        private void btnMateriaPrima_Click(object sender, EventArgs e)
        {
            if (slideActual != 4)
            {
                if (slideActual > 4) { slideControl.BackwardTo("slideMateriaPrima"); }
                else { slideControl.ForwardTo("slideMateriaPrima"); }
                SetTextBotones("< Datos", "< Conjuntos", "< Subconjuntos", "< Piezas", "Materia Prima ^");
                slideActual = 4;
            }
        }

        private void SetTextBotones(string datos, string conjuntos, string subconjuntos, string piezas, string materiaPrima)
        {
            btnDatos.Text = datos;
            btnConjuntos.Text = conjuntos;
            btnSubconjuntos.Text = subconjuntos;
            btnPiezas.Text = piezas;
            btnMateriaPrima.Text = materiaPrima;
        }

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbPartes.Parent = slideDatos;
            gbVer.Parent = slideDatos;
            gbCD.Parent = slideConjuntos;
            gbAC.Parent = slideConjuntos;
            gbCE.Parent = slideConjuntos;
            gbOC.Parent = slideConjuntos;
            gbSCD.Parent = slideSubconjuntos;
            gbASC.Parent = slideSubconjuntos;
            gbSCE.Parent = slideSubconjuntos;
            gbOSC.Parent = slideSubconjuntos;
            gbPD.Parent = slidePiezas;
            gbAP.Parent = slidePiezas;
            gbPE.Parent = slidePiezas;
            gbOP.Parent = slidePiezas;
            gbMPD.Parent = slideMateriaPrima;
            gbAMP.Parent = slideMateriaPrima;
            gbMPE.Parent = slideMateriaPrima;
            gbOMP.Parent = slideMateriaPrima;
            slideControl.AddSlide(slideDatos);
            slideControl.AddSlide(slideConjuntos);
            slideControl.AddSlide(slideSubconjuntos);
            slideControl.AddSlide(slidePiezas);
            slideControl.AddSlide(slideMateriaPrima);
            slideControl.AddSlide(slideArbol);
            slideControl.Selected = slideDatos;
        }

        #endregion SlideControl

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
                    chkActivo.Enabled = true;
                    chkActivo.Checked = false;
                    dtpFechaAlta.Enabled = true;
                    dtpFechaAlta.Value = BLL.DBBLL.GetFechaServidor();
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    //LIMPIAR GRILLAS PARTES-CE-SCE-PE-MPE - GONZALO
                    dsEstructura.LISTA_PARTES.Clear();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbAC.Enabled = true;
                    gbASC.Enabled = true;
                    gbAP.Enabled = true;
                    gbAMP.Enabled = true;
                    gbOC.Enabled = true;
                    gbOSC.Enabled = true;
                    gbOP.Enabled = true;
                    gbOMP.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SelectedIndex = -1;
                    chkActivo.Enabled = true;
                    chkActivo.Checked = false;
                    dtpFechaAlta.Enabled = true;
                    dtpFechaAlta.Value = BLL.DBBLL.GetFechaServidor();
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    //LIMPIAR GRILLAS PARTES-CE-SCE-PE-MPE - GONZALO
                    dsEstructura.LISTA_PARTES.Clear();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbAC.Enabled = true;
                    gbASC.Enabled = true;
                    gbAP.Enabled = true;
                    gbAMP.Enabled = true;
                    gbOC.Enabled = true;
                    gbOSC.Enabled = true;
                    gbOP.Enabled = true;
                    gbOMP.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbPlano.Enabled = false;
                    chkActivo.Enabled = false;
                    dtpFechaAlta.Enabled = false;
                    cbCocina.Enabled = false;
                    cbResponsable.Enabled = false;
                    dtpFechaModificacion.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    gbAC.Enabled = false;
                    gbASC.Enabled = false;
                    gbAP.Enabled = false;
                    gbAMP.Enabled = false;
                    gbOC.Enabled = false;
                    gbOSC.Enabled = false;
                    gbOP.Enabled = false;
                    gbOMP.Enabled = false;
                    estadoInterface = estadoUI.consultar;
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    cbPlano.Enabled = true;
                    chkActivo.Enabled = true;
                    dtpFechaAlta.Enabled = true;
                    cbCocina.Enabled = true;
                    cbResponsable.Enabled = true;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbAC.Enabled = true;
                    gbASC.Enabled = true;
                    gbAP.Enabled = true;
                    gbAMP.Enabled = true;
                    gbOC.Enabled = true;
                    gbOSC.Enabled = true;
                    gbOP.Enabled = true;
                    gbOMP.Enabled = true;
                    estadoInterface = estadoUI.modificar;
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
            if (sender.GetType().Equals(nudC.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }     
        
        private void SetGrillasCombosVistas()
        {
            //Obtenemos los datos iniciales necesarios: terminaciones, empleados, cocinas
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsCocina.TERMINACIONES);
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
            dgvEstructuras.Columns.Add("COC_COCINA", "Cocina");
            dgvEstructuras.Columns.Add("PNO_CODIGO", "Plano");
            dgvEstructuras.Columns.Add("E_CODIGO", "Responsable");
            dgvEstructuras.Columns.Add("ESTR_ACTIVO", "Activo");
            dgvEstructuras.Columns.Add("ESTR_FECHA_ALTA", "Fecha creación");
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstructuras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvEstructuras.Columns["ESTR_NOMBRE"].DataPropertyName = "ESTR_NOMBRE";
            dgvEstructuras.Columns["COC_COCINA"].DataPropertyName = "COC_COCINA";
            dgvEstructuras.Columns["PNO_CODIGO"].DataPropertyName = "PNO_CODIGO";
            dgvEstructuras.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvEstructuras.Columns["ESTR_ACTIVO"].DataPropertyName = "ESTR_ACTIVO";
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DataPropertyName = "ESTR_FECHA_ALTA";

            //Dataviews
            dvEstructuras = new DataView(dsEstructura.ESTRUCTURAS);
            dvEstructuras.Sort = "ESTR_NOMBRE ASC";
            dgvEstructuras.DataSource = dvEstructuras;
            dvCocinaBuscar = new DataView(dsCocina.COCINAS);
            dvCocinaBuscar.Sort = "COC_CODIGO_PRODUCTO ASC";
            dvResponsableBuscar = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsableBuscar.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            
            dvPlanoBuscar = new DataView(dsEstructura.PLANOS);
            dvPlanoBuscar.Sort = "PNO_NOMBRE ASC";

            //ComboBoxs
            cbCocinaBuscar.SetDatos(dvCocinaBuscar, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "--TODOS--", true);
            string[] displaymember = { "E_APELLIDO", "E_NOMBRE"};
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
            dgvPartes.Columns.Add("PAR_NOMBRE", "Nombre");
            dgvPartes.Columns.Add("PAR_TERMINACION", "Terminación");
            dgvPartes.Columns.Add("PAR_CANTIDAD", "Cantidad");
            dgvPartes.Columns.Add("PAR_UMED", "Unidad Medida");
            dgvPartes.Columns["PAR_TIPO"].DataPropertyName = "PAR_TIPO";
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
            dvPartes = new DataView(dsEstructura.LISTA_PARTES);
            //dvPartes.Sort = "PAR_NOMBRE ASC";

            //ComboBoxs
            cbCocina.SetDatos(dvCocina, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione", false);
            cbResponsable.SetDatos(dvResponsable, "E_CODIGO", displaymember, ", ", "Seleccione", false);
            cbPlano.SetDatos(dvPlano, "PNO_CODIGO", "PNO_NOMBRE", "Seleccione", false);
           
            #endregion Datos

            #region Conjuntos
            //Grilla Listado Conjuntos Disponibles
            dgvCD.AutoGenerateColumns = false;
            dgvCD.Columns.Add("CONJ_CODIGOPARTE", "Código");
            dgvCD.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvCD.Columns.Add("TE_NOMBRE", "Terminación");
            dgvCD.Columns.Add("CONJ_DESCRIPCION", "Descripción");
            dgvCD.Columns["CONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCD.Columns["CONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCD.Columns["TE_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCD.Columns["CONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCD.Columns["CONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvCD.Columns["CONJ_CODIGOPARTE"].DataPropertyName = "CONJ_CODIGOPARTE";
            dgvCD.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_NOMBRE";
            dgvCD.Columns["TE_NOMBRE"].DataPropertyName = "TE_CODIGO";
            dgvCD.Columns["CONJ_DESCRIPCION"].DataPropertyName = "CONJ_DESCRIPCION";

            //Grilla Listado Conjuntos en Estructura
            dgvCE.AutoGenerateColumns = false;
            dgvCE.Columns.Add("CONJ_CODIGOPARTE", "Código");
            dgvCE.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvCE.Columns.Add("TE_NOMBRE", "Terminación");
            dgvCE.Columns.Add("CXE_CANTIDAD", "Cantidad");
            dgvCE.Columns["CXE_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCE.Columns.Add("GRP_CODIGO", "Grupo");
            dgvCE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCE.Columns["CONJ_CODIGOPARTE"].DataPropertyName = "CONJ_CODIGO";
            dgvCE.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_CODIGO";
            dgvCE.Columns["TE_NOMBRE"].DataPropertyName = "CONJ_CODIGO";
            dgvCE.Columns["CXE_CANTIDAD"].DataPropertyName = "CXE_CANTIDAD";
            dgvCE.Columns["GRP_CODIGO"].DataPropertyName = "GRP_CODIGO";

            //Dataviews
            dvCD = new DataView(dsEstructura.CONJUNTOS);
            dvCD.Sort = "CONJ_NOMBRE ASC";
            dvCE = new DataView(dsEstructura.CONJUNTOSXESTRUCTURA);
            dgvCD.DataSource = dvCD;
            dgvCE.DataSource = dvCE;
            #endregion Conjuntos

            #region Subconjuntos
            //Grilla listado subconjuntos disponibles
            dgvSCD.AutoGenerateColumns = false;
            dgvSCD.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvSCD.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCD.Columns.Add("TE_NOMBRE", "Terminación");
            dgvSCD.Columns.Add("SCONJ_DESCRIPCION", "Descripción");
            dgvSCD.Columns["SCONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCD.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCD.Columns["TE_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCD.Columns["SCONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSCD.Columns["SCONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvSCD.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGOPARTE";
            dgvSCD.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_NOMBRE";
            dgvSCD.Columns["TE_NOMBRE"].DataPropertyName = "TE_CODIGO";
            dgvSCD.Columns["SCONJ_DESCRIPCION"].DataPropertyName = "SCONJ_DESCRIPCION";

            //Grilla listado de subconjuntos en estructura
            dgvSCE.AutoGenerateColumns = false;
            dgvSCE.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvSCE.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCE.Columns.Add("TE_NOMBRE", "Terminación");
            dgvSCE.Columns.Add("SCXE_CANTIDAD", "Cantidad");
            dgvSCE.Columns["SCXE_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSCE.Columns.Add("GRP_CODIGO", "Grupo");
            dgvSCE.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGO";
            dgvSCE.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_CODIGO";
            dgvSCE.Columns["TE_NOMBRE"].DataPropertyName = "SCONJ_CODIGO";
            dgvSCE.Columns["SCXE_CANTIDAD"].DataPropertyName = "SCXE_CANTIDAD";
            dgvSCE.Columns["GRP_CODIGO"].DataPropertyName = "GRP_CODIGO";

            //Dataviews
            dvSCD = new DataView(dsEstructura.SUBCONJUNTOS);
            dvSCD.Sort = "SCONJ_NOMBRE ASC";
            dvSCE = new DataView(dsEstructura.SUBCONJUNTOSXESTRUCTURA);
            dgvSCD.DataSource = dvSCD;
            dgvSCE.DataSource = dvSCE;

            #endregion Subconjuntos

            #region Piezas
            //Grilla listado de piezas disponibles
            dgvPD.AutoGenerateColumns = false;
            dgvPD.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPD.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPD.Columns.Add("TE_NOMBRE", "Terminación");
            dgvPD.Columns.Add("PZA_DESCRIPCION", "Descripción");
            dgvPD.Columns["PZA_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["TE_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["PZA_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPD.Columns["PZA_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvPD.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGOPARTE";
            dgvPD.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_NOMBRE";
            dgvPD.Columns["TE_NOMBRE"].DataPropertyName = "TE_CODIGO";
            dgvPD.Columns["PZA_DESCRIPCION"].DataPropertyName = "PZA_DESCRIPCION";

            //Grilla Listado piezas en Estructura
            dgvPE.AutoGenerateColumns = false;
            dgvPE.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPE.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPE.Columns.Add("TE_NOMBRE", "Terminación");
            dgvPE.Columns.Add("PXE_CANTIDAD", "Cantidad");
            dgvPE.Columns["PXE_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPE.Columns.Add("GRP_CODIGO", "Grupo");
            dgvPE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvPE.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGO";
            dgvPE.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_CODIGO";
            dgvPE.Columns["TE_NOMBRE"].DataPropertyName = "PZA_CODIGO";
            dgvPE.Columns["PXE_CANTIDAD"].DataPropertyName = "PXE_CANTIDAD";
            dgvPE.Columns["GRP_CODIGO"].DataPropertyName = "GRP_CODIGO";

            //Dataviews
            dvPD = new DataView(dsEstructura.PIEZAS);
            dvPD.Sort = "PZA_NOMBRE ASC";
            dvPE = new DataView(dsEstructura.PIEZASXESTRUCTURA);
            dgvPD.DataSource = dvPD;
            dgvPE.DataSource = dvPE;
            #endregion Piezas

            #region Materia Prima
            //Grilla listado materia primas disponibles
            dgvMPD.AutoGenerateColumns = false;
            dgvMPD.Columns.Add("MP_NOMBRE", "Nombre");
            dgvMPD.Columns.Add("UMED_NOMBRE", "Unidad Medida");
            dgvMPD.Columns.Add("MP_DESCRIPCION", "Descripción");
            dgvMPD.Columns["MP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPD.Columns["UMED_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPD.Columns["MP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMPD.Columns["MP_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvMPD.Columns["MP_NOMBRE"].DataPropertyName = "MP_NOMBRE";
            dgvMPD.Columns["UMED_NOMBRE"].DataPropertyName = "UMED_CODIGO";
            dgvMPD.Columns["MP_DESCRIPCION"].DataPropertyName = "MP_DESCRIPCION";

            //Grilla listado materias primas en estructura
            dgvMPE.AutoGenerateColumns = false;
            dgvMPE.Columns.Add("MP_NOMBRE", "Nombre");
            dgvMPE.Columns.Add("UMED_NOMBRE", "Unidad Medida");
            dgvMPE.Columns.Add("MPXE_CANTIDAD", "Cantidad");
            dgvMPE.Columns["MPXE_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMPE.Columns.Add("GRP_CODIGO", "Grupo");
            dgvMPE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvMPE.Columns["MP_NOMBRE"].DataPropertyName = "MP_CODIGO";
            dgvMPE.Columns["UMED_NOMBRE"].DataPropertyName = "MP_CODIGO";
            dgvMPE.Columns["MPXE_CANTIDAD"].DataPropertyName = "MPXE_CANTIDAD";
            dgvMPE.Columns["GRP_CODIGO"].DataPropertyName = "GRP_CODIGO";

            //Dataviews
            dvMPD = new DataView(dsEstructura.MATERIAS_PRIMAS);
            dvMPD.Sort = "MP_NOMBRE ASC";
            dvMPE = new DataView(dsEstructura.MATERIASPRIMASXESTRUCTURA);
            dgvMPD.DataSource = dvMPD;
            dgvMPE.DataSource = dvMPE;
            #endregion

            #region Árbol
            #endregion Árbol
        }

        private void CargarListaPartes()
        {
            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in dsEstructura.CONJUNTOSXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Conjunto";
                rowParte.PAR_NOMBRE = row.CONJUNTOSRow.CONJ_NOMBRE;
                rowParte.PAR_TERMINACION = dsCocina.TERMINACIONES.FindByTE_CODIGO(row.CONJUNTOSRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.CXE_CANTIDAD.ToString();
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in dsEstructura.SUBCONJUNTOSXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Subconjunto";
                rowParte.PAR_NOMBRE = row.SUBCONJUNTOSRow.SCONJ_NOMBRE;
                rowParte.PAR_TERMINACION = dsCocina.TERMINACIONES.FindByTE_CODIGO(row.SUBCONJUNTOSRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.SCXE_CANTIDAD.ToString();
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in dsEstructura.PIEZASXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Pieza";
                rowParte.PAR_NOMBRE = row.PIEZASRow.PZA_NOMBRE;
                rowParte.PAR_TERMINACION = dsCocina.TERMINACIONES.FindByTE_CODIGO(row.PIEZASRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.PXE_CANTIDAD.ToString();
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in dsEstructura.MATERIASPRIMASXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Materia Prima";
                rowParte.PAR_NOMBRE = row.MATERIAS_PRIMASRow.MP_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.MPXE_CANTIDAD.ToString();
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }
        }

        private void frmEstructuraCocina_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled) { txtNombreBuscar.Focus(); }
        }

        #endregion Servicios

        #region Cell_Formatting

        private void dgvEstructuras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvEstructuras.Columns[e.ColumnIndex].Name)
                {
                    case "COC_COCINA":
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
        
        private void dgvCD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvCD.Columns[e.ColumnIndex].Name == "TE_NOMBRE")
                {
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void dgvCE_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {            
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigo;
                switch (dgvCE.Columns[e.ColumnIndex].Name)
                {
                    case "CONJ_CODIGOPARTE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).CONJ_CODIGOPARTE;
                        e.Value = nombre;
                        break;
                    case "CONJ_NOMBRE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).CONJ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TE_NOMBRE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).TERMINACIONESRow.TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "GRP_CODIGO":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.GRUPOS_ESTRUCTURA.FindByGRP_CODIGO(codigo).GRP_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvSCD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvSCD.Columns[e.ColumnIndex].Name == "TE_NOMBRE")
                {
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void dgvSCE_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigo;
                switch (dgvSCE.Columns[e.ColumnIndex].Name)
                {
                    case "SCONJ_CODIGOPARTE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigo).SCONJ_CODIGOPARTE;
                        e.Value = nombre;
                        break;
                    case "SCONJ_NOMBRE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigo).SCONJ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TE_NOMBRE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigo).TERMINACIONESRow.TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "GRP_CODIGO":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.GRUPOS_ESTRUCTURA.FindByGRP_CODIGO(codigo).GRP_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvPD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvPD.Columns[e.ColumnIndex].Name == "TE_NOMBRE")
                {
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void dgvPE_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string nombre;
            int codigo;
            switch (dgvPE.Columns[e.ColumnIndex].Name)
            {
                case "PZA_CODIGOPARTE":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).PZA_CODIGOPARTE;
                    e.Value = nombre;
                    break;
                case "PZA_NOMBRE":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).PZA_NOMBRE;
                    e.Value = nombre;
                    break;
                case "TE_NOMBRE":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).TERMINACIONESRow.TE_NOMBRE;
                    e.Value = nombre;
                    break;
                case "GRP_CODIGO":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.GRUPOS_ESTRUCTURA.FindByGRP_CODIGO(codigo).GRP_NOMBRE;
                    e.Value = nombre;
                    break;
                default:
                    break;
            }
        }        

        private void dgvMPD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvMPD.Columns[e.ColumnIndex].Name == "UMED_NOMBRE")
                {
                    nombre = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value.ToString())).UMED_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void dgvMPE_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigo;
                switch (dgvMPE.Columns[e.ColumnIndex].Name)
                {
                    case "MP_NOMBRE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.MATERIAS_PRIMAS.FindByMP_CODIGO(codigo).MP_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "UMED_NOMBRE":
                        codigo = Convert.ToInt32(dsEstructura.MATERIAS_PRIMAS.FindByMP_CODIGO(Convert.ToInt32(e.Value.ToString())).UMED_CODIGO);
                        nombre = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(codigo).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "GRP_CODIGO":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.GRUPOS_ESTRUCTURA.FindByGRP_CODIGO(codigo).GRP_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        private void btnAC_Click(object sender, EventArgs e)
        {
            if (dgvCD.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudC.Value > 0)
            {
                bool agregarConjunto; //variable que indica si se debe agregar el conjunto al listado
                //Obtenemos el código del conjunto según sea nuevo o modificado, lo hacemos acá porque lo vamos a usar mucho
                int codigoEstructura;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
                else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }
                //Obtenemos el código del conjunto, también lo vamos a usar mucho
                int codigoConjunto = Convert.ToInt32(dvCD[dgvCD.SelectedRows[0].Index]["conj_codigo"]);

                //Primero vemos si la estructura tiene algún conjunto cargado, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene la estructura seleccionado                
                if (dvCE.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar el mismo conjunto haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "estr_codigo = " + codigoEstructura + " AND conj_codigo = " + codigoConjunto;
                    Data.dsEstructura.CONJUNTOSXESTRUCTURARow[] rows =
                        (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("La estructura ya posee el subconjunto seleccionado. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].CXE_CANTIDAD += nudC.Value;
                            nudC.Value = 0;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarConjunto = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarConjunto = true;
                    }
                }
                else
                {
                    //No tiene ningún conjunto agregado, marcamos que debe agregarse
                    agregarConjunto = true;
                }

                //Ahora comprobamos si debe agregarse el conjunto o no
                if (agregarConjunto)
                {
                    Data.dsEstructura.CONJUNTOSXESTRUCTURARow row = dsEstructura.CONJUNTOSXESTRUCTURA.NewCONJUNTOSXESTRUCTURARow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.CXE_CODIGO = cxe--; //-- para que se vaya autodecrementando en cada inserción
                    row.ESTR_CODIGO = codigoEstructura;
                    row.CONJ_CODIGO = codigoConjunto;
                    row.CXE_CANTIDAD = nudC.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.CONJUNTOSXESTRUCTURA.AddCONJUNTOSXESTRUCTURARow(row);
                    nudC.Value = 0;
                }
                nudC.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista y asignarle un valor mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEC_Click(object sender, EventArgs e)
        {
            if(dgvCE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoC = Convert.ToInt32(dvCE[dgvCE.SelectedRows[0].Index]["cxe_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoC).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSC_Click(object sender, EventArgs e)
        {
            if (dgvCE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoCXE = Convert.ToInt32(dvCE[dgvCE.SelectedRows[0].Index]["cxe_codigo"]);
                dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CXE_CANTIDAD += 1;
            }
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRC_Click(object sender, EventArgs e)
        {
            if (dgvCE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoCXE = Convert.ToInt32(dvCE[dgvCE.SelectedRows[0].Index]["cxe_codigo"]);
                dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CXE_CANTIDAD -= 1;
            }
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        
    }
}
