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
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(txtNombreBuscar.Text, cbTipoBuscar.GetSelectedValue(), cbSectorBuscar.GetSelectedValue(), cbActivoBuscar.GetSelectedValueInt(), dsCentroTrabajo.CENTROS_TRABAJOS);
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
            txtHorasNormal.Text = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASTRABAJONORMAL.ToString();
            txtHorasExtendido.Text = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASTRABAJOEXTENDIDO.ToString();
            cbActivo.SetSelectedValue(Convert.ToInt32(dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_ACTIVO));
            //Turnos de trabajo - gonzalo
            nudCapacidadCiclo.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_CAPACIDADCICLO;
            nudTiempoCiclo.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_HORASCICLO;
            nudTiempoAntes.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIEMPOANTES;
            nudTiempoDespues.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_TIEMPODESPUES;
            nudEficiencia.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_EFICIENCIA;
            nudCostoCiclo.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_COSTOCICLO;
            nudCostoHora.Value = dsCentroTrabajo.CENTROS_TRABAJOS.FindByCTO_CODIGO(codigoCentro).CTO_COSTOHORA;
        }

        #endregion

        #region Datos

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
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
                    NumericUpDown[] nuds1 = { nudCapacidadCiclo, nudTiempoCiclo, nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds1, true, true);
                    txtNombre.Text = String.Empty;
                    cbTipo.SetTexto("Seleccione");
                    cbSector.SetTexto("Seleccione");
                    txtDescripcion.Text = String.Empty;
                    cbActivo.SetTexto("Seleccione");
                    //deseleccionar todos los turnos - gonzalo
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
                    NumericUpDown[] nuds2 = { nudCapacidadCiclo, nudTiempoCiclo, nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds2, true, true);
                    txtNombre.Text = String.Empty;
                    cbTipo.SetTexto("Seleccione");
                    cbSector.SetTexto("Seleccione");
                    txtDescripcion.Text = String.Empty;
                    cbActivo.SetTexto("Seleccione");
                    //deseleccionar todos los turnos - gonzalo
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
                    NumericUpDown[] nuds3 = { nudCapacidadCiclo, nudTiempoCiclo, nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
                    SetearNuds(nuds3, false, false);
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
                    NumericUpDown[] nuds = { nudCapacidadCiclo, nudTiempoCiclo, nudTiempoAntes, nudTiempoDespues, nudEficiencia, nudCostoHora, nudCostoCiclo };
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

        #endregion

    }
}
