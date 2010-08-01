namespace GyCAP.UI.Sistema
{
    partial class frmVisorReporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crvVisor = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvVisor
            // 
            this.crvVisor.ActiveViewIndex = -1;
            this.crvVisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvVisor.DisplayGroupTree = false;
            this.crvVisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvVisor.Location = new System.Drawing.Point(0, 0);
            this.crvVisor.Name = "crvVisor";
            this.crvVisor.SelectionFormula = "";
            this.crvVisor.ShowCloseButton = false;
            this.crvVisor.ShowExportButton = false;
            this.crvVisor.ShowGotoPageButton = false;
            this.crvVisor.ShowGroupTreeButton = false;
            this.crvVisor.Size = new System.Drawing.Size(792, 570);
            this.crvVisor.TabIndex = 0;
            this.crvVisor.ViewTimeSelectionFormula = "";
            // 
            // frmVisorReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.crvVisor);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmVisorReporte";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Visor de Reportes";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvVisor;

    }
}