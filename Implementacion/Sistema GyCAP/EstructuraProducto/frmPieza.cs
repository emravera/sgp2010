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
    public partial class frmPieza : Form
    {
        private static frmPieza _frmPieza = null;
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        private DataView dvPiezas, dvDetallePieza, dvMPDisponibles, dvUnidadMedida;
        private DataView dvTerminacionBuscar, dvTerminaciones, dvEstado, dvPlano;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = 0;
        
        public frmPieza()
        {
            InitializeComponent();

            //Setea todas las grillas y las vistas
            setGrillasVistasCombo();

            //Setea todos los controles necesarios para el efecto de slide
            SetSlide();

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        //Método para evitar la creación de más de una pantalla
        public static frmPieza Instancia
        {
            get
            {
                if (_frmPieza == null || _frmPieza.IsDisposed)
                {
                    _frmPieza = new frmPieza();
                }
                else
                {
                    _frmPieza.BringToFront();
                }
                return _frmPieza;
            }
            set
            {
                _frmPieza = value;
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
                dsEstructura.PIEZAS.Clear();
                dsEstructura.MATERIAS_PRIMAS.Clear();
                dsEstructura.DETALLE_PIEZA.Clear();

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.PiezaBLL.ObtenerPiezas(txtNombreBuscar.Text, cbTerminacionBuscar.SelectedValue, dsEstructura);

                if (dsEstructura.PIEZAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Piezas con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvPiezas.Table = dsEstructura.PIEZAS;
                dvDetallePieza.Table = dsEstructura.DETALLE_PIEZA;
                dvMPDisponibles.Table = dsEstructura.MATERIAS_PRIMAS;

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Pieza - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
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
            //Controlamos que esté seleccionado algo
            if (dgvPiezas.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la pieza seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        int codigo = Convert.ToInt32(dvPiezas[dgvPiezas.SelectedRows[0].Index]["pza_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.PiezaBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).Delete();
                        dsEstructura.PIEZAS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
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
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Datos opcionales = plano, descripcion, imagen
            //Revisamos que completó los datos obligatorios
            bool datosOK = true;
            if (txtNombre.Text == String.Empty) { datosOK = false; }
            if (((Sistema.Item)cbTerminacion.SelectedItem).Value == 0) { datosOK = false; }
            if (((Sistema.Item)cbEstado.SelectedItem).Value == 0) { datosOK = false; }
            if (dgvDetallePieza.Rows.GetRowCount(DataGridViewElementStates.Selected) == 0) { datosOK = false; }
            if (datosOK)
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Como ahora tenemos más de una tabla y relacionadas vamos a trabajar diferente
                        //Primero lo agregamos a la tabla Piezas del dataset con código -1, luego la entidad 
                        //PiezaDAL se va a encargar de insertarle el código que corresponda y el stock inicial
                        Data.dsEstructura.PIEZASRow rowPieza = dsEstructura.PIEZAS.NewPIEZASRow();
                        rowPieza.BeginEdit();
                        rowPieza.PZA_CODIGO = -1;
                        rowPieza.PZA_NOMBRE = txtNombre.Text;
                        rowPieza.TE_CODIGO = ((Sistema.Item)cbTerminacion.SelectedItem).Value;
                        rowPieza.PAR_CODIGO = ((Sistema.Item)cbEstado.SelectedItem).Value;
                        rowPieza.PNO_CODIGO = ((Sistema.Item)cbEstado.SelectedItem).Value;
                        rowPieza.PZA_DESCRIPCION = txtDescripcion.Text;
                        rowPieza.EndEdit();
                        dsEstructura.PIEZAS.AddPIEZASRow(rowPieza);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad PiezaBLL y PiezaDAL sepan cuales insertar
                        BLL.PiezaBLL.Insertar(dsEstructura);
                        //Guardamos la imagen de la pieza, no importa si no la cargo ConjuntoBLL se encarga de determinar eso                        
                        BLL.PiezaBLL.GuardarImagen(Convert.ToInt32(rowPieza.PZA_CODIGO), pbImagen.Image);
                        //Ahora si aceptamos los cambios
                        dsEstructura.PIEZAS.AcceptChanges();
                        dsEstructura.DETALLE_PIEZA.AcceptChanges();
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
                        //Ya existe la pieza, descartamos los cambios pero sólo de piezas ya que puede querer
                        //modificar el nombre y/o la terminación e intentar de nuevo con la estructura cargada
                        dsEstructura.PIEZAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.PIEZAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    int codigoPieza = Convert.ToInt32(dvPiezas[dgvPiezas.SelectedRows[0].Index]["pza_codigo"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, la estructura se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_NOMBRE = txtNombre.Text;
                    dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).TE_CODIGO = Convert.ToInt32(cbTerminacion.SelectedValue);
                    dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_DESCRIPCION = txtDescripcion.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.PiezaBLL.Actualizar(dsEstructura);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsEstructura.PIEZAS.AcceptChanges();
                        dsEstructura.DETALLE_PIEZA.AcceptChanges();
                        //Actualizamos la imagen
                        BLL.PiezaBLL.GuardarImagen(codigoPieza, pbImagen.Image);
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de conjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.PIEZAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvPiezas.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetallePieza.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDPZA = Convert.ToInt32(dvDetallePieza[dgvDetallePieza.SelectedRows[0].Index]["dpza_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEstructura.DETALLE_PIEZA.FindByDPZA_CODIGO(codigoDPZA).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Materia Prima de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvDetallePieza.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDPZA = Convert.ToInt32(dvDetallePieza[dgvDetallePieza.SelectedRows[0].Index]["dpza_codigo"]);
                //Aumentamos la cantidad
                dsEstructura.DETALLE_PIEZA.FindByDPZA_CODIGO(codigoDPZA).DPZA_CANTIDAD++; ;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Materia Prima de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvDetallePieza.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDPZA = Convert.ToInt32(dvDetallePieza[dgvDetallePieza.SelectedRows[0].Index]["dpza_codigo"]);
                //Disminuimos la cantidad
                dsEstructura.DETALLE_PIEZA.FindByDPZA_CODIGO(codigoDPZA).DPZA_CANTIDAD--; ;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Materia Prima de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvMPDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidad.Value > 0)
            {
                bool agregarMP; //variable que indica si se debe agregar la materia prima al listado
                //Obtenemos el código de la pieza según sea nueva o modificada, lo hacemos acá porque lo vamos a usar mucho
                int piezaCodigo;
                if (estadoInterface == estadoUI.nuevo) { piezaCodigo = 0; }
                else { piezaCodigo = Convert.ToInt32(dvPiezas[dgvPiezas.SelectedRows[0].Index]["pza_codigo"]); }
                //Obtenemos el código de la materia prima, también lo vamos a usar mucho
                int materiaPrimaCodigo = Convert.ToInt32(dvMPDisponibles[dgvMPDisponibles.SelectedRows[0].Index]["mp_codigo"]);

                //Primero vemos si la pieza tiene algúna materia prima cargada, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvDetallePieza.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar la misma materia prima haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "pza_codigo = " + piezaCodigo + " AND mp_codigo = " + materiaPrimaCodigo;
                    Data.dsEstructura.DETALLE_PIEZARow[] rows =
                        (Data.dsEstructura.DETALLE_PIEZARow[])dsEstructura.DETALLE_PIEZA.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("La Pieza ya posee la materia prima seleccionada. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].DPZA_CANTIDAD += Convert.ToInt32(nudCantidad.Value);
                            nudCantidad.Value = 0;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarMP = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarMP = true;
                    }
                }
                else
                {
                    //No tiene ningúna materia prima agregada, marcamos que debe agregarse
                    agregarMP = true;
                }

                //Ahora comprobamos si debe agregarse la materia prima o no
                if (agregarMP)
                {
                    Data.dsEstructura.DETALLE_PIEZARow row = dsEstructura.DETALLE_PIEZA.NewDETALLE_PIEZARow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.DPZA_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.PZA_CODIGO = piezaCodigo;
                    row.MP_CODIGO = materiaPrimaCodigo;
                    row.DPZA_CANTIDAD = Convert.ToInt32(nudCantidad.Value);
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.DETALLE_PIEZA.AddDETALLE_PIEZARow(row);
                    nudCantidad.Value = 0;
                }
                nudCantidad.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una materia prima de la lista y asignarle una cantidad mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHecho_Click(object sender, EventArgs e)
        {
            slideControl.BackwardTo("slideDatos");
            nudCantidad.Value = 0;
            panelAcciones.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {            
            SetInterface(estadoUI.inicio);
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            ofdImagen.ShowDialog();
        }

        private void ofdImagen_FileOk(object sender, CancelEventArgs e)
        {
            pbImagen.ImageLocation = ofdImagen.FileName;
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            pbImagen.Image = EstructuraProducto.Properties.Resources.sinimagen;
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos = true;

                    if (dsEstructura.PIEZAS.Rows.Count == 0)
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
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.inicio;
                    tcConjunto.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    cbTerminacion.Enabled = true;
                    cbTerminacion.SelectedIndex = -1;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    cbTerminacion.Enabled = true;
                    cbTerminacion.SelectedIndex = -1;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    cbTerminacion.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    cbTerminacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Evento RowEnter de la grilla, va cargando los datos en la pestaña Datos a medida que se
        //hace clic en alguna fila de la grilla
        private void dgvPiezas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoPieza = Convert.ToInt32(dvPiezas[e.RowIndex]["pza_codigo"]);
            txtNombre.Text = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_NOMBRE;
            cbTerminacion.SelectedValue = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).TE_CODIGO;
            txtDescripcion.Text = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_DESCRIPCION;
            pbImagen.Image = BLL.PiezaBLL.ObtenerImagen(codigoPieza);
            //Usemos el filtro del dataview para mostrar sólo las MP de la pieza seleccionada
            dvDetallePieza.RowFilter = "pza_codigo = " + codigoPieza;
        }

        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvPiezas_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbMPDisponibles.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
            //panelDatos.Location = new Point(0, 10);
            //panelAgregar.Location = new Point(7, 7);
            //panelImagen.Location = new Point(405, 15);
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvPiezas.AutoGenerateColumns = false;
            dgvDetallePieza.AutoGenerateColumns = false;
            dgvMPDisponibles.AutoGenerateColumns = false;
            //Agregamos las columnas y sus propiedades
            dgvPiezas.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPiezas.Columns.Add("TE_CODIGO", "Terminación");
            dgvPiezas.Columns.Add("PAR_CODIGO", "Estado");
            dgvPiezas.Columns.Add("PNO_PLANO", "Plano");
            dgvPiezas.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezas.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezas.Columns["PAR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezas.Columns["PNO_PLANO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
           
            dgvDetallePieza.Columns.Add("MP_NOMBRE", "Nombre");
            dgvDetallePieza.Columns.Add("UMED_CODIGO", "Unidad Medida");
            dgvDetallePieza.Columns.Add("DPZA_CANTIDAD", "Cantidad");
            dgvDetallePieza.Columns["MP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePieza.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePieza.Columns["DPZA_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePieza.Columns["DPZA_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvMPDisponibles.Columns.Add("MP_NOMBRE", "Nombre");
            dgvMPDisponibles.Columns.Add("UMED_CODIGO", "Unidad Medida");
            dgvMPDisponibles.Columns.Add("MP_DESCRIPCION", "Descripción");
            dgvMPDisponibles.Columns["MP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPDisponibles.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPDisponibles.Columns["MP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMPDisponibles.Columns["MP_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvPiezas.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_NOMBRE";
            dgvPiezas.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvPiezas.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvPiezas.Columns["PNO_PLANO"].DataPropertyName = "PAR_CODIGO";

            dgvDetallePieza.Columns["MP_NOMBRE"].DataPropertyName = "MP_CODIGO";
            dgvDetallePieza.Columns["UMED_CODIGO"].DataPropertyName = "MP_CODIGO";
            dgvDetallePieza.Columns["DPZA_CANTIDAD"].DataPropertyName = "DPZA_CANTIDAD";

            dgvMPDisponibles.Columns["MP_NOMBRE"].DataPropertyName = "MP_NOMBRE";
            dgvMPDisponibles.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvMPDisponibles.Columns["MP_DESCRIPCION"].DataPropertyName = "MP_DESCRIPCION";

            //Creamos el dataview y lo asignamos a la grilla
            dvPiezas = new DataView(dsEstructura.PIEZAS);
            dvPiezas.Sort = "PZA_NOMBRE ASC";
            dgvPiezas.DataSource = dvPiezas;
            dvDetallePieza = new DataView(dsEstructura.DETALLE_PIEZA);
            
            dgvDetallePieza.DataSource = dvDetallePieza;
            dvMPDisponibles = new DataView(dsEstructura.MATERIAS_PRIMAS);
            dvMPDisponibles.Sort = "MP_NOMBRE ASC";
            dgvMPDisponibles.DataSource = dvMPDisponibles;
            dvUnidadMedida = new DataView(dsUnidadMedida.UNIDADES_MEDIDA);
            dvEstado = new DataView(dsEstructura.ESTADO_PARTES);
            dvEstado.Sort = "PAR_NOMBRE";
            dvPlano = new DataView(dsEstructura.PLANOS);
            dvPlano.Sort = "PNO_NOMBRE";

            //Obtenemos las terminaciones, los planos, los estados de las piezas, las MP y las terminaciones
            
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.EstadoParteBLL.ObtenerTodos(dsEstructura.ESTADO_PARTES);
                BLL.MateriaPrimaBLL.ObtenerTodos(dsEstructura.MATERIAS_PRIMAS);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsUnidadMedida.UNIDADES_MEDIDA);
                BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsUnidadMedida.TIPOS_UNIDADES_MEDIDA);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dvTerminaciones = new DataView(dsEstructura.TERMINACIONES);
            dvTerminaciones.Sort = "TE_NOMBRE ASC";
            dvTerminacionBuscar = new DataView(dsEstructura.TERMINACIONES);
            dvTerminacionBuscar.Sort = "TE_NOMBRE ASC";
            Sistema.FuncionesAuxiliares.llenarCombosFiltros(dvTerminacionBuscar, cbTerminacionBuscar, "te_codigo", "te_nombre", "--TODOS--");
            Sistema.FuncionesAuxiliares.llenarCombosFiltros(dvTerminaciones, cbTerminacion, "te_codigo", "te_nombre", "--SELECCIONE--");
            Sistema.FuncionesAuxiliares.llenarCombosFiltros(dvEstado, cbEstado, "par_codigo", "par_nombre", "--SELECCIONE--");
            Sistema.FuncionesAuxiliares.llenarCombosFiltros(dvPlano, cbPlano, "pno_codigo", "pno_nombre", "--SELECCIONE--");

            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";
        }

        private void dgvPiezas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;

                switch (dgvPiezas.Columns[e.ColumnIndex].Name)
                {
                    case "te_codigo":
                        nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "par_codigo":
                        nombre = dsEstructura.ESTADO_PARTES.FindByPAR_CODIGO(Convert.ToInt32(e.Value.ToString())).PAR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "pno_codigo":
                        nombre = dsEstructura.PLANOS.FindByPNO_CODIGO(Convert.ToInt32(e.Value.ToString())).PNO_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void dgvDetallePieza_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvDetallePieza.Columns[e.ColumnIndex].Name)
                {
                    case "MP_NOMBRE":
                        nombre = dsEstructura.MATERIAS_PRIMAS.FindByMP_CODIGO(Convert.ToInt32(e.Value.ToString())).MP_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "UMED_CODIGO":
                        decimal codigoUnidad = dsEstructura.MATERIAS_PRIMAS.FindByMP_CODIGO(Convert.ToInt32(e.Value.ToString())).UMED_CODIGO;
                        nombre = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(codigoUnidad).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvMPDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvDetallePieza.Columns[e.ColumnIndex].Name == "UMED_CODIGO")
                {
                    nombre = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value.ToString())).UMED_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        #endregion 
    }
}
