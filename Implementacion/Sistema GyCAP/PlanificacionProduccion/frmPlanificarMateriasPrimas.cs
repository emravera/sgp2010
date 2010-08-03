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
    public partial class frmPlanificarMateriasPrimas : Form
    {
        private static frmPlanificarMateriasPrimas _frmPlanificarMateriasPrimas = null;
        private Data.dsPlanMateriasPrimas dsPlanMP = new GyCAP.Data.dsPlanMateriasPrimas();
        private DataView dvListaPlanMP, dvListaDetalle, dvComboPlanificacion;
        private enum estadoUI { inicio, nuevo, buscar, calcular };
        private static estadoUI estadoActual;

        public frmPlanificarMateriasPrimas()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;

            //Para cada Lista
            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("PMPA_CODIGO", "Código");
            dgvLista.Columns.Add("PMPA_ANIO", "Año");
            dgvLista.Columns.Add("PMPA_MES", "Mes");
            dgvLista.Columns.Add("PMPA_FECHACREACION", "Fecha Creación Plan");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PMPA_CODIGO"].DataPropertyName = "PMPA_CODIGO";
            dgvLista.Columns["PMPA_ANIO"].DataPropertyName = "PMPA_ANIO";
            dgvLista.Columns["PMPA_MES"].DataPropertyName = "PMPA_MES";
            dgvLista.Columns["PMPA_FECHACREACION"].DataPropertyName = "PMPA_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanMP = new DataView(dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES);
            dvListaPlanMP.Sort = "PMPA_CODIGO ASC";
            dgvLista.DataSource = dvListaPlanMP;

            //Lista de Detalles
            //Agregamos la columnas
            //dgvDetalle.Columns.Add("DPMPA_CODIGO", "Código");
            dgvDetalle.Columns.Add("MP_CODIGO", "Materia Prima");
            dgvDetalle.Columns.Add("DPMPA_CANTIDAD", "Cantidad Mensual");
            dgvDetalle.Columns.Add("UMED_CODIGO", "Unidad Medida");

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            //dgvDetalle.Columns["DPMPA_CODIGO"].DataPropertyName = "DPMPA_CODIGO";
            dgvDetalle.Columns["MP_CODIGO"].DataPropertyName = "MP_CODIGO";
            dgvDetalle.Columns["DPMPA_CANTIDAD"].DataPropertyName = "DPMPA_CANTIDAD";
            dgvDetalle.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";

            dgvLista.Columns["PMPA_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL);
            dvListaDetalle.Sort = "DPMPA_CODIGO ASC";
            dgvDetalle.DataSource = dvListaDetalle;

            //Llenamos el dataset con las materias primas
            BLL.MateriaPrimaBLL.ObtenerMP(dsPlanMP);

            //Llenamos el dataset con las unidades de medida
            BLL.UnidadMedidaBLL.ObtenerUnidades(dsPlanMP);

            //Llenamos el dataset con las estimaciones
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanMP);

            //Cargamos el combo
            dvComboPlanificacion = new DataView(dsPlanMP.PLANES_ANUALES);
            cbPlanificacion.SetDatos(dvComboPlanificacion, "pan_codigo", "pan_anio" , "Seleccione", false);

            //Seteo el estado de inicio de la pantalla
            SetInterface(estadoUI.inicio);

        }

        //Método para evitar la creación de más de una pantalla
        public static frmPlanificarMateriasPrimas Instancia
        {
            get
            {
                if (_frmPlanificarMateriasPrimas == null || _frmPlanificarMateriasPrimas.IsDisposed)
                {
                    _frmPlanificarMateriasPrimas = new frmPlanificarMateriasPrimas();
                }
                else
                {
                    _frmPlanificarMateriasPrimas.BringToFront();
                }
                return _frmPlanificarMateriasPrimas;
            }
            set
            {
                _frmPlanificarMateriasPrimas = value;
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
                    gbBuscar.Visible = true;
                    gbDatosPrincipales.Visible = false;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoActual = estadoUI.inicio;
                    break;
                case estadoUI.nuevo:
                    txtAnioBuscar.Text = string.Empty;
                    gbGrillaDemanda.Visible = false;
                    gbGrillaDetalle.Visible = false;
                    gbBuscar.Visible = false;
                    gbDatosPrincipales.Visible = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoActual = estadoUI.inicio;
                    break;
                case estadoUI.buscar:
                    txtAnioBuscar.Text = string.Empty;
                    gbGrillaDemanda.Visible = true;
                    gbGrillaDetalle.Visible = false;
                    gbBuscar.Visible = true;
                    gbDatosPrincipales.Visible = false;
                    btnNuevo.Enabled = true;
                    bool hayDatos;
                    if (dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.Rows.Count > 0)
                    {
                        hayDatos = true;
                    }
                    else hayDatos = false;
                    btnConsultar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    estadoActual = estadoUI.inicio;
                    break;
                case estadoUI.calcular:
                    txtAnioBuscar.Text = string.Empty;
                    gbGrillaDemanda.Visible = true;
                    gbGrillaDetalle.Visible = false;
                    gbBuscar.Visible = false;
                    gbDatosPrincipales.Visible = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoActual = estadoUI.inicio;
                    break;

                default:
                    break;
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

        private void btnCalcularPlanMP_Click(object sender, EventArgs e)
        {
            try
            {
                //Traigo cada uno de los meses del plan anual
                int codigoPlanAnual =Convert.ToInt32(cbPlanificacion.GetSelectedValue());

                //Cargo el dataset con el detalle 
                BLL.DetallePlanAnualBLL.ObtenerFila(dsPlanMP,codigoPlanAnual);

                string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                
                IList<Entidades.DetallePlanAnual> plan = new List<Entidades.DetallePlanAnual>();

                foreach (Data.dsPlanMateriasPrimas.DETALLE_PLAN_ANUALRow row in dsPlanMP.DETALLE_PLAN_ANUAL.Rows)
                {
                    Entidades.DetallePlanAnual detallePlanAnual = new GyCAP.Entidades.DetallePlanAnual();

                    detallePlanAnual.Codigo =Convert.ToInt32(row.DPAN_CODIGO);
                    detallePlanAnual.Mes = row.DPAN_MES;

                    Entidades.PlanAnual PlanAnual = new GyCAP.Entidades.PlanAnual();
                    PlanAnual.Codigo =Convert.ToInt32(row.PAN_CODIGO);
                    PlanAnual.Anio =Convert.ToInt32(dsPlanMP.PLANES_ANUALES.FindByPAN_CODIGO(PlanAnual.Codigo).PAN_ANIO);


                    detallePlanAnual.PlanAnual = PlanAnual;
                    detallePlanAnual.CantidadMes =Convert.ToInt32(row.DPAN_CANTIDADMES);
                    plan.Add(detallePlanAnual);
                }

                //Cargo el dataset con las Materias Primas Principales
                BLL.MateriaPrimaPrincipalBLL.ObtenerMPPrincipales(dsPlanMP);

                IList<Entidades.MateriaPrimaPrincipal> materiaPrimaPrincipal = new List<Entidades.MateriaPrimaPrincipal>();

                foreach (Data.dsPlanMateriasPrimas.MATERIASPRIMASPRINCIPALESRow row in dsPlanMP.MATERIASPRIMASPRINCIPALES.Rows)
                {
                    Entidades.MateriaPrimaPrincipal materiaPrima = new GyCAP.Entidades.MateriaPrimaPrincipal();

                    Entidades.MateriaPrima mp = new GyCAP.Entidades.MateriaPrima();

                    mp.CodigoMateriaPrima =Convert.ToInt32(row.MP_CODIGO);
                    materiaPrima.MateriaPrima = mp;
                    materiaPrima.Codigo =Convert.ToInt32(row.MPPR_CODIGO);
                    materiaPrima.Cantidad = row.MPPR_CANTIDAD;
                    
                    materiaPrimaPrincipal.Add(materiaPrima);
                }

                //creo el plan y el detalle de las materias primas
                IList<Entidades.PlanMateriaPrima> planesMP = new List<Entidades.PlanMateriaPrima>();
                IList<Entidades.DetallePlanMateriasPrimas> detalleplanMP = new List<Entidades.DetallePlanMateriasPrimas>();

                            
                for(int i=0; i<12;i++)
                {
                    //Creo el plan de las materias primas 
                    Entidades.PlanMateriaPrima planMP = new GyCAP.Entidades.PlanMateriaPrima();
                    planMP.Anio = plan[0].PlanAnual.Anio;
                    planMP.FechaCreacion = BLL.DBBLL.GetFechaServidor();
                    planMP.Mes = Meses[i];
                    planMP.Codigo = BLL.PlanMateriaPrimaBLL.GuardarPlanAnual(planMP);
                    planesMP.Add(planMP);
                }

                int cont=-1;
                foreach (Entidades.PlanMateriaPrima pm in planesMP)
                {
                    
                    cont+=1;
                    decimal cantidadMensual = plan[cont].CantidadMes;
                    
                    foreach (Entidades.MateriaPrimaPrincipal mpPrincipal in materiaPrimaPrincipal)
                    {
                        Entidades.DetallePlanMateriasPrimas detalle = new GyCAP.Entidades.DetallePlanMateriasPrimas();

                        //Creo la materia prima y la asigno
                        Entidades.MateriaPrima  mp = new GyCAP.Entidades.MateriaPrima();
                        mp.CodigoMateriaPrima= mpPrincipal.MateriaPrima.CodigoMateriaPrima;
                        detalle.MateriaPrima = mp;
                        detalle.Plan = planesMP[cont];
                        detalle.Cantidad = mpPrincipal.Cantidad * cantidadMensual;
                        detalle.Codigo = BLL.PlanMateriaPrimaBLL.GuardarDetalle(detalle);
                        detalleplanMP.Add(detalle);

                    }

                }

                //Inserto todos los cambios en el dataset



                foreach (Entidades.PlanMateriaPrima p in planesMP)
                {

                    Data.dsPlanMateriasPrimas.PLANES_MATERIAS_PRIMAS_ANUALESRow rowPlanMP = dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.NewPLANES_MATERIAS_PRIMAS_ANUALESRow();
                    rowPlanMP.BeginEdit();
                    rowPlanMP.PMPA_ANIO = p.Anio;
                    rowPlanMP.PMPA_CODIGO = p.Codigo;
                    rowPlanMP.PMPA_FECHACREACION= p.FechaCreacion;
                    rowPlanMP.PMPA_MES = p.Mes;
                    rowPlanMP.EndEdit();
                    dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.AddPLANES_MATERIAS_PRIMAS_ANUALESRow(rowPlanMP);
                    dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.AcceptChanges();
                }
                
                //El detalle de la demanda anual
                foreach (Entidades.DetallePlanMateriasPrimas obje in detalleplanMP)
                {
                    Data.dsPlanMateriasPrimas.DETALLE_PLAN_MATERIAS_PRIMAS_ANUALRow rowDPlanMP = dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL.NewDETALLE_PLAN_MATERIAS_PRIMAS_ANUALRow();
                    rowDPlanMP.BeginEdit();
                    rowDPlanMP.DPMPA_CODIGO = obje.Codigo;
                    rowDPlanMP.DPMPA_CANTIDAD = obje.Cantidad;
                    rowDPlanMP.MP_CODIGO = obje.MateriaPrima.CodigoMateriaPrima;
                    rowDPlanMP.PMPA_CODIGO = obje.Plan.Codigo;
                    rowDPlanMP.EndEdit();
                    dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL.AddDETALLE_PLAN_MATERIAS_PRIMAS_ANUALRow(rowDPlanMP);
                    dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL.AcceptChanges();
                }

                //Seteo la interface
                SetInterface(estadoUI.calcular);
                                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL.Clear();

                int codigo = Convert.ToInt32(dvListaPlanMP[dgvLista.SelectedRows[0].Index]["pmpa_codigo"]);

                //Se llama a la funcion de busqueda con todos los parametros
                BLL.PlanMateriaPrimaBLL.ObtenerDetalle(codigo, dsPlanMP);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL;

                if (dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para esa demanda.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    gbGrillaDemanda.Visible = true;
                    gbGrillaDetalle.Visible = true;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Anual de Materias Primas - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }


        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "PMPA_FECHACREACION":
                        DateTime fecha = Convert.ToDateTime(e.Value);
                        e.Value = fecha.ToString("dd/MM/yyyy");
                        break;
                    default:
                        break;
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el Plan de Materias Primas seleccionado y todo su detalle ?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        
                        int cont=0;
                        foreach (DataRowView dr in dvListaPlanMP)
                        {

                            int codigo = Convert.ToInt32(dvListaPlanMP[cont]["pmpa_codigo"]);
                            //Elimino la planificacion de las materias primas
                            BLL.DemandaAnualBLL.Eliminar(codigo);
                            cont += 1;

                            //Lo eliminamos del dataset
                            dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.FindByPMPA_CODIGO(codigo).Delete();
                            dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.AcceptChanges();
                        }

                            //Limpio el dataset de detalles
                            dsPlanMP.DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL.Clear();

                            //Avisamos que se elimino 
                            MessageBox.Show("Se han eliminado los datos correctamente", "Información: Elemento Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Ponemos la ventana en el estado inicial
                            SetInterface(estadoUI.inicio);
                        
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Plan Anual de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.Clear();

                if (txtAnioBuscar.Text != string.Empty)
                {
                    //Valido que el año
                    if (txtAnioBuscar.Text.Length != 4) throw new Exception();

                    int anio = Convert.ToInt32(txtAnioBuscar.Text);

                    //Se llama a la funcion de busqueda con todos los parametros
                    BLL.PlanMateriaPrimaBLL.ObtenerTodos(anio, dsPlanMP);
                }
                else
                {
                    //Se llama a la funcion de busqueda para que traiga todos los valores
                    BLL.PlanMateriaPrimaBLL.ObtenerTodos(dsPlanMP);
                }
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanMP.Table = dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES;

                if (dsPlanMP.PLANES_MATERIAS_PRIMAS_ANUALES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Planes anuales con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvDetalle.Columns[e.ColumnIndex].Name)
                {
                    case "MP_CODIGO":
                        string nombreMP = dsPlanMP.MATERIAS_PRIMAS.FindByMP_CODIGO(Convert.ToInt32(e.Value)).MP_NOMBRE;
                        e.Value = nombreMP;
                        break;
                    case "UMED_CODIGO":
                        string nombreUMed = dsPlanMP.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombreUMed;
                        break;

                   default:
                        break;
                }

            }
           
        }

       



    }
}
