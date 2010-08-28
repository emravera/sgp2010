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
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private DataView dvConjuntos, dvSubconjuntosConjunto, dvSubconjuntosDisponibles, dvPiezasConjunto, dvPiezasDisponibles;
        private DataView dvEstados, dvPlanos, dvPartes, dvHojaRuta;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoSCC = -1, codigoPC = -1;
        int slideActual = 0; //slideActual 0-Datos, 1-Subconjuntos, 2-Piezas
                
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
            this.Close();
            this.Dispose(true);            
        }

        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsEstructura.CONJUNTOS.Clear();
                dsEstructura.SUBCONJUNTOSXCONJUNTO.Clear();
                dsEstructura.PIEZASXCONJUNTO.Clear();
                
                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.ConjuntoBLL.ObtenerConjuntos(txtNombreBuscar.Text, dsEstructura, true);
                    
                if (dsEstructura.CONJUNTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Conjuntos con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }           
                
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvConjuntos.Table = dsEstructura.CONJUNTOS;
                dvSubconjuntosConjunto.Table = dsEstructura.SUBCONJUNTOSXCONJUNTO;
                dvPiezasConjunto.Table = dsEstructura.PIEZASXCONJUNTO;
                
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
            string datosFaltantes = string.Empty;
            //Revisamos que completó los datos
            if (txtNombre.Text == string.Empty) { datosFaltantes += "* Nombre\n"; }
            if (cbEstado.GetSelectedIndex() == -1) { datosFaltantes += "* Estado\n"; }
            if (cbPlano.GetSelectedIndex() == -1) { datosFaltantes += "* Plano\n"; }
            if (dgvSubconjuntosConjunto.Rows.Count == 0 && dgvPiezasConjunto.Rows.Count == 0) { datosFaltantes += "* El detalle del conjunto\n"; }
            if (datosFaltantes == string.Empty)
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
                        rowConjunto.PAR_CODIGO = cbEstado.GetSelectedValueInt();
                        rowConjunto.PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        rowConjunto.CONJ_COSTO = nudCosto.Value;
                        rowConjunto.CONJ_COSTOFIJO = (chkCostoFijo.Checked) ? 1 : 0;
                        rowConjunto.CONJ_DESCRIPCION = txtDescripcion.Text;
                        if (cbHojaRuta.GetSelectedIndex() != -1) { rowConjunto.HR_CODIGO = cbHojaRuta.GetSelectedValueInt(); }
                        else { rowConjunto.SetHR_CODIGONull(); }
                        rowConjunto.EndEdit();
                        dsEstructura.CONJUNTOS.AddCONJUNTOSRow(rowConjunto);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad ConjuntoBLL y ConjuntoDAL sepan cuales insertar
                        BLL.ConjuntoBLL.Insertar(dsEstructura);                        
                        //Guardamos la imagen del conjunto, no importa si no la cargo ConjuntoBLL se encarga de determinar eso                        
                        BLL.ConjuntoBLL.GuardarImagen(Convert.ToInt32(rowConjunto.CONJ_CODIGO), pbImagen.Image);
                        //Ahora si aceptamos los cambios
                        dsEstructura.CONJUNTOS.AcceptChanges();
                        dsEstructura.SUBCONJUNTOSXCONJUNTO.AcceptChanges();
                        dsEstructura.PIEZASXCONJUNTO.AcceptChanges();
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
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_DESCRIPCION = txtDescripcion.Text;
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PAR_CODIGO = cbEstado.GetSelectedValueInt();
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PNO_CODIGO = cbPlano.GetSelectedValueInt();
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_COSTO = nudCosto.Value;
                    dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_COSTOFIJO = (chkCostoFijo.Checked) ? 1 : 0;
                    if (cbHojaRuta.GetSelectedIndex() != -1) { dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).HR_CODIGO = cbHojaRuta.GetSelectedValueInt(); }
                    else { dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).SetHR_CODIGONull(); }
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ConjuntoBLL.Actualizar(dsEstructura);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsEstructura.CONJUNTOS.AcceptChanges();
                        dsEstructura.SUBCONJUNTOSXCONJUNTO.AcceptChanges();
                        dsEstructura.PIEZASXCONJUNTO.AcceptChanges();
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
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            dsEstructura.SUBCONJUNTOS.RejectChanges();
            dsEstructura.SUBCONJUNTOSXCONJUNTO.RejectChanges();
            dsEstructura.PIEZASXCONJUNTO.RejectChanges();
            //Seteamos la interfaz
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
            ActualizarImagen();
        }
        #endregion

        #region Subconjuntos

        private void btnDeleteSubconjunto_Click(object sender, EventArgs e)
        {
            if (dgvSubconjuntosConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvSubconjuntosConjunto[dgvSubconjuntosConjunto.SelectedRows[0].Index]["scxcj_codigo"]);
                decimal costo = dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SUBCONJUNTOSRow.SCONJ_COSTO;
                decimal cantidad = dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SCXCJ_CANTIDAD;
                try { nudCosto.Value -= (costo * cantidad); }
                catch (System.ArgumentOutOfRangeException) { nudCosto.Value = 0; }
                //Lo borramos pero sólo del dataset
                dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumarSubconjunto_Click(object sender, EventArgs e)
        {
            if (dgvSubconjuntosConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvSubconjuntosConjunto[dgvSubconjuntosConjunto.SelectedRows[0].Index]["scxcj_codigo"]);                
                //Aumentamos la cantidad
                dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SCXCJ_CANTIDAD += 1;
                if (!chkCostoFijo.Checked)
                {
                    decimal costo = dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SUBCONJUNTOSRow.SCONJ_COSTO;
                    nudCosto.Value += costo;
                    dgvSubconjuntosConjunto.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestarSubconjunto_Click(object sender, EventArgs e)
        {
            if (dgvSubconjuntosConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDCJ = Convert.ToInt32(dvSubconjuntosConjunto[dgvSubconjuntosConjunto.SelectedRows[0].Index]["scxcj_codigo"]);
                //Disminuimos la cantidad si es mayor que 1 ya que no admitimos valores negativos
                if (dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SCXCJ_CANTIDAD > 1)
                {
                    dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SCXCJ_CANTIDAD -= 1;
                    if (!chkCostoFijo.Checked)
                    {
                        decimal costo = dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(codigoDCJ).SUBCONJUNTOSRow.SCONJ_COSTO;
                        try { nudCosto.Value -= costo; }
                        catch (System.ArgumentOutOfRangeException) { nudCosto.Value = 0; }
                    }
                    dgvSubconjuntosConjunto.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarSubconjunto_Click(object sender, EventArgs e)
        {
            if (dgvSCDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidadSubconjunto.Value > 0)
            {
                bool agregarSubconjunto; //variable que indica si se debe agregar el subconjunto al listado
                //Obtenemos el código del conjunto según sea nuevo o modificado, lo hacemos acá porque lo vamos a usar mucho
                int conjuntoCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { conjuntoCodigo = -1; }
                else { conjuntoCodigo = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]); }
                //Obtenemos el código del subconjunto, también lo vamos a usar mucho
                int subconjuntoCodigo = Convert.ToInt32(dvSubconjuntosDisponibles[dgvSCDisponibles.SelectedRows[0].Index]["sconj_codigo"]);

                //Primero vemos si el conjunto tiene algún subconjunto cargado, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvSubconjuntosConjunto.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar el mismo subconjunto haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "conj_codigo = " + conjuntoCodigo + " AND sconj_codigo = " + subconjuntoCodigo;
                    Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[] rows =
                        (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("El conjunto ya posee el subconjunto seleccionado. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].SCXCJ_CANTIDAD += nudCantidadSubconjunto.Value;
                            if (!chkCostoFijo.Checked) { nudCosto.Value += (rows[0].SUBCONJUNTOSRow.SCONJ_COSTO * nudCantidadSubconjunto.Value); }
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
                    Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row = dsEstructura.SUBCONJUNTOSXCONJUNTO.NewSUBCONJUNTOSXCONJUNTORow();
                    row.BeginEdit();
                    //Modificado 23/07/2010
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.SCXCJ_CODIGO = codigoSCC--; //-- para que se vaya autodecrementando en cada inserción
                    row.CONJ_CODIGO = conjuntoCodigo;
                    row.SCONJ_CODIGO = subconjuntoCodigo;
                    row.SCXCJ_CANTIDAD = nudCantidadSubconjunto.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.SUBCONJUNTOSXCONJUNTO.AddSUBCONJUNTOSXCONJUNTORow(row);
                    if (!chkCostoFijo.Checked) { nudCosto.Value += (row.SUBCONJUNTOSRow.SCONJ_COSTO * nudCantidadSubconjunto.Value); }
                }
                nudCantidadSubconjunto.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Subconjunto de la lista y asignarle una cantidad mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }       

        #endregion

        #region Piezas
        
        private void btnDeletePieza_Click(object sender, EventArgs e)
        {
            if (dgvPiezasConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoPXCJ = Convert.ToInt32(dvPiezasConjunto[dgvPiezasConjunto.SelectedRows[0].Index]["pxcj_codigo"]);
                if (!chkCostoFijo.Checked)
                {
                    decimal costo = dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PIEZASRow.PZA_COSTO;
                    decimal cantidad = dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PXCJ_CANTIDAD;
                    try { nudCosto.Value -= (costo * cantidad); }
                    catch (System.ArgumentOutOfRangeException) { nudCosto.Value = 0; }
                }
                //Lo borramos pero sólo del dataset
                dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).Delete();
                dgvPiezasConjunto.Refresh();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumarPieza_Click(object sender, EventArgs e)
        {
            if (dgvPiezasConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoPXCJ = Convert.ToInt32(dvPiezasConjunto[dgvPiezasConjunto.SelectedRows[0].Index]["pxcj_codigo"]);
                //Aumentamos la cantidad
                dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PXCJ_CANTIDAD += 1;
                if (!chkCostoFijo.Checked)
                {
                    decimal costo = dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PIEZASRow.PZA_COSTO;
                    nudCosto.Value += costo;
                }
                dgvPiezasConjunto.Refresh();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestarPieza_Click(object sender, EventArgs e)
        {
            if (dgvPiezasConjunto.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoPXCJ = Convert.ToInt32(dvPiezasConjunto[dgvPiezasConjunto.SelectedRows[0].Index]["pxcj_codigo"]);
                //Disminuimos la cantidad si es mayor que 1 ya que no admitimos valores negativos
                if (dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PXCJ_CANTIDAD > 1)
                {
                    dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PXCJ_CANTIDAD -= 1;
                    if (!chkCostoFijo.Checked)
                    {
                        decimal costo = dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(codigoPXCJ).PIEZASRow.PZA_COSTO;
                        try { nudCosto.Value -= costo; }
                        catch (System.ArgumentOutOfRangeException) { nudCosto.Value = 0; }
                    }
                    dgvPiezasConjunto.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarPieza_Click(object sender, EventArgs e)
        {
            if (dgvPDisponibles.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidadPieza.Value > 0)
            {
                bool agregarPieza; //variable que indica si se debe agregar la pieza al listado
                //Obtenemos el código del conjunto según sea nuevo o modificado, lo hacemos acá porque lo vamos a usar mucho
                int conjuntoCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { conjuntoCodigo = -1; }
                else { conjuntoCodigo = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]); }
                //Obtenemos el código de la pieza, también lo vamos a usar mucho
                int piezaCodigo = Convert.ToInt32(dvPiezasDisponibles[dgvPDisponibles.SelectedRows[0].Index]["pza_codigo"]);

                //Primero vemos si el conjunto tiene alguna pieza cargada, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvPiezasConjunto.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar la misma pieza haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "conj_codigo = " + conjuntoCodigo + " AND pza_codigo = " + piezaCodigo;
                    Data.dsEstructura.PIEZASXCONJUNTORow[] rows =
                        (Data.dsEstructura.PIEZASXCONJUNTORow[])dsEstructura.PIEZASXCONJUNTO.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("El conjunto ya posee la pieza seleccionada. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].PXCJ_CANTIDAD += nudCantidadPieza.Value;
                            if (!chkCostoFijo.Checked) { nudCosto.Value += (rows[0].PIEZASRow.PZA_COSTO * nudCantidadPieza.Value); }
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
                    //No tiene ningún subconjunto agregado, marcamos que debe agregarse
                    agregarPieza = true;
                }

                //Ahora comprobamos si debe agregarse el subconjunto o no
                if (agregarPieza)
                {
                    Data.dsEstructura.PIEZASXCONJUNTORow row = dsEstructura.PIEZASXCONJUNTO.NewPIEZASXCONJUNTORow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.PXCJ_CODIGO = codigoPC--; //-- para que se vaya autodecrementando en cada inserción
                    row.CONJ_CODIGO = conjuntoCodigo;
                    row.PZA_CODIGO = piezaCodigo;
                    row.PXCJ_CANTIDAD = nudCantidadPieza.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.PIEZASXCONJUNTO.AddPIEZASXCONJUNTORow(row);
                    if (!chkCostoFijo.Checked) { nudCosto.Value += (row.PIEZASRow.PZA_COSTO * nudCantidadPieza.Value); }
                }
                nudCantidadPieza.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista y asignarle una cantidad mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                    btnZoomOut.PerformClick();
                    tcConjunto.SelectedTab = tpBuscar;
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
                    cbHojaRuta.Enabled = true;
                    cbHojaRuta.SetTexto("Seleccione");
                    dsEstructura.LISTA_PARTES.Clear();
                    dvSubconjuntosConjunto.RowFilter = "CONJ_CODIGO = -1";
                    dvPiezasConjunto.RowFilter = "CONJ_CODIGO = -1";
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
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    panelAgregarSubconjunto.Enabled = true;
                    panelAccionesSubconjunto.Enabled = true;
                    panelAgregarPieza.Enabled = true;
                    panelAccionesPieza.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    btnZoomOut.PerformClick();
                    tcConjunto.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
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
                    cbHojaRuta.Enabled = true;
                    cbHojaRuta.SetTexto("Seleccione");
                    dsEstructura.LISTA_PARTES.Clear();
                    dvSubconjuntosConjunto.RowFilter = "CONJ_CODIGO = -1";
                    dvPiezasConjunto.RowFilter = "CONJ_CODIGO = -1";
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
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    panelAgregarSubconjunto.Enabled = true;
                    panelAccionesSubconjunto.Enabled = true;
                    panelAgregarPieza.Enabled = true;
                    panelAccionesPieza.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    btnZoomOut.PerformClick();
                    tcConjunto.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    cbEstado.Enabled = false;
                    cbPlano.Enabled = false;
                    cbEstado.SetTexto(string.Empty);
                    cbPlano.SetTexto(string.Empty);
                    cbHojaRuta.Enabled = false;
                    cbHojaRuta.SetTexto(string.Empty);
                    nudCosto.Enabled = false;
                    chkCostoFijo.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    btnAbrirImagen.Enabled = false;
                    btnQuitarImagen.Enabled = false;
                    panelAgregarSubconjunto.Enabled = false;
                    panelAccionesSubconjunto.Enabled = false;
                    panelAgregarPieza.Enabled = false;
                    panelAccionesPieza.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    CargarPartes();
                    btnZoomOut.PerformClick();
                    tcConjunto.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    cbEstado.Enabled = true;
                    cbPlano.Enabled = true;
                    cbHojaRuta.Enabled = true;
                    cbEstado.SetTexto(string.Empty);
                    cbPlano.SetTexto(string.Empty);
                    cbHojaRuta.SetTexto(string.Empty);
                    nudCosto.Enabled = true;
                    chkCostoFijo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    panelAgregarSubconjunto.Enabled = true;
                    panelAccionesSubconjunto.Enabled = true;
                    panelAgregarPieza.Enabled = true;
                    panelAccionesPieza.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    CargarPartes();
                    btnZoomOut.PerformClick();
                    tcConjunto.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
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
            cbEstado.SetSelectedValue(Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PAR_CODIGO));
            cbPlano.SetSelectedValue(Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).PNO_CODIGO));
            nudCosto.Value = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_COSTO;
            if (dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_COSTOFIJO == 0) { chkCostoFijo.Checked = false; }
            else { chkCostoFijo.Checked = true; }
            txtDescripcion.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).CONJ_DESCRIPCION;
            if (!dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).IsHR_CODIGONull())
            {
                cbHojaRuta.SetSelectedValue(Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoConjunto).HR_CODIGO));
            }
            else { cbHojaRuta.SetSelectedIndex(-1); }
            pbImagen.Image = BLL.ConjuntoBLL.ObtenerImagen(codigoConjunto);
            //Usemos el filtro del dataview para mostrar sólo los subconjuntos del conjunto seleccionado
            dvSubconjuntosConjunto.RowFilter = "conj_codigo = " + codigoConjunto;
            dvPiezasConjunto.RowFilter = "conj_codigo = " + codigoConjunto;
        }
        
        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvConjuntos_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        #region Slide

        private void SetSlide()
        {
            slideControl.AddSlide(slidePiezas);
            slideControl.AddSlide(slideSubconjuntos);
            slideControl.AddSlide(slideDatos);
            gbDatos.Parent = slideDatos;
            gbPartesConjunto.Parent = slideDatos;
            gbSCD.Parent = slideSubconjuntos;
            gbSCC.Parent = slideSubconjuntos;
            gbPD.Parent = slidePiezas;
            gbPC.Parent = slidePiezas;
            slideControl.Selected = slideDatos;           
        }

        private void btnDatos_Click(object sender, EventArgs e)
        {
            if (slideActual != 0)
            {
                slideControl.BackwardTo("slideDatos");
                SetBotones(0, btnDatos);
                SetBotones(1, btnSubconjuntos);
                SetBotones(1, btnPiezas);
                slideActual = 0;
                CargarPartes();
            }
        }

        private void btnSubconjuntos_Click(object sender, EventArgs e)
        {
            if (slideActual != 1)
            {
                if (slideActual > 1) { slideControl.BackwardTo("slideSubconjuntos"); }
                else { slideControl.ForwardTo("slideSubconjuntos"); }
                SetBotones(-1, btnDatos);
                SetBotones(0, btnSubconjuntos);
                SetBotones(1, btnPiezas);
                slideActual = 1;
            }
        }

        private void btnPiezas_Click(object sender, EventArgs e)
        {
            if (slideActual != 2)
            {
                if (slideActual > 2) { slideControl.BackwardTo("slidePiezas"); }
                else { slideControl.ForwardTo("slidePiezas"); }
                SetBotones(-1, btnDatos);
                SetBotones(-1, btnSubconjuntos);
                SetBotones(0, btnPiezas);
                slideActual = 2;
            }
        }

        private void SetBotones(int orientacion, Button boton)
        {
            switch (orientacion)
            {
                case -1:
                    boton.Image = Properties.Resources.izquierda1_15;
                    break;
                case 0:
                    boton.Image = Properties.Resources.arriba1_15;
                    break;
                case 1:
                    boton.Image = Properties.Resources.derecha1_15;
                    break;
                default:
                    break;
            }
            boton.TextImageRelation = TextImageRelation.TextBeforeImage;
        }
        #endregion

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente            

            #region Grilla Conjuntos de la búsqueda
            dgvConjuntos.AutoGenerateColumns = false;
            dgvConjuntos.Columns.Add("CONJ_CODIGOPARTE", "Código");
            dgvConjuntos.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvConjuntos.Columns.Add("CONJ_COSTO", "Costo");
            dgvConjuntos.Columns.Add("HR_CODIGO", "Hoja de Ruta");
            dgvConjuntos.Columns.Add("CONJ_DESCRIPCION", "Descripción");
            dgvConjuntos.Columns["CONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvConjuntos.Columns["HR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvConjuntos.Columns["CONJ_CODIGOPARTE"].DataPropertyName = "CONJ_CODIGOPARTE";
            dgvConjuntos.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_NOMBRE";
            dgvConjuntos.Columns["CONJ_COSTO"].DataPropertyName = "CONJ_COSTO";
            dgvConjuntos.Columns["CONJ_DESCRIPCION"].DataPropertyName = "CONJ_DESCRIPCION";
            dgvConjuntos.Columns["HR_CODIGO"].DataPropertyName = "HR_CODIGO";
            #endregion

            #region Grilla Partes
            dgvPartes.AutoGenerateColumns = false;
            dgvPartes.Columns.Add("PAR_TIPO","Tipo");
            dgvPartes.Columns.Add("PAR_CODIGO", "Código");
            dgvPartes.Columns.Add("PAR_NOMBRE", "Nombre");
            dgvPartes.Columns.Add("PAR_TERMINACION", "Terminación");
            dgvPartes.Columns.Add("PAR_CANTIDAD", "Cantidad");
            dgvPartes.Columns.Add("PAR_UMED", "Costo");
            dgvPartes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvPartes.Columns["PAR_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartes.Columns["PAR_UMED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartes.Columns["PAR_TIPO"].DataPropertyName = "PAR_TIPO";
            dgvPartes.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvPartes.Columns["PAR_NOMBRE"].DataPropertyName = "PAR_NOMBRE";
            dgvPartes.Columns["PAR_TERMINACION"].DataPropertyName = "PAR_TERMINACION";
            dgvPartes.Columns["PAR_CANTIDAD"].DataPropertyName = "PAR_CANTIDAD";
            dgvPartes.Columns["PAR_UMED"].DataPropertyName = "PAR_UMED";
            #endregion

            #region Subconjuntos
            dgvSubconjuntosConjunto.AutoGenerateColumns = false;
            dgvSubconjuntosConjunto.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvSubconjuntosConjunto.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSubconjuntosConjunto.Columns.Add("SCXCJ_CANTIDAD", "Cantidad");
            dgvSubconjuntosConjunto.Columns.Add("COSTO", "Costo");
            dgvSubconjuntosConjunto.Columns["SCONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntosConjunto.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntosConjunto.Columns["SCXCJ_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntosConjunto.Columns["SCXCJ_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSubconjuntosConjunto.Columns["COSTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSubconjuntosConjunto.Columns["COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSubconjuntosConjunto.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGO";
            dgvSubconjuntosConjunto.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_CODIGO";
            dgvSubconjuntosConjunto.Columns["SCXCJ_CANTIDAD"].DataPropertyName = "SCXCJ_CANTIDAD";
            dgvSubconjuntosConjunto.Columns["COSTO"].DataPropertyName = "SCXCJ_CODIGO";

            dgvSCDisponibles.AutoGenerateColumns = false;
            dgvSCDisponibles.Columns.Add("SCONJ_CODIGOPARTE", "Código");
            dgvSCDisponibles.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCDisponibles.Columns.Add("SCONJ_COSTO", "Costo");
            dgvSCDisponibles.Columns.Add("SCONJ_DESCRIPCION", "Descripción");
            dgvSCDisponibles.Columns["SCONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["SCONJ_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvSCDisponibles.Columns["SCONJ_CODIGOPARTE"].DataPropertyName = "SCONJ_CODIGOPARTE";
            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_NOMBRE";
            dgvSCDisponibles.Columns["SCONJ_COSTO"].DataPropertyName = "SCONJ_COSTO";
            dgvSCDisponibles.Columns["SCONJ_DESCRIPCION"].DataPropertyName = "SCONJ_DESCRIPCION";
            #endregion

            #region Piezas
            dgvPiezasConjunto.AutoGenerateColumns = false;
            dgvPiezasConjunto.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPiezasConjunto.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPiezasConjunto.Columns.Add("TE_CODIGO", "Terminación");
            dgvPiezasConjunto.Columns.Add("PXCJ_CANTIDAD", "Cantidad");
            dgvPiezasConjunto.Columns.Add("COSTO", "Costo");
            dgvPiezasConjunto.Columns["PZA_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasConjunto.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasConjunto.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasConjunto.Columns["PXCJ_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasConjunto.Columns["PXCJ_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPiezasConjunto.Columns["COSTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPiezasConjunto.Columns["COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPiezasConjunto.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGO";
            dgvPiezasConjunto.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_CODIGO";
            dgvPiezasConjunto.Columns["TE_CODIGO"].DataPropertyName = "PZA_CODIGO";
            dgvPiezasConjunto.Columns["PXCJ_CANTIDAD"].DataPropertyName = "PXCJ_CANTIDAD";
            dgvPiezasConjunto.Columns["COSTO"].DataPropertyName = "PXCJ_CODIGO";
            
            dgvPDisponibles.AutoGenerateColumns = false;
            dgvPDisponibles.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPDisponibles.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPDisponibles.Columns.Add("TE_CODIGO", "Terminación");
            dgvPDisponibles.Columns.Add("PZA_COSTO", "Costo");
            dgvPDisponibles.Columns.Add("PZA_DESCRIPCION", "Descripción");
            dgvPDisponibles.Columns["PZA_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPDisponibles.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPDisponibles.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPDisponibles.Columns["PZA_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPDisponibles.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGOPARTE";
            dgvPDisponibles.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_NOMBRE";
            dgvPDisponibles.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvPDisponibles.Columns["PZA_COSTO"].DataPropertyName = "PZA_COSTO";
            dgvPDisponibles.Columns["PZA_DESCRIPCION"].DataPropertyName = "PZA_DESCRIPCION";

            #endregion

            //Obtenemos las terminaciones, estados, planos, subconjuntos            
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.EstadoParteBLL.ObtenerTodos(dsEstructura.ESTADO_PARTES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.SubConjuntoBLL.ObtenerSubconjuntos(dsEstructura.SUBCONJUNTOS);
                BLL.PiezaBLL.ObtenerPiezas(dsEstructura.PIEZAS);
                BLL.HojaRutaBLL.ObtenerHojasRuta(dsEstructura.HOJAS_RUTA);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Dataviews de grillas            
            dvConjuntos = new DataView(dsEstructura.CONJUNTOS);
            dvConjuntos.Sort = "CONJ_NOMBRE ASC";
            dgvConjuntos.DataSource = dvConjuntos;
            dvPartes = new DataView(dsEstructura.LISTA_PARTES);
            dgvPartes.DataSource = dvPartes;
            dvSubconjuntosConjunto = new DataView(dsEstructura.SUBCONJUNTOSXCONJUNTO);
            dgvSubconjuntosConjunto.DataSource = dvSubconjuntosConjunto;
            dvSubconjuntosDisponibles = new DataView(dsEstructura.SUBCONJUNTOS);
            dvSubconjuntosDisponibles.Sort = "SCONJ_NOMBRE ASC";
            dgvSCDisponibles.DataSource = dvSubconjuntosDisponibles;
            dvPiezasConjunto = new DataView(dsEstructura.PIEZASXCONJUNTO);
            dgvPiezasConjunto.DataSource = dvPiezasConjunto;
            dvPiezasDisponibles = new DataView(dsEstructura.PIEZAS);
            dvPiezasDisponibles.Sort = "PZA_NOMBRE ASC";
            dgvPDisponibles.DataSource = dvPiezasDisponibles;
            
            //Dataviews de combos
            dvEstados = new DataView(dsEstructura.ESTADO_PARTES);
            dvPlanos = new DataView(dsEstructura.PLANOS);
            cbEstado.SetDatos(dvEstados, "par_codigo", "par_nombre", "Seleccione", false);
            cbPlano.SetDatos(dvPlanos, "pno_codigo", "pno_nombre", "Seleccione", false);
            dvHojaRuta = new DataView(dsEstructura.HOJAS_RUTA);
            cbHojaRuta.SetDatos(dvHojaRuta, "hr_codigo", "hr_nombre", "Seleccione", false);
            
            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            
            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";

        }

        #region CellFormatting
        private void dgvConjuntos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvConjuntos.Columns[e.ColumnIndex].Name)
                {
                    case "HR_CODIGO":
                        nombre = dsEstructura.HOJAS_RUTA.FindByHR_CODIGO(Convert.ToInt32(e.Value.ToString())).HR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CONJ_COSTO":
                        nombre = "$ " + e.Value.ToString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void dgvSubconjuntosConjunto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigoSubConjunto;
                switch (dgvSubconjuntosConjunto.Columns[e.ColumnIndex].Name)
                {
                    case "SCONJ_NOMBRE":
                        codigoSubConjunto = Convert.ToInt32(dvSubconjuntosConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubConjunto).SCONJ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "SCONJ_CODIGOPARTE":
                        codigoSubConjunto = Convert.ToInt32(dvSubconjuntosConjunto[e.RowIndex]["SCONJ_CODIGO"]);
                        nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoSubConjunto).SCONJ_CODIGOPARTE;
                        e.Value = nombre;
                        break;
                    case "COSTO":
                        int cod = Convert.ToInt32(e.Value.ToString());
                        nombre = (dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(cod).SCXCJ_CANTIDAD * dsEstructura.SUBCONJUNTOSXCONJUNTO.FindBySCXCJ_CODIGO(cod).SUBCONJUNTOSRow.SCONJ_COSTO).ToString();
                        e.Value = "$ " + nombre;
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
                if (dgvSCDisponibles.Columns[e.ColumnIndex].Name == "SCONJ_COSTO")
                {
                    nombre = "$ " + e.Value.ToString();
                    e.Value = nombre;
                }
            }
        }        

        private void dgvPDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvPDisponibles.Columns[e.ColumnIndex].Name)
                {
                    case "TE_CODIGO":
                        nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                        break;
                    case "PZA_COSTO":
                        nombre = "$ " + e.Value.ToString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvPiezasConjunto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string nombre;
            int codigoPieza, codigoTerminacion;
            switch (dgvPiezasConjunto.Columns[e.ColumnIndex].Name)
            {
                case "TE_CODIGO":
                    codigoPieza = Convert.ToInt32(dvPiezasConjunto[e.RowIndex]["PZA_CODIGO"]);
                    codigoTerminacion = Convert.ToInt32(dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).TE_CODIGO.ToString()); ;
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(codigoTerminacion).TE_NOMBRE;
                    e.Value = nombre;
                    break;
                case "PZA_NOMBRE":
                    codigoPieza = Convert.ToInt32(dvPiezasConjunto[e.RowIndex]["PZA_CODIGO"]);
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_NOMBRE;
                    e.Value = nombre;
                    break;
                case "PZA_CODIGOPARTE":
                    codigoPieza = Convert.ToInt32(dvPiezasConjunto[e.RowIndex]["PZA_CODIGO"]);
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoPieza).PZA_CODIGOPARTE;
                    e.Value = nombre;
                    break;
                case "COSTO":
                    int cod = Convert.ToInt32(e.Value.ToString());
                    nombre = (dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(cod).PXCJ_CANTIDAD * dsEstructura.PIEZASXCONJUNTO.FindByPXCJ_CODIGO(cod).PIEZASRow.PZA_COSTO).ToString();
                    e.Value = "$ " + nombre;
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCantidadSubconjunto.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void CargarPartes()
        {
            dsEstructura.LISTA_PARTES.Clear();
            int codigoConjunto = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoConjunto = -1; }
            else { codigoConjunto = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]); }

            foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row in
                (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select("CONJ_CODIGO = " + codigoConjunto))
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.ESTR_CODIGO = codigoConjunto.ToString();
                rowParte.PAR_TIPO = "Subconjunto";
                rowParte.PAR_CODIGO = row.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.SUBCONJUNTOSRow.SCONJ_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.SCXCJ_CANTIDAD.ToString();
                rowParte.PAR_UMED = "$ " + row.SUBCONJUNTOSRow.SCONJ_COSTO.ToString();
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.PIEZASXCONJUNTORow row in
                (Data.dsEstructura.PIEZASXCONJUNTORow[])dsEstructura.PIEZASXCONJUNTO.Select("CONJ_CODIGO = " + codigoConjunto))
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.ESTR_CODIGO = codigoConjunto.ToString();
                rowParte.PAR_TIPO = "Pieza";
                rowParte.PAR_CODIGO = row.PIEZASRow.PZA_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.PIEZASRow.PZA_NOMBRE;
                rowParte.PAR_TERMINACION = dsEstructura.TERMINACIONES.FindByTE_CODIGO(row.PIEZASRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.PXCJ_CANTIDAD.ToString();
                rowParte.PAR_UMED = "$ " + row.PIEZASRow.PZA_COSTO.ToString();
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            dvPartes.Table = dsEstructura.LISTA_PARTES;
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

        private void chkCostoFijo_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkCostoFijo.Checked) { nudCosto.Value = CalcularCosto(); }
            else { nudCosto.Value = 0; }
        }

        private decimal CalcularCosto()
        {
            decimal costo = 0;
            int codigoConjunto = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoConjunto = -1; }
            else { codigoConjunto = Convert.ToInt32(dvConjuntos[dgvConjuntos.SelectedRows[0].Index]["conj_codigo"]); }

            foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row in
                (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select("CONJ_CODIGO = " + codigoConjunto))
            {
                costo += (row.SUBCONJUNTOSRow.SCONJ_COSTO * row.SCXCJ_CANTIDAD);
            }

            foreach (Data.dsEstructura.PIEZASXCONJUNTORow row in
                (Data.dsEstructura.PIEZASXCONJUNTORow[])dsEstructura.PIEZASXCONJUNTO.Select("CONJ_CODIGO = " + codigoConjunto))
            {
                costo += (row.PIEZASRow.PZA_COSTO * row.PXCJ_CANTIDAD);
            }

            return costo;
        }       

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            Sistema.frmImagenZoom.Instancia.SetImagen(pbImagen.Image, "Imagen del Conjunto");
            animador.SetFormulario(Sistema.frmImagenZoom.Instancia, this, Sistema.ControlesUsuarios.AnimadorFormulario.animacionDerecha, 300, true);
            animador.MostrarFormulario();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            animador.CerrarFormulario();
        }

        private void ActualizarImagen()
        {
            if (animador.EsVisible())
            {
                (animador.GetForm() as Sistema.frmImagenZoom).SetImagen(pbImagen.Image, "Imagen de la Pieza");
            }
        }

        private void pbImagen_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ActualizarImagen();
        }

        #endregion
    }
}
