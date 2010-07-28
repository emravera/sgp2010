using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
   
    public partial class Grilla : UserControl
    {
        public event DataGridViewCellFormattingEventHandler CellFormatting;

        public Grilla()
        {
            InitializeComponent();
            dgvLista.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvLista_CellFormatting);
        }

        void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (CellFormatting != null) 
            {
                CellFormatting(sender, e);
            }
        }



        ColumnasGrillas m_Columnas;
        public ColumnasGrillas Columnas 
        {
            get { return m_Columnas; }
            set { m_Columnas = value;
            LlenarColumnas();
            }
        }
  
        object m_DataSource;
        public object DataSource
        {
            get { return m_DataSource; }
            set
            {
                m_DataSource = value;
                RefrescarGrilla();
            }
        }

        private void LlenarColumnas()
        {
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Oculta la columna que contiene los encabezados
            dgvLista.RowHeadersVisible = false;

            if (m_Columnas != null)
            {
                foreach (ColumnaGrilla cg in m_Columnas)
                {
                    //Agrega las columnas
                    dgvLista.Columns.Add(cg.campo, cg.nombre);

                    //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
                    dgvLista.Columns[cg.campo].DataPropertyName = cg.campo;

                    //Setemaos las columnas               
                    dgvLista.Columns[cg.campo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    //Alineacion de los numeros y las fechas en la grilla
                    if (cg.alinearDerecha == true)
                    {
                        dgvLista.Columns[cg.campo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }

        }

        private void RefrescarGrilla()
        {
            dgvLista.DataSource = DataSource;
        }

    }
    public class ColumnaGrilla
    {
        public string nombre { get; set; }
        public string campo { get; set; }
        public bool alinearDerecha { get; set; }

        public ColumnaGrilla(string pCampo, string pNombre)
        {
            nombre = pNombre;
            campo = pCampo;
        }

        public ColumnaGrilla(string pCampo, string pNombre, bool pAlinearDerecha)
        {
            nombre = pNombre;
            campo = pCampo;
            alinearDerecha = pAlinearDerecha;
        }

    }

    public class ColumnasGrillas : List<ColumnaGrilla>
    {
        public ColumnaGrilla Add(string pCampo, string pNombre)
        {
            ColumnaGrilla cg = new ColumnaGrilla(pCampo, pNombre);
            this.Add(cg);
            return cg;
        }

        public ColumnaGrilla Add(string pCampo, string pNombre, bool pAlinearDerecha)
        {
            ColumnaGrilla cg = new ColumnaGrilla(pCampo, pNombre, pAlinearDerecha);
            this.Add(cg);
            return cg;
        }
    }
}
