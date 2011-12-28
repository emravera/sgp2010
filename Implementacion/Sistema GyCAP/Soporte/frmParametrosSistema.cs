using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Soporte
{
    public partial class frmParametrosSistema : Form
    {
        private static frmParametrosSistema _frmParametrosSistema = null;
        private Data.dsParametrosSistema dsParametrosSistema = new GyCAP.Data.dsParametrosSistema();
        private DataView dvListaParametros;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        #region Inicio

        public frmParametrosSistema()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Agregamos las columnas
            dgvLista.Columns.Add("CONF_CODIGO", "Código");
            dgvLista.Columns.Add("CONF_NOMBRE", "Nombre");
            dgvLista.Columns.Add("CONF_VALOR", "Cliente");

            //Se setean los valores de las columnas 
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["CONF_CODIGO"].DataPropertyName = "CONF_CODIGO";
            dgvLista.Columns["CONF_NOMBRE"].DataPropertyName = "CONF_NOMBRE";
            dgvLista.Columns["CONF_VALOR"].DataPropertyName = "CONF_VALOR";

            //Escondemos la fila del codigo
            dgvLista.Columns["CONF_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaParametros = new DataView(dsParametrosSistema.CONFIGURACIONES_SISTEMA);
            dvListaParametros.Sort = "CONF_CODIGO ASC";
            dgvLista.DataSource = dvListaParametros;

            //Seteo de los Maxlengt
            txtNombreBuscar.MaxLength = 30;
            txtNombre.MaxLength = 30;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }
#endregion

        #region Servicios
        
        //Método para evitar la creación de más de una pantalla
        public static frmParametrosSistema Instancia
        {
            get
            {
                if (_frmParametrosSistema == null || _frmParametrosSistema.IsDisposed)
                {
                    _frmParametrosSistema = new frmParametrosSistema();
                }
                else
                {
                    _frmParametrosSistema.BringToFront();
                }
                return _frmParametrosSistema;
            }
            set
            {
                _frmParametrosSistema = value;
            }
        }

        //Metodo que setea la interface
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsParametrosSistema.CONFIGURACIONES_SISTEMA.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcParametros.SelectedTab = tpBuscar;

                    txtNombreBuscar.Text = "";
                    
                    break;
                case estadoUI.nuevo:
                    break;

                default:
                    break;
            }
        }


        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsParametrosSistema.CONFIGURACIONES_SISTEMA.Clear();

                //Llamamos al método de búsqueda de datos
                BLL.ConfiguracionSistemaBLL.ObtenerTodos(txtNombreBuscar.Text, txtValorBuscar.Text, dsParametrosSistema.CONFIGURACIONES_SISTEMA);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaParametros.Table = dsParametrosSistema.CONFIGURACIONES_SISTEMA;

                if (dsParametrosSistema.CONFIGURACIONES_SISTEMA.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Parámetros del Sistema", this.Text);
                }

                //Seteamos el estado de la interfaz           
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

        #region Pestaña Busqueda




        #endregion


    }
}
