namespace GyCAP.UI.GestionPedido
{
    partial class frmGestionPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGestionPedido));
            this.btnCliente = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.btnConsultarCliente = new System.Windows.Forms.Button();
            this.btnNuevoCliente = new System.Windows.Forms.Button();
            this.panelCliente = new System.Windows.Forms.Panel();
            this.btnPedido = new System.Windows.Forms.Button();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPedido = new System.Windows.Forms.Panel();
            this.btnConsultarPedido = new System.Windows.Forms.Button();
            this.btnNuevoPedido = new System.Windows.Forms.Button();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.panelCliente.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelPedido.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCliente
            // 
            this.btnCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCliente.Location = new System.Drawing.Point(0, 0);
            this.btnCliente.Margin = new System.Windows.Forms.Padding(0);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(158, 25);
            this.btnCliente.TabIndex = 0;
            this.btnCliente.Text = "Cliente";
            this.btnCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btn_Click);
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
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
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
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.btnSalir);
            this.panelSalir.Location = new System.Drawing.Point(0, 356);
            this.panelSalir.Margin = new System.Windows.Forms.Padding(0);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 87);
            this.panelSalir.TabIndex = 15;
            // 
            // btnConsultarCliente
            // 
            this.btnConsultarCliente.AutoSize = true;
            this.btnConsultarCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarCliente.FlatAppearance.BorderSize = 0;
            this.btnConsultarCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarCliente.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Find_48;
            this.btnConsultarCliente.Location = new System.Drawing.Point(48, 72);
            this.btnConsultarCliente.Name = "btnConsultarCliente";
            this.btnConsultarCliente.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarCliente.TabIndex = 1;
            this.btnConsultarCliente.Text = "Consultar";
            this.btnConsultarCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarCliente.UseVisualStyleBackColor = true;
            this.btnConsultarCliente.Click += new System.EventHandler(this.btnConsultarCliente_Click);
            this.btnConsultarCliente.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarCliente.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNuevoCliente
            // 
            this.btnNuevoCliente.AutoSize = true;
            this.btnNuevoCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoCliente.FlatAppearance.BorderSize = 0;
            this.btnNuevoCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoCliente.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.New_48;
            this.btnNuevoCliente.Location = new System.Drawing.Point(52, 1);
            this.btnNuevoCliente.Name = "btnNuevoCliente";
            this.btnNuevoCliente.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoCliente.TabIndex = 0;
            this.btnNuevoCliente.Text = " Nuevo";
            this.btnNuevoCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoCliente.UseVisualStyleBackColor = true;
            this.btnNuevoCliente.Click += new System.EventHandler(this.btnNuevoCliente_Click);
            this.btnNuevoCliente.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoCliente.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // panelCliente
            // 
            this.panelCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCliente.BackColor = System.Drawing.Color.Silver;
            this.panelCliente.Controls.Add(this.btnConsultarCliente);
            this.panelCliente.Controls.Add(this.btnNuevoCliente);
            this.panelCliente.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCliente.Location = new System.Drawing.Point(0, 25);
            this.panelCliente.Margin = new System.Windows.Forms.Padding(0);
            this.panelCliente.Name = "panelCliente";
            this.panelCliente.Size = new System.Drawing.Size(158, 154);
            this.panelCliente.TabIndex = 1;
            // 
            // btnPedido
            // 
            this.btnPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPedido.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPedido.Location = new System.Drawing.Point(0, 179);
            this.btnPedido.Margin = new System.Windows.Forms.Padding(0);
            this.btnPedido.Name = "btnPedido";
            this.btnPedido.Size = new System.Drawing.Size(158, 25);
            this.btnPedido.TabIndex = 0;
            this.btnPedido.Text = "Pedido";
            this.btnPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPedido.UseVisualStyleBackColor = true;
            this.btnPedido.Click += new System.EventHandler(this.btn_Click);
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
            this.scDown.TabIndex = 3;
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnCliente);
            this.flpMenu.Controls.Add(this.panelCliente);
            this.flpMenu.Controls.Add(this.btnPedido);
            this.flpMenu.Controls.Add(this.panelPedido);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 566);
            this.flpMenu.TabIndex = 0;
            // 
            // panelPedido
            // 
            this.panelPedido.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelPedido.BackColor = System.Drawing.Color.Silver;
            this.panelPedido.Controls.Add(this.btnConsultarPedido);
            this.panelPedido.Controls.Add(this.btnNuevoPedido);
            this.panelPedido.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPedido.Location = new System.Drawing.Point(0, 204);
            this.panelPedido.Margin = new System.Windows.Forms.Padding(0);
            this.panelPedido.Name = "panelPedido";
            this.panelPedido.Size = new System.Drawing.Size(158, 152);
            this.panelPedido.TabIndex = 2;
            // 
            // btnConsultarPedido
            // 
            this.btnConsultarPedido.AutoSize = true;
            this.btnConsultarPedido.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarPedido.FlatAppearance.BorderSize = 0;
            this.btnConsultarPedido.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarPedido.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarPedido.Image")));
            this.btnConsultarPedido.Location = new System.Drawing.Point(48, 70);
            this.btnConsultarPedido.Name = "btnConsultarPedido";
            this.btnConsultarPedido.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarPedido.TabIndex = 3;
            this.btnConsultarPedido.Text = "Consultar";
            this.btnConsultarPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarPedido.UseVisualStyleBackColor = true;
            this.btnConsultarPedido.Click += new System.EventHandler(this.btnConsultarPedido_Click);
            this.btnConsultarPedido.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarPedido.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNuevoPedido
            // 
            this.btnNuevoPedido.AutoSize = true;
            this.btnNuevoPedido.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoPedido.FlatAppearance.BorderSize = 0;
            this.btnNuevoPedido.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoPedido.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoPedido.Image")));
            this.btnNuevoPedido.Location = new System.Drawing.Point(52, 0);
            this.btnNuevoPedido.Name = "btnNuevoPedido";
            this.btnNuevoPedido.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoPedido.TabIndex = 2;
            this.btnNuevoPedido.Text = " Nuevo";
            this.btnNuevoPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoPedido.UseVisualStyleBackColor = true;
            this.btnNuevoPedido.Click += new System.EventHandler(this.btnNuevoPedido_Click);
            this.btnNuevoPedido.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoPedido.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // frmGestionPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmGestionPedido";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP  - Gestion de Pedido";
            this.Load += new System.EventHandler(this.frmGestionPedido_Load);
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.panelCliente.ResumeLayout(false);
            this.panelCliente.PerformLayout();
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panelPedido.ResumeLayout(false);
            this.panelPedido.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Button btnConsultarCliente;
        private System.Windows.Forms.Button btnNuevoCliente;
        private System.Windows.Forms.Panel panelCliente;
        private System.Windows.Forms.Button btnPedido;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelPedido;
        private System.Windows.Forms.Button btnConsultarPedido;
        private System.Windows.Forms.Button btnNuevoPedido;
    }
}