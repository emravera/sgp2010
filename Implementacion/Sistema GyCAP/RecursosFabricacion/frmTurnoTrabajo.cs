﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmTurnoTrabajo : Form
    {
        private static frmTurnoTrabajo _frmTurnoTrabajo = null;
        private Data.dsCentroTrabajo dsTurnos = new GyCAP.Data.dsCentroTrabajo();
        DataView dvTurnos;
        private enum estadoUI { inicio, modificar };
        private estadoUI estadoInterface;
        
        public frmTurnoTrabajo()
        {
            InitializeComponent();
            InicializarDatos();
            SetInterface(estadoUI.inicio);
        }

        public static frmTurnoTrabajo Instancia
        {
            get
            {
                if (_frmTurnoTrabajo == null || _frmTurnoTrabajo.IsDisposed)
                {
                    _frmTurnoTrabajo = new frmTurnoTrabajo();
                }
                else
                {
                    _frmTurnoTrabajo.BringToFront();
                }
                return _frmTurnoTrabajo;
            }
            set
            {
                _frmTurnoTrabajo = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region Botones

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvTurnos[dgvLista.SelectedRows[0].Index]["tur_codigo"]);
                txtNombre.Text = dsTurnos.TURNOS_TRABAJO.FindByTUR_CODIGO(codigo).TUR_NOMBRE;
                dtpHoraInicio.Value = Sistema.FuncionesAuxiliares.DecimalToDateTime(dsTurnos.TURNOS_TRABAJO.FindByTUR_CODIGO(codigo).TUR_HORAINICIO);
                dtpHoraFin.Value = Sistema.FuncionesAuxiliares.DecimalToDateTime(dsTurnos.TURNOS_TRABAJO.FindByTUR_CODIGO(codigo).TUR_HORAFIN);
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Turno de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el Turno de Trabajo seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos
                        int codigo = Convert.ToInt32(dvTurnos[dgvLista.SelectedRows[0].Index]["tur_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.TurnoTrabajoBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsTurnos.TURNOS_TRABAJO.FindByTUR_CODIGO(codigo).Delete();
                        dsTurnos.TURNOS_TRABAJO.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        dsTurnos.TURNOS_TRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsTurnos.TURNOS_TRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Turno de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            string datosFaltantes = string.Empty;
            if (txtNombre.Text == string.Empty) { datosFaltantes += "\n* Nombre"; }
            if (dtpHoraInicio.Value.ToShortTimeString() == "00:00") { datosFaltantes += "\n* Hora inicio"; }
            if (dtpHoraFin.Value.ToShortTimeString() == "00:00") { datosFaltantes += "\n* hora fin"; }
            if (datosFaltantes == String.Empty)
            {
                Entidades.TurnoTrabajo turno = new GyCAP.Entidades.TurnoTrabajo();

                //Revisamos que está haciendo
                if (estadoInterface != estadoUI.modificar)
                {
                    //Está cargando uno nuevo
                    turno.Nombre = txtNombre.Text;
                    turno.HoraInicio = Sistema.FuncionesAuxiliares.StringHourToDecimal(dtpHoraInicio.Value.ToShortTimeString());
                    turno.HoraFin = Sistema.FuncionesAuxiliares.StringHourToDecimal(dtpHoraFin.Value.ToShortTimeString());
                    try
                    {
                        //Primero lo creamos en la db
                        BLL.TurnoTrabajoBLL.Insertar(turno);
                        //Ahora lo agregamos al dataset
                        Data.dsCentroTrabajo.TURNOS_TRABAJORow row = dsTurnos.TURNOS_TRABAJO.NewTURNOS_TRABAJORow();
                        row.BeginEdit();
                        row.TUR_CODIGO = turno.Codigo;
                        row.TUR_NOMBRE = turno.Nombre;
                        row.TUR_HORAINICIO = turno.HoraInicio;
                        row.TUR_HORAFIN = turno.HoraFin;
                        row.EndEdit();
                        dsTurnos.TURNOS_TRABAJO.AddTURNOS_TRABAJORow(row);
                        dsTurnos.TURNOS_TRABAJO.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsTurnos.TURNOS_TRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsTurnos.TURNOS_TRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    turno.Codigo = Convert.ToInt32(dvTurnos[dgvLista.SelectedRows[0].Index]["tur_codigo"]);
                    turno.Nombre = txtNombre.Text;
                    turno.HoraInicio = Sistema.FuncionesAuxiliares.StringHourToDecimal(dtpHoraInicio.Value.ToShortTimeString());
                    turno.HoraFin = Sistema.FuncionesAuxiliares.StringHourToDecimal(dtpHoraFin.Value.ToShortTimeString());
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.TurnoTrabajoBLL.Actualizar(turno);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCentroTrabajo.TURNOS_TRABAJORow row = dsTurnos.TURNOS_TRABAJO.FindByTUR_CODIGO(turno.Codigo);
                        row.BeginEdit();
                        row.TUR_NOMBRE = turno.Nombre;
                        row.TUR_HORAINICIO = turno.HoraInicio;
                        row.TUR_HORAFIN = turno.HoraFin;
                        row.EndEdit();
                        dsTurnos.TURNOS_TRABAJO.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsTurnos.TURNOS_TRABAJO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
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

                    if (dsTurnos.TURNOS_TRABAJO.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    txtNombre.Text = String.Empty;
                    dtpHoraFin.Value = new DateTime(2000, 1, 1, 0, 0, 0);
                    dtpHoraInicio.Value = new DateTime(2000, 1, 1, 0, 0, 0);
                    btnCancelar.Enabled = false;
                    dgvLista.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    dtpHoraFin.Enabled = true;
                    dtpHoraInicio.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    break;
                default:
                    break;
            }
        }

        private void frmTurnoTrabajo_Activated(object sender, EventArgs e)
        {
            if (txtNombre.Enabled == true)
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void InicializarDatos()
        {
            //Grilla
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("TUR_NOMBRE","Nombre");
            dgvLista.Columns.Add("TUR_HORAINICIO","Hora Inicio");
            dgvLista.Columns.Add("TUR_HORAFIN","Hora Fin");
            dgvLista.Columns["TUR_HORAINICIO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["TUR_HORAFIN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLista.Columns["TUR_NOMBRE"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["TUR_NOMBRE"].DataPropertyName = "TUR_NOMBRE";
            dgvLista.Columns["TUR_HORAINICIO"].DataPropertyName = "TUR_HORAINICIO";
            dgvLista.Columns["TUR_HORAFIN"].DataPropertyName = "TUR_HORAFIN";

            try
            {
                BLL.TurnoTrabajoBLL.ObtenerTurnos(dsTurnos.TURNOS_TRABAJO);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Dataview
            dvTurnos = new DataView(dsTurnos.TURNOS_TRABAJO);
            dvTurnos.Sort = "TUR_NOMBRE ASC";
            dgvLista.DataSource = dvTurnos;
        }        

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "TUR_HORAINICIO":
                        nombre = Sistema.FuncionesAuxiliares.DecimalHourToString(decimal.Parse(e.Value.ToString()));
                        e.Value = nombre;
                        break;
                    case "TUR_HORAFIN":
                        nombre = Sistema.FuncionesAuxiliares.DecimalHourToString(decimal.Parse(e.Value.ToString()));
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
