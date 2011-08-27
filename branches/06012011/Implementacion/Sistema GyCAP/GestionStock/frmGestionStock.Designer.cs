﻿namespace GyCAP.UI.GestionStock
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
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnActualizacionStock = new System.Windows.Forms.Button();
            this.panelActualizacionStock = new System.Windows.Forms.Panel();
            this.btnConsultarActualizacionStock = new System.Windows.Forms.Button();
            this.btnInventarioAbc = new System.Windows.Forms.Button();
            this.panelInventarioABC = new System.Windows.Forms.Panel();
            this.btnConsultarInventarioAbc = new System.Windows.Forms.Button();
            this.btnEntregaProducto = new System.Windows.Forms.Button();
            this.panelEntregaProducto = new System.Windows.Forms.Panel();
            this.btnConsultarEntregaProducto = new System.Windows.Forms.Button();
            this.btnNuevoEntregaProducto = new System.Windows.Forms.Button();
            this.btnEstructuraStock = new System.Windows.Forms.Button();
            this.panelEstructuraStock = new System.Windows.Forms.Panel();
            this.btnConsultarEstructuraStock = new System.Windows.Forms.Button();
            this.btnUbicacionStock = new System.Windows.Forms.Button();
            this.panelUbicacionStock = new System.Windows.Forms.Panel();
            this.btnConsultarUbicacionStock = new System.Windows.Forms.Button();
            this.btnNuevoUbicacionStock = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panelActualizacionStock.SuspendLayout();
            this.panelInventarioABC.SuspendLayout();
            this.panelEntregaProducto.SuspendLayout();
            this.panelEstructuraStock.SuspendLayout();
            this.panelUbicacionStock.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.SuspendLayout();
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
            this.flpMenu.Controls.Add(this.btnActualizacionStock);
            this.flpMenu.Controls.Add(this.panelActualizacionStock);
            this.flpMenu.Controls.Add(this.btnInventarioAbc);
            this.flpMenu.Controls.Add(this.panelInventarioABC);
            this.flpMenu.Controls.Add(this.btnEntregaProducto);
            this.flpMenu.Controls.Add(this.panelEntregaProducto);
            this.flpMenu.Controls.Add(this.btnEstructuraStock);
            this.flpMenu.Controls.Add(this.panelEstructuraStock);
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
            // btnActualizacionStock
            // 
            this.btnActualizacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizacionStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizacionStock.Location = new System.Drawing.Point(0, 0);
            this.btnActualizacionStock.Margin = new System.Windows.Forms.Padding(0);
            this.btnActualizacionStock.Name = "btnActualizacionStock";
            this.btnActualizacionStock.Size = new System.Drawing.Size(158, 25);
            this.btnActualizacionStock.TabIndex = 19;
            this.btnActualizacionStock.Text = "Actualización de Stock";
            this.btnActualizacionStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizacionStock.UseVisualStyleBackColor = true;
            this.btnActualizacionStock.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelActualizacionStock
            // 
            this.panelActualizacionStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelActualizacionStock.BackColor = System.Drawing.Color.Silver;
            this.panelActualizacionStock.Controls.Add(this.btnConsultarActualizacionStock);
            this.panelActualizacionStock.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelActualizacionStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActualizacionStock.Location = new System.Drawing.Point(0, 25);
            this.panelActualizacionStock.Margin = new System.Windows.Forms.Padding(0);
            this.panelActualizacionStock.Name = "panelActualizacionStock";
            this.panelActualizacionStock.Size = new System.Drawing.Size(158, 154);
            this.panelActualizacionStock.TabIndex = 20;
            // 
            // btnConsultarActualizacionStock
            // 
            this.btnConsultarActualizacionStock.AutoSize = true;
            this.btnConsultarActualizacionStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarActualizacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarActualizacionStock.FlatAppearance.BorderSize = 0;
            this.btnConsultarActualizacionStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarActualizacionStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarActualizacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarActualizacionStock.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_48;
            this.btnConsultarActualizacionStock.Location = new System.Drawing.Point(48, 3);
            this.btnConsultarActualizacionStock.Name = "btnConsultarActualizacionStock";
            this.btnConsultarActualizacionStock.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarActualizacionStock.TabIndex = 1;
            this.btnConsultarActualizacionStock.Text = "Consultar";
            this.btnConsultarActualizacionStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarActualizacionStock.UseVisualStyleBackColor = true;
            this.btnConsultarActualizacionStock.Click += new System.EventHandler(this.btnConsultarActualizacionStock_Click);
            this.btnConsultarActualizacionStock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarActualizacionStock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnInventarioAbc
            // 
            this.btnInventarioAbc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventarioAbc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventarioAbc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventarioAbc.Location = new System.Drawing.Point(0, 179);
            this.btnInventarioAbc.Margin = new System.Windows.Forms.Padding(0);
            this.btnInventarioAbc.Name = "btnInventarioAbc";
            this.btnInventarioAbc.Size = new System.Drawing.Size(158, 25);
            this.btnInventarioAbc.TabIndex = 22;
            this.btnInventarioAbc.Text = "Inventario ABC";
            this.btnInventarioAbc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventarioAbc.UseVisualStyleBackColor = true;
            this.btnInventarioAbc.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelInventarioABC
            // 
            this.panelInventarioABC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelInventarioABC.BackColor = System.Drawing.Color.Silver;
            this.panelInventarioABC.Controls.Add(this.btnConsultarInventarioAbc);
            this.panelInventarioABC.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelInventarioABC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInventarioABC.Location = new System.Drawing.Point(0, 204);
            this.panelInventarioABC.Margin = new System.Windows.Forms.Padding(0);
            this.panelInventarioABC.Name = "panelInventarioABC";
            this.panelInventarioABC.Size = new System.Drawing.Size(158, 154);
            this.panelInventarioABC.TabIndex = 23;
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
            this.btnConsultarInventarioAbc.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_48;
            this.btnConsultarInventarioAbc.Location = new System.Drawing.Point(48, 3);
            this.btnConsultarInventarioAbc.Name = "btnConsultarInventarioAbc";
            this.btnConsultarInventarioAbc.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarInventarioAbc.TabIndex = 1;
            this.btnConsultarInventarioAbc.Text = "Consultar";
            this.btnConsultarInventarioAbc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarInventarioAbc.UseVisualStyleBackColor = true;
            this.btnConsultarInventarioAbc.Click += new System.EventHandler(this.btnConsultarInventarioAbc_Click);
            this.btnConsultarInventarioAbc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarInventarioAbc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnEntregaProducto
            // 
            this.btnEntregaProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntregaProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntregaProducto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntregaProducto.Location = new System.Drawing.Point(0, 358);
            this.btnEntregaProducto.Margin = new System.Windows.Forms.Padding(0);
            this.btnEntregaProducto.Name = "btnEntregaProducto";
            this.btnEntregaProducto.Size = new System.Drawing.Size(158, 25);
            this.btnEntregaProducto.TabIndex = 25;
            this.btnEntregaProducto.Text = "Entrega Producto";
            this.btnEntregaProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntregaProducto.UseVisualStyleBackColor = true;
            this.btnEntregaProducto.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelEntregaProducto
            // 
            this.panelEntregaProducto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEntregaProducto.BackColor = System.Drawing.Color.Silver;
            this.panelEntregaProducto.Controls.Add(this.btnConsultarEntregaProducto);
            this.panelEntregaProducto.Controls.Add(this.btnNuevoEntregaProducto);
            this.panelEntregaProducto.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelEntregaProducto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEntregaProducto.Location = new System.Drawing.Point(0, 383);
            this.panelEntregaProducto.Margin = new System.Windows.Forms.Padding(0);
            this.panelEntregaProducto.Name = "panelEntregaProducto";
            this.panelEntregaProducto.Size = new System.Drawing.Size(158, 152);
            this.panelEntregaProducto.TabIndex = 26;
            // 
            // btnConsultarEntregaProducto
            // 
            this.btnConsultarEntregaProducto.AutoSize = true;
            this.btnConsultarEntregaProducto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarEntregaProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarEntregaProducto.FlatAppearance.BorderSize = 0;
            this.btnConsultarEntregaProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarEntregaProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarEntregaProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarEntregaProducto.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_48;
            this.btnConsultarEntregaProducto.Location = new System.Drawing.Point(48, 70);
            this.btnConsultarEntregaProducto.Name = "btnConsultarEntregaProducto";
            this.btnConsultarEntregaProducto.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarEntregaProducto.TabIndex = 3;
            this.btnConsultarEntregaProducto.Text = "Consultar";
            this.btnConsultarEntregaProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarEntregaProducto.UseVisualStyleBackColor = true;
            this.btnConsultarEntregaProducto.Click += new System.EventHandler(this.btnConsultarEntregaProducto_Click);
            this.btnConsultarEntregaProducto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarEntregaProducto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNuevoEntregaProducto
            // 
            this.btnNuevoEntregaProducto.AutoSize = true;
            this.btnNuevoEntregaProducto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoEntregaProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoEntregaProducto.FlatAppearance.BorderSize = 0;
            this.btnNuevoEntregaProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNuevoEntregaProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoEntregaProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoEntregaProducto.Image = global::GyCAP.UI.GestionStock.Properties.Resources.New_48;
            this.btnNuevoEntregaProducto.Location = new System.Drawing.Point(52, 0);
            this.btnNuevoEntregaProducto.Name = "btnNuevoEntregaProducto";
            this.btnNuevoEntregaProducto.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoEntregaProducto.TabIndex = 2;
            this.btnNuevoEntregaProducto.Text = " Nuevo";
            this.btnNuevoEntregaProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoEntregaProducto.UseVisualStyleBackColor = true;
            this.btnNuevoEntregaProducto.Click += new System.EventHandler(this.btnNuevoEntregaProducto_Click);
            this.btnNuevoEntregaProducto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNuevoEntregaProducto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnEstructuraStock
            // 
            this.btnEstructuraStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstructuraStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstructuraStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstructuraStock.Location = new System.Drawing.Point(0, 535);
            this.btnEstructuraStock.Margin = new System.Windows.Forms.Padding(0);
            this.btnEstructuraStock.Name = "btnEstructuraStock";
            this.btnEstructuraStock.Size = new System.Drawing.Size(158, 25);
            this.btnEstructuraStock.TabIndex = 31;
            this.btnEstructuraStock.Text = "Estructura Stock";
            this.btnEstructuraStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstructuraStock.UseVisualStyleBackColor = true;
            this.btnEstructuraStock.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelEstructuraStock
            // 
            this.panelEstructuraStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEstructuraStock.BackColor = System.Drawing.Color.Silver;
            this.panelEstructuraStock.Controls.Add(this.btnConsultarEstructuraStock);
            this.panelEstructuraStock.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelEstructuraStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstructuraStock.Location = new System.Drawing.Point(158, 0);
            this.panelEstructuraStock.Margin = new System.Windows.Forms.Padding(0);
            this.panelEstructuraStock.Name = "panelEstructuraStock";
            this.panelEstructuraStock.Size = new System.Drawing.Size(158, 152);
            this.panelEstructuraStock.TabIndex = 32;
            // 
            // btnConsultarEstructuraStock
            // 
            this.btnConsultarEstructuraStock.AutoSize = true;
            this.btnConsultarEstructuraStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarEstructuraStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarEstructuraStock.FlatAppearance.BorderSize = 0;
            this.btnConsultarEstructuraStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnConsultarEstructuraStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarEstructuraStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarEstructuraStock.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_48;
            this.btnConsultarEstructuraStock.Location = new System.Drawing.Point(48, 6);
            this.btnConsultarEstructuraStock.Name = "btnConsultarEstructuraStock";
            this.btnConsultarEstructuraStock.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarEstructuraStock.TabIndex = 3;
            this.btnConsultarEstructuraStock.Text = "Consultar";
            this.btnConsultarEstructuraStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarEstructuraStock.UseVisualStyleBackColor = true;
            this.btnConsultarEstructuraStock.Click += new System.EventHandler(this.btnConsultarEstructuraStock_Click);
            this.btnConsultarEstructuraStock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnConsultarEstructuraStock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnUbicacionStock
            // 
            this.btnUbicacionStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacionStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbicacionStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUbicacionStock.Location = new System.Drawing.Point(158, 152);
            this.btnUbicacionStock.Margin = new System.Windows.Forms.Padding(0);
            this.btnUbicacionStock.Name = "btnUbicacionStock";
            this.btnUbicacionStock.Size = new System.Drawing.Size(158, 25);
            this.btnUbicacionStock.TabIndex = 33;
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
            this.panelUbicacionStock.Location = new System.Drawing.Point(158, 177);
            this.panelUbicacionStock.Margin = new System.Windows.Forms.Padding(0);
            this.panelUbicacionStock.Name = "panelUbicacionStock";
            this.panelUbicacionStock.Size = new System.Drawing.Size(158, 152);
            this.panelUbicacionStock.TabIndex = 34;
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
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.btnSalir);
            this.panelSalir.Location = new System.Drawing.Point(158, 329);
            this.panelSalir.Margin = new System.Windows.Forms.Padding(0);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 87);
            this.panelSalir.TabIndex = 35;
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
            this.panelActualizacionStock.ResumeLayout(false);
            this.panelActualizacionStock.PerformLayout();
            this.panelInventarioABC.ResumeLayout(false);
            this.panelInventarioABC.PerformLayout();
            this.panelEntregaProducto.ResumeLayout(false);
            this.panelEntregaProducto.PerformLayout();
            this.panelEstructuraStock.ResumeLayout(false);
            this.panelEstructuraStock.PerformLayout();
            this.panelUbicacionStock.ResumeLayout(false);
            this.panelUbicacionStock.PerformLayout();
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnActualizacionStock;
        private System.Windows.Forms.Panel panelActualizacionStock;
        private System.Windows.Forms.Button btnConsultarActualizacionStock;
        private System.Windows.Forms.Button btnInventarioAbc;
        private System.Windows.Forms.Panel panelInventarioABC;
        private System.Windows.Forms.Button btnConsultarInventarioAbc;
        private System.Windows.Forms.Button btnEntregaProducto;
        private System.Windows.Forms.Panel panelEntregaProducto;
        private System.Windows.Forms.Button btnConsultarEntregaProducto;
        private System.Windows.Forms.Button btnNuevoEntregaProducto;
        private System.Windows.Forms.Button btnEstructuraStock;
        private System.Windows.Forms.Panel panelEstructuraStock;
        private System.Windows.Forms.Button btnConsultarEstructuraStock;
        private System.Windows.Forms.Button btnUbicacionStock;
        private System.Windows.Forms.Panel panelUbicacionStock;
        private System.Windows.Forms.Button btnConsultarUbicacionStock;
        private System.Windows.Forms.Button btnNuevoUbicacionStock;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Button btnSalir;
    }
}