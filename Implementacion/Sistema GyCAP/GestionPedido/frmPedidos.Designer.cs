namespace GyCAP.UI.GestionPedido
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
            this.btnCancelarPedido = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.ofdImagen = new System.Windows.Forms.OpenFileDialog();
            this.tpCocinas = new System.Windows.Forms.TabPage();
            this.gbCocinas = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dpfechaInicio = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.lblVerificarCapacidad = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnValidarCapacidad = new System.Windows.Forms.Button();
            this.btnValidarStock = new System.Windows.Forms.Button();
            this.cbUbicacionStock = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label14 = new System.Windows.Forms.Label();
            this.lblCantProducir = new System.Windows.Forms.Label();
            this.numCantProducir = new System.Windows.Forms.NumericUpDown();
            this.lblCantStock = new System.Windows.Forms.Label();
            this.numCantStock = new System.Windows.Forms.NumericUpDown();
            this.sfFechaPrevista = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lblTotalCocina = new System.Windows.Forms.Label();
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
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboClientes = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.numCantProducir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantStock)).BeginInit();
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
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvDetallePedido);
            this.groupBox3.Controls.Add(this.panelAcciones);
            this.groupBox3.Location = new System.Drawing.Point(6, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(698, 210);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalle del Pedido";
            // 
            // dgvDetallePedido
            // 
            this.dgvDetallePedido.AllowUserToAddRows = false;
            this.dgvDetallePedido.AllowUserToDeleteRows = false;
            this.dgvDetallePedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetallePedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePedido.Location = new System.Drawing.Point(3, 16);
            this.dgvDetallePedido.MultiSelect = false;
            this.dgvDetallePedido.Name = "dgvDetallePedido";
            this.dgvDetallePedido.ReadOnly = true;
            this.dgvDetallePedido.RowHeadersVisible = false;
            this.dgvDetallePedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetallePedido.Size = new System.Drawing.Size(605, 188);
            this.dgvDetallePedido.TabIndex = 12;
            this.dgvDetallePedido.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetallePedido_CellFormatting);
            this.dgvDetallePedido.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDetallePedido_DataBindingComplete);
            // 
            // panelAcciones
            // 
            this.panelAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.btnCancelarPedido);
            this.panelAcciones.Controls.Add(this.label6);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Controls.Add(this.btnNew);
            this.panelAcciones.Location = new System.Drawing.Point(614, 16);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(78, 188);
            this.panelAcciones.TabIndex = 11;
            // 
            // btnCancelarPedido
            // 
            this.btnCancelarPedido.CausesValidation = false;
            this.btnCancelarPedido.FlatAppearance.BorderSize = 0;
            this.btnCancelarPedido.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnCancelarPedido.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnCancelarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarPedido.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Warning_25;
            this.btnCancelarPedido.Location = new System.Drawing.Point(7, 136);
            this.btnCancelarPedido.Name = "btnCancelarPedido";
            this.btnCancelarPedido.Size = new System.Drawing.Size(66, 44);
            this.btnCancelarPedido.TabIndex = 18;
            this.btnCancelarPedido.Text = "Cancelar";
            this.btnCancelarPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelarPedido.UseVisualStyleBackColor = true;
            this.btnCancelarPedido.Click += new System.EventHandler(this.btnCancelarPedido_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 26);
            this.label6.TabIndex = 13;
            this.label6.Text = "Detalles \r\nde Pedido";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            this.btnDelete.CausesValidation = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Delete_25;
            this.btnDelete.Location = new System.Drawing.Point(7, 85);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 44);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNew
            // 
            this.btnNew.CausesValidation = false;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.New_25;
            this.btnNew.Location = new System.Drawing.Point(7, 34);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(66, 44);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "Nuevo";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.btnNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 413);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(700, 46);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(631, 14);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 18;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardar.Location = new System.Drawing.Point(561, 14);
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
            this.tpCocinas.Size = new System.Drawing.Size(710, 463);
            this.tpCocinas.TabIndex = 2;
            this.tpCocinas.UseVisualStyleBackColor = true;
            // 
            // gbCocinas
            // 
            this.gbCocinas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCocinas.Controls.Add(this.panel1);
            this.gbCocinas.Controls.Add(this.dgvCocinas);
            this.gbCocinas.Location = new System.Drawing.Point(0, 0);
            this.gbCocinas.Name = "gbCocinas";
            this.gbCocinas.Size = new System.Drawing.Size(704, 201);
            this.gbCocinas.TabIndex = 13;
            this.gbCocinas.TabStop = false;
            this.gbCocinas.Text = "Carga de Detalle";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dpfechaInicio);
            this.panel1.Controls.Add(this.lblFechaInicio);
            this.panel1.Controls.Add(this.lblVerificarCapacidad);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnValidarCapacidad);
            this.panel1.Controls.Add(this.btnValidarStock);
            this.panel1.Controls.Add(this.cbUbicacionStock);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.lblCantProducir);
            this.panel1.Controls.Add(this.numCantProducir);
            this.panel1.Controls.Add(this.lblCantStock);
            this.panel1.Controls.Add(this.numCantStock);
            this.panel1.Controls.Add(this.sfFechaPrevista);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnAgregar);
            this.panel1.Controls.Add(this.lblTotalCocina);
            this.panel1.Controls.Add(this.nudCantidad);
            this.panel1.Location = new System.Drawing.Point(414, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 178);
            this.panel1.TabIndex = 6;
            // 
            // dpfechaInicio
            // 
            this.dpfechaInicio.CausesValidation = false;
            this.dpfechaInicio.CustomFormat = " ";
            this.dpfechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpfechaInicio.Location = new System.Drawing.Point(95, 123);
            this.dpfechaInicio.Name = "dpfechaInicio";
            this.dpfechaInicio.Size = new System.Drawing.Size(131, 20);
            this.dpfechaInicio.TabIndex = 44;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Location = new System.Drawing.Point(1, 127);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(97, 13);
            this.lblFechaInicio.TabIndex = 45;
            this.lblFechaInicio.Text = "Fecha Inicio Prod.:";
            // 
            // lblVerificarCapacidad
            // 
            this.lblVerificarCapacidad.AutoSize = true;
            this.lblVerificarCapacidad.Location = new System.Drawing.Point(226, 117);
            this.lblVerificarCapacidad.Name = "lblVerificarCapacidad";
            this.lblVerificarCapacidad.Size = new System.Drawing.Size(59, 26);
            this.lblVerificarCapacidad.TabIndex = 43;
            this.lblVerificarCapacidad.Text = "Simular\r\nProducción";
            this.lblVerificarCapacidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 26);
            this.label7.TabIndex = 42;
            this.label7.Text = "Verificar \r\nStock";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnValidarCapacidad
            // 
            this.btnValidarCapacidad.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.System_25;
            this.btnValidarCapacidad.Location = new System.Drawing.Point(231, 76);
            this.btnValidarCapacidad.Name = "btnValidarCapacidad";
            this.btnValidarCapacidad.Size = new System.Drawing.Size(46, 41);
            this.btnValidarCapacidad.TabIndex = 41;
            this.btnValidarCapacidad.UseVisualStyleBackColor = true;
            this.btnValidarCapacidad.Click += new System.EventHandler(this.btnValidarCapacidad_Click);
            // 
            // btnValidarStock
            // 
            this.btnValidarStock.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.Apply_25;
            this.btnValidarStock.Location = new System.Drawing.Point(231, 4);
            this.btnValidarStock.Name = "btnValidarStock";
            this.btnValidarStock.Size = new System.Drawing.Size(46, 41);
            this.btnValidarStock.TabIndex = 40;
            this.btnValidarStock.UseVisualStyleBackColor = true;
            this.btnValidarStock.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // cbUbicacionStock
            // 
            this.cbUbicacionStock.CausesValidation = false;
            this.cbUbicacionStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUbicacionStock.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbUbicacionStock.FormattingEnabled = true;
            this.cbUbicacionStock.Location = new System.Drawing.Point(94, 52);
            this.cbUbicacionStock.Name = "cbUbicacionStock";
            this.cbUbicacionStock.Size = new System.Drawing.Size(131, 21);
            this.cbUbicacionStock.TabIndex = 32;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Stock Cocina:";
            // 
            // lblCantProducir
            // 
            this.lblCantProducir.AutoSize = true;
            this.lblCantProducir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCantProducir.Location = new System.Drawing.Point(0, 102);
            this.lblCantProducir.Name = "lblCantProducir";
            this.lblCantProducir.Size = new System.Drawing.Size(89, 13);
            this.lblCantProducir.TabIndex = 37;
            this.lblCantProducir.Text = "Cant. a Producir:";
            // 
            // numCantProducir
            // 
            this.numCantProducir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numCantProducir.CausesValidation = false;
            this.numCantProducir.Location = new System.Drawing.Point(94, 100);
            this.numCantProducir.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numCantProducir.Name = "numCantProducir";
            this.numCantProducir.Size = new System.Drawing.Size(131, 20);
            this.numCantProducir.TabIndex = 35;
            this.numCantProducir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCantStock
            // 
            this.lblCantStock.AutoSize = true;
            this.lblCantStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCantStock.Location = new System.Drawing.Point(0, 78);
            this.lblCantStock.Name = "lblCantStock";
            this.lblCantStock.Size = new System.Drawing.Size(82, 13);
            this.lblCantStock.TabIndex = 35;
            this.lblCantStock.Text = "Cant. En Stock:";
            // 
            // numCantStock
            // 
            this.numCantStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numCantStock.CausesValidation = false;
            this.numCantStock.Location = new System.Drawing.Point(94, 76);
            this.numCantStock.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numCantStock.Name = "numCantStock";
            this.numCantStock.Size = new System.Drawing.Size(131, 20);
            this.numCantStock.TabIndex = 33;
            this.numCantStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sfFechaPrevista
            // 
            this.sfFechaPrevista.CausesValidation = false;
            this.sfFechaPrevista.CustomFormat = " ";
            this.sfFechaPrevista.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sfFechaPrevista.Location = new System.Drawing.Point(94, 28);
            this.sfFechaPrevista.Name = "sfFechaPrevista";
            this.sfFechaPrevista.Size = new System.Drawing.Size(131, 20);
            this.sfFechaPrevista.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Fecha Necesidad:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Location = new System.Drawing.Point(94, 151);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(131, 22);
            this.btnAgregar.TabIndex = 37;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lblTotalCocina
            // 
            this.lblTotalCocina.AutoSize = true;
            this.lblTotalCocina.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTotalCocina.Location = new System.Drawing.Point(1, 11);
            this.lblTotalCocina.Name = "lblTotalCocina";
            this.lblTotalCocina.Size = new System.Drawing.Size(54, 13);
            this.lblTotalCocina.TabIndex = 1;
            this.lblTotalCocina.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudCantidad.CausesValidation = false;
            this.nudCantidad.Location = new System.Drawing.Point(94, 4);
            this.nudCantidad.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(131, 20);
            this.nudCantidad.TabIndex = 30;
            this.nudCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvCocinas
            // 
            this.dgvCocinas.AllowUserToAddRows = false;
            this.dgvCocinas.AllowUserToDeleteRows = false;
            this.dgvCocinas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCocinas.CausesValidation = false;
            this.dgvCocinas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCocinas.Location = new System.Drawing.Point(6, 17);
            this.dgvCocinas.MultiSelect = false;
            this.dgvCocinas.Name = "dgvCocinas";
            this.dgvCocinas.ReadOnly = true;
            this.dgvCocinas.RowHeadersVisible = false;
            this.dgvCocinas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCocinas.Size = new System.Drawing.Size(402, 175);
            this.dgvCocinas.TabIndex = 0;
            this.dgvCocinas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCocinas_CellFormatting);
            this.dgvCocinas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCocinas_DataBindingComplete);
            // 
            // slideAgregar
            // 
            this.slideAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.slideAgregar.Location = new System.Drawing.Point(0, 3);
            this.slideAgregar.Name = "slideAgregar";
            this.slideAgregar.Size = new System.Drawing.Size(707, 187);
            this.slideAgregar.SlideControl = null;
            this.slideAgregar.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvLista);
            this.groupBox2.Location = new System.Drawing.Point(3, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(9);
            this.groupBox2.Size = new System.Drawing.Size(701, 360);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Pedidos";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvLista.CausesValidation = false;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 22);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(683, 329);
            this.dgvLista.TabIndex = 6;
            this.dgvLista.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_RowEnter);
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting);
            this.dgvLista.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(441, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Estado:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(222, 59);
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
            this.tsMenu.Size = new System.Drawing.Size(718, 50);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // sfFechaHasta
            // 
            this.sfFechaHasta.CausesValidation = false;
            this.sfFechaHasta.CustomFormat = " ";
            this.sfFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaHasta.Location = new System.Drawing.Point(296, 20);
            this.sfFechaHasta.Name = "sfFechaHasta";
            this.sfFechaHasta.Size = new System.Drawing.Size(139, 20);
            this.sfFechaHasta.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(219, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Fecha Hasta:";
            // 
            // sfFechaDesde
            // 
            this.sfFechaDesde.CausesValidation = false;
            this.sfFechaDesde.CustomFormat = " ";
            this.sfFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaDesde.Location = new System.Drawing.Point(85, 20);
            this.sfFechaDesde.Name = "sfFechaDesde";
            this.sfFechaDesde.Size = new System.Drawing.Size(131, 20);
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
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Número:";
            // 
            // txtNroPedidoBuscar
            // 
            this.txtNroPedidoBuscar.CausesValidation = false;
            this.txtNroPedidoBuscar.Location = new System.Drawing.Point(85, 56);
            this.txtNroPedidoBuscar.MaxLength = 80;
            this.txtNroPedidoBuscar.Name = "txtNroPedidoBuscar";
            this.txtNroPedidoBuscar.Size = new System.Drawing.Size(131, 20);
            this.txtNroPedidoBuscar.TabIndex = 2;
            // 
            // cboEstadoBuscar
            // 
            this.cboEstadoBuscar.CausesValidation = false;
            this.cboEstadoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstadoBuscar.FormattingEnabled = true;
            this.cboEstadoBuscar.Location = new System.Drawing.Point(491, 21);
            this.cboEstadoBuscar.Name = "cboEstadoBuscar";
            this.cboEstadoBuscar.Size = new System.Drawing.Size(133, 21);
            this.cboEstadoBuscar.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.CausesValidation = false;
            this.btnBuscar.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(549, 50);
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
            this.txtNombreBuscar.CausesValidation = false;
            this.txtNombreBuscar.Location = new System.Drawing.Point(296, 56);
            this.txtNombreBuscar.MaxLength = 80;
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(189, 20);
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
            this.tpDatos.Size = new System.Drawing.Size(710, 463);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatos.Controls.Add(this.btnNewCliente);
            this.gbDatos.Controls.Add(this.txtNumero);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.cboEstado);
            this.gbDatos.Controls.Add(this.cboClientes);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.txtObservacion);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Location = new System.Drawing.Point(3, 1);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(703, 194);
            this.gbDatos.TabIndex = 16;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del Pedido";
            // 
            // btnNewCliente
            // 
            this.btnNewCliente.AutoSize = true;
            this.btnNewCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNewCliente.CausesValidation = false;
            this.btnNewCliente.FlatAppearance.BorderSize = 0;
            this.btnNewCliente.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnNewCliente.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNewCliente.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCliente.Image = global::GyCAP.UI.GestionPedido.Properties.Resources.New_25;
            this.btnNewCliente.Location = new System.Drawing.Point(329, 38);
            this.btnNewCliente.Name = "btnNewCliente";
            this.btnNewCliente.Size = new System.Drawing.Size(109, 31);
            this.btnNewCliente.TabIndex = 20;
            this.btnNewCliente.TabStop = false;
            this.btnNewCliente.Text = "Nuevo Cliente";
            this.btnNewCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewCliente.UseVisualStyleBackColor = true;
            this.btnNewCliente.Click += new System.EventHandler(this.btnNewCliente_Click);
            this.btnNewCliente.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNewCliente.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // txtNumero
            // 
            this.txtNumero.CausesValidation = false;
            this.txtNumero.Location = new System.Drawing.Point(82, 19);
            this.txtNumero.MaxLength = 20;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(153, 20);
            this.txtNumero.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Número:";
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(82, 71);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(153, 21);
            this.cboEstado.TabIndex = 10;
            // 
            // cboClientes
            // 
            this.cboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientes.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboClientes.FormattingEnabled = true;
            this.cboClientes.Location = new System.Drawing.Point(82, 44);
            this.cboClientes.Name = "cboClientes";
            this.cboClientes.Size = new System.Drawing.Size(241, 21);
            this.cboClientes.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Estado:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.CausesValidation = false;
            this.txtObservacion.Location = new System.Drawing.Point(82, 101);
            this.txtObservacion.MaxLength = 200;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(356, 79);
            this.txtObservacion.TabIndex = 11;
            this.txtObservacion.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Observación:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Cliente:";
            // 
            // slideDatos
            // 
            this.slideDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.slideDatos.Location = new System.Drawing.Point(3, 3);
            this.slideDatos.Name = "slideDatos";
            this.slideDatos.Size = new System.Drawing.Size(707, 195);
            this.slideDatos.SlideControl = null;
            this.slideDatos.TabIndex = 15;
            // 
            // slideControl
            // 
            this.slideControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.slideControl.Location = new System.Drawing.Point(6, 3);
            this.slideControl.Name = "slideControl";
            this.slideControl.Selected = null;
            this.slideControl.Size = new System.Drawing.Size(704, 195);
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
            this.tpBuscar.Size = new System.Drawing.Size(710, 463);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(722, 528);
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
            this.tcPedido.Size = new System.Drawing.Size(718, 472);
            this.tcPedido.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcPedido.TabIndex = 8;
            // 
            // frmPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 528);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPedidos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Pedidos";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.gbGuardarCancelar.ResumeLayout(false);
            this.tpCocinas.ResumeLayout(false);
            this.gbCocinas.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantProducir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantStock)).EndInit();
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
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Label lblTotalCocina;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.DataGridView dgvCocinas;
        private SlickInterface.Slide slideAgregar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label9;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstado;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboClientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtObservacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SlickInterface.Slide slideDatos;
        private SlickInterface.SlideControl slideControl;
        private System.Windows.Forms.Button btnNewCliente;
        private System.Windows.Forms.Label lblCantProducir;
        private System.Windows.Forms.NumericUpDown numCantProducir;
        private System.Windows.Forms.Label lblCantStock;
        private System.Windows.Forms.NumericUpDown numCantStock;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha sfFechaPrevista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbUbicacionStock;
        private System.Windows.Forms.Button btnCancelarPedido;
        private System.Windows.Forms.Button btnValidarStock;
        private System.Windows.Forms.Button btnValidarCapacidad;
        private System.Windows.Forms.Label label7;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dpfechaInicio;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblVerificarCapacidad;
    }
}