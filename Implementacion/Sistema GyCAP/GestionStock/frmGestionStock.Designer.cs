namespace GyCAP.UI.GestionStock
{
    partial class frmGestionStock
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
            this.btnTemp = new System.Windows.Forms.Button();
            this.btnConsultarTemp = new System.Windows.Forms.Button();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTemp = new System.Windows.Forms.Panel();
            this.btnNuevoTemp = new System.Windows.Forms.Button();
            this.btnUbicacionStock = new System.Windows.Forms.Button();
            this.panelUbicacionStock = new System.Windows.Forms.Panel();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnConsultarUbicacionStock = new System.Windows.Forms.Button();
            this.btnNuevoUbicacionStock = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelTemp.SuspendLayout();
            this.panelUbicacionStock.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTemp
            // 
            this.btnTemp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemp.Location = new System.Drawing.Point(0, 0);
            this.btnTemp.Margin = new System.Windows.Forms.Padding(0);
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.Size = new System.Drawing.Size(158, 25);
            this.btnTemp.TabIndex = 0;
            this.btnTemp.Text = "temp";
            this.btnTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTemp.UseVisualStyleBackColor = true;
            this.btnTemp.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnConsultarTemp
            // 
            this.btnConsultarTemp.AutoSize = true;
            this.btnConsultarTemp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarTemp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarTemp.FlatAppearance.BorderSize = 0;
            this.btnConsultarTemp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarTemp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarTemp.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_48;
            this.btnConsultarTemp.Location = new System.Drawing.Point(48, 72);
            this.btnConsultarTemp.Name = "btnConsultarTemp";
            this.btnConsultarTemp.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarTemp.TabIndex = 1;
            this.btnConsultarTemp.Text = "Consultar";
            this.btnConsultarTemp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarTemp.UseVisualStyleBackColor = true;
            this.btnConsultarTemp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarTemp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            this.scDown.TabIndex = 4;
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnTemp);
            this.flpMenu.Controls.Add(this.panelTemp);
            this.flpMenu.Controls.Add(this.btnUbicacionStock);
            this.flpMenu.Controls.Add(this.panelUbicacionStock);
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
            this.panelTemp.Controls.Add(this.btnConsultarTemp);
            this.panelTemp.Controls.Add(this.btnNuevoTemp);
            this.panelTemp.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTemp.Location = new System.Drawing.Point(0, 25);
            this.panelTemp.Margin = new System.Windows.Forms.Padding(0);
            this.panelTemp.Name = "panelTemp";
            this.panelTemp.Size = new System.Drawing.Size(158, 154);
            this.panelTemp.TabIndex = 1;
            // 
            // btnNuevoTemp
            // 
            this.btnNuevoTemp.AutoSize = true;
            this.btnNuevoTemp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoTemp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoTemp.FlatAppearance.BorderSize = 0;
            this.btnNuevoTemp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoTemp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoTemp.Image = global::GyCAP.UI.GestionStock.Properties.Resources.New_48;
            this.btnNuevoTemp.Location = new System.Drawing.Point(52, 1);
            this.btnNuevoTemp.Name = "btnNuevoTemp";
            this.btnNuevoTemp.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoTemp.TabIndex = 0;
            this.btnNuevoTemp.Text = " Nuevo";
            this.btnNuevoTemp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoTemp.UseVisualStyleBackColor = true;
            this.btnNuevoTemp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoTemp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnUbicacionStock
            // 
            this.btnUbicacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbicacionStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUbicacionStock.Location = new System.Drawing.Point(0, 179);
            this.btnUbicacionStock.Margin = new System.Windows.Forms.Padding(0);
            this.btnUbicacionStock.Name = "btnUbicacionStock";
            this.btnUbicacionStock.Size = new System.Drawing.Size(158, 25);
            this.btnUbicacionStock.TabIndex = 0;
            this.btnUbicacionStock.Text = "Ubicación stock";
            this.btnUbicacionStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUbicacionStock.UseVisualStyleBackColor = true;
            this.btnUbicacionStock.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelUbicacionStock
            // 
            this.panelUbicacionStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelUbicacionStock.BackColor = System.Drawing.Color.Silver;
            this.panelUbicacionStock.Controls.Add(this.btnConsultarUbicacionStock);
            this.panelUbicacionStock.Controls.Add(this.btnNuevoUbicacionStock);
            this.panelUbicacionStock.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelUbicacionStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUbicacionStock.Location = new System.Drawing.Point(0, 204);
            this.panelUbicacionStock.Margin = new System.Windows.Forms.Padding(0);
            this.panelUbicacionStock.Name = "panelUbicacionStock";
            this.panelUbicacionStock.Size = new System.Drawing.Size(158, 152);
            this.panelUbicacionStock.TabIndex = 2;
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
            // btnConsultarUbicacionStock
            // 
            this.btnConsultarUbicacionStock.AutoSize = true;
            this.btnConsultarUbicacionStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarUbicacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarUbicacionStock.FlatAppearance.BorderSize = 0;
            this.btnConsultarUbicacionStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarUbicacionStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarUbicacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarUbicacionStock.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_48;
            this.btnConsultarUbicacionStock.Location = new System.Drawing.Point(48, 70);
            this.btnConsultarUbicacionStock.Name = "btnConsultarUbicacionStock";
            this.btnConsultarUbicacionStock.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarUbicacionStock.TabIndex = 3;
            this.btnConsultarUbicacionStock.Text = "Consultar";
            this.btnConsultarUbicacionStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarUbicacionStock.UseVisualStyleBackColor = true;
            this.btnConsultarUbicacionStock.Click += new System.EventHandler(this.btnConsultarUbicacionStock_Click);
            this.btnConsultarUbicacionStock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarUbicacionStock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            this.btnNuevoUbicacionStock.Image = global::GyCAP.UI.GestionStock.Properties.Resources.New_48;
            this.btnNuevoUbicacionStock.Location = new System.Drawing.Point(52, 0);
            this.btnNuevoUbicacionStock.Name = "btnNuevoUbicacionStock";
            this.btnNuevoUbicacionStock.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoUbicacionStock.TabIndex = 2;
            this.btnNuevoUbicacionStock.Text = " Nuevo";
            this.btnNuevoUbicacionStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoUbicacionStock.UseVisualStyleBackColor = true;
            this.btnNuevoUbicacionStock.Click += new System.EventHandler(this.btnNuevoUbicacionStock_Click);
            this.btnNuevoUbicacionStock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoUbicacionStock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            this.btnSalir.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Exit_48;
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
            // frmGestionStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmGestionStock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Gestion de Stock";
            this.Load += new System.EventHandler(this.frmGestionStock_Load);
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panelTemp.ResumeLayout(false);
            this.panelTemp.PerformLayout();
            this.panelUbicacionStock.ResumeLayout(false);
            this.panelUbicacionStock.PerformLayout();
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTemp;
        private System.Windows.Forms.Button btnConsultarTemp;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelTemp;
        private System.Windows.Forms.Button btnNuevoTemp;
        private System.Windows.Forms.Button btnUbicacionStock;
        private System.Windows.Forms.Panel panelUbicacionStock;
        private System.Windows.Forms.Button btnConsultarUbicacionStock;
        private System.Windows.Forms.Button btnNuevoUbicacionStock;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
    }
}