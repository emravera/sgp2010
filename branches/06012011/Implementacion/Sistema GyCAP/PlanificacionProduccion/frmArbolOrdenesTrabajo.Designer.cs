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
            this.tvArbolDependenciaSimple = new System.Windows.Forms.TreeView();
            this.tcArbol = new System.Windows.Forms.TabControl();
            this.tpArbol1 = new System.Windows.Forms.TabPage();
            this.tcArbol.SuspendLayout();
            this.tpArbol1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvArbolDependenciaSimple
            // 
            this.tvArbolDependenciaSimple.BackColor = System.Drawing.SystemColors.Control;
            this.tvArbolDependenciaSimple.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvArbolDependenciaSimple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvArbolDependenciaSimple.FullRowSelect = true;
            this.tvArbolDependenciaSimple.HideSelection = false;
            this.tvArbolDependenciaSimple.Location = new System.Drawing.Point(3, 3);
            this.tvArbolDependenciaSimple.Name = "tvArbolDependenciaSimple";
            this.tvArbolDependenciaSimple.Size = new System.Drawing.Size(363, 542);
            this.tvArbolDependenciaSimple.TabIndex = 0;
            this.tvArbolDependenciaSimple.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvArbolDependenciaSimple_NodeMouseClick);
            // 
            // tcArbol
            // 
            this.tcArbol.Controls.Add(this.tpArbol1);
            this.tcArbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcArbol.Location = new System.Drawing.Point(0, 0);
            this.tcArbol.Name = "tcArbol";
            this.tcArbol.SelectedIndex = 0;
            this.tcArbol.Size = new System.Drawing.Size(377, 574);
            this.tcArbol.TabIndex = 1;
            this.tcArbol.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcArbol_Selected);
            // 
            // tpArbol1
            // 
            this.tpArbol1.Controls.Add(this.tvArbolDependenciaSimple);
            this.tpArbol1.Location = new System.Drawing.Point(4, 22);
            this.tpArbol1.Name = "tpArbol1";
            this.tpArbol1.Padding = new System.Windows.Forms.Padding(3);
            this.tpArbol1.Size = new System.Drawing.Size(369, 548);
            this.tpArbol1.TabIndex = 0;
            this.tpArbol1.Text = "Órdenes";
            this.tpArbol1.UseVisualStyleBackColor = true;
            // 
            // frmArbolOrdenesTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 574);
            this.Controls.Add(this.tcArbol);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArbolOrdenesTrabajo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tcArbol.ResumeLayout(false);
            this.tpArbol1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvArbolDependenciaSimple;
        private System.Windows.Forms.TabControl tcArbol;
        private System.Windows.Forms.TabPage tpArbol1;
    }
}