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
    public partial class frmEstimarDemandaAnual : Form
    {
        private static frmEstimarDemandaAnual _frmEstimarDemandaAnual = null;
        private Data.dsEstimarDemanda dsEstimarDemanda = new GyCAP.Data.dsEstimarDemanda();
        private DataView dvListaDemanda, dvListaDetalle, dvChAnios;
        private enum estadoUI { inicio, nuevo, modificar, cargaHistorico };
        
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
            dgvLista.Columns.Add("DEMAN_FECHAINICIO", "Fecha Inicio Plan");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["DEMAN_CODIGO"].DataPropertyName = "DEMAN_CODIGO";
            dgvLista.Columns["DEMAN_ANIO"].DataPropertyName = "DEMAN_ANIO";
            dgvLista.Columns["DEMAN_FECHAINICIO"].DataPropertyName = "DEMAN_FECHAINICIO";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDemanda = new DataView(dsEstimarDemanda.DEMANDAS_ANUALES);
            dvListaDemanda.Sort = "DEMAN_ANIO ASC";
            dgvLista.DataSource = dvListaDemanda;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DDEMAN_CODIGO", "Código");
            dgvDetalle.Columns.Add("DDEMAN_MES", "Mes");
            dgvDetalle.Columns.Add("DDEMAN_CANTIDADMES", "Fecha Inicio Plan");

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
            dvListaDetalle.Sort = "DDEMAN_CODIGO ASC";
            dgvDetalle.DataSource = dvListaDetalle;

            //Seteo el maxlenght de los textbox
            
            txtAnioBuscar.MaxLength = 4;

            //Seteo el estado de inicio de la pantalla
            SetInterface(estadoUI.inicio);
            
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    gbGrillaDemanda.Visible = false;
                    gbGrillaDetalle.Visible = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnEliminar.Enabled = true;
                    tcDemanda.SelectedTab= tpBuscar;
                    break;
                case estadoUI.modificar:
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
                    break;
                case estadoUI.nuevo:
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = false;
                    gbGraficoEstimacion.Visible = false;
                    gbBotones.Visible = false;
                    gbDatosHistoricos.Visible = false;
                    gbDatosPrincipales.Enabled = true;
                    tcDemanda.SelectedTab = tpDatos;
                    break;
                case estadoUI.cargaHistorico:
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    gbEstimacionMes.Visible = true;
                    gbGraficoEstimacion.Visible = false;
                    gbDatosHistoricos.Visible = true;
                    gbBotones.Visible = true;
                    gbDatosPrincipales.Enabled = false;
                    tcDemanda.SelectedTab = tpDatos;
                    break;

                    default:
                    break;

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsEstimarDemanda.DEMANDAS_ANUALES.Clear();
                
                //Valido que el año
                if (txtAnioBuscar.Text.Length != 4) throw new Exception();

                int anio = Convert.ToInt32(txtAnioBuscar.Text);

                //Se llama a la funcion de busqueda con todos los parametros
                BLL.DemandaAnualBLL.ObtenerTodos(anio, dsEstimarDemanda);
                
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDemanda.Table = dsEstimarDemanda.DEMANDAS_ANUALES;

                if (dsEstimarDemanda.DEMANDAS_ANUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Demandas Anuales con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetInterface(estadoUI.modificar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Demanda Anual - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SetInterface(estadoUI.modificar);
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

            //Creo el dataview y lo asigno al control de cheklist
            dvChAnios = new DataView(dsEstimarDemanda.DEMANDAS_ANUALES);
            string anio;
            for (int i = 0; i <= (dsEstimarDemanda.DEMANDAS_ANUALES.Rows.Count - 1); i++)
            {
                anio = dvChAnios[i]["deman_anio"].ToString();
                chListAnios.Items.Add(anio);
            }

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
            SetInterface(estadoUI.nuevo);
        }

        private void Validar()
        {
            try
            {
                if (txtAnio.Text.Length != 4) throw new Exception();
                int anio = Convert.ToInt32(txtAnio.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("El año no tiene el formato Correcto", "Error: Demanda Anual - Carga de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
       

       

        

       

      
        
        
    }
}
