using System;
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
        private DataView dvListaMarca, dvComboMarca;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

#region Inicio
        public frmMarca()
        {
            InitializeComponent();


            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MCA_CODIGO", "Código");
            dgvLista.Columns.Add("CLI_CODIGO", "Cliente");
            dgvLista.Columns.Add("MCA_NOMBRE", "Nombre");
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
            dgvLista.DataSource = dvListaMarca;

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvComboMarca = new DataView(dsMarca.CLIENTES);
            cbClienteBuscar.DataSource = dvComboMarca;
            cbClienteBuscar.DisplayMember = "CLI_RAZONSOCIAL";
            cbClienteBuscar.ValueMember = "CLI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbClienteBuscar.SelectedIndex = -1;
            cbClienteBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            cbClienteDatos.DataSource = dvComboMarca;
            cbClienteDatos.DisplayMember = "CLI_RAZONSOCIAL";
            cbClienteDatos.ValueMember = "CLI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbClienteDatos.SelectedIndex = -1;
            cbClienteDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Selecciono por defecto buscar por nombre
            rbNombre.Checked = true;

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

                if (rbNombre.Checked == true && txtNombreBuscar.Text != string.Empty)
                {
                    BLL.MarcaBLL.ObtenerTodos(txtNombreBuscar.Text, dsMarca);

                }
                else if (rbCliente.Checked == true && cbClienteBuscar.SelectedIndex != -1)
                {
                    BLL.MarcaBLL.ObtenerTodos(Convert.ToInt32(cbClienteBuscar.SelectedValue), dsMarca);
                }
                else
                {
                    BLL.MarcaBLL.ObtenerTodos(dsMarca);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaMarca.Table = dsMarca.MARCAS;

                if (dsMarca.MARCAS.Rows.Count == 0)
                {
                    if (rbNombre.Checked == true)
                    {
                        MessageBox.Show("No se encontraron Marcas con el nombre ingresado.", "Aviso");
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Marcas del cliente seleccionado.", "Aviso");
                    }
                    
                }
                           

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
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
                        nombre = dsMarca.CLIENTES.FindByCLI_CODIGO(Convert.ToInt32(e.Value)).CLI_RAZONSOCIAL;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

        //Configuracion de los radio Buttons
        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            cbClienteDatos.SelectedIndex = -1;
            cbClienteBuscar.Enabled = false;
            txtNombreBuscar.Enabled = true;

        }

        private void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            txtNombreBuscar.Enabled = false;
            txtNombreBuscar.Text = String.Empty;
            cbClienteBuscar.Enabled = true;             
        }
        #endregion

        #region Pestaña Datos
        
        //Metodo que carga los datos desde la grilla hacia a los controles 
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoMarca = Convert.ToInt32(dvListaMarca[e.RowIndex]["mca_codigo"]);
            txtCodigo.Text = codigoMarca.ToString();
            txtNombre.Text = dsMarca.MARCAS.FindByMCA_CODIGO(codigoMarca).MCA_NOMBRE;
            cbClienteDatos.SelectedValue = dsMarca.MARCAS.FindByMCA_CODIGO(codigoMarca).CLI_CODIGO;
        }

        //Metodo para eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Marca seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo);
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
                        MessageBox.Show(ex.Message);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Marca de la lista.", "Aviso");
            }
        }

      //Guardado de los Datos  
      private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtNombre.Text != String.Empty &&  cbClienteDatos.SelectedIndex != -1)
            {
                Entidades.Marca marca = new GyCAP.Entidades.Marca();
                Entidades.Cliente cli = new GyCAP.Entidades.Cliente();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando una marca nueva
                    marca.Nombre = txtNombre.Text;
                    
                   
                    //Creo el objeto cliente y despues lo asigno
                    //Busco el codigo del cliente
                    int idCliente = Convert.ToInt32(cbClienteDatos.SelectedValue);
                    cli.Codigo =Convert.ToInt32(dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_CODIGO);
                    cli.RazonSocial = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_RAZONSOCIAL;
                    cli.Telefono = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_TELEFONO;
                    cli.FechaAlta = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_FECHAALTA;
                    
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
                        rowMarcas.CLI_CODIGO = marca.Cliente.Codigo;
                        //Termina la edición de la fila
                        rowMarcas.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMarca.MARCAS.AddMARCASRow(rowMarcas);
                        dsMarca.MARCAS.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //Está modificando una marca
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    marca.Codigo = Convert.ToInt32(dvListaMarca[dgvLista.SelectedRows[0].Index]["mca_codigo"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    marca.Nombre = txtNombre.Text;

                    //Creo el objeto cliente
                    int idCliente = Convert.ToInt32(cbClienteDatos.SelectedValue);
                    cli.Codigo = Convert.ToInt32(dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_CODIGO);
                    cli.RazonSocial = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_RAZONSOCIAL;
                    cli.Telefono = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_TELEFONO;
                    cli.FechaAlta = dsMarca.CLIENTES.FindByCLI_CODIGO(idCliente).CLI_FECHAALTA;
                    
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
                        rowMarca.CLI_CODIGO = marca.Cliente.Codigo;
                        //Termina la edición de la fila
                        rowMarca.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMarca.MARCAS.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Aviso");
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Aviso");
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
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    cbClienteDatos.Enabled = true;
                    cbClienteDatos.SelectedIndex = -1;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbClienteDatos.Enabled = false;
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
                    cbClienteDatos.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmMarca_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }

        }
        
#endregion

        
        
        

        

        
        

       
       

    }
}
