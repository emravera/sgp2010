using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.BLL;

namespace GyCAP.UI.Mantenimiento
{
    public partial class frmPlanMantenimiento : Form
    {
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private static frmPlanMantenimiento _frmPlanMantenimiento = null;
        //private Data.dsEstadoPedidos dsEstadoPedido = new GyCAP.Data.dsEstadoPedidos();
        private Data.dsPlanMantenimiento dsPlanMantenimiento = new GyCAP.Data.dsPlanMantenimiento();
        private DataView dvPlanMantenimiento, dvDetallePlanMantenimiento, dvMantenimientos, dvEstadoPlanMantenimiento;
        private DataView dvEstadoDetallePlanMantenimineto, dvUnidadMedida, dvEstadoPlanMantenimientoBuscar;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;

        public frmPlanMantenimiento()
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
        public static frmPlanMantenimiento Instancia
        {
            get
            {
                if (_frmPlanMantenimiento == null || _frmPlanMantenimiento.IsDisposed)
                {
                    _frmPlanMantenimiento = new frmPlanMantenimiento();
                }
                else
                {
                    _frmPlanMantenimiento.BringToFront();
                }
                return _frmPlanMantenimiento;
            }
            set
            {
                _frmPlanMantenimiento = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos = true;

                    if (dsPlanMantenimiento.PLANES_MANTENIMIENTO.Rows.Count == 0)
                    {
                        hayDatos = false;
                        btnBuscar.Focus();
                    }
                    else
                    {
                        hayDatos = true;
                        dgvLista.Focus();
                    }

                    limpiarControles(false);
                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.inicio;
                    tcPlan.SelectedTab = tpBuscar;
                    //txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    setControles(false);
                    limpiarControles(true);
                    txtNumero.Text = "No asignado...";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    dvDetallePlanMantenimiento.RowFilter = "DPMAN_CODIGO < 0";
                    tcPlan.SelectedTab = tpDatos;
                    txtDescripcion.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    setControles(false);
                    limpiarControles(true);
                    txtNumero.Text = "No asignado...";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    dvDetallePlanMantenimiento.RowFilter = "DPED_CODIGO < 0";
                    tcPlan.SelectedTab = tpDatos;
                    txtDescripcion.Focus();
                    break;
                case estadoUI.consultar:
                    setControles(true);
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcPlan.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    setControles(false);
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcPlan.SelectedTab = tpDatos;
                    txtDescripcion.Focus();
                    break;
                default:
                    break;
            }
        }

        private void setControles(bool pValue)
        {
            txtNumero.ReadOnly = true;
            txtDescripcion.ReadOnly = pValue;
            txtObservacion.ReadOnly = pValue;
            cboEstado.Enabled = pValue;
        }

        private void limpiarControles(bool pValue)
        {
            txtNumero.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtObservacion.Text = string.Empty;
            cboEstado.SelectedIndex = -1;

            if (pValue == true)
            {
                dgvDetallePlan.Refresh();
                cboEstado.SetSelectedValue(1); //Esto tiene que ser un parametro no puede quedar hardcodiado
                cboEstado.Enabled = false;
            }

        }

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbMantenimientos.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            dgvDetallePlan.AutoGenerateColumns = false;
            dgvMantenimientos.AutoGenerateColumns = false;

