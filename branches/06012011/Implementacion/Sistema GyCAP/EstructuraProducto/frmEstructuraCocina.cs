using System;
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
        private DataView dvEstructuras, dvPartesDisponibles, dvMPDisponibles, dvFiltroTipo;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, clonar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private int compId = -1; //Variable para el manejo de inserciones en los dataset con códigos unique
        private int columnIndex = -1; //Variable para manejar el menu contextual para bloquear columnas

        #region Inicio

        public frmEstructuraCocina()
        {
            InitializeComponent();

            SetGrillasCombosVistas();
            SetInterface(estadoUI.inicio);
            SetSlide();
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

        private void btnClonar_Click(object sender, EventArgs e)
        {
            if (dgvEstructuras.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codEstructura = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);

                tvEstructura.BeginUpdate();
                if (dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + codEstructura).Length > 0)
                {
                    BLL.EstructuraBLL.CrearArbolEstructura(codEstructura, dsEstructura, tvEstructura, true, out compId);
                }
                else { tvEstructura.Nodes.Clear(); }
                tvEstructura.EndUpdate();
                SetInterface(estadoUI.clonar);
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Estructura", MensajesABM.Generos.Femenino, this.Text);
            }
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
                DialogResult respuesta = MensajesABM.MsjConfirmaEliminarDatos("Estructura", MensajesABM.Generos.Femenino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //obtenemos el código
                        int codigo = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.EstructuraBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow row in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + codigo))
                        {
                            row.Delete();
                        }
                        dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codigo).Delete();
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                        dsEstructura.COMPUESTOS_PARTES.AcceptChanges();
                        MensajesABM.MsjConfirmaEliminar(this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoActivoException)
                    {
                        MensajesABM.MsjValidacion("No se puede eliminar una estructura con estado activa.", this.Text);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Estructura", MensajesABM.Generos.Femenino, this.Text);
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
                dsEstructura.COMPUESTOS_PARTES.Clear();
                dgvEstructuras.DataSource = null;
                BLL.EstructuraBLL.ObtenerEstructuras(txtNombreBuscar.Text, cbPlanoBuscar.GetSelectedValue(), dtpFechaAltaBuscar.GetFecha(), cbCocinaBuscar.GetSelectedValue(), cbResponsableBuscar.GetSelectedValue(), cboActivoBuscar.GetSelectedValue(), dsEstructura);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                //dvEstructuras.Table = dsEstructura.ESTRUCTURAS;
                dgvEstructuras.DataSource = dvEstructuras;
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
            List<string> validacion = new List<string>();
            if (txtNombre.Text == string.Empty) { validacion.Add("Nombre"); }
            if (cbPlano.GetSelectedIndex() == -1) { validacion.Add("Plano"); }
            if (cbCocina.GetSelectedIndex() == -1) { validacion.Add("Cocina"); }
            else if (BLL.CocinaBLL.TieneEstructuraActiva(cbCocina.GetSelectedValueInt())) { validacion.Add("La cocina seleccionada ya posee una estructura activa"); }
            //if (cbResponsable.GetSelectedIndex() == -1) { datosOK = false; datosFaltantes += "\\n* Responsable"; } Por ahora opcional
            //if (dtpFechaAlta.IsValueNull()) { dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor()); } Opcional por ahora
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno || estadoInterface == estadoUI.clonar && dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = -1").Length == 0) { validacion.Add("El detalle de la estructura"); } //que al menos haya cargado 1 parte
            else if (estadoInterface == estadoUI.modificar && dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"])).Length == 0) { validacion.Add("El detalle de la estructura"); } //que al menos haya cargado 1 parte
            if (cbEstado.GetSelectedIndex() == -1) { validacion.Add("Estado"); }
            if (validacion.Count == 0)
            {
                //Datos OK, revisemos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno || estadoInterface == estadoUI.clonar)
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
                        if (cbResponsable.GetSelectedValueInt() == -1) rowEstructura.SetE_CODIGONull();
                        else { rowEstructura.E_CODIGO = cbResponsable.GetSelectedValueInt(); }
                        if (dtpFechaModificacion.IsValueNull()) { rowEstructura.SetESTR_FECHA_MODIFICACIONNull(); }
                        else { rowEstructura.ESTR_FECHA_MODIFICACION = (DateTime)dtpFechaModificacion.GetFecha(); }
                        rowEstructura.ESTR_COSTO = nudcosto.Value;
                        rowEstructura.ESTR_DESCRIPCION = txtDescripcion.Text;
                        rowEstructura.EndEdit();
                        dsEstructura.ESTRUCTURAS.AddESTRUCTURASRow(rowEstructura);
                        decimal cod = BLL.EstructuraBLL.Insertar(dsEstructura);
                        rowEstructura.BeginEdit();
                        rowEstructura.ESTR_CODIGO = cod;
                        rowEstructura.EndEdit();
                        dsEstructura.ESTRUCTURAS.AcceptChanges();
                        dsEstructura.COMPUESTOS_PARTES.AcceptChanges();
                        
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
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
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
                        BLL.EstructuraBLL.Actualizar(dsEstructura);
                        MensajesABM.MsjConfirmaGuardar("Estructura", this.Text, MensajesABM.Operaciones.Modificación);

                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de estructuras ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsEstructura.ESTRUCTURAS.RejectChanges();
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                }
            }
            else
            {
                //le faltan completar datos, avisemos
                MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(MensajesABM.Validaciones.CompletarDatos, validacion), this.Text);
            }

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsEstructura.COMPUESTOS_PARTES.RejectChanges();
            dsEstructura.ESTRUCTURAS.RejectChanges();
            SetInterface(estadoUI.inicio);
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
                costo += row.COMP_CANTIDAD * ((row.IsMP_CODIGONull()) ? row.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_COSTO : row.MATERIAS_PRIMASRow.MP_COSTO);
            }

            return costo;
        }        

        #endregion    

        #region Partes

        private void btnVolverDePartes_Click(object sender, EventArgs e)
        {            
            nudcosto.Value = CalcularCosto();
            if (estadoInterface == estadoUI.modificar)
            {
                dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"])).ESTR_COSTO = nudcosto.Value;
            }
            
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
                    //              - si ya está, avisar y preguntar si quiere aumentar la cantidad ingresada
                    //              - que ella misma no sea ancestro
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
                    //              - si ya está, avisar y preguntar si quiere aumentar la cantidad ingresada
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
            if (tvEstructura.SelectedNode != null && rowParte != null && Convert.ToInt32(tvEstructura.SelectedNode.Tag) == BLL.CompuestoParteBLL.HijoEsMP) { validacion.Add("Una parte no puede ser hijo de una materia prima"); }
            if (tvEstructura.SelectedNode != null && 
                rowParte != null && 
                Convert.ToInt32(tvEstructura.SelectedNode.Tag) == BLL.CompuestoParteBLL.HijoEsParte && 
                tvEstructura.SelectedNode.Parent != null && 
                EsMismoPadreHijo(rowParte, tvEstructura.SelectedNode)) { validacion.Add("Una parte no puede ser padre e hijo al mismo tiempo"); }
            if (tvEstructura.SelectedNode != null && rowMP != null && Convert.ToInt32(tvEstructura.SelectedNode.Tag) == BLL.CompuestoParteBLL.HijoEsMP) { validacion.Add("Una materia prima no puede ser hijo de una materia prima"); }

            if (validacion.Count == 0)
            {
                Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowComp = EstaAgregada(rowParte, rowMP, tvEstructura.SelectedNode);
                if (rowComp == null)
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
                    }
                    nudCantidadAgregar.Value = 0;
                }
                else
                {
                    string pregunta = "La " + ((rowParte != null) ? "parte" : "materia prima") + " seleccionada ya se encuentra agregada.";
                    pregunta += "\n\n¿Desea sumar la cantidad ingresada?";
                    if (MensajesABM.MsjPreguntaAlUsuario(pregunta, this.Text) == DialogResult.Yes)
                    {
                        rowComp.COMP_CANTIDAD += nudCantidadAgregar.Value;
                        string texto = ((rowComp.IsMP_CODIGONull()) ? rowComp.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_NOMBRE + " - " + rowComp.PARTESRowByFK_COMPUESTOS_PARTES_PARTES_HIJO.PART_CODIGO : rowComp.MATERIAS_PRIMASRow.MP_NOMBRE);
                        texto += " / #" + rowComp.COMP_CANTIDAD.ToString();
                        texto += " " + rowComp.UNIDADES_MEDIDARow.UMED_ABREVIATURA;
                        tvEstructura.Nodes.Find(rowComp.COMP_CODIGO.ToString(), true)[0].Text = texto;
                        nudCantidadAgregar.Value = 0;
                    }
                }
            }
            else
            {
                MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(MensajesABM.Validaciones.CompletarDatos, validacion), this.Text);
            }
        }

        private void AgregarParteADataset(Data.dsEstructuraProducto.PARTESRow rowParte, Data.dsEstructuraProducto.MATERIAS_PRIMASRow rowMP, object padre)
        {
            Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto = dsEstructura.COMPUESTOS_PARTES.NewCOMPUESTOS_PARTESRow();
            rowCompuesto.BeginEdit();
            rowCompuesto.COMP_CODIGO = compId;
            compId--;
            if (padre != null) { rowCompuesto.COMP_CANTIDAD = nudCantidadAgregar.Value; }
            else { rowCompuesto.COMP_CANTIDAD = 1; }
            if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { rowCompuesto.ESTR_CODIGO = -1; }
            else { rowCompuesto.ESTR_CODIGO = Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]); }
            if (padre == null) { rowCompuesto.SetPART_NUMERO_PADRENull(); }
            else { rowCompuesto.PART_NUMERO_PADRE = dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(padre)).PART_NUMERO_HIJO; }
            
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

        private Data.dsEstructuraProducto.COMPUESTOS_PARTESRow EstaAgregada(Data.dsEstructuraProducto.PARTESRow rowParte, Data.dsEstructuraProducto.MATERIAS_PRIMASRow rowMP, TreeNode nodoPadre)
        {
            string filtro = string.Empty;
            if (rowParte != null && nodoPadre != null)
            {
                filtro = "PART_NUMERO_PADRE = " + dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(nodoPadre.Name)).PART_NUMERO_HIJO.ToString();
                filtro += " AND PART_NUMERO_HIJO = " + rowParte.PART_NUMERO.ToString();
            }
            if (rowMP != null && nodoPadre != null)
            {
                filtro = "PART_NUMERO_PADRE = " + dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(nodoPadre.Name)).PART_NUMERO_HIJO.ToString();
                filtro += " AND MP_CODIGO = " + rowMP.MP_CODIGO.ToString();
            }
            if (string.IsNullOrEmpty(filtro)) { return null; }
            else 
            {
                filtro += " AND estr_codigo = " + ((estadoInterface == estadoUI.modificar) ? dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"].ToString() : "-1");
                Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[] partes = (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(filtro);
                if (partes.Length > 0) { return partes[0]; }
                return null;
            }
        }

        private bool EsMismoPadreHijo(Data.dsEstructuraProducto.PARTESRow rowParte, TreeNode selectedNode)
        {
            bool encontrado = false;
            TreeNode nodo = selectedNode;
            while (nodo != null)
            {
                if (rowParte.PART_NUMERO == dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(nodo.Name)).PART_NUMERO_HIJO) { encontrado = true; }
                nodo = nodo.Parent;
            }
            return encontrado;
        }

        private void btnDeleteParte_Click(object sender, EventArgs e)
        {
            if (tvEstructura.SelectedNode != null)
            {
                foreach (TreeNode nodo in tvEstructura.SelectedNode.Nodes)
                {
                    EliminarNodos(nodo);
                }

                dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(tvEstructura.SelectedNode.Name)).Delete();
                tvEstructura.SelectedNode.Remove();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Parte", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void EliminarNodos(TreeNode nodoSelected)
        {
            foreach (TreeNode nodo in nodoSelected.Nodes)
            {
                EliminarNodos(nodo);
            }
            dsEstructura.COMPUESTOS_PARTES.FindByCOMP_CODIGO(Convert.ToInt32(nodoSelected.Name)).Delete();
            nodoSelected.Remove();
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
                    btnClonar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcEstructuraProducto.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SetTexto("Seleccione...");
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione...");
                    dtpFechaAlta.Enabled = true;
                    dtpFechaAlta.SetFechaNull();
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbCocina.SetTexto("Seleccione...");
                    cbResponsable.Enabled = true;
                    cbResponsable.SetSelectedValue(-1);
                    dtpFechaModificacion.Enabled = true;
                    dtpFechaModificacion.SetFechaNull();
                    txtDescripcion.ReadOnly = false;
                    nudcosto.Value = 0;
                    nudcosto.Enabled = true;
                    txtDescripcion.Clear();
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnClonar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbBtnDatosPartes.Enabled = true;
                    tvEstructura.Nodes.Clear();
                    estadoInterface = estadoUI.nuevo;
                    tcEstructuraProducto.SelectedTab = tpDatos;
                    slideControl1.Selected = slideDatos;
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    cbPlano.Enabled = true;
                    cbPlano.SetTexto("Seleccione...");
                    cbEstado.Enabled = true;
                    cbEstado.SetTexto("Seleccione...");
                    dtpFechaAlta.Enabled = true;
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbCocina.SetTexto("Seleccione...");
                    cbResponsable.Enabled = true;
                    cbResponsable.SetSelectedValue(-1);
                    dtpFechaModificacion.Enabled = true;
                    dtpFechaModificacion.SetFechaNull();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    nudcosto.Enabled = true;
                    nudcosto.Value = 0;
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnClonar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbBtnDatosPartes.Enabled = true;
                    tvEstructura.Nodes.Clear();
                    estadoInterface = estadoUI.nuevoExterno;
                    tcEstructuraProducto.SelectedTab = tpDatos;
                    slideControl1.Selected = slideDatos;
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
                    panelAccionesArbol.Enabled = false;
                    gbAgregarParteMP.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnClonar.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    gbBtnDatosPartes.Enabled = false;
                    estadoInterface = estadoUI.consultar;
                    tcEstructuraProducto.SelectedTab = tpDatos;
                    slideControl1.Selected = slideDatos;
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
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnClonar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbBtnDatosPartes.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcEstructuraProducto.SelectedTab = tpDatos;
                    slideControl1.Selected = slideDatos;
                    break;
                case estadoUI.clonar:
                    txtNombre.ReadOnly = false;
                    cbPlano.Enabled = true;
                    cbEstado.Enabled = true;
                    dtpFechaAlta.Enabled = true;                    
                    try
                    {
                        dtpFechaAlta.SetFecha(BLL.DBBLL.GetFechaServidor());
                    }
                    catch (Exception) { dtpFechaAlta.Value = DateTime.Today; }
                    cbCocina.Enabled = true;
                    cbResponsable.Enabled = true;
                    dtpFechaModificacion.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    nudcosto.Enabled = true;
                    panelAccionesArbol.Enabled = true;
                    gbAgregarParteMP.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnClonar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    gbBtnDatosPartes.Enabled = true;
                    estadoInterface = estadoUI.clonar;
                    tcEstructuraProducto.SelectedTab = tpDatos;
                    slideControl1.Selected = slideDatos;
                    break;
                default:
                    break;
            }
        }        

        private void SetGrillasCombosVistas()
        {
            //Obtenemos los datos iniciales necesarios
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
            cboActivoBuscar.SetDatos(nombres, valores, "--TODOS--", true);            

            #endregion Buscar

            #region Datos
            
            //Dataviews
            dvCocina = new DataView(dsCocina.COCINAS);
            dvCocina.Sort = "COC_CODIGO_PRODUCTO ASC";
            dvResponsable = new DataView(dsEmpleado.EMPLEADOS);
            dvResponsable.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";
            dvPlano = new DataView(dsEstructura.PLANOS);
            dvPlano.Sort = "PNO_NOMBRE ASC";

            //ComboBoxs
            cbCocina.SetDatos(dvCocina, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione...", false);
            cbResponsable.SetDatos(dvResponsable, "E_CODIGO", displaymember, ", ", "--Sin especificar--", true);
            cbPlano.SetDatos(dvPlano, "PNO_CODIGO", "PNO_NOMBRE", "Seleccione...", false);
            cbEstado.SetDatos(nombres, valores, "Seleccione...", false);

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
            cboFiltroTipoParte.SetDatos(dvFiltroTipo, "TPAR_CODIGO", "TPAR_NOMBRE", "--TODOS--", true);
            #endregion

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
            else { cbResponsable.SetSelectedValue(-1); }
            if (!dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).IsESTR_FECHA_MODIFICACIONNull())
            {
                dtpFechaModificacion.SetFecha(dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_FECHA_MODIFICACION);
            }
            else { dtpFechaModificacion.SetFechaNull(); }
            txtDescripcion.Text = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_DESCRIPCION;
            nudcosto.Value = dsEstructura.ESTRUCTURAS.FindByESTR_CODIGO(codEstructura).ESTR_COSTO;
            tvEstructura.BeginUpdate();
            if (dsEstructura.COMPUESTOS_PARTES.Select("estr_codigo = " + codEstructura).Length > 0) 
            { 
                BLL.EstructuraBLL.CrearArbolEstructura(codEstructura, dsEstructura, tvEstructura, false, out compId); 
            }
            else { tvEstructura.Nodes.Clear(); }
            tvEstructura.EndUpdate();
        }        

        #endregion

        #region Efecto botones y control enter

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

        #endregion

        #region Filtros grillas

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

        #region Menú bloquear columnas

        private void tsmiBloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvPartesDisponibles.Columns[columnIndex].Frozen = true; }
        }

        private void tsmiDesbloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvPartesDisponibles.Columns[columnIndex].Frozen = false; }
        }

        private void dgvPartesDisponibles_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex > -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    columnIndex = e.ColumnIndex;
                    if (dgvPartesDisponibles.Columns[columnIndex].Frozen)
                    {
                        tsmiBloquearColumna.Checked = true;
                        tsmiDesbloquearColumna.Checked = false;
                    }
                    else
                    {
                        tsmiBloquearColumna.Checked = false;
                        tsmiDesbloquearColumna.Checked = true;
                    }
                    cmsGrillaPartesDisponibles.Show(MousePosition);
                }
            }
        }

        #endregion

        #region Slide

        private void SetSlide()
        {
            slideControl1.AddSlide(slidePartes);
            slideControl1.AddSlide(slideDatos);
            gbAgregarParteMP.Parent = slidePartes;
            gbDatos.Parent = slideDatos;
            slideControl1.Selected = slideDatos;
        }        

        private void btnDatos_Click(object sender, EventArgs e)
        {
            if (slideControl1.Selected == slidePartes)
            {
                slideControl1.BackwardTo("slideDatos");
                btnDatos.Image = Properties.Resources.arriba1_15;
                btnPartes.Image = Properties.Resources.derecha1_15;
            }
        }

        private void btnPartes_Click(object sender, EventArgs e)
        {
            if (slideControl1.Selected == slideDatos)
            {
                slideControl1.ForwardTo("slidePartes");
                btnDatos.Image = Properties.Resources.izquierda1_15;
                btnPartes.Image = Properties.Resources.arriba1_15;
            }
        }

        #endregion

        

    }
}

