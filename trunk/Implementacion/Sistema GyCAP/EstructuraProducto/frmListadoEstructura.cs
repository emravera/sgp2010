using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmListadoEstructura : Form
    {
        private static frmListadoEstructura _frmListadoEstructura = null;
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        DataView dvPartes, dvCocinaBuscar, dvEstructuras;
        
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
                    dsEstructura.MATERIASPRIMASXESTRUCTURA.Clear();
                    dsEstructura.PIEZASXESTRUCTURA.Clear();
                    dsEstructura.SUBCONJUNTOSXESTRUCTURA.Clear();
                    dsEstructura.CONJUNTOSXESTRUCTURA.Clear();
                    dsEstructura.GRUPOS_ESTRUCTURA.Clear();
                    dsEstructura.ESTRUCTURAS.Clear();
                    BLL.EstructuraBLL.ObtenerEstructuraCocina(cbCocinaBuscar.GetSelectedValueInt(), dsEstructura, true);
                    dvEstructuras.Table = dsEstructura.ESTRUCTURAS;
                    if (dsEstructura.ESTRUCTURAS.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron Estructuras para la cocina seleccionada.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                                        
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MessageBox.Show(ex.Message, "Error: Estructuras - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Cocina.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                BLL.ConjuntoBLL.ObtenerConjuntos(dsEstructura.CONJUNTOS);
                BLL.SubConjuntoBLL.ObtenerSubconjuntos(dsEstructura.SUBCONJUNTOS);
                BLL.PiezaBLL.ObtenerPiezas(dsEstructura.PIEZAS);
                BLL.MateriaPrimaBLL.ObtenerTodos(dsEstructura.MATERIAS_PRIMAS);
                BLL.UnidadMedidaBLL.ObtenerTodos(dsUnidadMedida.UNIDADES_MEDIDA);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvEstructuras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            cbCocinaBuscar.SetDatos(dvCocinaBuscar, "COC_CODIGO", "COC_CODIGO_PRODUCTO", "Seleccione", false);
            IList<Sistema.Item> listaPor = new List<Sistema.Item>();
            listaPor.Add(new Sistema.Item("Sin selección", 0));
            listaPor.Add(new Sistema.Item("Nombre", 1));
            listaPor.Add(new Sistema.Item("Tipo de parte", 2));
            listaPor.Add(new Sistema.Item("Cantidad", 3));
            cbOrdenPor.DataSource = listaPor;
            cbOrdenPor.DisplayMember = "Name";
            cbOrdenPor.ValueMember = "Value";
            cbOrdenPor.SetSelectedValue(0);
            IList<Sistema.Item> listaForma = new List<Sistema.Item>();
            listaForma.Add(new Sistema.Item("Sin selección", 0));
            listaForma.Add(new Sistema.Item("Ascendente", 1));
            listaForma.Add(new Sistema.Item("Descendente", 2));
            cbOrdenForma.DataSource = listaForma;
            cbOrdenForma.DisplayMember = "Name";
            cbOrdenForma.ValueMember = "Value";
            cbOrdenForma.SetSelectedValue(0);
        }

        private void CargarListaPartes()
        {
            dsEstructura.LISTA_PARTES.Clear();
            foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in dsEstructura.CONJUNTOSXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Conjunto";
                rowParte.PAR_CODIGO = row.CONJUNTOSRow.CONJ_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.CONJUNTOSRow.CONJ_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.CXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in dsEstructura.SUBCONJUNTOSXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Subconjunto";
                rowParte.PAR_CODIGO = row.SUBCONJUNTOSRow.SCONJ_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.SUBCONJUNTOSRow.SCONJ_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.SCXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in dsEstructura.PIEZASXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Pieza";
                rowParte.PAR_CODIGO = row.PIEZASRow.PZA_CODIGOPARTE;
                rowParte.PAR_NOMBRE = row.PIEZASRow.PZA_NOMBRE;
                rowParte.PAR_TERMINACION = dsEstructura.TERMINACIONES.FindByTE_CODIGO(row.PIEZASRow.TE_CODIGO).TE_NOMBRE;
                rowParte.PAR_CANTIDAD = row.PXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = "Unidad";
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in dsEstructura.MATERIASPRIMASXESTRUCTURA)
            {
                Data.dsEstructura.LISTA_PARTESRow rowParte = dsEstructura.LISTA_PARTES.NewLISTA_PARTESRow();
                rowParte.BeginEdit();
                rowParte.PAR_TIPO = "Materia Prima";
                rowParte.PAR_CODIGO = string.Empty;
                rowParte.PAR_NOMBRE = row.MATERIAS_PRIMASRow.MP_NOMBRE;
                rowParte.PAR_TERMINACION = string.Empty;
                rowParte.PAR_CANTIDAD = row.MPXE_CANTIDAD.ToString();
                rowParte.PAR_UMED = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(row.MATERIAS_PRIMASRow.UMED_CODIGO).UMED_NOMBRE;
                rowParte.EndEdit();
                dsEstructura.LISTA_PARTES.AddLISTA_PARTESRow(rowParte);
            }

            dvPartes.Table = dsEstructura.LISTA_PARTES;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void dgvEstructuras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
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
                //Armemos el filtro según las selecciones realizadas
                string filtro = string.Empty, mostrar = string.Empty;
                if (chkConjunto.Checked) { mostrar += "'Conjunto'"; }
                if (chkSubconjunto.Checked && mostrar != string.Empty) { mostrar += ",'Subconjunto'"; }
                else if (chkSubconjunto.Checked && mostrar == string.Empty) { mostrar += "'Subconjunto'"; }
                if (chkPieza.Checked && mostrar != string.Empty) { mostrar += ",'Pieza'"; }
                else if (chkPieza.Checked && mostrar == string.Empty) { mostrar += "'Pieza'"; }
                if (chkMateriaPrima.Checked && mostrar != string.Empty) { mostrar += ",'Materia Prima'"; }
                else if (chkMateriaPrima.Checked && mostrar == string.Empty) { mostrar += "'Materia Prima'"; }
                if (mostrar != string.Empty) { filtro = "PAR_TIPO IN (" + mostrar + ")"; }
                else { filtro = "PAR_TIPO = 'ocultar todo'"; }

                //Ahora armemos el orden
                string columnaOrden = string.Empty, formaOrden = string.Empty;
                switch (cbOrdenPor.GetSelectedValueInt())
                {
                    case 1: //Por nombre
                        columnaOrden = "PAR_NOMBRE ";
                        break;
                    case 2: //Por tipo de parte
                        columnaOrden = "PAR_TIPO ";
                        break;
                    case 3: //Por cantidad
                        columnaOrden = "PAR_CANTIDAD ";
                        break;
                    default: //Nada, para cuando seleccione sin orden - valor 0 -
                        columnaOrden = string.Empty;
                        break;
                }

                switch (cbOrdenForma.GetSelectedValueInt())
                {
                    case 1: // ASC
                        if (columnaOrden != string.Empty) { formaOrden = " ASC"; }
                        break;
                    case 2: //DESC
                        if (columnaOrden != string.Empty) { formaOrden = " DESC"; }
                        break;
                    default: //NADA
                        formaOrden = string.Empty;
                        break;
                }

                //Carguemos los datos al dataview y asignemos el orden y filtro
                CargarListaPartes();
                dvPartes.Sort = columnaOrden + formaOrden;
                dvPartes.RowFilter = filtro;
                //Creamos el reporte y le asignamos el source
                Data.Reportes.crPartesEstructura2 reporte = new GyCAP.Data.Reportes.crPartesEstructura2();

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
                MessageBox.Show("Debe seleccionar una Estructura.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
