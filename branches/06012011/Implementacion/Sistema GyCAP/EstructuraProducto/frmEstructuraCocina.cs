﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmEstructuraCocina : Form
    {
        private static frmEstructuraCocina _frmEstructuraCocina = null;
        private Data.dsEstructuraProducto dsEstructura = new GyCAP.Data.dsEstructuraProducto();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private DataView dvCocinaBuscar, dvCocina, dvResponsableBuscar, dvResponsable, dvPlanoBuscar, dvPlano;
        private DataView dvEstructuras, dvListaPartes, dvPartesDisponibles, dvMPDisponibles, dvFiltroTipo;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private int compId = -1; //Variables para el manejo de inserciones en los dataset con códigos unique

        #region Inicio

        public frmEstructuraCocina()
        {
            InitializeComponent();

            SetGrillasCombosVistas();
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
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Estructura seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                dsEstructura.ESTRUCTURAS.Clear();
                BLL.EstructuraBLL.ObtenerEstructuras(txtNombreBuscar.Text, cbPlanoBuscar.GetSelectedValue(), dtpFechaAltaBuscar.GetFecha(), cbCocinaBuscar.GetSelectedValue(), cbResponsableBuscar.GetSelectedValue(), cboActivoBuscar.GetSelectedValue(), dsEstructura);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                //dvEstructuras.Table = dsEstructura.ESTRUCTURAS;
                if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Estructuras", this.Text);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
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
            if (dgvPartesEstructura.Rows.Count == 0) { datosFaltantes += "* El detalle de la estructura\n"; } //que al menos haya cargado 1 parte
            if (cbEstado.GetSelectedIndex() == -1) { datosFaltantes += "* Estado\n"; }
            if (datosFaltantes == string.Empty)
            {
                //Datos OK, revisemos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Creando uno nuevo
                    try
                    {
                        Data.dsEstructuraProducto.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.NewESTRUCTURASRow();
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
            dsEstructura.COMPUESTOS_PARTES.RejectChanges();
            dsEstructura.PARTES.RejectChanges();
            dsEstructura.ESTRUCTURAS.RejectChanges();
            //dsEstructura.LISTA_PARTES.Clear();
            SetInterface(estadoUI.inicio);
        }        

        #endregion    

        #region Partes

        private void btnVolverDePartes_Click(object sender, EventArgs e)
        {
            tcEstructuraProducto.SelectedTab = tpDatos;
        }

        private void btnAgregarParte_Click(object sender, EventArgs e)
        {
            if (tcPartesDisponibles.SelectedTab == tpPartesDisponibles)
            {
                if (dgvPartesDisponibles.SelectedRows.Count > 0)
                {
                    //agrego la parte
                    //Condiciones:
                    //              - que el padre no sea ella misma
                    //              - que el padre no sea una MP
                    int numeroParte = Convert.ToInt32(dvPartesDisponibles[dgvPartesDisponibles.SelectedRows[0].Index]["part_numero"]);
                    AgregarParteAArbol(dsEstructura.PARTES.FindByPART_NUMERO(numeroParte), null);
                }
                else
                {
                    MensajesABM.MsjSinSeleccion("Parte", MensajesABM.Generos.Femenino, this.Text);
                }
            }
            else
            {
                if (dgvMPDisponibles.SelectedRows.Count > 0)
                {
                    //agrego la MP
                    //Condiciones:
                    //              - que no sea una raíz
                    //              - que no sea hija de otra MP
                    int codigoMP = Convert.ToInt32(dvMPDisponibles[dgvMPDisponibles.SelectedRows[0].Index]["mp_codigo"]);
                    AgregarParteAArbol(null, dsEstructura.MATERIAS_PRIMAS.FindByMP_CODIGO(codigoMP));
                }
                else
                {
                    MensajesABM.MsjSinSeleccion("Materia prima", MensajesABM.Generos.Femenino, this.Text);
                }
            }
        }

        private void AgregarParteAArbol(Data.dsEstructuraProducto.PARTESRow rowParte, Data.dsEstructuraProducto.MATERIAS_PRIMASRow rowMP)
        {
            List<string> validacion = new List<string>();
            if (tvEstructura.Nodes.Count != 0 && nudCantidadAgregar.Value == 0) { validacion.Add("Cantidad"); }
            if (tvEstructura.Nodes.Count != 0 && tvEstructura.SelectedNode == null) { validacion.Add("Parte padre"); }
            if (tvEstructura.Nodes.Count == 0 && rowParte == null) { validacion.Add("Una materia prima no puede ser raíz"); }
            if (tvEstructura.SelectedNode != null && 
                rowParte != null && 
                Convert.ToInt32(tvEstructura.SelectedNode.Tag) == BLL.CompuestoParteBLL.HijoEsParte && 
                tvEstructura.SelectedNode.Parent != null && 
                rowParte.PART_NUMERO == dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToDecimal(tvEstructura.SelectedNode.Name)).PART_NUMERO_HIJO) { validacion.Add("Una parte no puede ser padre e hijo al mismo tiempo"); }
            if (tvEstructura.SelectedNode != null && rowParte != null && Convert.ToInt32(tvEstructura.SelectedNode.Tag) == BLL.CompuestoParteBLL.HijoEsMP) { validacion.Add("Una parte no puede ser hijo de una materia prima"); }
            if (tvEstructura.SelectedNode != null && rowMP != null && Convert.ToInt32(tvEstructura.SelectedNode.Tag) == BLL.CompuestoParteBLL.HijoEsMP) { validacion.Add("Una materia prima no puede ser hijo de una materia prima"); }

            if (validacion.Count == 0)
            {
                TreeNode nodoHijo = null;
                //Creamos el nodo hijo                
                if (rowParte != null)
                {
                    //Estoy agregando una parte
                    nodoHijo = new TreeNode();
                    nodoHijo.Name = compId.ToString();
                    nodoHijo.Text = rowParte.PART_NOMBRE + " - " + rowParte.PART_CODIGO;
                    if (tvEstructura.Nodes.Count != 0) { nodoHijo.Text += " / #" + nudCantidadAgregar.Value.ToString() + " " + rowParte.UNIDADES_MEDIDARow.UMED_ABREVIATURA; }
                    nodoHijo.Tag = BLL.CompuestoParteBLL.HijoEsParte;
                    AgregarParteADataset(rowParte, null, (tvEstructura.SelectedNode != null) ? tvEstructura.SelectedNode.Name : null);
                }
                else
                {
                    //Estoy agregando una mp
                    nodoHijo = new TreeNode();
                    nodoHijo.Name = compId.ToString();
                    nodoHijo.Text = rowMP.MP_NOMBRE + " / #" + nudCantidadAgregar.Value.ToString() + " " + rowMP.UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                    nodoHijo.Tag = BLL.CompuestoParteBLL.HijoEsMP;
                    AgregarParteADataset(null, rowMP, (tvEstructura.SelectedNode != null) ? tvEstructura.SelectedNode.Name : null);
                }

                //Verificamos si es el primer nodo que se agrega o ya existe algo para determinar el padre
                if (tvEstructura.Nodes.Count > 0)
                {
                    tvEstructura.SelectedNode.Nodes.Add(nodoHijo);
                    tvEstructura.SelectedNode.Expand();
                }
                else
                {
                    tvEstructura.Nodes.Add(nodoHijo);
                    AgregarParteADataset(rowParte, null, null);
                }
                nudCantidadAgregar.Value = 0;
                tvEstructura.SelectedNode = null;
            }
            else
            {
                MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(MensajesABM.Validaciones.CompletarDatos, validacion), this.Text);
            }
        }

        private void AgregarParteADataset(Data.dsEstructuraProducto.PARTESRow rowParte, Data.dsEstructuraProducto.MATERIAS_PRIMASRow rowMP, object padre)
        {
            if (padre != null)
            {
                Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
                rowCompuesto.BeginEdit();
                rowCompuesto.COMP_CODIGO = compId;
                compId--;
                rowCompuesto.COMP_CANTIDAD = nudCantidadAgregar.Value;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { rowCompuesto.ESTR_CODIGO = -1; }
                else { rowCompuesto.ESTR_CODIGO = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }
                rowCompuesto.PART_NUMERO_PADRE = Convert.ToInt32(padre);
                if (rowParte != null)
                {
                    rowCompuesto.SetMP_CODIGONull();
                    rowCompuesto.PART_NUMERO_HIJO = rowParte.PART_NUMERO;
                    rowCompuesto.UMED_CODIGO = rowParte.UMED_CODIGO;
                }
                else
                {
                    rowCompuesto.SetPART_NUMERO_HIJONull();
                    rowCompuesto.MP_CODIGO = rowMP.MP_CODIGO;
                    rowCompuesto.UMED_CODIGO = rowMP.UMED_CODIGO;
                }
                rowCompuesto.EndEdit();
                dsEstructura.COMPUESTOS_PARTES.AddCOMPUESTOS_PARTESRow(rowCompuesto);
            }
            else
            {
                int codEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).PART_NUMERO = rowParte.PART_NUMERO;
            }
        }

        private void btnDeleteParte_Click(object sender, EventArgs e)
        {
            if (tvEstructura.SelectedNode != null)
            {
                foreach (TreeNode nodo in tvEstructura.SelectedNode.Nodes)
                {
                    dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(nodo.Name)).Delete();
                }

                if (tvEstructura.SelectedNode.Parent != null)
                {
                    dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).Delete();
                }
                else
                {
                    int codEstr = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                    dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstr).SetPART_NUMERONull();
                }

                tvEstructura.SelectedNode.Remove();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Parte", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnSumarParte_Click(object sender, EventArgs e)
        {
            if (tvEstructura.SelectedNode != null)
            {
                string nodeText = string.Empty;
                if (dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).IsMP_CODIGONull())
                {
                    dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD += 1;
                    nodeText = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_NOMBRE;
                    nodeText += " - ";
                    nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_CODIGO;
                    nodeText += " / #";
                    nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD.ToString();
                    nodeText += " ";
                    nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                }
                else
                {
                    dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD += Convert.ToDecimal("0,1");
                    nodeText = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).MATERIAS_PRIMASRow.MP_NOMBRE;
                    nodeText += " / #";
                    nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD.ToString();
                    nodeText += " ";
                    nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).UNIDADES_MEDIDARow.UMED_ABREVIATURA;                    
                }
                tvEstructura.BeginUpdate();
                tvEstructura.SelectedNode.Text = nodeText;
                tvEstructura.EndUpdate();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Parte", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnRestarParte_Click(object sender, EventArgs e)
        {
            if (tvEstructura.SelectedNode != null)
            {
                string nodeText = tvEstructura.SelectedNode.Text;
                if (dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).IsMP_CODIGONull())
                {
                    if (dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD > 1)
                    {
                        dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD -= 1;
                        nodeText = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_NOMBRE;
                        nodeText += " - ";
                        nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_CODIGO;
                        nodeText += " / #";
                        nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD.ToString();
                        nodeText += " ";
                        nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                    }
                }
                else
                {
                    if (dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD > Convert.ToDecimal("0,1"))
                    {
                        dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD -= Convert.ToDecimal("0,1");
                        nodeText = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).MATERIAS_PRIMASRow.MP_NOMBRE;
                        nodeText += " / #";
                        nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).COMP_CANTIDAD.ToString();
                        nodeText += " ";
                        nodeText += dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                    }
                }
                tvEstructura.BeginUpdate();
                tvEstructura.SelectedNode.Text = nodeText;
                tvEstructura.EndUpdate();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Parte", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        #endregion

        private void btnDatos_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPartes_Click(object sender, EventArgs e)
        {
            tcEstructuraProducto.SelectedTab = tpPartes;
        }

        private void btnPiezas_Click(object sender, EventArgs e)
        {
            
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
                    tcEstructuraProducto.SelectedTab = tpBuscar;
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
                    chkFijo.Enabled = true;
                    chkFijo.Checked = false;
                    txtDescripcion.Clear();
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcEstructuraProducto.SelectedTab = tpDatos;
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
                    chkFijo.Enabled = true;
                    chkFijo.Checked = false;
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcEstructuraProducto.SelectedTab = tpDatos;
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
                    chkFijo.Enabled = false;
                    panelAccionesArbol.Enabled = false;
                    gbAgregarParteMP.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    btnDatos.PerformClick();
                    tcEstructuraProducto.SelectedTab = tpDatos;
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
                    chkFijo.Enabled = true;
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    btnDatos.PerformClick();
                    tcEstructuraProducto.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
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
                BLL.ParteBLL.ObtenerPartes(null, null, null, null, null, null, dsEstructura.PARTES);
                BLL.MateriaPrimaBLL.ObtenerTodos(dsEstructura.MATERIAS_PRIMAS);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsEstructura.UNIDADES_MEDIDA);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.TipoParteBLL.ObtenerTodos(dsEstructura.TIPOS_PARTES);
                BLL.EstadoParteBLL.ObtenerTodos(dsEstructura.ESTADO_PARTES);
                BLL.HojaRutaBLL.ObtenerHojasRuta(dsEstructura.HOJAS_RUTA);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
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
            string[] nombres = { "Activa", "Inactiva" };
            int[] valores = { BLL.EstructuraBLL.EstructuraActiva, BLL.EstructuraBLL.EstructuraInactiva };
            cboActivoBuscar.SetDatos(nombres, valores, "TODOS", true);            

            #endregion Buscar

            #region Datos
            //Grilla Listado Partes
            dgvPartesEstructura.AutoGenerateColumns = false;
            dgvPartesEstructura.Columns.Add("PART_NOMBRE", "Parte");
            dgvPartesEstructura.Columns.Add("PART_TIPO", "Tipo");
            dgvPartesEstructura.Columns.Add("COMP_CANTIDAD", "Cantidad");
            dgvPartesEstructura.Columns.Add("UMED_CODIGO", "Unidad medida");
            dgvPartesEstructura.Columns["PART_NOMBRE"].DataPropertyName = "COMP_CODIGO";
            dgvPartesEstructura.Columns["PART_TIPO"].DataPropertyName = "COMP_CODIGO";
            dgvPartesEstructura.Columns["COMP_CANTIDAD"].DataPropertyName = "COMP_CANTIDAD";
            dgvPartesEstructura.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvPartesEstructura.Columns["COMP_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartesEstructura.Columns["PART_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesEstructura.Columns["PART_TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesEstructura.Columns["COMP_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesEstructura.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dvListaPartes = new DataView(dsEstructura.COMPUESTOS_PARTES);
            dgvPartesEstructura.DataSource = dvListaPartes;

            //Dataviews
            dvCocina = new DataView(dsCocina.COCINAS);
            dvCocina.Sort = "COC_CODIGO_PRODUCTO ASC";
            dvResponsable = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsable.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            dvPlano = new DataView(dsEstructura.PLANOS);
            dvPlano.Sort = "PNO_NOMBRE ASC";

            //ComboBoxs
            cbCocina.SetDatos(dvCocina, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione", false);
            cbResponsable.SetDatos(dvResponsable, "E_CODIGO", displaymember, ", ", "Seleccione", false);
            cbPlano.SetDatos(dvPlano, "PNO_CODIGO", "PNO_NOMBRE", "Seleccione", false);
            cbEstado.SetDatos(nombres, valores, "Seleccione", false);

            #endregion Datos

            #region Partes y MP disponibles

            //Grilla materias primas disponibles
            dgvMPDisponibles.AutoGenerateColumns = false;
            dgvMPDisponibles.Columns.Add("MP_NOMBRE", "Nombre");
            dgvMPDisponibles.Columns.Add("UMED_CODIGO", "Unidad Medida");
            dgvMPDisponibles.Columns.Add("MP_COSTO", "Costo");
            dgvMPDisponibles.Columns.Add("MP_DESCRIPCION", "Descripción");
            dgvMPDisponibles.Columns["MP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPDisponibles.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPDisponibles.Columns["MP_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMPDisponibles.Columns["MP_COSTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMPDisponibles.Columns["MP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMPDisponibles.Columns["MP_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvMPDisponibles.Columns["MP_NOMBRE"].DataPropertyName = "MP_NOMBRE";
            dgvMPDisponibles.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvMPDisponibles.Columns["MP_COSTO"].DataPropertyName = "MP_COSTO";
            dgvMPDisponibles.Columns["MP_DESCRIPCION"].DataPropertyName = "MP_DESCRIPCION";
            dvMPDisponibles = new DataView(dsEstructura.MATERIAS_PRIMAS);
            dvMPDisponibles.Sort = "MP_NOMBRE ASC";
            dgvMPDisponibles.DataSource = dvMPDisponibles;

            //Grilla partes disponibles
            dgvPartesDisponibles.AutoGenerateColumns = false;
            dgvPartesDisponibles.Columns.Add("PART_NOMBRE", "Nombre");
            dgvPartesDisponibles.Columns.Add("PART_CODIGO", "Código");
            dgvPartesDisponibles.Columns.Add("PNO_CODIGO", "Plano");
            dgvPartesDisponibles.Columns.Add("PAR_CODIGO", "Estado");
            dgvPartesDisponibles.Columns.Add("TPAR_CODIGO", "Tipo de parte");
            dgvPartesDisponibles.Columns.Add("TE_CODIGO", "Terminación");
            dgvPartesDisponibles.Columns.Add("HR_CODIGO", "Hoja de ruta");
            dgvPartesDisponibles.Columns["PART_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["PART_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["PNO_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["PAR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["TPAR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["TE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["HR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPartesDisponibles.Columns["PART_NOMBRE"].DataPropertyName = "PART_NOMBRE";
            dgvPartesDisponibles.Columns["PART_CODIGO"].DataPropertyName = "PART_CODIGO";
            dgvPartesDisponibles.Columns["PNO_CODIGO"].DataPropertyName = "PNO_CODIGO";
            dgvPartesDisponibles.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvPartesDisponibles.Columns["TPAR_CODIGO"].DataPropertyName = "TPAR_CODIGO";
            dgvPartesDisponibles.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvPartesDisponibles.Columns["HR_CODIGO"].DataPropertyName = "HR_CODIGO";
            dvPartesDisponibles = new DataView(dsEstructura.PARTES);
            dvPartesDisponibles.Sort = "PART_NOMBRE ASC";
            dgvPartesDisponibles.DataSource = dvPartesDisponibles;

            dvFiltroTipo = new DataView(dsEstructura.TIPOS_PARTES);
            dvFiltroTipo.Sort = "TPAR_NOMBRE ASC";
            cboFiltroTipoParte.SetDatos(dvFiltroTipo, "TPAR_CODIGO", "TPAR_NOMBRE", "TODOS", true);
            #endregion

        }
        
        private decimal CalcularCosto()
        {
            decimal costo = 0;
            int codigoEstructura = 0;
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEstructura = -1; }
            else { codigoEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }

            foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow row in
                (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select("ESTR_CODIGO = " + codigoEstructura))
            {
                costo += (row.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_COSTO * row.COMP_CANTIDAD);
            }

            return costo;
        }

        private void chkFijo_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFijo.Checked) { nudcosto.Value = CalcularCosto(); }
            else { nudcosto.Value = 0; }
        }

        private void CrearArbol(int codigoEstructura)
        {
            


        }

        #endregion Servicios

        #region Cell_Formatting y RowEnter

        private void dgvEstructuras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty; //tira error despues de guardar - gonzalo
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

        private void dgvPartesEstructura_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;
                
                switch (dgvPartesEstructura.Columns[e.ColumnIndex].Name)
                {
                    case "PART_NOMBRE":
                        if (dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(e.Value)).IsPART_NUMERO_HIJONull())
                            { nombre = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(e.Value)).MATERIAS_PRIMASRow.MP_NOMBRE; }
                        else { nombre = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(e.Value)).PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_NOMBRE; }
                        e.Value = nombre;
                        break;
                    case "PART_TIPO":
                        if (!dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(e.Value)).IsPART_NUMERO_HIJONull())
                            { nombre = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(e.Value)).PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.TIPOS_PARTESRow.TPAR_NOMBRE; }
                        else { nombre = "Materia Prima"; }
                        e.Value = nombre;
                        break;
                    case "UMED_CODIGO":
                        nombre = dsEstructura.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvMPDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvMPDisponibles.Columns[e.ColumnIndex].Name)
                {
                    case "MP_COSTO":
                        nombre = "$ " + e.Value.ToString();
                        e.Value = nombre;
                        break;
                    case "UMED_CODIGO":
                        nombre = dsEstructura.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvPartesDisponibles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvPartesDisponibles.Columns[e.ColumnIndex].Name)
                {
                    case "PNO_CODIGO":
                        nombre = dsEstructura.PLANOS.FindByPNO_CODIGO(Convert.ToInt32(e.Value)).PNO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "PAR_CODIGO":
                        nombre = dsEstructura.ESTADO_PARTES.FindByPAR_CODIGO(Convert.ToInt32(e.Value)).PAR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TPAR_CODIGO":
                        nombre = dsEstructura.TIPOS_PARTES.FindByTPAR_CODIGO(Convert.ToInt32(e.Value)).TPAR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TE_CODIGO":
                        nombre = dsEstructura.TERMINACIONES.FindByTE_CODIGO(Convert.ToInt32(e.Value)).TE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "HR_CODIGO":
                        nombre = dsEstructura.HOJAS_RUTA.FindByHR_CODIGO(Convert.ToInt32(e.Value)).HR_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
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
            dvListaPartes.RowFilter = "ESTR_CODIGO = " + codEstructura;
        }        

        #endregion

        #region Look & Feel

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
                (sender as Button).Location = punto;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
                (sender as Button).Location = punto;
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudcosto.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void txtFiltroNombreMP_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombreMP.TextLength > 0)
            {
                dvMPDisponibles.RowFilter = "mp_nombre LIKE '" + txtFiltroNombreMP.Text + "*'";
            }
            else
            {
                dvMPDisponibles.RowFilter = string.Empty;
            }
        }

        private void txtFiltroNombreParte_TextChanged(object sender, EventArgs e)
        {
            dvPartesDisponibles.RowFilter = "1=1";
            if (txtFiltroNombreParte.TextLength > 0)
            {
                dvPartesDisponibles.RowFilter += " AND part_nombre LIKE '" + txtFiltroNombreParte.Text + "*'";
            }

            if (cboFiltroTipoParte.GetSelectedValueInt() != -1)
            {
                dvPartesDisponibles.RowFilter += " AND tpar_codigo = " + cboFiltroTipoParte.GetSelectedValueInt();
            }
        }        

        private void cboFiltroTipoParte_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dvPartesDisponibles.RowFilter = "1=1";
            if (txtFiltroNombreParte.TextLength > 0)
            {
                dvPartesDisponibles.RowFilter += " AND part_nombre LIKE '" + txtFiltroNombreParte.Text + "*'";
            }

            if (cboFiltroTipoParte.GetSelectedValueInt() != -1)
            {
                dvPartesDisponibles.RowFilter += " AND tpar_codigo = " + cboFiltroTipoParte.GetSelectedValueInt();
            }
        }

        #endregion

        

        

    }
}

