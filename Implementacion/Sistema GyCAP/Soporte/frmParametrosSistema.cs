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
        private enum estadoUI { inicio, actualizar };
        
        #region Inicio

        public frmParametrosSistema()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Agregamos las columnas
            dgvLista.Columns.Add("CONF_CODIGO", "Código");
            dgvLista.Columns.Add("CONF_NOMBRE", "Nombre");
            dgvLista.Columns.Add("CONF_VALOR", "Valor");

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
                    //Limpiamos el datatable antes de comenzar
                    dsParametrosSistema.CONFIGURACIONES_SISTEMA.Clear();

                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    btnActualizar.Enabled = true;
                    tcParametros.SelectedTab = tpBuscar;

                    txtNombreBuscar.Text = "";
                    txtValorBuscar.Text = "";                    
                    break;

                case estadoUI.actualizar:
                    txtNombre.Enabled = false;
                    btnActualizar.Enabled = false;
                    tcParametros.SelectedTab = tpDatos;
                    break;

                default:
                    break;
            }
        }
        #endregion
        
        #region Pestaña Busqueda
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

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
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.actualizar);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Se revisa y realiza la validacion de los formularios
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                //Creamos el objeto con los parametros necesarios
                Entidades.ConfiguracionesSistema parametro = new GyCAP.Entidades.ConfiguracionesSistema();

                parametro.Nombre = txtNombre.Text;
                parametro.Valor = Convert.ToInt32(txtValor.Text);

                //Actualizamos el valor del parametro en la base de datos
                BLL.ConfiguracionSistemaBLL.Actualizar(parametro);

                //Avisamos que se guardo correctamente
                Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Parámetro del Sistema", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                //Seteamos la interface al inicio
                SetInterface(estadoUI.inicio);

            }
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Seteamos los valores de la grilla cuando se selecciona
            int codigoParametro = Convert.ToInt32(dvListaParametros[e.RowIndex]["conf_codigo"]);
            txtNombre.Text = dsParametrosSistema.CONFIGURACIONES_SISTEMA.FindByCONF_CODIGO(codigoParametro).CONF_NOMBRE;
            txtValor.Text = dsParametrosSistema.CONFIGURACIONES_SISTEMA.FindByCONF_CODIGO(codigoParametro).CONF_VALOR;
        }

        #endregion  

    }
}
