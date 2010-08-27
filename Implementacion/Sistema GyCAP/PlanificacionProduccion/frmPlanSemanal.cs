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
        private DataView dvComboPlanesAnuales, dvComboCocinas, dvComboAnio, dvComboMes, dvComboSemBuscar;
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
            //Lista de Planes Semanales
            //Agregamos la columnas
            dgvLista.Columns.Add("PSEM_CODIGO", "Código");
            dgvLista.Columns.Add("PMES_CODIGO", "Mes");
            dgvLista.Columns.Add("PSEM_SEMANA", "N° Semana");
            dgvLista.Columns.Add("PSEM_FECHACREACION", "Fecha Creación Plan Semanal");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvLista.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvLista.Columns["PSEM_SEMANA"].DataPropertyName = "PSEM_SEMANA";
            dgvLista.Columns["PSEM_FECHACREACION"].DataPropertyName = "PSEM_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanSemanal.PLANES_SEMANALES);
            dgvLista.DataSource = dvListaPlanes;
            
            //Lista de Detalles de Planes Semanales
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPSEM_CODIGO", "Código");
            dgvDetalle.Columns.Add("PSEM_CODIGO", "Semana");
            dgvDetalle.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetalle.Columns.Add("DPSEM_DIA", "Semana");
            dgvDetalle.Columns.Add("DPSEM_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDetalle.Columns.Add("DPSEM_CANTIDADREAL", "Cantidad Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPSEM_CODIGO"].DataPropertyName = "DPSEM_CODIGO";
            dgvDetalle.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalle.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvDetalle.Columns["DPSEM_DIA"].DataPropertyName = "DPSEM_DIA";
            dgvDetalle.Columns["DPSEM_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDetalle.Columns["DPSEM_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanSemanal.DETALLE_PLANES_SEMANALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //GRILLAS DE DATOS
            //Lista de Planes Mensuales
            //Agregamos la columnas
            dgvPlanMensual.Columns.Add("DPMES_CODIGO", "Código");
            dgvPlanMensual.Columns.Add("PMES_CODIGO", "Mes");
            dgvPlanMensual.Columns.Add("COC_CODIGO", "Cocina Codigo");
            dgvPlanMensual.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvPlanMensual.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvPlanMensual.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvPlanMensual.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvPlanMensual.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvPlanMensual.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvPlanMensual.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvPlanMensual.Columns[0].Visible = false;
            dgvPlanMensual.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlanMensual.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

            //Cargamos el combo
            dvComboPlanesAnuales = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbPlanAnual.SetDatos(dvComboPlanesAnuales, "pan_codigo", "pan_anio", "Seleccione", false);

            dvComboAnio = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbAnio.SetDatos(dvComboAnio, "pan_codigo", "pan_anio", "Seleccione", false);

            //Setemoa el valor de la interface
            SetInterface(estadoUI.inicio);

        }

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
                    cbMes.Enabled = false;
                    cbSemana.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    cbPlanAnual.SetSelectedIndex(-1);
                    cbMes.SetSelectedIndex(-1);
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.inicio;
                    break;

                //Cuando termina de Buscar
                case estadoUI.buscar:
                    
                    break;
                //Cuando se carga el Detalle
                case estadoUI.cargaDetalle:
                   
                    break;

                case estadoUI.modificar:
                    
                    break;

                case estadoUI.nuevo:
                   
                    break;


                default:
                    break;

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

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

                    if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count > 0) cbMes.Enabled = true;
                    else cbMes.Enabled = false;

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
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
                    }
                    else
                    {
                        cbSemana.Enabled = false;
                        MessageBox.Show("-No hay planes Semanales creados para ese mes", "Información: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }

        }

        private void cargarDetalle()
        {
            int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());

            //Se traen las semanas que tiene cada mes en ese año
            int[] semanasMes = new int[12];
            semanasMes = SemanasAño(anio);

            //Se busca el mes (su posición en el array)
            int cont = 0;
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            foreach (string mes in Meses)
            {
                if (mes == cbMes.GetSelectedText().ToString())
                {
                    break;
                }
                else cont += 1;
            }
        }
    }
}
