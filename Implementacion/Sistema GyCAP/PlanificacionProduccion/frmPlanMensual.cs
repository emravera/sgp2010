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
        private DataView dvListaPlanes, dvListaDetalle, dvComboPlanesAnuales, dvComboCocinas;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        
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
            dgvLista.Columns.Add("PMES_MES", "Mes del Plan Mensual");
            dgvLista.Columns.Add("PMES_FECHACREACION", "Fecha Creación Plan Mensual");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvLista.Columns["PAN_CODIGO"].DataPropertyName = "PAN_CODIGO";
            dgvLista.Columns["PMES_MES"].DataPropertyName = "PMES_MES";
            dgvLista.Columns["PMES_FECHACREACION"].DataPropertyName = "PMES_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanMensual.PLANES_MENSUALES);
            dvListaPlanes.Sort = "PMES_CODIGO ASC";
            dgvLista.DataSource = dvListaPlanes;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPMES_CODIGO", "Código");
            dgvDetalle.Columns.Add("PMES_CODIGO", "Mes");
            dgvDetalle.Columns.Add("COC_CODIGO", "Mes");
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
            dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //Ponemos las columnas en visible false
            dgvLista.Columns["PMES_CODIGO"].Visible = false;
            dgvDetalle.Columns["DPMES_CODIGO"].Visible = false;

            //Llenamos el dataset con los planes anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanMensual);

            //Llenamos el detalle del Plan Anual
            BLL.DetallePlanAnualBLL.ObtenerDetalle(dsPlanMensual);

            //Llenamos el dataset de Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsPlanMensual.COCINAS);
            
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
                    cbMes.SelectedIndex = -1;
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
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    break;

                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
                    break;

                case estadoUI.nuevo:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    cbMesDatos.SelectedIndex = -1;
                    cbPlanAnual.SelectedIndex = -1;
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

      
        

        

       
    }
}
