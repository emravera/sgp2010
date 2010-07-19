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
    public partial class frmConjunto : Form
    {
        private static frmConjunto _frmConjunto = null;
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsTerminacion dsTerminacion = new GyCAP.Data.dsTerminacion();
        private DataView dvConjuntos, dvDetalleConjunto, dvSubconjuntosDisponibles;
        private DataView dvTerminacionBuscar, dvTerminaciones;
        private enum estadoUI { inicio, nuevo, consultar, modificar };
        private estadoUI estadoInterface;
        
        public frmConjunto()
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
        public static frmConjunto Instancia
        {
            get
            {
                if (_frmConjunto == null || _frmConjunto.IsDisposed)
                {
                    _frmConjunto = new frmConjunto();
                }
                else
                {
                    _frmConjunto.BringToFront();
                }
                return _frmConjunto;
            }
            set
            {
                _frmConjunto = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Vemos si hay cambios sin guardar
            //if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.modificar)
            //{
                //Hay cambios sin guardar, preguntemos si igual quiere salir
                //DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea salir, se perderán todos los cambios efectuados?", "Confirmar", MessageBoxButtons.YesNo);
                //if (respuesta == DialogResult.Yes)
                //{
                    //No quiere guardar nada, descartamos los cambios
                    //dsEstructura.SUBCONJUNTOS.RejectChanges();
                    //dsEstructura.DETALLE_CONJUNTO.RejectChanges();
                    //Salimos
                    this.Dispose(true);
                //}
            //}
            //else
            //{
                //No hay cambios sin guardar, Salimos
                //this.Dispose(true);
            //}            
        }

        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsEstructura.CONJUNTOS.Clear();
                dsEstructura.SUBCONJUNTOS.Clear();
                dsEstructura.DETALLE_CONJUNTO.Clear();
                
                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.ConjuntoBLL.ObtenerConjuntos(txtNombreBuscar.Text, cbTerminacionBuscar.SelectedValue, dsEstructura);
                    
                if (dsEstructura.CONJUNTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron conjuntos con los criterios de búsqueda ingresados.", "Aviso");
                }           
                
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvConjuntos.Table = dsEstructura.CONJUNTOS;
                dvDetalleConjunto.Table = dsEstructura.DETALLE_CONJUNTO;
                dvSubconjuntosDisponibles.Table = dsEstructura.SUBCONJUNTOS;
                
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
                SetInterface(estadoUI.inicio);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
            if (dgvConjuntos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el conjunto seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        int codigo = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.ConjuntoBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).Delete();
                        dsEstructura.CONJUNTOS.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar un conjunto de la lista.", "Aviso");
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {            
            //Revisamos que completó los datos
            if (txtNombre.Text != String.Empty && cbTerminacion.SelectedIndex != -1 && dgvDetalleConjunto.Rows.Count != 0)
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Como ahora tenemos más de una tabla y relacionadas vamos a trabajar diferente
                        //Primero lo agregamos a la tabla Conjuntos del dataset con código 0, luego la entidad 
                        //ConjuntoDAL se va a encargar de insertarle el código que corresponda y el stock inicial
                        Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.NewCONJUNTOSRow();
                        rowConjunto.BeginEdit();
                        rowConjunto.CONJ_CODIGO = 0;
                        rowConjunto.CONJ_NOMBRE = txtNombre.Text;
                        rowConjunto.TE_CODIGO = Convert.ToInt32(cbTerminacion.SelectedValue.ToString());
                        rowConjunto.EndEdit();
                        dsEstructura.CONJUNTOS.AddCONJUNTOSRow(rowConjunto);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad ConjuntoBLL y ConjuntoDAL sepan cuales insertar
                        BLL.ConjuntoBLL.Insertar(dsEstructura);                        
                        //Guardamos la imagen del conjunto, no importa si no la cargo ConjuntoBLL se encarga de determinar eso                        
                        BLL.ConjuntoBLL.GuardarImagen(Convert.ToInt32(rowConjunto.CONJ_CODIGO), pbImagen.Image);
                        //Ahora si aceptamos los cambios
                        dsEstructura.CONJUNTOS.AcceptChanges();
                        dsEstructura.DETALLE_CONJUNTO.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        //Ya existe el conjunto, descartamos los cambios pero sólo de conjuntos ya que puede querer
                        //modificar el nombre y/o la terminación e intentar de nuevo con la estructura cargada
                        dsEstructura.CONJUNTOS.RejectChanges();                        
                        MessageBox.Show(ex.Message);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de conjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    Entidades.Conjunto conjunto = new GyCAP.Entidades.Conjunto();
                    conjunto.CodigoConjunto = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, la estructura se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    conjunto.Nombre = txtNombre.Text;
                    conjunto.CodigoTerminacion = Convert.ToInt32(cbTerminacion.SelectedValue);
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ConjuntoBLL.Actualizar(dsEstructura);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsEstructura.CONJUNTOS.AcceptChanges();
                        dsEstructura.DETALLE_CONJUNTO.AcceptChanges();
                        //Actualizamos la imagen
                        BLL.ConjuntoBLL.GuardarImagen(conjunto.CodigoConjunto, pbImagen.Image);
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Aviso");
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de conjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        MessageBox.Show(ex.Message);
                    }
                }
                dgvConjuntos.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar todos los datos.", "Aviso");
            }
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetalleConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvDetalleConjunto[dgvDetalleConjunto.SelectedRows[0].Index]["sxc_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEstructura.DETALLE_CONJUNTO.FindByDCJ_CODIGO(codigoDCJ).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un subconjunto de la lista.", "Aviso");
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvDetalleConjunto[dgvDetalleConjunto.SelectedRows[0].Index]["sxc_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEstructura.DETALLE_CONJUNTO.FindByDCJ_CODIGO(codigoDCJ).DCJ_CANTIDAD++; ;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un subconjunto de la lista.", "Aviso");
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvDetalleConjunto[dgvDetalleConjunto.SelectedRows[0].Index]["sxc_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEstructura.DETALLE_CONJUNTO.FindByDCJ_CODIGO(codigoDCJ).DCJ_CANTIDAD--; ;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un subconjunto de la lista.", "Aviso");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvSCDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidad.Value > 0)
            {
                bool agregarSubconjunto; //variable que indica si se debe agregar el subconjunto al listado
                //Obtenemos el código del conjunto según sea nuevo o modificado, lo hacemos acá porque lo vamos a usar mucho
                int conjuntoCodigo;
                if (estadoInterface == estadoUI.nuevo) { conjuntoCodigo = 0; }
                else { conjuntoCodigo = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]); }
                //Obtenemos el código del subconjunto, también lo vamos a usar mucho
                int subconjuntoCodigo = Convert.ToInt32(dvSubconjuntosDisponibles[dgvSCDisponibles.SelectedRows[0].Index]["sconj_codigo"]);

                //Primero vemos si el conjunto tiene algún subconjunto cargado, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvDetalleConjunto.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar el mismo subconjunto haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "conj_codigo = " + conjuntoCodigo + " AND sconj_codigo = " + subconjuntoCodigo;
                    Data.dsEstructura.DETALLE_CONJUNTORow[] rows =
                        (Data.dsEstructura.DETALLE_CONJUNTORow[])dsEstructura.DETALLE_CONJUNTO.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("El conjunto ya posee el subconjunto seleccionado. ¿Desea sumar la cantidad ingresada?", "Confirmar", MessageBoxButtons.YesNo);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].DCJ_CANTIDAD += nudCantidad.Value;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarSubconjunto = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarSubconjunto = true;
                    }
                }
                else
                {
                    //No tiene ningún subconjunto agregado, marcamos que debe agregarse
                    agregarSubconjunto = true;
                }

                //Ahora comprobamos si debe agregarse el subconjunto o no
                if (agregarSubconjunto)
                {
                    Data.dsEstructura.DETALLE_CONJUNTORow row = dsEstructura.DETALLE_CONJUNTO.NewDETALLE_CONJUNTORow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con código 0, luego al guardar en la db se actualizará
                    row.DCJ_CODIGO = 0;
                    row.CONJ_CODIGO = conjuntoCodigo;
                    row.SCONJ_CODIGO = subconjuntoCodigo;
                    row.DCJ_CANTIDAD = nudCantidad.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.DETALLE_CONJUNTO.AddDETALLE_CONJUNTORow(row);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un subconjunto de la lista y asignarle una cantidad mayor a 0.", "Aviso");
            }
        }
        
        private void btnHecho_Click(object sender, EventArgs e)
        {
            slideControl.BackwardTo("slideDatos");
            panelAcciones.Enabled = true;
        }
        
        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Vemos si hay cambios sin guardar
            //if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.modificar)
            //{
                //Hay cambios sin guardar, preguntemos si igual quiere volver
                //DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea volver, se perderán todos los cambios efectuados?", "Confirmar", MessageBoxButtons.YesNo);
                //if (respuesta == DialogResult.Yes)
                //{
                    //No quiere guardar nada, descartamos los cambios
                    //dsEstructura.SUBCONJUNTOS.RejectChanges();
                    //dsEstructura.DETALLE_CONJUNTO.RejectChanges();
                    //Seteamos la interfaz
                    SetInterface(estadoUI.inicio);
                //}
            //}
            //else
            //{
                //No hay cambios sin guardar, volvemos
                //SetInterface(estadoUI.inicio);
           // }
            
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

                    if (dsEstructura.CONJUNTOS.Rows.Count == 0)
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
        private void dgvConjuntos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoConjunto = Convert.ToInt32(dvConjuntos[e.RowIndex]["conj_codigo"]);
            txtNombre.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_NOMBRE;
            cbTerminacion.SelectedValue = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).TE_CODIGO;
            txtDescripcion.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_DESCRIPCION;
            pbImagen.Image = BLL.ConjuntoBLL.ObtenerImagen(codigoConjunto);
            //Usemos el filtro del dataview para mostrar sólo los subconjuntos del conjunto seleccionado
            dvDetalleConjunto.RowFilter = "conj_codigo = " + codigoConjunto;
        }
        
        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvListaConjuntos_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void SetSlide()
        {
            panelDatos.Parent = slideDatos;
            panelAgregar.Parent = slideAgregar;
            panelImagen.Parent = slideDatos;
            
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
            panelDatos.Location = new Point(0, 10);
            panelAgregar.Location = new Point(7, 7);
            panelImagen.Location = new Point(405, 15);
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvConjuntos.AutoGenerateColumns = false;
            dgvDetalleConjunto.AutoGenerateColumns = false;
            dgvSCDisponibles.AutoGenerateColumns = false;
            //Agregamos las columnas y sus propiedades
            dgvConjuntos.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvConjuntos.Columns.Add("TE_CODIGO", "Terminación");
            dgvConjuntos.Columns.Add("CONJ_DESCRIPCION", "Descripción");
            dgvConjuntos.Columns["CONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            dgvDetalleConjunto.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvDetalleConjunto.Columns.Add("TE_CODIGO", "Terminación");
            dgvDetalleConjunto.Columns.Add("DCJ_CANTIDAD", "Cantidad");
            dgvDetalleConjunto.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["DCJ_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["DCJ_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvSCDisponibles.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCDisponibles.Columns.Add("TE_CODIGO", "Terminación");
            dgvSCDisponibles.Columns.Add("SCONJ_DESCRIPCION", "Descripción");
            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvConjuntos.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_NOMBRE";
            dgvConjuntos.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].DataPropertyName = "CONJ_DESCRIPCION";
            
            dgvDetalleConjunto.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_CODIGO";
            //dgvDetalleConjunto.Columns["TE_CODIGO"].DataPropertyName = "SCONJ_CODIGO";
            dgvDetalleConjunto.Columns["DCJ_CANTIDAD"].DataPropertyName = "DCJ_CANTIDAD";

            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_NOMBRE";
            dgvSCDisponibles.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].DataPropertyName = "SCONJ_DESCRIPCION";
            
            //Creamos el dataview y lo asignamos a la grilla
            dvConjuntos = new DataView(dsEstructura.CONJUNTOS);
            dgvConjuntos.DataSource = dvConjuntos;
            dvDetalleConjunto = new DataView(dsEstructura.DETALLE_CONJUNTO);
            dgvDetalleConjunto.DataSource = dvDetalleConjunto;
            dvSubconjuntosDisponibles = new DataView(dsEstructura.SUBCONJUNTOS);
            dgvSCDisponibles.DataSource = dvSubconjuntosDisponibles;
            
            //Obtenemos las terminaciones
            //Codigo chancho por no pensar bien antes como trabajar
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsTerminacion);
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
            }

            dvTerminaciones = new DataView(dsTerminacion.TERMINACIONES);
            cbTerminacion.DataSource = dvTerminaciones;
            cbTerminacion.DisplayMember = "te_nombre";
            cbTerminacion.ValueMember = "te_codigo";
            cbTerminacion.SelectedIndex = -1;
            dvTerminacionBuscar = new DataView(dsTerminacion.TERMINACIONES);
            cbTerminacionBuscar.DataSource = dvTerminacionBuscar;
            cbTerminacionBuscar.DisplayMember = "te_nombre";
            cbTerminacionBuscar.ValueMember = "te_codigo";
            cbTerminacionBuscar.SelectedIndex = -1;
            
            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            
            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.ico, *.jpeg, *.png)|*.bmp;*.gif;*.ico;*.jpg;*.png|Todos los archivos (*.*)|*.*";
        }
                
        private void dgvDetalleConjunto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigoSubConjunto, codigoTerminacion;
                switch (dgvDetalleConjunto.Columns[e.ColumnIndex].Name)
                {
                    case "te_codigo":
                        codigoSubConjunto = Convert.ToInt32(dvDetalleConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        codigoTerminacion = Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoSubConjunto).TE_CODIGO.ToString()); ;
                        nombre = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "sconj_nombre":
                        codigoSubConjunto = Convert.ToInt32(dvDetalleConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubConjunto).SCONJ_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvSCDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvSCDisponibles.Columns[e.ColumnIndex].Name == "te_codigo")
                {
                    nombre = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
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
