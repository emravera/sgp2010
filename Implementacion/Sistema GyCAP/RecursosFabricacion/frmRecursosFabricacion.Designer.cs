namespace GyCAP.UI.RecursosFabricacion
{
    partial class frmRecursosFabricacion
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
            this.btnEmpleado = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.panelEmpleado = new System.Windows.Forms.Panel();
            this.btnListadoEmpleado = new System.Windows.Forms.Button();
            this.btnConsultarEmpleado = new System.Windows.Forms.Button();
            this.btnNuevoEmpleado = new System.Windows.Forms.Button();
            this.btnMaquina = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMaquina = new System.Windows.Forms.Panel();
            this.btnListadoMaquina = new System.Windows.Forms.Button();
            this.btnConsultarMaquina = new System.Windows.Forms.Button();
            this.btnNuevoMaquina = new System.Windows.Forms.Button();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnProductividad = new System.Windows.Forms.Button();
            this.panelProductividad = new System.Windows.Forms.Panel();
            this.btnIndiceProductividad = new System.Windows.Forms.Button();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.panelEmpleado.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.panelMaquina.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelProductividad.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEmpleado
            // 
            this.btnEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpleado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpleado.Location = new System.Drawing.Point(0, 0);
            this.btnEmpleado.Margin = new System.Windows.Forms.Padding(0);
            this.btnEmpleado.Name = "btnEmpleado";
            this.btnEmpleado.Size = new System.Drawing.Size(158, 25);
            this.btnEmpleado.TabIndex = 0;
            this.btnEmpleado.Text = "Empleado";
            this.btnEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmpleado.UseVisualStyleBackColor = true;
            this.btnEmpleado.Click += new System.EventHandler(this.btn_Click);
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
            this.scUp.Size = new System.Drawing.Size(626, 568);
            this.scUp.SplitterDistance = 20;
            this.scUp.SplitterWidth = 3;
            this.scUp.TabIndex = 0;
            // 
            // btnMenu
            // 
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMenu.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(13, 568);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelEmpleado
            // 
            this.panelEmpleado.AutoSize = true;
            this.panelEmpleado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEmpleado.BackColor = System.Drawing.Color.Silver;
            this.panelEmpleado.Controls.Add(this.btnListadoEmpleado);
            this.panelEmpleado.Controls.Add(this.btnConsultarEmpleado);
            this.panelEmpleado.Controls.Add(this.btnNuevoEmpleado);
            this.panelEmpleado.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEmpleado.Location = new System.Drawing.Point(0, 25);
            this.panelEmpleado.Margin = new System.Windows.Forms.Padding(0);
            this.panelEmpleado.Name = "panelEmpleado";
            this.panelEmpleado.Size = new System.Drawing.Size(158, 219);
            this.panelEmpleado.TabIndex = 1;
            this.panelEmpleado.Visible = false;
            // 
            // btnListadoEmpleado
            // 
            this.btnListadoEmpleado.AutoSize = true;
            this.btnListadoEmpleado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnListadoEmpleado.BackColor = System.Drawing.Color.Silver;
            this.btnListadoEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListadoEmpleado.FlatAppearance.BorderSize = 0;
            this.btnListadoEmpleado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnListadoEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnListadoEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListadoEmpleado.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Listados_48;
            this.btnListadoEmpleado.Location = new System.Drawing.Point(51, 145);
            this.btnListadoEmpleado.Name = "btnListadoEmpleado";
            this.btnListadoEmpleado.Size = new System.Drawing.Size(56, 71);
            this.btnListadoEmpleado.TabIndex = 3;
            this.btnListadoEmpleado.Text = "Listados";
            this.btnListadoEmpleado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnListadoEmpleado.UseVisualStyleBackColor = false;
            this.btnListadoEmpleado.Click += new System.EventHandler(this.btnListadoEmpleado_Click);
            // 
            // btnConsultarEmpleado
            // 
            this.btnConsultarEmpleado.AutoSize = true;
            this.btnConsultarEmpleado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarEmpleado.FlatAppearance.BorderSize = 0;
            this.btnConsultarEmpleado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnConsultarEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarEmpleado.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Find_48;
            this.btnConsultarEmpleado.Location = new System.Drawing.Point(48, 74);
            this.btnConsultarEmpleado.Name = "btnConsultarEmpleado";
            this.btnConsultarEmpleado.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarEmpleado.TabIndex = 1;
            this.btnConsultarEmpleado.Text = "Consultar";
            this.btnConsultarEmpleado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarEmpleado.UseVisualStyleBackColor = true;
            this.btnConsultarEmpleado.Click += new System.EventHandler(this.btnConsultarEmpleado_Click);
            // 
            // btnNuevoEmpleado
            // 
            this.btnNuevoEmpleado.AutoSize = true;
            this.btnNuevoEmpleado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoEmpleado.FlatAppearance.BorderSize = 0;
            this.btnNuevoEmpleado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNuevoEmpleado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoEmpleado.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.New_48;
            this.btnNuevoEmpleado.Location = new System.Drawing.Point(52, 3);
            this.btnNuevoEmpleado.Name = "btnNuevoEmpleado";
            this.btnNuevoEmpleado.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoEmpleado.TabIndex = 0;
            this.btnNuevoEmpleado.Text = " Nuevo";
            this.btnNuevoEmpleado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoEmpleado.UseVisualStyleBackColor = true;
            this.btnNuevoEmpleado.Click += new System.EventHandler(this.btnNuevoEmpleado_Click);
            // 
            // btnMaquina
            // 
            this.btnMaquina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaquina.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaquina.Location = new System.Drawing.Point(0, 244);
            this.btnMaquina.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaquina.Name = "btnMaquina";
            this.btnMaquina.Size = new System.Drawing.Size(158, 25);
            this.btnMaquina.TabIndex = 3;
            this.btnMaquina.Text = "Máquina";
            this.btnMaquina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaquina.UseVisualStyleBackColor = true;
            this.btnMaquina.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.button1);
            this.panelSalir.Location = new System.Drawing.Point(158, 79);
            this.panelSalir.Margin = new System.Windows.Forms.Padding(0);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 87);
            this.panelSalir.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Exit_48;
            this.button1.Location = new System.Drawing.Point(48, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 71);
            this.button1.TabIndex = 2;
            this.button1.Text = " Salir";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panelMaquina
            // 
            this.panelMaquina.AutoSize = true;
            this.panelMaquina.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMaquina.BackColor = System.Drawing.Color.Silver;
            this.panelMaquina.Controls.Add(this.btnListadoMaquina);
            this.panelMaquina.Controls.Add(this.btnConsultarMaquina);
            this.panelMaquina.Controls.Add(this.btnNuevoMaquina);
            this.panelMaquina.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelMaquina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaquina.Location = new System.Drawing.Point(0, 269);
            this.panelMaquina.Margin = new System.Windows.Forms.Padding(0);
            this.panelMaquina.Name = "panelMaquina";
            this.panelMaquina.Size = new System.Drawing.Size(158, 219);
            this.panelMaquina.TabIndex = 4;
            this.panelMaquina.Visible = false;
            // 
            // btnListadoMaquina
            // 
            this.btnListadoMaquina.AutoSize = true;
            this.btnListadoMaquina.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnListadoMaquina.BackColor = System.Drawing.Color.Silver;
            this.btnListadoMaquina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListadoMaquina.FlatAppearance.BorderSize = 0;
            this.btnListadoMaquina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnListadoMaquina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnListadoMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListadoMaquina.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Listados_48;
            this.btnListadoMaquina.Location = new System.Drawing.Point(51, 145);
            this.btnListadoMaquina.Name = "btnListadoMaquina";
            this.btnListadoMaquina.Size = new System.Drawing.Size(56, 71);
            this.btnListadoMaquina.TabIndex = 4;
            this.btnListadoMaquina.Text = "Listados";
            this.btnListadoMaquina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnListadoMaquina.UseVisualStyleBackColor = false;
            this.btnListadoMaquina.Click += new System.EventHandler(this.btnListadoMaquina_Click);
            // 
            // btnConsultarMaquina
            // 
            this.btnConsultarMaquina.AutoSize = true;
            this.btnConsultarMaquina.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarMaquina.FlatAppearance.BorderSize = 0;
            this.btnConsultarMaquina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnConsultarMaquina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarMaquina.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Find_48;
            this.btnConsultarMaquina.Location = new System.Drawing.Point(48, 74);
            this.btnConsultarMaquina.Name = "btnConsultarMaquina";
            this.btnConsultarMaquina.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarMaquina.TabIndex = 1;
            this.btnConsultarMaquina.Text = "Consultar";
            this.btnConsultarMaquina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarMaquina.UseVisualStyleBackColor = true;
            this.btnConsultarMaquina.Click += new System.EventHandler(this.btnConsultarMaquina_Click);
            // 
            // btnNuevoMaquina
            // 
            this.btnNuevoMaquina.AutoSize = true;
            this.btnNuevoMaquina.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoMaquina.FlatAppearance.BorderSize = 0;
            this.btnNuevoMaquina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNuevoMaquina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoMaquina.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.New_48;
            this.btnNuevoMaquina.Location = new System.Drawing.Point(52, 3);
            this.btnNuevoMaquina.Name = "btnNuevoMaquina";
            this.btnNuevoMaquina.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoMaquina.TabIndex = 0;
            this.btnNuevoMaquina.Text = " Nuevo";
            this.btnNuevoMaquina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoMaquina.UseVisualStyleBackColor = true;
            this.btnNuevoMaquina.Click += new System.EventHandler(this.btnNuevoMaquina_Click);
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnEmpleado);
            this.flpMenu.Controls.Add(this.panelEmpleado);
            this.flpMenu.Controls.Add(this.btnMaquina);
            this.flpMenu.Controls.Add(this.panelMaquina);
            this.flpMenu.Controls.Add(this.btnProductividad);
            this.flpMenu.Controls.Add(this.panelProductividad);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 568);
            this.flpMenu.TabIndex = 0;
            // 
            // btnProductividad
            // 
            this.btnProductividad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductividad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductividad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductividad.Location = new System.Drawing.Point(0, 488);
            this.btnProductividad.Margin = new System.Windows.Forms.Padding(0);
            this.btnProductividad.Name = "btnProductividad";
            this.btnProductividad.Size = new System.Drawing.Size(158, 25);
            this.btnProductividad.TabIndex = 9;
            this.btnProductividad.Text = "Productividad";
            this.btnProductividad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductividad.UseVisualStyleBackColor = true;
            this.btnProductividad.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelProductividad
            // 
            this.panelProductividad.AutoSize = true;
            this.panelProductividad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelProductividad.BackColor = System.Drawing.Color.Silver;
            this.panelProductividad.Controls.Add(this.btnIndiceProductividad);
            this.panelProductividad.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelProductividad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProductividad.Location = new System.Drawing.Point(158, 0);
            this.panelProductividad.Margin = new System.Windows.Forms.Padding(0);
            this.panelProductividad.Name = "panelProductividad";
            this.panelProductividad.Size = new System.Drawing.Size(158, 79);
            this.panelProductividad.TabIndex = 10;
            this.panelProductividad.Visible = false;
            // 
            // btnIndiceProductividad
            // 
            this.btnIndiceProductividad.AutoSize = true;
            this.btnIndiceProductividad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnIndiceProductividad.FlatAppearance.BorderSize = 0;
            this.btnIndiceProductividad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnIndiceProductividad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnIndiceProductividad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndiceProductividad.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.New_48;
            this.btnIndiceProductividad.Location = new System.Drawing.Point(52, 5);
            this.btnIndiceProductividad.Name = "btnIndiceProductividad";
            this.btnIndiceProductividad.Size = new System.Drawing.Size(54, 71);
            this.btnIndiceProductividad.TabIndex = 0;
            this.btnIndiceProductividad.Text = "Índice";
            this.btnIndiceProductividad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIndiceProductividad.UseVisualStyleBackColor = true;
            this.btnIndiceProductividad.Click += new System.EventHandler(this.btnIndiceProductividad_Click);
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
            this.scDown.Size = new System.Drawing.Size(794, 572);
            this.scDown.SplitterDistance = 161;
            this.scDown.SplitterWidth = 3;
            this.scDown.TabIndex = 3;
            // 
            // frmRecursosFabricacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmRecursosFabricacion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Recursos de Fabricación";
            this.Load += new System.EventHandler(this.frmRecursosFabricacion_Load);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.panelEmpleado.ResumeLayout(false);
            this.panelEmpleado.PerformLayout();
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.panelMaquina.ResumeLayout(false);
            this.panelMaquina.PerformLayout();
            this.flpMenu.ResumeLayout(false);
            this.flpMenu.PerformLayout();
            this.panelProductividad.ResumeLayout(false);
            this.panelProductividad.PerformLayout();
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEmpleado;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnNuevoMaquina;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnIndiceProductividad;
        private System.Windows.Forms.Button btnConsultarMaquina;
        private System.Windows.Forms.Panel panelEmpleado;
        private System.Windows.Forms.Button btnConsultarEmpleado;
        private System.Windows.Forms.Button btnNuevoEmpleado;
        private System.Windows.Forms.Button btnMaquina;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Panel panelMaquina;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Button btnProductividad;
        private System.Windows.Forms.Panel panelProductividad;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.Button btnListadoEmpleado;
        private System.Windows.Forms.Button btnListadoMaquina;
    }
}