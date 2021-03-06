﻿namespace GyCAP.UI.GestionPedido
{
    partial class frmPedidos
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvDetallePedido = new System.Windows.Forms.DataGridView();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnSumar = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.ofdImagen = new System.Windows.Forms.OpenFileDialog();
            this.tpCocinas = new System.Windows.Forms.TabPage();
            this.gbCocinas = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnHecho = new System.Windows.Forms.Button();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.dgvCocinas = new System.Windows.Forms.DataGridView();
            this.slideAgregar = new SlickInterface.Slide();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sfFechaHasta = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.label13 = new System.Windows.Forms.Label();
            this.sfFechaDesde = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNroPedidoBuscar = new System.Windows.Forms.TextBox();
            this.cboEstadoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.btnNewCliente = new System.Windows.Forms.Button();
            this.sfFechaPrevista = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboClientes = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.slideDatos = new SlickInterface.Slide();
            this.slideControl = new SlickInterface.SlideControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcPedido = new System.Windows.Forms.TabControl();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).BeginInit();
            this.panelAcciones.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.tpCocinas.SuspendLayout();
            this.gbCocinas.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCocinas)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcPedido.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvDetallePedido);
            this.groupBox3.Controls.Add(this.panelAcciones);
            this.groupBox3.Location = new System.Drawing.Point(6, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(560, 201);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalle del pedido";
            // 
            // dgvDetallePedido
            // 
            this.dgvDetallePedido.AllowUserToAddRows = false;
            this.dgvDetallePedido.AllowUserToDeleteRows = false;
            this.dgvDetallePedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePedido.Location = new System.Drawing.Point(3, 16);
            this.dgvDetallePedido.MultiSelect = false;
            this.dgvDetallePedido.Name = "dgvDetallePedido";
            this.dgvDetallePedido.ReadOnly = true;
            this.dgvDetallePedido.RowHeadersVisible = false;
            this.dgvDetallePedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetallePedido.Size = new System.Drawing.Size(462, 180);
            this.dgvDetallePedido.TabIndex = 12;
            this.dgvDetallePedido.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetallePedido_CellFormatting);
            // 
            // panelAcciones
            // 
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.label7);
            this.panelAcciones.Controls.Add(this.label6);
            this.panelAcciones.Controls.Add(this.btnRestar);
            this.panelAcciones.Controls.Add(this.btnSumar);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Controls.Add(this.btnNew);
            this.panelAcciones.Location = new System.Drawing.Point(471, 16);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(78, 148);
            this.panelAcciones.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Cantidad";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cocina";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRestar
            // 
            this.btnRestar.FlatAppearance.BorderSize = 0;
            this.btnRestar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Restar_Gris_25;
            this.btnRestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestar.Location = new System.Drawing.Point(37, 103);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(30, 30);
            this.btnRestar.TabIndex = 16;
            this.btnRestar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            this.btnRestar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnRestar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnSumar
            // 
            this.btnSumar.FlatAppearance.BorderSize = 0;
            this.btnSumar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Sumar_Gris_25;
            this.btnSumar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSumar.Location = new System.Drawing.Point(3, 103);
            this.btnSumar.Name = "btnSumar";
            this.btnSumar.Size = new System.Drawing.Size(30, 30);
            this.btnSumar.TabIndex = 15;
            this.btnSumar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSumar.UseVisualStyleBackColor = true;
            this.btnSumar.Click += new System.EventHandler(this.btnSumar_Click);
            this.btnSumar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnSumar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Delete_25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(37, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNew
            // 
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.New_25;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(3, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(30, 30);
            this.btnNew.TabIndex = 13;
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.btnNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Location = new System.Drawing.Point(6, 401);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(560, 46);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(488, 15);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 18;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(418, 15);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // ofdImagen
            // 
            this.ofdImagen.Title = "Seleccione una imagen";
            // 
            // tpCocinas
            // 
            this.tpCocinas.Controls.Add(this.gbCocinas);
            this.tpCocinas.Controls.Add(this.slideAgregar);
            this.tpCocinas.Location = new System.Drawing.Point(4, 5);
            this.tpCocinas.Name = "tpCocinas";
            this.tpCocinas.Size = new System.Drawing.Size(570, 448);
            this.tpCocinas.TabIndex = 2;
            this.tpCocinas.UseVisualStyleBackColor = true;
            // 
            // gbCocinas
            // 
            this.gbCocinas.Controls.Add(this.panel1);
            this.gbCocinas.Controls.Add(this.dgvCocinas);
            this.gbCocinas.Location = new System.Drawing.Point(0, 0);
            this.gbCocinas.Name = "gbCocinas";
            this.gbCocinas.Size = new System.Drawing.Size(564, 190);
            this.gbCocinas.TabIndex = 13;
            this.gbCocinas.TabStop = false;
            this.gbCocinas.Text = "Cocinas";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnAgregar);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnHecho);
            this.panel1.Controls.Add(this.nudCantidad);
            this.panel1.Location = new System.Drawing.Point(470, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(73, 127);
            this.panel1.TabIndex = 6;
            // 
            // btnAgregar
            // 
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Location = new System.Drawing.Point(3, 63);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(64, 22);
            this.btnAgregar.TabIndex = 31;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Location = new System.Drawing.Point(0, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Cantidad";
            // 
            // btnHecho
            // 
            this.btnHecho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHecho.Location = new System.Drawing.Point(3, 90);
            this.btnHecho.Name = "btnHecho";
            this.btnHecho.Size = new System.Drawing.Size(64, 22);
            this.btnHecho.TabIndex = 32;
            this.btnHecho.Text = "Hecho";
            this.btnHecho.UseVisualStyleBackColor = true;
            this.btnHecho.Click += new System.EventHandler(this.btnHecho_Click);
            // 
            // nudCantidad
            // 
            this.nudCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudCantidad.Location = new System.Drawing.Point(3, 30);
            this.nudCantidad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(64, 20);
            this.nudCantidad.TabIndex = 30;
            this.nudCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCantidad.Enter += new System.EventHandler(this.nudCantidad_Enter);
            // 
            // dgvCocinas
            // 
            this.dgvCocinas.AllowUserToAddRows = false;
            this.dgvCocinas.AllowUserToDeleteRows = false;
            this.dgvCocinas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCocinas.Location = new System.Drawing.Point(6, 17);
            this.dgvCocinas.MultiSelect = false;
            this.dgvCocinas.Name = "dgvCocinas";
            this.dgvCocinas.ReadOnly = true;
            this.dgvCocinas.RowHeadersVisible = false;
            this.dgvCocinas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCocinas.Size = new System.Drawing.Size(441, 164);
            this.dgvCocinas.TabIndex = 0;
            this.dgvCocinas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCocinas_CellFormatting);
            // 
            // slideAgregar
            // 
            this.slideAgregar.Location = new System.Drawing.Point(6, 3);
            this.slideAgregar.Name = "slideAgregar";
            this.slideAgregar.Size = new System.Drawing.Size(561, 190);
            this.slideAgregar.SlideControl = null;
            this.slideAgregar.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvLista);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(9);
            this.groupBox2.Size = new System.Drawing.Size(564, 306);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Pedidos";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 22);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(546, 275);
            this.dgvLista.TabIndex = 6;
            this.dgvLista.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_RowEnter);
            this.dgvLista.DoubleClick += new System.EventHandler(this.dgvLista_DoubleClick);
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(232, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Estado:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(232, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Cliente:";
            // 
            // tsMenu
            // 
            this.tsMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnConsultar,
            this.btnModificar,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(2, 2);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0);
            this.tsMenu.Size = new System.Drawing.Size(578, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.New_25;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 47);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(57, 47);
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Text_Editor_25;
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(54, 47);
            this.btnModificar.Text = "&Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 47);
            this.btnEliminar.Text = "&Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sfFechaHasta);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.sfFechaDesde);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNroPedidoBuscar);
            this.groupBox1.Controls.Add(this.cboEstadoBuscar);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtNombreBuscar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // sfFechaHasta
            // 
            this.sfFechaHasta.CustomFormat = " ";
            this.sfFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaHasta.Location = new System.Drawing.Point(85, 46);
            this.sfFechaHasta.Name = "sfFechaHasta";
            this.sfFechaHasta.Size = new System.Drawing.Size(102, 20);
            this.sfFechaHasta.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Fecha Hasta:";
            // 
            // sfFechaDesde
            // 
            this.sfFechaDesde.CustomFormat = " ";
            this.sfFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaDesde.Location = new System.Drawing.Point(85, 20);
            this.sfFechaDesde.Name = "sfFechaDesde";
            this.sfFechaDesde.Size = new System.Drawing.Size(102, 20);
            this.sfFechaDesde.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Fecha Desde:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Número:";
            // 
            // txtNroPedidoBuscar
            // 
            this.txtNroPedidoBuscar.Location = new System.Drawing.Point(85, 72);
            this.txtNroPedidoBuscar.MaxLength = 80;
            this.txtNroPedidoBuscar.Name = "txtNroPedidoBuscar";
            this.txtNroPedidoBuscar.Size = new System.Drawing.Size(131, 20);
            this.txtNroPedidoBuscar.TabIndex = 2;
            // 
            // cboEstadoBuscar
            // 
            this.cboEstadoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstadoBuscar.FormattingEnabled = true;
            this.cboEstadoBuscar.Location = new System.Drawing.Point(282, 21);
            this.cboEstadoBuscar.Name = "cboEstadoBuscar";
            this.cboEstadoBuscar.Size = new System.Drawing.Size(115, 21);
            this.cboEstadoBuscar.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(480, 68);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombreBuscar
            // 
            this.txtNombreBuscar.Location = new System.Drawing.Point(282, 72);
            this.txtNombreBuscar.MaxLength = 80;
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(170, 20);
            this.txtNombreBuscar.TabIndex = 4;
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Controls.Add(this.slideDatos);
            this.tpDatos.Controls.Add(this.slideControl);
            this.tpDatos.Controls.Add(this.groupBox3);
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(570, 448);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.btnNewCliente);
            this.gbDatos.Controls.Add(this.sfFechaPrevista);
            this.gbDatos.Controls.Add(this.txtNumero);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.cboEstado);
            this.gbDatos.Controls.Add(this.cboClientes);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.txtObservacion);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Controls.Add(this.label1);
            this.gbDatos.Location = new System.Drawing.Point(3, 1);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(558, 185);
            this.gbDatos.TabIndex = 16;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del pedido";
            // 
            // btnNewCliente
            // 
            this.btnNewCliente.AutoSize = true;
            this.btnNewCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNewCliente.FlatAppearance.BorderSize = 0;
            this.btnNewCliente.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnNewCliente.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNewCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCliente.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCliente.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.New_25;
            this.btnNewCliente.Location = new System.Drawing.Point(386, 38);
            this.btnNewCliente.Name = "btnNewCliente";
            this.btnNewCliente.Size = new System.Drawing.Size(85, 31);
            this.btnNewCliente.TabIndex = 20;
            this.btnNewCliente.Text = "Nuevo...";
            this.btnNewCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewCliente.UseVisualStyleBackColor = true;
            this.btnNewCliente.Click += new System.EventHandler(this.btnNewCliente_Click);
            this.btnNewCliente.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNewCliente.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // sfFechaPrevista
            // 
            this.sfFechaPrevista.CustomFormat = " ";
            this.sfFechaPrevista.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaPrevista.Location = new System.Drawing.Point(96, 69);
            this.sfFechaPrevista.Name = "sfFechaPrevista";
            this.sfFechaPrevista.Size = new System.Drawing.Size(284, 20);
            this.sfFechaPrevista.TabIndex = 9;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(96, 19);
            this.txtNumero.MaxLength = 20;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(284, 20);
            this.txtNumero.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Numero:";
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(96, 94);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(284, 21);
            this.cboEstado.TabIndex = 10;
            // 
            // cboClientes
            // 
            this.cboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientes.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboClientes.FormattingEnabled = true;
            this.cboClientes.Location = new System.Drawing.Point(96, 44);
            this.cboClientes.Name = "cboClientes";
            this.cboClientes.Size = new System.Drawing.Size(284, 21);
            this.cboClientes.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Estado:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(96, 123);
            this.txtObservacion.MaxLength = 200;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(284, 50);
            this.txtObservacion.TabIndex = 11;
            this.txtObservacion.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Observación:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Cliente:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Fecha Prevista:";
            // 
            // slideDatos
            // 
            this.slideDatos.Location = new System.Drawing.Point(3, 3);
            this.slideDatos.Name = "slideDatos";
            this.slideDatos.Size = new System.Drawing.Size(564, 190);
            this.slideDatos.SlideControl = null;
            this.slideDatos.TabIndex = 15;
            // 
            // slideControl
            // 
            this.slideControl.Location = new System.Drawing.Point(6, 3);
            this.slideControl.Name = "slideControl";
            this.slideControl.Selected = null;
            this.slideControl.Size = new System.Drawing.Size(564, 195);
            this.slideControl.SlideSpeed = 250;
            this.slideControl.TabIndex = 14;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(570, 448);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcPedido, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(582, 513);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // tcPedido
            // 
            this.tcPedido.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcPedido.Controls.Add(this.tpBuscar);
            this.tcPedido.Controls.Add(this.tpDatos);
            this.tcPedido.Controls.Add(this.tpCocinas);
            this.tcPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPedido.ItemSize = new System.Drawing.Size(0, 1);
            this.tcPedido.Location = new System.Drawing.Point(2, 54);
            this.tcPedido.Margin = new System.Windows.Forms.Padding(0);
            this.tcPedido.Multiline = true;
            this.tcPedido.Name = "tcPedido";
            this.tcPedido.Padding = new System.Drawing.Point(0, 0);
            this.tcPedido.SelectedIndex = 0;
            this.tcPedido.Size = new System.Drawing.Size(578, 457);
            this.tcPedido.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcPedido.TabIndex = 8;
            // 
            // frmPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 513);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPedidos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Pedidos";
            this.Load += new System.EventHandler(this.frmPedidos_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.gbGuardarCancelar.ResumeLayout(false);
            this.tpCocinas.ResumeLayout(false);
            this.gbCocinas.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCocinas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.tpBuscar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcPedido.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvDetallePedido;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnSumar;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstadoBuscar;
        private System.Windows.Forms.OpenFileDialog ofdImagen;
        private System.Windows.Forms.TabPage tpCocinas;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcPedido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNroPedidoBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha sfFechaDesde;
        private System.Windows.Forms.Label label12;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha sfFechaHasta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbCocinas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnHecho;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.DataGridView dgvCocinas;
        private SlickInterface.Slide slideAgregar;
        private System.Windows.Forms.GroupBox gbDatos;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha sfFechaPrevista;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label9;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstado;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboClientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtObservacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private SlickInterface.Slide slideDatos;
        private SlickInterface.SlideControl slideControl;
        private System.Windows.Forms.Button btnNewCliente;
    }
}