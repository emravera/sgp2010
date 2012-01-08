using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmFabricanteMaquina : Form
    {
        private static frmFabricanteMaquina _frmFabricanteMaquina = null;
        private Data.dsMaquina dsFabricante = new GyCAP.Data.dsMaquina();
        private DataView dvFabricante;
        private enum estadoUI { inicio, modificar };
        private estadoUI estadoInterface;

        #region Inicio

        public frmFabricanteMaquina()
        {
            InitializeComponent();
                        
            dgvLista.AutoGenerateColumns = false;
            
            dgvLista.Columns.Add("FAB_RAZONSOCIAL", "Razón socialNombre");
            dgvLista.Columns["FAB_RAZONSOCIAL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLista.Columns["FAB_RAZONSOCIAL"].DataPropertyName = "FAB_RAZONSOCIAL";
            
            BLL.FabricanteMaquinaBLL.ObtenerTodos(dsFabricante);
            dvFabricante = new DataView(dsFabricante.FABRICANTE_MAQUINAS);
            dvFabricante.Sort = "FAB_RAZONSOCIAL ASC";
            dgvLista.DataSource = dvFabricante;
            
            SetInterface(estadoUI.inicio);
        }

        public static frmFabricanteMaquina Instancia
        {
            get
            {
                if (_frmFabricanteMaquina == null || _frmFabricanteMaquina.IsDisposed)
                {
                    _frmFabricanteMaquina = new frmFabricanteMaquina();
                }
                else
                {
                    _frmFabricanteMaquina.BringToFront();
                }
                return _frmFabricanteMaquina;
            }
            set
            {
                _frmFabricanteMaquina = value;
            }
        }

        #endregion

        #region Botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvFabricante[dgvLista.SelectedRows[0].Index]["fab_codigo"]);
                txtNombre.Text = dsFabricante.FABRICANTE_MAQUINAS.FindByFAB_CODIGO(codigo).FAB_RAZONSOCIAL;
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Fabricante", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            } 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {            
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {                
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Fabricante", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {                        
                        int codigo = Convert.ToInt32(dvFabricante[dgvLista.SelectedRows[0].Index]["fab_codigo"]);                        
                        BLL.FabricanteMaquinaBLL.Eliminar(codigo);

                        dsFabricante.FABRICANTE_MAQUINAS.FindByFAB_CODIGO(codigo).Delete();
                        dsFabricante.FABRICANTE_MAQUINAS.AcceptChanges();

                        //Mensaje de confirmacion de eliminacion
                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        dsFabricante.FABRICANTE_MAQUINAS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsFabricante.FABRICANTE_MAQUINAS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Fabricante", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {            
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.FabricanteMaquina fabricante = new GyCAP.Entidades.FabricanteMaquina();
                                
                if (estadoInterface != estadoUI.modificar)
                {                    
                    fabricante.RazonSocial = txtNombre.Text;
                    try
                    {
                        fabricante.Codigo = int.Parse(BLL.FabricanteMaquinaBLL.Insertar(fabricante).ToString());

                        Data.dsMaquina.FABRICANTE_MAQUINASRow row = dsFabricante.FABRICANTE_MAQUINAS.NewFABRICANTE_MAQUINASRow();                        
                        row.BeginEdit();
                        row.FAB_CODIGO = fabricante.Codigo;
                        row.FAB_RAZONSOCIAL = fabricante.RazonSocial;                        
                        row.EndEdit();
                                                
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Fabricante", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        dsFabricante.FABRICANTE_MAQUINAS.AddFABRICANTE_MAQUINASRow(row);
                        dsFabricante.FABRICANTE_MAQUINAS.AcceptChanges();
                                                
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsFabricante.FABRICANTE_MAQUINAS.RejectChanges();                        
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsFabricante.FABRICANTE_MAQUINAS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    fabricante.Codigo = Convert.ToInt32(dvFabricante[dgvLista.SelectedRows[0].Index]["fab_codigo"]);                    
                    fabricante.RazonSocial = txtNombre.Text;

                    try
                    {
                        BLL.FabricanteMaquinaBLL.Actualizar(fabricante);

                        Data.dsMaquina.FABRICANTE_MAQUINASRow row = dsFabricante.FABRICANTE_MAQUINAS.FindByFAB_CODIGO(fabricante.Codigo);                        
                        row.BeginEdit();
                        row.FAB_RAZONSOCIAL = txtNombre.Text;
                        row.EndEdit();
                        dsFabricante.FABRICANTE_MAQUINAS.AcceptChanges();                        
                                                
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Fabricante", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);
                                                
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsFabricante.FABRICANTE_MAQUINAS.RejectChanges();                        
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsFabricante.FABRICANTE_MAQUINAS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
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

                    if (dsFabricante.FABRICANTE_MAQUINAS.Rows.Count == 0)
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

        private string Validar()
        {
            string erroresValidacion = string.Empty;

            //Control de los blancos de los textbox
            List<string> datos = new List<string>();
            if (txtNombre.Text.Trim().Length == 0)
            {
                datos.Add("Razón social");
                erroresValidacion = erroresValidacion + Entidades.Mensajes.MensajesABM.EscribirValidacion(GyCAP.Entidades.Mensajes.MensajesABM.Validaciones.CompletarDatos, datos);
            }

            return erroresValidacion;
        }        

        #endregion
    }
}
