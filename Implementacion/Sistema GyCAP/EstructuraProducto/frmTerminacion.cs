﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmTerminacion : Form
    {
        private static frmTerminacion _frmABM = null;
        private Data.dsTerminacion dsTerminacion = new GyCAP.Data.dsTerminacion();
        private DataView dvTerminacion;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        public frmTerminacion()
        {
            InitializeComponent();
            
            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            
            //Agregamos las columnas
            dgvLista.Columns.Add("TE_CODIGO", "Código");
            dgvLista.Columns.Add("TE_NOMBRE", "Nombre");            
            dgvLista.Columns.Add("TE_ABREVIATURA", "Abreviatura");
            dgvLista.Columns.Add("TE_DESCRIPCION", "Descripción");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvLista.Columns["TE_NOMBRE"].DataPropertyName = "TE_NOMBRE";
            dgvLista.Columns["TE_DESCRIPCION"].DataPropertyName = "TE_DESCRIPCION";
            dgvLista.Columns["TE_ABREVIATURA"].DataPropertyName = "TE_ABREVIATURA";
            
            //Oculta la columna que contiene los encabezados
            dgvLista.RowHeadersVisible = false;

            //Setemaos las columnas
            dgvLista.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TE_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;            
            dgvLista.Columns["TE_ABREVIATURA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TE_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLista.Columns["TE_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["TE_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["TE_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvTerminacion = new DataView(dsTerminacion.TERMINACIONES);
            dgvLista.DataSource = dvTerminacion;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 250;
            txtNombre.MaxLength = 80;
            txtAbreviatura.MaxLength = 80;
            
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsTerminacion.TERMINACIONES.Rows.Count == 0)
                    {
                        hayDatos = false;
                        txtNombreBuscar.Focus();
                    }
                    else
                    {
                        hayDatos = true;
                        dgvLista.Focus();
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcABM.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    txtAbreviatura.ReadOnly = false;
                    txtAbreviatura.Clear();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtAbreviatura.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = string.Empty;
                    txtAbreviatura.Clear();
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcABM.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtAbreviatura.ReadOnly = true;
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtAbreviatura.ReadOnly = false;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcABM.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                default:
                    break;
            }
        }

        //Método para evitar la creación de más de una pantalla
        public static frmTerminacion Instancia
        {
            get
            {
                if (_frmABM == null || _frmABM.IsDisposed)
                {
                    _frmABM = new frmTerminacion();
                }
                else
                {
                    _frmABM.BringToFront();
                }
                return _frmABM;
            }
            set
            {
                _frmABM = value;
            }
        }

        //Evita que el formulario se cierre desde la cruz
        //private void frmTerminacion_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (e.CloseReason == CloseReason.UserClosing)
        //    {
        //        e.Cancel = true;
        //    }
        //}

        #endregion

        

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la terminación seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Creamos el objeto terminacion
                        int codigo = Convert.ToInt32(dvTerminacion[dgvLista.SelectedRows[0].Index]["TE_CODIGO"]);
                        //Lo eliminamos de la DB
                        BLL.TerminacionBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsTerminacion.TERMINACIONES.FindByTE_CODIGO(codigo).Delete();
                        dsTerminacion.TERMINACIONES.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una " + this.Text + " de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty && txtAbreviatura.Text != string.Empty)
            {
                Entidades.Terminacion terminacion = new GyCAP.Entidades.Terminacion();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando una terminacion nuevo
                    terminacion.Nombre = txtNombre.Text;
                    terminacion.Descripcion = txtDescripcion.Text;
                    terminacion.Abreviatura = txtAbreviatura.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        terminacion.Codigo = BLL.TerminacionBLL.Insertar(terminacion);
                        //Ahora lo agregamos al dataset
                        Data.dsTerminacion.TERMINACIONESRow rowTerminacion = dsTerminacion.TERMINACIONES.NewTERMINACIONESRow();
                        //Indicamos que comienza la edición de la fila
                        rowTerminacion.BeginEdit();
                        rowTerminacion.TE_CODIGO = terminacion.Codigo;
                        rowTerminacion.TE_NOMBRE = terminacion.Nombre;
                        rowTerminacion.TE_DESCRIPCION = terminacion.Descripcion;
                        rowTerminacion.TE_ABREVIATURA = terminacion.Abreviatura;
                        //Termina la edición de la fila
                        rowTerminacion.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsTerminacion.TERMINACIONES.AddTERMINACIONESRow(rowTerminacion);
                        dsTerminacion.TERMINACIONES.AcceptChanges();
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
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando una terminacion
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    terminacion.Codigo = Convert.ToInt32(dvTerminacion[dgvLista.SelectedRows[0].Index]["te_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    terminacion.Nombre = txtNombre.Text;
                    terminacion.Descripcion = txtDescripcion.Text;
                    terminacion.Abreviatura = txtAbreviatura.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.TerminacionBLL.Actualizar(terminacion);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsTerminacion.TERMINACIONESRow rowTerminacion = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(terminacion.Codigo);
                        rowTerminacion.BeginEdit();
                        rowTerminacion.TE_NOMBRE = txtNombre.Text;
                        rowTerminacion.TE_DESCRIPCION = txtDescripcion.Text;
                        rowTerminacion.TE_ABREVIATURA = txtAbreviatura.Text;
                        rowTerminacion.EndEdit();
                        dsTerminacion.TERMINACIONES.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //recarga de la grilla
                dgvLista.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsTerminacion.TERMINACIONES.Clear();
                BLL.TerminacionBLL.ObtenerTodos(txtNombreBuscar.Text,dsTerminacion);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvTerminacion.Table = dsTerminacion.TERMINACIONES;
                if (dsTerminacion.TERMINACIONES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron terminaciones con el nombre ingresado.","Aviso",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                }

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Terminación - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }   
        }

        //Evento RowEnter de la grilla, va cargando los datos en la pestaña Datos a medida que se
        //hace clic en alguna fila de la grilla
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > 0)
            //{
                long codigoTerminacion = Convert.ToInt64(dvTerminacion[e.RowIndex]["te_codigo"]);
                txtNombre.Text = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_NOMBRE;
                txtDescripcion.Text = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_DESCRIPCION;
                txtAbreviatura.Text = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_ABREVIATURA;
            //}
        }

        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            if (dsTerminacion.TERMINACIONES.Rows.Count != 0)
            {
                btnConsultar.PerformClick();
            }   
        }

        private void frmTerminacion_Activated(object sender, EventArgs e)
        {
            if (tcABM.SelectedTab == tpBuscar && txtNombreBuscar.Enabled == true) 
            {
                txtNombreBuscar.Focus();
            }
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void txtAbreviatura_Enter(object sender, EventArgs e)
        {
            txtAbreviatura.SelectAll();
        }

    }
}
