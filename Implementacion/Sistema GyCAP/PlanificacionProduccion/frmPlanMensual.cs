using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmPlanMensual : Form
    {
        private static frmPlanMensual _frmPlanMensual = null;
        private Data.dsPlanMensual dsPlanMensual = new GyCAP.Data.dsPlanMensual();
        private DataView dvListaPlanes, dvListaDetalle, dvListaDatos, dvComboPlanesAnuales, dvComboCocinas;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        private static int cantidadPlanificada; int codigoDetalle = -1;
        
        public frmPlanMensual()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;

            //Para cada Lista
            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("PMES_CODIGO", "Código");
            dgvLista.Columns.Add("PAN_CODIGO", "Plan Anual");
            dgvLista.Columns.Add("PAN_ANIO", "Anio Plan Anual");
            dgvLista.Columns.Add("PMES_MES", "Mes del Plan Mensual");
            dgvLista.Columns.Add("PMES_FECHACREACION", "Fecha Creación Plan Mensual");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvLista.Columns["PAN_CODIGO"].DataPropertyName = "PAN_CODIGO";
            dgvLista.Columns["PAN_ANIO"].DataPropertyName = "PAN_ANIO";
            dgvLista.Columns["PMES_MES"].DataPropertyName = "PMES_MES";
            dgvLista.Columns["PMES_FECHACREACION"].DataPropertyName = "PMES_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanMensual.PLANES_MENSUALES);
            dgvLista.DataSource = dvListaPlanes;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPMES_CODIGO", "Código");
            dgvDetalle.Columns.Add("PMES_CODIGO", "Mes");
            dgvDetalle.Columns.Add("COC_CODIGO", "Cocina Codigo");
            dgvDetalle.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDetalle.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvDetalle.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvDetalle.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalle.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDetalle.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalle.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

             //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //Lista de Datos
            //Agregamos la columnas
            dgvDatos.Columns.Add("DPMES_CODIGO", "Código");
            dgvDatos.Columns.Add("PMES_CODIGO", "Mes");
            dgvDatos.Columns.Add("DIAPMES_CODIGO", "Dia");
            dgvDatos.Columns.Add("COC_CODIGO", "Cocina Codigo");
            dgvDatos.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDatos.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatos.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvDatos.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvDatos.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDatos.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDatos.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvDatos.Columns[0].Visible = false;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDatos = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDatos.DataSource = dvListaDatos;


            //LLenado de Datasets
            //Llenamos el dataset con los planes anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanMensual.PLANES_ANUALES);

            //Llenamos el detalle del Plan Anual
            BLL.DetallePlanAnualBLL.ObtenerDetalle(dsPlanMensual);

            //Llenamos el dataset de Cocinas
            BLL.CocinaBLL.ObtenerCocinasSinCosto(dsPlanMensual.COCINAS);
            
            //Cargamos el combo de los meses
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int cont = 0; int[] valores = new int[12];

            foreach (string l in Meses)
            {
                valores[cont] = cont;
                cont++;
            }
            
            //Cargamos el combo de los planes anuales
            //Cargamos el combo
            dvComboPlanesAnuales = new DataView(dsPlanMensual.PLANES_ANUALES);
            cbPlanAnual.SetDatos(dvComboPlanesAnuales, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el combo con las cocinas
            dvComboCocinas = new DataView(dsPlanMensual.COCINAS);
            cbCocinas.SetDatos(dvComboCocinas, "coc_codigo", "coc_codigo_producto", "Seleccione", false);

            //Cargo los combos de los meses
            cbMes.SetDatos(Meses, valores, "Seleccione", false);
            cbMesDatos.SetDatos(Meses, valores, "Seleccione", false);


            //Seteamos los maxlength de los controles y los tipos de numeros
            txtAnioBuscar.MaxLength = 4;

            //Setemoa el valor de la interface
            SetInterface(estadoUI.inicio);
           
            }

        //Método para evitar la creación de más de una pantalla
        public static frmPlanMensual Instancia
        {
            get
            {
                if (_frmPlanMensual == null || _frmPlanMensual.IsDisposed)
                {
                    _frmPlanMensual = new frmPlanMensual();
                }
                else
                {
                    _frmPlanMensual.BringToFront();
                }
                return _frmPlanMensual;
            }
            set
            {
                _frmPlanMensual = value;
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
                    cbMes.SetSelectedIndex(-1);
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.inicio;
                    break;

                //Cuando termina de Buscar
                case estadoUI.buscar:
                    btnNuevo.Enabled = true;
                    bool hayDatos;
                    if (dsPlanMensual.PLANES_MENSUALES.Rows.Count > 0)
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
                    dgvLista.Columns["PMES_CODIGO"].Visible = false;
                    dgvLista.Columns["PAN_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPMES_CODIGO"].Visible = false;
                    dgvDetalle.Columns["PMES_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPMES_CANTIDADREAL"].Visible = false;
                    break;
                //Cuando se carga el Detalle
                case estadoUI.cargaDetalle:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.cargaDetalle;
                    //Manejo de Controles
                    //Groupbox
                    gbDatosPrincipales.Enabled = false;
                    gbCantidades.Visible = true;
                    gbCargaDetalle.Visible = true;
                    gbDetalleGrilla.Visible = true;
                    gbBotones.Visible = true;
                    //Textbox
                    txtCantAPlanificar.Text = string.Empty;
                    txtCantPlanificada.Text = string.Empty;
                    txtRestaPlanificar.Text = string.Empty;
                    txtCantAPlanificar.Enabled = false;
                    txtCantPlanificada.Enabled = false;
                    txtRestaPlanificar.Enabled = false;
                    //Combo
                    cbCocinas.SetSelectedIndex(-1);
                    //Numeric 
                    numPorcentaje.Value = 0;
                    //Radiobuttons
                    rbUnidades.Checked = true;
                    //Escondo las columnas que no quiero mostrar de la grilla
                    dgvDatos.Columns["DPMES_CODIGO"].Visible = false;
                    dgvDatos.Columns["PMES_CODIGO"].Visible = false;
                    dgvDatos.Columns["DPMES_CANTIDADREAL"].Visible = false;
                    break;
                    
                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    SetInterface(estadoUI.cargaDetalle);
                    estadoActual = estadoUI.modificar;
                    break;

                case estadoUI.nuevo:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    cbMesDatos.SetSelectedIndex(-1);
                    cbPlanAnual.SetSelectedIndex(-1);
                    gbDatosPrincipales.Enabled = true;
                    gbCantidades.Visible = false;
                    gbCargaDetalle.Visible = false;
                    gbDetalleGrilla.Visible = false;
                    gbBotones.Visible = false;
                    estadoActual = estadoUI.nuevo;
                    break;
                

                default:
                    break;

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int anio; string mes;

                //Limpiamos el Dataset
                dsPlanMensual.PLANES_MENSUALES.Clear();

                if (txtAnioBuscar.Text != string.Empty)
                {
                    //Valido que el año
                    if (txtAnioBuscar.Text.Length != 4) throw new Exception();

                   anio = Convert.ToInt32(txtAnioBuscar.Text);
                }
                else
                {
                    anio = 0;

                }
                if (cbMes.SelectedIndex != -1)
                {
                    mes = cbMes.GetSelectedText();
                }
                else
                {
                    mes = string.Empty;
                }

                BLL.PlanMensualBLL.ObtenerTodos(anio, mes, dsPlanMensual);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanes.Table = dsPlanMensual.PLANES_MENSUALES;

                if (dsPlanMensual.PLANES_MENSUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Planes Mensuales con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El año no tiene el formato Correcto", "Error: Plan Mensual - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);

                //Se llama a la funcion de busqueda del detalle
                BLL.DetallePlanMensualBLL.ObtenerDetalle(codigo, dsPlanMensual);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsPlanMensual.DETALLE_PLANES_MENSUALES;

                if (dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para ese Plan Mensual.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //muestro el groupbox del detalle
                    gbGrillaDetalle.Visible = true;
                    SetInterface(estadoUI.buscar);
                    gbGrillaDetalle.Visible = true;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvDetalle.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        string nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Seteo la variable global a cero
                cantidadPlanificada = 0;

                //Limpiamos el dataset de detalles
                dsPlanMensual.DETALLE_PLAN_ANUAL.Clear();

                if (cbPlanAnual.SelectedIndex != -1 && cbMesDatos.SelectedIndex != -1)
                {
                    int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());
                    string mes = cbMesDatos.GetSelectedText();

                    //Verifico que no exista ningun plan mensual para esos datos
                    if (BLL.PlanMensualBLL.ExistePlanMensual(anio, mes) == true)
                    {
                        SetInterface(estadoUI.cargaDetalle);

                        //Cargo el valor que debe planificar para ese mes
                        int cantidad = BLL.PlanAnualBLL.ObtenerTodos(anio, mes);
                        txtCantAPlanificar.Text = cantidad.ToString();
                        txtCantPlanificada.Text =Convert.ToString(0);
                        txtRestaPlanificar.Text = cantidad.ToString();

                        //Borro los detalles del dataset
                        dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Ya existe un plan Mensual para ese año y mes seleccionado", "Error: Plan Mensual - Carga de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un Plan Anual y un Mes para Cargar el Detalle", "Error: Plan Mensual - Carga de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Carga Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.cargaDetalle);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void rbUnidades_CheckedChanged(object sender, EventArgs e)
        {
            numUnidades.Visible = true;
            numPorcentaje.Visible = false;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void rbPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            numUnidades.Visible = false;
            numPorcentaje.Visible = true;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int codigoPlan=-1; int cantidad;
            try
            {
                //Ejecutamos la validacion para verificar que este todo OK
                ValidarDetalle();

                //Comenzamos con la edición de la fila en si misma
                Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row = dsPlanMensual.DETALLE_PLANES_MENSUALES.NewDETALLE_PLANES_MENSUALESRow();
                row.BeginEdit();
                row.DPMES_CODIGO = codigoDetalle--;
                row.PMES_CODIGO = codigoPlan;
                row.COC_CODIGO =Convert.ToInt32(cbCocinas.GetSelectedValue());
                if (rbUnidades.Checked == true)
                {
                    cantidad= Convert.ToInt32(numUnidades.Value);
                    row.DPMES_CANTIDADESTIMADA =cantidad;                    
                }
                else
                {
                    cantidad = Convert.ToInt32(Convert.ToInt32(txtCantAPlanificar.Text) * (numPorcentaje.Value / 100));
                    row.DPMES_CANTIDADESTIMADA = cantidad;
                    
                }
                row.EndEdit();
                dsPlanMensual.DETALLE_PLANES_MENSUALES.AddDETALLE_PLANES_MENSUALESRow(row);

                //Metodo que recalcula los valores Ingresados
                CalcularCantidades(cantidad);

                //Seteamos la interface al estado de carga de detalle
                cbCocinas.SetSelectedIndex(-1);
                numPorcentaje.Value = 0;
                numUnidades.Value = 0;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Validación - Carga Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalcularCantidades(int cantidadDetalle)
        {
            //Tengo los valores
           int cantidadAPlanificar = Convert.ToInt32(txtCantAPlanificar.Text);
           cantidadPlanificada = cantidadPlanificada + cantidadDetalle;
           int restaPlanificar =cantidadAPlanificar - cantidadPlanificada;

            //Los asigno a los textbox
            txtCantPlanificada.Text = cantidadPlanificada.ToString();
            if (restaPlanificar > 0)
            {
                txtRestaPlanificar.Text = restaPlanificar.ToString();
            }
            else txtRestaPlanificar.Text =Convert.ToString(0);

        }

        private void ValidarDetalle()
        {
            string msjerror=string.Empty;

            if (cbCocinas.SelectedIndex == -1) msjerror = msjerror + "-Debe seleccionar un modelo de cocina\n";
            if (rbUnidades.Checked == true)
            {
                if (numUnidades.Value == 0) msjerror = msjerror + "-La cantidad en unidades debe ser mayor a cero\n";
            }
            if (rbPorcentaje.Checked == true)
            {
                if (numPorcentaje.Value == 0) msjerror = msjerror + "-El porcentaje debe ser mayor a cero\n";
            }

           
            //Validamos que no se quiera agregar un modelo que ya está en el dataset
            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows)
            {
                if (row["COC_CODIGO"].ToString() == Convert.ToString(cbCocinas.GetSelectedValue()))
                {
                    msjerror = msjerror + "-El modelo de cocina que intenta agregar ya se encuentra en la planificación\n";
                }
            }
            
            if (msjerror.Length > 0)
            {
                msjerror = "Los errores encontrados son:\n" + msjerror;
                throw new Exception(msjerror);
            }
        }

        private void dgvDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvDatos.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        string nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);
                int cantidad =Convert.ToInt32(dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA);
                //Sumo las cantidades
                CalcularCantidades(cantidad * -1);

                //Elimino el dataset
                dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (1 < (Convert.ToInt32(txtCantAPlanificar.Text) - Convert.ToInt32(txtCantPlanificada.Text))) 
            {
                if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);
                    dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA += 1;

                    //Llamo a la función que recalcula los valores
                    CalcularCantidades(1);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("La cantidad de unidades no puede ser mayor de lo que resta planificar", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCantPlanificada.Text) >= 1)
            {
                if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);
                    dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA -= 1;

                    //Llamo a la función que recalcula los valores
                    CalcularCantidades(-1);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("La cantidad de unidades no puede ser mayor de lo que ya se planificó", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos el objeto de plan anual
                Entidades.PlanAnual planAnual = new GyCAP.Entidades.PlanAnual();
                planAnual.Codigo =Convert.ToInt32(cbPlanAnual.GetSelectedValue());
                planAnual.Anio =Convert.ToInt32(cbPlanAnual.GetSelectedText());

                //Creamos el objeto del plan mensual
                Entidades.PlanMensual planMensual = new GyCAP.Entidades.PlanMensual();
                planMensual.FechaCreacion = BLL.DBBLL.GetFechaServidor();
                planMensual.Mes = cbMesDatos.GetSelectedText();
                planMensual.PlanAnual = planAnual;

                if (estadoActual == estadoUI.cargaDetalle)
                {
                    //Enviamos los datos para guardar
                    BLL.PlanMensualBLL.GuardarPlan(planMensual, dsPlanMensual);
                }
                else if (estadoActual == estadoUI.modificar)
                {
                    planMensual.Codigo=Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);
                    BLL.PlanMensualBLL.GuardarPlanModificado(planMensual, dsPlanMensual);
                }
                //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                dsPlanMensual.AcceptChanges();
                
                MessageBox.Show("Los datos se han almacenado correctamente", "Informacion: Demanda Anual - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Vuelvo al estado inicial de la interface
                SetInterface(estadoUI.inicio);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Guardado Plan Mensual", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            //Datos de la cabecera
            //Selecciono el codigo de la demanda anual
            int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);

                int codigoPlanAnual = Convert.ToInt32(dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).PAN_CODIGO);
                int anio = Convert.ToInt32(dsPlanMensual.PLANES_ANUALES.FindByPAN_CODIGO(codigoPlanAnual).PAN_ANIO);

                cbPlanAnual.SetSelectedValue(Convert.ToInt32(dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).PAN_CODIGO));

                string mes = dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).PMES_MES.ToString();

                //Motodo que me busca el valuemember de un mes
                string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                int cont = 0;
                foreach (string l in Meses)
                {
                    if (mes == Meses[cont]) break;
                    cont++;
                }

                cbMesDatos.SetSelectedIndex(cont);

                //Seteo la interface al estado modificar
                SetInterface(estadoUI.modificar);

                //Datos calculados
                //Cargo el valor que debe planificar para ese mes
                int cantidad = BLL.PlanAnualBLL.ObtenerTodos(anio, mes);
                txtCantAPlanificar.Text = cantidad.ToString();
                txtCantPlanificada.Text = BLL.PlanMensualBLL.CalcularTotal(anio, mes).ToString();
                cantidadPlanificada = Convert.ToInt32(txtCantPlanificada.Text) + cantidadPlanificada;

                int restaPlanificar = cantidad - Convert.ToInt32(txtCantPlanificada.Text);
                if (restaPlanificar > 0) txtRestaPlanificar.Text = restaPlanificar.ToString();
                else txtRestaPlanificar.Text = Convert.ToString(0);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el Plan Mensual seleccionada y todo su detalle ?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtengo el Codigo del plan
                        int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pan_codigo"]);

                        //Pregunto si se puede eliminar
                        if (BLL.PlanMensualBLL.ExistePlanSemanal(codigo) == true)
                        {
                            //Elimino el plan anual y su detalle de la BD
                            BLL.PlanAnualBLL.Eliminar(codigo);

                            //Limpio el dataset de detalles
                            dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                            //Lo eliminamos del dataset
                            dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).Delete();
                            dsPlanMensual.PLANES_MENSUALES.AcceptChanges();

                            //Avisamos que se elimino 
                            MessageBox.Show("Se han eliminado los datos correctamente", "Información: Elemento Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Designación de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        

        

        

       

        

        

      
        

        

       
    }
}
