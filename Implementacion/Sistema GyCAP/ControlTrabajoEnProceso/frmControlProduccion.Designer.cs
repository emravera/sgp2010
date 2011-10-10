namespace GyCAP.UI.ControlTrabajoEnProceso
{
    partial class frmControlProduccion
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcOrdenTrabajo = new System.Windows.Forms.TabControl();
            this.tpOrdenesProduccion = new System.Windows.Forms.TabPage();
            this.gbBuscarOtros = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscarOP = new System.Windows.Forms.Button();
            this.txtCodigoOPBuscar = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvOrdenesProduccion = new System.Windows.Forms.DataGridView();
            this.tpOrdenesTrabajo = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFiltrarOT = new System.Windows.Forms.Button();
            this.txtCodigoOTFiltrar = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.dgvOrdenesTrabajo = new System.Windows.Forms.DataGridView();
            this.tpCierreParcial = new System.Windows.Forms.TabPage();
            this.gbAgregarCierreParcial = new System.Windows.Forms.GroupBox();
            this.btnCancelarCierre = new System.Windows.Forms.Button();
            this.btnGuardarCierre = new System.Windows.Forms.Button();
            this.txtObservacionesCierre = new System.Windows.Forms.RichTextBox();
            this.nudCantidadCierre = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEliminarCierre = new System.Windows.Forms.Button();
            this.btnModificarCierre = new System.Windows.Forms.Button();
            this.dgvCierresParciales = new System.Windows.Forms.DataGridView();
            this.btnAgregarCierre = new System.Windows.Forms.Button();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnIniciar = new System.Windows.Forms.ToolStripButton();
            this.btnFinalizar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.cmsProduccion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiBloquearProduccion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesbloquearProduccion = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTrabajo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiBloquearTrabajo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesbloquearTrabajo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCierres = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiBloquearCierre = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesbloquearCierre = new System.Windows.Forms.ToolStripMenuItem();
            this.nudOperacionesFallidas = new System.Windows.Forms.NumericUpDown();
            this.dtpFechaHastaOPBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaDesdeOPBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaGeneracionOPBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cboModoOPBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboEstadoOPBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpFechaInicioOTFiltrar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cboEstadoOTFiltrar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboMaquinaCierre = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboEmpleadoCierre = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpFechaCierre = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcOrdenTrabajo.SuspendLayout();
            this.tpOrdenesProduccion.SuspendLayout();
            this.gbBuscarOtros.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesProduccion)).BeginInit();
            this.tpOrdenesTrabajo.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesTrabajo)).BeginInit();
            this.tpCierreParcial.SuspendLayout();
            this.gbAgregarCierreParcial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadCierre)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCierresParciales)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.cmsProduccion.SuspendLayout();
            this.cmsTrabajo.SuspendLayout();
            this.cmsCierres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOperacionesFallidas)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcOrdenTrabajo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 572);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tcOrdenTrabajo
            // 
            this.tcOrdenTrabajo.Controls.Add(this.tpOrdenesProduccion);
            this.tcOrdenTrabajo.Controls.Add(this.tpOrdenesTrabajo);
            this.tcOrdenTrabajo.Controls.Add(this.tpCierreParcial);
            this.tcOrdenTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOrdenTrabajo.ItemSize = new System.Drawing.Size(150, 18);
            this.tcOrdenTrabajo.Location = new System.Drawing.Point(3, 53);
            this.tcOrdenTrabajo.Multiline = true;
            this.tcOrdenTrabajo.Name = "tcOrdenTrabajo";
            this.tcOrdenTrabajo.SelectedIndex = 0;
            this.tcOrdenTrabajo.Size = new System.Drawing.Size(788, 516);
            this.tcOrdenTrabajo.TabIndex = 0;
            this.tcOrdenTrabajo.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcOrdenTrabajo_Selecting);
            // 
            // tpOrdenesProduccion
            // 
            this.tpOrdenesProduccion.Controls.Add(this.gbBuscarOtros);
            this.tpOrdenesProduccion.Controls.Add(this.groupBox2);
            this.tpOrdenesProduccion.Location = new System.Drawing.Point(4, 22);
            this.tpOrdenesProduccion.Name = "tpOrdenesProduccion";
            this.tpOrdenesProduccion.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrdenesProduccion.Size = new System.Drawing.Size(780, 490);
            this.tpOrdenesProduccion.TabIndex = 0;
            this.tpOrdenesProduccion.Text = "Órdenes de Producción";
            this.tpOrdenesProduccion.UseVisualStyleBackColor = true;
            // 
            // gbBuscarOtros
            // 
            this.gbBuscarOtros.Controls.Add(this.dtpFechaHastaOPBuscar);
            this.gbBuscarOtros.Controls.Add(this.dtpFechaDesdeOPBuscar);
            this.gbBuscarOtros.Controls.Add(this.label5);
            this.gbBuscarOtros.Controls.Add(this.label2);
            this.gbBuscarOtros.Controls.Add(this.label1);
            this.gbBuscarOtros.Controls.Add(this.dtpFechaGeneracionOPBuscar);
            this.gbBuscarOtros.Controls.Add(this.btnBuscarOP);
            this.gbBuscarOtros.Controls.Add(this.txtCodigoOPBuscar);
            this.gbBuscarOtros.Controls.Add(this.label10);
            this.gbBuscarOtros.Controls.Add(this.cboModoOPBuscar);
            this.gbBuscarOtros.Controls.Add(this.label8);
            this.gbBuscarOtros.Controls.Add(this.cboEstadoOPBuscar);
            this.gbBuscarOtros.Controls.Add(this.label7);
            this.gbBuscarOtros.Location = new System.Drawing.Point(6, 6);
            this.gbBuscarOtros.Name = "gbBuscarOtros";
            this.gbBuscarOtros.Size = new System.Drawing.Size(765, 127);
            this.gbBuscarOtros.TabIndex = 3;
            this.gbBuscarOtros.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Fecha inicio hasta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Fecha inicio desde:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Fecha generación:";
            // 
            // btnBuscarOP
            // 
            this.btnBuscarOP.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.lupa_20;
            this.btnBuscarOP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarOP.Location = new System.Drawing.Point(649, 52);
            this.btnBuscarOP.Name = "btnBuscarOP";
            this.btnBuscarOP.Size = new System.Drawing.Size(75, 26);
            this.btnBuscarOP.TabIndex = 7;
            this.btnBuscarOP.Text = "&Buscar";
            this.btnBuscarOP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarOP.UseVisualStyleBackColor = true;
            this.btnBuscarOP.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCodigoOPBuscar
            // 
            this.txtCodigoOPBuscar.Location = new System.Drawing.Point(72, 25);
            this.txtCodigoOPBuscar.Name = "txtCodigoOPBuscar";
            this.txtCodigoOPBuscar.Size = new System.Drawing.Size(193, 21);
            this.txtCodigoOPBuscar.TabIndex = 1;
            this.txtCodigoOPBuscar.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Código:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Modo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Estado:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvOrdenesProduccion);
            this.groupBox2.Location = new System.Drawing.Point(3, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 345);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Órdenes de Producción";
            // 
            // dgvOrdenesProduccion
            // 
            this.dgvOrdenesProduccion.AllowUserToAddRows = false;
            this.dgvOrdenesProduccion.AllowUserToDeleteRows = false;
            this.dgvOrdenesProduccion.AllowUserToResizeRows = false;
            this.dgvOrdenesProduccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesProduccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenesProduccion.Location = new System.Drawing.Point(3, 17);
            this.dgvOrdenesProduccion.MultiSelect = false;
            this.dgvOrdenesProduccion.Name = "dgvOrdenesProduccion";
            this.dgvOrdenesProduccion.ReadOnly = true;
            this.dgvOrdenesProduccion.RowHeadersVisible = false;
            this.dgvOrdenesProduccion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenesProduccion.Size = new System.Drawing.Size(765, 325);
            this.dgvOrdenesProduccion.TabIndex = 8;
            this.dgvOrdenesProduccion.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenesProduccion_RowEnter);
            this.dgvOrdenesProduccion.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrdenesProduccion_ColumnHeaderMouseClick);
            this.dgvOrdenesProduccion.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrdenesProduccion_CellFormatting);
            // 
            // tpOrdenesTrabajo
            // 
            this.tpOrdenesTrabajo.Controls.Add(this.groupBox4);
            this.tpOrdenesTrabajo.Controls.Add(this.groupBox3);
            this.tpOrdenesTrabajo.Controls.Add(this.gbDatos);
            this.tpOrdenesTrabajo.Location = new System.Drawing.Point(4, 22);
            this.tpOrdenesTrabajo.Name = "tpOrdenesTrabajo";
            this.tpOrdenesTrabajo.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrdenesTrabajo.Size = new System.Drawing.Size(780, 490);
            this.tpOrdenesTrabajo.TabIndex = 1;
            this.tpOrdenesTrabajo.Text = "Órdenes de Trabajo";
            this.tpOrdenesTrabajo.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnImprimir);
            this.groupBox4.Location = new System.Drawing.Point(654, 417);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(120, 67);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.AutoSize = true;
            this.btnImprimir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImprimir.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Printer_25;
            this.btnImprimir.Location = new System.Drawing.Point(11, 13);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(99, 48);
            this.btnImprimir.TabIndex = 0;
            this.btnImprimir.Text = "Imprimir Órdenes";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnFiltrarOT);
            this.groupBox3.Controls.Add(this.dtpFechaInicioOTFiltrar);
            this.groupBox3.Controls.Add(this.cboEstadoOTFiltrar);
            this.groupBox3.Controls.Add(this.txtCodigoOTFiltrar);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(6, 417);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(642, 68);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtrar órdenes";
            // 
            // btnFiltrarOT
            // 
            this.btnFiltrarOT.Location = new System.Drawing.Point(558, 26);
            this.btnFiltrarOT.Name = "btnFiltrarOT";
            this.btnFiltrarOT.Size = new System.Drawing.Size(75, 25);
            this.btnFiltrarOT.TabIndex = 13;
            this.btnFiltrarOT.Text = "Filtrar";
            this.btnFiltrarOT.UseVisualStyleBackColor = true;
            this.btnFiltrarOT.Click += new System.EventHandler(this.btnFiltrarOT_Click);
            // 
            // txtCodigoOTFiltrar
            // 
            this.txtCodigoOTFiltrar.Location = new System.Drawing.Point(53, 29);
            this.txtCodigoOTFiltrar.Name = "txtCodigoOTFiltrar";
            this.txtCodigoOTFiltrar.Size = new System.Drawing.Size(122, 21);
            this.txtCodigoOTFiltrar.TabIndex = 10;
            this.txtCodigoOTFiltrar.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(370, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Fecha inicio:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(181, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Estado:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Código:";
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.dgvOrdenesTrabajo);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(774, 408);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Listado de Órdenes de Trabajo";
            // 
            // dgvOrdenesTrabajo
            // 
            this.dgvOrdenesTrabajo.AllowUserToAddRows = false;
            this.dgvOrdenesTrabajo.AllowUserToDeleteRows = false;
            this.dgvOrdenesTrabajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenesTrabajo.Location = new System.Drawing.Point(3, 17);
            this.dgvOrdenesTrabajo.MultiSelect = false;
            this.dgvOrdenesTrabajo.Name = "dgvOrdenesTrabajo";
            this.dgvOrdenesTrabajo.RowHeadersVisible = false;
            this.dgvOrdenesTrabajo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenesTrabajo.Size = new System.Drawing.Size(768, 388);
            this.dgvOrdenesTrabajo.TabIndex = 9;
            this.dgvOrdenesTrabajo.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenesTrabajo_RowEnter);
            this.dgvOrdenesTrabajo.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrdenesTrabajo_ColumnHeaderMouseClick);
            this.dgvOrdenesTrabajo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrdenesTrabajo_CellFormatting);
            this.dgvOrdenesTrabajo.SelectionChanged += new System.EventHandler(this.dgvOrdenesTrabajo_SelectionChanged);
            // 
            // tpCierreParcial
            // 
            this.tpCierreParcial.Controls.Add(this.gbAgregarCierreParcial);
            this.tpCierreParcial.Controls.Add(this.groupBox1);
            this.tpCierreParcial.Location = new System.Drawing.Point(4, 22);
            this.tpCierreParcial.Name = "tpCierreParcial";
            this.tpCierreParcial.Size = new System.Drawing.Size(780, 490);
            this.tpCierreParcial.TabIndex = 2;
            this.tpCierreParcial.Text = "Cierres Parciales";
            this.tpCierreParcial.UseVisualStyleBackColor = true;
            // 
            // gbAgregarCierreParcial
            // 
            this.gbAgregarCierreParcial.Controls.Add(this.nudOperacionesFallidas);
            this.gbAgregarCierreParcial.Controls.Add(this.btnCancelarCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.btnGuardarCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.txtObservacionesCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.nudCantidadCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.cboMaquinaCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.cboEmpleadoCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.dtpFechaCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.label12);
            this.gbAgregarCierreParcial.Controls.Add(this.label11);
            this.gbAgregarCierreParcial.Controls.Add(this.label9);
            this.gbAgregarCierreParcial.Controls.Add(this.label6);
            this.gbAgregarCierreParcial.Controls.Add(this.label4);
            this.gbAgregarCierreParcial.Controls.Add(this.label3);
            this.gbAgregarCierreParcial.Location = new System.Drawing.Point(5, 294);
            this.gbAgregarCierreParcial.Name = "gbAgregarCierreParcial";
            this.gbAgregarCierreParcial.Size = new System.Drawing.Size(770, 191);
            this.gbAgregarCierreParcial.TabIndex = 3;
            this.gbAgregarCierreParcial.TabStop = false;
            this.gbAgregarCierreParcial.Text = "Agregar cierre parcial";
            // 
            // btnCancelarCierre
            // 
            this.btnCancelarCierre.Location = new System.Drawing.Point(672, 117);
            this.btnCancelarCierre.Name = "btnCancelarCierre";
            this.btnCancelarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnCancelarCierre.TabIndex = 25;
            this.btnCancelarCierre.Text = "Cancelar";
            this.btnCancelarCierre.UseVisualStyleBackColor = true;
            this.btnCancelarCierre.Click += new System.EventHandler(this.btnCancelarCierre_Click);
            // 
            // btnGuardarCierre
            // 
            this.btnGuardarCierre.Location = new System.Drawing.Point(672, 71);
            this.btnGuardarCierre.Name = "btnGuardarCierre";
            this.btnGuardarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnGuardarCierre.TabIndex = 24;
            this.btnGuardarCierre.Text = "Guardar";
            this.btnGuardarCierre.UseVisualStyleBackColor = true;
            this.btnGuardarCierre.Click += new System.EventHandler(this.btnGuardarCierre_Click);
            // 
            // txtObservacionesCierre
            // 
            this.txtObservacionesCierre.Location = new System.Drawing.Point(347, 46);
            this.txtObservacionesCierre.MaxLength = 300;
            this.txtObservacionesCierre.Name = "txtObservacionesCierre";
            this.txtObservacionesCierre.Size = new System.Drawing.Size(281, 129);
            this.txtObservacionesCierre.TabIndex = 23;
            this.txtObservacionesCierre.Text = "";
            this.txtObservacionesCierre.Enter += new System.EventHandler(this.control_Enter);
            // 
            // nudCantidadCierre
            // 
            this.nudCantidadCierre.Location = new System.Drawing.Point(84, 83);
            this.nudCantidadCierre.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudCantidadCierre.Name = "nudCantidadCierre";
            this.nudCantidadCierre.Size = new System.Drawing.Size(200, 21);
            this.nudCantidadCierre.TabIndex = 20;
            this.nudCantidadCierre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCantidadCierre.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(344, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Observaciones";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Operaciones fallidas:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Fecha:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Cantidad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Máquina:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Empleado:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEliminarCierre);
            this.groupBox1.Controls.Add(this.btnModificarCierre);
            this.groupBox1.Controls.Add(this.dgvCierresParciales);
            this.groupBox1.Controls.Add(this.btnAgregarCierre);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(770, 285);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Cierres Parciales";
            // 
            // btnEliminarCierre
            // 
            this.btnEliminarCierre.Location = new System.Drawing.Point(689, 254);
            this.btnEliminarCierre.Name = "btnEliminarCierre";
            this.btnEliminarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnEliminarCierre.TabIndex = 17;
            this.btnEliminarCierre.Text = "Eliminar";
            this.btnEliminarCierre.UseVisualStyleBackColor = true;
            this.btnEliminarCierre.Click += new System.EventHandler(this.btnEliminarCierre_Click);
            // 
            // btnModificarCierre
            // 
            this.btnModificarCierre.Location = new System.Drawing.Point(608, 254);
            this.btnModificarCierre.Name = "btnModificarCierre";
            this.btnModificarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnModificarCierre.TabIndex = 16;
            this.btnModificarCierre.Text = "Modificar";
            this.btnModificarCierre.UseVisualStyleBackColor = true;
            this.btnModificarCierre.Click += new System.EventHandler(this.btnModificarCierre_Click);
            // 
            // dgvCierresParciales
            // 
            this.dgvCierresParciales.AllowUserToAddRows = false;
            this.dgvCierresParciales.AllowUserToDeleteRows = false;
            this.dgvCierresParciales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCierresParciales.Location = new System.Drawing.Point(6, 20);
            this.dgvCierresParciales.MultiSelect = false;
            this.dgvCierresParciales.Name = "dgvCierresParciales";
            this.dgvCierresParciales.ReadOnly = true;
            this.dgvCierresParciales.RowHeadersVisible = false;
            this.dgvCierresParciales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCierresParciales.Size = new System.Drawing.Size(758, 228);
            this.dgvCierresParciales.TabIndex = 14;
            this.dgvCierresParciales.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCierresParciales_RowEnter);
            this.dgvCierresParciales.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCierresParciales_ColumnHeaderMouseClick);
            this.dgvCierresParciales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCierresParciales_CellFormatting);
            this.dgvCierresParciales.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
            // 
            // btnAgregarCierre
            // 
            this.btnAgregarCierre.Location = new System.Drawing.Point(527, 254);
            this.btnAgregarCierre.Name = "btnAgregarCierre";
            this.btnAgregarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnAgregarCierre.TabIndex = 15;
            this.btnAgregarCierre.Text = "Agregar";
            this.btnAgregarCierre.UseVisualStyleBackColor = true;
            this.btnAgregarCierre.Click += new System.EventHandler(this.btnAgregarCierre_Click);
            // 
            // tsMenu
            // 
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIniciar,
            this.btnFinalizar,
            this.btnCancelar,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(794, 50);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Iniciar_25;
            this.btnIniciar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(40, 47);
            this.btnIniciar.Text = "&Iniciar";
            this.btnIniciar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Finalizar_25;
            this.btnFinalizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFinalizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(50, 47);
            this.btnFinalizar.Text = "&Finalizar";
            this.btnFinalizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Cancel_25;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(53, 47);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 47);
            this.btnEliminar.Text = "&Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // cmsProduccion
            // 
            this.cmsProduccion.AllowMerge = false;
            this.cmsProduccion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBloquearProduccion,
            this.tsmiDesbloquearProduccion});
            this.cmsProduccion.Name = "cmsProduccion";
            this.cmsProduccion.Size = new System.Drawing.Size(188, 48);
            // 
            // tsmiBloquearProduccion
            // 
            this.tsmiBloquearProduccion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiBloquearProduccion.Name = "tsmiBloquearProduccion";
            this.tsmiBloquearProduccion.Size = new System.Drawing.Size(187, 22);
            this.tsmiBloquearProduccion.Text = "Bloquear columna";
            this.tsmiBloquearProduccion.Click += new System.EventHandler(this.tsmiBloquearProduccion_Click);
            // 
            // tsmiDesbloquearProduccion
            // 
            this.tsmiDesbloquearProduccion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiDesbloquearProduccion.Name = "tsmiDesbloquearProduccion";
            this.tsmiDesbloquearProduccion.Size = new System.Drawing.Size(187, 22);
            this.tsmiDesbloquearProduccion.Text = "Desbloquear columna";
            this.tsmiDesbloquearProduccion.Click += new System.EventHandler(this.tsmiDesbloquearProduccion_Click);
            // 
            // cmsTrabajo
            // 
            this.cmsTrabajo.AllowMerge = false;
            this.cmsTrabajo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBloquearTrabajo,
            this.tsmiDesbloquearTrabajo});
            this.cmsTrabajo.Name = "cmsTrabajo";
            this.cmsTrabajo.Size = new System.Drawing.Size(188, 48);
            // 
            // tsmiBloquearTrabajo
            // 
            this.tsmiBloquearTrabajo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiBloquearTrabajo.Name = "tsmiBloquearTrabajo";
            this.tsmiBloquearTrabajo.Size = new System.Drawing.Size(187, 22);
            this.tsmiBloquearTrabajo.Text = "Bloquear columna";
            this.tsmiBloquearTrabajo.Click += new System.EventHandler(this.tsmiBloquearTrabajo_Click);
            // 
            // tsmiDesbloquearTrabajo
            // 
            this.tsmiDesbloquearTrabajo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiDesbloquearTrabajo.Name = "tsmiDesbloquearTrabajo";
            this.tsmiDesbloquearTrabajo.Size = new System.Drawing.Size(187, 22);
            this.tsmiDesbloquearTrabajo.Text = "Desbloquear columna";
            this.tsmiDesbloquearTrabajo.Click += new System.EventHandler(this.tsmiDesbloquearTrabajo_Click);
            // 
            // cmsCierres
            // 
            this.cmsCierres.AllowMerge = false;
            this.cmsCierres.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBloquearCierre,
            this.tsmiDesbloquearCierre});
            this.cmsCierres.Name = "cmsCierres";
            this.cmsCierres.Size = new System.Drawing.Size(188, 48);
            // 
            // tsmiBloquearCierre
            // 
            this.tsmiBloquearCierre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiBloquearCierre.Name = "tsmiBloquearCierre";
            this.tsmiBloquearCierre.Size = new System.Drawing.Size(187, 22);
            this.tsmiBloquearCierre.Text = "Bloquear columna";
            this.tsmiBloquearCierre.Click += new System.EventHandler(this.tsmiBloquearCierre_Click);
            // 
            // tsmiDesbloquearCierre
            // 
            this.tsmiDesbloquearCierre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiDesbloquearCierre.Name = "tsmiDesbloquearCierre";
            this.tsmiDesbloquearCierre.Size = new System.Drawing.Size(187, 22);
            this.tsmiDesbloquearCierre.Text = "Desbloquear columna";
            this.tsmiDesbloquearCierre.Click += new System.EventHandler(this.tsmiDesbloquearCierre_Click);
            // 
            // nudOperacionesFallidas
            // 
            this.nudOperacionesFallidas.Location = new System.Drawing.Point(134, 156);
            this.nudOperacionesFallidas.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudOperacionesFallidas.Name = "nudOperacionesFallidas";
            this.nudOperacionesFallidas.Size = new System.Drawing.Size(150, 21);
            this.nudOperacionesFallidas.TabIndex = 26;
            this.nudOperacionesFallidas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpFechaHastaOPBuscar
            // 
            this.dtpFechaHastaOPBuscar.CustomFormat = " ";
            this.dtpFechaHastaOPBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHastaOPBuscar.Location = new System.Drawing.Point(405, 86);
            this.dtpFechaHastaOPBuscar.Name = "dtpFechaHastaOPBuscar";
            this.dtpFechaHastaOPBuscar.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaHastaOPBuscar.TabIndex = 6;
            // 
            // dtpFechaDesdeOPBuscar
            // 
            this.dtpFechaDesdeOPBuscar.CustomFormat = " ";
            this.dtpFechaDesdeOPBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDesdeOPBuscar.Location = new System.Drawing.Point(405, 55);
            this.dtpFechaDesdeOPBuscar.Name = "dtpFechaDesdeOPBuscar";
            this.dtpFechaDesdeOPBuscar.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaDesdeOPBuscar.TabIndex = 5;
            // 
            // dtpFechaGeneracionOPBuscar
            // 
            this.dtpFechaGeneracionOPBuscar.CustomFormat = " ";
            this.dtpFechaGeneracionOPBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaGeneracionOPBuscar.Location = new System.Drawing.Point(405, 24);
            this.dtpFechaGeneracionOPBuscar.Name = "dtpFechaGeneracionOPBuscar";
            this.dtpFechaGeneracionOPBuscar.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaGeneracionOPBuscar.TabIndex = 4;
            // 
            // cboModoOPBuscar
            // 
            this.cboModoOPBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModoOPBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboModoOPBuscar.FormattingEnabled = true;
            this.cboModoOPBuscar.Location = new System.Drawing.Point(72, 87);
            this.cboModoOPBuscar.Name = "cboModoOPBuscar";
            this.cboModoOPBuscar.Size = new System.Drawing.Size(193, 21);
            this.cboModoOPBuscar.TabIndex = 3;
            // 
            // cboEstadoOPBuscar
            // 
            this.cboEstadoOPBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoOPBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstadoOPBuscar.FormattingEnabled = true;
            this.cboEstadoOPBuscar.Location = new System.Drawing.Point(72, 56);
            this.cboEstadoOPBuscar.Name = "cboEstadoOPBuscar";
            this.cboEstadoOPBuscar.Size = new System.Drawing.Size(193, 21);
            this.cboEstadoOPBuscar.TabIndex = 2;
            // 
            // dtpFechaInicioOTFiltrar
            // 
            this.dtpFechaInicioOTFiltrar.CustomFormat = " ";
            this.dtpFechaInicioOTFiltrar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicioOTFiltrar.Location = new System.Drawing.Point(442, 28);
            this.dtpFechaInicioOTFiltrar.Name = "dtpFechaInicioOTFiltrar";
            this.dtpFechaInicioOTFiltrar.Size = new System.Drawing.Size(110, 21);
            this.dtpFechaInicioOTFiltrar.TabIndex = 12;
            // 
            // cboEstadoOTFiltrar
            // 
            this.cboEstadoOTFiltrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoOTFiltrar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstadoOTFiltrar.FormattingEnabled = true;
            this.cboEstadoOTFiltrar.Location = new System.Drawing.Point(231, 29);
            this.cboEstadoOTFiltrar.Name = "cboEstadoOTFiltrar";
            this.cboEstadoOTFiltrar.Size = new System.Drawing.Size(133, 21);
            this.cboEstadoOTFiltrar.TabIndex = 11;
            // 
            // cboMaquinaCierre
            // 
            this.cboMaquinaCierre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaquinaCierre.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboMaquinaCierre.FormattingEnabled = true;
            this.cboMaquinaCierre.Location = new System.Drawing.Point(84, 55);
            this.cboMaquinaCierre.Name = "cboMaquinaCierre";
            this.cboMaquinaCierre.Size = new System.Drawing.Size(200, 21);
            this.cboMaquinaCierre.TabIndex = 19;
            // 
            // cboEmpleadoCierre
            // 
            this.cboEmpleadoCierre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpleadoCierre.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEmpleadoCierre.FormattingEnabled = true;
            this.cboEmpleadoCierre.Location = new System.Drawing.Point(84, 27);
            this.cboEmpleadoCierre.Name = "cboEmpleadoCierre";
            this.cboEmpleadoCierre.Size = new System.Drawing.Size(200, 21);
            this.cboEmpleadoCierre.TabIndex = 18;
            // 
            // dtpFechaCierre
            // 
            this.dtpFechaCierre.CustomFormat = " ";
            this.dtpFechaCierre.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCierre.Location = new System.Drawing.Point(84, 111);
            this.dtpFechaCierre.Name = "dtpFechaCierre";
            this.dtpFechaCierre.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaCierre.TabIndex = 21;
            // 
            // frmControlProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmControlProduccion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Control de Órdenes de Producción";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcOrdenTrabajo.ResumeLayout(false);
            this.tpOrdenesProduccion.ResumeLayout(false);
            this.gbBuscarOtros.ResumeLayout(false);
            this.gbBuscarOtros.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesProduccion)).EndInit();
            this.tpOrdenesTrabajo.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesTrabajo)).EndInit();
            this.tpCierreParcial.ResumeLayout(false);
            this.gbAgregarCierreParcial.ResumeLayout(false);
            this.gbAgregarCierreParcial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadCierre)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCierresParciales)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.cmsProduccion.ResumeLayout(false);
            this.cmsTrabajo.ResumeLayout(false);
            this.cmsCierres.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudOperacionesFallidas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcOrdenTrabajo;
        private System.Windows.Forms.TabPage tpOrdenesProduccion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvOrdenesProduccion;
        private System.Windows.Forms.TabPage tpOrdenesTrabajo;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnFinalizar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnIniciar;
        private System.Windows.Forms.GroupBox gbBuscarOtros;
        private System.Windows.Forms.Button btnBuscarOP;
        private System.Windows.Forms.TextBox txtCodigoOPBuscar;
        private System.Windows.Forms.Label label10;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboModoOPBuscar;
        private System.Windows.Forms.Label label8;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstadoOPBuscar;
        private System.Windows.Forms.Label label7;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaHastaOPBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaDesdeOPBuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaGeneracionOPBuscar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.TabPage tpCierreParcial;
        private System.Windows.Forms.DataGridView dgvOrdenesTrabajo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCierresParciales;
        private System.Windows.Forms.GroupBox gbAgregarCierreParcial;
        private System.Windows.Forms.Button btnCancelarCierre;
        private System.Windows.Forms.Button btnGuardarCierre;
        private System.Windows.Forms.RichTextBox txtObservacionesCierre;
        private System.Windows.Forms.NumericUpDown nudCantidadCierre;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboMaquinaCierre;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEmpleadoCierre;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaCierre;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEliminarCierre;
        private System.Windows.Forms.Button btnModificarCierre;
        private System.Windows.Forms.Button btnAgregarCierre;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnFiltrarOT;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaInicioOTFiltrar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstadoOTFiltrar;
        private System.Windows.Forms.TextBox txtCodigoOTFiltrar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ContextMenuStrip cmsProduccion;
        private System.Windows.Forms.ContextMenuStrip cmsTrabajo;
        private System.Windows.Forms.ContextMenuStrip cmsCierres;
        private System.Windows.Forms.ToolStripMenuItem tsmiBloquearTrabajo;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesbloquearTrabajo;
        private System.Windows.Forms.ToolStripMenuItem tsmiBloquearCierre;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesbloquearCierre;
        private System.Windows.Forms.ToolStripMenuItem tsmiBloquearProduccion;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesbloquearProduccion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.NumericUpDown nudOperacionesFallidas;
    }
}