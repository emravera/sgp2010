namespace GyCAP.UI.Calidad
{
    partial class frmModuloCalidad
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
            this.btnConsultarUbicacionStock = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnNuevoUbicacionStock = new System.Windows.Forms.Button();
            this.btnLineaMontaje = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnConsultarInventarioAbc = new System.Windows.Forms.Button();
            this.btnTemp = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.panelLineaMontaje = new System.Windows.Forms.Panel();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTemp = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.panelLineaMontaje.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelTemp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConsultarUbicacionStock
            // 
            this.btnConsultarUbicacionStock.AutoSize = true;
            this.btnConsultarUbicacionStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarUbicacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarUbicacionStock.FlatAppearance.BorderSize = 0;
            this.btnConsultarUbicacionStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarUbicacionStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarUbicacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarUbicacionStock.Image = global::GyCAP.UI.Calidad.Properties.Resources.Find_48;
            this.btnConsultarUbicacionStock.Location = new System.Drawing.Point(48, 70);
            this.btnConsultarUbicacionStock.Name = "btnConsultarUbicacionStock";
            this.btnConsultarUbicacionStock.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarUbicacionStock.TabIndex = 3;
            this.btnConsultarUbicacionStock.Text = "Consultar";
            this.btnConsultarUbicacionStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarUbicacionStock.UseVisualStyleBackColor = true;
            this.btnConsultarUbicacionStock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarUbicacionStock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            // btnNuevoUbicacionStock
            // 
            this.btnNuevoUbicacionStock.AutoSize = true;
            this.btnNuevoUbicacionStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoUbicacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoUbicacionStock.FlatAppearance.BorderSize = 0;
            this.btnNuevoUbicacionStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoUbicacionStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoUbicacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoUbicacionStock.Image = global::GyCAP.UI.Calidad.Properties.Resources.New_48;
            this.btnNuevoUbicacionStock.Location = new System.Drawing.Point(52, 1);
            this.btnNuevoUbicacionStock.Name = "btnNuevoUbicacionStock";
            this.btnNuevoUbicacionStock.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoUbicacionStock.TabIndex = 2;
            this.btnNuevoUbicacionStock.Text = " Nuevo";
            this.btnNuevoUbicacionStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoUbicacionStock.UseVisualStyleBackColor = true;
            this.btnNuevoUbicacionStock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoUbicacionStock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnLineaMontaje
            // 
            this.btnLineaMontaje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLineaMontaje.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLineaMontaje.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLineaMontaje.Location = new System.Drawing.Point(0, 0);
            this.btnLineaMontaje.Margin = new System.Windows.Forms.Padding(0);
            this.btnLineaMontaje.Name = "btnLineaMontaje";
            this.btnLineaMontaje.Size = new System.Drawing.Size(158, 25);
            this.btnLineaMontaje.TabIndex = 0;
            this.btnLineaMontaje.Text = "Linea de montaje";
            this.btnLineaMontaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLineaMontaje.UseVisualStyleBackColor = true;
            this.btnLineaMontaje.Click += new System.EventHandler(this.btn_Click);
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
            this.btnSalir.Image = global::GyCAP.UI.Calidad.Properties.Resources.Exit_48;
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
            // btnConsultarInventarioAbc
            // 
            this.btnConsultarInventarioAbc.AutoSize = true;
            this.btnConsultarInventarioAbc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarInventarioAbc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarInventarioAbc.FlatAppearance.BorderSize = 0;
            this.btnConsultarInventarioAbc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarInventarioAbc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarInventarioAbc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarInventarioAbc.Image = global::GyCAP.UI.Calidad.Properties.Resources.Find_48;
            this.btnConsultarInventarioAbc.Location = new System.Drawing.Point(48, 75);
            this.btnConsultarInventarioAbc.Name = "btnConsultarInventarioAbc";
            this.btnConsultarInventarioAbc.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarInventarioAbc.TabIndex = 1;
            this.btnConsultarInventarioAbc.Text = "Consultar";
            this.btnConsultarInventarioAbc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarInventarioAbc.UseVisualStyleBackColor = true;
            this.btnConsultarInventarioAbc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarInventarioAbc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnTemp
            // 
            this.btnTemp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemp.Location = new System.Drawing.Point(0, 179);
            this.btnTemp.Margin = new System.Windows.Forms.Padding(0);
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.Size = new System.Drawing.Size(158, 25);
            this.btnTemp.TabIndex = 0;
            this.btnTemp.Text = "Temp";
            this.btnTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTemp.UseVisualStyleBackColor = true;
            this.btnTemp.Click += new System.EventHandler(this.btn_Click);
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
            // panelLineaMontaje
            // 
            this.panelLineaMontaje.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelLineaMontaje.BackColor = System.Drawing.Color.Silver;
            this.panelLineaMontaje.Controls.Add(this.button1);
            this.panelLineaMontaje.Controls.Add(this.btnConsultarInventarioAbc);
            this.panelLineaMontaje.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelLineaMontaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLineaMontaje.Location = new System.Drawing.Point(0, 25);
            this.panelLineaMontaje.Margin = new System.Windows.Forms.Padding(0);
            this.panelLineaMontaje.Name = "panelLineaMontaje";
            this.panelLineaMontaje.Size = new System.Drawing.Size(158, 154);
            this.panelLineaMontaje.TabIndex = 1;
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
            this.flpMenu.Controls.Add(this.btnLineaMontaje);
            this.flpMenu.Controls.Add(this.panelLineaMontaje);
            this.flpMenu.Controls.Add(this.btnTemp);
            this.flpMenu.Controls.Add(this.panelTemp);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 566);
            this.flpMenu.TabIndex = 0;
            // 
            // panelTemp
            // 
            this.panelTemp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTemp.BackColor = System.Drawing.Color.Silver;
            this.panelTemp.Controls.Add(this.btnConsultarUbicacionStock);
            this.panelTemp.Controls.Add(this.btnNuevoUbicacionStock);
            this.panelTemp.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTemp.Location = new System.Drawing.Point(0, 204);
            this.panelTemp.Margin = new System.Windows.Forms.Padding(0);
            this.panelTemp.Name = "panelTemp";
            this.panelTemp.Size = new System.Drawing.Size(158, 152);
            this.panelTemp.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::GyCAP.UI.Calidad.Properties.Resources.New_48;
            this.button1.Location = new System.Drawing.Point(52, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 71);
            this.button1.TabIndex = 3;
            this.button1.Text = " Nuevo";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // frmModuloCalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmModuloCalidad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Calidad";
            this.Load += new System.EventHandler(this.frmfrmModuloCalidad_Load);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.panelLineaMontaje.ResumeLayout(false);
            this.panelLineaMontaje.PerformLayout();
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panelTemp.ResumeLayout(false);
            this.panelTemp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsultarUbicacionStock;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnNuevoUbicacionStock;
        private System.Windows.Forms.Button btnLineaMontaje;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnConsultarInventarioAbc;
        private System.Windows.Forms.Button btnTemp;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Panel panelLineaMontaje;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelTemp;
        private System.Windows.Forms.Button button1;
    }
}