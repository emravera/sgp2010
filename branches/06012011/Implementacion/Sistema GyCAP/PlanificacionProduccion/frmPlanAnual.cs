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
    public partial class frmPlanAnual : Form
    {
        private static frmPlanAnual _frmPlanAnual = null;
        private Data.dsPlanAnual dsPlanAnual = new GyCAP.Data.dsPlanAnual();
        private DataView dvListaPlanes, dvListaDetalle, dvChAnios, dvComboEstimaciones;
        private enum estadoUI { inicio, nuevo, buscar, modificar, calcularPlanificacion };
        private static estadoUI estadoActual;
        private static decimal seriesGraficos, totalsistema = 0, totalActual;

        #region Inicio

        public frmPlanAnual()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;

            //Para cada Lista
            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("PAN_CODIGO", "Código");
            dgvLista.Columns.Add("PAN_ANIO", "Año Plan");
            dgvLista.Columns.Add("DEMAN_CODIGO", "Demanda");
            dgvLista.Columns.Add("PAN_FECHACREACION", "Fecha Creación");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PAN_CODIGO"].DataPropertyName = "PAN_CODIGO";
            dgvLista.Columns["PAN_ANIO"].DataPropertyName = "PAN_ANIO";
            dgvLista.Columns["DEMAN_CODIGO"].DataPropertyName = "DEMAN_CODIGO";
            dgvLista.Columns["PAN_FECHACREACION"].DataPropertyName = "PAN_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanAnual.PLANES_ANUALES);
            dvListaPlanes.Sort = "PAN_ANIO ASC";
            dgvLista.DataSource = dvListaPlanes;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPAN_CODIGO", "Código");
            dgvDetalle.Columns.Add("DPAN_MES", "Mes");
            dgvDetalle.Columns.Add("DPAN_CANTIDADMES", "Cantidad Mes");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPAN_CODIGO"].DataPropertyName = "DPAN_CODIGO";
            dgvDetalle.Columns["DPAN_MES"].DataPropertyName = "DPAN_MES";
            dgvDetalle.Columns["DPAN_CANTIDADMES"].DataPropertyName = "DPAN_CANTIDADMES";
            
            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanAnual.DETALLE_PLAN_ANUAL);
            dgvDetalle.DataSource = dvListaDetalle;            

            //Llenamos el dataset con las estimaciones
            BLL.DemandaAnualBLL.ObtenerTodos(dsPlanAnual);
                        
            //Cargamos el combo
            dvComboEstimaciones = new DataView(dsPlanAnual.DEMANDAS_ANUALES);
            string[] display = { "deman_anio" , "deman_nombre" };
            cbEstimacionDemanda.SetDatos(dvComboEstimaciones, "deman_codigo", display,"-", "Seleccione", false);
            
            //Seteamos los maxlength de los controles y los tipos de numeros
            txtAnio.MaxLength = 4;
            txtAnioBuscar.MaxLength = 4;

            //Configuracion de los numeric
            numAdelantamiento.Increment = 1;
            numCapacidadProducción.Increment = 1;
            numCapacidadStock.Increment = 1;
            numCostofijo.Increment =Convert.ToDecimal(0.01);
            numCostofijo.DecimalPlaces = 2;
            numCostoVariable.Increment =Convert.ToDecimal(0.01);
            numCostoVariable.DecimalPlaces = 2;
            numPrecioVenta.Increment =Convert.ToDecimal(0.01);
            numPrecioVenta.DecimalPlaces = 2;

            //Configuracion de limites
            numAdelantamiento.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "52", NumericLimitValues.IncludeExclude.Inclusivo);
            numCapacidadProducción.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);
            numCapacidadStock.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);
            numCostofijo.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);
            numCostoVariable.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);
            numPrecioVenta.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);

            //Limites de cantidades en los meses
            numEnero.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numFebrero.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numMarzo.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numAbril.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numMayo.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numJunio.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numJulio.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numAgosto.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numSeptiembre.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numOctubre.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numNoviembre.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            numDiciembre.Tag = new Sistema.Validaciones.NumericLimitValues("0", "5000000");
            
            //Seteamos el estado de la interface
            SetInterface(estadoUI.inicio);
        }
        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                //Cuando Arranca la pantalla
                case estadoUI.inicio:
                    txtAnioBuscar.Text = string.Empty;
                    gbGrillaDemanda.Visible = true;
                    gbGrillaDetalle.Visible = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnPlanificar.Enabled = false;

                    //Ponemos las columnas en visible false
                    dgvLista.Columns["PAN_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPAN_CODIGO"].Visible = false;

                    //Limpiamos los datatables de la busqueda
                    dsPlanAnual.PLANES_ANUALES.Clear();
                    dsPlanAnual.DETALLE_PLAN_ANUAL.Clear();

                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.inicio;
                    break;

                //Cuando termina de Buscar
                case estadoUI.buscar:
                    btnNuevo.Enabled = true;
                    bool hayDatos;
                    if (dsPlanAnual.DEMANDAS_ANUALES.Rows.Count > 0)
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

                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    txtTotal.Enabled = false;
                    numAdelantamiento.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnPlanificar.Enabled = false;
                    chListAnios.Visible = false;

                    gbModificacion.Visible = false;
                    gbGraficoEstimacion.Visible = true;
                    gbEstimacionMes.Visible = true;
                    gbBotones.Visible = true;
                    gbDatosPrincipales.Enabled = true;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
                    DesactivaControles(false);
                    //Escondo los controles que no se tienen que ver
                    numAdelantamiento.Visible = false;
                    numCapacidadProducción.Visible = false;
                    numCapacidadStock.Visible = false;
                    gbDemandaAñoSiguiente.Visible = false;
                    gbPuntoEquilibrio.Visible = false;
                    lblAdelantamiento.Visible = false;
                    lblCapacidadProduccion.Visible = false;
                    lblCapacidadStock.Visible = false;
                    lblUnidad1.Visible = false;
                    lblUnidad2.Visible = false;
                    lblUnidad3.Visible = false;
                    break;

                case estadoUI.nuevo:
                    txtAnio.Text = string.Empty;
                    numAdelantamiento.Value = 0;
                    numAdelantamiento.Enabled = true;
                    rbDemandaActual.Checked = true;
                    chListAnios.Visible = false;
                    btnPlanificar.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = false;
                    gbGraficoEstimacion.Visible = false;
                    gbBotones.Visible = false;
                    gbDatosPrincipales.Enabled = true;
                    gbModificacion.Visible = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.nuevo;
                    CargarAñosBase();   
                    //Manejo los controles
                    txtAnio.Focus();
                    numPuntoEquilibrio.Enabled = false;
                    txtTotal.Enabled = false;
                    numPrecioVenta.Value = 0;
                    numPuntoEquilibrio.Value = 0;
                    numPuntoEquilibrio.Enabled = false;
                    chPuntoEquilibrio.Checked = false;
                    numCostofijo.Value = 0;
                    numCostoVariable.Value = 0;
                    //Obtengo los valores de capcidad de stock y produccion calculados
                    numCapacidadProducción.Value = BLL.FabricaBLL.GetCapacidadAnualBruta(BLL.CocinaBLL.GetCodigoCocinaBase(), GyCAP.Entidades.Enumeraciones.RecursosFabricacionEnum.TipoHorario.Normal);
                    numCapacidadStock.Value = BLL.ConfiguracionSistemaBLL.GetConfiguracion<int>("CapacidadStock");
                    
                    //Cargo el costo variable del producto
                    

                    //Escondo los controles que no se tienen que ver
                    numAdelantamiento.Visible = true;
                    numCapacidadProducción.Visible = true;
                    numCapacidadStock.Visible = true;
                    gbDemandaAñoSiguiente.Visible = true;
                    gbPuntoEquilibrio.Visible = true;
                    lblAdelantamiento.Visible = true;
                    lblCapacidadProduccion.Visible = true;
                    lblCapacidadStock.Visible = true;
                    lblUnidad1.Visible = true;
                    lblUnidad2.Visible = true;
                    lblUnidad3.Visible = true;
                    break;

                case estadoUI.calcularPlanificacion:
                    btnPlanificar.Enabled = false;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = true;
                    gbGraficoEstimacion.Visible = true;
                    gbBotones.Visible = true;
                    gbDatosPrincipales.Enabled = false;
                    gbModificacion.Visible = true;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.calcularPlanificacion;
                    DesactivaControles(false);
                    break;

                default:
                    break;
            }
        }
        
        //Método para evitar la creación de más de una pantalla
        public static frmPlanAnual Instancia
        {
            get
            {
                if (_frmPlanAnual == null || _frmPlanAnual.IsDisposed)
                {
                    _frmPlanAnual = new frmPlanAnual();
                }
                else
                {
                    _frmPlanAnual.BringToFront();
                }
                return _frmPlanAnual;
            }
            set
            {
                _frmPlanAnual = value;
            }
        }

        //Seteo los métodos para formatear las listas
        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }
        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }
        #endregion
        
        #region Funciones Formulario

        private void CargarAñosBase()
        {
            //Creo el dataview y lo asigno al control de cheklist
            dvChAnios = new DataView(dsPlanAnual.DEMANDAS_ANUALES);
            chListAnios.Items.Clear();
            string anio, nombre, union;

            for (int i = 0; i <= (dsPlanAnual.DEMANDAS_ANUALES.Rows.Count - 1); i++)
            {
                anio = dvChAnios[i]["deman_anio"].ToString();
                nombre = dvChAnios[i]["deman_nombre"].ToString();
                union = anio + "-" + nombre;
                chListAnios.Items.Add(union);
            }

        }
        
        private string Validar(estadoUI estado)
        {
            string strError = string.Empty;

            if (estado == estadoUI.nuevo)
            {
                //Validacion del año
                if (txtAnio.Text.Length != 4) strError = strError + "-El año no tiene el tamaño adecuado\n";

                try
                {
                    int anio = Convert.ToInt32(txtAnio.Text);
                }
                catch (Exception) { strError = strError + "-El año no es un número\n"; }

                //Validacion Stock y Producción
                if (numCapacidadProducción.Value==0) strError = strError + "-El valor de Capacidad Semanal de Producción no puede ser cero\n";
                if (numCapacidadStock.Value == 0) strError = strError + "-El valor de Capacidad Anual de Stock no puede ser cero\n";
                
                if (rbOtraDemanda.Checked==true)
                {
                    //Verifico que haya seleccionado alguna estimacion 
                    if (chListAnios.CheckedItems.Count == 0) strError = strError + "-Debe seleccionar una demanda para el próximo año\n";
                    else if (chListAnios.CheckedItems.Count > 1) strError = strError + "-Debe seleccionar sólo una demanda para el próximo año\n";
                }
                if (Convert.ToInt32(numPuntoEquilibrio.Value) == 0) strError = strError + "-Debe realizar el Cálculo del punto de equilibrio para determinar mínimo\n";
                if (Convert.ToInt32(cbEstimacionDemanda.GetSelectedIndex()) == -1) strError = strError + "-Debe seleccionar una Demanda Anual\n";

            }
            else if (estado == estadoUI.modificar)
            {
                //Validacion del año
                if (txtAnio.Text.Length != 4) strError = strError + "-El año no tiene el tamaño adecuado\n";

                try
                {
                    int anio = Convert.ToInt32(txtAnio.Text);
                }
                catch (Exception) { strError = strError + "-El año no es un número\n"; }
                if (Convert.ToInt32(cbEstimacionDemanda.GetSelectedIndex()) == -1) strError = strError + "-Debe seleccionar una Demanda\n";
            }
            if (strError != string.Empty)
            {
                strError = "Errores de Validación:\n" + strError;
            }
            return strError;
        }

        private void DesactivaControles(bool estado)
        {
            if (estado == true) estado = false;
            if (estado == false) estado = true;

            numEnero.Enabled = estado;
            numFebrero.Enabled = estado;
            numMarzo.Enabled = estado;
            numAbril.Enabled = estado;
            numMayo.Enabled = estado;
            numJunio.Enabled = estado;
            numJulio.Enabled = estado;
            numAgosto.Enabled = estado;
            numSeptiembre.Enabled = estado;
            numOctubre.Enabled = estado;
            numNoviembre.Enabled = estado;
            numDiciembre.Enabled = estado;
        }

        private void LimpiarControles()
        {
            numEnero.Value = 0;
            numFebrero.Value = 0;
            numMarzo.Value = 0;
            numAbril.Value = 0;
            numMayo.Value = 0;
            numJunio.Value = 0;
            numJulio.Value = 0;
            numAgosto.Value = 0;
            numSeptiembre.Value = 0;
            numOctubre.Value = 0;
            numNoviembre.Value = 0;
            numDiciembre.Value = 0;
        }
        private void CalculaTotal()
        {
            int totalDemanda=0;
            if (txtTotal.Text != string.Empty && txtDemandaNoCubierta.Text != string.Empty)
            {
                totalDemanda = Convert.ToInt32(txtTotal.Text) + Convert.ToInt32(txtDemandaNoCubierta.Text);
            }
            totalActual = 0;
            totalActual += Convert.ToDecimal(numEnero.Value);
            totalActual += Convert.ToDecimal(numFebrero.Value);
            totalActual += Convert.ToDecimal(numMarzo.Value);
            totalActual += Convert.ToDecimal(numAbril.Value);
            totalActual += Convert.ToDecimal(numMayo.Value);
            totalActual += Convert.ToDecimal(numJunio.Value);
            totalActual += Convert.ToDecimal(numJulio.Value);
            totalActual += Convert.ToDecimal(numAgosto.Value);
            totalActual += Convert.ToDecimal(numSeptiembre.Value);
            totalActual += Convert.ToDecimal(numOctubre.Value);
            totalActual += Convert.ToDecimal(numNoviembre.Value);
            totalActual += Convert.ToDecimal(numDiciembre.Value);

            //Se lo asigno al texbox que lo muestra por pantalla
            txtTotal.Text = totalActual.ToString();
            txtDemandaNoCubierta.Text = Convert.ToString(totalDemanda - totalActual);           
        }

        //Metodo para generar gráficos a partir de un array
        private void GenerarGrafico(int[] Plan)
        {
            //Creo el grafico
            //Elimino la serie que esta ahora
            chartDemanda.Series.Remove(chartDemanda.Series[0]);
            //Agrego una serie nueva con el nombre del contador de series
            chartDemanda.Series.Add(seriesGraficos.ToString());
            seriesGraficos += seriesGraficos;
            //Defino el tipo de grafico que deseo
            chartDemanda.Series[seriesGraficos.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            //Lo dibujo
            double plotY = 0;
            if (chartDemanda.Series[seriesGraficos.ToString()].Points.Count > 0)
            {
                plotY = chartDemanda.Series[seriesGraficos.ToString()].Points[chartDemanda.Series[seriesGraficos.ToString()].Points.Count - 1].YValues[0];
            }

            for (int pointIndex = 0; pointIndex < Plan.Count(); pointIndex++)
            {
                plotY = Convert.ToInt32(Plan[pointIndex]);
                chartDemanda.Series[seriesGraficos.ToString()].Points.AddY(plotY);
            }
        }

        #endregion

        #region Pantalla Busqueda
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanAnual.PLANES_ANUALES.Clear();

                if (txtAnioBuscar.Text != string.Empty)
                {
                    //Valido el año ingresado
                    if (txtAnioBuscar.Text.Length != 4 ) throw new Exception();

                    int anio = int.Parse(txtAnioBuscar.Text);

                    //Se llama a la funcion de busqueda con todos los parametros
                    BLL.PlanAnualBLL.ObtenerTodos(anio, dsPlanAnual);
                }
                else
                {
                    //Se llama a la funcion de busqueda para que traiga todos los valores
                    BLL.PlanAnualBLL.ObtenerTodos(dsPlanAnual.PLANES_ANUALES);
                }
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanes.Table = dsPlanAnual.PLANES_ANUALES;

                if (dsPlanAnual.PLANES_ANUALES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Planes Anuales", this.Text);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }
            catch (Exception)
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("El año ingresado no tiene el formato correcto", this.Text);
            }
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsPlanAnual.DETALLE_PLAN_ANUAL.Clear();

                int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pan_codigo"]);

                //Se llama a la funcion de busqueda con todos los parametros
                BLL.DetallePlanAnualBLL.ObtenerDetalle(codigo, dsPlanAnual.DETALLE_PLAN_ANUAL);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsPlanAnual.DETALLE_PLAN_ANUAL;

                if (dsPlanAnual.DETALLE_PLAN_ANUAL.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Detalles de Plan Anual", this.Text);
                }
                else
                {
                    SetInterface(estadoUI.buscar);                    
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);   
            }
        }
