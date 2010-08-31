using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmPlanSemanal : Form
    {
        private static frmPlanSemanal _frmPlanSemanal = null;
        private Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        private DataView dvListaPlanes, dvListaDetalle, dvListaDatos, dvListaPlanesMensuales;
        private DataView dvComboPlanesAnuales, dvComboMes, dvComboSemBuscar;
        private DataView dvComboAnio, dvComboMesDatos, dvComboSemanaDatos;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;


        public frmPlanSemanal()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;
            dgvDatos.AutoGenerateColumns = false;
            dgvPlanMensual.AutoGenerateColumns = false;

            //Para cada Lista
            //LISTAS DE BUSQUEDA
            //Lista de los Dias de los Planes Semanales
            //Agregamos la columnas
            dgvLista.Columns.Add("DIAPSEM_CODIGO", "Código");
            dgvLista.Columns.Add("PSEM_CODIGO", "N° Semana");
            dgvLista.Columns.Add("DIAPSEM_DIA", "Dia");
            dgvLista.Columns.Add("DIAPSEM_FECHA", "Fecha");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvLista.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvLista.Columns["DIAPSEM_DIA"].DataPropertyName = "DIAPSEM_DIA";
            dgvLista.Columns["DIAPSEM_FECHA"].DataPropertyName = "DIAPSEM_FECHA";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanSemanal.DIAS_PLAN_SEMANAL);
            dgvLista.DataSource = dvListaPlanes;
            
            //Lista de Detalles de Planes Semanales
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDetalle.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetalle.Columns.Add("DIAPSEM_CODIGO", "Código Dia");
            dgvDetalle.Columns.Add("DPSEM_CANTIDADESTIMADA", "C.Estimada");
            dgvDetalle.Columns.Add("DPSEM_CANTIDADREAL", "C.Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDetalle.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDetalle.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalle.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDetalle.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanSemanal.DETALLE_PLANES_SEMANALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //GRILLAS DE DATOS
            //Lista de Planes Mensuales
            //Agregamos la columnas
            dgvPlanMensual.Columns.Add("DPMES_CODIGO", "Código");
            dgvPlanMensual.Columns.Add("PMES_CODIGO", "Mes");
            dgvPlanMensual.Columns.Add("COC_CODIGO", "Cocina Codigo");
            dgvPlanMensual.Columns.Add("DPMES_CANTIDADESTIMADA", "C.Estimada");
            dgvPlanMensual.Columns.Add("DPMES_CANTIDADREAL", "C.Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvPlanMensual.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvPlanMensual.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvPlanMensual.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvPlanMensual.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvPlanMensual.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvPlanMensual.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanesMensuales = new DataView(dsPlanSemanal.DETALLE_PLANES_MENSUALES);
            dgvPlanMensual.DataSource = dvListaPlanesMensuales;

            //Lista de Detalle del Plan Semanal
            //Agregamos la columnas
            dgvDatos.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDatos.Columns.Add("PSEM_CODIGO", "Semana");
            dgvDatos.Columns.Add("COC_CODIGO", "Cocina");
            dgvDatos.Columns.Add("DPSEM_DIA", "Dia Semana");
            dgvDatos.Columns.Add("DPSEM_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDatos.Columns.Add("DPSEM_CANTIDADREAL", "Cantidad Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatos.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDatos.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvDatos.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDatos.Columns["DPSEM_DIA"].DataPropertyName = "DPSEM_DIA";
            dgvDatos.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPSEM_CANTIDADESTIMADA";
            dgvDatos.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPSEM_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvDatos.Columns[0].Visible = false;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

            //Setemoa el valor de la interface
            SetInterface(estadoUI.inicio);

        }
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
                    gbGrillaDemanda.Visible = false;
                    gbGrillaDetalle.Visible = false;
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
                    gbGrillaDemanda.Visible = hayDatos;
                    gbGrillaDetalle.Visible = false;
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    //Columnas de las grillas
                    //Ponemos las columnas de las grillas en visible false
                    dgvLista.Columns["DIAPSEM_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DIAPSEM_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPSEM_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPSEM_CANTIDADREAL"].Visible = false;
                    break;
                //Cuando se presiona el boton nuevo
                case estadoUI.nuevo:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.inicio;
                    //Ponemos los combo en su estado
                    cbPlanAnual.Enabled = true;
                    cbMesDatos.Enabled = false;
                    cbSemanaDatos.Enabled = false;
                    //Seteamos los estados de los groupbox
                    gbDatosPrincipales.Visible = true;
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
                    estadoActual = estadoUI.inicio;
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
                    dvListaPlanesMensuales.Sort = "DPMES_CODIGO ASC";
                    break;

                case estadoUI.modificar:
                    
                    break;



                default:
                    break;

            }
        }
        private string ValidarCargaDetalle()
        {
            string msjerror = string.Empty;

            //Validamos que esten seleccionados los combos
            if (cbPlanAnual.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar el Año del Plan Anual\n";
            if (cbMesDatos.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar el Plan Mensual\n";
            if (cbSemanaDatos.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar la Semana\n";
        


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
            numPorcentaje.Visible = false;
            numUnidades.Visible = true;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void rbPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            numPorcentaje.Visible = true;
            numUnidades.Visible = false;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }
        #endregion

        #region PestañaBuscar
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
                    cbMes.SetDatos(dvComboMes, "pmes_codigo", "pmes_mes", "Seleccione", false);

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
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
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
                        MessageBox.Show("-No hay planes Semanales creados para ese mes", "Información: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetInterface(estadoUI.inicio);
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("No se encontraron Dias para la semama ingresada.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("No se encontraron Detalles para ese día del plan semanal.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }



        }
#endregion

        #region FormatoGrillas
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
        #endregion

        #region Botones
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
        #endregion

        #region Combos Datos

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
                    cbMesDatos.SetDatos(dvComboMesDatos, "pmes_codigo", "pmes_mes", "Seleccione", false);

                    if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count > 0)
                    {
                        cbPlanAnual.Enabled = false;
                        cbMesDatos.Enabled = true;
                    }
                    else
                    {
                        cbPlanAnual .Enabled = true;
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
            int numeroMes=-1;

            //Verifico que numero de mes es
            
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int cont = 0; int[] valores = new int[12];

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
                valoresSemanas[i] = i;
                textoSemanas[i] = (semanasAcum + i + 1).ToString();
            }

            //Cargo el combo con las semanas
            cbSemanaDatos.SetDatos(textoSemanas, valoresSemanas, "Seleccione", false);
            cbSemanaDatos.Enabled = true;

        }
        #endregion

        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            string validacion = ValidarCargaDetalle();
            try
            {
                if (validacion == string.Empty)
                {
                    //Se debe cargar la grilla de datos del plan mensual
                    int codigoAnio = Convert.ToInt32(cbPlanAnual.GetSelectedValue());
                    int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());
                    string mes = cbMesDatos.GetSelectedText().ToString();

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
                            if (l == Meses[cont])
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

                        //Seteo el estado de la interface
                        SetInterface(estadoUI.cargaDetalle);
                    }
                }
                else
                {
                    MessageBox.Show(validacion, "Error: Plan Semanal - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Carga de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        

        

    }
}
