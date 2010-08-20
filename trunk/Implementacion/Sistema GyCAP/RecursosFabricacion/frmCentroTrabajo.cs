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
    public partial class frmCentroTrabajo : Form
    {
        private static frmCentroTrabajo _frmCentroTrabajo = null;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        Data.dsCentroTrabajo dsCentroTrabajo = new GyCAP.Data.dsCentroTrabajo();
        DataView dvCentrosTrabajo, dvSectorBuscar, dvSector, dvTurnoTrabajo;
        int txct = -1;

        #region Inicio

        public frmCentroTrabajo()
        {
            InitializeComponent();
            InicializarDatos();
        }        

        public static frmCentroTrabajo Instancia
        {
            get
            {
                if (_frmCentroTrabajo == null || _frmCentroTrabajo.IsDisposed)
                {
                    _frmCentroTrabajo = new frmCentroTrabajo();
                }
                else
                {
                    _frmCentroTrabajo.BringToFront();
                }
                return _frmCentroTrabajo;
            }
            set
            {
                _frmCentroTrabajo = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }
        #endregion

        #region Botones Menu

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
            //Controlamos que esté seleccionado algo
            /*if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el Modelo de Cocina seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //obtenemos el código
                        int codigo = Convert.ToInt32(dvModeloCocina[dgvLista.SelectedRows[0].Index]["mod_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.ModeloCocinaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsModeloCocina.MODELOS_COCINAS.FindByMOD_CODIGO(codigo).Delete();
                        dsModeloCocina.MODELOS_COCINAS.AcceptChanges();
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
            }*/
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsCentroTrabajo.CENTROS_TRABAJOS.Clear();
                dsCentroTrabajo.TURNOSXCENTROTRABAJO.Clear();                
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(txtNombreBuscar.Text, cbTipoBuscar.GetSelectedValue(), cbSectorBuscar.GetSelectedValue(), cbActivoBuscar.GetSelectedValueInt(), dsCentroTrabajo);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvCentrosTrabajo.Table = dsCentroTrabajo.CENTROS_TRABAJOS;
                if (dsCentroTrabajo.CENTROS_TRABAJOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Centros de Trabajo con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Centros de Trabajo - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "SEC_CODIGO":
                        nombre = dsCentroTrabajo.SECTORES.FindBySEC_CODIGO(Convert.ToInt32(e.Value.ToString())).SEC_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CTO_TIPO":
                        if (Convert.ToInt32(e.Value.ToString()) == BLL.CentroTrabajoBLL.TipoMaquina)
                        { nombre = "Máquina"; }
                        else if (Convert.ToInt32(e.Value.ToString()) == BLL.CentroTrabajoBLL.TipoHombre)
                        { nombre = "Hombre"; }
                        e.Value = nombre;
                        break;
                    case "CTO_ACTIVO":
                        if (Convert.ToInt32(e.Value.ToString()) == BLL.CentroTrabajoBLL.CentroActivo)
                        { nombre = "Activo"; }
                        else if (Convert.ToInt32(e.Value.ToString()) == BLL.CentroTrabajoBLL.CentroInactivo)
                        { nombre = "Inactivo"; }
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoCentro = Convert.ToInt32(dvCentrosTrabajo[e.RowIndex]["cto_codigo"]);
            txtNombre.Text = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_NOMBRE;
            cbTipo.SetSelectedValue(Convert.ToInt32(dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIPO));
            cbSector.SetSelectedValue(Convert.ToInt32(dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).SEC_CODIGO));
            txtDescripcion.Text = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_DESCRIPCION;
            nudHorasNormal.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASTRABAJONORMAL;
            nudHorasExtendido.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASTRABAJOEXTENDIDO;
            cbActivo.SetSelectedValue(Convert.ToInt32(dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_ACTIVO));
            nudCapacidadCiclo.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_CAPACIDADCICLO;
            nudTiempoCiclo.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASCICLO;
            nudTiempoAntes.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIEMPOANTES;
            nudTiempoDespues.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIEMPODESPUES;
            nudEficiencia.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_EFICIENCIA;
            nudCostoCiclo.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_COSTOCICLO;
            nudCostoHora.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_COSTOHORA;

            //Turnos
            foreach (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow row in (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow[])dsCentroTrabajo.TURNOSXCENTROTRABAJO.Select("CTO_CODIGO = " + codigoCentro))
            {
                lvTurnos.Items.Find(row.TUR_CODIGO.ToString(), false)[0].Checked = true;
            }
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string datosFaltantes = string.Empty;
            //Revisamos que completó los datos
            if (txtNombre.Text == string.Empty) { datosFaltantes += "* Nombre\n"; }
            if (cbTipo.GetSelectedIndex() == -1) { datosFaltantes += "* Tipo\n"; }
            if (nudHorasNormal.Value == 0) { datosFaltantes += "* Horas de trabajo normal\n"; }
            if (nudHorasExtendido.Value == 0) { datosFaltantes += "* Horas de trabajo extendido\n"; }
            if (cbActivo.GetSelectedIndex() == -1) { datosFaltantes += "* Estado\n"; }
            if (cbSector.GetSelectedIndex() == -1) { datosFaltantes += "* Sector de trabajo\n"; }
            //revisar demas datos obligatorios - gonzalo
            if (datosFaltantes == string.Empty)
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Creamos el centro de trabajo
                        Data.dsCentroTrabajo.CENTROS_TRABAJOSRow rowCentro = dsCentroTrabajo.CENTROS_TRABAJOS.NewCENTROS_TRABAJOSRow();
                        rowCentro.BeginEdit();
                        rowCentro.CTO_CODIGO = -1;
                        rowCentro.SEC_CODIGO = cbSector.GetSelectedValueInt();
                        rowCentro.CTO_ACTIVO = cbActivo.GetSelectedValueInt();
                        rowCentro.CTO_CAPACIDADCICLO = nudCapacidadCiclo.Value;
                        rowCentro.CTO_COSTOCICLO = nudCostoCiclo.Value;
                        rowCentro.CTO_COSTOHORA = nudCostoHora.Value;
                        rowCentro.CTO_DESCRIPCION = txtDescripcion.Text;
                        rowCentro.CTO_EFICIENCIA = nudEficiencia.Value;
                        rowCentro.CTO_HORASCICLO = nudTiempoCiclo.Value;
                        rowCentro.CTO_HORASTRABAJOEXTENDIDO = nudHorasExtendido.Value;
                        rowCentro.CTO_HORASTRABAJONORMAL = nudHorasNormal.Value;
                        rowCentro.CTO_NOMBRE = txtNombre.Text;
                        rowCentro.CTO_TIEMPOANTES = nudTiempoAntes.Value;
                        rowCentro.CTO_TIEMPODESPUES = nudTiempoDespues.Value;
                        rowCentro.CTO_TIPO = cbTipo.GetSelectedValueInt();
                        rowCentro.EndEdit();
                        dsCentroTrabajo.CENTROS_TRABAJOS.AddCENTROS_TRABAJOSRow(rowCentro);

                        //Creamos los turnosxcentro
                        foreach (ListViewItem item in lvTurnos.CheckedItems)
                        {
                            Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow row = dsCentroTrabajo.TURNOSXCENTROTRABAJO.NewTURNOSXCENTROTRABAJORow();
                            row.BeginEdit();
                            row.TXCT_CODIGO = txct--;
                            row.TUR_CODIGO = Convert.ToDecimal(item.Name);
                            row.CTO_CODIGO = -1;
                            row.EndEdit();
                            dsCentroTrabajo.TURNOSXCENTROTRABAJO.AddTURNOSXCENTROTRABAJORow(row);
                        }

                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad BLL y DAL sepan cuales insertar
                        BLL.CentroTrabajoBLL.Insertar(dsCentroTrabajo);
                        //Ahora si aceptamos los cambios
                        dsCentroTrabajo.CENTROS_TRABAJOS.AcceptChanges();
                        dsCentroTrabajo.TURNOSXCENTROTRABAJO.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz

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
                        //Ya existe descartamos los cambios
                        dsCentroTrabajo.CENTROS_TRABAJOS.RejectChanges();
                        dsCentroTrabajo.TURNOSXCENTROTRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios
                        dsCentroTrabajo.CENTROS_TRABAJOS.RejectChanges();
                        dsCentroTrabajo.TURNOSXCENTROTRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    int codigoCentro = Convert.ToInt32(dvCentrosTrabajo[dgvLista.SelectedRows[0].Index]["cto_codigo"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).SEC_CODIGO = cbSector.GetSelectedValueInt();
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_ACTIVO = cbActivo.GetSelectedValueInt();
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_CAPACIDADCICLO = nudCapacidadCiclo.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_COSTOCICLO = nudCostoCiclo.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_COSTOHORA = nudCostoHora.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_DESCRIPCION = txtDescripcion.Text;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_EFICIENCIA = nudEficiencia.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASCICLO = nudTiempoCiclo.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASTRABAJOEXTENDIDO = nudHorasExtendido.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASTRABAJONORMAL = nudHorasNormal.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_NOMBRE = txtNombre.Text;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIEMPOANTES = nudTiempoAntes.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIEMPODESPUES = nudTiempoDespues.Value;
                    dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIPO = cbTipo.GetSelectedValueInt();
                    //los turnos
                    foreach (Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow row in dsCentroTrabajo.TURNOSXCENTROTRABAJO)
                    {
                        row.Delete();
                    }
                    foreach (ListViewItem item in lvTurnos.CheckedItems)
                    {
                        Data.dsCentroTrabajo.TURNOSXCENTROTRABAJORow row = dsCentroTrabajo.TURNOSXCENTROTRABAJO.NewTURNOSXCENTROTRABAJORow();
                        row.BeginEdit();
                        row.TXCT_CODIGO = txct--;
                        row.TUR_CODIGO = Convert.ToDecimal(item.Name);
                        row.CTO_CODIGO = codigoCentro;
                        row.EndEdit();
                        dsCentroTrabajo.TURNOSXCENTROTRABAJO.AddTURNOSXCENTROTRABAJORow(row);
                    }
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.CentroTrabajoBLL.Actualizar(dsCentroTrabajo);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsCentroTrabajo.CENTROS_TRABAJOS.AcceptChanges();
                        dsCentroTrabajo.TURNOSXCENTROTRABAJO.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de conjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsCentroTrabajo.CENTROS_TRABAJOS.RejectChanges();
                        dsCentroTrabajo.TURNOSXCENTROTRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvLista.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            dsCentroTrabajo.CENTROS_TRABAJOS.RejectChanges();
            dsCentroTrabajo.TURNOSXCENTROTRABAJO.RejectChanges();
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            NumericUpDown[] nuds;
            switch (estado)
            {                
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsCentroTrabajo.CENTROS_TRABAJOS.Rows.Count == 0)
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
                    tcCentroTrabajo.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    cbTipo.Enabled = true;
                    cbSector.Enabled = true;
                    txtDescripcion.ReadOnly = false;                    
                    cbActivo.Enabled = true;
                    lvTurnos.Enabled = true;
                    nuds = new NumericUpDown[] { nudHorasNormal, nudHorasExtendido, nudCapacidadCiclo, nudTiempoCiclo, 
                                                nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds, true, true);
                    txtNombre.Text = String.Empty;
                    cbTipo.SetTexto("Seleccione");
                    cbSector.SetTexto("Seleccione");
                    txtDescripcion.Text = String.Empty;
                    cbActivo.SetTexto("Seleccione");
                    DeseleccionarTurnos();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcCentroTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    cbTipo.Enabled = true;
                    cbSector.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    cbActivo.Enabled = true;
                    lvTurnos.Enabled = true;
                    nuds = new NumericUpDown[] { nudHorasNormal, nudHorasExtendido, nudCapacidadCiclo, nudTiempoCiclo, 
                                                nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds, true, true);
                    txtNombre.Text = String.Empty;
                    cbTipo.SetTexto("Seleccione");
                    cbSector.SetTexto("Seleccione");
                    txtDescripcion.Text = String.Empty;
                    cbActivo.SetTexto("Seleccione");
                    DeseleccionarTurnos();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcCentroTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbTipo.Enabled = false;
                    cbSector.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    cbActivo.Enabled = false;
                    lvTurnos.Enabled = false;
                    nuds = new NumericUpDown[] { nudHorasNormal, nudHorasExtendido, nudCapacidadCiclo, nudTiempoCiclo, 
                                                nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds, false, false);
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcCentroTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    cbTipo.Enabled = true;
                    cbSector.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    cbActivo.Enabled = true;
                    lvTurnos.Enabled = true;
                    nuds = new NumericUpDown[] { nudHorasNormal, nudHorasExtendido, nudCapacidadCiclo, nudTiempoCiclo, 
                                                nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds, true, false);
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcCentroTrabajo.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        private void SetearNuds(NumericUpDown[] nuds, bool activar, bool reiniciar)
        {
            foreach (NumericUpDown nud in nuds)
            {
                nud.Enabled = activar;
                if (reiniciar) { nud.Value = 0; }
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCostoHora.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void InicializarDatos()
        {
            //Seteamos la grilla de la búsqueda
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("CTO_NOMBRE", "Nombre");
            dgvLista.Columns.Add("SEC_CODIGO", "Sector");
            dgvLista.Columns.Add("CTO_TIPO", "Tipo");
            dgvLista.Columns.Add("CTO_ACTIVO", "Activo");
            dgvLista.Columns.Add("CTO_DESCRIPCION", "Descripción");
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvLista.Columns["CTO_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["CTO_NOMBRE"].DataPropertyName = "CTO_NOMBRE";
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["CTO_TIPO"].DataPropertyName = "CTO_TIPO";
            dgvLista.Columns["CTO_ACTIVO"].DataPropertyName = "CTO_ACTIVO";
            dgvLista.Columns["CTO_DESCRIPCION"].DataPropertyName = "CTO_DESCRIPCION";

            //Obtenemos los datos de la DB            
            try
            {
                BLL.TurnoTrabajoBLL.ObtenerTurnos(dsCentroTrabajo.TURNOS_TRABAJO);
                BLL.SectorBLL.ObtenerTodos(dsCentroTrabajo.SECTORES);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CargarTurnos();

            //Dataviews y Combos
            dvCentrosTrabajo = new DataView(dsCentroTrabajo.CENTROS_TRABAJOS);
            dvCentrosTrabajo.Sort = "CTO_NOMBRE ASC";
            dgvLista.DataSource = dvCentrosTrabajo;
            dvSectorBuscar = new DataView(dsCentroTrabajo.SECTORES);
            dvSectorBuscar.Sort = "SEC_NOMBRE ASC";
            dvSector = new DataView(dsCentroTrabajo.SECTORES);
            dvSector.Sort = "SEC_NOMBRE ASC";
            cbSectorBuscar.SetDatos(dvSectorBuscar, "SEC_CODIGO", "SEC_NOMBRE", "--TODOS--", true);
            cbSector.SetDatos(dvSector, "SEC_CODIGO", "SEC_NOMBRE", "Seleccione", false);
            string[] nombres;
            int[] valores;
            nombres = new string[] { "Humano", "Máquina" };
            valores = new int[] { BLL.CentroTrabajoBLL.TipoHombre, BLL.CentroTrabajoBLL.TipoMaquina };
            cbTipoBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            cbTipo.SetDatos(nombres, valores, "Seleccione", false);
            nombres = new string[] { "Activo", "Inactivo"};
            valores = new int[] { BLL.CentroTrabajoBLL.CentroActivo, BLL.CentroTrabajoBLL.CentroInactivo };
            cbActivoBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            cbActivo.SetDatos(nombres, valores, "Seleccione", false);
            dvTurnoTrabajo = new DataView(dsCentroTrabajo.TURNOS_TRABAJO);
            dvTurnoTrabajo.Sort = "TUR_NOMBRE ASC";            
        }

        private void CargarTurnos()
        {
            lvTurnos.Columns.Clear();
            lvTurnos.Items.Clear();
            ListViewItem item;
            lvTurnos.Columns.Add("Turno", 140);
            lvTurnos.Columns.Add("Inicio", 50, HorizontalAlignment.Right);
            lvTurnos.Columns.Add("Fin", 50, HorizontalAlignment.Right);

            foreach (Data.dsCentroTrabajo.TURNOS_TRABAJORow turno in dsCentroTrabajo.TURNOS_TRABAJO)
            {
                item = new ListViewItem();
                item.Name = turno.TUR_CODIGO.ToString();
                item.Text = turno.TUR_NOMBRE;
                item.SubItems.Add(turno.TUR_HORAINICIO.ToString().Replace(",", ":") + "h");
                item.SubItems.Add(turno.TUR_HORAFIN.ToString().Replace(",", ":") + "h");
                lvTurnos.Items.Add(item);
            }
        }

        private void DeseleccionarTurnos()
        {
            foreach (ListViewItem item in lvTurnos.CheckedItems)
            {
                item.Checked = false;
            }
        }

        private void SeleccionarTurnos(int[] codigo)
        {
            foreach (int item in codigo)
            {
                lvTurnos.Items.Find(item.ToString(), false)[0].Checked = true;
            }
        }

        #endregion


    }
}
