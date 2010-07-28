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
        private DataView dvConjuntos, dvDetalleConjunto, dvSubconjuntosDisponibles;
        private DataView dvTerminacionBuscar, dvTerminaciones, dvEstados, dvPlanos;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1; 
                
        public frmConjunto()
        {
            InitializeComponent();

            //Setea todas las grillas y las vistas
            setGrillasVistasCombo();
            
            //Setea todos los controles necesarios para el efecto de slide
            SetSlide();
            
            //Seteamos el estado de la interfaz
            //SetInterface(estadoUI.inicio);
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

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
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
                dsEstructura.DETALLE_CONJUNTO.Clear();
                
                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.ConjuntoBLL.ObtenerConjuntos(txtNombreBuscar.Text, cbTerminacionBuscar.GetSelectedValueInt(), dsEstructura, true);
                    
                if (dsEstructura.CONJUNTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Conjuntos con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Error: Conjunto - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvConjuntos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el conjunto seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        MessageBox.Show(ex.Message, "Error: Conjunto - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Conjunto - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool datosOK = true;
            //Revisamos que completó los datos
            if (txtNombre.Text == string.Empty) { datosOK = false; }
            if (cbTerminacion.SelectedIndex == -1) { datosOK = false; }
            if (cbEstado.SelectedIndex == -1) { datosOK = false; }
            if (cbPlano.SelectedIndex == -1) { datosOK = false; }
            if (dgvDetalleConjunto.Rows.Count == 0) { datosOK = false; }
            if (datosOK)
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Como ahora tenemos más de una tabla y relacionadas vamos a trabajar diferente
                        //Primero lo agregamos a la tabla Conjuntos del dataset con código -1, luego la entidad 
                        //ConjuntoDAL se va a encargar de insertarle el código que corresponda y el stock inicial
                        Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.NewCONJUNTOSRow();
                        rowConjunto.BeginEdit();
                        rowConjunto.CONJ_CODIGO = -1;
                        rowConjunto.CONJ_CODIGOPARTE = txtCodigo.Text;
                        rowConjunto.CONJ_NOMBRE = txtNombre.Text;
                        rowConjunto.TE_CODIGO = cbTerminacion.GetSelectedValueInt();
                        rowConjunto.PAR_CODIGO = cbEstado.GetSelectedValueInt();
                        rowConjunto.PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        rowConjunto.CONJ_DESCRIPCION = txtDescripcion.Text;
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
                        //Ya existe el conjunto, descartamos los cambios pero sólo de conjuntos ya que puede querer
                        //modificar el nombre y/o la terminación e intentar de nuevo con la estructura cargada
                        dsEstructura.CONJUNTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de conjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.CONJUNTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    int codigoConjunto = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, la estructura se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_CODIGOPARTE = txtCodigo.Text;
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_NOMBRE = txtNombre.Text;
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).TE_CODIGO = cbTerminacion.GetSelectedValueInt();
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_DESCRIPCION = txtDescripcion.Text;
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PAR_CODIGO = cbEstado.GetSelectedValueInt();
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PNO_CODIGO = cbPlano.GetSelectedValueInt();
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ConjuntoBLL.Actualizar(dsEstructura);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsEstructura.CONJUNTOS.AcceptChanges();
                        dsEstructura.DETALLE_CONJUNTO.AcceptChanges();
                        //Actualizamos la imagen
                        BLL.ConjuntoBLL.GuardarImagen(codigoConjunto, pbImagen.Image);
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de conjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.CONJUNTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvConjuntos.Refresh();
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
            if (dgvDetalleConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvDetalleConjunto[dgvDetalleConjunto.SelectedRows[0].Index]["dcj_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEstructura.DETALLE_CONJUNTO.FindByDCJ_CODIGO(codigoDCJ).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvDetalleConjunto[dgvDetalleConjunto.SelectedRows[0].Index]["dcj_codigo"]);
                //Aumentamos la cantidad
                dsEstructura.DETALLE_CONJUNTO.FindByDCJ_CODIGO(codigoDCJ).DCJ_CANTIDAD += Convert.ToDecimal(0.1);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvDetalleConjunto[dgvDetalleConjunto.SelectedRows[0].Index]["dcj_codigo"]);
                //Disminuimos la cantidad
                dsEstructura.DETALLE_CONJUNTO.FindByDCJ_CODIGO(codigoDCJ).DCJ_CANTIDAD -= Convert.ToDecimal(0.1);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        DialogResult respuesta = MessageBox.Show("El conjunto ya posee el subconjunto seleccionado. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].DCJ_CANTIDAD += nudCantidad.Value;
                            nudCantidad.Value = 0;
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
                    //Modificado 23/07/2010
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.DCJ_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.CONJ_CODIGO = conjuntoCodigo;
                    row.SCONJ_CODIGO = subconjuntoCodigo;
                    row.DCJ_CANTIDAD = nudCantidad.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.DETALLE_CONJUNTO.AddDETALLE_CONJUNTORow(row);
                    nudCantidad.Value = 0;
                }
                nudCantidad.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un subconjunto de la lista y asignarle una cantidad mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    txtNombreBuscar.Focus();
                    break;                
                case estadoUI.nuevo:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    chkAutogenerar.Enabled = true;
                    chkAutogenerar.Checked = false;
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbTerminacion.Enabled = true;
                    cbTerminacion.SetTexto("Seleccione");
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    cbPlano.Enabled = true;
                    cbPlano.SetTexto("Seleccione");
                    dvDetalleConjunto.RowFilter = "DCJ_CODIGO < 0";
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
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    chkAutogenerar.Enabled = true;
                    chkAutogenerar.Checked = false;
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbTerminacion.Enabled = true;
                    cbTerminacion.SetTexto("Seleccione");
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    cbPlano.Enabled = true;
                    cbPlano.SetTexto("Seleccione");
                    dvDetalleConjunto.RowFilter = "DCJ_CODIGO < 0";
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
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    chkAutogenerar.Enabled = false;
                    chkAutogenerar.Checked = false;
                    cbTerminacion.Enabled = false;
                    cbEstado.Enabled = false;
                    cbPlano.Enabled = false;
                    cbTerminacion.SetTexto(string.Empty);
                    cbEstado.SetTexto(string.Empty);
                    cbPlano.SetTexto(string.Empty);
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    chkAutogenerar.Enabled = true;
                    chkAutogenerar.Checked = false;
                    txtNombre.ReadOnly = false;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    cbPlano.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbTerminacion.SetTexto(string.Empty);
                    cbEstado.SetTexto(string.Empty);
                    cbPlano.SetTexto(string.Empty);
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
                    txtCodigo.Focus();
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
            txtCodigo.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_CODIGOPARTE;
            txtNombre.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_NOMBRE;
            cbTerminacion.SetSelectedValue(Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).TE_CODIGO));
            cbEstado.SetSelectedValue(Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PAR_CODIGO));
            cbPlano.SetSelectedValue(Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PNO_CODIGO));
            txtDescripcion.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_DESCRIPCION;
            pbImagen.Image = BLL.ConjuntoBLL.ObtenerImagen(codigoConjunto);
            //Usemos el filtro del dataview para mostrar sólo los subconjuntos del conjunto seleccionado
            dvDetalleConjunto.RowFilter = "conj_codigo = " + codigoConjunto;
        }
        
        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvConjuntos_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void SetSlide()
        {
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            gbDatos.Parent = slideDatos;
            gbSCD.Parent = slideAgregar;
            slideControl.Selected = slideDatos;           
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvConjuntos.AutoGenerateColumns = false;
            dgvDetalleConjunto.AutoGenerateColumns = false;
            dgvSCDisponibles.AutoGenerateColumns = false;
            //Agregamos las columnas y sus propiedades
            dgvConjuntos.Columns.Add("CONJ_CODIGOPARTE", "Código");
            dgvConjuntos.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvConjuntos.Columns.Add("TE_CODIGO", "Terminación");
            dgvConjuntos.Columns.Add("CONJ_DESCRIPCION", "Descripción");
            dgvConjuntos.Columns["CONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            dgvDetalleConjunto.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvDetalleConjunto.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvDetalleConjunto.Columns.Add("TE_CODIGO", "Terminación");
            dgvDetalleConjunto.Columns.Add("DCJ_CANTIDAD", "Cantidad");
            dgvDetalleConjunto.Columns["SCONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["DCJ_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleConjunto.Columns["DCJ_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvSCDisponibles.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvSCDisponibles.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCDisponibles.Columns.Add("TE_CODIGO", "Terminación");
            dgvSCDisponibles.Columns.Add("SCONJ_DESCRIPCION", "Descripción");
            dgvSCDisponibles.Columns["SCONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvConjuntos.Columns["CONJ_CODIGOPARTE"].DataPropertyName = "CONJ_CODIGOPARTE";
            dgvConjuntos.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_NOMBRE";
            dgvConjuntos.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].DataPropertyName = "CONJ_DESCRIPCION";

            dgvDetalleConjunto.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGO";
            dgvDetalleConjunto.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_CODIGO";
            dgvDetalleConjunto.Columns["TE_CODIGO"].DataPropertyName = "SCONJ_CODIGO";
            dgvDetalleConjunto.Columns["DCJ_CANTIDAD"].DataPropertyName = "DCJ_CANTIDAD";

            dgvSCDisponibles.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGOPARTE";
            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_NOMBRE";
            dgvSCDisponibles.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].DataPropertyName = "SCONJ_DESCRIPCION";
            
            //Obtenemos las terminaciones, estados, planos, subconjuntos            
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.EstadoParteBLL.ObtenerTodos(dsEstructura.ESTADO_PARTES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.SubConjuntoBLL.ObtenerSubconjuntos(dsEstructura.SUBCONJUNTOS);                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Dataviews de grillas
            dvConjuntos = new DataView(dsEstructura.CONJUNTOS);
            dvConjuntos.Sort = "CONJ_NOMBRE ASC";
            dgvConjuntos.DataSource = dvConjuntos;
            dvDetalleConjunto = new DataView(dsEstructura.DETALLE_CONJUNTO);
            dgvDetalleConjunto.DataSource = dvDetalleConjunto;
            dvSubconjuntosDisponibles = new DataView(dsEstructura.SUBCONJUNTOS);
            dvSubconjuntosDisponibles.Sort = "SCONJ_NOMBRE ASC";
            dgvSCDisponibles.DataSource = dvSubconjuntosDisponibles;

            //Dataviews de combos
            dvTerminaciones = new DataView(dsEstructura.TERMINACIONES);
            cbTerminacion.SetDatos(dvTerminaciones, "te_codigo", "te_nombre", "Seleccione", false);
            dvTerminacionBuscar = new DataView(dsEstructura.TERMINACIONES);
            cbTerminacionBuscar.SetDatos(dvTerminacionBuscar, "te_codigo", "te_nombre", "--TODOS--", true);
            dvEstados = new DataView(dsEstructura.ESTADO_PARTES);
            dvPlanos = new DataView(dsEstructura.PLANOS);
            cbEstado.SetDatos(dvEstados, "par_codigo", "par_nombre", "Seleccione", false);
            cbPlano.SetDatos(dvPlanos, "pno_codigo", "pno_nombre", "Seleccione", false);
            
            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            
            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";
            
        }

        private void dgvConjuntos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvConjuntos.Columns[e.ColumnIndex].Name == "TE_CODIGO")
                {
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                }
            }
        }
        
        private void dgvDetalleConjunto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigoSubConjunto, codigoTerminacion;
                switch (dgvDetalleConjunto.Columns[e.ColumnIndex].Name)
                {
                    case "TE_CODIGO":
                        codigoSubConjunto = Convert.ToInt32(dvDetalleConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        codigoTerminacion = Convert.ToInt32(dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubConjunto).TE_CODIGO.ToString()); ;
                        nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "SCONJ_NOMBRE":
                        codigoSubConjunto = Convert.ToInt32(dvDetalleConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubConjunto).SCONJ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "SCONJ_CODIGOPARTE":
                        codigoSubConjunto = Convert.ToInt32(dvDetalleConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubConjunto).SCONJ_CODIGOPARTE;
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
                if (dgvSCDisponibles.Columns[e.ColumnIndex].Name == "TE_CODIGO")
                {
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCantidad.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }  
        
        #endregion        

    }
}
