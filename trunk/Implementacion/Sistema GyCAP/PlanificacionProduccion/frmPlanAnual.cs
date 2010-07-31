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
    public partial class frmPlanAnual : Form
    {
        private static frmPlanAnual _frmPlanAnual = null;
        private Data.dsPlanAnual dsPlanAnual = new GyCAP.Data.dsPlanAnual();
        private DataView dvListaPlanes, dvListaDetalle, dvChAnios, dvComboEstimaciones;
        private enum estadoUI { inicio, nuevo, buscar, modificar, calcularPlanificacion };
        private static estadoUI estadoActual;
        private static decimal seriesGraficos, totalsistema = 0, totalActual;


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
            dgvLista.Columns.Add("PAN_FECHACREACION", "Fecha Creación Plan Anual");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

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
            dgvDetalle.Columns.Add("DPAN_CANTIDADMES", "Fecha Inicio Plan");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPAN_CODIGO"].DataPropertyName = "DPAN_CODIGO";
            dgvDetalle.Columns["DPAN_MES"].DataPropertyName = "DPAN_MES";
            dgvDetalle.Columns["DPAN_CANTIDADMES"].DataPropertyName = "DPAN_CANTIDADMES";
            

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            


            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanAnual.DETALLE_PLAN_ANUAL);
            dgvDetalle.DataSource = dvListaDetalle;


            //Llenamos el dataset con las estimaciones
            BLL.DemandaAnualBLL.ObtenerTodos(dsPlanAnual);
                        
            //Cargamos el combo
            dvComboEstimaciones = new DataView(dsPlanAnual.DEMANDAS_ANUALES);
            string[] display = { "deman_anio" , "deman_nombre" };
            cbEstimacionDemanda.SetDatos(dvComboEstimaciones, "deman_codigo", display,"-" , "Seleccione", false);


            //Seteamos los maxlength de los controles y los tipos de numeros
            txtAnio.MaxLength = 4;
            txtAnioBuscar.MaxLength = 4;
            numAdelantamiento.Increment = 1;
            numCapacidadProducción.Increment = 1;
            numCapacidadStock.Increment = 1;
            numCostofijo.Increment =Convert.ToDecimal(0.01);
            numCostofijo.DecimalPlaces = 2;
            numCostoVariable.Increment =Convert.ToDecimal(0.01);
            numCostoVariable.DecimalPlaces = 2;
            numPrecioVenta.Increment =Convert.ToDecimal(0.01);
            numPrecioVenta.DecimalPlaces = 2;
            
            //Seteamos el estado de la interface
            SetInterface(estadoUI.inicio);

        }
        #region Servicios

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
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                //Cuando Arranca la pantalla
                case estadoUI.inicio:
                    txtAnioBuscar.Text = string.Empty;
                    gbGrillaDemanda.Visible = false;
                    gbGrillaDetalle.Visible = false;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnPlanificar.Enabled = false;
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
                    gbGrillaDemanda.Visible = hayDatos;
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
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
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
                    numCapacidadProducción.Value = 0;
                    numCapacidadStock.Value = 0;
                    numCostofijo.Value = 0;
                    numCostoVariable.Value = 0;
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
                    DesactivaControles(true);
                    break;

                
                default:
                    break;

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
                catch (Exception) { strError = strError + "-El año no es un numero\n"; }

                //Validacion Stock y Producción
                if (numCapacidadProducción.Value==0) strError = strError + "-El valor de Capacidad Semanal de Producción no puede ser cero\n";
                if (numCapacidadStock.Value == 0) strError = strError + "-El valor de Capacidad Anual de Stock no puede ser cero\n";
                
                if (rbOtraDemanda.Checked==true)
                {
                    //Verifico que haya seleccionado alguna estimacion 
                    if (chListAnios.CheckedItems.Count == 0) strError = strError + "-Debe seleccionar una demanda para el próximo año\n";
                    else if (chListAnios.CheckedItems.Count > 1) strError = strError + "-Debe seleccionar solo una demanda para el próximo año\n";
                }
                if (Convert.ToInt32(numPuntoEquilibrio.Value) == 0) strError = strError + "-Debe realizar el Cálculo del punto de equilibrio para determinar minimo\n";

            }

            if (strError != string.Empty)
            {
                strError = "Errores de Validación:\n" + strError;
            }
            return strError;
        }

        private void DesactivaControles(bool estado)
        {
            numEnero.ReadOnly = estado;
            numFebrero.ReadOnly = estado;
            numMarzo.ReadOnly = estado;
            numAbril.ReadOnly = estado;
            numMayo.ReadOnly = estado;
            numJunio.ReadOnly = estado;
            numJulio.ReadOnly = estado;
            numAgosto.ReadOnly = estado;
            numSeptiembre.ReadOnly = estado;
            numOctubre.ReadOnly = estado;
            numNoviembre.ReadOnly = estado;
            numDiciembre.ReadOnly = estado;
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
        }
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


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanAnual.PLANES_ANUALES.Clear();

                if (txtAnioBuscar.Text != string.Empty)
                {
                    //Valido que el año
                    if (txtAnioBuscar.Text.Length != 4) throw new Exception();

                    int anio = Convert.ToInt32(txtAnioBuscar.Text);

                    //Se llama a la funcion de busqueda con todos los parametros
                    BLL.PlanAnualBLL.ObtenerTodos(anio, dsPlanAnual);
                }
                else
                {
                    //Se llama a la funcion de busqueda para que traiga todos los valores
                    BLL.PlanAnualBLL.ObtenerTodos(dsPlanAnual);
                }
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanes.Table = dsPlanAnual.PLANES_ANUALES;

                if (dsPlanAnual.PLANES_ANUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Planes Anuales con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Anual - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El año no tiene el formato Correcto", "Error: Demanda Anual - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                BLL.DetallePlanAnualBLL.ObtenerDetalle(codigo, dsPlanAnual);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsPlanAnual.DETALLE_PLAN_ANUAL;

                if (dsPlanAnual.DETALLE_PLAN_ANUAL.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para ese Plan Anual.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SetInterface(estadoUI.buscar);
                    gbGrillaDetalle.Visible = true;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Demanda Anual - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }

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
                            contPicos += 1;
                            semanaPicos[contPicos] = i;
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
                                if (capacidadStock > 0)
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
                    txtDemandaNoCubierta.Text = demandaNoCubierta.ToString();

                    //Generamos el Grafico de Planificacion
                    GenerarGrafico(planMeses);

                    //Seteamos el estado de la interfaz
                    SetInterface(estadoUI.calcularPlanificacion);
                }
                else
                {
                    throw new Exception(msjvalidar);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private int CalcularPuntoEquilibrio()
        {
            string stdError= string.Empty;
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
                    throw new Exception(stdError);
                }
            }
            catch (Exception)
            {
                throw new Exception(stdError);
            }

            return puntoEquilibrio;

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                numPuntoEquilibrio.Value = CalcularPuntoEquilibrio();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnModificarPlanificacion_Click(object sender, EventArgs e)
        {
            DesactivaControles(false);
        }

       
        

       

        
    }
}
