using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using GyCAP.UI.Sistema.Validaciones;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmPlanSemanal : Form
    {
        private static frmPlanSemanal _frmPlanSemanal = null;
        private Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        private DataView dvListaPlanes, dvListaDetalle, dvListaDatos, dvListaPlanesMensuales,
        dvComboPlanesAnuales, dvComboMes, dvComboSemBuscar, dvComboAnio, dvComboMesDatos, dvComboSemanaDatos;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        int codigoDetalle = -1; bool esPrimero = true; int codigoPlanSemanal = 0;
        private static bool checkeoExcepciones = false;

        #region Inicio

        public frmPlanSemanal()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;
            dgvDatos.AutoGenerateColumns = false;
            dgvPlanMensual.AutoGenerateColumns = false;

            //Para cada Lista
            //*******************************************************************************
            //                                  LISTAS DE BUSQUEDA
            //*******************************************************************************

            //Lista de los Dias de los Planes Semanales
            //Agregamos la columnas
            dgvLista.Columns.Add("DIAPSEM_CODIGO", "Código");
            dgvLista.Columns.Add("PSEM_CODIGO", "N° Semana");
            dgvLista.Columns.Add("DIAPSEM_DIA", "Día");
            dgvLista.Columns.Add("DIAPSEM_FECHA", "Fecha");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvLista.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvLista.Columns["DIAPSEM_DIA"].DataPropertyName = "DIAPSEM_DIA";
            dgvLista.Columns["DIAPSEM_FECHA"].DataPropertyName = "DIAPSEM_FECHA";

            //Ponemos las columnas de las grillas en visible false
            dgvLista.Columns["DIAPSEM_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanSemanal.DIAS_PLAN_SEMANAL);
            dgvLista.DataSource = dvListaPlanes;
            
            //Lista de Detalles de Planes Semanales
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDetalle.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetalle.Columns.Add("DIAPSEM_CODIGO", "Código Día");
            dgvDetalle.Columns.Add("DPSEM_CANTIDADESTIMADA", "C.Estimada");
            dgvDetalle.Columns.Add("DPSEM_CANTIDADREAL", "C.Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDetalle.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDetalle.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalle.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPSEM_CANTIDADESTIMADA";
            dgvDetalle.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPSEM_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Ponemos las columnas de las grillas en visible false
            dgvDetalle.Columns["DIAPSEM_CODIGO"].Visible = false;
            dgvDetalle.Columns["DPSEM_CODIGO"].Visible = false;
            dgvDetalle.Columns["DPSEM_CANTIDADREAL"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanSemanal.DETALLE_PLANES_SEMANALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //***************************************************************************
            //                                GRILLAS DE DATOS
            //***************************************************************************

            //Lista de Planes Mensuales
            //Agregamos la columnas
            dgvPlanMensual.Columns.Add("DPMES_CODIGO", "Código");
            dgvPlanMensual.Columns.Add("PMES_CODIGO", "Mes");
            dgvPlanMensual.Columns.Add("COC_CODIGO", "Cocina Código");
            dgvPlanMensual.Columns.Add("DPMES_CANTIDADESTIMADA", "C.Estimada");
            dgvPlanMensual.Columns.Add("DPMES_CANTPLANIFICADA", "C.Planificada");
            dgvPlanMensual.Columns.Add("DPED_CODIGO", "Detalle Pedido");
            dgvPlanMensual.Columns.Add("DPMES_CANTIDADREAL", "C.Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvPlanMensual.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvPlanMensual.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvPlanMensual.Columns["DPMES_CANTPLANIFICADA"].DataPropertyName = "DPMES_CANTPLANIFICADA";
            dgvPlanMensual.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvPlanMensual.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvPlanMensual.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";
            dgvPlanMensual.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";

            //Seteamos el modo de tamaño de las columnas
            dgvPlanMensual.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPlanMensual.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPlanMensual.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanesMensuales = new DataView(dsPlanSemanal.DETALLE_PLANES_MENSUALES);
            dgvPlanMensual.DataSource = dvListaPlanesMensuales;

            //Lista de Detalle del Plan Semanal
            //Agregamos la columnas
            dgvDatos.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDatos.Columns.Add("COC_CODIGO", "Cocina");
            dgvDatos.Columns.Add("DIAPSEM_CODIGO", "Día Semana");
            dgvDatos.Columns.Add("DPSEM_CANTIDADESTIMADA", "C.Estimada");
            dgvDatos.Columns.Add("DPSEM_CANTIDADREAL", "C. Real");
            dgvDatos.Columns.Add("DPSEM_ESTADO", "Estado");
            dgvDatos.Columns.Add("DPED_CODIGO", "Detalle Pedido");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatos.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDatos.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDatos.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDatos.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPSEM_CANTIDADESTIMADA";
            dgvDatos.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPSEM_CANTIDADREAL";
            dgvDatos.Columns["DPSEM_ESTADO"].DataPropertyName = "DPSEM_ESTADO";
            dgvDatos.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";

            //Seteamos el modo de tamaño de las columnas
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDatos = new DataView(dsPlanSemanal.DETALLE_PLANES_SEMANALES);
            dgvDatos.DataSource = dvListaDatos;

            //Metodos para llenar el Dataset
            //Planes Anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);

            //Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsPlanSemanal.COCINAS);

            //CARGA DE COMBOS
            //Cargamos el combo de busqueda
            dvComboPlanesAnuales = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbPlanAnual.SetDatos(dvComboPlanesAnuales, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargamos el combo de los datos
            dvComboAnio = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbAnio.SetDatos(dvComboAnio, "pan_codigo", "pan_anio", "Seleccione", false);

            //Seteamos los valores máximos de los numeric
            numPorcentaje.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "100", NumericLimitValues.IncludeExclude.Inclusivo);
            numUnidades.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);

            //Setemoa el valor de la interface
            SetInterface(estadoUI.inicio);

        }

        #endregion
        
        #region Servicios
        
        //Método para evitar la creación de más de una pantalla
        public static frmPlanSemanal Instancia
        {
            get
            {
                if (_frmPlanSemanal == null || _frmPlanSemanal.IsDisposed)
                {
                    _frmPlanSemanal = new frmPlanSemanal();
                }
                else
                {
                    _frmPlanSemanal.BringToFront();
                }
                return _frmPlanSemanal;
            }
            set
            {
                _frmPlanSemanal = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                //Cuando Arranca la pantalla
                case estadoUI.inicio:
                    cbAnio.Enabled = true;
                    cbMes.Enabled = false;
                    cbSemana.Enabled = false;
                   
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    cbMes.SetSelectedIndex(-1);
                    cbSemana.SetSelectedIndex(-1);
                    cbAnio.SetSelectedIndex(-1);

                    //Limpio los combos
                    cbMes.DataSource = null;
                    dsPlanSemanal.PLANES_MENSUALES.Clear();
                    cbMes.Items.Clear();

                    cbSemana.DataSource = null;
                    dsPlanSemanal.PLANES_SEMANALES.Clear();
                    cbSemana.Items.Clear();                    
            
                    //Limpiamos los valores ya buscados
                    dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();

                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.inicio;
                    break;

                //Cuando termina de Buscar
                case estadoUI.buscar:
                    btnNuevo.Enabled = true;
                    bool hayDatos;
                    if (dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows.Count > 0)
                    {
                        hayDatos = true;
                    }
                    else hayDatos = false;
                    btnConsultar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnModificar.Enabled = hayDatos;
                   
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    break;

                //Cuando se presiona el boton nuevo
                case estadoUI.nuevo:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.nuevo;

                    //Ponemos los combo en su estado
                    cbPlanAnual.Enabled = true;
                    cbMesDatos.Enabled = false;
                    cbSemanaDatos.Enabled = false;

                    cbPlanAnual.SetSelectedIndex(-1);
                    cbMesDatos.SetSelectedIndex(-1);
                    cbSemanaDatos.SetSelectedIndex(-1);

                    //Limpiamos los combos
                    cbMesDatos.DataSource = null;
                    dsPlanSemanal.PLANES_MENSUALES.Clear();
                    cbMesDatos.Items.Clear();

                    cbSemanaDatos.DataSource = null;
                    dsPlanSemanal.PLANES_SEMANALES.Clear();
                    cbSemanaDatos.Items.Clear();

                    //Seteamos los estados de los groupbox
                    gbDatosPrincipales.Visible = true;
                    gbDatosPrincipales.Enabled = true;
                    gbCargaDetalle.Visible = false;
                    gbDetalleGrillaDatos.Visible = false;
                    gbBotones.Visible = false;
                    break;

                //Cuando se carga el Detalle
                case estadoUI.cargaDetalle:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.cargaDetalle;
                    gbDatosPrincipales.Enabled = false;

                    //Seteamos los estados de los groupbox
                    gbDatosPrincipales.Visible = true;
                    gbCargaDetalle.Visible = true;
                    gbDetalleGrillaDatos.Visible = true;
                    gbBotones.Visible = true;
                    
                    //Seteamos los estados de los controles
                    rbUnidades.Checked = true;

                    //Seteamos las columnas que se quieren ocultar
                    dgvPlanMensual.Columns["DPMES_CODIGO"].Visible = false;
                    dgvPlanMensual.Columns["PMES_CODIGO"].Visible = false;
                    dgvPlanMensual.Columns["DPMES_CANTIDADREAL"].Visible = false;
                    dvListaPlanesMensuales.Sort = "DPMES_CODIGO ASC";

                    dgvDatos.Columns["DPSEM_CODIGO"].Visible = false;
                    dgvDatos.Columns["DIAPSEM_CODIGO"].Visible = false;
                    dgvDatos.Columns["DPSEM_ESTADO"].Visible = false;
                    dgvDatos.Columns["DPSEM_CANTIDADREAL"].Visible = false;

                    //Limpiamos el dataset
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();
                    break;

                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;

                    //Seteamos los estados de los groupbox
                    gbDatosPrincipales.Visible = true;
                    gbDatosPrincipales.Enabled = false;
                    gbCargaDetalle.Visible = true;
                    gbDetalleGrillaDatos.Visible = true;
                    gbBotones.Visible = true;
                    
                    //Seteamos los estados de los controles
                    rbUnidades.Checked = true;

                    //Seteamos las columnas que se quieren ocultar
                    dgvPlanMensual.Columns["DPMES_CODIGO"].Visible = false;
                    dgvPlanMensual.Columns["PMES_CODIGO"].Visible = false;
                    dvListaPlanesMensuales.Sort = "DPMES_CODIGO ASC";

                    dgvDatos.Columns["DPSEM_CODIGO"].Visible = false;
                    dgvDatos.Columns["DIAPSEM_CODIGO"].Visible = false;
                    dgvDatos.Columns["DPSEM_ESTADO"].Visible = false;
                    dgvDatos.Columns["DPSEM_CANTIDADREAL"].Visible = false;
                    break;

                default:
                    break;

            }
        }
        
        //Metodo que me determina que dia de la semana cae una fecha
        private string getDia(DateTime fecha)
        {
            string dia = string.Empty;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dia = "Lunes";
                    break;
                case DayOfWeek.Tuesday:
                    dia = "Martes";
                    break;
                case DayOfWeek.Wednesday:
                    dia = "Miércoles";
                    break;
                case DayOfWeek.Thursday:
                    dia = "Jueves";
                    break;
                case DayOfWeek.Friday:
                    dia = "Viernes";
                    break;
                case DayOfWeek.Saturday:
                    dia = "Sábado";
                    break;
                case DayOfWeek.Sunday:
                    dia = "Domingo";
                    break;
            }
            return dia;
        }
        //********************************************************************
        //                              VALIDACIONES
        //********************************************************************

        private string ValidarCargaDetalle()
        {
            string msjerror = string.Empty;
            int pmes =0, numeroSemana=0;

            //Validamos que esten seleccionados los combos
            if (cbPlanAnual.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar el Año del Plan Anual\n";
            if (cbMesDatos.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar el Plan Mensual\n";
            else pmes = Convert.ToInt32(cbMesDatos.GetSelectedValue());
            if (cbSemanaDatos.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar la Semana\n";
            else numeroSemana =Convert.ToInt32(cbSemanaDatos.GetSelectedText());
            
            //Valido que la semana que se quiere cargar no exista 
            if (BLL.PlanSemanalBLL.validarDetalle(pmes, numeroSemana) == false) msjerror = msjerror + "-Ya existe un plan semanal generado para esa semana del año\n";

            if (msjerror != string.Empty)
            {
                msjerror = "Los errores de Validación encontrados son:\n" + msjerror;
            }

            return msjerror; 
        }

        //Metodo que valida que se agregue un detalle
        private string ValidarDetalle()
        {
            string msjerror = string.Empty;

            int anio =Convert.ToInt32(cbPlanAnual.GetSelectedText());
            DateTime fechaDia= dtpFechaDia.Value;
            //Verifico que numero de mes es            
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int cont = 0;
            int numeroMes = 0;

            foreach (string l in Meses)
            {
                if (cbMesDatos.GetSelectedText().ToString() == Meses[cont])
                {
                    numeroMes = cont + 1;
                    break; 
                }
                cont++;
            }
            //Valido la fecha
            if (fechaDia.Year != anio) msjerror = msjerror + "-El año debe coincidir con el plan anual a producir\n";
            if (fechaDia.Month != numeroMes) msjerror = msjerror + "-El mes debe coincidir con el plan mensual a producir\n";

            //Valido que se este seleccionando algo
            if (dgvPlanMensual.Rows.GetRowCount(DataGridViewElementStates.Selected) == 0) msjerror = msjerror + "-Debe Seleccionar un detalle del plan mensual a producir\n";

            //Validamos las cantidades ingresadas
            
            if (rbUnidades.Checked == true)
            {
                if (numUnidades.Value == 0) msjerror = msjerror + "-La cantidad en unidades debe ser mayor a cero\n";
            }
            if (rbPorcentaje.Checked == true)
            {
                if (numPorcentaje.Value == 0) msjerror = msjerror + "-El porcentaje debe ser mayor a cero\n";
            }
            
            //Se valida que no se quiera agregar un modelo que ya este en el detalle
            
            int codigo = Convert.ToInt32(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["coc_codigo"]);
            int codigoPedido=0;

            if (dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dped_codigo"].ToString() != string.Empty)
            {
                codigoPedido = Convert.ToInt32(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dped_codigo"]);
            }

            foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
            {
                if (row["COC_CODIGO"].ToString() == codigo.ToString())
                {
                    if (row["DPED_CODIGO"].ToString() == codigoPedido.ToString())
                    {
                        msjerror = msjerror + "-El modelo de cocina que intenta agregar ya se encuentra en la planificación\n";
                    }
                }
            }
            //Valido que no exista un plan para el dia que selecciono}
            if (BLL.PlanSemanalBLL.validarDia(Convert.ToDateTime(dtpFechaDia.Value)) == false) msjerror = msjerror + "-El día que intenta agregar ya esta incluido en otro plan semanal\n"; ;
            
            //Verifico si hay errores y les agrego una cabecera
            if (msjerror != string.Empty)
            {
                msjerror = "Los errores de Validación encontrados son:\n" + msjerror;
            }

            return msjerror;
        }
        //Metodo que devuelve las semanas que tiene cada uno de los meses dependiendo del año
        private int[] SemanasAño(int año)
        {
            int[] semanasMes = new int[12];

            for (int i = 1; i <= 12; i++)
            {
                int ultimaSemana, primerSemana;

                //Calculo en que semana esta el primer dia
                DateTime s1 = new DateTime(año, i, 01);
                primerSemana = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(s1, CalendarWeekRule.FirstDay, s1.DayOfWeek);

                if (i < 12)
                {   //Calculo la semana del proximo mes
                    DateTime s2 = new DateTime(año, i + 1, 01);
                    ultimaSemana = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(s2, CalendarWeekRule.FirstDay, s2.DayOfWeek);
                }
                else
                {
                    //Calculo la semana del proximo mes
                    DateTime s2 = new DateTime(año, i, 31);
                    ultimaSemana = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(s2, CalendarWeekRule.FirstDay, s2.DayOfWeek);
                }
                semanasMes[i - 1] = ultimaSemana - primerSemana;
            }
            return semanasMes;
        }
        private void rbUnidades_CheckedChanged(object sender, EventArgs e)
        {
            numPorcentaje.Enabled = false;
            numUnidades.Enabled = true;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void rbPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            numPorcentaje.Enabled = true;
            numUnidades.Enabled = false;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion

        #region Pestaña Buscar
        private void cbAnio_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbAnio.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbAnio.GetSelectedValue());

                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, codigo);

                    dvComboMes = new DataView(dsPlanSemanal.PLANES_MENSUALES);
                    cbMes.SetDatos(dvComboMes, "pmes_codigo", "pmes_mes","pmes_codigo ASC", "Seleccione", false);

                    if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count > 0)
                    {
                        cbAnio.Enabled = false;
                        cbMes.Enabled = true;
                    }
                    else
                    {
                        cbAnio.Enabled = true;
                        cbMes.Enabled = false;
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }
        }

        
        private void cbMes_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbMes.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbMes.GetSelectedValue());
                    
                    BLL.PlanSemanalBLL.obtenerPS(dsPlanSemanal.PLANES_SEMANALES, codigo);

                    if (dsPlanSemanal.PLANES_SEMANALES.Rows.Count > 0)
                    {

                        dvComboSemBuscar = new DataView(dsPlanSemanal.PLANES_SEMANALES);
                        cbSemana.SetDatos(dvComboSemBuscar, "psem_codigo", "psem_semana", "Seleccione", false);
                        cbSemana.Enabled = true;
                        cbMes.Enabled = false;
                    }
                    else
                    {
                        cbSemana.Enabled = false;
                        Entidades.Mensajes.MensajesABM.MsjValidacion("No hay planes Semanales creados para ese mes", this.Text);
                        SetInterface(estadoUI.inicio);
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }       

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();

                //Obtengo el Codigo del plan Semanal
                int codigo =Convert.ToInt32(cbSemana.GetSelectedValue());

                //Se busca el Plan Semanal
                BLL.DiasPlanSemanalBLL.obtenerDias(dsPlanSemanal.DIAS_PLAN_SEMANAL, codigo);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanes.Table = dsPlanSemanal.DIAS_PLAN_SEMANAL;

                if (dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Dias Planificados", this.Text);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }    

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();

                int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["diapsem_codigo"]);

                //Se llama a la funcion de busqueda del detalle
                BLL.DetallePlanSemanalBLL.ObtenerDetalle(codigo, dsPlanSemanal.DETALLE_PLANES_SEMANALES);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsPlanSemanal.DETALLE_PLANES_SEMANALES;

                if (dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Detalles de Plan Semanal", this.Text);                    
                }
                else
                {
                    //muestro el groupbox del detalle
                    SetInterface(estadoUI.buscar);
                    gbGrillaDetalle.Visible = true;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }
#endregion

        #region Controles

        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {            
            if (e.Value != null)
            {
                switch (dgvDetalle.Columns[e.ColumnIndex].Name)
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
        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvLista.Columns[e.ColumnIndex].Name)
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
        private void dgvDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvDatos.Columns[e.ColumnIndex].Name)
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

        private void dgvPlanMensual_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvPlanMensual.Columns[e.ColumnIndex].Name)
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

        //**********************************************************************************************
        //                                          COMBO DATOS
        //**********************************************************************************************

        private void cbPlanAnual_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbPlanAnual.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbPlanAnual.GetSelectedValue());

                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, codigo);

                    dvComboMesDatos = new DataView(dsPlanSemanal.PLANES_MENSUALES);
                    cbMesDatos.SetDatos(dvComboMesDatos, "pmes_codigo", "pmes_mes", "pmes_codigo ASC", "Seleccione", false);

                    if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count > 0)
                    {
                        cbPlanAnual.Enabled = false;
                        cbMesDatos.Enabled = true;
                    }
                    else
                    {
                        cbPlanAnual.Enabled = true;
                        cbMesDatos.Enabled = false;
                    }
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void cbMesDatos_DropDownClosed(object sender, EventArgs e)
        {
            //Se busca el mes y el año que quedaron seleccionados
            int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());
            string mes = cbMesDatos.GetSelectedText().ToString();
            int numeroMes = -1;

            //Verifico que numero de mes es            
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int cont = 0;

            foreach (string l in Meses)
            {
                if (cbMesDatos.GetSelectedText().ToString() == Meses[cont])
                {
                    numeroMes = cont;
                    break;
                }
                cont++;
            }

            //Calculo las semanas que tine cada mes en el año
            int[] SemanasXMes = new int[12];
            SemanasXMes = SemanasAño(anio);

            //Sumo las semanas hasta el contador
            int semanasAcum = 0;
            for (int i = 0; i < cont; i++)
            {
                semanasAcum = semanasAcum + SemanasXMes[i];
            }

            //Defino la cantidad de semanas que tiene el mes
            int cantidadSemanas = SemanasXMes[numeroMes];

            //Creo las estructuras para cargar el combo
            int[] valoresSemanas = new int[cantidadSemanas];
            string[] textoSemanas = new string[cantidadSemanas];

            for (int i = 0; i < cantidadSemanas; i++)
            {
                valoresSemanas[i] = (semanasAcum + i + 1);
                textoSemanas[i] = (semanasAcum + i + 1).ToString();
            }
            //Deshabilito el Combo de Meses
            cbMesDatos.Enabled = false;

            //Cargo el combo con las semanas
            cbSemanaDatos.SetDatos(textoSemanas, valoresSemanas, "Seleccione", false);
            cbSemanaDatos.Enabled = true;
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

        private void numUnidades_Enter(object sender, EventArgs e)
        {
            numUnidades.Select(0, 10);
        }

        private void numPorcentaje_Enter(object sender, EventArgs e)
        {
            numPorcentaje.Select(0, 10);
        }
        #endregion

        #region Pestaña Datos

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);           
        }
        
        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            string validacion = ValidarCargaDetalle();
            try
            {
                //Se pone el plan Mensual en cero
                codigoPlanSemanal = 0;

                if (validacion == string.Empty)
                {
                    //Se debe cargar la grilla de datos del plan mensual
                    int codigoAnio = Convert.ToInt32(cbPlanAnual.GetSelectedValue());
                    int codigoPlanMensual = Convert.ToInt32(cbMesDatos.GetSelectedValue());

                    int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());
                    string mes = cbMesDatos.GetSelectedText().ToString();

                    //Limpio el datatable
                    dsPlanSemanal.DETALLE_PLANES_MENSUALES.Clear();

                    //Lleno el dataset
                    BLL.DetallePlanMensualBLL.ObtenerDetallePM(codigoAnio, mes, dsPlanSemanal.DETALLE_PLANES_MENSUALES);

                    //Lleno los controles a utilizar
                    if (dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows.Count > 0)
                    {
                        //CANTIDAD SEMANAS
                        //Verifico que numero de mes es
                        string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        int cont = 0;

                        foreach (string l in Meses)
                        {
                            if (cbMesDatos.GetSelectedText().ToString() == Meses[cont])
                            {
                                break;
                            }
                            cont++;
                        }
                        
                        //Le asigno el valor a la caja de texto y la bloqueo}
                        txtSemana.Text = cbSemanaDatos.GetSelectedText().ToString();
                        txtSemana.Enabled = false;

                        //Pongo el calendario en fecha
                        dtpFechaDia.Value =Convert.ToDateTime("01/" + (cont + 1).ToString() + "/" + anio.ToString());

                        //Se calcula la cantidad ya planificada para cada cocina
                        foreach (Data.dsPlanSemanal.DETALLE_PLANES_MENSUALESRow row in dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows)
                        {
                            int? cantidadPlanificada = null;
                            
                            //busco el codigo del modelo de cocina
                            int codigoCocina =Convert.ToInt32(row.COC_CODIGO);
                            if (row["DPED_CODIGO"].ToString() == string.Empty)
                            {
                                //Busco en la base de datos la cantidad
                                cantidadPlanificada = BLL.PlanSemanalBLL.obtenerCocinasPlanificadas(codigoCocina, codigoAnio, codigoPlanMensual);
                            }
                            else
                            {
                                cantidadPlanificada = BLL.PlanSemanalBLL.obtenerCocinasPlanificadas(codigoCocina, codigoAnio, codigoPlanMensual,Convert.ToInt32(row.DPED_CODIGO));
                            }
                            
                            if (cantidadPlanificada == null) cantidadPlanificada = 0;

                            row.BeginEdit();
                            row.DPMES_CANTPLANIFICADA =Convert.ToDecimal(cantidadPlanificada);
                            row.EndEdit();
                        }

                        //Seteo el estado de la interface
                        SetInterface(estadoUI.cargaDetalle);
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);                    
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);                
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string validacion =ValidarDetalle();
                int cantidad = 0;

                if (validacion == string.Empty)
                {
                    int codigoDia = -1;

                    //Empiezo con la edición del Dataset
                    Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row = dsPlanSemanal.DETALLE_PLANES_SEMANALES.NewDETALLE_PLANES_SEMANALESRow();
                    row.BeginEdit();
                    row.DPSEM_CODIGO = codigoDetalle--;
                    row.DIAPSEM_CODIGO = codigoDia;
                    row.COC_CODIGO = Convert.ToInt32(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["coc_codigo"]);

                    if (estadoActual == estadoUI.cargaDetalle)
                    {
                        //Le pongo estado 0 que significa que no tiene ordenes de trabajo
                        row.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoGenerado;
                    }
                    else if (estadoActual == estadoUI.modificar)
                    {
                        //Le pongo estado 0 que significa que no tiene ordenes de trabajo
                        row.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoModificado;
                    }
                    if (rbUnidades.Checked == true)
                    {
                        cantidad = Convert.ToInt32(numUnidades.Value);
                        row.DPSEM_CANTIDADESTIMADA = cantidad;
                    }
                    else
                    {
                        int cantidadMes = Convert.ToInt32(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dpmes_cantidadestimada"]);
                        cantidad = Convert.ToInt32(cantidadMes * (numPorcentaje.Value / 100));
                        row.DPSEM_CANTIDADESTIMADA = cantidad;

                    }
                    //Si tiene un pedido lo agrego sino pongo 0
                    if (dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dped_codigo"].ToString() != string.Empty)
                    {
                        row.DPED_CODIGO = Convert.ToInt32(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dped_codigo"]);
                    }
                    else
                    {
                        row.DPED_CODIGO = 0;
                    }
                    row.EndEdit();
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.AddDETALLE_PLANES_SEMANALESRow(row);

                    //Agrego el valor al dataset de planes mensuales
                    int codigoCocinaAgregada = Convert.ToInt32(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["coc_codigo"]);
                    string pedido = string.Empty;

                    if (dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dped_codigo"].ToString() != string.Empty)
                    {
                        pedido = Convert.ToString(dvListaPlanesMensuales[dgvPlanMensual.SelectedRows[0].Index]["dped_codigo"]);

                    }
                        //Se calcula la cantidad ya planificada para cada cocina
                        foreach (Data.dsPlanSemanal.DETALLE_PLANES_MENSUALESRow rows in dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows)
                        {
                            //busco el codigo del modelo de cocina
                            int codigoCocina = Convert.ToInt32(rows.COC_CODIGO);

                            if (codigoCocina == codigoCocinaAgregada)
                            {
                                if (pedido == rows["DPED_CODIGO"].ToString())
                                {
                                    rows.BeginEdit();
                                    rows.DPMES_CANTPLANIFICADA += cantidad;
                                    rows.EndEdit();
                                }
                            }
                        }                   

                    //Seteo los valores de los controles en cero
                    rbUnidades.Checked = true;
                    numPorcentaje.Value = 0;
                    numUnidades.Value = 0;
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);                    
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);                
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            //Obtengo la cantidad a producir en el mes
            int cocina = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["coc_codigo"]);
            int codigoPedido = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]);
            DataRow[] fila;
            if (codigoPedido != 0)
            {
                fila = dsPlanSemanal.DETALLE_PLANES_MENSUALES.Select("coc_codigo=" + cocina.ToString() + "and dped_codigo =" + codigoPedido.ToString());
            }
            else
            {
                fila = dsPlanSemanal.DETALLE_PLANES_MENSUALES.Select("coc_codigo=" + cocina.ToString());
            }
            int cantidadMes = fila[0].Field<int>("dpmes_cantidadestimada");
            int? cantidadReal = fila[0].Field<int?>("dpmes_cantidadreal");
            if (cantidadReal == null) cantidadReal = 0;

            //Obtengo la cantidad que ya se quiere producir
            int cantidadPlanificada = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpsem_cantidadestimada"]);

            //Controlo que no se planifique mas de lo que dice el plan semanal
            if (cantidadPlanificada + 1 <= (cantidadMes - cantidadReal))
            {
                if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    //Agrego el valor al dataset de planes mensuales
                    int codigoCocinaAgregada = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["coc_codigo"]);
                    string pedido = string.Empty;

                    if (dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"].ToString() != string.Empty)
                    {
                        pedido = Convert.ToString(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]);
                    }

                    //Se calcula la cantidad ya planificada para cada cocina
                    foreach (Data.dsPlanSemanal.DETALLE_PLANES_MENSUALESRow rows in dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows)
                    {
                        //busco el codigo del modelo de cocina
                        int codigoCocina = Convert.ToInt32(rows.COC_CODIGO);

                        if (codigoCocina == codigoCocinaAgregada)
                        {
                            if (pedido != "0" && pedido == rows["DPED_CODIGO"].ToString())
                            {
                                rows.BeginEdit();
                                rows.DPMES_CANTPLANIFICADA += 1;
                                rows.EndEdit();
                            }
                            else
                            {
                                rows.BeginEdit();
                                rows.DPMES_CANTPLANIFICADA += 1;
                                rows.EndEdit();
                            }
                        }
                    }

                    int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpsem_codigo"]);
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigo).DPSEM_CANTIDADESTIMADA += 1;
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Modelo de Cocina", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                    
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("La cantidad a planificar en la semana no puede ser mayor que la de todo el mes", this.Text);                
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            //Obtengo la cantidad que ya se quiere producir
            int cantidadPlanificada = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpsem_cantidadestimada"]);

            //Controlo que no se planifique mas de lo que dice el plan semanal
            if (cantidadPlanificada -1 > 0)
            {
                if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    //Agrego el valor al dataset de planes mensuales
                    int codigoCocinaAgregada = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["coc_codigo"]);
                    string pedido = string.Empty;

                    if (dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"].ToString() != string.Empty)
                    {
                        pedido = Convert.ToString(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]);
                    }

                    //Se calcula la cantidad ya planificada para cada cocina
                    foreach (Data.dsPlanSemanal.DETALLE_PLANES_MENSUALESRow rows in dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows)
                    {
                        //busco el codigo del modelo de cocina
                        int codigoCocina = Convert.ToInt32(rows.COC_CODIGO);

                        if (codigoCocina == codigoCocinaAgregada)
                        {
                            if (pedido != "0" && pedido == rows["DPED_CODIGO"].ToString())
                            {
                                rows.BeginEdit();
                                rows.DPMES_CANTPLANIFICADA -= 1;
                                rows.EndEdit();
                            }
                            else
                            {
                                rows.BeginEdit();
                                rows.DPMES_CANTPLANIFICADA -= 1;
                                rows.EndEdit();
                            }
                        }
                    }

                    int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpsem_codigo"]);
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigo).DPSEM_CANTIDADESTIMADA -= 1;
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Modelo de Cocina", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                    
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("La cantidad a planificar en la semana no puede ser menor que cero", this.Text);                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpsem_codigo"]);
                int codigoCocina = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["coc_codigo"]);
                int cantidad = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpsem_cantidadestimada"]);

                    //Se calcula la cantidad ya planificada para cada cocina
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_MENSUALESRow rows in dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows)
                {
                    if (rows.COC_CODIGO == codigoCocina)
                    {
                        rows.BeginEdit();
                        rows.DPMES_CANTPLANIFICADA -= cantidad;
                        rows.EndEdit();
                    }
                }

                //Elimino el dataset
                dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigo).Delete();
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Modelo de Cocina", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valido que el detalle del plan no sea nulo
                if (dsPlanSemanal.DETALLE_PLANES_SEMANALES.Count > 0)
                {

                    //Checkeo las excepciones relacionadas con el Plan Semanal
                    List<Entidades.ExcepcionesPlan> excepciones = new List<GyCAP.Entidades.ExcepcionesPlan>();
                    excepciones = BLL.PlanSemanalBLL.CheckeoExcepciones(dsPlanSemanal.DETALLE_PLANES_SEMANALES);

                    if (excepciones.Count == 0 || checkeoExcepciones == true)
                    {
                        //Creamos el objeto del plan mensual para insertar en el plan mensual
                        Entidades.PlanMensual planMensual = new GyCAP.Entidades.PlanMensual();
                        planMensual.Codigo = Convert.ToInt32(cbMesDatos.GetSelectedValue());

                        //Creamos el objeto del Plan semanal
                        Entidades.PlanSemanal planSemanal = new GyCAP.Entidades.PlanSemanal();
                        planSemanal.FechaCreacion = BLL.DBBLL.GetFechaServidor();
                        planSemanal.PlanMensual = planMensual;
                        planSemanal.Semana = Convert.ToInt32(cbSemanaDatos.GetSelectedText());
                        planSemanal.Codigo = codigoPlanSemanal;

                        //Creamos el objeto del dia del Plan Semanal
                        Entidades.DiasPlanSemanal diaPlan = new GyCAP.Entidades.DiasPlanSemanal();
                        diaPlan.Dia = getDia(dtpFechaDia.Value);
                        diaPlan.Fecha = dtpFechaDia.Value;
                        diaPlan.PlanSemanal = planSemanal;

                        if (estadoActual == estadoUI.cargaDetalle)
                        {
                            if (esPrimero == true)
                            {
                                //Llamamos al método que realiza el guardado de los datos en la clase Plan Semanal
                                codigoPlanSemanal = BLL.PlanSemanalBLL.InsertarPlanSemanal(planSemanal, diaPlan, dsPlanSemanal, esPrimero);
                            }
                            else
                            {
                                int guardaCodigo = codigoPlanSemanal;
                                codigoPlanSemanal = BLL.PlanSemanalBLL.InsertarPlanSemanal(planSemanal, diaPlan, dsPlanSemanal, esPrimero);
                                codigoPlanSemanal = guardaCodigo;
                            }
                        }
                        else if (estadoActual == estadoUI.modificar)
                        {
                            //Llamamos al método que realiza el guardado de los datos en la clase Plan Semanal
                            BLL.PlanSemanalBLL.ModificarPlanSemanal(planSemanal, diaPlan, dsPlanSemanal);
                        }

                        //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                        dsPlanSemanal.AcceptChanges();

                        //Verificamos si esta modificando
                        if (estadoActual == estadoUI.modificar)
                        {
                            Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Día de Plan Semanal", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                            SetInterface(estadoUI.inicio);
                        }
                        else if (estadoActual == estadoUI.cargaDetalle)
                        {
                            //Verifico si quiere continuar cargando
                            Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Día de Plan Semanal", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                            DialogResult respuesta = MessageBox.Show("¿Desea continuar cargando el siguiente día de la semana?", "Pregunta: Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (respuesta == DialogResult.Yes)
                            {
                                esPrimero = false;
                                
                                //Agrego un dia al control que maneja el dia planificado
                                dtpFechaDia.Value = dtpFechaDia.Value.AddDays(1);
                                dtpFechaDia.Focus();

                                //Pongo el valor en False para que vuelva a calcular las excepciones
                                checkeoExcepciones = false;

                                SetInterface(estadoUI.cargaDetalle);
                            }
                            else
                            {
                                SetInterface(estadoUI.inicio);
                            }
                        }
                    }
                    else
                    {
                        //Si existen excepciones relacionadas con el Plan Semanal
                        PlanificacionProduccion.frmExcepcionesPlan frmExcepciones = new frmExcepcionesPlan();
                        frmExcepciones.TopLevel = false;
                        frmExcepciones.Parent = this.Parent;
                        frmExcepciones.CargarGrilla(excepciones);
                        frmExcepciones.Show();
                        frmExcepciones.BringToFront();

                        //Cambio el valor de checkeo excepciones a TRUE para que pase una vez
                        checkeoExcepciones = true;
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion("No se puede guardar un plan semanal sin que tenga detalle", this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            catch (Entidades.Excepciones.CocinaSinEstructuraActivaException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            catch (Entidades.Excepciones.EstructuraSinMateriaPrimaException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    //Obtenemos los códigos
                    int codigoPlanAnual = Convert.ToInt32(cbAnio.GetSelectedValue());
                    int codigoPlanMensual = Convert.ToInt32(cbMes.GetSelectedValue());
                    int codigoSemana = Convert.ToInt32(cbSemana.GetSelectedText());

                    //Limpiamos los combobox y los seteamos 
                    cbPlanAnual.SetSelectedIndex(-1);
                    cbMesDatos.SetSelectedIndex(-1);
                    cbSemanaDatos.SetSelectedIndex(-1);

                    //Limpiamos los combos
                    cbMesDatos.DataSource = null;
                    dsPlanSemanal.PLANES_MENSUALES.Clear();
                    cbMesDatos.Items.Clear();

                    cbSemanaDatos.DataSource = null;
                    dsPlanSemanal.PLANES_SEMANALES.Clear();
                    cbSemanaDatos.Items.Clear();
                    
                    //Se los seteamos a cada uno de los controles
                    EventArgs ev = new EventArgs();
                    cbPlanAnual.SetSelectedValue(codigoPlanAnual);
                    cbPlanAnual_DropDownClosed(cbPlanAnual, ev);
                    cbMesDatos.SetSelectedValue(codigoPlanMensual);
                    cbMesDatos_DropDownClosed(cbMesDatos, ev);
                    cbSemanaDatos.SetSelectedValue(codigoSemana);            

                    //Se cargan los otros controles
                    //Se debe cargar la grilla de datos del plan mensual
                    int codigoAnio = Convert.ToInt32(cbPlanAnual.GetSelectedValue());
                    int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());
                    string mes = cbMesDatos.GetSelectedText().ToString();

                    //Limpio el datatable
                    dsPlanSemanal.DETALLE_PLANES_MENSUALES.Clear();

                    //Lleno el dataset
                    BLL.DetallePlanMensualBLL.ObtenerDetallePM(codigoAnio, mes, dsPlanSemanal.DETALLE_PLANES_MENSUALES);

                    //Lleno los controles a utilizar
                    if (dsPlanSemanal.DETALLE_PLANES_MENSUALES.Rows.Count > 0)
                    {
                        //Le asigno el valor a la caja de texto y la bloqueo}
                        txtSemana.Text = cbSemanaDatos.GetSelectedText().ToString();
                        txtSemana.Enabled = false;

                        //Pongo el calendario en fecha
                        DateTime fecha = Convert.ToDateTime(dvListaPlanes[dgvLista.SelectedRows[0].Index]["diapsem_fecha"]);
                        dtpFechaDia.Value = fecha;
                    }

                    SetInterface(estadoUI.modificar);
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Día de Plan Semanal", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                    
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Plan Semanal y su Detalle", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtengo el Codigo del plan Semanal
                        int codigoPlanSemanal =Convert.ToInt32(cbSemana.GetSelectedValue());
                        
                       //Elimino el plan anual y su detalle de la BD
                        BLL.PlanSemanalBLL.EliminarPlan(codigoPlanSemanal, dsPlanSemanal);

                       //Limpio el dataset de detalles
                       dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();

                       //Limpio el Dataset de Dias
                       dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();

                       //Lo eliminamos del dataset
                       dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(codigoPlanSemanal).Delete();
                       dsPlanSemanal.PLANES_SEMANALES.AcceptChanges();

                       //Avisamos que se elimino 
                       Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                       
                       //Ponemos la ventana en el estado inicial
                       SetInterface(estadoUI.inicio);
                                             
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);                        
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);                        
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Designación", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);                
            }            
        }       

        #endregion
    }
}
