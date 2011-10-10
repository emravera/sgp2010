using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmCocina : Form
    {
        private static frmCocina _frmCocina = null;
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        DataView dvCocinas, dvMarcaBuscar, dvTerminacionBuscar;
        DataView dvModelo, dvMarca, dvDesignacion, dvColor, dvTerminacion;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmCocina()
        {
            InitializeComponent();

            SetGrillaCombosDatos();
            SetInterface(estadoUI.inicio);
        }

        public static frmCocina Instancia
        {
            get
            {
                if (_frmCocina == null || _frmCocina.IsDisposed)
                {
                    _frmCocina = new frmCocina();
                }
                else
                {
                    _frmCocina.BringToFront();
                }
                return _frmCocina;
            }
            set
            {
                _frmCocina = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvListaCocina.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MensajesABM.MsjConfirmaEliminarDatos("Cocina", MensajesABM.Generos.Femenino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvCocinas[dgvListaCocina.SelectedRows[0].Index]["coc_codigo"]);
                        BLL.CocinaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsCocina.COCINAS.FindByCOC_CODIGO(codigo).Delete();
                        dsCocina.COCINAS.AcceptChanges();
                        btnVolver.PerformClick();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.CocinaBaseException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Cocina", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        #endregion Inicio

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsCocina.COCINAS.Clear();

                BLL.CocinaBLL.ObtenerCocinas(txtCodigoBuscar.Text, cbMarcaBuscar.GetSelectedValueInt(), cbTerminacionBuscar.GetSelectedValueInt(), cbEstadoBuscar.GetSelectedValueInt(), dsCocina.COCINAS);

                dvCocinas.Table = dsCocina.COCINAS;

                if (dsCocina.COCINAS.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Cocinas", this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
            }
            finally
            {
                SetInterface(estadoUI.inicio);
            }
        }

        private void dgvListaCocina_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;
                switch (dgvListaCocina.Columns[e.ColumnIndex].Name)
                {
                    case "MOD_CODIGO":
                        nombre = dsCocina.MODELOS_COCINAS.FindByMOD_CODIGO(Convert.ToInt32(e.Value)).MOD_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MCA_CODIGO":
                        nombre = dsCocina.MARCAS.FindByMCA_CODIGO(Convert.ToInt32(e.Value)).MCA_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "COC_ESTADO":
                        if (Convert.ToInt32(e.Value) == 1) { e.Value = "Activa"; }
                        else if (Convert.ToInt32(e.Value) == 0) { e.Value = "Inactiva"; }
                        break;
                    case "COC_IS_BASE":
                        if (Convert.ToInt32(e.Value) == 1) { e.Value = "Si"; }
                        else if (Convert.ToInt32(e.Value) == 0) { e.Value = "No"; }
                        break;                        
                    default:
                        break;
                }
            }
        }

        private void dgvListaCocina_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoCocina = Convert.ToInt32(dvCocinas[e.RowIndex]["coc_codigo"]);
            txtCodigo.Text = dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).COC_CODIGO_PRODUCTO;
            cbModelo.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).MOD_CODIGO));
            cbMarca.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).MCA_CODIGO));
            cbDesignacion.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).DESIG_CODIGO));
            cbColor.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).COL_CODIGO));
            cbTerminacion.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).TE_CODIGO));
            cbEstado.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).COC_ACTIVO));
            chkBase.Checked = (dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).COC_IS_BASE == 1) ? true : false;
            //cargar imagen solo al consultar con WCF - gonzalo
            pbImagen.Image = BLL.CocinaBLL.ObtenerImagen(codigoCocina);
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.Cocina cocina = new GyCAP.Entidades.Cocina();
                cocina.CodigoProducto = txtCodigo.Text;
                cocina.Modelo = new GyCAP.Entidades.ModeloCocina() { Codigo = cbModelo.GetSelectedValueInt()};
                cocina.Marca = new GyCAP.Entidades.Marca() { Codigo = cbMarca.GetSelectedValueInt() } ;
                cocina.Designacion = new GyCAP.Entidades.Designacion() { Codigo = cbDesignacion.GetSelectedValueInt() };
                cocina.Color = new GyCAP.Entidades.Color() { Codigo = cbColor.GetSelectedValueInt() };
                cocina.TerminacionHorno = new GyCAP.Entidades.Terminacion() { Codigo = cbTerminacion.GetSelectedValueInt() };
                cocina.Activo = cbEstado.GetSelectedValueInt();
                cocina.EsBase = chkBase.Checked;
                cocina.HasImage = BLL.ImageRepository.WithImage;
                //determinar si tiene imagen que no sea la sinimagen - gonzalo

                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    try
                    {
                        //Primero lo creamos en la db
                        cocina.CodigoCocina = BLL.CocinaBLL.Insertar(cocina);
                        //Ahora lo agregamos al dataset                        
                        Data.dsCocina.COCINASRow rowCocina = dsCocina.COCINAS.NewCOCINASRow();
                        rowCocina.BeginEdit();
                        rowCocina.COC_CODIGO = cocina.CodigoCocina;
                        rowCocina.MOD_CODIGO = cocina.Modelo.Codigo;
                        rowCocina.MCA_CODIGO = cocina.Marca.Codigo;
                        rowCocina.COL_CODIGO = cocina.Color.Codigo;
                        rowCocina.COC_CODIGO_PRODUCTO = cocina.CodigoProducto;
                        rowCocina.TE_CODIGO = cocina.TerminacionHorno.Codigo;
                        rowCocina.DESIG_CODIGO = cocina.Designacion.Codigo;
                        rowCocina.COC_ACTIVO = cocina.Activo;
                        rowCocina.COC_IS_BASE = (cocina.EsBase) ? 1 : 0;
                        rowCocina.COC_HAS_IMAGE = cocina.HasImage;
                        rowCocina.EndEdit();
                        Image imagen = pbImagen.Image;
                        dsCocina.COCINAS.AddCOCINASRow(rowCocina);
                        dsCocina.COCINAS.AcceptChanges();
                        BLL.CocinaBLL.GuardarImagen(cocina.CodigoCocina, imagen);
                        imagen.Dispose();
                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                            SetInterface(estadoUI.inicio);
                        }
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.CocinaBaseException ex)
                    {
                        MensajesABM.MsjValidacion(ex.Message, this.Text);
                    }
                }
                else
                {
                    cocina.CodigoCocina = Convert.ToInt32(dvCocinas[dgvListaCocina.SelectedRows[0].Index]["coc_codigo"]);

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.CocinaBLL.Actualizar(cocina);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCocina.COCINASRow rowCocina = dsCocina.COCINAS.FindByCOC_CODIGO(cocina.CodigoCocina);
                        rowCocina.BeginEdit();
                        rowCocina.COC_CODIGO = cocina.CodigoCocina;
                        rowCocina.MOD_CODIGO = cocina.Modelo.Codigo;
                        rowCocina.MCA_CODIGO = cocina.Marca.Codigo;
                        rowCocina.COL_CODIGO = cocina.Color.Codigo;
                        rowCocina.COC_CODIGO_PRODUCTO = cocina.CodigoProducto;
                        rowCocina.TE_CODIGO = cocina.TerminacionHorno.Codigo;
                        rowCocina.DESIG_CODIGO = cocina.Designacion.Codigo;
                        rowCocina.COC_ACTIVO = cocina.Activo;
                        rowCocina.COC_IS_BASE = (cocina.EsBase) ? 1 : 0;
                        rowCocina.COC_HAS_IMAGE = cocina.HasImage;
                        rowCocina.EndEdit();
                        dsCocina.COCINAS.AcceptChanges();
                        //Actualizamos la imagen
                        BLL.CocinaBLL.GuardarImagen(cocina.CodigoCocina, pbImagen.Image);
                        MensajesABM.MsjConfirmaGuardar("Cocina", this.Text, MensajesABM.Operaciones.Modificación);
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                    catch (Entidades.Excepciones.CocinaBaseException ex)
                    {
                        MensajesABM.MsjValidacion(ex.Message, this.Text);
                    }
                }
                dgvListaCocina.Refresh();
            }            
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            ofdImagen.ShowDialog();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            pbImagen.Image = EstructuraProducto.Properties.Resources.sinimagen;
            ActualizarImagen();
        }

        private void ofdImagen_FileOk(object sender, CancelEventArgs e)
        {
            pbImagen.Image = Image.FromFile(ofdImagen.FileName);
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsCocina.COCINAS.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    btnZoomOut.PerformClick();
                    tcCocina.SelectedTab = tpBuscar;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtCodigoBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    cbModelo.Enabled = true;
                    cbMarca.Enabled = true;
                    cbDesignacion.Enabled = true;
                    cbColor.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;                   
                    cbModelo.SetTexto("Seleccione...");
                    cbMarca.SetTexto("Seleccione...");
                    cbDesignacion.SetTexto("Seleccione...");
                    cbColor.SetTexto("Seleccione...");
                    cbTerminacion.SetTexto("Seleccione...");
                    cbEstado.SetSelectedValue(1);
                    chkBase.Checked = false;
                    chkBase.Enabled = true;
                    pbImagen.Image = EstructuraProducto.Properties.Resources.sinimagen;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    btnZoomOut.PerformClick();
                    tcCocina.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    cbModelo.Enabled = true;
                    cbMarca.Enabled = true;
                    cbDesignacion.Enabled = true;
                    cbColor.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    cbModelo.SetTexto("Seleccione");
                    cbMarca.SetTexto("Seleccione");
                    cbDesignacion.SetTexto("Seleccione");
                    cbColor.SetTexto("Seleccione");
                    cbTerminacion.SetTexto("Seleccione");
                    cbEstado.SetSelectedValue(1);
                    chkBase.Checked = false;
                    chkBase.Enabled = true;
                    pbImagen.Image = EstructuraProducto.Properties.Resources.sinimagen;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    btnZoomOut.PerformClick();
                    tcCocina.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    cbModelo.Enabled = false;
                    cbMarca.Enabled = false;
                    cbDesignacion.Enabled = false;
                    cbColor.Enabled = false;
                    cbTerminacion.Enabled = false;
                    cbEstado.Enabled = false;
                    chkBase.Enabled = false;
                    btnAbrirImagen.Enabled = false;
                    btnQuitarImagen.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    btnZoomOut.PerformClick();
                    tcCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    cbModelo.Enabled = true;
                    cbMarca.Enabled = true;
                    cbDesignacion.Enabled = true;
                    cbColor.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    chkBase.Enabled = true;
                    btnAbrirImagen.Enabled = true;
                    btnQuitarImagen.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    btnZoomOut.PerformClick();
                    tcCocina.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                default:
                    break;
            }
        }

        private void SetGrillaCombosDatos()
        {
            //Cargo los datos
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsCocina.TERMINACIONES);
                BLL.MarcaBLL.ObtenerTodos(dsCocina.MARCAS);
                BLL.ModeloCocinaBLL.ObtenerTodos(dsCocina.MODELOS_COCINAS);
                BLL.DesignacionBLL.ObtenerTodos(dsCocina.DESIGNACIONES);
                BLL.ColorBLL.ObtenerTodos(dsCocina.COLORES);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }


            //Grilla 
            dgvListaCocina.AutoGenerateColumns = false;
            dgvListaCocina.Columns.Add("COC_CODIGO_PRODUCTO","Código");
            dgvListaCocina.Columns.Add("MOD_CODIGO", "Modelo");
            dgvListaCocina.Columns.Add("MCA_CODIGO", "Marca");
            dgvListaCocina.Columns.Add("COC_ESTADO", "Estado");
            dgvListaCocina.Columns.Add("COC_IS_BASE", "Es base");
            dgvListaCocina.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvListaCocina.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvListaCocina.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvListaCocina.Columns["COC_ESTADO"].DataPropertyName = "COC_ACTIVO";
            dgvListaCocina.Columns["COC_IS_BASE"].DataPropertyName = "COC_IS_BASE";
                        
            //Dataviews
            dvMarcaBuscar = new DataView(dsCocina.MARCAS);
            dvTerminacionBuscar = new DataView(dsCocina.TERMINACIONES);
            dvCocinas = new DataView(dsCocina.COCINAS);
            dvCocinas.Sort = "COC_CODIGO_PRODUCTO";
            dgvListaCocina.DataSource = dvCocinas;
            dvModelo = new DataView(dsCocina.MODELOS_COCINAS);
            dvMarca = new DataView(dsCocina.MARCAS);
            dvDesignacion = new DataView(dsCocina.DESIGNACIONES);
            dvColor = new DataView(dsCocina.COLORES);
            dvTerminacion = new DataView(dsCocina.TERMINACIONES);
            
            //Combos
            cbMarcaBuscar.SetDatos(dvMarcaBuscar, "MCA_CODIGO", "MCA_NOMBRE", "--TODOS--", true);
            string[] nombres = { "Activa", "Inactiva"};
            int[] valores = {1, 0 };
            cbEstadoBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            cbTerminacionBuscar.SetDatos(dvTerminacionBuscar, "TE_CODIGO", "TE_NOMBRE", "--TODOS--", true);
            cbMarca.SetDatos(dvMarca, "MCA_CODIGO", "MCA_NOMBRE", "Seleccione", false);
            cbEstado.SetDatos(nombres, valores, "Seleccione", false);
            cbTerminacion.SetDatos(dvTerminacion, "TE_CODIGO", "TE_NOMBRE", "Seleccione", false);            
            cbModelo.SetDatos(dvModelo, "MOD_CODIGO", "MOD_NOMBRE", "Seleccione", false);
            cbDesignacion.SetDatos(dvDesignacion, "DESIG_CODIGO", "DESIG_NOMBRE", "Seleccione", false);
            cbColor.SetDatos(dvColor, "COL_CODIGO", "COL_NOMBRE", "Seleccione", false);

            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";
        }

        private void control_Enter(object sender, EventArgs e)
        {
            //if (sender.GetType().Equals(typeof(TextBox))) { (sender as TextBox).SelectAll(); }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            Sistema.frmImagenZoom.Instancia.SetImagen(pbImagen.Image, "Imagen de la Cocina");
            animador.SetFormulario(Sistema.frmImagenZoom.Instancia, this, Sistema.ControlesUsuarios.AnimadorFormulario.animacionDerecha, 300, true);
            animador.MostrarFormulario();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            animador.CerrarFormulario();
        }

        private void ActualizarImagen()
        {
            if (animador.EsVisible())
            {
                (animador.GetForm() as Sistema.frmImagenZoom).SetImagen(pbImagen.Image, "Imagen de la Cocina");
            }
        }

        private void pbImagen_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ActualizarImagen();
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
                (sender as Button).Location = punto;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
                (sender as Button).Location = punto;
            }
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion Servicios


    }
}
