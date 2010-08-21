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
    public partial class frmSubconjunto : Form
    {
        private static frmSubconjunto _frmSubconjunto = null;
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        //private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        private DataView dvSubconjuntos, dvDetalleSubconjunto, dvPiezasDisponibles;
        private DataView dvEstados, dvPlanos;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;
        
        public frmSubconjunto()
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
        public static frmSubconjunto Instancia
        {
            get
            {
                if (_frmSubconjunto == null || _frmSubconjunto.IsDisposed)
                {
                    _frmSubconjunto = new frmSubconjunto();
                }
                else
                {
                    _frmSubconjunto.BringToFront();
                }
                return _frmSubconjunto;
            }
            set
            {
                _frmSubconjunto = value;
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
                dsEstructura.SUBCONJUNTOS.Clear();
                dsEstructura.PIEZASXSUBCONJUNTO.Clear();

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.SubConjuntoBLL.ObtenerSubconjuntos(txtNombreBuscar.Text, dsEstructura, true);

                if (dsEstructura.SUBCONJUNTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Subconjuntos con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvSubconjuntos.Table = dsEstructura.SUBCONJUNTOS;
                dvDetalleSubconjunto.Table = dsEstructura.PIEZASXSUBCONJUNTO;
                dvPiezasDisponibles.Table = dsEstructura.PIEZAS;

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Subconjunto - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvSubconjuntos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el subconjunto seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        int codigo = Convert.ToInt32(dvSubconjuntos[dgvSubconjuntos.SelectedRows[0].Index]["sconj_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.SubConjuntoBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigo).Delete();
                        dsEstructura.SUBCONJUNTOS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Subconjunto - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Subconjunto - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string datosFaltantes = string.Empty;
            //Revisamos que completó los datos
            if (txtNombre.Text == string.Empty) { datosFaltantes += "* Nombre\n"; }
            if (cbEstado.SelectedIndex == -1) { datosFaltantes += "* Estado\n"; }
            if (cbPlano.SelectedIndex == -1) { datosFaltantes += "* Plano\n"; }
            if (dgvDetalleSubconjunto.Rows.Count == 0) { datosFaltantes += "* El detalle del subconjunto\n"; }
            if (datosFaltantes == string.Empty)
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Como ahora tenemos más de una tabla y relacionadas vamos a trabajar diferente
                        //Primero lo agregamos a la tabla subonjuntos del dataset con código -1, luego la entidad 
                        //SubconjuntoDAL se va a encargar de insertarle el código que corresponda y el stock inicial
                        Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = dsEstructura.SUBCONJUNTOS.NewSUBCONJUNTOSRow();
                        rowSubconjunto.BeginEdit();
                        rowSubconjunto.SCONJ_CODIGO = -1;
                        rowSubconjunto.SCONJ_CODIGOPARTE = txtCodigo.Text;
                        rowSubconjunto.SCONJ_NOMBRE = txtNombre.Text;
                        rowSubconjunto.PAR_CODIGO = cbEstado.GetSelectedValueInt();
                        rowSubconjunto.PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        rowSubconjunto.SCONJ_COSTO = nudCosto.Value;
                        rowSubconjunto.SCONJ_DESCRIPCION = txtDescripcion.Text;
                        rowSubconjunto.EndEdit();
                        dsEstructura.SUBCONJUNTOS.AddSUBCONJUNTOSRow(rowSubconjunto);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad SubconjuntoBLL y SubconjuntoDAL sepan cuales insertar
                        BLL.SubConjuntoBLL.Insertar(dsEstructura);
                        //Guardamos la imagen del subconjunto, no importa si no la cargo SubconjuntoBLL se encarga de determinar eso                        
                        BLL.SubConjuntoBLL.GuardarImagen(Convert.ToInt32(rowSubconjunto.SCONJ_CODIGO), pbImagen.Image);
                        //Ahora si aceptamos los cambios
                        dsEstructura.SUBCONJUNTOS.AcceptChanges();
                        dsEstructura.PIEZASXSUBCONJUNTO.AcceptChanges();
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
                        //Ya existe el subconjunto, descartamos los cambios pero sólo de subconjuntos ya que puede querer
                        //modificar el nombre y/o la terminación e intentar de nuevo con la estructura cargada
                        dsEstructura.SUBCONJUNTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de subconjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.SUBCONJUNTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    int codigoSubconjunto = Convert.ToInt32(dvSubconjuntos[dgvSubconjuntos.SelectedRows[0].Index]["sconj_codigo"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, la estructura se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_CODIGOPARTE = txtCodigo.Text;
                    dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_NOMBRE = txtNombre.Text;
                    dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).PAR_CODIGO = cbEstado.GetSelectedValueInt();
                    dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).PNO_CODIGO = cbPlano.GetSelectedValueInt();
                    dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_DESCRIPCION = txtDescripcion.Text;
                    dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_COSTO = nudCosto.Value;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.SubConjuntoBLL.Actualizar(dsEstructura);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsEstructura.SUBCONJUNTOS.AcceptChanges();
                        dsEstructura.PIEZASXSUBCONJUNTO.AcceptChanges();
                        //Actualizamos la imagen
                        BLL.SubConjuntoBLL.GuardarImagen(codigoSubconjunto, pbImagen.Image);
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de subconjuntos ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.SUBCONJUNTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvSubconjuntos.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetalleSubconjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDSC = Convert.ToInt32(dvDetalleSubconjunto[dgvDetalleSubconjunto.SelectedRows[0].Index]["pxsc_codigo"]);
                if (!chkCostoFijo.Checked)
                {
                    decimal costo = dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PIEZASRow.PZA_COSTO;
                    decimal cantidad = dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PXSC_CANTIDAD;
                    try { nudCosto.Value -= (costo * cantidad); }
                    catch (System.ArgumentOutOfRangeException) { nudCosto.Value = 0; }
                }
                //Lo borramos pero sólo del dataset
                dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleSubconjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDSC = Convert.ToInt32(dvDetalleSubconjunto[dgvDetalleSubconjunto.SelectedRows[0].Index]["pxsc_codigo"]);
                //Aumentamos la cantidad
                dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PXSC_CANTIDAD += 1;
                if (!chkCostoFijo.Checked)
                {
                    decimal costo = dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PIEZASRow.PZA_COSTO;
                    nudCosto.Value += costo;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleSubconjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDSC = Convert.ToInt32(dvDetalleSubconjunto[dgvDetalleSubconjunto.SelectedRows[0].Index]["pxsc_codigo"]);
                //Disminuimos la cantidad
                if (dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PXSC_CANTIDAD > 1)
                {
                    dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PXSC_CANTIDAD -= 1;
                    if (!chkCostoFijo.Checked)
                    {
                        decimal costo = dsEstructura.PIEZASXSUBCONJUNTO.FindByPXSC_CODIGO(codigoDSC).PIEZASRow.PZA_COSTO;
                        try { nudCosto.Value -= costo; }
                        catch (System.ArgumentOutOfRangeException) { nudCosto.Value = 0; }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvPiezasDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidad.Value > 0)
            {
                bool agregarPieza; //variable que indica si se debe agregar la pieza al listado
                //Obtenemos el código del subconjunto según sea nuevo o modificado, lo hacemos acá porque lo vamos a usar mucho
                int subconjuntoCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { subconjuntoCodigo = -1; }
                else { subconjuntoCodigo = Convert.ToInt32(dvSubconjuntos[dgvSubconjuntos.SelectedRows[0].Index]["sconj_codigo"]); }
                //Obtenemos el código de la pieza, también lo vamos a usar mucho
                int piezaCodigo = Convert.ToInt32(dvPiezasDisponibles[dgvPiezasDisponibles.SelectedRows[0].Index]["pza_codigo"]);

                //Primero vemos si el subconjunto tiene algúna pieza cargada, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el subconjunto seleccionado                
                if (dvDetalleSubconjunto.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar la misma pieza haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "sconj_codigo = " + subconjuntoCodigo + " AND pza_codigo = " + piezaCodigo;
                    Data.dsEstructura.PIEZASXSUBCONJUNTORow[] rows =
                        (Data.dsEstructura.PIEZASXSUBCONJUNTORow[])dsEstructura.PIEZASXSUBCONJUNTO.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("El subconjunto ya posee la pieza seleccionada. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].PXSC_CANTIDAD += nudCantidad.Value;
                            if (!chkCostoFijo.Checked) { nudCosto.Value += (rows[0].PIEZASRow.PZA_COSTO * nudCantidad.Value); }
                            nudCantidad.Value = 0;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarPieza = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarPieza = true;
                    }
                }
                else
                {
                    //No tiene ningúna pieza agregado, marcamos que debe agregarse
                    agregarPieza = true;
                }

                //Ahora comprobamos si debe agregarse la pieza o no
                if (agregarPieza)
                {
                    Data.dsEstructura.PIEZASXSUBCONJUNTORow row = dsEstructura.PIEZASXSUBCONJUNTO.NewPIEZASXSUBCONJUNTORow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.PXSC_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.SCONJ_CODIGO = subconjuntoCodigo;
                    row.PZA_CODIGO = piezaCodigo;
                    row.PXSC_CANTIDAD = nudCantidad.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.PIEZASXSUBCONJUNTO.AddPIEZASXSUBCONJUNTORow(row);
                    if (!chkCostoFijo.Checked) { nudCosto.Value += (row.PIEZASRow.PZA_COSTO * nudCantidad.Value); }
                    nudCantidad.Value = 0;
                }
                nudCantidad.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una pieza de la lista y asignarle una cantidad mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHecho_Click(object sender, EventArgs e)
        {
            nudCantidad.Value = 0;
            slideControl.BackwardTo("slideDatos");
            panelAcciones.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {            
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsEstructura.SUBCONJUNTOS.RejectChanges();
            dsEstructura.PIEZASXSUBCONJUNTO.RejectChanges();
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

                    if (dsEstructura.SUBCONJUNTOS.Rows.Count == 0)
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
                    tcSubconjunto.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    cbPlano.Enabled = true;
                    cbPlano.SetTexto("Seleccione");
                    dvDetalleSubconjunto.RowFilter = "PXSC_CODIGO < 0";
                    nudCosto.Enabled = true;
                    nudCosto.Value = 0;
                    chkCostoFijo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    panelImagen.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcSubconjunto.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    cbPlano.Enabled = true;
                    cbPlano.SetTexto("Seleccione");
                    dvDetalleSubconjunto.RowFilter = "PXSC_CODIGO < 0";
                    nudCosto.Enabled = true;
                    nudCosto.Value = 0;
                    chkCostoFijo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    panelImagen.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcSubconjunto.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    cbEstado.Enabled = false;
                    cbPlano.Enabled = false;
                    cbEstado.SetTexto(string.Empty);
                    cbPlano.SetTexto(string.Empty);
                    nudCosto.Enabled = false;
                    chkCostoFijo.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    panelImagen.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcSubconjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    cbEstado.Enabled = true;
                    cbPlano.Enabled = true;
                    cbEstado.SetTexto(string.Empty);
                    cbPlano.SetTexto(string.Empty);
                    nudCosto.Enabled = true;
                    chkCostoFijo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    panelImagen.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcSubconjunto.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                default:
                    break;
            }
        }

        //Evento RowEnter de la grilla, va cargando los datos en la pestaña Datos a medida que se
        //hace clic en alguna fila de la grilla
        private void dgvSubconjuntos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoSubconjunto = Convert.ToInt32(dvSubconjuntos[e.RowIndex]["sconj_codigo"]);
            txtCodigo.Text = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_CODIGOPARTE;
            txtNombre.Text = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_NOMBRE;
            cbEstado.SetSelectedValue(Convert.ToInt32(dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).PAR_CODIGO));
            cbPlano.SetSelectedValue(Convert.ToInt32(dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).PNO_CODIGO));
            nudCosto.Value = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_COSTO;
            txtDescripcion.Text = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubconjunto).SCONJ_DESCRIPCION;
            pbImagen.Image = BLL.SubConjuntoBLL.ObtenerImagen(codigoSubconjunto);
            //Usemos el filtro del dataview para mostrar sólo las piezas del subconjunto seleccionado
            dvDetalleSubconjunto.RowFilter = "sconj_codigo = " + codigoSubconjunto;
        }

        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvSubconjuntos_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void SetSlide()
        {
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            gbDatos.Parent = slideDatos;
            gbPD.Parent = slideAgregar;            
            slideControl.Selected = slideDatos;
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvSubconjuntos.AutoGenerateColumns = false;
            dgvDetalleSubconjunto.AutoGenerateColumns = false;
            dgvPiezasDisponibles.AutoGenerateColumns = false;
            //Agregamos las columnas y sus propiedades
            dgvSubconjuntos.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvSubconjuntos.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSubconjuntos.Columns.Add("SCONJ_COSTO", "Costo");
            dgvSubconjuntos.Columns.Add("SCONJ_DESCRIPCION", "Descripción");
            dgvSubconjuntos.Columns["SCONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntos.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntos.Columns["SCONJ_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntos.Columns["SCONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSubconjuntos.Columns["SCONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            dgvDetalleSubconjunto.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvDetalleSubconjunto.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvDetalleSubconjunto.Columns.Add("TE_CODIGO", "Terminación");
            dgvDetalleSubconjunto.Columns.Add("PXSC_CANTIDAD", "Cantidad");
            dgvDetalleSubconjunto.Columns["PZA_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleSubconjunto.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleSubconjunto.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleSubconjunto.Columns["PXSC_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleSubconjunto.Columns["PXSC_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvPiezasDisponibles.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPiezasDisponibles.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPiezasDisponibles.Columns.Add("TE_CODIGO", "Terminación");
            dgvPiezasDisponibles.Columns.Add("PZA_COSTO", "Costo");
            dgvPiezasDisponibles.Columns.Add("PZA_DESCRIPCION", "Descripción");
            dgvPiezasDisponibles.Columns["PZA_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasDisponibles.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasDisponibles.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasDisponibles.Columns["PZA_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasDisponibles.Columns["PZA_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPiezasDisponibles.Columns["PZA_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvSubconjuntos.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGOPARTE";
            dgvSubconjuntos.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_NOMBRE";
            dgvSubconjuntos.Columns["SCONJ_COSTO"].DataPropertyName = "SCONJ_COSTO";
            dgvSubconjuntos.Columns["SCONJ_DESCRIPCION"].DataPropertyName = "SCONJ_DESCRIPCION";

            dgvDetalleSubconjunto.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGO";
            dgvDetalleSubconjunto.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_CODIGO";
            dgvDetalleSubconjunto.Columns["TE_CODIGO"].DataPropertyName = "PZA_CODIGO";
            dgvDetalleSubconjunto.Columns["PXSC_CANTIDAD"].DataPropertyName = "PXSC_CANTIDAD";

            dgvPiezasDisponibles.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGOPARTE";
            dgvPiezasDisponibles.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_NOMBRE";
            dgvPiezasDisponibles.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvPiezasDisponibles.Columns["PZA_COSTO"].DataPropertyName = "PZA_COSTO";
            dgvPiezasDisponibles.Columns["PZA_DESCRIPCION"].DataPropertyName = "PZA_DESCRIPCION";        
            
            //Obtenemos las terminaciones, estados, planos y piezas
            
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.EstadoParteBLL.ObtenerTodos(dsEstructura.ESTADO_PARTES);
                BLL.PiezaBLL.ObtenerPiezas(dsEstructura.PIEZAS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Dataviews de grillas y asignación
            dvSubconjuntos = new DataView(dsEstructura.SUBCONJUNTOS);
            dvSubconjuntos.Sort = "SCONJ_NOMBRE ASC";
            dgvSubconjuntos.DataSource = dvSubconjuntos;
            dvDetalleSubconjunto = new DataView(dsEstructura.PIEZASXSUBCONJUNTO);
            dgvDetalleSubconjunto.DataSource = dvDetalleSubconjunto;
            dvPiezasDisponibles = new DataView(dsEstructura.PIEZAS);
            dvPiezasDisponibles.Sort = "PZA_NOMBRE ASC";
            dgvPiezasDisponibles.DataSource = dvPiezasDisponibles;
            //Dataviews de combos
            dvEstados = new DataView(dsEstructura.ESTADO_PARTES);
            dvPlanos = new DataView(dsEstructura.PLANOS);
            cbEstado.SetDatos(dvEstados, "par_codigo", "par_nombre", "Seleccione", false);
            cbPlano.SetDatos(dvPlanos, "pno_codigo", "pno_nombre", "Seleccione", false);

            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";
        }

        private void dgvSubconjuntos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
        
        private void dgvDetalleSubconjunto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigoPieza, codigoTerminacion;
                switch (dgvDetalleSubconjunto.Columns[e.ColumnIndex].Name)
                {
                    case "TE_CODIGO":
                        codigoPieza = Convert.ToInt32(dvDetalleSubconjunto[e.RowIndex]["PZA_CODIGO"]);
                        codigoTerminacion = Convert.ToInt32(dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).TE_CODIGO.ToString()); ;
                        nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "PZA_NOMBRE":
                        codigoPieza = Convert.ToInt32(dvDetalleSubconjunto[e.RowIndex]["PZA_CODIGO"]);
                        nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "PZA_CODIGOPARTE":
                        codigoPieza = Convert.ToInt32(dvDetalleSubconjunto[e.RowIndex]["PZA_CODIGO"]);
                        nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_CODIGOPARTE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvPiezasDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvPiezasDisponibles.Columns[e.ColumnIndex].Name == "TE_CODIGO")
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

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
            (sender as Button).Location = punto;
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
            (sender as Button).Location = punto;
        }

        #endregion 

    }
}
