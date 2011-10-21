namespace GyCAP.UI.GestionStock
{
    partial class frmEntregaProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntregaProducto));
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDetalleGrilla = new System.Windows.Forms.GroupBox();
            this.dgvDatosEntrega = new System.Windows.Forms.DataGridView();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnSumar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tcEntregaProducto = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gbGrillaDetalleBus = new System.Windows.Forms.GroupBox();
            this.dgvDetalleBusqueda = new System.Windows.Forms.DataGridView();
            this.gbGrillaEntregasBus = new System.Windows.Forms.GroupBox();
            this.dgvListaEntregas = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chFechaEntrega = new System.Windows.Forms.CheckBox();
            this.chCliente = new System.Windows.Forms.CheckBox();
            this.dtpFechaBusqueda = new System.Windows.Forms.DateTimePicker();
            this.cbClienteBus = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbStock = new System.Windows.Forms.GroupBox();
            this.tcDatos = new System.Windows.Forms.TabControl();
            this.tpPedidos = new System.Windows.Forms.TabPage();
            this.gbPedidos = new System.Windows.Forms.GroupBox();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.lblMsj = new System.Windows.Forms.Label();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.tpDetallePedido = new System.Windows.Forms.TabPage();
            this.gbDetallePedido = new System.Windows.Forms.GroupBox();
            this.btnEntregar = new System.Windows.Forms.Button();
            this.dgvDetallePedido = new System.Windows.Forms.DataGridView();
            this.gbBotones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCargaDetalle = new System.Windows.Forms.Button();
            this.cbCliente = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEmpleado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.gbDetalleGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosEntrega)).BeginInit();
            this.panelAcciones.SuspendLayout();
            this.tcEntregaProducto.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.gbGrillaDetalleBus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleBusqueda)).BeginInit();
            this.gbGrillaEntregasBus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaEntregas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbStock.SuspendLayout();
            this.tcDatos.SuspendLayout();
            this.tpPedidos.SuspendLayout();
            this.gbPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.tpDetallePedido.SuspendLayout();
            this.gbDetallePedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).BeginInit();
            this.gbBotones.SuspendLayout();
            this.gbDatosPrincipales.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(188, 17);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 26);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gbDetalleGrilla
            // 
            this.gbDetalleGrilla.Controls.Add(this.dgvDatosEntrega);
            this.gbDetalleGrilla.Controls.Add(this.panelAcciones);
            this.gbDetalleGrilla.Location = new System.Drawing.Point(402, 70);
            this.gbDetalleGrilla.Name = "gbDetalleGrilla";
            this.gbDetalleGrilla.Size = new System.Drawing.Size(352, 330);
            this.gbDetalleGrilla.TabIndex = 17;
            this.gbDetalleGrilla.TabStop = false;
            this.gbDetalleGrilla.Text = "Detalle Entrega de Productos Terminados";
            // 
            // dgvDatosEntrega
            // 
            this.dgvDatosEntrega.AllowUserToAddRows = false;
            this.dgvDatosEntrega.AllowUserToDeleteRows = false;
            this.dgvDatosEntrega.AllowUserToResizeRows = false;
            this.dgvDatosEntrega.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosEntrega.Location = new System.Drawing.Point(12, 17);
            this.dgvDatosEntrega.Name = "dgvDatosEntrega";
            this.dgvDatosEntrega.ReadOnly = true;
            this.dgvDatosEntrega.RowHeadersVisible = false;
            this.dgvDatosEntrega.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatosEntrega.Size = new System.Drawing.Size(334, 246);
            this.dgvDatosEntrega.TabIndex = 5;
            this.dgvDatosEntrega.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDatosEntrega_CellFormatting);
            this.dgvDatosEntrega.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDatosEntrega_DataBindingComplete);
            // 
            // panelAcciones
            // 
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.label7);
            this.panelAcciones.Controls.Add(this.btnRestar);
            this.panelAcciones.Controls.Add(this.btnSumar);
            this.panelAcciones.Controls.Add(this.label6);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Location = new System.Drawing.Point(12, 268);
            this.panelAcciones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(334, 51);
            this.panelAcciones.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Cantidades";
            // 
            // btnRestar
            // 
            this.btnRestar.FlatAppearance.BorderSize = 0;
            this.btnRestar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestar.Image = ((System.Drawing.Image)(resources.GetObject("btnRestar.Image")));
            this.btnRestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestar.Location = new System.Drawing.Point(253, 2);
            this.btnRestar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(30, 30);
            this.btnRestar.TabIndex = 19;
            this.btnRestar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnSumar
            // 
            this.btnSumar.FlatAppearance.BorderSize = 0;
            this.btnSumar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumar.Image = ((System.Drawing.Image)(resources.GetObject("btnSumar.Image")));
            this.btnSumar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSumar.Location = new System.Drawing.Point(214, 2);
            this.btnSumar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSumar.Name = "btnSumar";
            this.btnSumar.Size = new System.Drawing.Size(30, 30);
            this.btnSumar.TabIndex = 18;
            this.btnSumar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSumar.UseVisualStyleBackColor = true;
            this.btnSumar.Click += new System.EventHandler(this.btnSumar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Eliminar";
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Delete_25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(76, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tcEntregaProducto
            // 
            this.tcEntregaProducto.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcEntregaProducto.Controls.Add(this.tpBuscar);
            this.tcEntregaProducto.Controls.Add(this.tpDatos);
            this.tcEntregaProducto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEntregaProducto.ItemSize = new System.Drawing.Size(0, 1);
            this.tcEntregaProducto.Location = new System.Drawing.Point(2, 54);
            this.tcEntregaProducto.Margin = new System.Windows.Forms.Padding(0);
            this.tcEntregaProducto.Multiline = true;
            this.tcEntregaProducto.Name = "tcEntregaProducto";
            this.tcEntregaProducto.Padding = new System.Drawing.Point(0, 0);
            this.tcEntregaProducto.SelectedIndex = 0;
            this.tcEntregaProducto.Size = new System.Drawing.Size(768, 470);
            this.tcEntregaProducto.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcEntregaProducto.TabIndex = 8;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.gbGrillaDetalleBus);
            this.tpBuscar.Controls.Add(this.gbGrillaEntregasBus);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(760, 461);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // gbGrillaDetalleBus
            // 
            this.gbGrillaDetalleBus.Controls.Add(this.dgvDetalleBusqueda);
            this.gbGrillaDetalleBus.Location = new System.Drawing.Point(380, 75);
            this.gbGrillaDetalleBus.Name = "gbGrillaDetalleBus";
            this.gbGrillaDetalleBus.Size = new System.Drawing.Size(374, 380);
            this.gbGrillaDetalleBus.TabIndex = 2;
            this.gbGrillaDetalleBus.TabStop = false;
            this.gbGrillaDetalleBus.Text = "Listado Detalle Entrega";
            // 
            // dgvDetalleBusqueda
            // 
            this.dgvDetalleBusqueda.AllowUserToAddRows = false;
            this.dgvDetalleBusqueda.AllowUserToDeleteRows = false;
            this.dgvDetalleBusqueda.AllowUserToResizeColumns = false;
            this.dgvDetalleBusqueda.AllowUserToResizeRows = false;
            this.dgvDetalleBusqueda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalleBusqueda.Enabled = false;
            this.dgvDetalleBusqueda.Location = new System.Drawing.Point(3, 16);
            this.dgvDetalleBusqueda.MultiSelect = false;
            this.dgvDetalleBusqueda.Name = "dgvDetalleBusqueda";
            this.dgvDetalleBusqueda.ReadOnly = true;
            this.dgvDetalleBusqueda.RowHeadersVisible = false;
            this.dgvDetalleBusqueda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleBusqueda.Size = new System.Drawing.Size(368, 361);
            this.dgvDetalleBusqueda.TabIndex = 1;
            this.dgvDetalleBusqueda.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetalleBusqueda_CellFormatting);
            this.dgvDetalleBusqueda.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDetalleBusqueda_DataBindingComplete);
            // 
            // gbGrillaEntregasBus
            // 
            this.gbGrillaEntregasBus.Controls.Add(this.dgvListaEntregas);
            this.gbGrillaEntregasBus.Location = new System.Drawing.Point(6, 75);
            this.gbGrillaEntregasBus.Name = "gbGrillaEntregasBus";
            this.gbGrillaEntregasBus.Size = new System.Drawing.Size(368, 380);
            this.gbGrillaEntregasBus.TabIndex = 1;
            this.gbGrillaEntregasBus.TabStop = false;
            this.gbGrillaEntregasBus.Text = "Listado de Entregas";
            // 
            // dgvListaEntregas
            // 
            this.dgvListaEntregas.AllowUserToAddRows = false;
            this.dgvListaEntregas.AllowUserToDeleteRows = false;
            this.dgvListaEntregas.AllowUserToResizeRows = false;
            this.dgvListaEntregas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaEntregas.Location = new System.Drawing.Point(3, 16);
            this.dgvListaEntregas.MultiSelect = false;
            this.dgvListaEntregas.Name = "dgvListaEntregas";
            this.dgvListaEntregas.ReadOnly = true;
            this.dgvListaEntregas.RowHeadersVisible = false;
            this.dgvListaEntregas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaEntregas.Size = new System.Drawing.Size(362, 361);
            this.dgvListaEntregas.TabIndex = 1;
            this.dgvListaEntregas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvListaEntregas_CellFormatting);
            this.dgvListaEntregas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaEntregas_CellClick);
            this.dgvListaEntregas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvListaEntregas_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chFechaEntrega);
            this.groupBox1.Controls.Add(this.chCliente);
            this.groupBox1.Controls.Add(this.dtpFechaBusqueda);
            this.groupBox1.Controls.Add(this.cbClienteBus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // chFechaEntrega
            // 
            this.chFechaEntrega.AutoSize = true;
            this.chFechaEntrega.Location = new System.Drawing.Point(268, 27);
            this.chFechaEntrega.Name = "chFechaEntrega";
            this.chFechaEntrega.Size = new System.Drawing.Size(15, 14);
            this.chFechaEntrega.TabIndex = 33;
            this.chFechaEntrega.UseVisualStyleBackColor = true;
            // 
            // chCliente
            // 
            this.chCliente.AutoSize = true;
            this.chCliente.Location = new System.Drawing.Point(22, 27);
            this.chCliente.Name = "chCliente";
            this.chCliente.Size = new System.Drawing.Size(15, 14);
            this.chCliente.TabIndex = 32;
            this.chCliente.UseVisualStyleBackColor = true;
            // 
            // dtpFechaBusqueda
            // 
            this.dtpFechaBusqueda.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaBusqueda.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaBusqueda.Location = new System.Drawing.Point(380, 23);
            this.dtpFechaBusqueda.Name = "dtpFechaBusqueda";
            this.dtpFechaBusqueda.Size = new System.Drawing.Size(86, 20);
            this.dtpFechaBusqueda.TabIndex = 31;
            // 
            // cbClienteBus
            // 
            this.cbClienteBus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClienteBus.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbClienteBus.FormattingEnabled = true;
            this.cbClienteBus.Location = new System.Drawing.Point(85, 24);
            this.cbClienteBus.Name = "cbClienteBus";
            this.cbClienteBus.Size = new System.Drawing.Size(139, 21);
            this.cbClienteBus.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Cliente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Fecha de Entrega:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.GestionStock.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(560, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.groupBox3);
            this.tpDatos.Controls.Add(this.gbStock);
            this.tpDatos.Controls.Add(this.gbDetalleGrilla);
            this.tpDatos.Controls.Add(this.gbBotones);
            this.tpDatos.Controls.Add(this.gbDatosPrincipales);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(760, 461);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(8, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 52);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            // 
            // gbStock
            // 
            this.gbStock.Controls.Add(this.tcDatos);
            this.gbStock.Location = new System.Drawing.Point(8, 70);
            this.gbStock.Name = "gbStock";
            this.gbStock.Size = new System.Drawing.Size(388, 330);
            this.gbStock.TabIndex = 18;
            this.gbStock.TabStop = false;
            this.gbStock.Text = "Carga Entrega de Productos Terminados";
            // 
            // tcDatos
            // 
            this.tcDatos.Controls.Add(this.tpPedidos);
            this.tcDatos.Controls.Add(this.tpDetallePedido);
            this.tcDatos.Location = new System.Drawing.Point(6, 17);
            this.tcDatos.Name = "tcDatos";
            this.tcDatos.SelectedIndex = 0;
            this.tcDatos.Size = new System.Drawing.Size(376, 302);
            this.tcDatos.TabIndex = 20;
            this.tcDatos.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcDatos_Selecting);
            // 
            // tpPedidos
            // 
            this.tpPedidos.BackColor = System.Drawing.Color.Transparent;
            this.tpPedidos.Controls.Add(this.gbPedidos);
            this.tpPedidos.Controls.Add(this.btnVerDetalle);
            this.tpPedidos.Location = new System.Drawing.Point(4, 22);
            this.tpPedidos.Name = "tpPedidos";
            this.tpPedidos.Padding = new System.Windows.Forms.Padding(3);
            this.tpPedidos.Size = new System.Drawing.Size(368, 276);
            this.tpPedidos.TabIndex = 1;
            this.tpPedidos.Text = "Pedidos Cliente";
            this.tpPedidos.UseVisualStyleBackColor = true;
            // 
            // gbPedidos
            // 
            this.gbPedidos.Controls.Add(this.dgvPedidos);
            this.gbPedidos.Controls.Add(this.lblMsj);
            this.gbPedidos.Location = new System.Drawing.Point(3, 3);
            this.gbPedidos.Name = "gbPedidos";
            this.gbPedidos.Size = new System.Drawing.Size(359, 235);
            this.gbPedidos.TabIndex = 0;
            this.gbPedidos.TabStop = false;
            this.gbPedidos.Text = "Pedidos Cliente";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.AllowUserToResizeRows = false;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.Location = new System.Drawing.Point(3, 16);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.ReadOnly = true;
            this.dgvPedidos.RowHeadersVisible = false;
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.Size = new System.Drawing.Size(353, 216);
            this.dgvPedidos.TabIndex = 16;
            this.dgvPedidos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPedidos_CellFormatting);
            this.dgvPedidos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPedidos_DataBindingComplete);
            // 
            // lblMsj
            // 
            this.lblMsj.Location = new System.Drawing.Point(33, 83);
            this.lblMsj.Name = "lblMsj";
            this.lblMsj.Size = new System.Drawing.Size(273, 32);
            this.lblMsj.TabIndex = 17;
            this.lblMsj.Text = "El cliente seleccionado no tiene Pedidos en Estado Finalizado para Entregar";
            this.lblMsj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.Location = new System.Drawing.Point(287, 244);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(75, 26);
            this.btnVerDetalle.TabIndex = 14;
            this.btnVerDetalle.Text = "Ver Detalle";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // tpDetallePedido
            // 
            this.tpDetallePedido.BackColor = System.Drawing.Color.Transparent;
            this.tpDetallePedido.Controls.Add(this.gbDetallePedido);
            this.tpDetallePedido.Location = new System.Drawing.Point(4, 22);
            this.tpDetallePedido.Name = "tpDetallePedido";
            this.tpDetallePedido.Size = new System.Drawing.Size(368, 276);
            this.tpDetallePedido.TabIndex = 2;
            this.tpDetallePedido.Text = "DetallePedido";
            this.tpDetallePedido.UseVisualStyleBackColor = true;
            // 
            // gbDetallePedido
            // 
            this.gbDetallePedido.BackColor = System.Drawing.SystemColors.Control;
            this.gbDetallePedido.Controls.Add(this.btnEntregar);
            this.gbDetallePedido.Controls.Add(this.dgvDetallePedido);
            this.gbDetallePedido.Location = new System.Drawing.Point(7, 6);
            this.gbDetallePedido.Name = "gbDetallePedido";
            this.gbDetallePedido.Size = new System.Drawing.Size(358, 267);
            this.gbDetallePedido.TabIndex = 1;
            this.gbDetallePedido.TabStop = false;
            this.gbDetallePedido.Text = "Detalle Pedido";
            // 
            // btnEntregar
            // 
            this.btnEntregar.Location = new System.Drawing.Point(274, 235);
            this.btnEntregar.Name = "btnEntregar";
            this.btnEntregar.Size = new System.Drawing.Size(75, 26);
            this.btnEntregar.TabIndex = 14;
            this.btnEntregar.Text = "Entregar";
            this.btnEntregar.UseVisualStyleBackColor = true;
            this.btnEntregar.Click += new System.EventHandler(this.btnEntregar_Click);
            // 
            // dgvDetallePedido
            // 
            this.dgvDetallePedido.AllowUserToAddRows = false;
            this.dgvDetallePedido.AllowUserToDeleteRows = false;
            this.dgvDetallePedido.AllowUserToResizeRows = false;
            this.dgvDetallePedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePedido.Location = new System.Drawing.Point(6, 19);
            this.dgvDetallePedido.Name = "dgvDetallePedido";
            this.dgvDetallePedido.ReadOnly = true;
            this.dgvDetallePedido.RowHeadersVisible = false;
            this.dgvDetallePedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetallePedido.Size = new System.Drawing.Size(343, 199);
            this.dgvDetallePedido.TabIndex = 7;
            this.dgvDetallePedido.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetallePedido_CellFormatting);
            this.dgvDetallePedido.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDetallePedido_DataBindingComplete);
            // 
            // gbBotones
            // 
            this.gbBotones.Controls.Add(this.btnVolver);
            this.gbBotones.Controls.Add(this.btnGuardar);
            this.gbBotones.Location = new System.Drawing.Point(402, 406);
            this.gbBotones.Name = "gbBotones";
            this.gbBotones.Size = new System.Drawing.Size(352, 49);
            this.gbBotones.TabIndex = 13;
            this.gbBotones.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(267, 17);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 26);
            this.btnVolver.TabIndex = 22;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Controls.Add(this.dtpFechaEntrega);
            this.gbDatosPrincipales.Controls.Add(this.label12);
            this.gbDatosPrincipales.Controls.Add(this.btnCargaDetalle);
            this.gbDatosPrincipales.Controls.Add(this.cbCliente);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Controls.Add(this.cbEmpleado);
            this.gbDatosPrincipales.Controls.Add(this.label4);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 3);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(751, 61);
            this.gbDatosPrincipales.TabIndex = 10;
            this.gbDatosPrincipales.TabStop = false;
            this.gbDatosPrincipales.Text = "Datos Principales";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(285, 24);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(86, 20);
            this.dtpFechaEntrega.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(381, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Responsable Entrega:";
            // 
            // btnCargaDetalle
            // 
            this.btnCargaDetalle.Location = new System.Drawing.Point(659, 21);
            this.btnCargaDetalle.Name = "btnCargaDetalle";
            this.btnCargaDetalle.Size = new System.Drawing.Size(85, 26);
            this.btnCargaDetalle.TabIndex = 25;
            this.btnCargaDetalle.Text = "Cargar Detalle";
            this.btnCargaDetalle.UseVisualStyleBackColor = true;
            this.btnCargaDetalle.Click += new System.EventHandler(this.btnCargaDetalle_Click);
            // 
            // cbCliente
            // 
            this.cbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCliente.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(51, 24);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(133, 21);
            this.cbCliente.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Cliente:";
            // 
            // cbEmpleado
            // 
            this.cbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmpleado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEmpleado.FormattingEnabled = true;
            this.cbEmpleado.Location = new System.Drawing.Point(499, 24);
            this.cbEmpleado.Name = "cbEmpleado";
            this.cbEmpleado.Size = new System.Drawing.Size(145, 21);
            this.cbEmpleado.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Fecha Entrega:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcEntregaProducto, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 526);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tsMenu
            // 
            this.tsMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8F);
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
            this.tsMenu.Size = new System.Drawing.Size(768, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.GestionStock.Properties.Resources.New_25;
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
            this.btnConsultar.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(43, 47);
            this.btnConsultar.Text = "&Buscar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Text_Editor_25;
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
            this.btnEliminar.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Delete_25;
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
            this.btnSalir.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmEntregaProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 526);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmEntregaProducto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Entrega de Productos Terminados";
            this.gbDetalleGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosEntrega)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.tcEntregaProducto.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.gbGrillaDetalleBus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleBusqueda)).EndInit();
            this.gbGrillaEntregasBus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaEntregas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbStock.ResumeLayout(false);
            this.tcDatos.ResumeLayout(false);
            this.tpPedidos.ResumeLayout(false);
            this.gbPedidos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.tpDetallePedido.ResumeLayout(false);
            this.gbDetallePedido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).EndInit();
            this.gbBotones.ResumeLayout(false);
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDetalleGrilla;
        private System.Windows.Forms.DataGridView dgvDatosEntrega;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnSumar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TabControl tcEntregaProducto;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox gbGrillaDetalleBus;
        private System.Windows.Forms.DataGridView dgvDetalleBusqueda;
        private System.Windows.Forms.GroupBox gbGrillaEntregasBus;
        private System.Windows.Forms.DataGridView dgvListaEntregas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbBotones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private System.Windows.Forms.Button btnCargaDetalle;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCliente;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEmpleado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip tsMenu;
        public System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.Label label12;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbClienteBus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaBusqueda;
        private System.Windows.Forms.CheckBox chFechaEntrega;
        private System.Windows.Forms.CheckBox chCliente;
        private System.Windows.Forms.GroupBox gbStock;
        private System.Windows.Forms.TabControl tcDatos;
        private System.Windows.Forms.TabPage tpPedidos;
        private System.Windows.Forms.GroupBox gbPedidos;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.TabPage tpDetallePedido;
        private System.Windows.Forms.GroupBox gbDetallePedido;
        private System.Windows.Forms.Button btnEntregar;
        private System.Windows.Forms.DataGridView dgvDetallePedido;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Label lblMsj;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}