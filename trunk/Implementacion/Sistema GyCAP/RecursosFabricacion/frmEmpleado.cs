﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmEmpleado : Form
    {
        private static frmEmpleado _frmEmpleado = null;
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado(); 
        private DataView dvEmpleado, dvEstadoEmpleado,dvEstadoEmpleadoBuscar, dvListaSectores;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmEmpleado()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Agregamos las columnas
            ColumnasGrillas columnas = new ColumnasGrillas();

            columnas.Add("E_CODIGO", "Código",true);
            columnas.Add("E_LEGAJO", "Legajo");
            columnas.Add("E_APELLIDO", "Apellido");
            columnas.Add("E_NOMBRE", "Nombre");
            columnas.Add("SEC_CODIGO", "Sector");
            columnas.Add("EE_CODIGO", "Estado");

            dgvLista.Columnas = columnas;

            //Creamos el dataview y lo asignamos a la grilla
            dvEmpleado = new DataView(dsEmpleado.EMPLEADOS);
            dvEmpleado.Sort = "E_APELLIDO, E_NOMBRE ASC";
            dgvLista.DataSource = dvEmpleado;

            //Llena el Dataset con los estados
            BLL.EstadoEmpleadoBLL.ObtenerTodos(dsEmpleado);

            //Llena el Dataset con los Sectores
            BLL.SectorBLL.ObtenerTodos(dsEmpleado);

            //Carga de la Lista de Sectores
            dvListaSectores = new DataView(dsEmpleado.SECTORES);
            FuncionesAuxiliares.llenarListas(dvListaSectores, clbSectores);

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvEstadoEmpleadoBuscar = new DataView(dsEmpleado.ESTADO_EMPLEADOS);
            dvEstadoEmpleadoBuscar.Sort = "EE_NOMBRE ASC";
            DataRowView newDRV = dvEstadoEmpleadoBuscar.AddNew();
            newDRV["EE_CODIGO"] = "0";
            newDRV["EE_NOMBRE"] = "--- Todos ---";
            newDRV.EndEdit();
            cboBuscarEstado.DataSource = dvEstadoEmpleadoBuscar;
            cboBuscarEstado.DisplayMember = "EE_NOMBRE";
            cboBuscarEstado.ValueMember = "EE_CODIGO";

            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cboBuscarEstado.SelectedIndex = 0;
            cboBuscarEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            cboBuscarPor.Items.Add("Legajo");
            cboBuscarPor.Items.Add("Nombre");
            cboBuscarPor.Items.Add("Apellido");
            cboBuscarPor.SelectedIndex = 0;

            //Combo de Datos
            dvEstadoEmpleado = new DataView(dsEmpleado.ESTADO_EMPLEADOS);

            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.DataSource = dvEstadoEmpleado ;
            cboEstado.DisplayMember = "EE_NOMBRE";
            cboEstado.ValueMember = "EE_CODIGO";
            cboEstado.SelectedIndex = 0;

            dvListaSectores = new DataView(dsEmpleado.SECTORES);
            cboSector.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSector.DataSource = dvListaSectores;
            cboSector.DisplayMember = "SEC_NOMBRE";
            cboSector.ValueMember = "SEC_CODIGO";
            cboSector.SelectedIndex = 0;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtApellido.MaxLength = 80;
            txtNombre.MaxLength = 80;
            txtLegajo.MaxLength = 20;
            txtTelefono.MaxLength = 15;            

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsEmpleado.EMPLEADOS.Rows.Count == 0)
                    {
                        hayDatos = false;
                        txtNombreBuscar.Focus();
                    }
                    else
                    {
                        hayDatos = true;
                        dgvLista.Focus();
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcABM.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    setBotones(false);
                    txtNombre.Text = String.Empty;
                    txtApellido.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    txtLegajo.Focus();
                    break;
                case estadoUI.consultar:
                    setBotones(true);
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    setBotones(false);
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcABM.SelectedTab = tpDatos;
                    txtApellido.Focus();
                    break;
                default:
                    break;
            }
        }

        private void setBotones(bool pValue) 
        {
            txtApellido.ReadOnly = pValue;
            txtNombre.ReadOnly = pValue;
            txtFechaNac.ReadOnly = pValue;
            txtLegajo.ReadOnly = pValue;
            txtTelefono.ReadOnly = pValue;
            cboEstado.Enabled = ! pValue;
            cboSector.Enabled = ! pValue;
        }

        //Método para evitar la creación de más de una pantalla
        public static frmEmpleado Instancia
        {
            get
            {
                if (_frmEmpleado == null || _frmEmpleado.IsDisposed)
                {
                    _frmEmpleado = new frmEmpleado();
                }
                else
                {
                    _frmEmpleado.BringToFront();
                }
                return _frmEmpleado;
            }
            set
            {
                _frmEmpleado = value;
            }
        }

        #endregion

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoEmpleado = Convert.ToInt32(dvEmpleado[e.RowIndex]["e_codigo"]);
            txtApellido.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_APELLIDO;
            txtNombre.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_NOMBRE;
            txtLegajo.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_LEGAJO;
            txtFechaNac.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_FECHANACIMIENTO.ToString();
            txtTelefono.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_TELEFONO;
            cboEstado.SelectedValue = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).EE_CODIGO;
            cboEstado.SelectedValue = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).SEC_CODIGO;
        }

        private void btnListado_Click(object sender, EventArgs e)
        {
            /*//frmVisorInformes visor = new frmVisorInformes();
            //visor.ShowDialog();
            BaseReporte2.conectar("NGA", "Proyecto",string.Empty, string.Empty);
            BaseReporte2.rutaRpt = "E:\\Repositorio\\Implementacion\\Sistema GyCAP\\RecursosFabricacion\\Reportes\\" ;
            BaseReporte2.printrpt("rptEmpleados.rpt","marcelo");*/

            Sistema.frmVisorReporte visor = new GyCAP.UI.Sistema.frmVisorReporte();
            Data.Reportes.reporte report = new GyCAP.Data.Reportes.reporte();
            report.SetDataSource(dsEmpleado);
            visor.crvVisor.ReportSource = report;
            visor.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsEmpleado.EMPLEADOS.Clear();
                BLL.EmpleadoBLL.ObtenerTodos(cboBuscarPor.Text,txtNombreBuscar.Text, int.Parse(cboBuscarEstado.SelectedValue.ToString()), dsEmpleado);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvEmpleado.Table = dsEmpleado.EMPLEADOS;
                if (dsEmpleado.EMPLEADOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Empleados con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Empleados - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columnas[e.ColumnIndex].campo)
                {
                    case "EE_CODIGO":
                        nombre = dsEmpleado.ESTADO_EMPLEADOS.FindByEE_CODIGO(Convert.ToInt32(e.Value)).EE_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "SEC_CODIGO":
                        nombre = dsEmpleado.SECTORES.FindBySEC_CODIGO(Convert.ToInt32(e.Value)).SEC_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

        



        
    }


}
