using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmEstimarDemandaAnual : Form
    {
        private static frmEstimarDemandaAnual _frmEstimarDemandaAnual = null;
        private Data.dsEstimarDemanda dsEstimarDemanda = new GyCAP.Data.dsEstimarDemanda();
        private DataView dvListaDemanda, dvListaDetalle, dvChAnios;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaHistorico, calcularEstimacion };
        private static estadoUI estadoActual;
        //Defino el valor total de la estimacion global para el formulario
        private static decimal totalsistema = 0, totalActual, seriesGraficos;
        
        public frmEstimarDemandaAnual()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;

            //Para cada Lista
            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("DEMAN_CODIGO", "Código");
            dgvLista.Columns.Add("DEMAN_ANIO", "Año");
            dgvLista.Columns.Add("DEMAN_NOMBRE", "Denominación");
            dgvLista.Columns.Add("DEMAN_FECHACREACION", "Fecha Creación Estimación");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["DEMAN_CODIGO"].DataPropertyName = "DEMAN_CODIGO";
            dgvLista.Columns["DEMAN_ANIO"].DataPropertyName = "DEMAN_ANIO";
            dgvLista.Columns["DEMAN_FECHACREACION"].DataPropertyName = "DEMAN_FECHACREACION";
            dgvLista.Columns["DEMAN_NOMBRE"].DataPropertyName = "DEMAN_NOMBRE";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDemanda = new DataView(dsEstimarDemanda.DEMANDAS_ANUALES);
            dvListaDemanda.Sort = "DEMAN_ANIO ASC";
            dgvLista.DataSource = dvListaDemanda;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DDEMAN_CODIGO", "Código");
            dgvDetalle.Columns.Add("DDEMAN_MES", "Mes");
            dgvDetalle.Columns.Add("DDEMAN_CANTIDADMES", "Cantidad Mensual");
            dgvDetalle.Columns["DDEMAN_CODIGO"].Visible = false;
            //Seteamos el modo de tamaño de las columnas
            
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DDEMAN_CODIGO"].DataPropertyName = "DDEMAN_CODIGO";
            dgvDetalle.Columns["DDEMAN_MES"].DataPropertyName = "DDEMAN_MES";
            dgvDetalle.Columns["DDEMAN_CANTIDADMES"].DataPropertyName = "DDEMAN_CANTIDADMES";
                        
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //Escondemos los codigos
            dgvDetalle.Columns["DDEMAN_CODIGO"].Visible = false;
            dgvLista.Columns["DEMAN_CODIGO"].Visible=false;

            //Seteo el maxlenght de los textbox
            txtAnioBuscar.MaxLength = 4;
            txtIdentificacion.MaxLength = 80;
            txtDenominacionHistorico.MaxLength = 80;
            txtAnio.MaxLength = 4;
            numCrecimiento.Increment =Convert.ToDecimal(0.01);
            numCrecimiento.DecimalPlaces = 2;

           
            //Seteo el estado de inicio de la pantalla
            SetInterface(estadoUI.inicio);

        }
        #region Servicios
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    txtAnioBuscar.Text = string.Empty;
                    gbGrillaDemanda.Visible = false;
                    gbGrillaDetalle.Visible = false;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcDemanda.SelectedTab= tpBuscar;
                    break;
                case estadoUI.buscar:
                    btnNuevo.Enabled = true;
                    bool hayDatos;
                    if (dsEstimarDemanda.DEMANDAS_ANUALES.Rows.Count > 0)
                    {
                        hayDatos = true;
                    }
                    else hayDatos = false;
                    btnConsultar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnModificar.Enabled = hayDatos;
                    gbGrillaDemanda.Visible = hayDatos;
                    tcDemanda.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    break;
                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    txtTotal.Enabled = false;
                    numCrecimiento.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnCalcularEstimacion.Enabled = false;
                    btnCargarHistorico.Enabled = false;
                    chListAnios.Visible = false;
                    lblAñosBase.Visible = false;
                    gbModificacion.Visible = false;
                    gbGraficoEstimacion.Visible = true;
                    gbEstimacionMes.Visible = true;
                    gbBotones.Visible = true;
                    tcDemanda.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
                    break;
                case estadoUI.nuevo:
                    txtIdentificacion.Text = string.Empty;
                    txtAnio.Focus();
                    txtAnio.Text = string.Empty;
                    numCrecimiento.Value = 0;
                    numCrecimiento.Enabled = true;
                    chListAnios.Visible = true;
                    lblAñosBase.Visible = true;
                    btnCalcularEstimacion.Enabled = true;
                    btnCargarHistorico.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = false;
                    gbGraficoEstimacion.Visible = false;
                    gbBotones.Visible = false;
                    gbDatosHistoricos.Visible = false;
                    gbDatosPrincipales.Enabled = true;
                    gbModificacion.Visible = false;
                    tcDemanda.SelectedTab = tpDatos;
                    estadoActual = estadoUI.nuevo;
                    CargarAñosBase();
                    break;
                case estadoUI.cargaHistorico:
                    txtTotal.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = true;
                    gbGraficoEstimacion.Visible = false;
                    gbDatosHistoricos.Visible = true;
                    gbBotones.Visible = true;
                    gbModificacion.Visible = false;
                    gbDatosPrincipales.Enabled = false;
                    tcDemanda.SelectedTab = tpDatos;
                    LimpiarControles();
                    estadoActual = estadoUI.cargaHistorico;
                    break;
                case estadoUI.calcularEstimacion:
                    txtTotal.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = true;
                    gbGraficoEstimacion.Visible = true;
                    gbDatosHistoricos.Visible = false;
                    gbBotones.Visible = true;
                    gbModificacion.Visible = true;
                    gbDatosPrincipales.Enabled = false;
                    gbModificacion.Visible = true;
                    tcDemanda.SelectedTab = tpDatos;
                    estadoActual = estadoUI.calcularEstimacion;
                    DesactivaControles(true);
                    break;

                    default:
                    break;

            }

        }

        #endregion

        #region Botones

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsEstimarDemanda.DEMANDAS_ANUALES.Clear();

                if (txtAnioBuscar.Text != string.Empty)
                {
                    //Valido que el año
                    if (txtAnioBuscar.Text.Length != 4) throw new Exception();

                    int anio = Convert.ToInt32(txtAnioBuscar.Text);

                    //Se llama a la funcion de busqueda con todos los parametros
                    BLL.DemandaAnualBLL.ObtenerTodos(anio, dsEstimarDemanda);
                }
                else
                {
                    //Se llama a la funcion de busqueda para que traiga todos los valores
                    BLL.DemandaAnualBLL.ObtenerTodos(dsEstimarDemanda);
                }
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDemanda.Table = dsEstimarDemanda.DEMANDAS_ANUALES;

                if (dsEstimarDemanda.DEMANDAS_ANUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Demandas Anuales con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Demanda Anual - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El año no tiene el formato Correcto", "Error: Demanda Anual - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            LlenarDetalle();
        }

        private void LlenarDetalle()
        {
            try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.Clear();

                int codigo = Convert.ToInt32(dvListaDemanda[dgvLista.SelectedRows[0].Index]["deman_codigo"]);

                //Se llama a la funcion de busqueda con todos los parametros
                BLL.DetalleDemandaAnualBLL.ObtenerDetalle(codigo, dsEstimarDemanda);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES;

                if (dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para esa demanda.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //lleno el Dataset de demandas anuales
            BLL.DemandaAnualBLL.ObtenerTodos(dsEstimarDemanda);
            
            //Seteo la interface
            SetInterface(estadoUI.nuevo);

        }     

        private void btnCargarHistorico_Click(object sender, EventArgs e)
        {
            //Seteamos los valores a cargar historicos
            SetInterface(estadoUI.cargaHistorico);

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (estadoActual == estadoUI.calcularEstimacion || estadoActual == estadoUI.cargaHistorico)
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
                string validacion= string.Empty;
                //Creo el objeto de Demanda
                Entidades.DemandaAnual demanda = new GyCAP.Entidades.DemandaAnual();

                if (estadoActual == estadoUI.cargaHistorico)
                {
                    validacion = Validar(estadoUI.cargaHistorico);
                    //Pregunto si esta todo escrito
                    if ( validacion == string.Empty)
                    {

                        //Se definen los parámetros que se van a guardar

                        demanda.Anio = Convert.ToInt32(txtAnioHistorico.Text);
                        demanda.Nombre = txtDenominacionHistorico.Text;
                        demanda.ParametroCrecimiento = 0;
                        demanda.FechaCreacion = BLL.DBBLL.GetFechaServidor();

                     }
                }
                else if ( estadoActual == estadoUI.calcularEstimacion)
                {
                    validacion=string.Empty;
                    validacion = Validar(estadoUI.calcularEstimacion);

                    //Pregunto si esta todo escrito
                    if ( validacion == string.Empty)
                    {
                        //Se definen los parámetros que se van a guardar

                        demanda.Anio = Convert.ToInt32(txtAnio.Text);
                        demanda.Nombre = txtIdentificacion.Text;
                        demanda.ParametroCrecimiento = Convert.ToDecimal(numCrecimiento.Value);
                        demanda.FechaCreacion = BLL.DBBLL.GetFechaServidor();
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
                            
                            demanda.Codigo= Convert.ToInt32(dvListaDemanda[dgvLista.SelectedRows[0].Index]["deman_codigo"]);
                            demanda.Anio = Convert.ToInt32(txtAnio.Text);
                            demanda.Nombre = txtIdentificacion.Text;
                    }
                }

                if (validacion== string.Empty)
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

                        //Se crea la lista de objetos genericos
                        IList<Entidades.DetalleDemandaAnual> detalle = new List<Entidades.DetalleDemandaAnual>();

                        //Se van creando los objetos
                        for (int i = 0; i < 12; i++)
                        {
                            Entidades.DetalleDemandaAnual objeto = new Entidades.DetalleDemandaAnual();
                            objeto.Demanda = demanda;
                            objeto.Cantidadmes = Cantidad[i];
                            objeto.Mes = Meses[i];
                            detalle.Add(objeto);
                        }

                        if (estadoActual == estadoUI.calcularEstimacion || estadoActual == estadoUI.cargaHistorico)
                        {
                            //Se guardan los datos en la BD
                            detalle=BLL.DemandaAnualBLL.Insertar(demanda, detalle);
                            demanda.Codigo = detalle[0].Demanda.Codigo;
                        }

                        //SI ESTA GUARDANDO UN HISTORICO O EN CALCULO
                        if (estadoActual == estadoUI.calcularEstimacion || estadoActual == estadoUI.cargaHistorico)
                        {
                            //Se agrega todo al dataset
                            //La demanda anual (cabecera)
                            Data.dsEstimarDemanda.DEMANDAS_ANUALESRow rowDemanda = dsEstimarDemanda.DEMANDAS_ANUALES.NewDEMANDAS_ANUALESRow();
                            rowDemanda.BeginEdit();
                            rowDemanda.DEMAN_ANIO = demanda.Anio;
                            rowDemanda.DEMAN_CODIGO = demanda.Codigo;
                            rowDemanda.DEMAN_FECHACREACION = demanda.FechaCreacion;
                            rowDemanda.DEMAN_NOMBRE = demanda.Nombre;
                            rowDemanda.DEMAN_PARAMCRECIMIENTO = demanda.ParametroCrecimiento;
                            rowDemanda.EndEdit();
                            dsEstimarDemanda.DEMANDAS_ANUALES.AddDEMANDAS_ANUALESRow(rowDemanda);
                            dsEstimarDemanda.DEMANDAS_ANUALES.AcceptChanges();

                            //El detalle de la demanda anual

                            foreach (Entidades.DetalleDemandaAnual obje in detalle)
                            {
                                Data.dsEstimarDemanda.DETALLE_DEMANDAS_ANUALESRow rowDDemanda = dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.NewDETALLE_DEMANDAS_ANUALESRow();
                                rowDDemanda.BeginEdit();
                                rowDDemanda.DDEMAN_CODIGO = obje.Codigo;
                                rowDDemanda.DDEMAN_CANTIDADMES = obje.Cantidadmes;
                                rowDDemanda.DDEMAN_MES = obje.Mes;
                                rowDDemanda.DEMAN_CODIGO = obje.Demanda.Codigo;
                                rowDDemanda.EndEdit();
                                dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.AddDETALLE_DEMANDAS_ANUALESRow(rowDDemanda);
                                dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.AcceptChanges();
                            }

                            //Se debe recargar el control de los años base
                            CargarAñosBase();

                            MessageBox.Show("Los datos se han almacenado correctamente", "Informacion: Demanda Anual - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Seteo el estado de la interface a nuevo
                            SetInterface(estadoUI.nuevo);
                        }
                        else if (estadoActual == estadoUI.modificar)
                        {
                            //Se modifica en la BD
                            BLL.DemandaAnualBLL.Modificar(demanda, detalle);
                            
                            //Se modifica el dataset
                            //La demanda anual (cabecera)
                            Data.dsEstimarDemanda.DEMANDAS_ANUALESRow rowDemanda = dsEstimarDemanda.DEMANDAS_ANUALES.FindByDEMAN_CODIGO(demanda.Codigo);
                            rowDemanda.BeginEdit();
                            rowDemanda.DEMAN_ANIO = demanda.Anio;
                            rowDemanda.DEMAN_NOMBRE = demanda.Nombre;
                            rowDemanda.EndEdit();
                            dsEstimarDemanda.DEMANDAS_ANUALES.AcceptChanges();

                            //El detalle de la demanda anual

                            foreach (Entidades.DetalleDemandaAnual obje in detalle)
                            {
                                obje.Codigo = BLL.DetalleDemandaAnualBLL.ObtenerID(obje);
                                Data.dsEstimarDemanda.DETALLE_DEMANDAS_ANUALESRow rowDDemanda = dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.FindByDDEMAN_CODIGO(obje.Codigo);
                                rowDDemanda.BeginEdit();
                                rowDDemanda.DDEMAN_CANTIDADMES = obje.Cantidadmes;
                                rowDDemanda.EndEdit();
                                dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.AcceptChanges();
                            }

                            MessageBox.Show("Los datos se han Actualizado correctamente", "Informacion: Demanda Anual - Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Pongo la interface en el estado de busqueda
                            SetInterface(estadoUI.buscar);
                        }
                    }
                else 
                    {
                        MessageBox.Show(validacion, "Error: Demanda Anual - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Demanda Anual - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia: Demanda Anual - Modificación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCalcularEstimacion_Click(object sender, EventArgs e)
        {
            string errorValidacion=Validar(estadoUI.calcularEstimacion);

            if (errorValidacion == string.Empty)
            {
                //Verificamos el valor del parametro de crecimiento
                decimal paramCre = Convert.ToDecimal(numCrecimiento.Value);
                paramCre = paramCre / 100;

                //Verificamos los años seleccionados 
                IList<Entidades.DemandaAnual> años = new List<Entidades.DemandaAnual>();
                string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                años = AñosSeleccionados();

                //Se define la cantidad de años seleccionados
                int cantAños = años.Count;

                //se define una matriz de datos con los años y los meses
                int[,] mesesXaños = new int[12, cantAños];

                //Se llena la matriz con los datos
                int column = 0;
                foreach (Entidades.DemandaAnual a in años)
                {
                    //LLeno la matriz con los valores
                    for (int i = 0; i < 12; i++)
                    {
                        mesesXaños[i, column] = BLL.DetalleDemandaAnualBLL.CantidadAñoMes(a.Anio, a.Nombre, Meses[i]);
                    }
                    column += 1;
                }

                //Se Calcula el promedio por cada una de las filas
                decimal[] promedio = new decimal[12];

                for (int j = 0; j < 12; j++)
                {
                    for (int h = 0; h < cantAños; h++)
                    {
                        promedio[j] = promedio[j] + mesesXaños[j, h];
                    }

                    promedio[j] = promedio[j] / cantAños;
                    promedio[j] = Math.Round(promedio[j] + (promedio[j] * paramCre), 0);
                }

                //Se calcula el total de la estimacion
                

                for (int j = 0; j < 12; j++)
                {
                    totalsistema = totalsistema + promedio[j];
                }

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

                //Se asigna el valor al total
                txtTotal.Enabled = false;
                txtTotal.Text = totalsistema.ToString();
                lblTotalSistema.Text = totalsistema.ToString();

                //Se pone el estado de la interface
                SetInterface(estadoUI.calcularEstimacion);

               
                //Se genera el grafico
                GenerarGrafico(promedio);
            }
            else
            {
                MessageBox.Show(errorValidacion, "Error: Demanda Anual - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void btnModificarEstimacion_Click(object sender, EventArgs e)
        {
            //Se activan todos los controles que le permiten realizar modificaciones
            DesactivaControles(false);
        }
        #endregion

        #region Funciones Formulario

        private void CargarAñosBase()
        {
            //Creo el dataview y lo asigno al control de cheklist
            dvChAnios = new DataView(dsEstimarDemanda.DEMANDAS_ANUALES);
            chListAnios.Items.Clear();
            string anio, nombre, union;

            for (int i = 0; i <= (dsEstimarDemanda.DEMANDAS_ANUALES.Rows.Count - 1); i++)
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

            if (estado == estadoUI.calcularEstimacion || estado== estadoUI.modificar)
            {
                //Validacion del año
                if (txtAnio.Text.Length != 4) strError = strError + "-El año no tiene el tamaño adecuado\n";

                try
                {
                    int anio = Convert.ToInt32(txtAnio.Text);
                }
                catch (Exception) { strError = strError + "-El año no es un numero\n"; }

                //Validacion identificacion
                if (txtIdentificacion.Text == string.Empty) strError = strError + "-El nombre de identificación no puede estar vacío\n";
                
                if (estadoActual == estadoUI.calcularEstimacion)
                {
                    //Verifico que haya seleccionado alguna estimacion anterior
                    if (chListAnios.CheckedItems.Count == 0) strError = strError + "-En modo Calcular Estimacion por sistema se deben seleccionar datos anteriores para utilizar\n";
                }
            }
            if (estado == estadoUI.cargaHistorico)
            {
                //Validacion del año
                if (txtAnioHistorico.Text.Length != 4) strError = strError + "-El año no tiene el tamaño adecuado\n";

                try
                {
                    int anio = Convert.ToInt32(txtAnioHistorico.Text);
                }
                catch (Exception) { strError = strError + "-El año no es un numero\n"; }

                //Validacion identificacion
                if (txtDenominacionHistorico.Text == string.Empty) strError = strError + "-El nombre de identificación no puede estar vacío\n";
            }
            if (strError != string.Empty)
            {
                strError = "Errores de Validación:\n" + strError;
            }
            return strError;
        }

        private void GenerarGrafico(decimal[] promedio)
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

            for (int pointIndex = 0; pointIndex < promedio.Count(); pointIndex++)
            {
                plotY = Convert.ToInt32(promedio[pointIndex]);
                chartDemanda.Series[seriesGraficos.ToString()].Points.AddY(plotY);
            }

        }
        //Método para evitar la creación de más de una pantalla
        public static frmEstimarDemandaAnual Instancia
        {
            get
            {
                if (_frmEstimarDemandaAnual == null || _frmEstimarDemandaAnual.IsDisposed)
                {
                    _frmEstimarDemandaAnual = new frmEstimarDemandaAnual();
                }
                else
                {
                    _frmEstimarDemandaAnual.BringToFront();
                }
                return _frmEstimarDemandaAnual;
            }
            set
            {
                _frmEstimarDemandaAnual = value;
            }
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
            txtAnioHistorico.Text = string.Empty;
            txtDenominacionHistorico.Text = string.Empty;
        }

        private IList<Entidades.DemandaAnual> AñosSeleccionados()
        {
            IList<Entidades.DemandaAnual> años = new List<Entidades.DemandaAnual>();
            string contenido;
            int largo;
            for (int i = 0; i <= chListAnios.Items.Count - 1; i++)
            {
                Entidades.DemandaAnual demanda = new Entidades.DemandaAnual();
                chListAnios.SelectedIndex = i;
                if (chListAnios.GetItemChecked(i))
                {
                    contenido = chListAnios.SelectedItem.ToString();
                    demanda.Anio = Convert.ToInt32(contenido.Substring(0, 4));
                    largo = contenido.Length;
                    largo = largo - 5;
                    if (largo > 0) demanda.Nombre = contenido.Substring(5, largo);
                    años.Add(demanda);
                }
            }

            return años;
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

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "DEMAN_FECHACREACION":
                        DateTime fecha = Convert.ToDateTime(e.Value);
                        e.Value = fecha.ToString("dd/MM/yyyy");
                        break;
                    default:
                        break;
                }

            }

        }
        #endregion

        #region Controles

        private void numFebrero_ValueChanged(object sender, EventArgs e)
        {
            CalculaTotal();

            if (estadoActual == estadoUI.modificar || estadoActual == estadoUI.calcularEstimacion)
            {
                //lleno de nuevo un array con los datos
                decimal[] promedio = new decimal[12];

                //Se asignan los valores calculados a los textbox
                promedio[0] = Convert.ToDecimal(numEnero.Value);
                promedio[1] = Convert.ToDecimal(numFebrero.Value);
                promedio[2] = Convert.ToDecimal(numMarzo.Value);
                promedio[3] = Convert.ToDecimal(numAbril.Value);
                promedio[4] = Convert.ToDecimal(numMayo.Value);
                promedio[5] = Convert.ToDecimal(numJunio.Value);
                promedio[6] = Convert.ToDecimal(numJulio.Value);
                promedio[7] = Convert.ToDecimal(numAgosto.Value);
                promedio[8] = Convert.ToDecimal(numSeptiembre.Value);
                promedio[9] = Convert.ToDecimal(numOctubre.Value);
                promedio[10] = Convert.ToDecimal(numNoviembre.Value);
                promedio[11] = Convert.ToDecimal(numDiciembre.Value);

                //creo el grafico
                GenerarGrafico(promedio);
            }
        }

        private void numEnero_Enter_1(object sender, EventArgs e)
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
        private void numCrecimiento_Enter(object sender, EventArgs e)
        {
            numCrecimiento.Select(0, 10);
        }
        #endregion 



        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Selecciono el codigo de la demanda anual
            int codigo = Convert.ToInt32(dvListaDemanda[dgvLista.SelectedRows[0].Index]["deman_codigo"]);
            txtAnio.Text = dsEstimarDemanda.DEMANDAS_ANUALES.FindByDEMAN_CODIGO(codigo).DEMAN_ANIO.ToString();
            txtIdentificacion.Text = dsEstimarDemanda.DEMANDAS_ANUALES.FindByDEMAN_CODIGO(codigo).DEMAN_NOMBRE;
            numCrecimiento.Value = dsEstimarDemanda.DEMANDAS_ANUALES.FindByDEMAN_CODIGO(codigo).DEMAN_PARAMCRECIMIENTO;


            decimal[] promedio= new decimal[12];
            int cont=0;
            foreach (Data.dsEstimarDemanda.DETALLE_DEMANDAS_ANUALESRow dr in dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.Rows)
            {
                promedio[cont] =Convert.ToDecimal(dr.DDEMAN_CANTIDADMES);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Seteo el estado al inicio
            SetInterface(estadoUI.inicio);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Demanda Anual seleccionada y todo su detalle ?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtengo el Codigo de la demanda
                        int codigo = Convert.ToInt32(dvListaDemanda[dgvLista.SelectedRows[0].Index]["deman_codigo"]);
                        
                        //Pregunto si se puede eliminar
                        if (BLL.DemandaAnualBLL.PuedeEliminarse(codigo))
                        {
                            //Elimino la demanda anual y su detalle de la BD
                            BLL.DemandaAnualBLL.Eliminar(codigo);

                            //Limpio el dataset de detalles
                            dsEstimarDemanda.DETALLE_DEMANDAS_ANUALES.Clear();

                           //Lo eliminamos del dataset
                            dsEstimarDemanda.DEMANDAS_ANUALES.FindByDEMAN_CODIGO(codigo).Delete();
                            dsEstimarDemanda.DEMANDAS_ANUALES.AcceptChanges();

                            //Avisamos que se elimino 
                            MessageBox.Show("Se han eliminado los datos correctamente" , "Información: Elemento Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Ponemos la ventana en el estado inicial
                            SetInterface(estadoUI.inicio);
                        }
                        else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
                        
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
                MessageBox.Show("Debe seleccionar una Designación de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LlenarDetalle();
        }

        
       

       
       

        













    }
}
