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
        private int cxe = -1, scxe = -1, pxe = -1, mpxe = -1; //Variables para el manejo de inserciones en los dataset con códigos unique

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
                BLL.EstructuraBLL.ObtenerEstructuras(txtNombreBuscar.Text, cbPlanoBuscar.SelectedValue, dtpFechaAltaBuscar.Value.ToShortDateString(), cbCocinaBuscar.SelectedValue, cbResponsableBuscar.SelectedValue, cbActivoBuscar.SelectedItem, dsEstructura);
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
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
                    dtpFechaAlta.Value = DateTime.Today.Date;
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    //LIMPIAR GRILLAS PARTES-CE-SCE-PE-MPE - GONZALO
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
                    dtpFechaAlta.Value = DateTime.Today.Date;
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    //LIMPIAR GRILLAS PARTES-CE-SCE-PE-MPE - GONZALO
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
        
        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void SetGrillasCombosVistas()
        {
            //Obtenemos las terminaciones
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsCocina.TERMINACIONES);
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
            cbCocinaBuscar.DataSource = dvCocinaBuscar;
            cbCocinaBuscar.DisplayMember = "COC_CODIGO_PRODUCTO";
            cbCocinaBuscar.ValueMember = "COC_CODIGO";
            cbCocinaBuscar.SelectedIndex = -1;
            cbResponsableBuscar.DataSource = dvResponsableBuscar;
            cbResponsableBuscar.DisplayMember = "E_APELLIDO" + ", " + "E_NOMBRE";
            cbResponsableBuscar.ValueMember = "E_CODIGO";
            cbResponsableBuscar.SelectedIndex = -1;
            cbPlanoBuscar.DataSource = dvPlanoBuscar;
            cbPlanoBuscar.DisplayMember = "PNO_NOMBRE";
            cbPlanoBuscar.ValueMember = "PNO_CODIGO";
            cbPlanoBuscar.SelectedIndex = -1;
            cbActivoBuscar.Items.Add("SI");
            cbActivoBuscar.Items.Add("NO");
            cbActivoBuscar.SelectedIndex = -1;

            #endregion Buscar

            #region Datos
            //Grilla Listado Partes
            dgvPartes.AutoGenerateColumns = false;
            dgvPartes.Columns.Add("PAR_TIPO", "Nombre");
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
            cbCocina.DataSource = dvCocina;
            cbCocina.DisplayMember = "COC_CODIGO_PRODUCTO";
            cbCocina.ValueMember = "COC_CODIGO";
            cbCocina.SelectedIndex = -1;
            cbResponsable.DataSource = dvResponsable;
            cbResponsable.DisplayMember = "E_APELLIDO" + ", " + "E_NOMBRE";
            cbResponsable.ValueMember = "E_CODIGO";
            cbResponsable.SelectedIndex = -1;
            cbPlano.DataSource = dvPlano;
            cbPlano.DisplayMember = "PNO_NOMBRE";
            cbPlano.ValueMember = "PNO_CODIGO";
            cbPlano.SelectedIndex = -1;
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
    }
}
