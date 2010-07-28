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
    public partial class frmPlanAnual : Form
    {
        private static frmPlanAnual _frmPlanAnual = null;
        private Data.dsPlanAnual dsPlanAnual = new GyCAP.Data.dsPlanAnual();
        private DataView dvListaPlanes, dvListaDetalle, dvChAnios, dvComboEstimaciones;
        private enum estadoUI { inicio, nuevo, buscar, modificar, calcularPlanificacion };
        private static estadoUI estadoActual;


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


            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Llenamos el dataset con las estimaciones
            BLL.DemandaAnualBLL.ObtenerTodos(dsPlanAnual);
                        
            //Cargamos el combo
            dvComboEstimaciones = new DataView(dsPlanAnual.DEMANDAS_ANUALES);
            string[] display = { "deman_anio" , "deman_nombre" };
            cbEstimacionDemanda.SetDatos(dvComboEstimaciones, "deman_codigo", display,"-" , "Seleccione", false);


            //Seteamos los maxlength de los controles
            txtAnio.MaxLength = 4;
            txtAnioBuscar.MaxLength = 4;

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
                    tcPlanAnual.SelectedTab = tpBuscar;
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
                    numCrecimiento.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnCalcularEstimacion.Enabled = false;
                    chListAnios.Visible = false;
                    gbModificacion.Visible = false;
                    gbGraficoEstimacion.Visible = true;
                    gbEstimacionMes.Visible = true;
                    gbBotones.Visible = true;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
                    break;

                case estadoUI.nuevo:
                    txtAnio.Focus();
                    txtAnio.Text = string.Empty;
                    numCrecimiento.Value = 0;
                    numCrecimiento.Enabled = true;
                    chListAnios.Visible = true;
                    btnCalcularEstimacion.Enabled = true;
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
                    break;                
                
                default:
                    break;

            }

        }



        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*try
            {
                //Limpiamos el Dataset
                dsPlanAnual.PLANES_ANUALES.Clear();

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
            }*/
            


        }
    }
}
