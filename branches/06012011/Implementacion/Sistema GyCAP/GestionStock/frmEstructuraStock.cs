using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.GestionStock
{
    public partial class frmEstructuraStock : Form
    {
        private static frmEstructuraStock _frmEstructuraStock = null;
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();

        #region Inicio

        public frmEstructuraStock()
        {
            InitializeComponent();
            tvcEstructura.TreeView.CheckBoxes = false;
            CrearEstructura();
        }        

        public static frmEstructuraStock Instancia
        {
            get
            {
                if (_frmEstructuraStock == null || _frmEstructuraStock.IsDisposed)
                {
                    _frmEstructuraStock = new frmEstructuraStock();
                }
                else
                {
                    _frmEstructuraStock.BringToFront();
                }
                return _frmEstructuraStock;
            }
            set
            {
                _frmEstructuraStock = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion

        #region Datos
        
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CrearEstructura();
        }

        private void CrearEstructura()
        {
            dsStock.Clear();
            tvcEstructura.TreeView.Nodes.Clear();
            tvcEstructura.Columns.Clear();

            try
            {
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }            
            
            tvcEstructura.Columns.Add("ustck", "Ubicación de Stock");            
            tvcEstructura.Columns.Add("cantidad_real", "Cantidad Real");
            tvcEstructura.Columns.Add("cantidad_virtual", "Cantidad Virtual");
            tvcEstructura.Columns[0].Width = 400;
            tvcEstructura.Columns[1].Width = 180;
            tvcEstructura.Columns[2].Width = 180;
            
            foreach (Data.dsStock.UBICACIONES_STOCKRow row in (Data.dsStock.UBICACIONES_STOCKRow[])dsStock.UBICACIONES_STOCK.Select("ustck_padre IS NULL"))
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = row.USTCK_NOMBRE;
                nodo.Name = row.USTCK_NUMERO.ToString();
                nodo.Tag = new string[] { row.USTCK_CANTIDADREAL.ToString(), row.USTCK_CANTIDADVIRTUAL.ToString() };
                tvcEstructura.TreeView.Nodes.Add(nodo);

                CrearHijos(nodo);
            }
        }

        private void CrearHijos(TreeNode nodoPadre)
        {
            foreach (Data.dsStock.UBICACIONES_STOCKRow row in (Data.dsStock.UBICACIONES_STOCKRow[])dsStock.UBICACIONES_STOCK.Select("ustck_padre = " + Convert.ToInt32(nodoPadre.Name)))
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = row.USTCK_NOMBRE;
                nodo.Name = row.USTCK_NUMERO.ToString();
                nodo.Tag = new string[] { row.USTCK_CANTIDADREAL.ToString(), row.USTCK_CANTIDADVIRTUAL.ToString() };
                nodoPadre.Nodes.Add(nodo);

                CrearHijos(nodo);
            }
        }

        #endregion
        
    }
}
