namespace GyCAP.UI.ControlTrabajoEnProceso
{
    partial class frmControlTrabajoEnProceso
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
            this.btnConsultarOrdenTrabajo = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnOrdenProduccion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnConsultarPlanificacion = new System.Windows.Forms.Button();
            this.btnPlanificacion = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.panelOrdenProduccion = new System.Windows.Forms.Panel();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPlanificacion = new System.Windows.Forms.Panel();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.panelOrdenProduccion.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelPlanificacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConsultarOrdenTrabajo
            // 
            this.btnConsultarOrdenTrabajo.AutoSize = true;
            this.btnConsultarOrdenTrabajo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarOrdenTrabajo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarOrdenTrabajo.FlatAppearance.BorderSize = 0;
            this.btnConsultarOrdenTrabajo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarOrdenTrabajo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarOrdenTrabajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarOrdenTrabajo.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Find_48;
            this.btnConsultarOrdenTrabajo.Location = new System.Drawing.Point(48, 8);
            this.btnConsultarOrdenTrabajo.Name = "btnConsultarOrdenTrabajo";
            this.btnConsultarOrdenTrabajo.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarOrdenTrabajo.TabIndex = 3;
            this.btnConsultarOrdenTrabajo.Text = "Consultar";
            this.btnConsultarOrdenTrabajo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarOrdenTrabajo.UseVisualStyleBackColor = true;
            this.btnConsultarOrdenTrabajo.Click += new System.EventHandler(this.btnConsultarPlanificacion_Click);
            this.btnConsultarOrdenTrabajo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarOrdenTrabajo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // scUp
            // 
            this.scUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scUp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scUp.IsSplitterFixed = true;
            this.scUp.Location = new System.Drawing.Point(0, 0);
            this.scUp.Name = "scUp";
            // 
            // scUp.Panel1
            // 
            this.scUp.Panel1.AutoScroll = true;
            this.scUp.Panel1.Controls.Add(this.btnMenu);
            this.scUp.Panel1MinSize = 20;
            // 
            // scUp.Panel2
            // 
            this.scUp.Panel2.AutoScroll = true;
            this.scUp.Panel2.AutoScrollMargin = new System.Drawing.Size(1000, 1000);
            this.scUp.Panel2.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.scUp_Panel2_ControlRemoved);
            this.scUp.Size = new System.Drawing.Size(624, 566);
            this.scUp.SplitterDistance = 20;
            this.scUp.SplitterWidth = 3;
            this.scUp.TabIndex = 0;
            // 
            // btnMenu
            // 
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMenu.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(13, 566);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // btnOrdenProduccion
            // 
            this.btnOrdenProduccion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrdenProduccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrdenProduccion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdenProduccion.Location = new System.Drawing.Point(0, 0);
            this.btnOrdenProduccion.Margin = new System.Windows.Forms.Padding(0);
            this.btnOrdenProduccion.Name = "btnOrdenProduccion";
            this.btnOrdenProduccion.Size = new System.Drawing.Size(158, 25);
            this.btnOrdenProduccion.TabIndex = 0;
            this.btnOrdenProduccion.Text = "Producción";
            this.btnOrdenProduccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrdenProduccion.UseVisualStyleBackColor = true;
            this.btnOrdenProduccion.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = true;
            this.btnSalir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Exit_48;
            this.btnSalir.Location = new System.Drawing.Point(52, 12);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(54, 71);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = " Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnSalir.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnConsultarPlanificacion
            // 
            this.btnConsultarPlanificacion.AutoSize = true;
            this.btnConsultarPlanificacion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarPlanificacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarPlanificacion.FlatAppearance.BorderSize = 0;
            this.btnConsultarPlanificacion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPlanificacion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPlanificacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarPlanificacion.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Find_48;
            this.btnConsultarPlanificacion.Location = new System.Drawing.Point(48, 5);
            this.btnConsultarPlanificacion.Name = "btnConsultarPlanificacion";
            this.btnConsultarPlanificacion.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarPlanificacion.TabIndex = 1;
            this.btnConsultarPlanificacion.Text = "Consultar";
            this.btnConsultarPlanificacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarPlanificacion.UseVisualStyleBackColor = true;
            this.btnConsultarPlanificacion.Click += new System.EventHandler(this.btnConsultarOrdenProduccion_Click);
            this.btnConsultarPlanificacion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarPlanificacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnPlanificacion
            // 
            this.btnPlanificacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlanificacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanificacion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanificacion.Location = new System.Drawing.Point(0, 179);
            this.btnPlanificacion.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlanificacion.Name = "btnPlanificacion";
            this.btnPlanificacion.Size = new System.Drawing.Size(158, 25);
            this.btnPlanificacion.TabIndex = 0;
            this.btnPlanificacion.Text = "Planificación";
            this.btnPlanificacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanificacion.UseVisualStyleBackColor = true;
            this.btnPlanificacion.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.btnSalir);
            this.panelSalir.Location = new System.Drawing.Point(0, 356);
            this.panelSalir.Margin = new System.Windows.Forms.Padding(0);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 87);
            this.panelSalir.TabIndex = 15;
            // 
            // panelOrdenProduccion
            // 
            this.panelOrdenProduccion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelOrdenProduccion.BackColor = System.Drawing.Color.Silver;
            this.panelOrdenProduccion.Controls.Add(this.btnConsultarPlanificacion);
            this.panelOrdenProduccion.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelOrdenProduccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrdenProduccion.Location = new System.Drawing.Point(0, 25);
            this.panelOrdenProduccion.Margin = new System.Windows.Forms.Padding(0);
            this.panelOrdenProduccion.Name = "panelOrdenProduccion";
            this.panelOrdenProduccion.Size = new System.Drawing.Size(158, 154);
            this.panelOrdenProduccion.TabIndex = 1;
            // 
            // scDown
            // 
            this.scDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDown.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scDown.IsSplitterFixed = true;
            this.scDown.Location = new System.Drawing.Point(0, 0);
            this.scDown.Name = "scDown";
            // 
            // scDown.Panel1
            // 
            this.scDown.Panel1.AutoScroll = true;
            this.scDown.Panel1.Controls.Add(this.flpMenu);
            // 
            // scDown.Panel2
            // 
            this.scDown.Panel2.AutoScroll = true;
            this.scDown.Panel2.Controls.Add(this.scUp);
            this.scDown.Size = new System.Drawing.Size(792, 570);
            this.scDown.SplitterDistance = 161;
            this.scDown.SplitterWidth = 3;
            this.scDown.TabIndex = 5;
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnOrdenProduccion);
            this.flpMenu.Controls.Add(this.panelOrdenProduccion);
            this.flpMenu.Controls.Add(this.btnPlanificacion);
            this.flpMenu.Controls.Add(this.panelPlanificacion);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 566);
            this.flpMenu.TabIndex = 0;
            // 
            // panelPlanificacion
            // 
            this.panelPlanificacion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelPlanificacion.BackColor = System.Drawing.Color.Silver;
            this.panelPlanificacion.Controls.Add(this.btnConsultarOrdenTrabajo);
            this.panelPlanificacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelPlanificacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlanificacion.Location = new System.Drawing.Point(0, 204);
            this.panelPlanificacion.Margin = new System.Windows.Forms.Padding(0);
            this.panelPlanificacion.Name = "panelPlanificacion";
            this.panelPlanificacion.Size = new System.Drawing.Size(158, 152);
            this.panelPlanificacion.TabIndex = 2;
            // 
            // frmControlTrabajoEnProceso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmControlTrabajoEnProceso";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Control de Trabajos En Proceso";
            this.Load += new System.EventHandler(this.frmControlTrabajoEnProceso_Load);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.panelOrdenProduccion.ResumeLayout(false);
            this.panelOrdenProduccion.PerformLayout();
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panelPlanificacion.ResumeLayout(false);
            this.panelPlanificacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsultarOrdenTrabajo;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnOrdenProduccion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnConsultarPlanificacion;
        private System.Windows.Forms.Button btnPlanificacion;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Panel panelOrdenProduccion;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelPlanificacion;
    }
}