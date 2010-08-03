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
    public partial class frmCocina : Form
    {
        private static frmCocina _frmCocina = null;
        Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        DataView dvCocinas, dvMarcaBuscar, dvEstadoBuscar, dvTerminacionBuscar;
        DataView dvModelo, dvMarca, dvDesignacion, dvColor, dvTerminacion, dvEstado;
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
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Cocina seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Unidad de Medida de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Inicio

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsCocina.COCINAS.Clear();

                //Metodo para la busqueda con todos los parámetros
                BLL.CocinaBLL.ObtenerCocinas(txtCodigoBuscar.Text, cbMarcaBuscar.GetSelectedValueInt(), cbTerminacionBuscar.GetSelectedValueInt(), cbEstadoBuscar.GetSelectedValueInt(), dsCocina.COCINAS);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvCocinas.Table = dsCocina.COCINAS;

                if (dsCocina.COCINAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Cocinas con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Cocinas - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    case "ECOC_CODIGO":
                        nombre = dsCocina.ESTADO_COCINAS.FindByECOC_CODIGO(Convert.ToInt32(e.Value)).ECOC_NOMBRE;
                        e.Value = nombre;
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
            cbEstado.SetSelectedValue(Convert.ToInt32(dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).ECOC_CODIGO));
            nudPrecio.Value = dsCocina.COCINAS.FindByCOC_CODIGO(codigoCocina).COC_PRECIO;
            pbImagen.Image = BLL.CocinaBLL.ObtenerImagen(codigoCocina);
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos los datos
            string datosCompletar = string.Empty;
            if (txtCodigo.Text == string.Empty) { datosCompletar += "* Código"; }
            if (cbModelo.GetSelectedIndex() == -1) { datosCompletar += "* Modelo"; }
            if (cbMarca.GetSelectedIndex() == -1) { datosCompletar += "* Marca"; }
            if (cbDesignacion.GetSelectedIndex() == -1) { datosCompletar += "* Designación"; }
            if (cbColor.GetSelectedIndex() == -1) { datosCompletar += "* Color"; }
            if (cbTerminacion.GetSelectedIndex() == -1) { datosCompletar += "* "; }
            if (cbEstado.GetSelectedIndex() == -1) { datosCompletar += "* Estado"; }
            if (nudPrecio.Value == 0) { datosCompletar += "* Precio"; }

            if (datosCompletar == string.Empty)
            {
                Entidades.Cocina cocina = new GyCAP.Entidades.Cocina();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    cocina.CodigoProducto = txtCodigo.Text;
                    cocina.Modelo = new GyCAP.Entidades.ModeloCocina();
                    cocina.Modelo.Codigo = cbModelo.GetSelectedValueInt();
                    cocina.Marca = new GyCAP.Entidades.Marca();
                    cocina.Marca.Codigo = cbMarca.GetSelectedValueInt();
                    cocina.Designacion = new GyCAP.Entidades.Designacion();
                    cocina.Designacion.Codigo = cbDesignacion.GetSelectedValueInt();
                    cocina.Color = new GyCAP.Entidades.Color();
                    cocina.Color.Codigo = cbColor.GetSelectedValueInt();
                    cocina.TerminacionHorno = new GyCAP.Entidades.Terminacion();
                    cocina.TerminacionHorno.Codigo = cbTerminacion.GetSelectedValueInt();
                    cocina.Estado = new GyCAP.Entidades.EstadoCocina();
                    cocina.Estado.Codigo = cbEstado.GetSelectedValueInt();
                    cocina.Precio = nudPrecio.Value;

                    try
                    {
                        //Primero lo creamos en la db
                        cocina.CodigoCocina = BLL.CocinaBLL.Insertar(cocina);
                        //Ahora lo agregamos al dataset
                        Data.dsCocina.COCINASRow rowCocina = dsCocina.COCINAS.NewCOCINASRow();
                        rowCocina.BeginEdit();
                        rowCocina.COC_CODIGO = cocina.CodigoCocina;
                        rowCocina.ECOC_CODIGO = cocina.Estado.Codigo;
                        rowCocina.MOD_CODIGO = cocina.Modelo.Codigo;
                        rowCocina.MCA_CODIGO = cocina.Marca.Codigo;
                        rowCocina.COL_CODIGO = cocina.Color.Codigo;
                        rowCocina.COC_CODIGO_PRODUCTO = cocina.CodigoProducto;
                        rowCocina.TE_CODIGO = cocina.TerminacionHorno.Codigo;
                        rowCocina.DESIG_CODIGO = cocina.Designacion.Codigo;
                        rowCocina.COC_PRECIO = cocina.Precio;
                        rowCocina.COC_CANTIDADSTOCK = 0;
                        rowCocina.COC_ACTIVO = cbEstado.GetSelectedValueInt();
                        rowCocina.EndEdit();
                        dsCocina.COCINAS.AddCOCINASRow(rowCocina);
                        dsCocina.COCINAS.AcceptChanges();
                        //Guardamos la imagen
                        BLL.CocinaBLL.GuardarImagen(cocina.CodigoCocina, pbImagen.Image);
                        //Y por último seteamos el estado de la interfaz

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
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    cocina.CodigoCocina = Convert.ToInt32(dvCocinas[dgvListaCocina.SelectedRows[0].Index]["coc_codigo"]);
                    cocina.CodigoProducto = txtCodigo.Text;
                    cocina.Modelo = new GyCAP.Entidades.ModeloCocina();
                    cocina.Modelo.Codigo = cbModelo.GetSelectedValueInt();
                    cocina.Marca = new GyCAP.Entidades.Marca();
                    cocina.Marca.Codigo = cbMarca.GetSelectedValueInt();
                    cocina.Designacion = new GyCAP.Entidades.Designacion();
                    cocina.Designacion.Codigo = cbDesignacion.GetSelectedValueInt();
                    cocina.Color = new GyCAP.Entidades.Color();
                    cocina.Color.Codigo = cbColor.GetSelectedValueInt();
                    cocina.TerminacionHorno = new GyCAP.Entidades.Terminacion();
                    cocina.TerminacionHorno.Codigo = cbTerminacion.GetSelectedValueInt();
                    cocina.Estado = new GyCAP.Entidades.EstadoCocina();
                    cocina.Estado.Codigo = cbEstado.GetSelectedValueInt();
                    cocina.Precio = nudPrecio.Value;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.CocinaBLL.Actualizar(cocina);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCocina.COCINASRow rowCocina = dsCocina.COCINAS.FindByCOC_CODIGO(cocina.CodigoCocina);
                        rowCocina.BeginEdit();
                        rowCocina.COC_CODIGO = cocina.CodigoCocina;
                        rowCocina.ECOC_CODIGO = cocina.Estado.Codigo;
                        rowCocina.MOD_CODIGO = cocina.Modelo.Codigo;
                        rowCocina.MCA_CODIGO = cocina.Marca.Codigo;
                        rowCocina.COL_CODIGO = cocina.Color.Codigo;
                        rowCocina.COC_CODIGO_PRODUCTO = cocina.CodigoProducto;
                        rowCocina.TE_CODIGO = cocina.TerminacionHorno.Codigo;
                        rowCocina.DESIG_CODIGO = cocina.Designacion.Codigo;
                        rowCocina.COC_PRECIO = cocina.Precio;
                        rowCocina.EndEdit();
                        dsCocina.COCINAS.AcceptChanges();
                        //Actualizamos la imagen
                        BLL.CocinaBLL.GuardarImagen(cocina.CodigoCocina, pbImagen.Image);
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvListaCocina.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosCompletar, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string datosCompletar = string.Empty, codigo = string.Empty;
            if (cbMarca.GetSelectedIndex() == -1) { datosCompletar += "* Marca"; }
            if (cbModelo.GetSelectedIndex() == -1) { datosCompletar += "* Modelo"; }
            if (cbTerminacion.GetSelectedIndex() == -1) { datosCompletar += "* "; }
            if (cbDesignacion.GetSelectedIndex() == -1) { datosCompletar += "* Designación"; }
            if (datosCompletar == string.Empty)
            {
                txtCodigo.Text = dsCocina.MARCAS.FindByMCA_CODIGO(cbMarca.GetSelectedValueInt()).MCA_NOMBRE;
                txtCodigo.Text += dsCocina.MODELOS_COCINAS.FindByMOD_CODIGO(cbModelo.GetSelectedValueInt()).MOD_NOMBRE;
                txtCodigo.Text += dsCocina.TERMINACIONES.FindByTE_CODIGO(cbTerminacion.GetSelectedValueInt()).TE_ABREVIATURA;
                txtCodigo.Text += dsCocina.DESIGNACIONES.FindByDESIG_CODIGO(cbDesignacion.GetSelectedValueInt()).DESIG_NOMBRE;
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosCompletar, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void ofdImagen_FileOk(object sender, CancelEventArgs e)
        {
            pbImagen.ImageLocation = ofdImagen.FileName;
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
                    tcCocina.SelectedTab = tpBuscar;
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
                    nudPrecio.Enabled = true;
                    gbImagen.Enabled = true;                    
                    cbModelo.SetSelectedIndex(-1);
                    cbMarca.SetSelectedIndex(-1);
                    cbDesignacion.SetSelectedIndex(-1);
                    cbColor.SetSelectedIndex(-1);
                    cbTerminacion.SetSelectedIndex(-1);
                    cbEstado.SetSelectedIndex(-1);
                    nudPrecio.Value = 0;
                    pbImagen.Image = null;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
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
                    nudPrecio.Enabled = true;
                    gbImagen.Enabled = true;
                    cbModelo.SetSelectedIndex(-1);
                    cbMarca.SetSelectedIndex(-1);
                    cbDesignacion.SetSelectedIndex(-1);
                    cbColor.SetSelectedIndex(-1);
                    cbTerminacion.SetSelectedIndex(-1);
                    cbEstado.SetSelectedIndex(-1);
                    nudPrecio.Value = 0;
                    pbImagen.Image = null;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
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
                    nudPrecio.Enabled = false;
                    gbImagen.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
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
                    nudPrecio.Enabled = true;
                    gbImagen.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
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
                BLL.EstadoCocinaBLL.ObtenerEstados(dsCocina.ESTADO_COCINAS);
                BLL.ModeloCocinaBLL.ObtenerTodos(dsCocina.MODELOS_COCINAS);
                BLL.DesignacionBLL.ObtenerTodos(dsCocina.DESIGNACIONES);
                BLL.ColorBLL.ObtenerTodos(dsCocina.COLORES);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Grilla 
            dgvListaCocina.AutoGenerateColumns = false;
            dgvListaCocina.Columns.Add("COC_CODIGO_PRODUCTO","Código");
            dgvListaCocina.Columns.Add("MOD_CODIGO", "Modelo");
            dgvListaCocina.Columns.Add("MCA_CODIGO", "Marca");
            dgvListaCocina.Columns.Add("ECOC_CODIGO", "Estado");
            dgvListaCocina.Columns["COC_CODIGO_PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["MOD_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["MCA_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["ECOC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvListaCocina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListaCocina.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvListaCocina.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvListaCocina.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvListaCocina.Columns["ECOC_CODIGO"].DataPropertyName = "ECOC_CODIGO";
            
            //Dataviews
            dvMarcaBuscar = new DataView(dsCocina.MARCAS);
            dvEstadoBuscar = new DataView(dsCocina.ESTADO_COCINAS);
            dvTerminacionBuscar = new DataView(dsCocina.TERMINACIONES);
            dvCocinas = new DataView(dsCocina.COCINAS);
            dvCocinas.Sort = "COC_CODIGO_PRODUCTO";
            dgvListaCocina.DataSource = dvCocinas;
            dvModelo = new DataView(dsCocina.MODELOS_COCINAS);
            dvMarca = new DataView(dsCocina.MARCAS);
            dvDesignacion = new DataView(dsCocina.DESIGNACIONES);
            dvColor = new DataView(dsCocina.COLORES);
            dvTerminacion = new DataView(dsCocina.TERMINACIONES);
            dvEstado = new DataView(dsCocina.ESTADO_COCINAS);

            //Combos
            cbMarcaBuscar.SetDatos(dvMarcaBuscar, "MCA_CODIGO", "MCA_NOMBRE", "--TODOS--", true);
            cbEstadoBuscar.SetDatos(dvEstadoBuscar, "ECOC_CODIGO", "ECOC_NOMBRE", "--TODOS--", true);
            cbTerminacionBuscar.SetDatos(dvTerminacionBuscar, "TE_CODIGO", "TE_NOMBRE", "--TODOS--", true);
            cbMarca.SetDatos(dvMarca, "MCA_CODIGO", "MCA_NOMBRE", "Seleccione", false);
            cbEstado.SetDatos(dvEstado, "ECOC_CODIGO", "ECOC_NOMBRE", "Seleccione", false);
            cbTerminacion.SetDatos(dvTerminacion, "TE_CODIGO", "TE_NOMBRE", "Seleccione", false);            
            cbModelo.SetDatos(dvModelo, "MOD_CODIGO", "MOD_NOMBRE", "Seleccione", false);
            cbDesignacion.SetDatos(dvDesignacion, "DESIG_CODIGO", "DESIG_NOMBRE", "Seleccione", false);
            cbColor.SetDatos(dvColor, "COL_CODIGO", "COL_NOMBRE", "Seleccione", false);

            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";
        }

        private void dgvListaCocina_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void txtCodigoBuscar_Enter(object sender, EventArgs e)
        {
            txtCodigoBuscar.SelectAll();
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }

        private void nudPrecio_Enter(object sender, EventArgs e)
        {
            nudPrecio.Select(0, 20);
        }

        #endregion Servicios

        

        

        

    }
}
