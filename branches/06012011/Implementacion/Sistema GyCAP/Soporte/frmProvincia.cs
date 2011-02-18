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
    public partial class frmProvincia : Form
    {
        private static frmProvincia _frmProvincia = null;
        Data.dsProveedor dsProvincia = new GyCAP.Data.dsProveedor();
        private DataView dvProvincias;
        private enum estadoUI { inicio, modificar };
        private estadoUI estadoInterface;
        
        public frmProvincia()
        {
            InitializeComponent();

            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("PCIA_NOMBRE", "Nombre");
            dgvLista.Columns["PCIA_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLista.Columns["PCIA_NOMBRE"].DataPropertyName = "PCIA_NOMBRE";
            BLL.ProvinciaBLL.ObtenerProvincias(dsProvincia.PROVINCIAS);
            dvProvincias = new DataView(dsProvincia.PROVINCIAS);
            dvProvincias.Sort = "PCIA_NOMBRE ASC";
            dgvLista.DataSource = dvProvincias;
            
            SetInterface(estadoUI.inicio);
        }

        public static frmProvincia Instancia
        {
            get
            {
                if (_frmProvincia == null || _frmProvincia.IsDisposed)
                {
                    _frmProvincia = new frmProvincia();
                }
                else
                {
                    _frmProvincia.BringToFront();
                }
                return _frmProvincia;
            }
            set
            {
                _frmProvincia = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region Botones

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvProvincias[dgvLista.SelectedRows[0].Index]["pcia_codigo"]);
                txtNombre.Text = dsProvincia.PROVINCIAS.FindByPCIA_CODIGO(codigo).PCIA_NOMBRE;
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Provincia de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Provincia seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos
                        int codigo = Convert.ToInt32(dvProvincias[dgvLista.SelectedRows[0].Index]["pcia_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.ProvinciaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsProvincia.PROVINCIAS.FindByPCIA_CODIGO(codigo).Delete();
                        dsProvincia.PROVINCIAS.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar una Provincia de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty)
            {
                //Revisamos que está haciendo
                if (estadoInterface != estadoUI.modificar)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Primero lo creamos en la db
                        int codigo = BLL.ProvinciaBLL.Insertar(txtNombre.Text);
                        //Ahora lo agregamos al dataset
                        Data.dsProveedor.PROVINCIASRow row = dsProvincia.PROVINCIAS.NewPROVINCIASRow();
                        //Indicamos que comienza la edición de la fila
                        row.BeginEdit();
                        row.PCIA_CODIGO = codigo;
                        row.PCIA_NOMBRE = txtNombre.Text;
                        //Termina la edición de la fila
                        row.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsProvincia.PROVINCIAS.AddPROVINCIASRow(row);
                        dsProvincia.PROVINCIAS.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
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
                    //Está modificando
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    Entidades.Provincia provincia = new GyCAP.Entidades.Provincia();
                    provincia.Codigo = Convert.ToInt32(dvProvincias[dgvLista.SelectedRows[0].Index]["pcia_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    provincia.Nombre = txtNombre.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ProvinciaBLL.Actualizar(provincia);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsProveedor.PROVINCIASRow row = dsProvincia.PROVINCIAS.FindByPCIA_CODIGO(provincia.Codigo);
                        row.BeginEdit();
                        row.PCIA_NOMBRE = txtNombre.Text;
                        row.EndEdit();
                        dsProvincia.PROVINCIAS.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar el nombre.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsProvincia.PROVINCIAS.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    txtNombre.Text = String.Empty;
                    btnCancelar.Enabled = false;
                    dgvLista.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    btnCancelar.Enabled = true;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    break;
                default:
                    break;
            }
        }

        private void frmProvincia_Activated(object sender, EventArgs e)
        {
            if (txtNombre.Enabled == true)
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        #endregion

        
    }
}