            //Agregamos las columnas y sus propiedades
            dgvLista.Columns.Add("PMAN_NUMERO", "Código");
            dgvLista.Columns.Add("PMAN_DESCRIPCION", "Descripción");
            dgvLista.Columns.Add("PMAN_FECHA", "Fecha");
            dgvLista.Columns.Add("EPMAN_CODIGO", "Estado");
            dgvLista.Columns.Add("PMAN_OBSERVACIONES", "Observaciones");
            dgvLista.Columns["PMAN_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PMAN_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["PMAN_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PMAN_FECHA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PMAN_FECHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["PMAN_FECHA"].MinimumWidth = 110;
            dgvLista.Columns["EPMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PMAN_OBSERVACIONES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["PMAN_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvLista.Columns["PMAN_NUMERO"].Visible = false;

            //dgvDetallePlan.Columns.Add("DPED_CODIGO", "Codigo");
            dgvDetallePlan.Columns.Add("DPMAN_DESCRIPCION", "Descripción");
            dgvDetallePlan.Columns.Add("MAN_CODIGO", "Mantenimiento");
            dgvDetallePlan.Columns.Add("DPMAN_FRECUENCIA", "Frecuencia");
            dgvDetallePlan.Columns.Add("UMED_CODIGO", "Un.Med.");
            dgvDetallePlan.Columns.Add("EDMAN_CODIGO", "Estado");

            //dgvDetallePlan.Columns["DPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPMAN_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["MAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPMAN_FRECUENCIA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPMAN_FRECUENCIA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetallePlan.Columns["EDMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
            //Alineacion de los numeros y las fechas en la grilla
            //dgvDetallePlan.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDetallePlan.Columns["DPED_CODIGO"].Visible = false;

            
            dgvMantenimientos.Columns.Add("MAN_CODIGO", "Código");
            dgvMantenimientos.Columns.Add("TMAN_CODIGO", "Tipo");
            dgvMantenimientos.Columns.Add("MAN_DESCRIPCION", "Descripción");
            dgvMantenimientos.Columns.Add("CEMP_CODIGO", "Encargado");
            //dgvCocinas.Columns.Add("COC_ESTADO", "Estado");
            dgvMantenimientos.Columns.Add("MAN_REQUIERE_PARAR_PLANTA", "Requiere parar Planta");
            dgvMantenimientos.Columns.Add("MAN_OBSERVACION", "Observaciones");

            dgvMantenimientos.Columns["MAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMantenimientos.Columns["TMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMantenimientos.Columns["MAN_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMantenimientos.Columns["CEMP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMantenimientos.Columns["MAN_REQUIERE_PARAR_PLANTA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMantenimientos.Columns["MAN_OBSERVACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMantenimientos.Columns["MAN_OBSERVACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMantenimientos.Columns["MAN_CODIGO"].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvLista.Columns["PMAN_NUMERO"].DataPropertyName = "PMAN_NUMERO";
            dgvLista.Columns["PMAN_DESCRIPCION"].DataPropertyName = "PMAN_DESCRIPCION";
            dgvLista.Columns["PMAN_FECHA"].DataPropertyName = "PMAN_FECHA";
            dgvLista.Columns["EPMAN_CODIGO"].DataPropertyName = "EPMAN_CODIGO";
            dgvLista.Columns["PMAN_OBSERVACIONES"].DataPropertyName = "PMAN_OBSERVACIONES";

            //dgvDetallePlan.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePlan.Columns["DPMAN_DESCRIPCION"].DataPropertyName = "DPMAN_DESCRIPCION";
            dgvDetallePlan.Columns["MAN_CODIGO"].DataPropertyName = "MAN_CODIGO";
            dgvDetallePlan.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvDetallePlan.Columns["DPMAN_FRECUENCIA"].DataPropertyName = "DPMAN_FRECUENCIA";
            dgvDetallePlan.Columns["EDMAN_CODIGO"].DataPropertyName = "EDMAN_CODIGO";

            dgvMantenimientos.Columns["MAN_CODIGO"].DataPropertyName = "MAN_CODIGO";
            dgvMantenimientos.Columns["TMAN_CODIGO"].DataPropertyName = "TMAN_CODIGO";
            dgvMantenimientos.Columns["MAN_DESCRIPCION"].DataPropertyName = "MAN_DESCRIPCION";
            dgvMantenimientos.Columns["CEMP_CODIGO"].DataPropertyName = "CEMP_CODIGO";
            dgvMantenimientos.Columns["MAN_REQUIERE_PARAR_PLANTA"].DataPropertyName = "MAN_REQUIERE_PARAR_PLANTA";    
            //dgvCocinas.Columns["COC_ESTADO"].DataPropertyName = "COC_ESTADO";
            dgvMantenimientos.Columns["MAN_OBSERVACION"].DataPropertyName = "MAN_OBSERVACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvPlanMantenimiento = new DataView(dsPlanMantenimiento.PLANES_MANTENIMIENTO);
            dvPlanMantenimiento.Sort = "PMAN_NUMERO ASC";
            dgvLista.DataSource = dvPlanMantenimiento;

            dvDetallePlanMantenimiento = new DataView(dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO);
            dgvDetallePlan.DataSource = dvDetallePlanMantenimiento;

            dvMantenimientos = new DataView(dsPlanMantenimiento.MANTENIMIENTOS );
            dvMantenimientos.Sort = "MAN_CODIGO ASC";
            dgvMantenimientos.DataSource = dvMantenimientos;

            dvEstadoPlanMantenimiento = new DataView(dsPlanMantenimiento.ESTADO_PLANES_MANTENIMIENTO);
            dvEstadoPlanMantenimiento.Sort = "EPMAN_NOMBRE";

            dvEstadoPlanMantenimientoBuscar = new DataView(dsPlanMantenimiento.ESTADO_PLANES_MANTENIMIENTO);
            dvEstadoPlanMantenimientoBuscar.Sort = "EPMAN_NOMBRE";

            dvEstadoDetallePlanMantenimineto = new DataView(dsPlanMantenimiento.ESTADO_DETALLE_MANTENIMIENTOS);
            dvEstadoDetallePlanMantenimineto.Sort = "EDMAN_NOMBRE";

            dvUnidadMedida = new DataView(dsPlanMantenimiento.UNIDADES_MEDIDA);
            dvUnidadMedida.Sort = "UMED_NOMBRE";

            //Obtenemos las terminaciones, los planos, los estados de las piezas, las MP, unidades medidas, hojas ruta
            try
            {
                BLL.EstadoPlanMantenimientoBLL.ObtenerTodos(dsPlanMantenimiento.ESTADO_PLANES_MANTENIMIENTO);
                BLL.EstadoDetalleMantenimientoBLL.ObtenerTodos(dsPlanMantenimiento.ESTADO_DETALLE_MANTENIMIENTOS);
                BLL.MantenimientoBLL.ObtenerMantenimientos(dsPlanMantenimiento.MANTENIMIENTOS);
                //BLL.ClienteBLL.ObtenerTodos(dsPlanMantenimiento.CLIENTES);
                BLL.TipoMantenimientoBLL.ObtenerTodos(dsPlanMantenimiento.TIPOS_MANTENIMIENTOS);
                BLL.CapacidadEmpleadoBLL.ObtenerTodos(dsPlanMantenimiento.CAPACIDAD_EMPLEADOS);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsPlanMantenimiento.UNIDADES_MEDIDA);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dvEstadoPlanMantenimientoBuscar = new DataView(dsPlanMantenimiento.ESTADO_PLANES_MANTENIMIENTO);

            cboEstadoBuscar.SetDatos(dvEstadoPlanMantenimientoBuscar, "EPMAN_CODIGO", "EPMAN_NOMBRE", "--TODOS--", true);
            cboEstado.SetDatos(dvEstadoPlanMantenimiento, "EPMAN_CODIGO", "EPMAN_NOMBRE", "", false);
            cboUnidadMedida.SetDatos(dvUnidadMedida, "UMED_CODIGO", "UMED_ABREVIATURA", "Seleccione...", false);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsPlanMantenimiento.PLANES_MANTENIMIENTO.Clear();
                dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Clear();

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.PlanMantenimientoBLL.ObtenerPlanMantenimiento(txtNombreBuscar.Text, txtNroPedidoBuscar.Text, cboEstadoBuscar.GetSelectedValueInt(), dsPlanMantenimiento, true);

                if (dsPlanMantenimiento.PLANES_MANTENIMIENTO.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Planes de mantenimiento con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvPlanMantenimiento.Table = dsPlanMantenimiento.PLANES_MANTENIMIENTO;
                dvDetallePlanMantenimiento.Table = dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO;
                dvMantenimientos.Table = dsPlanMantenimiento.MANTENIMIENTOS;

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Pedido - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            long codigo = Convert.ToInt64(dvPlanMantenimiento[e.RowIndex]["PMAN_NUMERO"]);
            txtNumero.Text = dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigo).PMAN_NUMERO.ToString();            
            cboEstado.SetSelectedValue(Convert.ToInt32(dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigo).EPMAN_CODIGO));
            txtObservacion.Text = dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigo).PMAN_OBSERVACIONES;

            //Usemos el filtro del dataview para mostrar sólo las Detalles del Pedido seleccionado
            dvDetallePlanMantenimiento.RowFilter = "PMAN_NUMERO = " + codigo;

        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "EPMAN_CODIGO":
                        nombre = dsPlanMantenimiento.ESTADO_PLANES_MANTENIMIENTO.FindByEPMAN_CODIGO(Convert.ToInt64(e.Value.ToString())).EPMAN_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "PMAN_FECHA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsPlanMantenimiento.PLANES_MANTENIMIENTO.RejectChanges();
            dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.RejectChanges();
            SetInterface(estadoUI.inicio);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetallePlan.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                long codigoDetalle = Convert.ToInt64(dvDetallePlanMantenimiento[dgvDetallePlan.SelectedRows[0].Index]["DPMAN_CODIGO"]);
                //Lo borramos pero sólo del dataset                
                dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.FindByDPMAN_CODIGO(codigoDetalle).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Plan de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            //if (dgvDetallePlan.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            //{
            //    //Obtenemos el código
            //    int codigoDetalle = Convert.ToInt32(dvDetallePlanMantenimiento[dgvDetallePlan.SelectedRows[0].Index]["dped_codigo"]);
            //    //Aumentamos la cantidad                
            //    dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.FindByDPED_CODIGO(codigoDetalle).DPMAN_FRECUENCIA += 1;
            //    dgvDetallePlan.Refresh();
            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar una Cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            //if (dgvDetallePlan.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            //{
            //    //Obtenemos el código
            //    long codigoDetalle = Convert.ToInt64(dvDetallePlanMantenimiento[dgvDetallePlan.SelectedRows[0].Index]["dped_codigo"]);
            //    //Disminuimos la cantidad
            //    if (dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.FindByDPED_CODIGO(codigoDetalle).DPMAN_FRECUENCIA > 1)
            //    {
            //        dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.FindByDPED_CODIGO(codigoDetalle).DPMAN_FRECUENCIA -= 1;
            //    }
            //    dgvDetallePlan.Refresh();
            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar una Cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void nudCantidad_Enter(object sender, EventArgs e)
        {
            //if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            //if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCantidad.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void btnHecho_Click(object sender, EventArgs e)
        {
            slideControl.BackwardTo("slideDatos");
            nudCantidad.Value = 0;
            panelAcciones.Enabled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvMantenimientos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidad.Value > 0 && cboUnidadMedida.SelectedIndex != -1)
            {
                bool agregarMantenimiento; //variable que indica si se debe agregar la cocina al listado
                //Obtenemos el código de la pieza según sea nueva o modificada, lo hacemos acá porque lo vamos a usar mucho
                int planCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { planCodigo = -1; }
                else { planCodigo = Convert.ToInt32(dvPlanMantenimiento[dgvLista.SelectedRows[0].Index]["PMAN_NUMERO"]); }
                //Obtenemos el código del mantenimiento, también lo vamos a usar mucho
                int mantenimientoCodigo = Convert.ToInt32(dvMantenimientos[dgvMantenimientos.SelectedRows[0].Index]["MAN_CODIGO"]);

                int unidadMedida = cboUnidadMedida.GetSelectedValueInt();

                //Primero vemos si el pedido tiene algúna cocina cargada, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvDetallePlanMantenimiento.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar la misma cocina haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "PMAN_NUMERO = " + planCodigo + " AND MAN_CODIGO = " + mantenimientoCodigo;
                    Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow[] rows =
                        (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow[])dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("El Plan ya posee el mantenimiento seleccionado. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].DPMAN_FRECUENCIA += int.Parse(nudCantidad.Value.ToString());
                            nudCantidad.Value = 0;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarMantenimiento = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarMantenimiento = true;
                    }
                }
                else
                {
                    //No tiene ningúna materia prima agregada, marcamos que debe agregarse
                    agregarMantenimiento = true;
                }

                //Ahora comprobamos si debe agregarse el mantenimiento o no
                if (agregarMantenimiento)
                {
                    Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow row = dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.NewDETALLE_PLANES_MANTENIMIENTORow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.DPMAN_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.PMAN_NUMERO = planCodigo;
                    row.EDMAN_CODIGO = 1; //ACTIVO - Esto tiene que ser un parametro
                    row.MAN_CODIGO = mantenimientoCodigo;
                    row.UMED_CODIGO = unidadMedida;
                    row.DPMAN_DESCRIPCION = txtDescripcionMantenimiento.Text.Trim();
                    row.DPMAN_FRECUENCIA = nudCantidad.Value.ToString();
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.AddDETALLE_PLANES_MANTENIMIENTORow(row);
                    nudCantidad.Value = 0;
                }
                nudCantidad.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Mantenimiento de la lista, asignarle una unidad de medida y una frecuencia mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Datos opcionales = descripcion
            //Revisamos que completó los datos obligatorios
            string datosFaltantes = string.Empty;
            //if (txtNumero.Text == string.Empty) { datosFaltantes += "* Numero\n"; }
            if (cboEstado.GetSelectedIndex() == -1) { datosFaltantes += "* Estado\n"; }
            if (dgvDetallePlan.Rows.Count == 0) { datosFaltantes += "* El detalle del Pedido\n"; }
            if (datosFaltantes == string.Empty)
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
                        Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow rowPlan = dsPlanMantenimiento.PLANES_MANTENIMIENTO.NewPLANES_MANTENIMIENTORow();
                        rowPlan.BeginEdit();
                        rowPlan.PMAN_NUMERO = -1;
                        rowPlan.PMAN_FECHA = DBBLL.GetFechaServidor();
                        rowPlan.PMAN_DESCRIPCION = txtDescripcion.Text.Trim();
                        rowPlan.PMAN_OBSERVACIONES = txtObservacion.Text.Trim();
                        rowPlan.EPMAN_CODIGO = cboEstado.GetSelectedValueInt();
                        
                        rowPlan.EndEdit();

                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.AddPLANES_MANTENIMIENTORow(rowPlan);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad PiezaBLL y PiezaDAL sepan cuales insertar
                        BLL.PlanMantenimientoBLL.Insertar(dsPlanMantenimiento);

                        //Ahora si aceptamos los cambios
                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.AcceptChanges();
                        dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz

                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            dgvLista.Refresh();

                            //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                            SetInterface(estadoUI.inicio);
                        }
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        //Ya existe la pieza, descartamos los cambios pero sólo de piezas ya que puede querer
                        //modificar el nombre y/o la terminación e intentar de nuevo con la estructura cargada
                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    long codigoPlan = Convert.ToInt64(dvPlanMantenimiento[dgvLista.SelectedRows[0].Index]["PMAN_NUMERO"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, el detalle se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigoPlan).PMAN_NUMERO = long.Parse(txtNumero.Text.ToString());
                    dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigoPlan).PMAN_DESCRIPCION = txtDescripcion.Text; 
                    dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigoPlan).EPMAN_CODIGO = cboEstado.GetSelectedValueInt();
                    dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigoPlan).PMAN_OBSERVACIONES = txtObservacion.Text;
                    //dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigoPlan).PMAN_FECHA = DateTime.Parse(sfFechaPrevista.GetFecha().ToString());

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.PlanMantenimientoBLL.Actualizar(dsPlanMantenimiento);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.AcceptChanges();
                        dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.ErrorInesperadoException ex)
                    {
                        //Hubo problemas no esperados, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsPlanMantenimiento.PLANES_MANTENIMIENTO.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvLista.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int estado = Convert.ToInt32(dvPlanMantenimiento[dgvLista.SelectedRows[0].Index]["EPMAN_CODIGO"]);
                if (estado != 1) //Si no esta pendiente no lo puede eliminar PARAMETRIZAR
                {
                    //Preguntamos si está seguro
                    DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el Plan de Mantenimiento seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            //Obtenemos el codigo
                            long codigo = Convert.ToInt64(dvPlanMantenimiento[dgvLista.SelectedRows[0].Index]["PMAN_CODIGO"]);
                            //Lo eliminamos de la DB
                            BLL.PlanMantenimientoBLL.Eliminar(codigo);
                            //Lo eliminamos de la tabla conjuntos del dataset
                            dsPlanMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigo).Delete();
                            dsPlanMantenimiento.PLANES_MANTENIMIENTO.AcceptChanges();
                        }
                        catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                        {
                            MessageBox.Show(ex.Message, "Error: Pieza - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
                            MessageBox.Show(ex.Message, "Error: Pieza - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Plan de Mantenimiento de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
  
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

        private void dgvMantenimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvMantenimientos.Columns[e.ColumnIndex].Name)
                {
                    case "TMAN_CODIGO":
                        nombre = dsPlanMantenimiento.TIPOS_MANTENIMIENTOS.FindByTMAN_CODIGO(Convert.ToInt32(e.Value.ToString())).TMAN_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CEMP_CODIGO":
                        nombre = dsPlanMantenimiento.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(Convert.ToInt32(e.Value.ToString())).CEMP_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MAN_REQUIERE_PARAR_PLANTA":
                        nombre = "No";
                        if (e.Value.ToString() == "S")
                            nombre = "Si";
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvMantenimientos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            long codigo = Convert.ToInt64(dvMantenimientos[e.RowIndex]["MAN_CODIGO"]);
            txtDescripcionMantenimiento.Text = dsPlanMantenimiento.MANTENIMIENTOS.FindByMAN_CODIGO(codigo).MAN_DESCRIPCION.ToString();
        }

        private void dgvDetallePlan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvDetallePlan.Columns[e.ColumnIndex].Name)
                {
                    case "UMED_CODIGO":
                        nombre = dsPlanMantenimiento.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value.ToString())).UMED_ABREVIATURA;
                        e.Value = nombre;
                        break;
                    case "MAN_CODIGO":
                        nombre = dsPlanMantenimiento.MANTENIMIENTOS.FindByMAN_CODIGO(Convert.ToInt32(e.Value.ToString())).MAN_DESCRIPCION;
                        e.Value = nombre;
                        break;
                    case "EDMAN_CODIGO":
                        nombre = dsPlanMantenimiento.ESTADO_DETALLE_MANTENIMIENTOS.FindByEDMAN_CODIGO(Convert.ToInt32(e.Value.ToString())).EDMAN_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
