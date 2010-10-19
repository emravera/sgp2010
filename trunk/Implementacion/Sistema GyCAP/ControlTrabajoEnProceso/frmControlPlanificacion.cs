using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GyCAP.UI.ControlTrabajoEnProceso
{
    public partial class frmControlPlanificacion : Form
    {
        private static frmControlPlanificacion _frmControlPlanificacion = null;
        private enum estadoUI { inicio, controlPSem, controlPAN };
        //Dataviews Primer Tabpage
        private DataView dvComboPlanAnual, dvComboPlanMensual, dvComboSemana, dvListaPlanSemanal, dvListaDetalleDiario;
        //Dataview segundo tabpage
        private DataView dvComboPlanAnual2, dvListaPlanAnual, dvListaDetallePA;

        private static estadoUI estadoActual;
        private Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        private static bool seleccionPestaña = false;
        private static int seriesGraficos = 0;

        public frmControlPlanificacion()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalleDiario.AutoGenerateColumns = false;
            dgvDetalleMensual.AutoGenerateColumns = false;
            dgvDetallePlan.AutoGenerateColumns = false;
            dgvMesesPlanAnual.AutoGenerateColumns = false;

            //*********************************CREACION GRILLAS PLAN SEMANAL *********************************
            //Lista de PLAN SEMANAL (DIAS)
            //Agregamos la columnas
            dgvDetallePlan.Columns.Add("DIAPSEM_CODIGO", "Código");
            dgvDetallePlan.Columns.Add("PSEM_CODIGO", "N° Semana");
            dgvDetallePlan.Columns.Add("DIAPSEM_DIA", "Día");
            dgvDetallePlan.Columns.Add("DIAPSEM_FECHA", "Fecha");

            //Seteamos el modo de tamaño de las columnas
            dgvDetallePlan.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetallePlan.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDetallePlan.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvDetallePlan.Columns["DIAPSEM_DIA"].DataPropertyName = "DIAPSEM_DIA";
            dgvDetallePlan.Columns["DIAPSEM_FECHA"].DataPropertyName = "DIAPSEM_FECHA";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanSemanal = new DataView(dsPlanSemanal.DIAS_PLAN_SEMANAL);
            dgvDetallePlan.DataSource = dvListaPlanSemanal;


            //Lista de Detalles de Planes Semanales
            //Agregamos la columnas
            dgvDetalleDiario.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDetalleDiario.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetalleDiario.Columns.Add("DIAPSEM_CODIGO", "Código Día");
            dgvDetalleDiario.Columns.Add("DPSEM_CANTIDADESTIMADA", "C.Estimada");
            dgvDetalleDiario.Columns.Add("DPSEM_CANTIDADREAL", "C.Real");
            dgvDetalleDiario.Columns.Add("DPSEM_CANTIDADENPROCESO", "C.Proceso");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalleDiario.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDetalleDiario.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDetalleDiario.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalleDiario.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPSEM_CANTIDADESTIMADA";
            dgvDetalleDiario.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPSEM_CANTIDADREAL";
            dgvDetalleDiario.Columns["DPSEM_CANTIDADENPROCESO"].DataPropertyName = "DPSEM_CANTIDADENPROCESO";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalleDiario.Columns[0].Visible = false;
            dgvDetalleDiario.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleDiario.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleDiario.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleDiario.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleDiario.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalleDiario = new DataView(dsPlanSemanal.DETALLE_PLANES_SEMANALES);
            dgvDetalleDiario.DataSource = dvListaDetalleDiario;
            //******************************************************************************************

            //********************************* CREACION DE GRILLAS DE PLAN ANUAL **********************
            //Lista de PLAN ANUAL (DIAS)
            //Agregamos la columnas
            dgvMesesPlanAnual.Columns.Add("PMES_CODIGO", "Código");
            dgvMesesPlanAnual.Columns.Add("PAN_CODIGO", "Codigo Plan Anual");
            dgvMesesPlanAnual.Columns.Add("PMES_MES", "Mes");
            dgvMesesPlanAnual.Columns.Add("PMES_FECHACREACION", "Fecha Creación");

            //Seteamos el modo de tamaño de las columnas
            dgvMesesPlanAnual.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMesesPlanAnual.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMesesPlanAnual.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMesesPlanAnual.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvMesesPlanAnual.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvMesesPlanAnual.Columns["PAN_CODIGO"].DataPropertyName = "PAN_CODIGO";
            dgvMesesPlanAnual.Columns["PMES_MES"].DataPropertyName = "PMES_MES";
            dgvMesesPlanAnual.Columns["PMES_FECHACREACION"].DataPropertyName = "PMES_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanAnual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
            dgvMesesPlanAnual.DataSource = dvListaPlanAnual;


            //Lista de Detalles de Planes Semanales que nacen del plan anual
            //Agregamos la columnas
            dgvDetalleMensual.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDetalleMensual.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetalleMensual.Columns.Add("DIAPSEM_CODIGO", "Código Día");
            dgvDetalleMensual.Columns.Add("DPSEM_CANTIDADESTIMADA", "C.Estimada");
            dgvDetalleMensual.Columns.Add("DPSEM_CANTIDADREAL", "C.Real");
            dgvDetalleMensual.Columns.Add("DPSEM_CANTIDADENPROCESO", "C.Proceso");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalleMensual.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDetalleMensual.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDetalleMensual.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalleMensual.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPSEM_CANTIDADESTIMADA";
            dgvDetalleMensual.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPSEM_CANTIDADREAL";
            dgvDetalleMensual.Columns["DPSEM_CANTIDADENPROCESO"].DataPropertyName = "DPSEM_CANTIDADENPROCESO";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalleMensual.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleMensual.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleMensual.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleMensual.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleMensual.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetallePA = new DataView(dsPlanSemanal.DETALLE_PLANES_SEMANALES);
            dgvDetalleMensual.DataSource = dvListaDetallePA;

            //******************************************************************************************

            //Metodos para llenar el Dataset
            //Planes Anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);
            //Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsPlanSemanal.COCINAS);

            //CARGA DE COMBOS
            //Cargamos el combo de busqueda del Plan semanal
            dvComboPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbPlanAnual.SetDatos(dvComboPlanAnual, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el combo de búqueda del plan Anual
            dvComboPlanAnual2 = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbPlanAnual2.SetDatos(dvComboPlanAnual2, "pan_codigo", "pan_anio", "Seleccione", false);





            //Seteamos la interface
            SetInterface(estadoUI.inicio);
        }

        #region Servicios
        public static frmControlPlanificacion Instancia
        {
            get
            {
                if (_frmControlPlanificacion == null || _frmControlPlanificacion.IsDisposed)
                {
                    _frmControlPlanificacion = new frmControlPlanificacion();
                }
                else
                {
                    _frmControlPlanificacion.BringToFront();
                }
                return _frmControlPlanificacion;
            }
            set
            {
                _frmControlPlanificacion = value;
            }
        }
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    //Tabcontrol Plan Semanal
                    gbDatosPrincipales.Visible = true;
                    gbDetallePlanSemanal.Visible = false;
                    gbGraficaSemanal.Visible = false;

                    cbPlanAnual.Enabled = true;
                    cbPlanAnual.SetSelectedIndex(-1);
                    cbPlanMensual.Enabled = true;
                    cbPlanMensual.SetSelectedIndex(-1);
                    cbPlanSemanal.Enabled = true;
                    cbPlanSemanal.SetSelectedIndex(-1);

                    //Tabcontrol del Plan Anual
                    gbDatosPlanAnual.Visible = true;
                    gbDetallePlanAnual.Visible = false;
                    gbGraficaPlanAnual.Visible = false;
                    break;
                case estadoUI.controlPSem:
                    //Tab control del Plan semanal
                    gbDatosPrincipales.Visible = true;
                    gbDetallePlanSemanal.Visible = true;
                    gbGraficaSemanal.Visible = false;

                    //Columnas de las grillas
                    //Ponemos las columnas de las grillas en visible false
                    dgvDetallePlan.Columns["DIAPSEM_CODIGO"].Visible = false;
                    break;
                case estadoUI.controlPAN:
                    //Tab control del Plan Anual
                    gbDatosPlanAnual.Visible = true;
                    gbDetallePlanAnual.Visible = true;
                    gbGraficaPlanAnual.Visible = false;

                    //Columnas de las grillas
                    //Ponemos las columnas de las grillas en visible false
                    dgvMesesPlanAnual.Columns["PMES_CODIGO"].Visible = false;
                    dgvMesesPlanAnual.Columns["PAN_CODIGO"].Visible = false;

                    break;

            }
        }
        //Botones del MENU
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #endregion



        #region Pestaña Plan Semanal

        private void cbPlanAnual_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbPlanAnual.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbPlanAnual.GetSelectedValue());

                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, codigo);

                    dvComboPlanMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
                    cbPlanMensual.SetDatos(dvComboPlanMensual, "pmes_codigo", "pmes_mes", "pmes_codigo ASC", "Seleccione", false);

                    if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count > 0)
                    {
                        cbPlanAnual.Enabled = false;
                        cbPlanMensual.Enabled = true;
                    }
                    else
                    {
                        cbPlanAnual.Enabled = true;
                        cbPlanMensual.Enabled = false;
                    }
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control Plan - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void cbPlanMensual_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbPlanMensual.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbPlanMensual.GetSelectedValue());

                    BLL.PlanSemanalBLL.obtenerPS(dsPlanSemanal.PLANES_SEMANALES, codigo);

                    if (dsPlanSemanal.PLANES_SEMANALES.Rows.Count > 0)
                    {

                        dvComboSemana = new DataView(dsPlanSemanal.PLANES_SEMANALES);
                        cbPlanSemanal.SetDatos(dvComboSemana, "psem_codigo", "psem_semana", "Seleccione", false);
                        cbPlanSemanal.Enabled = true;
                        cbPlanMensual.Enabled = false;
                    }
                    else
                    {
                        cbPlanSemanal.Enabled = false;
                        MessageBox.Show("-No hay planes Semanales creados para ese mes", "Información: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetInterface(estadoUI.inicio);
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control Planes - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }

        }
        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();

                //Obtengo el Codigo del plan Semanal
                int codigo = Convert.ToInt32(cbPlanSemanal.GetSelectedValue());

                //Se busca el Plan Semanal
                BLL.DiasPlanSemanalBLL.obtenerDias(dsPlanSemanal.DIAS_PLAN_SEMANAL, codigo);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanSemanal.Table = dsPlanSemanal.DIAS_PLAN_SEMANAL;

                if (dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Días para la semama ingresada.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SetInterface(estadoUI.controlPSem);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }
        private void GenerarDetalleDiario(int codigo, bool limpiar)
        {
            try
            {
                if (limpiar == true)
                {
                    //Se programa la busqueda del detalle
                    //Limpiamos el Dataset
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();
                }

                //Se llama a la funcion de busqueda del detalle
                BLL.DetallePlanSemanalBLL.ObtenerDetalle(codigo, dsPlanSemanal.DETALLE_PLANES_SEMANALES);

                //Se calcula la cantidad de produccion en proceso para cada detalle
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                {
                    //Obtengo el Código de detalle
                    int codigoDetalle = Convert.ToInt32(row.DPSEM_CODIGO);

                    //Verifico que exista una orden de producción para ese detalle
                    if (BLL.PlanSemanalBLL.VerificarOrden(codigoDetalle) == 1)
                    {
                        //Busco si el Codigo de detalle esta en proceso
                        int estado = Convert.ToInt32(BLL.PlanSemanalBLL.ObtenerEstado(codigoDetalle));

                        //si esta en proceso entonces tomo la cantidad y la pongo como cantidad en proceso
                        if (estado == BLL.OrdenProduccionBLL.EstadoEnProceso)
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADENPROCESO = Convert.ToDecimal(row.DPSEM_CANTIDADESTIMADA);
                            row.EndEdit();
                        }
                        else
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADENPROCESO = 0;
                            row.EndEdit();
                        }
                        if (row.IsDPSEM_CANTIDADREALNull())
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADREAL = 0;
                            row.EndEdit();
                        }
                    }
                    else
                    {
                        if (row.IsDPSEM_CANTIDADREALNull())
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADREAL = 0;
                            row.EndEdit();
                        }

                        row.BeginEdit();
                        row.DPSEM_CANTIDADENPROCESO = 0;
                        row.EndEdit();
                    }
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalleDiario.Table = dsPlanSemanal.DETALLE_PLANES_SEMANALES;

                if (dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para ese día del plan semanal.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }

        }

        private void btnDetalleDiario_Click(object sender, EventArgs e)
        {
            try
            {
                //Habilito la seleccion de la tabpage
                seleccionPestaña = true;

                //Se programa los datos que se utilizaron para hacer la grafica
                if (chDetallesDiarios.Checked == false)
                {
                    int codigo = Convert.ToInt32(dvListaPlanSemanal[dgvDetallePlan.SelectedRows[0].Index]["diapsem_codigo"]);

                    GenerarDetalleDiario(codigo, true);

                    tcDetalle.SelectedTab = tpDetalleDiario;

                    //Ocultamos las columnas que no se quieren ver
                    dgvDetalleDiario.Columns["DIAPSEM_CODIGO"].Visible = false;
                    dgvDetalleDiario.Columns["DPSEM_CODIGO"].Visible = false;
                }
                else
                {
                    foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows)
                    {
                        //Obtengo el código del día
                        int codigo = Convert.ToInt32(rowDia["diapsem_codigo"]);

                        //Llamo a la funcion para que me genere el detalle
                        GenerarDetalleDiario(codigo, false);

                        tcDetalle.SelectedTab = tpDetalleDiario;

                        //Ocultamos las columnas que no se quieren ver
                        dgvDetalleDiario.Columns["DIAPSEM_CODIGO"].Visible = false;
                        dgvDetalleDiario.Columns["DPSEM_CODIGO"].Visible = false;
                    }
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnAvanceDiario_Click(object sender, EventArgs e)
        {
            try
            {
                //Defino el vector para pasar los datos
                int[] cantidades = new int[3];

                if (chDetallesDiarios.Checked == true)
                {
                    foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows)
                    {
                        //Obtengo el código del día
                        int codigo = Convert.ToInt32(rowDia["diapsem_codigo"]);
                        string nombreSerie = rowDia.DIAPSEM_DIA.ToString();

                        //Llamo a la funcion para que me genere el detalle
                        GenerarDetalleDiario(codigo, true);

                        //Si se quiere sacar todos los detalles juntos
                        foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                        {
                            //Cargo los valores del Grafico a un vector
                            cantidades[0] = cantidades[0] + Convert.ToInt32(row.DPSEM_CANTIDADESTIMADA);
                            cantidades[1] = cantidades[1] + Convert.ToInt32(row.DPSEM_CANTIDADENPROCESO);
                            cantidades[2] = cantidades[2] + Convert.ToInt32(row.DPSEM_CANTIDADREAL);
                        }

                        //Genero el grafico con los valores
                        GenerarGrafico(cantidades, chartAvance, nombreSerie);

                        //Vuelvo los valores del vector a 0
                        cantidades[0] = 0; cantidades[1] = 0; cantidades[2] = 0;
                    }
                   
                }
                else
                    {
                        //Obtengo el código del día
                        int codigo = Convert.ToInt32(dvListaPlanSemanal[dgvDetallePlan.SelectedRows[0].Index]["diapsem_codigo"]);
                        string nombreSerie = dvListaPlanSemanal[dgvDetallePlan.SelectedRows[0].Index]["diapsem_dia"].ToString();

                        //Llamo a la funcion para que me genere el detalle
                        GenerarDetalleDiario(codigo, true);

                        //Si se quiere sacar todos los detalles juntos
                        foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                        {
                            //Cargo los valores del Grafico a un vector
                            cantidades[0] = cantidades[0] + Convert.ToInt32(row.DPSEM_CANTIDADESTIMADA);
                            cantidades[1] = cantidades[1] + Convert.ToInt32(row.DPSEM_CANTIDADENPROCESO);
                            cantidades[2] = cantidades[2] + Convert.ToInt32(row.DPSEM_CANTIDADREAL);
                        }

                        //Genero el grafico con los valores
                        GenerarGrafico(cantidades, chartAvance, nombreSerie);

                        //Vuelvo los valores del vector a 0
                        cantidades[0] = 0; cantidades[1] = 0; cantidades[2] = 0;
                    }

                    //Muestro el grafico generado
                    gbGraficaSemanal.Visible = true;
                    gbDetallePlanSemanal.Visible = false;

                    //pongo en 0 la serie de nuevo
                    seriesGraficos = 0;
            
            }            
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }

        }

        private void dgvDetallePlan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvDetallePlan.Columns[e.ColumnIndex].Name)
                {
                    case "PSEM_CODIGO":
                        int semana = Convert.ToInt32(dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(Convert.ToInt32(e.Value)).PSEM_SEMANA);
                        e.Value = semana;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvDetalleDiario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvDetalleDiario.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        string nombre = dsPlanSemanal.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void tcDetalle_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpDetalleDiario && seleccionPestaña == false)
            {
                e.Cancel = true;
            }
            else if (seleccionPestaña == true)
            {
                seleccionPestaña = false;
            }
        }

        //METODO QUE GENERA EL GRAFICO
        private void GenerarGrafico(int[] Valores, Chart grafica, string nombreSerie)
        {
            if (seriesGraficos == 0)
            {
                grafica.Series.Clear();
                seriesGraficos = 1;
            }
            
            //Agrego una serie nueva con el nombre del contador de series
            grafica.Series.Add(nombreSerie);
                  
            //Defino el tipo de grafico que deseo
            grafica.Series[nombreSerie].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            //Lo dibujo
            double plotY = 0;
            if (grafica.Series[nombreSerie].Points.Count > 0)
            {
                plotY = grafica.Series[nombreSerie].Points[grafica.Series["0"].Points.Count - 1].YValues[0];
            }

            for (int pointIndex = 0; pointIndex < Valores.Count(); pointIndex++)
            {

                plotY = Convert.ToInt32(Valores[(pointIndex)]);
                grafica.Series[nombreSerie].Points.AddY(plotY);

                switch (pointIndex%3)
                {
                    case 0:
                        grafica.Series[nombreSerie].Points[pointIndex].AxisLabel = "Produccion Planificada";
                        break;
                    case 1:
                        grafica.Series[nombreSerie].Points[pointIndex].AxisLabel = "Produccion Proceso";
                        break;
                    case 2:
                        grafica.Series[nombreSerie].Points[pointIndex].AxisLabel = "Produccion Terminada";
                        break;
                }
            }

        }
        //Boton Volver
        private void button1_Click(object sender, EventArgs e)
        {
            tcDetalle.SelectedTab = tpDias;
        }

        #endregion

        #region Pestaña Plan Anual

        private void btnDatosPlanAnual_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanSemanal.PLANES_MENSUALES.Clear();

                //Obtengo el Codigo del plan anual seleccionado
                int codigo = Convert.ToInt32(cbPlanAnual2.GetSelectedValue());

                //Se busca los planes mensuales generados para ese año
                BLL.PlanSemanalBLL.ObtenerPlanesMensuales(codigo, dsPlanSemanal);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanAnual.Table = dsPlanSemanal.PLANES_MENSUALES;

                if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron planes mensuales para el año ingresado.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SetInterface(estadoUI.controlPAN);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        private void GenerarDetalleMes(int codigoPM, bool elimina)
        {
            try
            {

                //Ponemos la seleccion de la pestaña en True
                seleccionPestaña = true;

                if (elimina == true)
                {
                    //Limpiamos el Dataset
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();
                    dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();
                    dsPlanSemanal.PLANES_SEMANALES.Clear();
                }

                //Cargo las semanas que se generaron para ese plan mensual
                BLL.PlanSemanalBLL.obtenerPS(dsPlanSemanal.PLANES_SEMANALES, codigoPM);

                //Recorro para todas las semanas y cargo los dias de las mismas
                foreach (Data.dsPlanSemanal.PLANES_SEMANALESRow row in dsPlanSemanal.PLANES_SEMANALES.Rows)
                {
                    //Obtengo el código del plan semanal
                    int codigoPS = Convert.ToInt32(row["PSEM_CODIGO"]);

                    //Cargo los dias de ese plan semanal
                    BLL.DiasPlanSemanalBLL.obtenerDias(dsPlanSemanal.DIAS_PLAN_SEMANAL, codigoPS);
                }

                //Recorro cada uno de los dias del plan semanal y obtengo su detalle
                foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow diarow in dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows)
                {
                    //Obtengo el código del día
                    int codigoDia = Convert.ToInt32(diarow["DIAPSEM_CODIGO"]);

                    //Se llama a la funcion de busqueda del detalle
                    BLL.DetallePlanSemanalBLL.ObtenerDetalle(codigoDia, dsPlanSemanal.DETALLE_PLANES_SEMANALES);
                }


                //Se calcula la cantidad de produccion en proceso para cada detalle
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                {
                    //Obtengo el Código de detalle
                    int codigoDetalle = Convert.ToInt32(row.DPSEM_CODIGO);

                    //Verifico que exista una orden de producción para ese detalle
                    if (BLL.PlanSemanalBLL.VerificarOrden(codigoDetalle) == 1)
                    {
                        //Busco si el Codigo de detalle esta en proceso
                        int estado = Convert.ToInt32(BLL.PlanSemanalBLL.ObtenerEstado(codigoDetalle));

                        //si esta en proceso entonces tomo la cantidad y la pongo como cantidad en proceso
                        if (estado == BLL.OrdenProduccionBLL.EstadoEnProceso)
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADENPROCESO = Convert.ToDecimal(row.DPSEM_CANTIDADESTIMADA);
                            row.EndEdit();
                        }
                        else
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADENPROCESO = 0;
                            row.EndEdit();
                        }
                        if (row.IsDPSEM_CANTIDADREALNull())
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADREAL = 0;
                            row.EndEdit();
                        }
                    }
                    else
                    {
                        if (row.IsDPSEM_CANTIDADREALNull())
                        {
                            row.BeginEdit();
                            row.DPSEM_CANTIDADREAL = 0;
                            row.EndEdit();
                        }

                        row.BeginEdit();
                        row.DPSEM_CANTIDADENPROCESO = 0;
                        row.EndEdit();
                    }
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetallePA.Table = dsPlanSemanal.DETALLE_PLANES_SEMANALES;

                /*if (dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para ese mes en el plan anual.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Anual", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvDetalleMensual_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvDetalleMensual.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        string nombre = dsPlanSemanal.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }

        }

        private void btnVolver2_Click(object sender, EventArgs e)
        {
            tcPlanAnual.SelectedTab = tpMesesPlanAnual;
        }

        private void tcPlanAnual_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpDetalleMeses && seleccionPestaña == false)
            {
                e.Cancel = true;
            }
            else if (seleccionPestaña == true)
            {
                seleccionPestaña = false;
            }
        }

        private void btnGraficaPA_Click(object sender, EventArgs e)
        {

            try
            {
                //Defino el vector para pasar los datos
                int[] cantidades = new int[3];

                if (chTodosMeses.Checked == true)
                {
                    foreach (Data.dsPlanSemanal.PLANES_MENSUALESRow rowMes in dsPlanSemanal.PLANES_MENSUALES.Rows)
                    {
                        //Obtenemos el Código del Plan Mensual seleccionado
                        int codigoPM = Convert.ToInt32(rowMes["pmes_codigo"]);
                        string nombreSerie = rowMes.PMES_MES.ToString();

                        //Llamo a la funcion para que me genere el detalle
                        GenerarDetalleMes(codigoPM, true); 

                        //Si se quiere sacar todos los detalles juntos
                        foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                        {
                            //Cargo los valores del Grafico a un vector
                            cantidades[0] = cantidades[0] + Convert.ToInt32(row.DPSEM_CANTIDADESTIMADA);
                            cantidades[1] = cantidades[1] + Convert.ToInt32(row.DPSEM_CANTIDADENPROCESO);
                            cantidades[2] = cantidades[2] + Convert.ToInt32(row.DPSEM_CANTIDADREAL);
                        }                        

                        //Genero el grafico con los valores
                        GenerarGrafico(cantidades, chartPlanAnual, nombreSerie);

                        //Vuelvo los valores del vector a 0
                        cantidades[0] = 0; cantidades[1] = 0; cantidades[2] = 0;
                    }
                }
                else
                {

                    int codigoPM = Convert.ToInt32(dvListaPlanAnual[dgvMesesPlanAnual.SelectedRows[0].Index]["pmes_codigo"]);
                    string nombreSerie =dvListaPlanAnual[dgvMesesPlanAnual.SelectedRows[0].Index]["pmes_mes"].ToString(); 

                    //Llamo a la funcion para que me genere el detalle
                    GenerarDetalleMes(codigoPM, true);

                    //Si se quiere sacar todos los detalles juntos
                    foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                    {
                        //Cargo los valores del Grafico a un vector
                        cantidades[0] = cantidades[0] + Convert.ToInt32(row.DPSEM_CANTIDADESTIMADA);
                        cantidades[1] = cantidades[1] + Convert.ToInt32(row.DPSEM_CANTIDADENPROCESO);
                        cantidades[2] = cantidades[2] + Convert.ToInt32(row.DPSEM_CANTIDADREAL);
                    }

                    //Genero el grafico con los valores
                    GenerarGrafico(cantidades, chartPlanAnual, nombreSerie);

                    //Vuelvo los valores del vector a 0
                    cantidades[0] = 0; cantidades[1] = 0; cantidades[2] = 0;
                }

                    //Muestro el grafico generado
                    gbDetallePlanAnual.Visible = false;
                    gbGraficaPlanAnual.Visible = true;

                    //pongo en 0 la serie de nuevo
                    seriesGraficos = 0;

             
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Anual", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetalleMeses_Click(object sender, EventArgs e)
        {
            try
            {
                //Habilito la seleccion de la tabpage
                seleccionPestaña = true;

                //Se programa los datos que se utilizaron para hacer la grafica
                if (chTodosMeses.Checked == false)
                {
                    int codigo = Convert.ToInt32(dvListaPlanAnual[dgvMesesPlanAnual.SelectedRows[0].Index]["pmes_codigo"]);

                    GenerarDetalleMes(codigo, true);

                    tcPlanAnual.SelectedTab = tpDetalleMeses;

                    //Ocultamos las columnas que no se quieren ver
                    dgvDetalleMensual.Columns["DPSEM_CODIGO"].Visible = false;
                    dgvDetalleMensual.Columns["DIAPSEM_CODIGO"].Visible = false;
                }
                else
                {
                    foreach (Data.dsPlanSemanal.PLANES_MENSUALESRow rowMes in dsPlanSemanal.PLANES_MENSUALES.Rows)
                    {
                        //Obtengo el código del mes
                        int codigo = Convert.ToInt32(rowMes["pmes_codigo"]);
                        
                        GenerarDetalleMes(codigo, false);

                        tcPlanAnual.SelectedTab = tpDetalleMeses;

                        //Ocultamos las columnas que no se quieren ver
                        dgvDetalleMensual.Columns["DPSEM_CODIGO"].Visible = false;
                        dgvDetalleMensual.Columns["DIAPSEM_CODIGO"].Visible = false;
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control de Planificación - Plan Anual", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnVolverGrafico_Click(object sender, EventArgs e)
        {
            gbGraficaPlanAnual.Visible = false;
            gbDetallePlanAnual.Visible = true;
        }

        private void btnVolverPlanSemanal_Click(object sender, EventArgs e)
        {
            gbDetallePlanSemanal.Visible = true;
            gbGraficaSemanal.Visible = false;
        }
        
    }
}
