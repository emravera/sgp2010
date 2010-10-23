namespace GyCAP.UI.Mantenimiento
{
    partial class frmModuloMantenimiento
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
            this.btnConsultarPlanMantenimiento = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnNuevoPlanMantenimiento = new System.Windows.Forms.Button();
            this.btnMantenimiento = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnConsultarMantenimiento = new System.Windows.Forms.Button();
            this.btnPlanMantenimiento = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.panelMantenimiento = new System.Windows.Forms.Panel();
            this.btnNuevoMantenimiento = new System.Windows.Forms.Button();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPlanMantenimiento = new System.Windows.Forms.Panel();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.panelMantenimiento.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelPlanMantenimiento.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConsultarPlanMantenimiento
            // 
            this.btnConsultarPlanMantenimiento.AutoSize = true;
            this.btnConsultarPlanMantenimiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarPlanMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarPlanMantenimiento.FlatAppearance.BorderSize = 0;
            this.btnConsultarPlanMantenimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPlanMantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPlanMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarPlanMantenimiento.Image = global::GyCAP.UI.Mantenimiento.Properties.Resources.Find_48;
            this.btnConsultarPlanMantenimiento.Location = new System.Drawing.Point(48, 72);
            this.btnConsultarPlanMantenimiento.Name = "btnConsultarPlanMantenimiento";
            this.btnConsultarPlanMantenimiento.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarPlanMantenimiento.TabIndex = 3;
            this.btnConsultarPlanMantenimiento.Text = "Consultar";
            this.btnConsultarPlanMantenimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarPlanMantenimiento.UseVisualStyleBackColor = true;
            this.btnConsultarPlanMantenimiento.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarPlanMantenimiento.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            // btnNuevoPlanMantenimiento
            // 
            this.btnNuevoPlanMantenimiento.AutoSize = true;
            this.btnNuevoPlanMantenimiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoPlanMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoPlanMantenimiento.FlatAppearance.BorderSize = 0;
            this.btnNuevoPlanMantenimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoPlanMantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoPlanMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoPlanMantenimiento.Image = global::GyCAP.UI.Mantenimiento.Properties.Resources.New_48;
            this.btnNuevoPlanMantenimiento.Location = new System.Drawing.Point(52, 4);
            this.btnNuevoPlanMantenimiento.Name = "btnNuevoPlanMantenimiento";
            this.btnNuevoPlanMantenimiento.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoPlanMantenimiento.TabIndex = 2;
            this.btnNuevoPlanMantenimiento.Text = " Nuevo";
            this.btnNuevoPlanMantenimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoPlanMantenimiento.UseVisualStyleBackColor = true;
            this.btnNuevoPlanMantenimiento.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoPlanMantenimiento.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnMantenimiento
            // 
            this.btnMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMantenimiento.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMantenimiento.Location = new System.Drawing.Point(0, 0);
            this.btnMantenimiento.Margin = new System.Windows.Forms.Padding(0);
            this.btnMantenimiento.Name = "btnMantenimiento";
            this.btnMantenimiento.Size = new System.Drawing.Size(158, 25);
            this.btnMantenimiento.TabIndex = 0;
            this.btnMantenimiento.Text = "Mantenimiento";
            this.btnMantenimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMantenimiento.UseVisualStyleBackColor = true;
            this.btnMantenimiento.Click += new System.EventHandler(this.btn_Click);
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
            this.btnSalir.Image = global::GyCAP.UI.Mantenimiento.Properties.Resources.Exit_48;
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
            // btnConsultarMantenimiento
            // 
            this.btnConsultarMantenimiento.AutoSize = true;
            this.btnConsultarMantenimiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarMantenimiento.FlatAppearance.BorderSize = 0;
            this.btnConsultarMantenimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarMantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarMantenimiento.Image = global::GyCAP.UI.Mantenimiento.Properties.Resources.Find_48;
            this.btnConsultarMantenimiento.Location = new System.Drawing.Point(48, 75);
            this.btnConsultarMantenimiento.Name = "btnConsultarMantenimiento";
            this.btnConsultarMantenimiento.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarMantenimiento.TabIndex = 1;
            this.btnConsultarMantenimiento.Text = "Consultar";
            this.btnConsultarMantenimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarMantenimiento.UseVisualStyleBackColor = true;
            this.btnConsultarMantenimiento.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarMantenimiento.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnPlanMantenimiento
            // 
            this.btnPlanMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlanMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanMantenimiento.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanMantenimiento.Location = new System.Drawing.Point(0, 179);
            this.btnPlanMantenimiento.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlanMantenimiento.Name = "btnPlanMantenimiento";
            this.btnPlanMantenimiento.Size = new System.Drawing.Size(158, 25);
            this.btnPlanMantenimiento.TabIndex = 0;
            this.btnPlanMantenimiento.Text = "Plan Mantenimiento";
            this.btnPlanMantenimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanMantenimiento.UseVisualStyleBackColor = true;
            this.btnPlanMantenimiento.Click += new System.EventHandler(this.btn_Click);
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
            // panelMantenimiento
            // 
            this.panelMantenimiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMantenimiento.BackColor = System.Drawing.Color.Silver;
            this.panelMantenimiento.Controls.Add(this.btnNuevoMantenimiento);
            this.panelMantenimiento.Controls.Add(this.btnConsultarMantenimiento);
            this.panelMantenimiento.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelMantenimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMantenimiento.Location = new System.Drawing.Point(0, 25);
            this.panelMantenimiento.Margin = new System.Windows.Forms.Padding(0);
            this.panelMantenimiento.Name = "panelMantenimiento";
            this.panelMantenimiento.Size = new System.Drawing.Size(158, 154);
            this.panelMantenimiento.TabIndex = 1;
            // 
            // btnNuevoMantenimiento
            // 
            this.btnNuevoMantenimiento.AutoSize = true;
            this.btnNuevoMantenimiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoMantenimiento.FlatAppearance.BorderSize = 0;
            this.btnNuevoMantenimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoMantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoMantenimiento.Image = global::GyCAP.UI.Mantenimiento.Properties.Resources.New_48;
            this.btnNuevoMantenimiento.Location = new System.Drawing.Point(52, 3);
            this.btnNuevoMantenimiento.Name = "btnNuevoMantenimiento";
            this.btnNuevoMantenimiento.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoMantenimiento.TabIndex = 3;
            this.btnNuevoMantenimiento.Text = " Nuevo";
            this.btnNuevoMantenimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoMantenimiento.UseVisualStyleBackColor = true;
            this.btnNuevoMantenimiento.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoMantenimiento.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            this.flpMenu.Controls.Add(this.btnMantenimiento);
            this.flpMenu.Controls.Add(this.panelMantenimiento);
            this.flpMenu.Controls.Add(this.btnPlanMantenimiento);
            this.flpMenu.Controls.Add(this.panelPlanMantenimiento);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 566);
            this.flpMenu.TabIndex = 0;
            // 
            // panelPlanMantenimiento
            // 
            this.panelPlanMantenimiento.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelPlanMantenimiento.BackColor = System.Drawing.Color.Silver;
            this.panelPlanMantenimiento.Controls.Add(this.btnConsultarPlanMantenimiento);
            this.panelPlanMantenimiento.Controls.Add(this.btnNuevoPlanMantenimiento);
            this.panelPlanMantenimiento.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelPlanMantenimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlanMantenimiento.Location = new System.Drawing.Point(0, 204);
            this.panelPlanMantenimiento.Margin = new System.Windows.Forms.Padding(0);
            this.panelPlanMantenimiento.Name = "panelPlanMantenimiento";
            this.panelPlanMantenimiento.Size = new System.Drawing.Size(158, 152);
            this.panelPlanMantenimiento.TabIndex = 2;
            // 
            // frmModuloMantenimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmModuloMantenimiento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Mantenimiento";
            this.Load += new System.EventHandler(this.frmModuloMantenimiento_Load);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.panelMantenimiento.ResumeLayout(false);
            this.panelMantenimiento.PerformLayout();
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panelPlanMantenimiento.ResumeLayout(false);
            this.panelPlanMantenimiento.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsultarPlanMantenimiento;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnNuevoPlanMantenimiento;
        private System.Windows.Forms.Button btnMantenimiento;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnConsultarMantenimiento;
        private System.Windows.Forms.Button btnPlanMantenimiento;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Panel panelMantenimiento;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelPlanMantenimiento;
        private System.Windows.Forms.Button btnNuevoMantenimiento;
    }
}