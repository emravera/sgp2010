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
    public partial class frmRegistrarMantenimiento : Form
    {
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private static frmRegistrarMantenimiento _frmRegistrarMantenimiento = null;        
        private Data.dsRegistrarMantenimiento dsRegistrarMantenimiento = new GyCAP.Data.dsRegistrarMantenimiento();
        private DataView dvRegistrarMantenimiento, dvDetalleRepuestos, dvRepuestos, dvPlanMantenimiento, dvDetallePlanMantenimiento;
        private DataView dvMantenimiento, dvTipoMantenimientoBuscar, dvEmpleadoBuscar, dvMaquinaBuscar, dvMaquina;
        private DataView dvTipoMantenimiento, dvEmpleado;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;

        public frmRegistrarMantenimiento()
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
        public static frmRegistrarMantenimiento Instancia
        {
            get
            {
                if (_frmRegistrarMantenimiento == null || _frmRegistrarMantenimiento.IsDisposed)
                {
                    _frmRegistrarMantenimiento = new frmRegistrarMantenimiento();
                }
                else
                {
                    _frmRegistrarMantenimiento.BringToFront();
                }
                return _frmRegistrarMantenimiento;
            }
            set
            {
                _frmRegistrarMantenimiento = value;
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

                    if (dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.Rows.Count == 0)
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
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    dvDetalleRepuestos.RowFilter = "RRMAN_CODIGO < 0";
                    tcPlan.SelectedTab = tpDatos;
                    cboTipoMantenimiento.SetSelectedValue(1); //HARDCODE 
                    setearTipoMantenimiento();
                    cboTipoMantenimiento.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    setControles(false);
                    limpiarControles(true);
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    dvDetalleRepuestos.RowFilter = "RRMAN_CODIGO < 0";
                    tcPlan.SelectedTab = tpDatos;
                    cboTipoMantenimiento.SetSelectedValue(1); //HARDCODE 
                    setearTipoMantenimiento();
                    cboTipoMantenimiento.Focus();
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
                    cboTipoMantenimiento.Focus();
                    break;
                default:
                    break;
            }
        }

        private void setControles(bool pValue)
        {
            cboTipoMantenimiento.Enabled = !pValue;
            txtObservacion.ReadOnly = pValue;
            cboEmpleado.Enabled = !pValue;
            sfFechaRealizacion.Enabled = !pValue;

        }

        private void limpiarControles(bool pValue)
        {
            txtObservacion.Text = string.Empty;
            cboEmpleado.SelectedIndex = -1;
            sfFechaRealizacion.SetFechaNull();
            

            if (pValue == true)
            {
                //cboEmpleado.SetSelectedValue(1); //Esto tiene que ser un parametro no puede quedar hardcodiado
                //cboEmpleado.Enabled = false;
            }

        }

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbRepuestos.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            dgvRepuestos.AutoGenerateColumns = false;
            dgvDetalle.AutoGenerateColumns = false;
            dgvDetallePlan.AutoGenerateColumns = false;

            //Agregamos las columnas y sus propiedades
            dgvLista.Columns.Add("RMAN_CODIGO", "Código");
            dgvLista.Columns.Add("TMAN_CODIGO", "Tipo");
            dgvLista.Columns.Add("RMAN_FECHA_REALIZACION", "Fecha");
            dgvLista.Columns.Add("MAN_CODIGO", "Mantenimiento");
            dgvLista.Columns.Add("E_CODIGO", "Empleado");
            dgvLista.Columns.Add("PMAN_OBSERVACIONES", "Observaciones");
            dgvLista.Columns["RMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["RMAN_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["TMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["RMAN_FECHA_REALIZACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["RMAN_FECHA_REALIZACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvLista.Columns["RMAN_FECHA_REALIZACION"].MinimumWidth = 110;
            dgvLista.Columns["E_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PMAN_OBSERVACIONES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["RMAN_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvLista.Columns["RMAN_CODIGO"].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvLista.Columns["RMAN_CODIGO"].DataPropertyName = "RMAN_CODIGO";
            dgvLista.Columns["TMAN_CODIGO"].DataPropertyName = "TMAN_CODIGO";
            dgvLista.Columns["RMAN_FECHA_REALIZACION"].DataPropertyName = "RMAN_FECHA_REALIZACION";
            dgvLista.Columns["MAN_CODIGO"].DataPropertyName = "MAN_CODIGO";
            dgvLista.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvLista.Columns["PMAN_OBSERVACIONES"].DataPropertyName = "PMAN_OBSERVACIONES";


            dgvDetalle.Columns.Add("RRMAN_CODIGO", "Código");
            dgvDetalle.Columns.Add("REP_CODIGO", "Repuesto");
            dgvDetalle.Columns.Add("RRMAN_CANTIDAD", "Cantidad");
            dgvDetalle.Columns.Add("Costo", "Costo Total");
            dgvDetalle.Columns["RRMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns["RRMAN_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns["REP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalle.Columns["Costo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns["RRMAN_CODIGO"].DataPropertyName = "RRMAN_CODIGO";
            dgvDetalle.Columns["RRMAN_CANTIDAD"].DataPropertyName = "RRMAN_CANTIDAD";
            dgvDetalle.Columns["REP_CODIGO"].DataPropertyName = "REP_CODIGO";
            dgvDetalle.Columns["Costo"].DataPropertyName = "REP_CODIGO";
            dgvDetalle.Columns["RRMAN_CODIGO"].Visible = false;
            
            //dgvRepuestos.Columns.Add("REP_CODIGO", "Código");
            dgvRepuestos.Columns.Add("TREP_CODIGO", "Tipo");
            dgvRepuestos.Columns.Add("REP_NOMBRE", "Descripción");
            dgvRepuestos.Columns.Add("REP_CANTIDADSTOCK", "Stock");
            dgvRepuestos.Columns.Add("REP_COSTO", "Costo");
            //dgvRepuestos.Columns["REP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRepuestos.Columns["TREP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRepuestos.Columns["REP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRepuestos.Columns["REP_CANTIDADSTOCK"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvRepuestos.Columns["REP_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvRepuestos.Columns["REP_CODIGO"].DataPropertyName = "REP_CODIGO";
            dgvRepuestos.Columns["TREP_CODIGO"].DataPropertyName = "TREP_CODIGO";
            dgvRepuestos.Columns["REP_NOMBRE"].DataPropertyName = "REP_NOMBRE";
            dgvRepuestos.Columns["REP_CANTIDADSTOCK"].DataPropertyName = "REP_CANTIDADSTOCK";
            dgvRepuestos.Columns["REP_COSTO"].DataPropertyName = "REP_COSTO";
            //dgvRepuestos.Columns["REP_CODIGO"].Visible = false;


            dgvDetallePlan.Columns.Add("DPMAN_DESCRIPCION", "Descripción");
            dgvDetallePlan.Columns.Add("MAN_CODIGO", "Mantenimiento");
            dgvDetallePlan.Columns.Add("DPMAN_FRECUENCIA", "Frecuencia");
            dgvDetallePlan.Columns.Add("UMED_CODIGO", "Un.Med.");
            dgvDetallePlan.Columns.Add("EDMAN_CODIGO", "Estado");
            dgvDetallePlan.Columns["DPMAN_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetallePlan.Columns["MAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPMAN_FRECUENCIA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPMAN_FRECUENCIA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetallePlan.Columns["EDMAN_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetallePlan.Columns["DPMAN_DESCRIPCION"].DataPropertyName = "DPMAN_DESCRIPCION";
            dgvDetallePlan.Columns["MAN_CODIGO"].DataPropertyName = "MAN_CODIGO";
            dgvDetallePlan.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvDetallePlan.Columns["DPMAN_FRECUENCIA"].DataPropertyName = "DPMAN_FRECUENCIA";
            dgvDetallePlan.Columns["EDMAN_CODIGO"].DataPropertyName = "EDMAN_CODIGO";
            dgvDetallePlan.Columns["MAN_CODIGO"].Visible = false;
            dgvDetallePlan.Columns["EDMAN_CODIGO"].Visible = false;


            //Creamos el dataview y lo asignamos a la grilla
            dvRegistrarMantenimiento = new DataView(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS);
            dvRegistrarMantenimiento.Sort = "RMAN_CODIGO ASC";
            dgvLista.DataSource = dvRegistrarMantenimiento;

            dvDetalleRepuestos = new DataView(dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO);
            dgvDetalle.DataSource = dvDetalleRepuestos; 

            dvRepuestos = new DataView(dsRegistrarMantenimiento.REPUESTOS);
            dvRepuestos.Sort = "REP_CODIGO ASC";
            dgvRepuestos.DataSource = dvRepuestos;

            dvEmpleadoBuscar = new DataView(dsRegistrarMantenimiento.EMPLEADOS);
            dvEmpleadoBuscar.Sort = "E_APELLIDO, E_NOMBRE";

            dvEmpleado = new DataView(dsRegistrarMantenimiento.EMPLEADOS);
            dvEmpleado.Sort = "E_APELLIDO, E_NOMBRE";

            dvMaquinaBuscar = new DataView(dsRegistrarMantenimiento.MAQUINAS);
            dvMaquinaBuscar.Sort = "MAQ_NOMBRE";

            dvMaquina = new DataView(dsRegistrarMantenimiento.MAQUINAS);
            dvMaquina.Sort = "MAQ_NOMBRE";

            dvMantenimiento = new DataView(dsRegistrarMantenimiento.MANTENIMIENTOS);
            dvMantenimiento.Sort = "MAN_DESCRIPCION";

            dvTipoMantenimientoBuscar = new DataView(dsRegistrarMantenimiento.TIPOS_MANTENIMIENTOS);
            dvTipoMantenimientoBuscar.Sort = "TMAN_NOMBRE";

            dvTipoMantenimiento = new DataView(dsRegistrarMantenimiento.TIPOS_MANTENIMIENTOS);
            dvTipoMantenimiento.Sort = "TMAN_NOMBRE";

            dvPlanMantenimiento = new DataView(dsRegistrarMantenimiento.PLANES_MANTENIMIENTO);
            dvPlanMantenimiento.Sort = "PMAN_DESCRIPCION";

            dvDetallePlanMantenimiento = new DataView(dsRegistrarMantenimiento.DETALLE_PLANES_MANTENIMIENTO);
            dgvDetallePlan.DataSource = dvDetallePlanMantenimiento;

            //Obtenemos las terminaciones, los planos, los estados de las piezas, las MP, unidades medidas, hojas ruta
            try
            {
                BLL.UnidadMedidaBLL.ObtenerTodos(dsRegistrarMantenimiento.UNIDADES_MEDIDA);
                BLL.EmpleadoBLL.ObtenerEmpleados(dsRegistrarMantenimiento.EMPLEADOS);
                BLL.MaquinaBLL.ObtenerMaquinas(dsRegistrarMantenimiento.MAQUINAS);
                BLL.MantenimientoBLL.ObtenerMantenimientos(dsRegistrarMantenimiento.MANTENIMIENTOS);
                BLL.TipoMantenimientoBLL.ObtenerTodos(dsRegistrarMantenimiento.TIPOS_MANTENIMIENTOS);
                BLL.PlanMantenimientoBLL.ObtenerPlanMantenimiento(dsRegistrarMantenimiento.PLANES_MANTENIMIENTO);
                BLL.RepuestoBLL.ObtenerTodos(dsRegistrarMantenimiento.REPUESTOS);
                BLL.TipoRepuestoBLL.ObtenerTodos(dsRegistrarMantenimiento.TIPOS_REPUESTOS);
                
                //BLL.CapacidadEmpleadoBLL.ObtenerTodos(dsRegistrarMantenimiento.CAPACIDAD_EMPLEADOS);
                

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //dvEmpleadoBuscar = new DataView(dsRegistrarMantenimiento.EMPLEADOS);

            //cboEmpleadoBuscar.SetDatos(dvEmpleadoBuscar, "E_CODIGO", "E_APELLIDO, E_NOMBRE", "--TODOS--", true);
            string[] nombres = { "E_APELLIDO" , "E_NOMBRE"};
            cboEmpleadoBuscar.SetDatos(dvEmpleadoBuscar, "E_CODIGO", nombres, ", ", "--TODOS--", true);
            //cboEmpleado.SetDatos(dvEmpleado, "E_CODIGO", "E_APELLIDO, E_NOMBRE", "Seleccione...", true);
            cboEmpleado.SetDatos(dvEmpleado, "E_CODIGO", nombres, ", ", "Seleccione...", true);
            cboTipoMantenimientoBuscar.SetDatos(dvTipoMantenimientoBuscar, "TMAN_CODIGO", "TMAN_NOMBRE", "--TODOS--", true);
            cboMaquinaBuscar.SetDatos(dvMaquinaBuscar, "MAQ_CODIGO", "MAQ_NOMBRE", "--TODOS--", true);
            cboMaquina.SetDatos(dvMaquina, "MAQ_CODIGO", "MAQ_NOMBRE", "Seleccione...", false);
            cboPlanes.SetDatos(dvPlanMantenimiento, "PMAN_NUMERO", "PMAN_DESCRIPCION", "Seleccione...", false);
            cboMantenimiento.SetDatos(dvMantenimiento, "MAN_CODIGO", "MAN_DESCRIPCION", "Seleccione...", false);
            cboTipoMantenimiento.SetDatos(dvTipoMantenimiento, "TMAN_CODIGO", "TMAN_NOMBRE", "", false );

        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.Clear();
                dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.Clear();

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.RegistrosMantenimientoBLL.ObtenerRegistrosMantenimiento(sfFechaDesde.GetFecha(), sfFechaHasta.GetFecha(), cboEmpleadoBuscar.GetSelectedValueInt(), cboMaquinaBuscar.GetSelectedValueInt(), cboTipoMantenimientoBuscar.GetSelectedValueInt(), dsRegistrarMantenimiento, true);

                if (dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros de mantenimiento con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //seleccionarCampos(0);
                }
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvRegistrarMantenimiento.Table = dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS;
                dvDetalleRepuestos.Table = dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO;
                dvRepuestos.Table = dsRegistrarMantenimiento.REPUESTOS;

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Registro de Mantenimiento - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //seleccionarCampos(e.RowIndex);
            long codigo = Convert.ToInt64(dvRegistrarMantenimiento[e.RowIndex]["RMAN_CODIGO"]);
            int tipo = Convert.ToInt32(dvRegistrarMantenimiento[e.RowIndex]["TMAN_CODIGO"]);
            //txtNumero.Text = dsRegistrarMantenimiento.PLANES_MANTENIMIENTO.FindByPMAN_NUMERO(codigo).RMAN_CODIGO.ToString();
            if (tipo == 1)
            {//Preventivo
                cboTipoMantenimiento.SetSelectedValue(1); 
                cboMantenimiento.SetSelectedValue(Convert.ToInt32(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).MAN_CODIGO));  
                //cboPlanes.SetSelectedValue(Convert.ToInt32(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).pman_CODIGO));
            }
            else 
            {//Correctivo
                cboTipoMantenimiento.SetSelectedValue(2); 
                cboMaquina.SetSelectedValue(Convert.ToInt32(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).MAQ_CODIGO));
                cboMantenimiento.SetSelectedValue(Convert.ToInt32(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).MAN_CODIGO));  
            }
            setearTipoMantenimiento();
            cboEmpleado.SetSelectedValue(Convert.ToInt32(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).E_CODIGO));
            txtObservacion.Text = dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).RMAN_OBSERVACION;
            sfFechaRealizacion.SetFecha(dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).RMAN_FECHA_REALIZACION);
            
            //Usemos el filtro del dataview para mostrar sólo las Detalles del Pedido seleccionado
            dvDetalleRepuestos.RowFilter = "RMAN_CODIGO = " + codigo;

        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {
                string nombre;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "E_CODIGO":
                        nombre = dsRegistrarMantenimiento.EMPLEADOS.FindByE_CODIGO(Convert.ToInt64(e.Value.ToString())).E_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "TMAN_CODIGO":
                        nombre = dsRegistrarMantenimiento.TIPOS_MANTENIMIENTOS.FindByTMAN_CODIGO(Convert.ToInt64(e.Value.ToString())).TMAN_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MAN_CODIGO":
                        nombre = dsRegistrarMantenimiento.MANTENIMIENTOS.FindByMAN_CODIGO(Convert.ToInt64(e.Value.ToString())).MAN_DESCRIPCION;
                        e.Value = nombre;
                        break;
                    //case "RMAN_FECHA_REALIZACION":
                    //    nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                    //    e.Value = nombre;
                    //    break;
                    default:
                        break;
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.RejectChanges();
            dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.RejectChanges();
            SetInterface(estadoUI.inicio);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                long codigoDetalle = Convert.ToInt64(dvDetalleRepuestos[dgvDetalle.SelectedRows[0].Index]["DPMAN_CODIGO"]);
                //Lo borramos pero sólo del dataset                
                dsRegistrarMantenimiento.DETALLE_PLANES_MANTENIMIENTO.FindByDPMAN_CODIGO(codigoDetalle).Delete();
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
            if (dgvRepuestos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidad.Value > 0 )
            {
                bool agregarRepuesto; //variable que indica si se debe agregar la cocina al listado
                //Obtenemos el código de la pieza según sea nueva o modificada, lo hacemos acá porque lo vamos a usar mucho
                int registroCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { registroCodigo = -1; }
                else { registroCodigo = Convert.ToInt32(dvRegistrarMantenimiento[dgvLista.SelectedRows[0].Index]["RMAN_CODIGO"]); }
                //Obtenemos el código del mantenimiento, también lo vamos a usar mucho
                int repuestoCodigo = Convert.ToInt32(dvRepuestos[dgvRepuestos.SelectedRows[0].Index]["REP_CODIGO"]);


                //Primero vemos si el pedido tiene algúna cocina cargada, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvDetalleRepuestos.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar la misma cocina haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "RMAN_CODIGO = " + registroCodigo + " AND REP_CODIGO = " + repuestoCodigo;
                    Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow[] rows =
                        (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow[])dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = MessageBox.Show("El Registro ya posee el repuesto seleccionado. ¿Desea sumar la cantidad ingresada?", "Pregunta: Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].RRMAN_CANTIDAD += int.Parse(nudCantidad.Value.ToString());
                            nudCantidad.Value = 0;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarRepuesto = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarRepuesto = true;
                    }
                }
                else
                {
                    //No tiene ningúna materia prima agregada, marcamos que debe agregarse
                    agregarRepuesto = true;
                }

                //Ahora comprobamos si debe agregarse el mantenimiento o no
                if (agregarRepuesto)
                {
                    Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow row = dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.NewREPUESTOS_REGISTRO_MANTENIMIENTORow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.RRMAN_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.RMAN_CODIGO = registroCodigo;
                    row.REP_CODIGO = repuestoCodigo;
                    row.RRMAN_CANTIDAD = decimal.Parse(nudCantidad.Value.ToString());
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.AddREPUESTOS_REGISTRO_MANTENIMIENTORow(row);
                    nudCantidad.Value = 0;
                }
                nudCantidad.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Repuesto de la lista, asignarle una cantidad mayor a 0.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Datos opcionales = descripcion
            //Revisamos que completó los datos obligatorios
            string datosFaltantes = string.Empty;
            //if (txtNumero.Text == string.Empty) { datosFaltantes += "* Numero\n"; }
            if (sfFechaRealizacion.GetFecha() == null) { datosFaltantes += "* Fecha de Realizacion\n"; }
            if (cboEmpleado.GetSelectedIndex() == -1) { datosFaltantes += "* Empleado\n"; }
            if (cboTipoMantenimiento.GetSelectedValueInt() == 1)
            {//PREVENTIVO
                //if (cboPlanes.GetSelectedValueInt() == -1) 
                //{
                //    { datosFaltantes += "* Plan\n"; }
                //}
                //if (dgvDetallePlan.Rows.Count < 1 )
                //{
                //    { datosFaltantes += "* Detalle del Plan\n"; }
                //}
            }
            else
            {//CORRECTIVO
                //if (cboMantenimiento.GetSelectedValueInt() == -1)
                //{
                //    { datosFaltantes += "* Mantenimiento\n"; }
                //}
                //if (cboMaquina.GetSelectedValueInt() == -1)
                //{
                //    { datosFaltantes += "* Maquina\n"; }
                //}
            }
            //if (txtDescripcion.Text == string.Empty) { datosFaltantes += "* Estado\n"; }
            //if (dgvDetalle.Rows.Count == 0) { datosFaltantes += "* El detalle del Pedido\n"; }
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
                        Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow rowRegistro = dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.NewREGISTROS_MANTENIMIENTOSRow();
                        rowRegistro.BeginEdit();
                        rowRegistro.RMAN_CODIGO = -1;
                        rowRegistro.RMAN_FECHA = DBBLL.GetFechaServidor();
                        rowRegistro.E_CODIGO = cboEmpleado.GetSelectedValueInt();
                        
                        if (cboTipoMantenimiento.GetSelectedValueInt() == 1)
                        {
                            rowRegistro.DPMAN_CODIGO = decimal.Parse(dvDetallePlanMantenimiento[dgvDetallePlan.SelectedRows[0].Index]["DPMAN_CODIGO"].ToString());
                            rowRegistro.MAN_CODIGO = long.Parse(dvDetallePlanMantenimiento[dgvDetallePlan.SelectedRows[0].Index]["MAN_CODIGO"].ToString());
                        }
                        else 
                        {
                            rowRegistro.MAQ_CODIGO = cboMaquina.GetSelectedValueInt();
                            rowRegistro.MAN_CODIGO = cboMantenimiento.GetSelectedValueInt();
                        }
                        
                        rowRegistro.RMAN_FECHA_REALIZACION = DateTime.Parse(sfFechaRealizacion.GetFecha().ToString());
                        rowRegistro.RMAN_OBSERVACION = txtObservacion.Text.Trim();
                        rowRegistro.CF_NUMERO = 0; 
                        

                        rowRegistro.EndEdit();

                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.AddREGISTROS_MANTENIMIENTOSRow(rowRegistro);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad PiezaBLL y PiezaDAL sepan cuales insertar
                        BLL.RegistrosMantenimientoBLL.Insertar(dsRegistrarMantenimiento);

                        //Ahora si aceptamos los cambios
                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.AcceptChanges();
                        dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.AcceptChanges();
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
                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    long codigoRegistro = Convert.ToInt64(dvRegistrarMantenimiento[dgvLista.SelectedRows[0].Index]["RMAN_CODIGO"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, el detalle se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    //dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).RMAN_CODIGO = long.Parse(txtNumero.Text.ToString());
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).E_CODIGO = cboEmpleado.GetSelectedValueInt();
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).MAN_CODIGO = cboMantenimiento.GetSelectedValueInt();
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).MAQ_CODIGO = cboMaquina.GetSelectedValueInt();
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).RMAN_FECHA_REALIZACION = DateTime.Parse(sfFechaRealizacion.GetFecha().ToString());
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).RMAN_OBSERVACION = txtObservacion.Text.Trim();
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).CF_NUMERO = 0;
                    //dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).DPMAN_CODIGO = dgvDetallePlan.Rows[dgvDetallePlan.SelectedRows].Cells["DPMAN_CODIGO"];
                    dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigoRegistro).DPMAN_CODIGO = decimal.Parse(dvDetallePlanMantenimiento[dgvDetallePlan.SelectedRows[0].Index]["DPMAN_CODIGO"].ToString());
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.RegistrosMantenimientoBLL.Actualizar(dsRegistrarMantenimiento);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.AcceptChanges();
                        dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.ErrorInesperadoException ex)
                    {
                        //Hubo problemas no esperados, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.RejectChanges();
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
                //int estado = Convert.ToInt32(dvRegistrarMantenimiento[dgvLista.SelectedRows[0].Index]["EPMAN_CODIGO"]);
                //if (estado != 1) //Si no esta pendiente no lo puede eliminar PARAMETRIZAR
                //{
                    //Preguntamos si está seguro
                    DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el Registro de Mantenimiento seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            //Obtenemos el codigo
                            long codigo = Convert.ToInt64(dvRegistrarMantenimiento[dgvLista.SelectedRows[0].Index]["RMAN_CODIGO"]);
                            //Lo eliminamos de la DB
                            BLL.RegistrosMantenimientoBLL.Eliminar(codigo);
                            //Lo eliminamos de la tabla conjuntos del dataset
                            dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.FindByRMAN_CODIGO(codigo).Delete();
                            dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.AcceptChanges();
                        }
                        catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                        {
                            MessageBox.Show(ex.Message, "Error: Registro Mantenimiento - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
                            MessageBox.Show(ex.Message, "Error: (Base) Registro Mantenimiento - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }   
                    }
                //}
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Registro de Mantenimiento de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cboTipoMantenimiento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setearTipoMantenimiento();
        }

        private void setearTipoMantenimiento() 
        {
            if (cboTipoMantenimiento.GetSelectedValueInt() == 1)
            {
                //Preventivo
                groupCorrectivo.Visible = false;
                groupPreventivo.Visible = true;
            }
            else
            {
                //Correctivo
                groupPreventivo.Visible = false;
                groupCorrectivo.Visible = true;
            }
        }

        private void cboPlanes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dsRegistrarMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Clear();

            BLL.DetallePlanMantenimientoBLL.ObtenerDetallePlanMantenimiento(dsRegistrarMantenimiento.DETALLE_PLANES_MANTENIMIENTO, cboPlanes.GetSelectedValueInt());

            if (dsRegistrarMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Rows.Count > 0)
            {
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvDetallePlanMantenimiento.Table = dsRegistrarMantenimiento.DETALLE_PLANES_MANTENIMIENTO;
            }
            
        }

        private void dgvDetallePlan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;

                switch (dgvDetallePlan.Columns[e.ColumnIndex].Name)
                {
                    case "UMED_CODIGO":
                        nombre = dsRegistrarMantenimiento.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value.ToString())).UMED_ABREVIATURA;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvRepuestos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvRepuestos.Columns[e.ColumnIndex].Name)
                {
                    case "TREP_CODIGO":
                        nombre = dsRegistrarMantenimiento.TIPOS_REPUESTOS.FindByTREP_CODIGO(Convert.ToInt32(e.Value.ToString())).TREP_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvDetalle.Columns[e.ColumnIndex].Name)
                {
                    case "REP_CODIGO":
                        nombre = dsRegistrarMantenimiento.REPUESTOS.FindByREP_CODIGO(Convert.ToInt32(e.Value.ToString())).REP_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "Costo":
                        float prod;
                        prod = dsRegistrarMantenimiento.REPUESTOS.FindByREP_CODIGO(Convert.ToInt32(e.Value.ToString())).REP_COSTO * float.Parse(dgvDetalle.Rows[e.RowIndex].Cells["RRMAN_CANTIDAD"].Value.ToString());
                        nombre = prod.ToString() ;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
