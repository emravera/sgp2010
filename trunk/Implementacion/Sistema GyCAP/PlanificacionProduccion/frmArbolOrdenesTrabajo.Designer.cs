namespace GyCAP.UI.PlanificacionProduccion
{
    partial class frmArbolOrdenesTrabajo
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
            this.tvOrdenes = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvOrdenes
            // 
            this.tvOrdenes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOrdenes.FullRowSelect = true;
            this.tvOrdenes.Location = new System.Drawing.Point(0, 0);
            this.tvOrdenes.Name = "tvOrdenes";
            this.tvOrdenes.Size = new System.Drawing.Size(377, 570);
            this.tvOrdenes.TabIndex = 0;
            // 
            // frmArbolOrdenesTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 570);
            this.Controls.Add(this.tvOrdenes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArbolOrdenesTrabajo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvOrdenes;
    }
}