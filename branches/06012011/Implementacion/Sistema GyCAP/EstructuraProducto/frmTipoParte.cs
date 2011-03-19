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
    public partial class frmTipoParte : Form
    {
        private static frmTipoParte _frmTipoParte = null;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        Data.dsEstructuraProducto dsTipoParte = new GyCAP.Data.dsEstructuraProducto();
        DataView dvTipoParte;

        #region Inicio
        public frmTipoParte()
        {
            InitializeComponent();

            SetearDatosIniciales();
            SetInterface(estadoUI.inicio);
        }

        public static frmTipoParte Instancia
        {
            get
            {
                if (_frmTipoParte == null || _frmTipoParte.IsDisposed)
                {
                    _frmTipoParte = new frmTipoParte();
                }
                else
                {
                    _frmTipoParte.BringToFront();
                }
                return _frmTipoParte;
            }
            set
            {
                _frmTipoParte = value;
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
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Tipo de parte", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, "Tipos de Partes");
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //obtenemos el código
                        int codigo = Convert.ToInt32(dvTipoParte[dgvLista.SelectedRows[0].Index]["tpar_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.TipoParteBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).Delete();
                        dsTipoParte.TIPOS_PARTES.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, "Tipos de Partes");
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, "Tipos de Partes", GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo de parte", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, "Tipos de Partes");
            }
        }

        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsTipoParte.TIPOS_PARTES.Clear();
                BLL.TipoParteBLL.ObtenerTiposPartes(txtNombreBuscar.Text, chkFantasmaBuscar.Checked, chkOTSBuscar.Checked, chkEnsambladoBuscar.Checked, chkAdquiridoBuscar.Checked, chkTerminadoBuscar.Checked, dsTipoParte.TIPOS_PARTES);
                if (dsTipoParte.TIPOS_PARTES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Tipos de partes", "Tipos de Partes");
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, "Tipos de Partes", GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Datos

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsTipoParte.TIPOS_PARTES.RejectChanges();
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsTipoParte.TIPOS_PARTES.Rows.Count == 0)
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
                    tcTipoParte.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    chkAdquirido.Checked = false;
                    chkFantasma.Checked = false;
                    chkOTS.Checked = false;
                    chkTerminado.Checked = false;
                    chkEnsamblado.Checked = false;
                    chkEnsamblado.Enabled = true;
                    chkAdquirido.Enabled = true;
                    chkFantasma.Enabled = true;
                    chkOTS.Enabled = true;
                    chkTerminado.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcTipoParte.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    chkAdquirido.Checked = false;
                    chkFantasma.Checked = false;
                    chkOTS.Checked = false;
                    chkTerminado.Checked = false;
                    chkEnsamblado.Checked = false;
                    chkEnsamblado.Enabled = true;
                    chkAdquirido.Enabled = true;
                    chkFantasma.Enabled = true;
                    chkOTS.Enabled = true;
                    chkTerminado.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcTipoParte.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    chkEnsamblado.Enabled = false;
                    chkAdquirido.Enabled = false;
                    chkFantasma.Enabled = false;
                    chkOTS.Enabled = false;
                    chkTerminado.Enabled = false;                    
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcTipoParte.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    chkEnsamblado.Enabled = true;
                    chkAdquirido.Enabled = true;
                    chkFantasma.Enabled = true;
                    chkOTS.Enabled = true;
                    chkTerminado.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcTipoParte.SelectedTab = tpDatos;
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
                BLL.TipoParteBLL.ObtenerTodos(dsTipoParte.TIPOS_PARTES);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, "Tipos de Partes", GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio);
            }

            //Grilla 
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("TPAR_NOMBRE", "Nombre");
            dgvLista.Columns.Add("TPAR_PRODUCTOTERMINADO", "Producto terminado");
            dgvLista.Columns.Add("TPAR_FANTASMA", "Fantasma");
            dgvLista.Columns.Add("TPAR_ORDENTRABAJO", "Generar orden");
            dgvLista.Columns.Add("TPAR_ENSAMBLADO", "Ensamblado");
            dgvLista.Columns.Add("TPAR_ADQUIRIDO", "Adquirido");
            dgvLista.Columns["TPAR_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_PRODUCTOTERMINADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_FANTASMA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_ORDENTRABAJO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_ENSAMBLADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_ADQUIRIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TPAR_NOMBRE"].DataPropertyName = "TPAR_NOMBRE";
            dgvLista.Columns["TPAR_PRODUCTOTERMINADO"].DataPropertyName = "TPAR_PRODUCTOTERMINADO";
            dgvLista.Columns["TPAR_FANTASMA"].DataPropertyName = "TPAR_FANTASMA";
            dgvLista.Columns["TPAR_ORDENTRABAJO"].DataPropertyName = "TPAR_ORDENTRABAJO";
            dgvLista.Columns["TPAR_ENSAMBLADO"].DataPropertyName = "TPAR_ENSAMBLADO";
            dgvLista.Columns["TPAR_ADQUIRIDO"].DataPropertyName = "TPAR_ADQUIRIDO";
            dgvLista.Columns["TPAR_PRODUCTOTERMINADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["TPAR_FANTASMA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["TPAR_ORDENTRABAJO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["TPAR_ENSAMBLADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["TPAR_ADQUIRIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                      

            //Dataviews
            dvTipoParte = new DataView(dsTipoParte.TIPOS_PARTES);
            dvTipoParte.Sort = "TPAR_NOMBRE ASC";
            dgvLista.DataSource = dvTipoParte;            
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
        }        

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "TPAR_ORDENTRABAJO":
                    case "TPAR_PRODUCTOTERMINADO":
                    case "TPAR_FANTASMA":
                    case "TPAR_ENSAMBLADO":
                    case "TPAR_ADQUIRIDO":
                        nombre = "No";
                        if (Convert.ToInt32(e.Value) == 1) { nombre = "Si"; }
                        e.Value = nombre;    
                    break;
                    default:
                        break;
                }
            }
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvTipoParte[e.RowIndex]["tpar_codigo"]);
            txtNombre.Text = dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_NOMBRE;
            txtDescripcion.Text = dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_DESCRIPCION;
            if (dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_ADQUIRIDO == 0) { chkAdquirido.Checked = false; }
            else { chkAdquirido.Checked = true; }
            if (dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_ENSAMBLADO == 0) { chkEnsamblado.Checked = false; }
            else { chkEnsamblado.Checked = true; }
            if (dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_FANTASMA == 0) { chkFantasma.Checked = false; }
            else { chkFantasma.Checked = true; }
            if (dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_ORDENTRABAJO == 0) { chkOTS.Checked = false; }
            else { chkOTS.Checked = true; }
            if (dsTipoParte.TIPOS_PARTES.FindByTPAR_CODIGO(codigo).TPAR_PRODUCTOTERMINADO == 0) { chkTerminado.Checked = false; }
            else { chkTerminado.Checked = true; }
        }

        #endregion

        

        

    }
}
