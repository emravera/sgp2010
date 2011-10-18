using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades.Mensajes;
using GyCAP.Entidades.ArbolEstructura;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmListadoEstructura : Form
    {
        private static frmListadoEstructura _frmListadoEstructura = null;
        private Data.dsEstructuraProducto dsEstructura = new GyCAP.Data.dsEstructuraProducto();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private Data.dsPlanMP dsUnidadMedida = new GyCAP.Data.dsPlanMP();
        DataView dvPartes, dvCocinaBuscar, dvEstructuras;
        private ArbolEstructura arbolEstructura;
        
        public frmListadoEstructura()
        {
            InitializeComponent();
            CargarDatos();
        }

        public static frmListadoEstructura Instancia
        {
            get
            {
                if (_frmListadoEstructura == null || _frmListadoEstructura.IsDisposed)
                {
                    _frmListadoEstructura = new frmListadoEstructura();
                }
                else
                {
                    _frmListadoEstructura.BringToFront();
                }
                return _frmListadoEstructura;
            }
            set
            {
                _frmListadoEstructura = value;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbCocinaBuscar.GetSelectedIndex() != -1)
            {
                try
                {
                    dsEstructura.ESTRUCTURAS.Clear();
                    BLL.EstructuraBLL.ObtenerEstructuras(null, null, null, cbCocinaBuscar.GetSelectedValueInt(), null, null, dsEstructura);
                    dvEstructuras.Table = dsEstructura.ESTRUCTURAS;

                    if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
                    {
                        MensajesABM.MsjBuscarNoEncontrado("Estructuras", this.Text);
                    }
                                        
                }
                catch (BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Cocina", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void CargarDatos()
        {
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsEstructura.TERMINACIONES);
                BLL.PlanoBLL.ObtenerTodos(dsEstructura.PLANOS);
                BLL.CocinaBLL.ObtenerCocinas(dsCocina.COCINAS);
                BLL.EmpleadoBLL.ObtenerEmpleados(dsEmpleado.EMPLEADOS);                
                BLL.MateriaPrimaBLL.ObtenerMP(dsEstructura.MATERIAS_PRIMAS);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsUnidadMedida.UNIDADES_MEDIDA);
            }
            catch (BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            //Grilla
            dgvEstructuras.AutoGenerateColumns = false;
            dgvEstructuras.Columns.Add("ESTR_NOMBRE", "Nombre");
            dgvEstructuras.Columns.Add("COC_CODIGO", "Cocina");
            dgvEstructuras.Columns.Add("PNO_CODIGO", "Plano");
            dgvEstructuras.Columns.Add("E_CODIGO", "Responsable");
            dgvEstructuras.Columns.Add("ESTR_ACTIVO", "Activo");
            dgvEstructuras.Columns.Add("ESTR_FECHA_ALTA", "Fecha creación");
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEstructuras.Columns["ESTR_NOMBRE"].DataPropertyName = "ESTR_NOMBRE";
            dgvEstructuras.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvEstructuras.Columns["PNO_CODIGO"].DataPropertyName = "PNO_CODIGO";
            dgvEstructuras.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvEstructuras.Columns["ESTR_ACTIVO"].DataPropertyName = "ESTR_ACTIVO";
            dgvEstructuras.Columns["ESTR_FECHA_ALTA"].DataPropertyName = "ESTR_FECHA_ALTA";
            
            //Dataviews
            dvCocinaBuscar = new DataView(dsCocina.COCINAS);
            dvPartes = new DataView(dsEstructura.LISTA_PARTES);
            dvEstructuras = new DataView(dsEstructura.ESTRUCTURAS);
            dvEstructuras.Sort = "ESTR_NOMBRE ASC";
            dgvEstructuras.DataSource = dvEstructuras;

            //ComboBoxs
            cbCocinaBuscar.SetDatos(dvCocinaBuscar, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione...", false);
            IList<Sistema.Item> listaPor = new List<Sistema.Item>();
            listaPor.Add(new Sistema.Item("--Sin selección--", 0));
            listaPor.Add(new Sistema.Item("Nombre", 1));
            listaPor.Add(new Sistema.Item("Tipo de parte", 2));
            listaPor.Add(new Sistema.Item("Cantidad", 3));
            cbOrdenPor.DataSource = listaPor;
            cbOrdenPor.DisplayMember = "Name";
            cbOrdenPor.ValueMember = "Value";
            cbOrdenPor.SetSelectedValue(0);
            IList<Sistema.Item> listaForma = new List<Sistema.Item>();
            listaForma.Add(new Sistema.Item("--Sin selección--", 0));
            listaForma.Add(new Sistema.Item("Ascendente", 1));
            listaForma.Add(new Sistema.Item("Descendente", 2));
            cbOrdenForma.DataSource = listaForma;
            cbOrdenForma.DisplayMember = "Name";
            cbOrdenForma.ValueMember = "Value";
            cbOrdenForma.SetSelectedValue(0);
        }

        private void CargarListaPartes()
        {
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void dgvEstructuras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;
                switch (dgvEstructuras.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsCocina.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "PNO_CODIGO":
                        nombre = dsEstructura.PLANOS.FindByPNO_CODIGO(Convert.ToInt32(e.Value)).PNO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "E_CODIGO":
                        nombre = dsEmpleado.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_APELLIDO;
                        nombre += ", " + dsEmpleado.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "ESTR_ACTIVO":
                        nombre = "No";
                        if (Convert.ToInt32(e.Value) == 1) { nombre = "Si"; }
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (dgvEstructuras.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                dsEstructura.LISTA_PARTES.Clear();

                try
                {
                    arbolEstructura = BLL.EstructuraBLL.GetArbolEstructuraByEstructura(Convert.ToInt32(dvEstructuras[dgvEstructuras.SelectedRows[0].Index]["estr_codigo"]), false);

                    IList<NodoEstructura> listaNodos = new List<NodoEstructura>();

                    if (chkPartes.Checked && chkMateriaPrima.Checked)
                    {
                        listaNodos = arbolEstructura.AsList(NodoEstructura.tipoContenido.Todos, true);
                    }

                    if (chkPartes.Checked && !chkMateriaPrima.Checked)
                    {
                        listaNodos = arbolEstructura.AsList(NodoEstructura.tipoContenido.Parte, true);
                    }

                    if (!chkPartes.Checked && chkMateriaPrima.Checked)
                    {
                        listaNodos = arbolEstructura.AsList(NodoEstructura.tipoContenido.MateriaPrima, false);
                    }

                    switch (cbOrdenPor.GetSelectedValueInt())
                    {
                        case 1: //Por nombre
                            if(cbOrdenForma.GetSelectedValueInt() == 2)
                            {
                                //descendente
                                listaNodos = listaNodos.OrderByDescending(p => p.CompuestoFriendlyName).ToList();
                            }
                            else
                            {
                                //Ascendente o default
                                listaNodos = listaNodos.OrderBy(p => p.CompuestoFriendlyName).ToList();
                            }
                            break;
                        case 2: //Por tipo de parte
                            if (cbOrdenForma.GetSelectedValueInt() == 2)
                            {
                                //descendente
                                listaNodos = listaNodos.OrderByDescending(p => p.Contenido).ToList();
                            }
                            else
                            {
                                //Ascendente o default
                                listaNodos = listaNodos.OrderBy(p => p.Contenido).ToList();
                            }
                            break;
                        case 3: //Por cantidad
                            if (cbOrdenForma.GetSelectedValueInt() == 2)
                            {
                                //descendente
                                listaNodos = listaNodos.OrderByDescending(p => p.Compuesto.Cantidad).ToList();
                            }
                            else
                            {
                                //Ascendente o default
                                listaNodos = listaNodos.OrderBy(p => p.Compuesto.Cantidad).ToList();
                            }
                            break;
                        default: //Nada, para cuando seleccione sin orden - valor 0 -                            
                            break;
                    }
                    
                    foreach (NodoEstructura nodo in listaNodos)
                    {
                        Data.dsEstructuraProducto.LISTA_PARTESRow row = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                        row.BeginEdit();

                        row.PAR_CANTIDAD = nodo.Compuesto.Cantidad.ToString();
                        row.PAR_CODIGO = (nodo.Contenido == NodoEstructura.tipoContenido.MateriaPrima) ? nodo.Compuesto.MateriaPrima.Nombre : nodo.Compuesto.Parte.Codigo;
                        row.PAR_NOMBRE = (nodo.Contenido == NodoEstructura.tipoContenido.MateriaPrima) ? nodo.Compuesto.MateriaPrima.Nombre : nodo.Compuesto.Parte.Nombre;
                        row.PAR_TERMINACION = (nodo.Contenido == NodoEstructura.tipoContenido.MateriaPrima) ? string.Empty : nodo.Compuesto.Parte.Terminacion.Nombre;
                        row.PAR_TIPO = (nodo.Contenido == NodoEstructura.tipoContenido.MateriaPrima) ? "Materia Prima" : nodo.Compuesto.Parte.Tipo.Nombre;
                        row.PAR_UMED = nodo.Compuesto.UnidadMedida.Nombre;

                        row.EndEdit();
                        dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(row);
                    }

                    dsEstructura.LISTA_PARTES.AcceptChanges();
                }
                catch (BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Generación);
                }

                dvPartes.Table = dsEstructura.LISTA_PARTES;                
                
                //Creamos el reporte y le asignamos el source
                Data.Reportes.crPartesEstructura2 reporte = new GyCAP.Data.Reportes.crPartesEstructura2();
                reporte.SummaryInfo.ReportTitle = dsCocina.COCINAS.FindByCOC_CODIGO(cbCocinaBuscar.GetSelectedValueInt()).COC_CODIGO_PRODUCTO;
                reporte.SetDataSource(dvPartes);
                //Creamos la pantalla que muestra todos los reportes y le asignamos el reporte
                Sistema.frmVisorReporte visor = new GyCAP.UI.Sistema.frmVisorReporte();
                visor.crvVisor.ReportSource = reporte;
                visor.ShowDialog();
                visor.Dispose();
                reporte.Dispose();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Estructura", MensajesABM.Generos.Femenino, this.Text);
            }
        }
    }
}
