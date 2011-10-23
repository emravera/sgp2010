namespace GyCAP.UI.GestionPedido
{
    partial class frmDialogResult
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
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnAsignarFecha = new System.Windows.Forms.Button();
            this.btnAsignarCantidad = new System.Windows.Forms.Button();
            this.btnIgnorar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(61, 20);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(35, 13);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "label1";
            // 
            // btnAsignarFecha
            // 
            this.btnAsignarFecha.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAsignarFecha.Location = new System.Drawing.Point(40, 88);
            this.btnAsignarFecha.Name = "btnAsignarFecha";
            this.btnAsignarFecha.Size = new System.Drawing.Size(100, 30);
            this.btnAsignarFecha.TabIndex = 1;
            this.btnAsignarFecha.Text = "Asignar fecha";
            this.btnAsignarFecha.UseVisualStyleBackColor = true;
            // 
            // btnAsignarCantidad
            // 
            this.btnAsignarCantidad.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnAsignarCantidad.Location = new System.Drawing.Point(146, 88);
            this.btnAsignarCantidad.Name = "btnAsignarCantidad";
            this.btnAsignarCantidad.Size = new System.Drawing.Size(100, 30);
            this.btnAsignarCantidad.TabIndex = 2;
            this.btnAsignarCantidad.Text = "Asignar cantidad";
            this.btnAsignarCantidad.UseVisualStyleBackColor = true;
            // 
            // btnIgnorar
            // 
            this.btnIgnorar.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnIgnorar.Location = new System.Drawing.Point(252, 88);
            this.btnIgnorar.Name = "btnIgnorar";
            this.btnIgnorar.Size = new System.Drawing.Size(100, 30);
            this.btnIgnorar.TabIndex = 3;
            this.btnIgnorar.Text = "Ignorar";
            this.btnIgnorar.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Question_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 41);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // frmDialogResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 128);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnIgnorar);
            this.Controls.Add(this.btnAsignarCantidad);
            this.Controls.Add(this.btnAsignarFecha);
            this.Controls.Add(this.lblMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDialogResult";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDialogResult";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnAsignarFecha;
        private System.Windows.Forms.Button btnAsignarCantidad;
        private System.Windows.Forms.Button btnIgnorar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}