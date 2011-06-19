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
        private DataView dvUbicaciones;

        #region Inicio

        public frmEstructuraStock()
        {
            InitializeComponent();

            Inicializar();
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

        #region Servicios

        private void Inicializar()
        {
            try
            {
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvUbicaciones = new DataView(dsStock.UBICACIONES_STOCK);
        }

        #endregion
    }
}
