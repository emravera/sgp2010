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
    public partial class frmMarca : Form
    {
        private static frmMarca _frmMarca = null;
        private Data.dsMarca dsMarca = new GyCAP.Data.dsMarca();
        private DataView dvListaMarca, dvComboMarcaDatos, dvComboMarcaBuscar;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio
        public frmMarca()
        {
            InitializeComponent();


            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MCA_CODIGO", "Código");
            dgvLista.Columns.Add("MCA_NOMBRE", "Nombre");
            dgvLista.Columns.Add("CLI_CODIGO", "Cliente");

            //Se setean los valores de las columnas 
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvLista.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvLista.Columns["MCA_NOMBRE"].DataPropertyName = "MCA_NOMBRE";
            
            //Llena el Dataset con los clientes
            BLL.ClienteBLL.ObtenerTodos(dsMarca);

            //Creamos el dataview y lo asignamos a la grilla
            dvListaMarca = new DataView(dsMarca.MARCAS);
            dvListaMarca.Sort = "MCA_NOMBRE ASC";
            dgvLista.DataSource = dvListaMarca;

            //CARGA DE COMBOS
            //Combo de la Busqueda
            //Creamos el Dataview y se lo asignamos al combo
            dvComboMarcaBuscar = new DataView(dsMarca.CLIENTES);
            cbClienteBuscar.DataSource = dvComboMarcaBuscar;
            cbClienteBuscar.DisplayMember = "CLI_RAZONSOCIAL";
            cbClienteBuscar.ValueMember = "CLI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbClienteBuscar.SelectedIndex = -1;
            cbClienteBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            dvComboMarcaDatos = new DataView(dsMarca.CLIENTES);
            cbClienteDatos.DataSource = dvComboMarcaDatos;
            cbClienteDatos.DisplayMember = "CLI_RAZONSOCIAL";
            cbClienteDatos.ValueMember = "CLI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbClienteDatos.SelectedIndex = -1;
            cbClienteDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Seteo de los Maxlengt
            txtNombreBuscar.MaxLength = 30;
            txtNombre.MaxLength = 30;
            
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);

        }

        //Método para evitar la creación de más de una pantalla
        public static frmMarca Instancia
        {
            get
            {
                if (_frmMarca == null || _frmMarca.IsDisposed)
                {
                    _frmMarca = new frmMarca();
                }
                else
                {
                    _frmMarca.BringToFront();
                }
                return _frmMarca;
            }
            set
            {
                _frmMarca = value;
            }
        }

        private void frmMarca_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled == true)
            {
                txtNombreBuscar.Focus();
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        #endregion

        #region Botones
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
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
#endregion


        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsMarca.MARCAS.Clear();

                //Llamamos al método de búsqueda de datos
                BLL.MarcaBLL.ObtenerTodos(txtNombreBuscar.Text, Convert.ToInt32(cbClienteBuscar.SelectedValue), dsMarca);
                
             
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaMarca.Table = dsMarca.MARCAS;

                if (dsMarca.MARCAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Marcas con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Seteamos el estado de la interfaz           
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Marcas - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    case "CLI_CODIGO":
                        if (Convert.ToInt32(e.Value) != 0)
                        {
                            nombre = dsMarca.CLIENTES.FindByCLI_CODIGO(Convert.ToInt32(e.Value)).CLI_RAZONSOCIAL;
                        }
                        else nombre = "";
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

        #endregion

        #region Pestaña Datos
        
        //Metodo que carga los datos desde la grilla hacia a los controles 
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoMarca = Convert.ToInt32(dvListaMarca[e.RowIndex]["mca_codigo"]);
            txtNombre.Text = dsMarca.MARCAS.FindByMCA_CODIGO(codigoMarca).MCA_NOMBRE;

            try
            {
                cbClienteDatos.SelectedValue = dsMarca.MARCAS.FindByMCA_CODIGO(codigoMarca).CLI_CODIGO;
            }
            catch 
            {
                cbClienteDatos.SelectedIndex = -1;
            }
            
        }

        //Metodo para eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Marca seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvListaMarca[dgvLista.SelectedRows[0].Index]["mca_codigo"]);
                        BLL.MarcaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsMarca.MARCAS.FindByMCA_CODIGO(codigo).Delete();
                        dsMarca.MARCAS.AcceptChanges();
                        btnVolver.PerformClick();
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Marca de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


      //Guardado de los Datos  
      private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtNombre.Text != String.Empty)
            {
                Entidades.Marca marca = new GyCAP.Entidades.Marca();
                Entidades.Cliente cli = new GyCAP.Entidades.Cliente();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando una marca nueva
                    marca.Nombre = txtNombre.Text;

                    if (cbClienteDatos.SelectedIndex != -1)
                    {
                        //Es una marca con el cliente asociado
                        //Creo el objeto cliente y despues lo asigno
                        //Busco el codigo del cliente
                        int idCliente = Convert.ToInt32(cbClienteDatos.SelectedValue);
                        cli.Codigo = Convert.ToInt32(dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_CODIGO);
                        cli.RazonSocial = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_RAZONSOCIAL;
                        cli.Telefono = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_TELEFONO;
                        cli.FechaAlta = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_FECHAALTA;
                    }
                    else 
                    {
                        //Es una Marca que no tiene cliente asociado
                        cli.Codigo = 0;
                    }

                    //Asigno el cliente creado a cliente de la marca
                    marca.Cliente = cli;
                    
                    try
                    {
                        //Primero lo creamos en la db
                        marca.Codigo = BLL.MarcaBLL.Insertar(marca);
                        //Ahora lo agregamos al dataset
                        Data.dsMarca.MARCASRow rowMarcas = dsMarca.MARCAS.NewMARCASRow();
                        //Indicamos que comienza la edición de la fila
                        rowMarcas.BeginEdit();
                        rowMarcas.MCA_CODIGO = marca.Codigo;
                        rowMarcas.MCA_NOMBRE = marca.Nombre;
                        if (marca.Cliente.Codigo != 0)
                        {
                            rowMarcas.CLI_CODIGO = marca.Cliente.Codigo;
                        }
                        //Termina la edición de la fila
                        rowMarcas.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMarca.MARCAS.AddMARCASRow(rowMarcas);
                        dsMarca.MARCAS.AcceptChanges();
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
                    //Está modificando una marca
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    marca.Codigo = Convert.ToInt32(dvListaMarca[dgvLista.SelectedRows[0].Index]["mca_codigo"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    marca.Nombre = txtNombre.Text;

                    if (cbClienteDatos.SelectedIndex != -1 && chboxCliente.Checked == true)
                    {
                        //Creo el objeto cliente
                        int idCliente = Convert.ToInt32(cbClienteDatos.SelectedValue);
                        cli.Codigo = Convert.ToInt32(dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_CODIGO);
                        cli.RazonSocial = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_RAZONSOCIAL;
                        cli.Telefono = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_TELEFONO;
                        cli.FechaAlta = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_FECHAALTA;
                    }
                    else
                    {
                        cli.Codigo = 0;
                    }

                    //Asigno el cliente creado a cliente de la marca
                    marca.Cliente = cli;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.MarcaBLL.Actualizar(marca);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsMarca.MARCASRow rowMarca = dsMarca.MARCAS.FindByMCA_CODIGO(marca.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowMarca.BeginEdit();
                        rowMarca.MCA_NOMBRE = marca.Nombre;
                        if (marca.Cliente.Codigo != 0)
                        {
                            rowMarca.CLI_CODIGO = marca.Cliente.Codigo;
                        }
                        else rowMarca.CLI_CODIGO = 0;
                        //Termina la edición de la fila
                        rowMarca.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMarca.MARCAS.AcceptChanges();
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
                //Actualizamos la lista con las modificaciones
                dgvLista.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


      private void chboxCliente_CheckStateChanged(object sender, EventArgs e)
      {
          if (chboxCliente.Checked == true)
          {
              cbClienteDatos.Enabled = true;
          }
          else
          {
              cbClienteDatos.Enabled = false;
              if (estadoInterface != estadoUI.modificar)
              {
                  cbClienteDatos.SelectedIndex = -1;
              }
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

                    if (dsMarca.MARCAS.Rows.Count == 0)
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
                    tcMarca.SelectedTab = tpBuscar;
                    txtNombreBuscar.Text = "";
                    txtNombreBuscar.Focus();
                    cbClienteBuscar.SelectedIndex = -1;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    cbClienteDatos.Enabled = false;
                    cbClienteDatos.SelectedIndex = -1;
                    chboxCliente.Visible = true;
                    chboxCliente.Checked = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcMarca.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    cbClienteDatos.Enabled = false;
                    cbClienteDatos.SelectedIndex = -1;
                    chboxCliente.Visible = true;
                    chboxCliente.Checked = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcMarca.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbClienteDatos.Enabled = false;
                    chboxCliente.Visible = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    chboxCliente.Visible = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcMarca.SelectedTab = tpDatos;

                    if (cbClienteDatos.SelectedIndex != -1)
                    {
                        chboxCliente.Checked = true;
                        cbClienteDatos.Enabled = true;
                    }
                    else
                    {
                        chboxCliente.Checked = false;
                        cbClienteDatos.Enabled = false;
                    }
                    break;
                default:
                    break;
            }
        }
        //Metodos que controlan el enter de los textbox
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
