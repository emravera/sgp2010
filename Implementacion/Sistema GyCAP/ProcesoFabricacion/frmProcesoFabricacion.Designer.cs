﻿namespace GyCAP.UI.ProcesoFabricacion
{
    partial class frmProcesoFabricacion
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
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHojaRuta = new System.Windows.Forms.Button();
            this.panelHojaRuta = new System.Windows.Forms.Panel();
            this.btnConsultarhojaRuta = new System.Windows.Forms.Button();
            this.btnNuevoHojaRuta = new System.Windows.Forms.Button();
            this.btnOperacionFabricacion = new System.Windows.Forms.Button();
            this.panelOperacionFabricacion = new System.Windows.Forms.Panel();
            this.btnConsultarOperacionFabricacion = new System.Windows.Forms.Button();
            this.btnNuevoOperacionFabricacion = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelHojaRuta.SuspendLayout();
            this.panelOperacionFabricacion.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.SuspendLayout();
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
            this.flpMenu.Controls.Add(this.btnHojaRuta);
            this.flpMenu.Controls.Add(this.panelHojaRuta);
            this.flpMenu.Controls.Add(this.btnOperacionFabricacion);
            this.flpMenu.Controls.Add(this.panelOperacionFabricacion);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 566);
            this.flpMenu.TabIndex = 0;
            // 
            // btnHojaRuta
            // 
            this.btnHojaRuta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHojaRuta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHojaRuta.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHojaRuta.Location = new System.Drawing.Point(0, 0);
            this.btnHojaRuta.Margin = new System.Windows.Forms.Padding(0);
            this.btnHojaRuta.Name = "btnHojaRuta";
            this.btnHojaRuta.Size = new System.Drawing.Size(158, 25);
            this.btnHojaRuta.TabIndex = 24;
            this.btnHojaRuta.Text = "Hoja de Ruta";
            this.btnHojaRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHojaRuta.UseVisualStyleBackColor = true;
            this.btnHojaRuta.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelHojaRuta
            // 
            this.panelHojaRuta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHojaRuta.BackColor = System.Drawing.Color.Silver;
            this.panelHojaRuta.Controls.Add(this.btnConsultarhojaRuta);
            this.panelHojaRuta.Controls.Add(this.btnNuevoHojaRuta);
            this.panelHojaRuta.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelHojaRuta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHojaRuta.Location = new System.Drawing.Point(0, 25);
            this.panelHojaRuta.Margin = new System.Windows.Forms.Padding(0);
            this.panelHojaRuta.Name = "panelHojaRuta";
            this.panelHojaRuta.Size = new System.Drawing.Size(158, 144);
            this.panelHojaRuta.TabIndex = 25;
            // 
            // btnConsultarhojaRuta
            // 
            this.btnConsultarhojaRuta.AutoSize = true;
            this.btnConsultarhojaRuta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarhojaRuta.FlatAppearance.BorderSize = 0;
            this.btnConsultarhojaRuta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarhojaRuta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarhojaRuta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarhojaRuta.Image = global::GyCAP.UI.ProcesoFabricacion.Properties.Resources.Find_48;
            this.btnConsultarhojaRuta.Location = new System.Drawing.Point(47, 70);
            this.btnConsultarhojaRuta.Name = "btnConsultarhojaRuta";
            this.btnConsultarhojaRuta.Size = new System.Drawing.Size(64, 71);
            this.btnConsultarhojaRuta.TabIndex = 1;
            this.btnConsultarhojaRuta.Text = "Consultar";
            this.btnConsultarhojaRuta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarhojaRuta.UseVisualStyleBackColor = true;
            this.btnConsultarhojaRuta.Click += new System.EventHandler(this.btnConsultarhojaRuta_Click);
            this.btnConsultarhojaRuta.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarhojaRuta.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNuevoHojaRuta
            // 
            this.btnNuevoHojaRuta.AutoSize = true;
            this.btnNuevoHojaRuta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoHojaRuta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoHojaRuta.FlatAppearance.BorderSize = 0;
            this.btnNuevoHojaRuta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoHojaRuta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoHojaRuta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoHojaRuta.Image = global::GyCAP.UI.ProcesoFabricacion.Properties.Resources.New_48;
            this.btnNuevoHojaRuta.Location = new System.Drawing.Point(52, 0);
            this.btnNuevoHojaRuta.Name = "btnNuevoHojaRuta";
            this.btnNuevoHojaRuta.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoHojaRuta.TabIndex = 0;
            this.btnNuevoHojaRuta.Text = "Nuevo";
            this.btnNuevoHojaRuta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoHojaRuta.UseVisualStyleBackColor = true;
            this.btnNuevoHojaRuta.Click += new System.EventHandler(this.btnNuevoHojaRuta_Click);
            this.btnNuevoHojaRuta.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoHojaRuta.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnOperacionFabricacion
            // 
            this.btnOperacionFabricacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOperacionFabricacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOperacionFabricacion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOperacionFabricacion.Location = new System.Drawing.Point(0, 169);
            this.btnOperacionFabricacion.Margin = new System.Windows.Forms.Padding(0);
            this.btnOperacionFabricacion.Name = "btnOperacionFabricacion";
            this.btnOperacionFabricacion.Size = new System.Drawing.Size(158, 25);
            this.btnOperacionFabricacion.TabIndex = 41;
            this.btnOperacionFabricacion.Text = "Operación de Fabricación";
            this.btnOperacionFabricacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOperacionFabricacion.UseVisualStyleBackColor = true;
            this.btnOperacionFabricacion.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelOperacionFabricacion
            // 
            this.panelOperacionFabricacion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelOperacionFabricacion.BackColor = System.Drawing.Color.Silver;
            this.panelOperacionFabricacion.Controls.Add(this.btnConsultarOperacionFabricacion);
            this.panelOperacionFabricacion.Controls.Add(this.btnNuevoOperacionFabricacion);
            this.panelOperacionFabricacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelOperacionFabricacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOperacionFabricacion.Location = new System.Drawing.Point(0, 194);
            this.panelOperacionFabricacion.Margin = new System.Windows.Forms.Padding(0);
            this.panelOperacionFabricacion.Name = "panelOperacionFabricacion";
            this.panelOperacionFabricacion.Size = new System.Drawing.Size(158, 144);
            this.panelOperacionFabricacion.TabIndex = 42;
            // 
            // btnConsultarOperacionFabricacion
            // 
            this.btnConsultarOperacionFabricacion.AutoSize = true;
            this.btnConsultarOperacionFabricacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarOperacionFabricacion.FlatAppearance.BorderSize = 0;
            this.btnConsultarOperacionFabricacion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarOperacionFabricacion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarOperacionFabricacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarOperacionFabricacion.Image = global::GyCAP.UI.ProcesoFabricacion.Properties.Resources.Find_48;
            this.btnConsultarOperacionFabricacion.Location = new System.Drawing.Point(47, 70);
            this.btnConsultarOperacionFabricacion.Name = "btnConsultarOperacionFabricacion";
            this.btnConsultarOperacionFabricacion.Size = new System.Drawing.Size(64, 71);
            this.btnConsultarOperacionFabricacion.TabIndex = 1;
            this.btnConsultarOperacionFabricacion.Text = "Consultar";
            this.btnConsultarOperacionFabricacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarOperacionFabricacion.UseVisualStyleBackColor = true;
            this.btnConsultarOperacionFabricacion.Click += new System.EventHandler(this.btnConsultarOperacionFabricacion_Click);
            this.btnConsultarOperacionFabricacion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarOperacionFabricacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNuevoOperacionFabricacion
            // 
            this.btnNuevoOperacionFabricacion.AutoSize = true;
            this.btnNuevoOperacionFabricacion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoOperacionFabricacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoOperacionFabricacion.FlatAppearance.BorderSize = 0;
            this.btnNuevoOperacionFabricacion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoOperacionFabricacion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoOperacionFabricacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoOperacionFabricacion.Image = global::GyCAP.UI.ProcesoFabricacion.Properties.Resources.New_48;
            this.btnNuevoOperacionFabricacion.Location = new System.Drawing.Point(52, 0);
            this.btnNuevoOperacionFabricacion.Name = "btnNuevoOperacionFabricacion";
            this.btnNuevoOperacionFabricacion.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoOperacionFabricacion.TabIndex = 0;
            this.btnNuevoOperacionFabricacion.Text = "Nuevo";
            this.btnNuevoOperacionFabricacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoOperacionFabricacion.UseVisualStyleBackColor = true;
            this.btnNuevoOperacionFabricacion.Click += new System.EventHandler(this.btnNuevoOperacionFabricacion_Click);
            this.btnNuevoOperacionFabricacion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoOperacionFabricacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.btnSalir);
            this.panelSalir.Location = new System.Drawing.Point(0, 338);
            this.panelSalir.Margin = new System.Windows.Forms.Padding(0);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 87);
            this.panelSalir.TabIndex = 43;
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
            this.btnSalir.Image = global::GyCAP.UI.ProcesoFabricacion.Properties.Resources.Exit_48;
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
            // frmProcesoFabricacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmProcesoFabricacion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Proceso de Fabricación";
            this.Load += new System.EventHandler(this.frmProcesoFabricacion_Load);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panelHojaRuta.ResumeLayout(false);
            this.panelHojaRuta.PerformLayout();
            this.panelOperacionFabricacion.ResumeLayout(false);
            this.panelOperacionFabricacion.PerformLayout();
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Button btnHojaRuta;
        private System.Windows.Forms.Panel panelHojaRuta;
        private System.Windows.Forms.Button btnConsultarhojaRuta;
        private System.Windows.Forms.Button btnNuevoHojaRuta;
        private System.Windows.Forms.Button btnOperacionFabricacion;
        private System.Windows.Forms.Panel panelOperacionFabricacion;
        private System.Windows.Forms.Button btnConsultarOperacionFabricacion;
        private System.Windows.Forms.Button btnNuevoOperacionFabricacion;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Button btnSalir;
    }
}