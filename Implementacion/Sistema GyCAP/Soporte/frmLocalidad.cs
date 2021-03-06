﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Soporte
{
    public partial class frmLocalidad : Form
    {
        private static frmLocalidad _frmLocalidad = null;
        private Data.dsProveedor dsLocalidad = new GyCAP.Data.dsProveedor();
        private DataView dvLocalidad, dvProvincias, dvProvinciasBuscar;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        
        public frmLocalidad()
        {
            InitializeComponent();
            InicializarPantalla();
        }

        public static frmLocalidad Instancia
        {
            get
            {
                if (_frmLocalidad == null || _frmLocalidad.IsDisposed)
                {
                    _frmLocalidad = new frmLocalidad();
                }
                else
                {
                    _frmLocalidad.BringToFront();
                }
                return _frmLocalidad;
            }
            set
            {
                _frmLocalidad = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsLocalidad.LOCALIDADES.Clear();              
                BLL.LocalidadBLL.ObtenerLocalidades(txtNombreBuscar.Text, cbProvinciaBuscar.GetSelectedValueInt(), dsLocalidad.LOCALIDADES);
                dvLocalidad.Table = dsLocalidad.LOCALIDADES;

                if (dsLocalidad.LOCALIDADES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Localidades con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Localidad - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        //Metodo para formatear la grilla que cambia las foreign keys por el nombre
        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "PCIA_NOMBRE":
                        nombre = dsLocalidad.PROVINCIAS.FindByPCIA_CODIGO(Convert.ToInt32(e.Value)).PCIA_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Pestaña Datos

        //Programacion de Cada uno de los botones
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Localidad seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvLocalidad[dgvLista.SelectedRows[0].Index]["loc_codigo"]);
                        BLL.LocalidadBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsLocalidad.LOCALIDADES.FindByLOC_CODIGO(codigo).Delete();
                        dsLocalidad.LOCALIDADES.AcceptChanges();
                        btnVolver.PerformClick();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Localidad de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Metodo que carga los datos desde la grilla hacia a los controles 
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvLocalidad[e.RowIndex]["loc_codigo"]);
            txtNombre.Text = dsLocalidad.LOCALIDADES.FindByLOC_CODIGO(codigo).LOC_NOMBRE;
            cbProvincia.SetSelectedValue(Convert.ToInt32(dsLocalidad.LOCALIDADES.FindByLOC_CODIGO(codigo).PCIA_CODIGO));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtNombre.Text != String.Empty && cbProvincia.GetSelectedIndex() != -1)
            {
                Entidades.Localidad localidad = new GyCAP.Entidades.Localidad();
                localidad.Provincia = new GyCAP.Entidades.Provincia();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando una nueva
                    localidad.Nombre = txtNombre.Text;
                    localidad.Provincia.Codigo = cbProvincia.GetSelectedValueInt();
                    
                    try
                    {
                        //Primero lo creamos en la db
                        BLL.LocalidadBLL.Insertar(localidad);
                        //Ahora lo agregamos al dataset
                        Data.dsProveedor.LOCALIDADESRow row = dsLocalidad.LOCALIDADES.NewLOCALIDADESRow();
                        //Indicamos que comienza la edición de la fila
                        row.BeginEdit();
                        row.LOC_CODIGO = localidad.Codigo;
                        row.LOC_NOMBRE = localidad.Nombre;
                        row.PCIA_CODIGO = localidad.Provincia.Codigo;
                        //Termina la edición de la fila
                        row.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsLocalidad.LOCALIDADES.AddLOCALIDADESRow(row);
                        dsLocalidad.LOCALIDADES.AcceptChanges();
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
                    //Está modificando
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    localidad.Codigo = Convert.ToInt32(dvLocalidad[dgvLista.SelectedRows[0].Index]["loc_codigo"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    localidad.Nombre = txtNombre.Text;
                    localidad.Provincia.Codigo = cbProvincia.GetSelectedValueInt();

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.LocalidadBLL.Actualizar(localidad);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsProveedor.LOCALIDADESRow row = dsLocalidad.LOCALIDADES.FindByLOC_CODIGO(localidad.Codigo);
                        //Indicamos que comienza la edición de la fila
                        row.BeginEdit();
                        row.LOC_NOMBRE = localidad.Nombre;
                        row.PCIA_CODIGO = localidad.Provincia.Codigo;
                        //Termina la edición de la fila
                        row.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsLocalidad.LOCALIDADES.AcceptChanges();
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
                dvLocalidad.Table = dsLocalidad.LOCALIDADES;
            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    if (dsLocalidad.LOCALIDADES.Rows.Count == 0)
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
                    tcLocalidad.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    cbProvincia.Enabled = true;
                    cbProvincia.SetTexto("Seleccione");
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcLocalidad.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    cbProvincia.Enabled = true;
                    cbProvincia.SetTexto("Seleccione");
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcLocalidad.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbProvincia.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcLocalidad.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    cbProvincia.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcLocalidad.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        private void InicializarPantalla()
        {
            //Grilla
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("LOC_NOMBRE", "Nombre");
            dgvLista.Columns.Add("PCIA_NOMBRE", "Provincia");
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLista.Columns["LOC_NOMBRE"].DataPropertyName = "LOC_NOMBRE";
            dgvLista.Columns["PCIA_NOMBRE"].DataPropertyName = "PCIA_CODIGO";
            

            //Cargamos las provincias
            try
            {
                BLL.ProvinciaBLL.ObtenerProvincias(dsLocalidad.PROVINCIAS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            //Dataviews
            dvLocalidad = new DataView(dsLocalidad.LOCALIDADES);
            dvLocalidad.Sort = "LOC_NOMBRE ASC";
            dgvLista.DataSource = dvLocalidad;
            dvProvincias = new DataView(dsLocalidad.PROVINCIAS);
            dvProvincias.Sort = "PCIA_NOMBRE ASC";
            dvProvinciasBuscar = new DataView(dsLocalidad.PROVINCIAS);
            dvProvinciasBuscar.Sort = "PCIA_NOMBRE ASC";

            //Combos
            cbProvinciaBuscar.SetDatos(dvProvinciasBuscar, "PCIA_CODIGO", "PCIA_NOMBRE", "--TODOS--", true);
            cbProvincia.SetDatos(dvProvincias, "PCIA_CODIGO", "PCIA_NOMBRE", "Seleccione", false);
        }
        
        private void frmLocalidad_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled == true)
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

        #endregion
    }
}
