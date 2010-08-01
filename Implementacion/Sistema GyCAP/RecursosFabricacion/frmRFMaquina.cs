using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Data;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmRFMaquina : Form
    {
        private static frmRFMaquina _frmRFMaquina = null;
        private Data.dsMaquina dsMaquina = new GyCAP.Data.dsMaquina();
        private DataView dvMaquina, dvEstadoMaquina, dvEstadoMaquinaBuscar,
                         dvListaModelos, dvModelo,
                         dvListaFabricante, dvFabricante;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmRFMaquina()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MAQ_CODIGO", "Código");
            dgvLista.Columns.Add("MAQ_NOMBRE", "Nombre");
            dgvLista.Columns.Add("MAQ_NUMEROSERIE", "Nro. Serie");
            dgvLista.Columns.Add("MAQ_MARCA", "Marca");
            dgvLista.Columns.Add("MODM_CODIGO", "Modelo");
            dgvLista.Columns.Add("FAB_CODIGO", "Fabricante");
            dgvLista.Columns.Add("EMAQ_CODIGO", "Estado");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["MAQ_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MAQ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MAQ_NUMEROSERIE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MAQ_MARCA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MODM_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["FAB_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["EMAQ_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MAQ_CODIGO"].DataPropertyName = "MAQ_CODIGO";
            dgvLista.Columns["MAQ_NOMBRE"].DataPropertyName = "MAQ_NOMBRE";
            dgvLista.Columns["MAQ_NUMEROSERIE"].DataPropertyName = "MAQ_NUMEROSERIE";
            dgvLista.Columns["MAQ_MARCA"].DataPropertyName = "MAQ_MARCA";
            dgvLista.Columns["MODM_CODIGO"].DataPropertyName = "MODM_CODIGO";
            dgvLista.Columns["FAB_CODIGO"].DataPropertyName = "FAB_CODIGO";
            dgvLista.Columns["EMAQ_CODIGO"].DataPropertyName = "EMAQ_CODIGO";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["MAQ_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["MAQ_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvMaquina = new DataView(dsMaquina.MAQUINAS);
            dvMaquina.Sort = "MAQ_NOMBRE ASC";
            dgvLista.DataSource = dvMaquina;

            //Llena el Dataset con los fabricantes
            BLL.FabricanteMaquinaBLL.ObtenerTodos(dsMaquina);

            //Llena el Dataset con los Sectores
            BLL.ModeloMaquinaBLL.ObtenerTodos(dsMaquina);

            //Llena el Dataset con los estados
            BLL.EstadoMaquinaBLL.ObtenerTodos(dsMaquina);

            //Carga de la Lista de Modelos
            dvListaModelos = new DataView(dsMaquina.MODELOS_MAQUINAS);

            lvModelos.View = View.Details;
            lvModelos.FullRowSelect = true;
            lvModelos.MultiSelect = false;
            lvModelos.CheckBoxes = true;
            lvModelos.GridLines = true;
            lvModelos.Columns.Add("Modelos", 120);
            lvModelos.Columns.Add("Codigo", 0);
            if (dvListaModelos.Count != 0)
            {
                foreach (DataRowView dr in dvListaModelos)
                {
                    ListViewItem li = new ListViewItem(dr["MODM_NOMBRE"].ToString());
                    li.SubItems.Add(dr["MODM_CODIGO"].ToString());
                    li.Checked = true;
                    lvModelos.Items.Add(li);
                }
            }

            //Carga de la Lista de Modelos
            dvListaFabricante = new DataView(dsMaquina.FABRICANTE_MAQUINAS);

            lvFabricantes.View = View.Details;
            lvFabricantes.FullRowSelect = true;
            lvFabricantes.MultiSelect = false;
            lvFabricantes.CheckBoxes = true;
            lvFabricantes.GridLines = true;
            lvFabricantes.Columns.Add("Fabricantes", 120);
            lvFabricantes.Columns.Add("Codigo", 0);
            if (dvListaModelos.Count != 0)
            {
                foreach (DataRowView dr in dvListaFabricante)
                {
                    ListViewItem li = new ListViewItem(dr["FAB_RAZONSOCIAL"].ToString());
                    li.SubItems.Add(dr["FAB_CODIGO"].ToString());
                    li.Checked = true;
                    lvFabricantes.Items.Add(li);
                }
            }

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvEstadoMaquinaBuscar = new DataView(dsMaquina.ESTADO_MAQUINAS);
            cboBuscarEstado.SetDatos(dvEstadoMaquinaBuscar, "EMAQ_codigo", "EMAQ_nombre", "-- TODOS --", true);

            //Combo de Datos
            dvEstadoMaquina = new DataView(dsMaquina.ESTADO_MAQUINAS);
            cboEstado.SetDatos(dvEstadoMaquina, "EMAQ_CODIGO", "EMAQ_NOMBRE", "Seleccione un Estado...", false);

            dvFabricante = new DataView(dsMaquina.FABRICANTE_MAQUINAS);
            cboFabricante.SetDatos(dvFabricante, "FAB_CODIGO", "FAB_RAZONSOCIAL", "Seleccione un Fabricante...", false);
            
            dvModelo = new DataView(dsMaquina.MODELOS_MAQUINAS);
            cboModelo.SetDatos(dvModelo, "MODM_CODIGO", "MODM_NOMBRE", "Seleccione un Modelo...", false);

            //Limitamos el maximo de los controles
            txtMarca.MaxLength = 80;
            txtNombre.MaxLength = 80;
            txtNroSerie.MaxLength = 20;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);

        }

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsMaquina.MAQUINAS.Rows.Count == 0)
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
                    setBotones(false);
                    txtNombre.Text = string.Empty;
                    txtMarca.Text = string.Empty;
                    txtNroSerie.Text = string.Empty;                    
                    cboEstado.SelectedIndex = -1;
                    cboFabricante.SelectedIndex = -1;
                    cboModelo.SelectedIndex = -1;

                    //gbGuardarCancelar.Enabled = true;
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
                case estadoUI.consultar:
                    setBotones(true);
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    setBotones(false);
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

        private void setBotones(bool pValue)
        {
            txtMarca.ReadOnly = pValue;
            txtNombre.ReadOnly = pValue;
            txtNroSerie.ReadOnly = pValue;
            cboModelo.Enabled = !pValue; 
            cboEstado.Enabled = !pValue;
            cboFabricante.Enabled = !pValue;
        }

        //Método para evitar la creación de más de una pantalla
        public static frmRFMaquina Instancia
        {
            get
            {
                if (_frmRFMaquina == null || _frmRFMaquina.IsDisposed)
                {
                    _frmRFMaquina = new frmRFMaquina();
                }
                else
                {
                    _frmRFMaquina.BringToFront();
                }
                return _frmRFMaquina;
            }
            set
            {
                _frmRFMaquina = value;
            }
        }

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtNombre.Text != String.Empty && txtMarca.Text != String.Empty && txtNroSerie.Text != String.Empty &&
                cboModelo.SelectedIndex != -1 && cboFabricante.SelectedIndex != -1 && cboEstado.SelectedIndex != -1)
            {
                Entidades.Maquina maquina = new GyCAP.Entidades.Maquina();
                Entidades.ModeloMaquina modelo = new GyCAP.Entidades.ModeloMaquina();
                Entidades.FabricanteMaquina fabricante = new GyCAP.Entidades.FabricanteMaquina();
                Entidades.EstadoMaquina estado = new GyCAP.Entidades.EstadoMaquina();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo )
                {
                    //Está cargando una marca nueva
                    maquina.Nombre = txtNombre.Text;
                    maquina.Marca = txtMarca.Text;
                    maquina.NumeroSerie = txtNroSerie.Text;
                    maquina.FechaAlta = BLL.DBBLL.GetFechaServidor();

                    //Creo el objeto Modelo y despues lo asigno
                    //Busco el codigo de la Modelo
                    int idModelo = Convert.ToInt32(cboModelo.GetSelectedValueInt());
                    modelo.Codigo = Convert.ToInt32(dsMaquina.MODELOS_MAQUINAS.FindByMODM_CODIGO(idModelo).MODM_CODIGO);
                    //Asigno el Modelo creada a la Maquina correspondiente
                    maquina.Modelo = modelo;

                    int idFabricante = Convert.ToInt32(cboFabricante.GetSelectedValueInt());
                    fabricante.Codigo = Convert.ToInt32(dsMaquina.FABRICANTE_MAQUINAS.FindByFAB_CODIGO(idFabricante).FAB_CODIGO);
                    //Asigno el Fabricante creada a la Maquina correspondiente
                    maquina.Fabricante = fabricante;

                    int idEstado = Convert.ToInt32(cboEstado.GetSelectedValueInt());
                    estado.Codigo = Convert.ToInt32(dsMaquina.ESTADO_MAQUINAS.FindByEMAQ_CODIGO(idEstado).EMAQ_CODIGO);
                    //Asigno el Estado creada a la Maquina correspondiente
                    maquina.Estado = estado;

                    try
                    {
                        //Primero lo creamos en la db
                        maquina.Codigo = BLL.MaquinaBLL.Insertar(maquina);
                        //Ahora lo agregamos al dataset
                        Data.dsMaquina.MAQUINASRow rowMaquina = dsMaquina.MAQUINAS.NewMAQUINASRow();
                        //Indicamos que comienza la edición de la fila
                        rowMaquina.BeginEdit();
                        rowMaquina.MAQ_CODIGO = maquina.Codigo;
                        rowMaquina.MAQ_NOMBRE = maquina.Nombre;
                        rowMaquina.MAQ_NUMEROSERIE = maquina.NumeroSerie;
                        rowMaquina.MAQ_MARCA = maquina.Marca;
                        rowMaquina.MAQ_FECHAALTA = maquina.FechaAlta; 
                        rowMaquina.FAB_CODIGO = maquina.Fabricante.Codigo;
                        rowMaquina.MODM_CODIGO = maquina.Modelo.Codigo;
                        rowMaquina.EMAQ_CODIGO = maquina.Estado.Codigo;   
                        //Termina la edición de la fila
                        rowMaquina.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMaquina.MAQUINAS.AddMAQUINASRow(rowMaquina);
                        dsMaquina.MAQUINAS.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz

                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        //if (estadoInterface == estadoUI.nuevoExterno)
                        //{
                        //    //Nuevo desde acceso directo, cerramos el formulario
                        //    btnSalir.PerformClick();
                        //}
                        //else
                        //{
                        //    //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                        SetInterface(estadoUI.inicio);
                        //}
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
                    //Está modificando una maquina
                    maquina.Codigo = Convert.ToInt32(dvMaquina[dgvLista.SelectedRows[0].Index]["maq_codigo"]);
                    //Está cargando una marca nueva
                    maquina.Nombre = txtNombre.Text;
                    maquina.Marca = txtMarca.Text;
                    maquina.NumeroSerie = txtNroSerie.Text;

                    //Creo el objeto Modelo y despues lo asigno
                    //Busco el codigo de la Modelo
                    int idModelo = Convert.ToInt32(cboModelo.GetSelectedValueInt());
                    modelo.Codigo = Convert.ToInt32(dsMaquina.MODELOS_MAQUINAS.FindByMODM_CODIGO(idModelo).MODM_CODIGO);
                    //Asigno el Modelo creada a la Maquina correspondiente
                    maquina.Modelo = modelo;

                    int idFabricante = Convert.ToInt32(cboFabricante.GetSelectedValueInt());
                    fabricante.Codigo = Convert.ToInt32(dsMaquina.FABRICANTE_MAQUINAS.FindByFAB_CODIGO(idFabricante).FAB_CODIGO);
                    //Asigno el Fabricante creada a la Maquina correspondiente
                    maquina.Fabricante = fabricante;

                    int idEstado = Convert.ToInt32(cboEstado.GetSelectedValueInt());
                    estado.Codigo = Convert.ToInt32(dsMaquina.ESTADO_MAQUINAS.FindByEMAQ_CODIGO(idEstado).EMAQ_CODIGO);
                    //Asigno el Estado creada a la Maquina correspondiente
                    maquina.Estado = estado;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.MaquinaBLL.Actualizar(maquina);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsMaquina.MAQUINASRow rowMaquina = dsMaquina.MAQUINAS.FindByMAQ_CODIGO(maquina.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowMaquina.BeginEdit();
                        rowMaquina.MAQ_NOMBRE = maquina.Nombre;
                        rowMaquina.MAQ_NUMEROSERIE = maquina.NumeroSerie;
                        rowMaquina.MAQ_MARCA = maquina.Marca;
                        rowMaquina.FAB_CODIGO = maquina.Fabricante.Codigo;
                        rowMaquina.MODM_CODIGO = maquina.Modelo.Codigo;
                        rowMaquina.EMAQ_CODIGO = maquina.Estado.Codigo;
                        //Termina la edición de la fila
                        rowMaquina.EndEdit();
                        
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMaquina.MAQUINAS.AcceptChanges();
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
                //Lista Fabricacion
                ListView.CheckedListViewItemCollection FabricanteChequeados = lvFabricantes.CheckedItems;
                string cadFabricantes = string.Empty;

                //Lista Modelos
                ListView.CheckedListViewItemCollection ModelosChequeados = lvModelos.CheckedItems;
                string cadModelos = string.Empty;

                if (FabricanteChequeados.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos un Fabricante.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (ListViewItem item in FabricanteChequeados)
                    {
                        if (cadFabricantes == string.Empty)
                        {
                            cadFabricantes = item.SubItems[1].Text;
                        }
                        else
                        {
                            cadFabricantes += ", " + item.SubItems[1].Text;
                        }
                    }
                }

                if (ModelosChequeados.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos un Modelo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (ListViewItem item in ModelosChequeados)
                    {
                        if (cadModelos == string.Empty)
                        {
                            cadModelos = item.SubItems[1].Text;
                        }
                        else
                        {
                            cadModelos += ", " + item.SubItems[1].Text;
                        }
                    }
                }

                if (FabricanteChequeados.Count != 0 || ModelosChequeados.Count != 0)
                {
                    dsMaquina.MAQUINAS.Clear();
                    BLL.MaquinaBLL.ObtenerTodos(txtNombreBuscar.Text, cboBuscarEstado.GetSelectedValueInt(), cadFabricantes, cadModelos, dsMaquina);
                    //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                    //por una consulta a la BD
                    dvMaquina.Table = dsMaquina.MAQUINAS;

                    if (dsMaquina.MAQUINAS.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron Maquinas con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    SetInterface(estadoUI.inicio);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Maquinas - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "FAB_CODIGO":
                        nombre = dsMaquina.FABRICANTE_MAQUINAS.FindByFAB_CODIGO(Convert.ToInt32(e.Value)).FAB_RAZONSOCIAL;
                        e.Value = nombre;
                        break;
                    case "EMAQ_CODIGO":
                        nombre = dsMaquina.ESTADO_MAQUINAS.FindByEMAQ_CODIGO(Convert.ToInt32(e.Value)).EMAQ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MODM_CODIGO":
                        nombre = dsMaquina.MODELOS_MAQUINAS.FindByMODM_CODIGO(Convert.ToInt32(e.Value)).MODM_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoMaquina = Convert.ToInt32(dvMaquina[e.RowIndex]["MAQ_CODIGO"]);
            txtNombre.Text = dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigoMaquina).MAQ_NOMBRE;
            txtMarca.Text = dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigoMaquina).MAQ_MARCA;
            txtNroSerie.Text = dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigoMaquina).MAQ_NUMEROSERIE;
            cboEstado.SetSelectedValue(int.Parse(dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigoMaquina).EMAQ_CODIGO.ToString()));
            cboFabricante.SetSelectedValue(int.Parse(dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigoMaquina).FAB_CODIGO.ToString()));
            cboModelo.SetSelectedValue(int.Parse(dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigoMaquina).MODM_CODIGO.ToString()));
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Maquina seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        long codigo = Convert.ToInt32(dvMaquina[dgvLista.SelectedRows[0].Index]["maq_codigo"]);
                        BLL.MaquinaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsMaquina.MAQUINAS.FindByMAQ_CODIGO(codigo).Delete();
                        dsMaquina.MAQUINAS.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar una Maquina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtMarca_Enter(object sender, EventArgs e)
        {
            txtMarca.SelectAll();
        }

        private void txtNroSerie_Enter(object sender, EventArgs e)
        {
            txtNroSerie.SelectAll();
        }
    }
}