#endregion

        #region Pantalla Datos

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }       

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnPlanificar_Click(object sender, EventArgs e)
        {
            string msjvalidar= Validar(estadoUI.nuevo);
            
            try
            {
                if (msjvalidar == string.Empty)
                {

                    //Primero estimamos la demanda real para el año haciendo el corrimiento de la curva
                    //Se busca el parámetro de adelantamiento 
                    int paramAdelantamiento = Convert.ToInt32(numAdelantamiento.Value);

                    //Cargo la demanda del año actual a un vector
                    //1-Obtengo la Demanda Actual
                    Entidades.DemandaAnual demandaActual = new GyCAP.Entidades.DemandaAnual();
                    demandaActual = ObtenerDemanda(true);
                    //2-Genero el vector
                    int[] demandaActualCantidades = new int[12];

                    string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                    for (int i = 0; i < 12; i++)
                    {
                        demandaActualCantidades[i] = BLL.DetalleDemandaAnualBLL.CantidadAñoMes(demandaActual.Anio, demandaActual.Nombre, Meses[i]);

                    }
                    //3-Lo paso a semanas
                    //3-1-Obtengo las semanas en cada mes 
                    int[] semanasMesDA = new int[13];

                    //calculo las semanas del año
                    semanasMesDA = SemanasAño(demandaActual.Anio);

                    //Calculo el total de semanas en el año
                    int totalSemanaActual = semanasMesDA.Sum();

                    //Creo el vector de cantidades de demanda en semanas
                    int[] demandaActCantSemana = new int[totalSemanaActual];

                    //Paso la demanda actual a semanas
                    int cont = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < semanasMesDA[i]; j++)
                        {
                            demandaActCantSemana[cont] = demandaActualCantidades[i] / semanasMesDA[i];
                            cont += 1;
                        }
                    }

                    //****************************DEMANDA AÑO PROXIMO *************************
                    //4-Obtengo la Demanda del Año Proximo
                    Entidades.DemandaAnual demandaProxima = new GyCAP.Entidades.DemandaAnual();
                    if (rbOtraDemanda.Checked == true)
                    {
                        demandaProxima = ObtenerDemanda(false);
                    }
                    else demandaProxima = demandaActual;

                    //Defino el vector con las cantidades mensuales
                    int[] demandaProximaCantidades = new int[12];

                    for (int i = 0; i < 12; i++)
                    {
                        demandaProximaCantidades[i] = BLL.DetalleDemandaAnualBLL.CantidadAñoMes(demandaProxima.Anio, demandaProxima.Nombre, Meses[i]);
                    }

                    //5-Lo paso a semanas
                    //5-1-Obtengo las semanas en cada mes 
                    int[] semanasMesDProx = new int[13];

                    //calculo las semanas del año
                    semanasMesDProx = SemanasAño(demandaProxima.Anio);
                    int totalSemanaProx = semanasMesDProx.Sum();

                    //Creo el vector de cantidades de demanda en semanas
                    int[] demandaProxCantSemana = new int[totalSemanaProx];

                    //Paso la demanda actual a semanas
                    cont = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < semanasMesDProx[i]; j++)
                        {
                            demandaProxCantSemana[cont] = demandaProximaCantidades[i] / semanasMesDProx[i];
                            cont += 1;
                        }
                    }

                    //Comienzo la Manipulacion de los datos para acomodarlos
                    //BORRO LOS PRIMEROS N ELEMENTOS DEL AÑO ACTUAL
                    for (int i = 0; i < paramAdelantamiento; i++)
                    {
                        demandaActCantSemana[i] = 0;
                    }
                    //Correr los elementos n lugares
                    for (int i = paramAdelantamiento; i < totalSemanaActual; i++)
                    {
                        demandaActCantSemana[i - paramAdelantamiento] = demandaActCantSemana[i];
                    }
                    //Inserto en los ultimos n lugares los de la demanda proxima
                    cont = 0;
                    for (int i = totalSemanaActual - paramAdelantamiento; i < totalSemanaActual; i++)
                    {
                        demandaActCantSemana[i] = demandaProxCantSemana[cont];
                    }
                    
                    //********************** Creo un nuevo array de demanda ******************
                    int[] demandaStock = new int[totalSemanaActual];
                    for (int i = 0; i < totalSemanaActual; i++)
                    {
                        demandaStock[i] = demandaActCantSemana[i];
                    }

                    //*************PLANIFICACION DE LA DEMANDA EN CADA MES *******************
                    //Se verifican los parametros ingresados
                    int capacidadProduccion = Convert.ToInt32(numCapacidadProducción.Value);
                    int capacidadStock = Convert.ToInt32(numCapacidadStock.Value);

                    #region ALGORITMO Asignacion
                    //***************************ALGORITMO DE ASIGNACION *********************
                    int demandaNoCubierta = 0, redistribucion = 0;

                    //Calculo cuantas sobredemandas tengo y las cuento
                    int contPicos = 0;
                    int[] semanaPicos = new int[totalSemanaActual];

                    for (int i = 0; i < totalSemanaActual; i++)
                    {
                        if (demandaActCantSemana[i] - capacidadProduccion > 0)
                        {
                            semanaPicos[contPicos] = i;
                            contPicos += 1;
                        }
                    }

                    int[,] stock = new int[contPicos, 2];
                    int variableStock;


                    //Calculo el minimo valor a producir usando el punto de equilibrio
                    int puntoEquilibrio;
                    if (Convert.ToInt32(numPuntoEquilibrio.Value) == 0)
                    {
                        puntoEquilibrio = CalcularPuntoEquilibrio();
                    }
                    else puntoEquilibrio = Convert.ToInt32(numPuntoEquilibrio.Value);

                    int minimo = puntoEquilibrio;
                    
                    cont = 0;
                    int pico = 0;
                    bool Vieneminimo = true;
                    //Se hace la redistribucion del plan
                    for (int i = 0; i < totalSemanaActual; i++)
                    {
                        //Existe una sobredemanda
                        if (demandaActCantSemana[i] - capacidadProduccion > 0)
                        {

                            if (Vieneminimo == true)
                            {
                                stock[cont, 0] = capacidadStock;
                                variableStock = capacidadStock;
                                stock[cont, 1] = i;
                                Vieneminimo = false;
                            }
                            else
                            {
                                capacidadStock = Convert.ToInt32(numCapacidadStock.Value);
                                stock[cont, 0] = capacidadStock;
                                variableStock = capacidadStock;
                                stock[cont, 1] = i;
                            }


                            //Si es el primer mes se hace la capacidad maxima

                            if (i == 0)
                            {
                                demandaActCantSemana[i] = capacidadProduccion;
                                demandaNoCubierta = demandaNoCubierta + demandaActCantSemana[i] - capacidadProduccion;
                            }
                            if (i != 0)
                            {
                                //Se debe redistribuir la diferencia entre los meses anteriores
                                redistribucion = demandaActCantSemana[i] - capacidadProduccion;

                                //Se redistribuye
                                for (int j = i; j >= 0; j--)
                                {
                                    //Pregunto si tienen lugar para producir
                                    if (demandaActCantSemana[j] < capacidadProduccion)
                                    {
                                        //Saco cuanto le sobra
                                        int sobra = capacidadProduccion - demandaActCantSemana[j];
                                        //Pregunto en que mes estamos
                                        if (cont > 0)
                                        {
                                            if (stock[cont - 1, 1] == j)
                                            {
                                                variableStock = stock[cont - 1, 0];
                                            }
                                            else
                                            {
                                                stock[cont, 0] = variableStock;
                                            }
                                        }
                                        else stock[cont, 0] = variableStock;

                                        //Si redistribucion mayor de lo que sobra
                                        if (redistribucion > sobra)
                                        {
                                            if (variableStock >= sobra)
                                            {
                                                redistribucion = redistribucion - sobra;
                                                demandaActCantSemana[j] = capacidadProduccion;
                                                variableStock -= sobra;
                                            }
                                            else
                                            {
                                                redistribucion = redistribucion - variableStock;
                                                demandaActCantSemana[j] = demandaActCantSemana[j] + variableStock;
                                                variableStock = 0;
                                                j = -1;
                                            }
                                        }
                                        else
                                        {
                                            demandaActCantSemana[j] = demandaActCantSemana[j] + redistribucion;
                                            variableStock -= redistribucion;
                                            redistribucion = 0;
                                            j = -1;

                                        }

                                    }
                                }

                                //Si quedo algo por distribuir se suma a la demanda no cubierta
                                demandaActCantSemana[i] = capacidadProduccion;
                                demandaNoCubierta += redistribucion;
                            }
                            cont += 1;
                        }
                        else
                        {
                            //Es necesario ver si produce el minimo 
                            if (demandaActCantSemana[i] < minimo)
                            {
                                //Me fijo cuanto falta para ese minimo
                                int falta = minimo - demandaActCantSemana[i];

                                //me fijo si hay capacidad de stock
                                if (capacidadStock > falta)
                                {
                                    //Debo producir el minimo para cubrir los costos
                                    demandaActCantSemana[i] = minimo;
                                    capacidadStock = capacidadStock - falta;
                                }
                                //Me fijo el proximo pico de demanda
                                if (demandaActCantSemana[pico] - capacidadProduccion <= 0)
                                {
                                    pico = 0;
                                }
                                bool encontroPico = false;

                                while (encontroPico == false)
                                {
                                    if (pico == 0)
                                    {
                                        for (int h = 0; h < contPicos; h++)
                                        {
                                            if (i < semanaPicos[h] && semanaPicos[h] != 0)
                                            {
                                                pico = semanaPicos[h];
                                                semanaPicos[h] = 0;
                                                break;
                                            }
                                        }
                                    }
                                    //Le resto al proximo pico de demanda lo que asigne
                                    //Pregunto si todavia es un  pico
                                    if (demandaActCantSemana[pico] - capacidadProduccion > 0)
                                    {
                                        demandaActCantSemana[pico] -= falta;
                                        encontroPico = true;
                                    }
                                    if (i > semanaPicos[contPicos])
                                    {
                                        encontroPico = true;
                                    }
                                    Vieneminimo = true;
                                }
                            }
                        }
                    }

                    #endregion

                    int[] planMeses = new int[12];

                    //Paso la Planificacion de la Produccion a meses
                    cont = 0;
                    int acum = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        acum += semanasMesDA[i];

                        for (int j = 0; j < totalSemanaActual; j++)
                        {
                            if (i > 0)
                            {
                                if (j < acum && j >= acum - semanasMesDA[i])
                                {
                                    planMeses[cont] = planMeses[cont] + demandaActCantSemana[j];
                                }
                            }
                            else
                            {
                                if (j < acum)
                                {
                                    planMeses[cont] = planMeses[cont] + demandaActCantSemana[j];
                                }
                            }
                        }
                        cont += 1;
                    }

                    //Paso los valores a cada uno de los meses

                    //Se asignan los valores calculados a los textbox
                    numEnero.Value = planMeses[0];
                    numFebrero.Value = planMeses[1];
                    numMarzo.Value = planMeses[2];
                    numAbril.Value = planMeses[3];
                    numMayo.Value = planMeses[4];
                    numJunio.Value = planMeses[5];
                    numJulio.Value = planMeses[6];
                    numAgosto.Value = planMeses[7];
                    numSeptiembre.Value = planMeses[8];
                    numOctubre.Value = planMeses[9];
                    numNoviembre.Value = planMeses[10];
                    numDiciembre.Value = planMeses[11];

                    //Calculamos el Total de la Planificación
                    txtTotal.Text = planMeses.Sum().ToString();
                    lblTotalSistema.Text = planMeses.Sum().ToString();

                    //Muestro lo que no se pudo asignar
                    txtDemandaNoCubierta.Text =(BLL.DetalleDemandaAnualBLL.ObtenerTotal(demandaActual.Codigo)- Convert.ToInt32(txtTotal.Text)).ToString(); ;

                    //Generamos el Grafico de Planificacion
                    GenerarGrafico(planMeses);

                    //Seteamos el estado de la interfaz
                    SetInterface(estadoUI.calcularPlanificacion);
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(msjvalidar, this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            
        }

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

        private Entidades.DemandaAnual ObtenerDemanda(bool esCombo)
        {
            Entidades.DemandaAnual demanda = new Entidades.DemandaAnual();
            string contenido;
            int largo;

            if (esCombo == true)
            {
                contenido = cbEstimacionDemanda.GetSelectedText();
                demanda.Codigo =Convert.ToInt32(cbEstimacionDemanda.GetSelectedValue());
                demanda.Anio = Convert.ToInt32(contenido.Substring(0, 4));
                largo = contenido.Length;
                largo = largo - 5;
                if (largo > 0) demanda.Nombre = contenido.Substring(5, largo);
            }
            else
            {
                for (int i = 0; i <= chListAnios.Items.Count - 1; i++)
                {
                    
                    chListAnios.SelectedIndex = i;
                    if (chListAnios.GetItemChecked(i))
                    {
                        contenido = chListAnios.SelectedItem.ToString();
                        demanda.Anio = Convert.ToInt32(contenido.Substring(0, 4));
                        largo = contenido.Length;
                        largo = largo - 5;
                        if (largo > 0) demanda.Nombre = contenido.Substring(5, largo);
                    }
                }
            }
            return demanda;
        }

        private void rbDemandaActual_CheckedChanged(object sender, EventArgs e)
        {
            chListAnios.Visible = false;
        }

        private void rbOtraDemanda_CheckedChanged(object sender, EventArgs e)
        {
            chListAnios.Visible = true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        //MEtodo para calcular el Punto de Equilibrio
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chPuntoEquilibrio.Checked == false)
                {
                    numPuntoEquilibrio.Value = CalcularPuntoEquilibrio();
                }                
            }
            catch (Entidades.Excepciones.ErrorValidacionException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }

        private int CalcularPuntoEquilibrio()
        {
            string stdError = string.Empty;
            int puntoEquilibrio;
            try
            {
                if (Convert.ToInt32(numCostofijo.Value) == 0) stdError = stdError + "-El Costo Fijo debe Ser mayor a Cero\n";
                if (Convert.ToInt32(numCostoVariable.Value) == 0) stdError = stdError + "-El Costo Variable debe Ser mayor a Cero\n";
                if (Convert.ToInt32(numPrecioVenta.Value) == 0) stdError = stdError + "-El Precio de Venta debe Ser mayor a Cero\n";
                if (Convert.ToInt32(numPrecioVenta.Value) < Convert.ToInt32(numCostoVariable.Value)) stdError = stdError + "-El Precio de Venta debe Ser mayor al costo Variable\n";

                if (stdError == string.Empty)
                {
                    decimal costoFijo = Convert.ToDecimal(numCostofijo.Value);
                    decimal costoVariable = Convert.ToDecimal(numCostoVariable.Value);
                    decimal precioVenta = Convert.ToDecimal(numPrecioVenta.Value);

                    puntoEquilibrio = Convert.ToInt32(costoFijo / (precioVenta - costoVariable));
                }
                else
                {
                    stdError = "Los Errores de validación encontrados son:\n" + stdError;
                    throw new Entidades.Excepciones.ErrorValidacionException(stdError);
                }
            }
            catch (Entidades.Excepciones.ErrorValidacionException)
            {
                throw new Entidades.Excepciones.ErrorValidacionException(stdError);
            }

            return puntoEquilibrio;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (estadoActual == estadoUI.calcularPlanificacion)
            {
                SetInterface(estadoUI.nuevo);
            }
            else if (estadoActual == estadoUI.modificar)
            {
                SetInterface(estadoUI.buscar);
            }
        }       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string validacion = string.Empty;
                //Creo el objeto de Demanda
                Entidades.PlanAnual planAnual = new GyCAP.Entidades.PlanAnual();
                Entidades.DemandaAnual demanda = new GyCAP.Entidades.DemandaAnual();
                //Se crea la lista de objetos genericos
                IList<Entidades.DetallePlanAnual> detalle = new List<Entidades.DetallePlanAnual>();
                
                if(estadoActual== estadoUI.calcularPlanificacion)
                {
                    validacion = Validar(estadoUI.nuevo);
                    //Pregunto si esta todo escrito
                    if (validacion == string.Empty)
                    {
                        //Se definen los parámetros que se van a guardar
                        planAnual.Anio = Convert.ToInt32(txtAnio.Text);
                        planAnual.FechaCreacion =  BLL.DBBLL.GetFechaServidor();
                                             
                        demanda= ObtenerDemanda(true);                        
                        planAnual.Demanda=demanda;
                    }
                }
                else if (estadoActual == estadoUI.modificar)
                {
                    validacion = string.Empty;
                    validacion = Validar(estadoUI.modificar);

                    //Pregunto si esta todo escrito
                    if (validacion == string.Empty)
                    {
                        //Se definen los parámetros que se van a guardar
                        planAnual.Codigo= Convert.ToInt32(dvListaDetalle[dgvLista.SelectedRows[0].Index]["pan_codigo"]);
                        planAnual.Anio = Convert.ToInt32(txtAnio.Text);
                        demanda = ObtenerDemanda(true);
                        planAnual.Demanda = demanda;
                    }
                }

                if (validacion == string.Empty)
                {
                    //Meses
                    int enero, febrero, marzo, abril, mayo, junio, julio, agosto, septiembre;
                    int octubre, noviembre, diciembre;

                    //Se setean los valores que se van a pasar
                    enero = Convert.ToInt32(numEnero.Value);
                    febrero = Convert.ToInt32(numFebrero.Value);
                    marzo = Convert.ToInt32(numMarzo.Value);
                    abril = Convert.ToInt32(numAbril.Value);
                    mayo = Convert.ToInt32(numMayo.Value);
                    junio = Convert.ToInt32(numJunio.Value);
                    julio = Convert.ToInt32(numJulio.Value);
                    agosto = Convert.ToInt32(numAgosto.Value);
                    septiembre = Convert.ToInt32(numSeptiembre.Value);
                    octubre = Convert.ToInt32(numOctubre.Value);
                    noviembre = Convert.ToInt32(numNoviembre.Value);
                    diciembre = Convert.ToInt32(numDiciembre.Value);

                    string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                    int[] Cantidad = { enero, febrero, marzo, abril, mayo, junio, julio, agosto, septiembre, octubre, noviembre, diciembre };

                    //Se van creando los objetos
                    for (int i = 0; i < 12; i++)
                    {
                        Entidades.DetallePlanAnual objeto = new Entidades.DetallePlanAnual();
                        objeto.PlanAnual = planAnual;
                        objeto.CantidadMes = Cantidad[i];
                        objeto.Mes = Meses[i];
                        detalle.Add(objeto);
                    }

                    if (estadoActual == estadoUI.calcularPlanificacion)
                    {
                        //Se guardan los datos en la BD
                        detalle = BLL.PlanAnualBLL.Insertar(planAnual, detalle);
                        planAnual.Codigo = detalle[0].PlanAnual.Codigo;
                    }

                    //SI ESTA GUARDANDO 
                    if (estadoActual == estadoUI.calcularPlanificacion)
                    {
                        //Se agrega todo al dataset
                        //La demanda anual (cabecera)
                        Data.dsPlanAnual.PLANES_ANUALESRow rowPlan = dsPlanAnual.PLANES_ANUALES.NewPLANES_ANUALESRow();
                        rowPlan.BeginEdit();
                        rowPlan.PAN_CODIGO = planAnual.Codigo;
                        rowPlan.PAN_ANIO = planAnual.Anio;
                        rowPlan.PAN_FECHACREACION = planAnual.FechaCreacion;
                        rowPlan.DEMAN_CODIGO = planAnual.Demanda.Codigo;
                        rowPlan.EndEdit();
                        dsPlanAnual.PLANES_ANUALES.AddPLANES_ANUALESRow(rowPlan);
                        dsPlanAnual.PLANES_ANUALES.AcceptChanges();

                        //El detalle de la demanda anual

                        foreach (Entidades.DetallePlanAnual obje in detalle)
                        {
                            Data.dsPlanAnual.DETALLE_PLAN_ANUALRow rowDPlan = dsPlanAnual.DETALLE_PLAN_ANUAL.NewDETALLE_PLAN_ANUALRow();
                            rowDPlan.BeginEdit();
                            rowDPlan.DPAN_CODIGO = obje.Codigo;
                            rowDPlan.DPAN_CANTIDADMES = obje.CantidadMes;
                            rowDPlan.DPAN_MES = obje.Mes;
                            rowDPlan.PAN_CODIGO = obje.PlanAnual.Codigo;
                            rowDPlan.EndEdit();
                            dsPlanAnual.DETALLE_PLAN_ANUAL.AddDETALLE_PLAN_ANUALRow(rowDPlan);
                            dsPlanAnual.DETALLE_PLAN_ANUAL.AcceptChanges();
                        }
                        
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Plan Anual", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        //Seteo el estado de la interface a inicio
                        SetInterface(estadoUI.inicio);
                    }
                    else if (estadoActual == estadoUI.modificar)
                    {
                        //Se modifica en la BD
                        BLL.PlanAnualBLL.Modificar(planAnual, detalle);

                        //Se modifica el dataset
                        //La demanda anual (cabecera)
                        Data.dsPlanAnual.PLANES_ANUALESRow rowPlan = dsPlanAnual.PLANES_ANUALES.FindByPAN_CODIGO(planAnual.Codigo);
                        rowPlan.BeginEdit();
                        rowPlan.DEMAN_CODIGO = planAnual.Demanda.Codigo;
                        rowPlan.PAN_ANIO = planAnual.Anio;
                        rowPlan.EndEdit();
                        dsPlanAnual.PLANES_ANUALES.AcceptChanges();

                        //El detalle de la demanda anual
                        foreach (Entidades.DetallePlanAnual obje in detalle)
                        {
                            obje.Codigo = BLL.DetallePlanAnualBLL.ObtenerID(obje);
                            Data.dsPlanAnual.DETALLE_PLAN_ANUALRow rowDPlan = dsPlanAnual.DETALLE_PLAN_ANUAL.FindByDPAN_CODIGO(obje.Codigo);
                            rowDPlan.BeginEdit();
                            rowDPlan.DPAN_CANTIDADMES = obje.CantidadMes;
                            rowDPlan.EndEdit();
                            dsPlanAnual.DETALLE_PLAN_ANUAL.AcceptChanges();
                        }

                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Plan Anual", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);

                        //Pongo la interface al inicio
                        SetInterface(estadoUI.inicio);
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
            catch (Exception ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
             //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && dsPlanAnual.DETALLE_PLAN_ANUAL.Count != 0)
            {
                //Selecciono el codigo de la demanda anual
                int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pan_codigo"]);
                txtAnio.Text = dsPlanAnual.PLANES_ANUALES.FindByPAN_CODIGO(codigo).PAN_ANIO.ToString();
                cbEstimacionDemanda.SetSelectedValue(Convert.ToInt32(dsPlanAnual.PLANES_ANUALES.FindByPAN_CODIGO(codigo).DEMAN_CODIGO));

                if (txtTotal.Text == string.Empty)
                {
                    txtTotal.Text = "0";
                    //Muestro lo que no se pudo asignar
                    txtDemandaNoCubierta.Text = BLL.DetalleDemandaAnualBLL.ObtenerTotal(Convert.ToInt32(dsPlanAnual.PLANES_ANUALES.FindByPAN_CODIGO(codigo).DEMAN_CODIGO)).ToString();
                }

                int[] promedio = new int[12];
                int cont = 0;
                foreach (Data.dsPlanAnual.DETALLE_PLAN_ANUALRow dr in dsPlanAnual.DETALLE_PLAN_ANUAL.Rows)
                {
                    promedio[cont] = Convert.ToInt32(dr.DPAN_CANTIDADMES);
                    cont += 1;
                }
                //Asigno los valores
                //Se asignan los valores calculados a los textbox
                numEnero.Value = promedio[0];
                numFebrero.Value = promedio[1];
                numMarzo.Value = promedio[2];
                numAbril.Value = promedio[3];
                numMayo.Value = promedio[4];
                numJunio.Value = promedio[5];
                numJulio.Value = promedio[6];
                numAgosto.Value = promedio[7];
                numSeptiembre.Value = promedio[8];
                numOctubre.Value = promedio[9];
                numNoviembre.Value = promedio[10];
                numDiciembre.Value = promedio[11];

                //Se genera el grafico
                GenerarGrafico(promedio);

                //se setea el estado
                SetInterface(estadoUI.modificar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("Debe seleccionar un Plan Anual con su detalle para modificarlo", this.Text);
            }
        }        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Plan Anual", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtengo el Codigo del plan
                        int codigo = Convert.ToInt32(dvListaDetalle[dgvLista.SelectedRows[0].Index]["pan_codigo"]);

                        //Pregunto si se puede eliminar
                        if (BLL.PlanAnualBLL.PuedeEliminarse(codigo))
                        {
                            //Elimino el plan anual y su detalle de la BD
                            BLL.PlanAnualBLL.Eliminar(codigo);

                            //Limpio el dataset de detalles
                            dsPlanAnual.DETALLE_PLAN_ANUAL.Clear();

                            //Lo eliminamos del dataset
                            dsPlanAnual.PLANES_ANUALES.FindByPAN_CODIGO(codigo).Delete();
                            dsPlanAnual.PLANES_ANUALES.AcceptChanges();

                            //Avisamos que se elimino 
                            Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);

                            //Ponemos la ventana en el estado inicial
                            SetInterface(estadoUI.inicio);
                        }
                        else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }

                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Plan Anual", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }
#endregion

        #region Controles
        private void numAdelantamiento_Enter(object sender, EventArgs e)
        {
            numAdelantamiento.Select(0, 10);
        }

        private void numCapacidadStock_Enter(object sender, EventArgs e)
        {
            numCapacidadStock.Select(0, 10);
        }

        private void numCapacidadProducción_Enter(object sender, EventArgs e)
        {
            numCapacidadProducción.Select(0, 10);
        }

        private void numCostofijo_Enter(object sender, EventArgs e)
        {
            numCostofijo.Select(0, 10);
        }

        private void numCostoVariable_Enter(object sender, EventArgs e)
        {
            numCostoVariable.Select(0, 10);
        }

        private void numPrecioVenta_Enter(object sender, EventArgs e)
        {
            numPrecioVenta.Select(0, 10);
        }

        private void numPuntoEquilibrio_Enter(object sender, EventArgs e)
        {
            numPuntoEquilibrio.Select(0, 10);
        }

        private void numEnero_Enter(object sender, EventArgs e)
        {
            numEnero.Select(0, 10);
        }

        private void numFebrero_Enter(object sender, EventArgs e)
        {
            numFebrero.Select(0, 10);
        }

        private void numMarzo_Enter(object sender, EventArgs e)
        {
            numMarzo.Select(0, 10);
        }

        private void numAbril_Enter(object sender, EventArgs e)
        {
            numAbril.Select(0, 10);
        }

        private void numMayo_Enter(object sender, EventArgs e)
        {
            numMayo.Select(0, 10);
        }

        private void numJunio_Enter(object sender, EventArgs e)
        {
            numJunio.Select(0, 10);
        }

        private void numJulio_Enter(object sender, EventArgs e)
        {
            numJulio.Select(0, 10);
        }

        private void numAgosto_Enter(object sender, EventArgs e)
        {
            numAgosto.Select(0, 10);
        }

        private void numSeptiembre_Enter(object sender, EventArgs e)
        {
            numSeptiembre.Select(0, 10);
        }

        private void numOctubre_Enter(object sender, EventArgs e)
        {
            numOctubre.Select(0, 10);
        }

        private void numNoviembre_Enter(object sender, EventArgs e)
        {
            numNoviembre.Select(0, 10);
        }

        private void numDiciembre_Enter(object sender, EventArgs e)
        {
            numDiciembre.Select(0, 10);
        }
        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "DEMAN_CODIGO":
                        string nombre = dsPlanAnual.DEMANDAS_ANUALES.FindByDEMAN_CODIGO(Convert.ToInt32(e.Value)).DEMAN_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }
        private void numEnero_ValueChanged(object sender, EventArgs e)
        {
            CalculaTotal();

            if (estadoActual == estadoUI.modificar || estadoActual == estadoUI.calcularPlanificacion)
            {
                //lleno de nuevo un array con los datos
                int[] promedio = new int[12];

                //Se asignan los valores calculados a los textbox
                promedio[0] = Convert.ToInt32(numEnero.Value);
                promedio[1] = Convert.ToInt32(numFebrero.Value);
                promedio[2] = Convert.ToInt32(numMarzo.Value);
                promedio[3] = Convert.ToInt32(numAbril.Value);
                promedio[4] = Convert.ToInt32(numMayo.Value);
                promedio[5] = Convert.ToInt32(numJunio.Value);
                promedio[6] = Convert.ToInt32(numJulio.Value);
                promedio[7] = Convert.ToInt32(numAgosto.Value);
                promedio[8] = Convert.ToInt32(numSeptiembre.Value);
                promedio[9] = Convert.ToInt32(numOctubre.Value);
                promedio[10] = Convert.ToInt32(numNoviembre.Value);
                promedio[11] = Convert.ToInt32(numDiciembre.Value);

                //creo el grafico
                GenerarGrafico(promedio);
            }
        }

        private void chPuntoEquilibrio_CheckedChanged(object sender, EventArgs e)
        {
            if (chPuntoEquilibrio.Checked == true)
            {
                numPuntoEquilibrio.Enabled = true;
                numPrecioVenta.Enabled = false;
                numCostofijo.Enabled = false;
                numCostoVariable.Enabled = false;
                btnPuntoEquilibrio.Enabled=false;
            }
            else
            {
                numPuntoEquilibrio.Enabled = false;
                numPrecioVenta.Enabled = true;
                numCostofijo.Enabled = true;
                numCostoVariable.Enabled = true;
                btnPuntoEquilibrio.Enabled = true;
            }
        }
        #endregion              

        
        
    }
}
