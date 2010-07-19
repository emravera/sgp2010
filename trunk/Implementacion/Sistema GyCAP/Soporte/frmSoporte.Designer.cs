namespace GyCAP.UI.Soporte
{
    partial class frmSoporte
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
            this.btnCapacidadEmpleado = new System.Windows.Forms.Button();
            this.btnLocalidad = new System.Windows.Forms.Button();
            this.panelCapacidadEmpleado = new System.Windows.Forms.Panel();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelLocalidad = new System.Windows.Forms.Panel();
            this.btnMarca = new System.Windows.Forms.Button();
            this.panelMarca = new System.Windows.Forms.Panel();
            this.btnProcesoFabricacion = new System.Windows.Forms.Button();
            this.panelProcesoFabricacion = new System.Windows.Forms.Panel();
            this.btnProvincia = new System.Windows.Forms.Button();
            this.panelProvincia = new System.Windows.Forms.Panel();
            this.btnSectorTrabajo = new System.Windows.Forms.Button();
            this.panelSectorTrabajo = new System.Windows.Forms.Panel();
            this.btnTipoRepuesto = new System.Windows.Forms.Button();
            this.panelTipoRepuesto = new System.Windows.Forms.Panel();
            this.btnTipoUnidadMedida = new System.Windows.Forms.Button();
            this.panelTipoUnidadMedida = new System.Windows.Forms.Panel();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCapacidadEmpleado
            // 
            this.btnCapacidadEmpleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapacidadEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapacidadEmpleado.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapacidadEmpleado.Location = new System.Drawing.Point(0, 0);
            this.btnCapacidadEmpleado.Margin = new System.Windows.Forms.Padding(0);
            this.btnCapacidadEmpleado.Name = "btnCapacidadEmpleado";
            this.btnCapacidadEmpleado.Size = new System.Drawing.Size(158, 25);
            this.btnCapacidadEmpleado.TabIndex = 0;
            this.btnCapacidadEmpleado.Text = "Capacidad de Empleado";
            this.btnCapacidadEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapacidadEmpleado.UseVisualStyleBackColor = true;
            this.btnCapacidadEmpleado.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnLocalidad
            // 
            this.btnLocalidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocalidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalidad.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocalidad.Location = new System.Drawing.Point(0, 125);
            this.btnLocalidad.Margin = new System.Windows.Forms.Padding(0);
            this.btnLocalidad.Name = "btnLocalidad";
            this.btnLocalidad.Size = new System.Drawing.Size(158, 25);
            this.btnLocalidad.TabIndex = 0;
            this.btnLocalidad.Text = "Localidad";
            this.btnLocalidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLocalidad.UseVisualStyleBackColor = true;
            this.btnLocalidad.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelCapacidadEmpleado
            // 
            this.panelCapacidadEmpleado.BackColor = System.Drawing.Color.Silver;
            this.panelCapacidadEmpleado.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelCapacidadEmpleado.Location = new System.Drawing.Point(0, 25);
            this.panelCapacidadEmpleado.Margin = new System.Windows.Forms.Padding(0);
            this.panelCapacidadEmpleado.Name = "panelCapacidadEmpleado";
            this.panelCapacidadEmpleado.Size = new System.Drawing.Size(158, 100);
            this.panelCapacidadEmpleado.TabIndex = 1;
            this.panelCapacidadEmpleado.Visible = false;
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
            this.scDown.TabIndex = 2;
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnCapacidadEmpleado);
            this.flpMenu.Controls.Add(this.panelCapacidadEmpleado);
            this.flpMenu.Controls.Add(this.btnLocalidad);
            this.flpMenu.Controls.Add(this.panelLocalidad);
            this.flpMenu.Controls.Add(this.btnMarca);
            this.flpMenu.Controls.Add(this.panelMarca);
            this.flpMenu.Controls.Add(this.btnProcesoFabricacion);
            this.flpMenu.Controls.Add(this.panelProcesoFabricacion);
            this.flpMenu.Controls.Add(this.btnProvincia);
            this.flpMenu.Controls.Add(this.panelProvincia);
            this.flpMenu.Controls.Add(this.btnSectorTrabajo);
            this.flpMenu.Controls.Add(this.panelSectorTrabajo);
            this.flpMenu.Controls.Add(this.btnTipoRepuesto);
            this.flpMenu.Controls.Add(this.panelTipoRepuesto);
            this.flpMenu.Controls.Add(this.btnTipoUnidadMedida);
            this.flpMenu.Controls.Add(this.panelTipoUnidadMedida);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 568);
            this.flpMenu.TabIndex = 0;
            // 
            // panelLocalidad
            // 
            this.panelLocalidad.BackColor = System.Drawing.Color.Silver;
            this.panelLocalidad.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelLocalidad.Location = new System.Drawing.Point(0, 150);
            this.panelLocalidad.Margin = new System.Windows.Forms.Padding(0);
            this.panelLocalidad.Name = "panelLocalidad";
            this.panelLocalidad.Size = new System.Drawing.Size(158, 100);
            this.panelLocalidad.TabIndex = 2;
            this.panelLocalidad.Visible = false;
            // 
            // btnMarca
            // 
            this.btnMarca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarca.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarca.Location = new System.Drawing.Point(0, 250);
            this.btnMarca.Margin = new System.Windows.Forms.Padding(0);
            this.btnMarca.Name = "btnMarca";
            this.btnMarca.Size = new System.Drawing.Size(158, 25);
            this.btnMarca.TabIndex = 3;
            this.btnMarca.Text = "Marca";
            this.btnMarca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarca.UseVisualStyleBackColor = true;
            this.btnMarca.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelMarca
            // 
            this.panelMarca.BackColor = System.Drawing.Color.Silver;
            this.panelMarca.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelMarca.Location = new System.Drawing.Point(0, 275);
            this.panelMarca.Margin = new System.Windows.Forms.Padding(0);
            this.panelMarca.Name = "panelMarca";
            this.panelMarca.Size = new System.Drawing.Size(158, 100);
            this.panelMarca.TabIndex = 4;
            this.panelMarca.Visible = false;
            // 
            // btnProcesoFabricacion
            // 
            this.btnProcesoFabricacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcesoFabricacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesoFabricacion.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesoFabricacion.Location = new System.Drawing.Point(0, 375);
            this.btnProcesoFabricacion.Margin = new System.Windows.Forms.Padding(0);
            this.btnProcesoFabricacion.Name = "btnProcesoFabricacion";
            this.btnProcesoFabricacion.Size = new System.Drawing.Size(158, 25);
            this.btnProcesoFabricacion.TabIndex = 5;
            this.btnProcesoFabricacion.Text = "Proceso de Fabricación";
            this.btnProcesoFabricacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProcesoFabricacion.UseVisualStyleBackColor = true;
            this.btnProcesoFabricacion.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelProcesoFabricacion
            // 
            this.panelProcesoFabricacion.BackColor = System.Drawing.Color.Silver;
            this.panelProcesoFabricacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelProcesoFabricacion.Location = new System.Drawing.Point(0, 400);
            this.panelProcesoFabricacion.Margin = new System.Windows.Forms.Padding(0);
            this.panelProcesoFabricacion.Name = "panelProcesoFabricacion";
            this.panelProcesoFabricacion.Size = new System.Drawing.Size(158, 100);
            this.panelProcesoFabricacion.TabIndex = 6;
            this.panelProcesoFabricacion.Visible = false;
            // 
            // btnProvincia
            // 
            this.btnProvincia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProvincia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProvincia.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProvincia.Location = new System.Drawing.Point(0, 500);
            this.btnProvincia.Margin = new System.Windows.Forms.Padding(0);
            this.btnProvincia.Name = "btnProvincia";
            this.btnProvincia.Size = new System.Drawing.Size(158, 25);
            this.btnProvincia.TabIndex = 7;
            this.btnProvincia.Text = "Provincia";
            this.btnProvincia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProvincia.UseVisualStyleBackColor = true;
            this.btnProvincia.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelProvincia
            // 
            this.panelProvincia.BackColor = System.Drawing.Color.Silver;
            this.panelProvincia.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelProvincia.Location = new System.Drawing.Point(158, 0);
            this.panelProvincia.Margin = new System.Windows.Forms.Padding(0);
            this.panelProvincia.Name = "panelProvincia";
            this.panelProvincia.Size = new System.Drawing.Size(158, 100);
            this.panelProvincia.TabIndex = 8;
            this.panelProvincia.Visible = false;
            // 
            // btnSectorTrabajo
            // 
            this.btnSectorTrabajo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSectorTrabajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSectorTrabajo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSectorTrabajo.Location = new System.Drawing.Point(158, 100);
            this.btnSectorTrabajo.Margin = new System.Windows.Forms.Padding(0);
            this.btnSectorTrabajo.Name = "btnSectorTrabajo";
            this.btnSectorTrabajo.Size = new System.Drawing.Size(158, 25);
            this.btnSectorTrabajo.TabIndex = 9;
            this.btnSectorTrabajo.Text = "Sector de Trabajo";
            this.btnSectorTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSectorTrabajo.UseVisualStyleBackColor = true;
            this.btnSectorTrabajo.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelSectorTrabajo
            // 
            this.panelSectorTrabajo.BackColor = System.Drawing.Color.Silver;
            this.panelSectorTrabajo.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelSectorTrabajo.Location = new System.Drawing.Point(158, 125);
            this.panelSectorTrabajo.Margin = new System.Windows.Forms.Padding(0);
            this.panelSectorTrabajo.Name = "panelSectorTrabajo";
            this.panelSectorTrabajo.Size = new System.Drawing.Size(158, 100);
            this.panelSectorTrabajo.TabIndex = 10;
            this.panelSectorTrabajo.Visible = false;
            // 
            // btnTipoRepuesto
            // 
            this.btnTipoRepuesto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTipoRepuesto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTipoRepuesto.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTipoRepuesto.Location = new System.Drawing.Point(158, 225);
            this.btnTipoRepuesto.Margin = new System.Windows.Forms.Padding(0);
            this.btnTipoRepuesto.Name = "btnTipoRepuesto";
            this.btnTipoRepuesto.Size = new System.Drawing.Size(158, 25);
            this.btnTipoRepuesto.TabIndex = 11;
            this.btnTipoRepuesto.Text = "Tipo de Repuesto";
            this.btnTipoRepuesto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTipoRepuesto.UseVisualStyleBackColor = true;
            this.btnTipoRepuesto.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelTipoRepuesto
            // 
            this.panelTipoRepuesto.BackColor = System.Drawing.Color.Silver;
            this.panelTipoRepuesto.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTipoRepuesto.Location = new System.Drawing.Point(158, 250);
            this.panelTipoRepuesto.Margin = new System.Windows.Forms.Padding(0);
            this.panelTipoRepuesto.Name = "panelTipoRepuesto";
            this.panelTipoRepuesto.Size = new System.Drawing.Size(158, 100);
            this.panelTipoRepuesto.TabIndex = 12;
            this.panelTipoRepuesto.Visible = false;
            // 
            // btnTipoUnidadMedida
            // 
            this.btnTipoUnidadMedida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTipoUnidadMedida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTipoUnidadMedida.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTipoUnidadMedida.Location = new System.Drawing.Point(158, 350);
            this.btnTipoUnidadMedida.Margin = new System.Windows.Forms.Padding(0);
            this.btnTipoUnidadMedida.Name = "btnTipoUnidadMedida";
            this.btnTipoUnidadMedida.Size = new System.Drawing.Size(158, 25);
            this.btnTipoUnidadMedida.TabIndex = 13;
            this.btnTipoUnidadMedida.Text = "Tipo de Unidad de Medida";
            this.btnTipoUnidadMedida.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTipoUnidadMedida.UseVisualStyleBackColor = true;
            this.btnTipoUnidadMedida.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelTipoUnidadMedida
            // 
            this.panelTipoUnidadMedida.BackColor = System.Drawing.Color.Silver;
            this.panelTipoUnidadMedida.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTipoUnidadMedida.Location = new System.Drawing.Point(158, 375);
            this.panelTipoUnidadMedida.Margin = new System.Windows.Forms.Padding(0);
            this.panelTipoUnidadMedida.Name = "panelTipoUnidadMedida";
            this.panelTipoUnidadMedida.Size = new System.Drawing.Size(158, 100);
            this.panelTipoUnidadMedida.TabIndex = 14;
            this.panelTipoUnidadMedida.Visible = false;
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
            this.scUp.Size = new System.Drawing.Size(625, 568);
            this.scUp.SplitterDistance = 20;
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
            this.btnMenu.Size = new System.Drawing.Size(15, 568);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.btnSalir);
            this.panelSalir.Location = new System.Drawing.Point(319, 3);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 100);
            this.panelSalir.TabIndex = 15;
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = true;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Image = global::GyCAP.UI.Soporte.Properties.Resources.Salir_25;
            this.btnSalir.Location = new System.Drawing.Point(42, 28);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 52);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmSoporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSoporte";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Soporte";
            this.Load += new System.EventHandler(this.frmSoporte_Load);
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCapacidadEmpleado;
        private System.Windows.Forms.Button btnLocalidad;
        private System.Windows.Forms.Panel panelCapacidadEmpleado;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelLocalidad;
        private System.Windows.Forms.Button btnMarca;
        private System.Windows.Forms.Panel panelMarca;
        private System.Windows.Forms.Button btnProcesoFabricacion;
        private System.Windows.Forms.Panel panelProcesoFabricacion;
        private System.Windows.Forms.Button btnProvincia;
        private System.Windows.Forms.Panel panelProvincia;
        private System.Windows.Forms.Button btnSectorTrabajo;
        private System.Windows.Forms.Panel panelSectorTrabajo;
        private System.Windows.Forms.Button btnTipoRepuesto;
        private System.Windows.Forms.Panel panelTipoRepuesto;
        private System.Windows.Forms.Button btnTipoUnidadMedida;
        private System.Windows.Forms.Panel panelTipoUnidadMedida;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Button btnSalir;


    }
}