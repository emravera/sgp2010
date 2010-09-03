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
    public partial class frmEstructuraCocina : Form
    {
        private static frmEstructuraCocina _frmEstructuraCocina = null;
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        //Nombre objetos: CD=ConjuntosDisponibles - CE=ConjuntosEstructura - PD=PiezasDisponibles - PE=PiezasEstructura
        private DataView dvCocinaBuscar, dvCocina, dvResponsableBuscar, dvResponsable, dvPlanoBuscar, dvPlano;
        private DataView dvEstructuras, dvPartes, dvCD, dvCE, dvPD, dvPE;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private int slideActual = 0; //0-Datos, 1-Conjuntos, 2-Piezas
        private int cxe = -1, pxe = -1; //Variables para el manejo de inserciones en los dataset con códigos unique

        #region Inicio

        public frmEstructuraCocina()
        {
            InitializeComponent();

            SetGrillasCombosVistas();
            SetSlide();
            SetInterface(estadoUI.inicio);
        }

        public static frmEstructuraCocina Instancia
        {
            get
            {
                if (_frmEstructuraCocina == null || _frmEstructuraCocina.IsDisposed)
                {
                    _frmEstructuraCocina = new frmEstructuraCocina();
                }
                else
                {
                    _frmEstructuraCocina.BringToFront();
                }
                return _frmEstructuraCocina;
            }
            set
            {
                _frmEstructuraCocina = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
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
            if (dgvEstructuras.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Estructura seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //obtenemos el código
                        int codigo = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.EstructuraBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigo).Delete();
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Modelo de Cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion Inicio

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {                
                dsEstructura.PIEZASXESTRUCTURA.Clear();                
                dsEstructura.CONJUNTOSXESTRUCTURA.Clear();
                dsEstructura.GRUPOS_ESTRUCTURA.Clear();
                dsEstructura.ESTRUCTURAS.Clear();
                BLL.EstructuraBLL.ObtenerEstructuras(txtNombreBuscar.Text, cbPlanoBuscar.GetSelectedValue(), dtpFechaAltaBuscar.GetFecha(), cbCocinaBuscar.GetSelectedValue(), cbResponsableBuscar.GetSelectedValue(), ((Sistema.Item)cbActivoBuscar.SelectedItem).Value, dsEstructura);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvEstructuras.Table = dsEstructura.ESTRUCTURAS;
                if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Estructuras con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Estructuras - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisemos que completó todos los datos obligatorios          
            string datosFaltantes = string.Empty;
            if (txtNombre.Text == string.Empty) { datosFaltantes += "* Nombre\n"; }
            if (cbPlano.GetSelectedIndex() == -1) { datosFaltantes += "* Plano\n"; }
            if (cbCocina.GetSelectedIndex() == -1) { datosFaltantes += "* Cocina\n"; }
            //if (cbResponsable.GetSelectedIndex() == -1) { datosOK = false; datosFaltantes += "\\n* Responsable"; } Por ahora opcional
            //if (dtpFechaAlta.IsValueNull()) { dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor()); } Opcional por ahora
            if (dgvPartes.Rows.Count == 0) { datosFaltantes += "* El detalle de la estructura\n"; } //que al menos haya cargado 1 parte
            if (cbEstado.GetSelectedIndex() == -1) { datosFaltantes += "* Estado\n"; }
            if (datosFaltantes == string.Empty)
            {
                //Datos OK, revisemos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Creando uno nuevo
                    try
                    {
                        Data.dsEstructura.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.NewESTRUCTURASRow();
                        rowEstructura.BeginEdit();
                        rowEstructura.ESTR_CODIGO = -1;
                        rowEstructura.ESTR_NOMBRE = txtNombre.Text;
                        rowEstructura.PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        rowEstructura.ESTR_ACTIVO = cbEstado.GetSelectedValueInt(); ;
                        if (dtpFechaAlta.IsValueNull()) { rowEstructura.ESTR_FECHA_ALTA = BLL.DBBLL.GetFechaServidor(); }
                        else { rowEstructura.ESTR_FECHA_ALTA = (DateTime)dtpFechaAlta.GetFecha(); }
                        rowEstructura.COC_CODIGO = cbCocina.GetSelectedValueInt();
                        if (cbResponsable.GetSelectedIndex() == -1) rowEstructura.SetE_CODIGONull();
                        else { rowEstructura.E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { rowEstructura.SetESTR_FECHA_MODIFICACIONNull(); }
                        else { rowEstructura.ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        rowEstructura.ESTR_COSTO = nudcosto.Value;
                        rowEstructura.ESTR_DESCRIPCION = txtDescripcion.Text;
                        rowEstructura.ESTR_COSTOFIJO = (chkFijo.Checked) ? 1 : 0;
                        rowEstructura.EndEdit();
                        dsEstructura.ESTRUCTURAS.AddESTRUCTURASRow(rowEstructura);
                        BLL.EstructuraBLL.Insertar(dsEstructura);
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                        dsEstructura.GRUPOS_ESTRUCTURA.AcceptChanges();
                        dsEstructura.CONJUNTOSXESTRUCTURA.AcceptChanges();
                        dsEstructura.PIEZASXESTRUCTURA.AcceptChanges();

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
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de estructuras ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.ESTRUCTURAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Modificando
                    try
                    {
                        int codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_NOMBRE = txtNombre.Text;
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).PNO_CODIGO = cbPlano.GetSelectedValueInt();
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_ACTIVO = cbEstado.GetSelectedValueInt();
                        if (dtpFechaAlta.IsValueNull()) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_ALTA = BLL.DBBLL.GetFechaServidor(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_ALTA = (DateTime)dtpFechaAlta.GetFecha(); }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).COC_CODIGO = cbCocina.GetSelectedValueInt();
                        if (cbResponsable.GetSelectedIndex() == -1) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).SetE_CODIGONull(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).SetESTR_FECHA_MODIFICACIONNull(); }
                        else { dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_DESCRIPCION = txtDescripcion.Text;
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_COSTO = nudcosto.Value;
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigoEstructura).ESTR_COSTOFIJO = (chkFijo.Checked) ? 1 : 0;
                        BLL.EstructuraBLL.Actualizar(dsEstructura);
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de estructuras ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.ESTRUCTURAS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //le faltan completar datos, avisemos
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsEstructura.PIEZASXESTRUCTURA.RejectChanges();
            dsEstructura.CONJUNTOSXESTRUCTURA.RejectChanges();
            dsEstructura.GRUPOS_ESTRUCTURA.RejectChanges();
            dsEstructura.ESTRUCTURAS.RejectChanges();
            dsEstructura.LISTA_PARTES.Clear();
            SetInterface(estadoUI.inicio);
        }

        private void chkVer(object sender, EventArgs e)
        {
            string filtro = string.Empty, mostrar = string.Empty;
            if (chkConjunto.Checked) { mostrar += "'Conjunto'"; }            
            if (chkPieza.Checked && mostrar != string.Empty) { mostrar += ",'Pieza'"; }
            else if (chkPieza.Checked && mostrar == string.Empty) { mostrar += "'Pieza'"; }           
            filtro = "PAR_TIPO IN (" + mostrar + ")";
            if (mostrar != string.Empty) { dvPartes.RowFilter = filtro; }
            else { dvPartes.RowFilter = "PAR_TIPO = 'ocultar todo'"; }
        }

        #endregion

        #region Conjuntos

        private void btnAC_Click(object sender, EventArgs e)
        {
            if (dgvCD.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudC.Value > 0)
            {
                bool agregarConjunto; //variable que indica si se debe agregar el conjunto al listado
                //Obtenemos el código del conjunto según sea nuevo o modificado, lo hacemos acá porque lo vamos a usar mucho
                int codigoEstructura;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
                else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }
                //Obtenemos el código del conjunto, también lo vamos a usar mucho
                int codigoConjunto = Convert.ToInt32(dvCD[dgvCD.SelectedRows[0].Index]["conj_codigo"]);

                //Primero vemos si la estructura tiene algún conjunto cargado, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene la estructura seleccionado                
                if (dvCE.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar el mismo conjunto haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "estr_codigo = " + codigoEstructura + " AND conj_codigo = " + codigoConjunto;
                    Data.dsEstructura.CONJUNTOSXESTRUCTURARow[] rows =
                        (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("La estructura ya posee el conjunto seleccionado. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].CXE_CANTIDAD += nudC.Value;
                            if (!chkFijo.Checked)
                            {
                                nudcosto.Value += (nudC.Value * rows[0].CONJUNTOSRow.CONJ_COSTO);
                            }
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarConjunto = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarConjunto = true;
                    }
                }
                else
                {
                    //No tiene ningún conjunto agregado, marcamos que debe agregarse
                    agregarConjunto = true;
                }

                //Ahora comprobamos si debe agregarse el conjunto o no
                if (agregarConjunto)
                {
                    Data.dsEstructura.CONJUNTOSXESTRUCTURARow row = dsEstructura.CONJUNTOSXESTRUCTURA.NewCONJUNTOSXESTRUCTURARow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.CXE_CODIGO = cxe--; //-- para que se vaya autodecrementando en cada inserción
                    row.ESTR_CODIGO = codigoEstructura;
                    row.CONJ_CODIGO = codigoConjunto;
                    row.CXE_CANTIDAD = nudC.Value;
                    if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { row.GRP_CODIGO = -1; }
                    else { row.GRP_CODIGO = Convert.ToInt32(dsEstructura.GRUPOS_ESTRUCTURA[0]["GRP_CODIGO"]); }
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsEstructura.CONJUNTOSXESTRUCTURA.AddCONJUNTOSXESTRUCTURARow(row);
                    if (!chkFijo.Checked)
                    {
                        nudcosto.Value += (row.CONJUNTOSRow.CONJ_COSTO * row.CXE_CANTIDAD);
                    }
                }
                nudC.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista y asignarle un valor mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEC_Click(object sender, EventArgs e)
        {
            if (dgvCE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoC = Convert.ToInt32(dvCE[dgvCE.SelectedRows[0].Index]["cxe_codigo"]);
                //Lo borramos pero sólo del dataset
                if (!chkFijo.Checked)
                {
                    nudcosto.Value -= dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoC).CONJUNTOSRow.CONJ_COSTO * dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoC).CXE_CANTIDAD;
                }
                dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoC).Delete();                
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSC_Click(object sender, EventArgs e)
        {
            if (dgvCE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoCXE = Convert.ToInt32(dvCE[dgvCE.SelectedRows[0].Index]["cxe_codigo"]);
                dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CXE_CANTIDAD += 1;
                if (!chkFijo.Checked)
                {
                    nudcosto.Value += dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CONJUNTOSRow.CONJ_COSTO;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRC_Click(object sender, EventArgs e)
        {
            if (dgvCE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoCXE = Convert.ToInt32(dvCE[dgvCE.SelectedRows[0].Index]["cxe_codigo"]);
                if (dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CXE_CANTIDAD > 1)
                {
                    dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CXE_CANTIDAD -= 1;
                    if (!chkFijo.Checked)
                    {
                        nudcosto.Value -= dsEstructura.CONJUNTOSXESTRUCTURA.FindByCXE_CODIGO(codigoCXE).CONJUNTOSRow.CONJ_COSTO;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Conjunto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Piezas

        private void btnAP_Click(object sender, EventArgs e)
        {
            if (dgvPD.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudP.Value > 0)
            {
                bool agregarPieza;
                int codigoEstructura;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
                else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }
                int codigoPieza = Convert.ToInt32(dvPD[dgvPD.SelectedRows[0].Index]["pza_codigo"]);

                if (dvPE.Count > 0)
                {
                    string filtro = "estr_codigo = " + codigoEstructura + " AND pza_codigo = " + codigoPieza;
                    Data.dsEstructura.PIEZASXESTRUCTURARow[] rows =
                        (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(filtro);
                    if (rows.Length > 0)
                    {
                        DialogResult respuesta = MessageBox.Show("La estructura ya posee la pieza seleccionada. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            rows[0].PXE_CANTIDAD += nudP.Value;
                            if (!chkFijo.Checked) { nudcosto.Value += (nudP.Value * rows[0].PXE_CANTIDAD); }
                        }
                        agregarPieza = false;
                    }
                    else
                    {
                        agregarPieza = true;
                    }
                }
                else
                {
                    agregarPieza = true;
                }

                if (agregarPieza)
                {
                    Data.dsEstructura.PIEZASXESTRUCTURARow row = dsEstructura.PIEZASXESTRUCTURA.NewPIEZASXESTRUCTURARow();
                    row.BeginEdit();
                    row.PXE_CODIGO = pxe--;
                    row.ESTR_CODIGO = codigoEstructura;
                    row.PZA_CODIGO = codigoPieza;
                    row.PXE_CANTIDAD = nudP.Value;
                    if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { row.GRP_CODIGO = -1; }
                    else { row.GRP_CODIGO = Convert.ToInt32(dsEstructura.GRUPOS_ESTRUCTURA[0]["GRP_CODIGO"]); }
                    row.EndEdit();
                    dsEstructura.PIEZASXESTRUCTURA.AddPIEZASXESTRUCTURARow(row);
                    if (!chkFijo.Checked)
                    {
                        nudcosto.Value += (row.PXE_CANTIDAD * row.PIEZASRow.PZA_COSTO);
                    }
                }
                nudP.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista y asignarle un valor mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEP_Click(object sender, EventArgs e)
        {
            if (dgvPE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoP = Convert.ToInt32(dvPE[dgvPE.SelectedRows[0].Index]["pxe_codigo"]);
                if (!chkFijo.Checked)
                {
                    nudcosto.Value -= (dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoP).PIEZASRow.PZA_COSTO * dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoP).PXE_CANTIDAD);
                }
                dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoP).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            if (dgvPE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoPXE = Convert.ToInt32(dvPE[dgvPE.SelectedRows[0].Index]["pxe_codigo"]);
                dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoPXE).PXE_CANTIDAD += 1;
                if (!chkFijo.Checked)
                {
                    nudcosto.Value += dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoPXE).PIEZASRow.PZA_COSTO;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRP_Click(object sender, EventArgs e)
        {
            if (dgvPE.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigoPXE = Convert.ToInt32(dvPE[dgvPE.SelectedRows[0].Index]["pxe_codigo"]);
                if (dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoPXE).PXE_CANTIDAD > 1)
                {
                    dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoPXE).PXE_CANTIDAD -= 1;
                    if (!chkFijo.Checked)
                    {
                        nudcosto.Value -= dsEstructura.PIEZASXESTRUCTURA.FindByPXE_CODIGO(codigoPXE).PIEZASRow.PZA_COSTO;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Pieza de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region SlideControl

        private void btnDatos_Click(object sender, EventArgs e)
        {
            if (slideActual != 0)
            {
                slideControl.BackwardTo("slideDatos");
                SetBotones(0, btnDatos);
                SetBotones(1, btnConjuntos);
                SetBotones(1, btnPiezas);
                slideActual = 0;
                CargarListaPartes();
            }
        }

        private void btnConjuntos_Click(object sender, EventArgs e)
        {
            if (slideActual != 1)
            {
                if (slideActual > 1) { slideControl.BackwardTo("slideConjuntos"); }
                else { slideControl.ForwardTo("slideConjuntos"); }
                SetBotones(-1, btnDatos);
                SetBotones(0, btnConjuntos);
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
                SetBotones(-1, btnConjuntos);
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

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbPartes.Parent = slideDatos;
            gbVer.Parent = slideDatos;
            gbCD.Parent = slideConjuntos;
            gbAC.Parent = slideConjuntos;
            gbCE.Parent = slideConjuntos;
            gbOC.Parent = slideConjuntos;
            gbPD.Parent = slidePiezas;
            gbAP.Parent = slidePiezas;
            gbPE.Parent = slidePiezas;
            gbOP.Parent = slidePiezas;
            slideControl.AddSlide(slideDatos);
            slideControl.AddSlide(slideConjuntos);
            slideControl.AddSlide(slidePiezas);
            slideControl.Selected = slideDatos;
        }

        #endregion SlideControl

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
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
                    slideControl.Selected = slideDatos;
                    tcEstructuraCocina.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SelectedIndex = -1;
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    dtpFechaAlta.Enabled = true;
                    dtpFechaAlta.SetFechaNull();
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    dtpFechaModificacion.SetFechaNull();
                    txtDescripcion.ReadOnly = false;
                    nudcosto.Value = 0;
                    nudcosto.Enabled = true;
                    lblPeso.BackColor = System.Drawing.Color.White;
                    chkFijo.Enabled = true;
                    chkFijo.Checked = false;
                    txtDescripcion.Clear();
                    dsEstructura.LISTA_PARTES.Clear();
                    dvCE.RowFilter = "ESTR_CODIGO = -1";
                    dvPE.RowFilter = "ESTR_CODIGO = -1";
                    CrearGrupoCero();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbAC.Enabled = true;
                    gbAP.Enabled = true;
                    gbOC.Enabled = true;
                    gbOP.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    CargarCSCPMP();
                    slideControl.Selected = slideDatos;
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SelectedIndex = -1;
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione");
                    dtpFechaAlta.Enabled = true;
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbCocina.SelectedIndex = -1;
                    cbResponsable.Enabled = true;
                    cbResponsable.SelectedIndex = -1;
                    dtpFechaModificacion.Enabled = true;
                    dtpFechaModificacion.SetFechaNull();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    nudcosto.Enabled = true;
                    nudcosto.Value = 0;
                    lblPeso.BackColor = System.Drawing.Color.White;
                    chkFijo.Enabled = true;
                    chkFijo.Checked = false;
                    dsEstructura.LISTA_PARTES.Clear();
                    dvCE.RowFilter = "ESTR_CODIGO = -1";
                    dvPE.RowFilter = "ESTR_CODIGO = -1";
                    CrearGrupoCero();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbAC.Enabled = true;
                    gbAP.Enabled = true;
                    gbOC.Enabled = true;
                    gbOP.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    CargarCSCPMP();
                    slideControl.Selected = slideDatos;
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    btnDatos.PerformClick();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbPlano.Enabled = false;
                    cbEstado.Enabled = false;
                    dtpFechaAlta.Enabled = false;
                    cbCocina.Enabled = false;
                    cbResponsable.Enabled = false;
                    dtpFechaModificacion.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    nudcosto.Enabled = false;
                    lblPeso.BackColor = System.Drawing.Color.Empty;
                    chkFijo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    gbAC.Enabled = false;
                    gbAP.Enabled = false;
                    gbOC.Enabled = false;
                    gbOP.Enabled = false;
                    estadoInterface = estadoUI.consultar;
                    CargarCSCPMP();
                    CargarListaPartes();
                    slideControl.Selected = slideDatos;
                    btnDatos.PerformClick();
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    cbPlano.Enabled = true;
                    cbEstado.Enabled = true;
                    dtpFechaAlta.Enabled = true;
                    cbCocina.Enabled = true;
                    cbResponsable.Enabled = true;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    nudcosto.Enabled = true;
                    lblPeso.BackColor = System.Drawing.Color.White;
                    chkFijo.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbAC.Enabled = true;
                    gbAP.Enabled = true;
                    gbOC.Enabled = true;
                    gbOP.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    CargarCSCPMP();
                    CargarListaPartes();
                    slideControl.Selected = slideDatos;
                    btnDatos.PerformClick();
                    tcEstructuraCocina.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudC.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void SetGrillasCombosVistas()
        {
            //Obtenemos los datos iniciales necesarios: terminaciones, empleados, cocinas
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.CocinaBLL.ObtenerCocinas(dsCocina.COCINAS);
                BLL.EmpleadoBLL.ObtenerEmpleados(dsEmpleado.EMPLEADOS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region Buscar
            //Grilla Listado Estructuras
            dgvEstructuras.AutoGenerateColumns = false;
            dgvEstructuras.Columns.Add("ESTR_NOMBRE", "Nombre");
            dgvEstructuras.Columns.Add("COC_CODIGO", "Cocina");
            dgvEstructuras.Columns.Add("PNO_CODIGO", "Plano");
            dgvEstructuras.Columns.Add("E_CODIGO", "Responsable");
            dgvEstructuras.Columns.Add("ESTR_ACTIVO", "Activo");
            dgvEstructuras.Columns.Add("ESTR_FECHA_ALTA", "Fecha creación");
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstructuras.Columns["ESTR_ACTIVO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEstructuras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].MinimumWidth = 110;
            dgvEstructuras.Columns["ESTR_NOMBRE"].DataPropertyName = "ESTR_NOMBRE";
            dgvEstructuras.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvEstructuras.Columns["PNO_CODIGO"].DataPropertyName = "PNO_CODIGO";
            dgvEstructuras.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvEstructuras.Columns["ESTR_ACTIVO"].DataPropertyName = "ESTR_ACTIVO";
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DataPropertyName = "ESTR_FECHA_ALTA";

            //Dataviews
            dvEstructuras = new DataView(dsEstructura.ESTRUCTURAS);
            dvEstructuras.Sort = "ESTR_NOMBRE ASC";
            dgvEstructuras.DataSource = dvEstructuras;
            dvCocinaBuscar = new DataView(dsCocina.COCINAS);
            dvResponsableBuscar = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsableBuscar.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            dvPlanoBuscar = new DataView(dsEstructura.PLANOS);

            //ComboBoxs
            cbCocinaBuscar.SetDatos(dvCocinaBuscar, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "--TODOS--", true);
            string[] displaymember = { "E_APELLIDO", "E_NOMBRE" };
            cbResponsableBuscar.SetDatos(dvResponsableBuscar, "E_CODIGO", displaymember, ", ", "--TODOS--", true);
            cbPlanoBuscar.SetDatos(dvPlanoBuscar, "PNO_CODIGO", "PNO_NOMBRE", "--TODOS--", true);
            cbActivoBuscar.Items.Add(new Sistema.Item("--TODOS--", -1));
            cbActivoBuscar.Items.Add(new Sistema.Item("SI", 1));
            cbActivoBuscar.Items.Add(new Sistema.Item("NO", 0));
            cbActivoBuscar.DisplayMember = "Name";
            cbActivoBuscar.ValueMember = "Value";
            cbActivoBuscar.SelectedIndex = 0;

            #endregion Buscar

            #region Datos
            //Grilla Listado Partes
            dgvPartes.AutoGenerateColumns = false;
            dgvPartes.Columns.Add("PAR_TIPO", "Tipo");
            dgvPartes.Columns.Add("PAR_CODIGO", "Código");
            dgvPartes.Columns.Add("PAR_NOMBRE", "Nombre");
            dgvPartes.Columns.Add("PAR_TERMINACION", "Terminación");
            dgvPartes.Columns.Add("PAR_CANTIDAD", "Cantidad");
            dgvPartes.Columns.Add("PAR_UMED", "Unidad Medida");
            dgvPartes.Columns["PAR_TIPO"].DataPropertyName = "PAR_TIPO";
            dgvPartes.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvPartes.Columns["PAR_NOMBRE"].DataPropertyName = "PAR_NOMBRE";
            dgvPartes.Columns["PAR_TERMINACION"].DataPropertyName = "PAR_TERMINACION";
            dgvPartes.Columns["PAR_CANTIDAD"].DataPropertyName = "PAR_CANTIDAD";
            dgvPartes.Columns["PAR_UMED"].DataPropertyName = "PAR_UMED";
            dgvPartes.Columns["PAR_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //Dataviews
            dvCocina = new DataView(dsCocina.COCINAS);
            dvCocina.Sort = "COC_CODIGO_PRODUCTO ASC";
            dvResponsable = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsable.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            dvPlano = new DataView(dsEstructura.PLANOS);
            dvPlano.Sort = "PNO_NOMBRE ASC";
            dvPartes = new DataView(dsEstructura.LISTA_PARTES);
            dgvPartes.DataSource = dvPartes;

            //ComboBoxs
            cbCocina.SetDatos(dvCocina, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione", false);
            cbResponsable.SetDatos(dvResponsable, "E_CODIGO", displaymember, ", ", "Seleccione", false);
            cbPlano.SetDatos(dvPlano, "PNO_CODIGO", "PNO_NOMBRE", "Seleccione", false);
            string[] nombres = { "Activa", "Inactiva" };
            int[] valores = { BLL.EstructuraBLL.EstructuraActiva, BLL.EstructuraBLL.EstructuraInactiva };
            cbEstado.SetDatos(nombres, valores, "Seleccione", false);

            #endregion Datos

            #region Conjuntos
            //Grilla Listado Conjuntos Disponibles
            dgvCD.AutoGenerateColumns = false;
            dgvCD.Columns.Add("CONJ_CODIGOPARTE", "Código");
            dgvCD.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvCD.Columns.Add("CONJ_COSTO", "Costo");
            dgvCD.Columns.Add("CONJ_DESCRIPCION", "Descripción");
            dgvCD.Columns["CONJ_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCD.Columns["CONJ_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCD.Columns["CONJ_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCD.Columns["CONJ_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvCD.Columns["CONJ_CODIGOPARTE"].DataPropertyName = "CONJ_CODIGOPARTE";
            dgvCD.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_NOMBRE";
            dgvCD.Columns["CONJ_COSTO"].DataPropertyName = "CONJ_COSTO";
            dgvCD.Columns["CONJ_DESCRIPCION"].DataPropertyName = "CONJ_DESCRIPCION";

            //Grilla Listado Conjuntos en Estructura
            dgvCE.AutoGenerateColumns = false;
            dgvCE.Columns.Add("CONJ_CODIGOPARTE", "Código");
            dgvCE.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvCE.Columns.Add("CXE_CANTIDAD", "Cantidad");
            dgvCE.Columns["CXE_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCE.Columns.Add("GRP_CODIGO", "Grupo");
            dgvCE.Columns["GRP_CODIGO"].Visible = false;
            dgvCE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCE.Columns["CONJ_CODIGOPARTE"].DataPropertyName = "CONJ_CODIGO";
            dgvCE.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_CODIGO";
            dgvCE.Columns["CXE_CANTIDAD"].DataPropertyName = "CXE_CANTIDAD";
            dgvCE.Columns["GRP_CODIGO"].DataPropertyName = "GRP_CODIGO";

            //Dataviews
            dvCD = new DataView(dsEstructura.CONJUNTOS);
            dvCD.Sort = "CONJ_NOMBRE ASC";
            dvCE = new DataView(dsEstructura.CONJUNTOSXESTRUCTURA);
            dgvCD.DataSource = dvCD;
            dgvCE.DataSource = dvCE;
            #endregion Conjuntos

            #region Piezas
            //Grilla listado de piezas disponibles
            dgvPD.AutoGenerateColumns = false;
            dgvPD.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPD.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPD.Columns.Add("TE_NOMBRE", "Terminación");
            dgvPD.Columns.Add("PZA_COSTO", "Costo");
            dgvPD.Columns.Add("PZA_DESCRIPCION", "Descripción");
            dgvPD.Columns["PZA_CODIGOPARTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["PZA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["TE_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["PZA_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPD.Columns["PZA_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvPD.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGOPARTE";
            dgvPD.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_NOMBRE";
            dgvPD.Columns["TE_NOMBRE"].DataPropertyName = "TE_CODIGO";
            dgvPD.Columns["PZA_COSTO"].DataPropertyName = "PZA_COSTO";
            dgvPD.Columns["PZA_DESCRIPCION"].DataPropertyName = "PZA_DESCRIPCION";

            //Grilla Listado piezas en Estructura
            dgvPE.AutoGenerateColumns = false;
            dgvPE.Columns.Add("PZA_CODIGOPARTE", "Código");
            dgvPE.Columns.Add("PZA_NOMBRE", "Nombre");
            dgvPE.Columns.Add("TE_NOMBRE", "Terminación");
            dgvPE.Columns.Add("PXE_CANTIDAD", "Cantidad");
            dgvPE.Columns["PXE_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPE.Columns.Add("GRP_CODIGO", "Grupo");
            dgvPE.Columns["GRP_CODIGO"].Visible = false;
            dgvPE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvPE.Columns["PZA_CODIGOPARTE"].DataPropertyName = "PZA_CODIGO";
            dgvPE.Columns["PZA_NOMBRE"].DataPropertyName = "PZA_CODIGO";
            dgvPE.Columns["TE_NOMBRE"].DataPropertyName = "PZA_CODIGO";
            dgvPE.Columns["PXE_CANTIDAD"].DataPropertyName = "PXE_CANTIDAD";
            dgvPE.Columns["GRP_CODIGO"].DataPropertyName = "GRP_CODIGO";

            //Dataviews
            dvPD = new DataView(dsEstructura.PIEZAS);
            dvPD.Sort = "PZA_NOMBRE ASC";
            dvPE = new DataView(dsEstructura.PIEZASXESTRUCTURA);
            dgvPD.DataSource = dvPD;
            dgvPE.DataSource = dvPE;
            #endregion Piezas

        }

        private void CargarListaPartes()
        {
            dsEstructura.LISTA_PARTES.Clear();
            int codigoEstructura = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
            else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }

            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in
                (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.ESTR_CODIGO = codigoEstructura.ToString();
                rowParte.PAR_TIPO = "Conjunto";
                rowParte.PAR_CODIGO = row.CONJUNTOSRow.CONJ_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.CONJUNTOSRow.CONJ_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.CXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in
                (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.ESTR_CODIGO = codigoEstructura.ToString();
                rowParte.PAR_TIPO = "Pieza";
                rowParte.PAR_CODIGO = row.PIEZASRow.PZA_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.PIEZASRow.PZA_NOMBRE;
                rowParte.PAR_TERMINACION = dsEstructura.TERMINACIONES.FindByTE_CODIGO(row.PIEZASRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.PXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            dvPartes.Table = dsEstructura.LISTA_PARTES;
        }

        private void frmEstructuraCocina_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled) { txtNombreBuscar.Focus(); }
        }

        private void CargarCSCPMP()
        {
            try
            {
                BLL.ConjuntoBLL.ObtenerConjuntos(dsEstructura.CONJUNTOS, BLL.ConjuntoBLL.estadoActivo);
                BLL.PiezaBLL.ObtenerPiezas(dsEstructura.PIEZAS, BLL.PiezaBLL.estadoActivo);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsUnidadMedida.UNIDADES_MEDIDA);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Carga de partes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearGrupoCero()
        {
            //Agrego el grupo 0 que tiene toda estructura - cambiar de lugar - gonzalo
            Data.dsEstructura.GRUPOS_ESTRUCTURARow row = dsEstructura.GRUPOS_ESTRUCTURA.NewGRUPOS_ESTRUCTURARow();
            row.BeginEdit();
            row.GRP_CODIGO = -1;
            row.GRP_NOMBRE = "Grupo 0";
            row.GRP_NUMERO = 0;
            row.SetGRP_PADRE_CODIGONull();
            row.GRP_DESCRIPCION = "grupo 0";
            row.GRP_CONCRETO = 0;
            row.ESTR_CODIGO = -1;
            row.EndEdit();
            dsEstructura.GRUPOS_ESTRUCTURA.AddGRUPOS_ESTRUCTURARow(row);
        }

        private decimal CalcularCosto()
        {
            decimal costo = 0;
            int codigoEstructura = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
            else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }

            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in
                (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                costo += (row.CONJUNTOSRow.CONJ_COSTO * row.CXE_CANTIDAD);
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in
                (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                costo += (row.PIEZASRow.PZA_COSTO * row.PXE_CANTIDAD);
            }

            return costo;
        }

        private void chkFijo_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFijo.Checked) { nudcosto.Value = CalcularCosto(); }
            else { nudcosto.Value = 0; }
        }

        #endregion Servicios

        #region Cell_Formatting y RowEnter

        private void dgvEstructuras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre; //tira error despues de guardar - gonzalo
                switch (dgvEstructuras.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsCocina.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "PNO_CODIGO":
                        nombre = dsEstructura.PLANOS.FindByPNO_CODIGO(Convert.ToInt32(e.Value)).PNO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "E_CODIGO":
                        nombre = dsEmpleado.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_APELLIDO;
                        nombre += ", " + dsEmpleado.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "ESTR_ACTIVO":
                        nombre = "No";
                        if (Convert.ToInt32(e.Value) == 1) { nombre = "Si"; }
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvCD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void dgvCE_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                int codigo;
                switch (dgvCE.Columns[e.ColumnIndex].Name)
                {
                    case "CONJ_CODIGOPARTE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).CONJ_CODIGOPARTE;
                        e.Value = nombre;
                        break;
                    case "CONJ_NOMBRE":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).CONJ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "GRP_CODIGO":
                        codigo = Convert.ToInt32(e.Value.ToString());
                        nombre = dsEstructura.GRUPOS_ESTRUCTURA.FindByGRP_CODIGO(codigo).GRP_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvPD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                if (dgvPD.Columns[e.ColumnIndex].Name == "TE_NOMBRE")
                {
                    nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value.ToString())).TE_NOMBRE;
                    e.Value = nombre;
                }
            }
        }

        private void dgvPE_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string nombre;
            int codigo;
            switch (dgvPE.Columns[e.ColumnIndex].Name)
            {
                case "PZA_CODIGOPARTE":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).PZA_CODIGOPARTE;
                    e.Value = nombre;
                    break;
                case "PZA_NOMBRE":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).PZA_NOMBRE;
                    e.Value = nombre;
                    break;
                case "TE_NOMBRE":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigo).TERMINACIONESRow.TE_NOMBRE;
                    e.Value = nombre;
                    break;
                case "GRP_CODIGO":
                    codigo = Convert.ToInt32(e.Value.ToString());
                    nombre = dsEstructura.GRUPOS_ESTRUCTURA.FindByGRP_CODIGO(codigo).GRP_NOMBRE;
                    e.Value = nombre;
                    break;
                default:
                    break;
            }
        }

        private void dgvEstructuras_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codEstructura = Convert.ToInt32(dvEstructuras[e.RowIndex]["estr_codigo"]);
            txtNombre.Text = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_NOMBRE;
            cbPlano.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).PLANOSRow.PNO_CODIGO));
            cbEstado.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_ACTIVO));
            dtpFechaAlta.SetFecha(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_FECHA_ALTA);
            cbCocina.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).COC_CODIGO));
            if (!dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).IsE_CODIGONull())
            {
                cbResponsable.SetSelectedValue(Convert.ToInt32(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).E_CODIGO));
            }
            else { cbResponsable.SetSelectedIndex(-1); }
            if (!dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).IsESTR_FECHA_MODIFICACIONNull())
            {
                dtpFechaModificacion.SetFecha(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_FECHA_MODIFICACION);
            }
            else { dtpFechaModificacion.SetFechaNull(); }
            txtDescripcion.Text = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_DESCRIPCION;
            nudcosto.Value = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_COSTO;
            if (dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_COSTOFIJO == 0) { chkFijo.Checked = false; }
            else { chkFijo.Checked = true; }
            dvCE.RowFilter = "ESTR_CODIGO = " + codEstructura;
            dvPE.RowFilter = "ESTR_CODIGO = " + codEstructura;
        }

        #endregion

        

    }
}

