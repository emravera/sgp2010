using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Mantenimiento
{
    public partial class frmPlanMantenimiento : Form
    {
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private static frmPlanMantenimiento _frmPlanMantenimiento = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvMaquina, dvEstadoMaquina, dvEstadoMaquinaBuscar,
                         dvPlan, dvDetallePlan, dvEmpleados, dvEstadoDetalle,
                         dvEstadoPlan;

        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;

        public frmPlanMantenimiento()
        {
            InitializeComponent();

            //Setea todas las grillas y las vistas
            setGrillasVistasCombo();

            //Setea todos los controles necesarios para el efecto de slide
            SetSlide();

            //Seteamos el estado de la interfaz
            //SetInterface(estadoUI.inicio);

        }

        private void SetSlide()
        {
            //gbDatos.Parent = slideDatos;
            //gbMaquinas.Parent = slideAgregar;
            //slideControl.AddSlide(slideAgregar);
            //slideControl.AddSlide(slideDatos);
            //slideControl.Selected = slideDatos;
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvPlan.AutoGenerateColumns = false;
            dgvDetallePlan.AutoGenerateColumns = false;
            dgvMaquinas.AutoGenerateColumns = false;

            //Agregamos las columnas y sus propiedades
            dgvPlan.Columns.Add("PED_CODIGO", "Código");
            dgvPlan.Columns.Add("PED_NUMERO", "Número");
            dgvPlan.Columns.Add("CLI_CODIGO", "Cliente");
            dgvPlan.Columns.Add("PED_FECHAENTREGAPREVISTA", "Fecha Prevista");
            dgvPlan.Columns.Add("EPED_CODIGO", "Estado");
            dgvPlan.Columns.Add("PED_OBSERVACIONES", "Observaciones");
            dgvPlan.Columns["PED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlan.Columns["PED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlan.Columns["PED_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlan.Columns["PED_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlan.Columns["CLI_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlan.Columns["PED_FECHAENTREGAPREVISTA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlan.Columns["PED_FECHAENTREGAPREVISTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlan.Columns["PED_FECHAENTREGAPREVISTA"].MinimumWidth = 110;
            dgvPlan.Columns["EPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPlan.Columns["PED_OBSERVACIONES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Alineacion de los numeros y las fechas en la grilla
            dgvPlan.Columns["PED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlan.Columns["PED_CODIGO"].Visible = false;

            //dgvDetallePlan.Columns.Add("DPED_CODIGO", "Codigo");
            dgvDetallePlan.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetallePlan.Columns.Add("DPED_CANTIDAD", "Cantidad");
            dgvDetallePlan.Columns.Add("EDPED_CODIGO", "Estado");

            //dgvDetallePlan.Columns["DPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["COC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPED_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns["DPED_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetallePlan.Columns["EDPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Alineacion de los numeros y las fechas en la grilla
            //dgvDetallePlan.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDetallePlan.Columns["DPED_CODIGO"].Visible = false;


            dgvMaquinas.Columns.Add("COC_CODIGO_PRODUCTO", "Código");
            dgvMaquinas.Columns.Add("MOD_CODIGO", "Modelo");
            dgvMaquinas.Columns.Add("MCA_CODIGO", "Marca");
            //dgvMaquinas.Columns.Add("COC_ESTADO", "Estado");
            dgvMaquinas.Columns.Add("COC_COSTO", "Costo");
            dgvMaquinas.Columns["COC_CODIGO_PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMaquinas.Columns["MOD_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMaquinas.Columns["MCA_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMaquinas.Columns["COC_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMaquinas.Columns["COC_COSTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvMaquinas.Columns["COC_ESTADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //Indicamos de dónde van a sacar los datos cada columna
            dgvPlan.Columns["PED_CODIGO"].DataPropertyName = "PED_CODIGO";
            dgvPlan.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvPlan.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvPlan.Columns["PED_FECHAENTREGAPREVISTA"].DataPropertyName = "PED_FECHAENTREGAPREVISTA";
            dgvPlan.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvPlan.Columns["PED_OBSERVACIONES"].DataPropertyName = "PED_OBSERVACIONES";

            //dgvDetallePlan.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePlan.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePlan.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            dgvDetallePlan.Columns["EDPED_CODIGO"].DataPropertyName = "EDPED_CODIGO";

            dgvMaquinas.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvMaquinas.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvMaquinas.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            //dgvMaquinas.Columns["COC_ESTADO"].DataPropertyName = "COC_ESTADO";
            dgvMaquinas.Columns["COC_COSTO"].DataPropertyName = "COC_COSTO";

            //Creamos el dataview y lo asignamos a la grilla
            dvPlan = new DataView(dsMantenimiento.PLANES_MANTENIMIENTO);
            dvPlan.Sort = "PMAN_CODIGO ASC";
            dgvPlan.DataSource = dvPlan;

            dvDetallePlan = new DataView(dsMantenimiento.DETALLE_PLANES_MANTENIMIENTO);
            dgvDetallePlan.DataSource = dvDetallePlan;

            dvMaquina = new DataView(dsMantenimiento.MAQUINAS);
            dvMaquina.Sort = "MAQ_CODIGO ASC";
            dgvMaquinas.DataSource = dvMaquina;

            dvEmpleados = new DataView(dsMantenimiento.EMPLEADOS);
            dvEmpleados.Sort = "E_APELLIDO, E_NOMBRE ASC ";

            dvEstadoPlan = new DataView(dsMantenimiento.ESTADO_PLANES_MANTENIMIENTO);
            dvEstadoPlan.Sort = "EPMAN_NOMBRE";

            dvEstadoDetalle = new DataView(dsMantenimiento.ESTADO_DETALLE_MANTENIMIENTOS);
            dvEstadoDetalle.Sort = "EDMAN_CODIGO";

            //Obtenemos las terminaciones, los planos, los estados de las piezas, las MP, unidades medidas, hojas ruta
            try
            {
                BLL.EstadoPlanMantenimientoBLL.ObtenerTodos(dsMantenimiento.ESTADO_PLANES_MANTENIMIENTO);
                BLL.EstadoDetalleMantenimientoBLL.ObtenerTodos(dsMantenimiento.ESTADO_DETALLE_MANTENIMIENTOS); 
                BLL.MaquinaBLL.ObtenerMaquinas(dsMantenimiento.MAQUINAS);
                BLL.EmpleadoBLL.ObtenerEmpleados(dsMantenimiento.EMPLEADOS);
                BLL.TipoRepuestoBLL.ObtenerTodos(dsMantenimiento.TIPOS_REPUESTOS);
                BLL.RepuestoBLL.ObtenerTodos(dsMantenimiento.REPUESTOS);
                //BLL.MarcaBLL.ObtenerTodos(dsMantenimiento.MARCAS);
                //BLL.ModeloCocinaBLL.ObtenerTodos(dsMantenimiento.MODELOS_COCINAS);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //dvEstadoPlaedidoBuscar = new DataView(dsMantenimiento.ESTADO_PEDIDOS);

            //cboEstadoBuscar.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "--TODOS--", true);
            //cboEstado.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "", false);
            //cboClientes.SetDatos(dvEmpleados, "CLI_CODIGO", "CLI_RAZONSOCIAL", "", false);

        }

    }
}
