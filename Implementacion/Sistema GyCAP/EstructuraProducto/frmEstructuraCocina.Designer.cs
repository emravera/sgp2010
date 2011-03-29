namespace GyCAP.UI.EstructuraProducto
{
    partial class frmEstructuraCocina
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
            this.tcEstructuraProducto = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvEstructuras = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboActivoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpFechaAltaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cbResponsableBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlanoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbCocinaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbVer = new System.Windows.Forms.GroupBox();
            this.btnDiagramaEstructura = new System.Windows.Forms.Button();
            this.btnConjuntos = new System.Windows.Forms.Button();
            this.btnDatos = new System.Windows.Forms.Button();
            this.btnArbol = new System.Windows.Forms.Button();
            this.gbPartes = new System.Windows.Forms.GroupBox();
            this.dgvPartesEstructura = new System.Windows.Forms.DataGridView();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.chkFijo = new System.Windows.Forms.CheckBox();
            this.cbEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label17 = new System.Windows.Forms.Label();
            this.nudcosto = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpFechaModificacion = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaAlta = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cbResponsable = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlano = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbCocina = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbGuardarVolver = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.tpPartes = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnVolverDePartes = new System.Windows.Forms.Button();
            this.gbArbolEstructura = new System.Windows.Forms.GroupBox();
            this.panelAccionesArbol = new System.Windows.Forms.Panel();
            this.btnRestarParte = new System.Windows.Forms.Button();
            this.btnSumarParte = new System.Windows.Forms.Button();
            this.btnDeleteParte = new System.Windows.Forms.Button();
            this.tvEstructura = new System.Windows.Forms.TreeView();
            this.gbAgregarParteMP = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.nudCantidadAgregar = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarParte = new System.Windows.Forms.Button();
            this.tcPartesDisponibles = new System.Windows.Forms.TabControl();
            this.tpPartesDisponibles = new System.Windows.Forms.TabPage();
            this.cboFiltroTipoParte = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label18 = new System.Windows.Forms.Label();
            this.txtFiltroNombreParte = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dgvPartesDisponibles = new System.Windows.Forms.DataGridView();
            this.tpMPDisponibles = new System.Windows.Forms.TabPage();
            this.txtFiltroNombreMP = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dgvMPDisponibles = new System.Windows.Forms.DataGridView();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.cmsGrillaOrdenesProduccion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiBloquearColumna = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesbloquearColumna = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcEstructuraProducto.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstructuras)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbVer.SuspendLayout();
            this.gbPartes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartesEstructura)).BeginInit();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudcosto)).BeginInit();
            this.gbGuardarVolver.SuspendLayout();
            this.tpPartes.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbArbolEstructura.SuspendLayout();
            this.panelAccionesArbol.SuspendLayout();
            this.gbAgregarParteMP.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadAgregar)).BeginInit();
            this.tcPartesDisponibles.SuspendLayout();
            this.tpPartesDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartesDisponibles)).BeginInit();
            this.tpMPDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMPDisponibles)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.cmsGrillaOrdenesProduccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcEstructuraProducto, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 570);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tcEstructuraProducto
            // 
            this.tcEstructuraProducto.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcEstructuraProducto.Controls.Add(this.tpBuscar);
            this.tcEstructuraProducto.Controls.Add(this.tpDatos);
            this.tcEstructuraProducto.Controls.Add(this.tpPartes);
            this.tcEstructuraProducto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEstructuraProducto.ItemSize = new System.Drawing.Size(0, 1);
            this.tcEstructuraProducto.Location = new System.Drawing.Point(3, 53);
            this.tcEstructuraProducto.Multiline = true;
            this.tcEstructuraProducto.Name = "tcEstructuraProducto";
            this.tcEstructuraProducto.SelectedIndex = 0;
            this.tcEstructuraProducto.Size = new System.Drawing.Size(786, 514);
            this.tcEstructuraProducto.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcEstructuraProducto.TabIndex = 0;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(778, 505);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.Text = "Buscar";
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvEstructuras);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(772, 397);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Estructuras";
            // 
            // dgvEstructuras
            // 
            this.dgvEstructuras.AllowUserToAddRows = false;
            this.dgvEstructuras.AllowUserToDeleteRows = false;
            this.dgvEstructuras.AllowUserToResizeRows = false;
            this.dgvEstructuras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstructuras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEstructuras.Location = new System.Drawing.Point(3, 16);
            this.dgvEstructuras.MultiSelect = false;
            this.dgvEstructuras.Name = "dgvEstructuras";
            this.dgvEstructuras.ReadOnly = true;
            this.dgvEstructuras.RowHeadersVisible = false;
            this.dgvEstructuras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstructuras.Size = new System.Drawing.Size(766, 378);
            this.dgvEstructuras.TabIndex = 8;
            this.dgvEstructuras.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstructuras_RowEnter);
            this.dgvEstructuras.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEstructuras_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboActivoBuscar);
            this.groupBox1.Controls.Add(this.dtpFechaAltaBuscar);
            this.groupBox1.Controls.Add(this.cbResponsableBuscar);
            this.groupBox1.Controls.Add(this.cbPlanoBuscar);
            this.groupBox1.Controls.Add(this.cbCocinaBuscar);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNombreBuscar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de Búsqueda";
            // 
            // cboActivoBuscar
            // 
            this.cboActivoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActivoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboActivoBuscar.FormattingEnabled = true;
            this.cboActivoBuscar.Location = new System.Drawing.Point(574, 20);
            this.cboActivoBuscar.Name = "cboActivoBuscar";
            this.cboActivoBuscar.Size = new System.Drawing.Size(95, 21);
            this.cboActivoBuscar.TabIndex = 13;
            // 
            // dtpFechaAltaBuscar
            // 
            this.dtpFechaAltaBuscar.CustomFormat = " ";
            this.dtpFechaAltaBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaAltaBuscar.Location = new System.Drawing.Point(574, 59);
            this.dtpFechaAltaBuscar.Name = "dtpFechaAltaBuscar";
            this.dtpFechaAltaBuscar.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaAltaBuscar.TabIndex = 4;
            // 
            // cbResponsableBuscar
            // 
            this.cbResponsableBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResponsableBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbResponsableBuscar.FormattingEnabled = true;
            this.cbResponsableBuscar.Location = new System.Drawing.Point(333, 60);
            this.cbResponsableBuscar.Name = "cbResponsableBuscar";
            this.cbResponsableBuscar.Size = new System.Drawing.Size(166, 21);
            this.cbResponsableBuscar.TabIndex = 12;
            // 
            // cbPlanoBuscar
            // 
            this.cbPlanoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanoBuscar.FormattingEnabled = true;
            this.cbPlanoBuscar.Location = new System.Drawing.Point(333, 20);
            this.cbPlanoBuscar.Name = "cbPlanoBuscar";
            this.cbPlanoBuscar.Size = new System.Drawing.Size(166, 21);
            this.cbPlanoBuscar.TabIndex = 2;
            // 
            // cbCocinaBuscar
            // 
            this.cbCocinaBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCocinaBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCocinaBuscar.FormattingEnabled = true;
            this.cbCocinaBuscar.Location = new System.Drawing.Point(67, 60);
            this.cbCocinaBuscar.Name = "cbCocinaBuscar";
            this.cbCocinaBuscar.Size = new System.Drawing.Size(166, 21);
            this.cbCocinaBuscar.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(517, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Activo:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(517, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 26);
            this.label12.TabIndex = 8;
            this.label12.Text = "Fecha de\r\ncreación:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 26);
            this.label8.TabIndex = 7;
            this.label8.Text = "Responsable\r\nde confección:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Plano:";
            // 
            // txtNombreBuscar
            // 
            this.txtNombreBuscar.Location = new System.Drawing.Point(67, 20);
            this.txtNombreBuscar.MaxLength = 80;
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(166, 20);
            this.txtNombreBuscar.TabIndex = 1;
            this.txtNombreBuscar.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nombre:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(684, 35);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 30);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cocina:";
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbVer);
            this.tpDatos.Controls.Add(this.gbPartes);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Controls.Add(this.gbGuardarVolver);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(778, 505);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.Text = "Datos";
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbVer
            // 
            this.gbVer.Controls.Add(this.btnDiagramaEstructura);
            this.gbVer.Controls.Add(this.btnConjuntos);
            this.gbVer.Controls.Add(this.btnDatos);
            this.gbVer.Controls.Add(this.btnArbol);
            this.gbVer.Location = new System.Drawing.Point(618, 177);
            this.gbVer.Name = "gbVer";
            this.gbVer.Size = new System.Drawing.Size(156, 274);
            this.gbVer.TabIndex = 10;
            this.gbVer.TabStop = false;
            this.gbVer.Text = "Ver";
            // 
            // btnDiagramaEstructura
            // 
            this.btnDiagramaEstructura.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDiagramaEstructura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiagramaEstructura.Location = new System.Drawing.Point(36, 158);
            this.btnDiagramaEstructura.Name = "btnDiagramaEstructura";
            this.btnDiagramaEstructura.Size = new System.Drawing.Size(92, 47);
            this.btnDiagramaEstructura.TabIndex = 11;
            this.btnDiagramaEstructura.Text = "Diagrama de\r\nEstructura";
            this.btnDiagramaEstructura.UseVisualStyleBackColor = true;
            // 
            // btnConjuntos
            // 
            this.btnConjuntos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConjuntos.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.derecha1_15;
            this.btnConjuntos.Location = new System.Drawing.Point(21, 62);
            this.btnConjuntos.Name = "btnConjuntos";
            this.btnConjuntos.Size = new System.Drawing.Size(115, 28);
            this.btnConjuntos.TabIndex = 4;
            this.btnConjuntos.Text = "Partes";
            this.btnConjuntos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnConjuntos.UseVisualStyleBackColor = true;
            this.btnConjuntos.Click += new System.EventHandler(this.btnPartes_Click);
            // 
            // btnDatos
            // 
            this.btnDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatos.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.arriba1_15;
            this.btnDatos.Location = new System.Drawing.Point(21, 28);
            this.btnDatos.Name = "btnDatos";
            this.btnDatos.Size = new System.Drawing.Size(115, 28);
            this.btnDatos.TabIndex = 10;
            this.btnDatos.Text = "Datos";
            this.btnDatos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDatos.UseVisualStyleBackColor = true;
            this.btnDatos.Click += new System.EventHandler(this.btnDatos_Click);
            // 
            // btnArbol
            // 
            this.btnArbol.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnArbol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArbol.Location = new System.Drawing.Point(36, 211);
            this.btnArbol.Name = "btnArbol";
            this.btnArbol.Size = new System.Drawing.Size(92, 47);
            this.btnArbol.TabIndex = 4;
            this.btnArbol.Text = "Árbol de\r\nEstructura";
            this.btnArbol.UseVisualStyleBackColor = true;
            // 
            // gbPartes
            // 
            this.gbPartes.Controls.Add(this.dgvPartesEstructura);
            this.gbPartes.Location = new System.Drawing.Point(3, 177);
            this.gbPartes.Name = "gbPartes";
            this.gbPartes.Size = new System.Drawing.Size(609, 325);
            this.gbPartes.TabIndex = 8;
            this.gbPartes.TabStop = false;
            this.gbPartes.Text = "Listado de partes";
            // 
            // dgvPartesEstructura
            // 
            this.dgvPartesEstructura.AllowUserToAddRows = false;
            this.dgvPartesEstructura.AllowUserToDeleteRows = false;
            this.dgvPartesEstructura.AllowUserToResizeRows = false;
            this.dgvPartesEstructura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartesEstructura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPartesEstructura.Location = new System.Drawing.Point(3, 16);
            this.dgvPartesEstructura.MultiSelect = false;
            this.dgvPartesEstructura.Name = "dgvPartesEstructura";
            this.dgvPartesEstructura.ReadOnly = true;
            this.dgvPartesEstructura.RowHeadersVisible = false;
            this.dgvPartesEstructura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartesEstructura.Size = new System.Drawing.Size(603, 306);
            this.dgvPartesEstructura.TabIndex = 0;
            this.dgvPartesEstructura.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPartesEstructura_CellFormatting);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.chkFijo);
            this.gbDatos.Controls.Add(this.cbEstado);
            this.gbDatos.Controls.Add(this.label17);
            this.gbDatos.Controls.Add(this.nudcosto);
            this.gbDatos.Controls.Add(this.label15);
            this.gbDatos.Controls.Add(this.dtpFechaModificacion);
            this.gbDatos.Controls.Add(this.dtpFechaAlta);
            this.gbDatos.Controls.Add(this.cbResponsable);
            this.gbDatos.Controls.Add(this.cbPlano);
            this.gbDatos.Controls.Add(this.cbCocina);
            this.gbDatos.Controls.Add(this.label11);
            this.gbDatos.Controls.Add(this.label10);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.label7);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.txtDescripcion);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Location = new System.Drawing.Point(3, 0);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(772, 175);
            this.gbDatos.TabIndex = 5;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la Estructura de Producto";
            // 
            // chkFijo
            // 
            this.chkFijo.AutoSize = true;
            this.chkFijo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFijo.Location = new System.Drawing.Point(719, 88);
            this.chkFijo.Name = "chkFijo";
            this.chkFijo.Size = new System.Drawing.Size(43, 17);
            this.chkFijo.TabIndex = 31;
            this.chkFijo.Text = "Fijo";
            this.chkFijo.UseVisualStyleBackColor = true;
            this.chkFijo.CheckedChanged += new System.EventHandler(this.chkFijo_CheckedChanged);
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(610, 114);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(152, 21);
            this.cbEstado.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(532, 117);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Estado:";
            // 
            // nudcosto
            // 
            this.nudcosto.DecimalPlaces = 2;
            this.nudcosto.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudcosto.Location = new System.Drawing.Point(610, 87);
            this.nudcosto.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudcosto.Name = "nudcosto";
            this.nudcosto.Size = new System.Drawing.Size(103, 20);
            this.nudcosto.TabIndex = 27;
            this.nudcosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(532, 89);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Costo total $:";
            // 
            // dtpFechaModificacion
            // 
            this.dtpFechaModificacion.CustomFormat = " ";
            this.dtpFechaModificacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaModificacion.Location = new System.Drawing.Point(419, 56);
            this.dtpFechaModificacion.Name = "dtpFechaModificacion";
            this.dtpFechaModificacion.Size = new System.Drawing.Size(107, 20);
            this.dtpFechaModificacion.TabIndex = 25;
            // 
            // dtpFechaAlta
            // 
            this.dtpFechaAlta.CustomFormat = " ";
            this.dtpFechaAlta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaAlta.Location = new System.Drawing.Point(419, 27);
            this.dtpFechaAlta.Name = "dtpFechaAlta";
            this.dtpFechaAlta.Size = new System.Drawing.Size(107, 20);
            this.dtpFechaAlta.TabIndex = 24;
            // 
            // cbResponsable
            // 
            this.cbResponsable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResponsable.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbResponsable.FormattingEnabled = true;
            this.cbResponsable.Location = new System.Drawing.Point(610, 57);
            this.cbResponsable.Name = "cbResponsable";
            this.cbResponsable.Size = new System.Drawing.Size(152, 21);
            this.cbResponsable.TabIndex = 23;
            // 
            // cbPlano
            // 
            this.cbPlano.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlano.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlano.FormattingEnabled = true;
            this.cbPlano.Location = new System.Drawing.Point(610, 28);
            this.cbPlano.Name = "cbPlano";
            this.cbPlano.Size = new System.Drawing.Size(152, 21);
            this.cbPlano.TabIndex = 22;
            // 
            // cbCocina
            // 
            this.cbCocina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCocina.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCocina.FormattingEnabled = true;
            this.cbCocina.Location = new System.Drawing.Point(76, 57);
            this.cbCocina.Name = "cbCocina";
            this.cbCocina.Size = new System.Drawing.Size(199, 21);
            this.cbCocina.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(532, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Responsable:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(281, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Fecha última modificación:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Fecha de Creación:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(532, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Plano:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cocina:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.AutoWordSelection = true;
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Location = new System.Drawing.Point(76, 87);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(450, 76);
            this.txtDescripcion.TabIndex = 4;
            this.txtDescripcion.Text = "";
            this.txtDescripcion.Enter += new System.EventHandler(this.control_Enter);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(76, 28);
            this.txtNombre.MaxLength = 80;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(199, 20);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // gbGuardarVolver
            // 
            this.gbGuardarVolver.Controls.Add(this.btnGuardar);
            this.gbGuardarVolver.Controls.Add(this.btnVolver);
            this.gbGuardarVolver.Location = new System.Drawing.Point(618, 452);
            this.gbGuardarVolver.Name = "gbGuardarVolver";
            this.gbGuardarVolver.Size = new System.Drawing.Size(159, 50);
            this.gbGuardarVolver.TabIndex = 11;
            this.gbGuardarVolver.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(13, 14);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 30);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(83, 14);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 30);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // tpPartes
            // 
            this.tpPartes.Controls.Add(this.groupBox5);
            this.tpPartes.Controls.Add(this.gbArbolEstructura);
            this.tpPartes.Controls.Add(this.gbAgregarParteMP);
            this.tpPartes.Location = new System.Drawing.Point(4, 5);
            this.tpPartes.Name = "tpPartes";
            this.tpPartes.Size = new System.Drawing.Size(778, 505);
            this.tpPartes.TabIndex = 2;
            this.tpPartes.Text = "tabPage1";
            this.tpPartes.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnVolverDePartes);
            this.groupBox5.Location = new System.Drawing.Point(444, 438);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(329, 56);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // btnVolverDePartes
            // 
            this.btnVolverDePartes.Location = new System.Drawing.Point(248, 17);
            this.btnVolverDePartes.Name = "btnVolverDePartes";
            this.btnVolverDePartes.Size = new System.Drawing.Size(64, 30);
            this.btnVolverDePartes.TabIndex = 2;
            this.btnVolverDePartes.Text = "Volver";
            this.btnVolverDePartes.UseVisualStyleBackColor = true;
            this.btnVolverDePartes.Click += new System.EventHandler(this.btnVolverDePartes_Click);
            // 
            // gbArbolEstructura
            // 
            this.gbArbolEstructura.Controls.Add(this.panelAccionesArbol);
            this.gbArbolEstructura.Controls.Add(this.tvEstructura);
            this.gbArbolEstructura.Location = new System.Drawing.Point(444, 3);
            this.gbArbolEstructura.Name = "gbArbolEstructura";
            this.gbArbolEstructura.Size = new System.Drawing.Size(329, 434);
            this.gbArbolEstructura.TabIndex = 1;
            this.gbArbolEstructura.TabStop = false;
            // 
            // panelAccionesArbol
            // 
            this.panelAccionesArbol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAccionesArbol.Controls.Add(this.btnRestarParte);
            this.panelAccionesArbol.Controls.Add(this.btnSumarParte);
            this.panelAccionesArbol.Controls.Add(this.btnDeleteParte);
            this.panelAccionesArbol.Location = new System.Drawing.Point(57, 387);
            this.panelAccionesArbol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelAccionesArbol.Name = "panelAccionesArbol";
            this.panelAccionesArbol.Size = new System.Drawing.Size(211, 43);
            this.panelAccionesArbol.TabIndex = 12;
            // 
            // btnRestarParte
            // 
            this.btnRestarParte.AutoSize = true;
            this.btnRestarParte.FlatAppearance.BorderSize = 0;
            this.btnRestarParte.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnRestarParte.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestarParte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestarParte.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Restar_Gris_25;
            this.btnRestarParte.Location = new System.Drawing.Point(134, -3);
            this.btnRestarParte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRestarParte.Name = "btnRestarParte";
            this.btnRestarParte.Size = new System.Drawing.Size(60, 48);
            this.btnRestarParte.TabIndex = 32;
            this.btnRestarParte.Text = "Cantidad";
            this.btnRestarParte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRestarParte.UseVisualStyleBackColor = true;
            this.btnRestarParte.Click += new System.EventHandler(this.btnRestarParte_Click);
            this.btnRestarParte.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnRestarParte.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnSumarParte
            // 
            this.btnSumarParte.AutoSize = true;
            this.btnSumarParte.FlatAppearance.BorderSize = 0;
            this.btnSumarParte.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSumarParte.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSumarParte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumarParte.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Sumar_Gris_25;
            this.btnSumarParte.Location = new System.Drawing.Point(54, -3);
            this.btnSumarParte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSumarParte.Name = "btnSumarParte";
            this.btnSumarParte.Size = new System.Drawing.Size(85, 48);
            this.btnSumarParte.TabIndex = 31;
            this.btnSumarParte.Text = "Cantidad";
            this.btnSumarParte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSumarParte.UseVisualStyleBackColor = true;
            this.btnSumarParte.Click += new System.EventHandler(this.btnSumarParte_Click);
            this.btnSumarParte.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnSumarParte.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnDeleteParte
            // 
            this.btnDeleteParte.AutoSize = true;
            this.btnDeleteParte.FlatAppearance.BorderSize = 0;
            this.btnDeleteParte.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteParte.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteParte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteParte.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Delete_25;
            this.btnDeleteParte.Location = new System.Drawing.Point(9, -3);
            this.btnDeleteParte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteParte.Name = "btnDeleteParte";
            this.btnDeleteParte.Size = new System.Drawing.Size(53, 48);
            this.btnDeleteParte.TabIndex = 30;
            this.btnDeleteParte.Text = "Eliminar";
            this.btnDeleteParte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteParte.UseVisualStyleBackColor = true;
            this.btnDeleteParte.Click += new System.EventHandler(this.btnDeleteParte_Click);
            this.btnDeleteParte.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDeleteParte.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // tvEstructura
            // 
            this.tvEstructura.AllowDrop = true;
            this.tvEstructura.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvEstructura.HideSelection = false;
            this.tvEstructura.Location = new System.Drawing.Point(3, 16);
            this.tvEstructura.Name = "tvEstructura";
            this.tvEstructura.Size = new System.Drawing.Size(323, 366);
            this.tvEstructura.TabIndex = 0;
            // 
            // gbAgregarParteMP
            // 
            this.gbAgregarParteMP.Controls.Add(this.panel1);
            this.gbAgregarParteMP.Controls.Add(this.tcPartesDisponibles);
            this.gbAgregarParteMP.Location = new System.Drawing.Point(5, 3);
            this.gbAgregarParteMP.Name = "gbAgregarParteMP";
            this.gbAgregarParteMP.Size = new System.Drawing.Size(433, 491);
            this.gbAgregarParteMP.TabIndex = 0;
            this.gbAgregarParteMP.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.nudCantidadAgregar);
            this.panel1.Controls.Add(this.btnAgregarParte);
            this.panel1.Location = new System.Drawing.Point(42, 443);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 43);
            this.panel1.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Cantidad:";
            // 
            // nudCantidadAgregar
            // 
            this.nudCantidadAgregar.DecimalPlaces = 3;
            this.nudCantidadAgregar.Location = new System.Drawing.Point(79, 11);
            this.nudCantidadAgregar.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCantidadAgregar.Name = "nudCantidadAgregar";
            this.nudCantidadAgregar.Size = new System.Drawing.Size(120, 20);
            this.nudCantidadAgregar.TabIndex = 1;
            this.nudCantidadAgregar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAgregarParte
            // 
            this.btnAgregarParte.Location = new System.Drawing.Point(226, 10);
            this.btnAgregarParte.Name = "btnAgregarParte";
            this.btnAgregarParte.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarParte.TabIndex = 3;
            this.btnAgregarParte.Text = "Agregar";
            this.btnAgregarParte.UseVisualStyleBackColor = true;
            this.btnAgregarParte.Click += new System.EventHandler(this.btnAgregarParte_Click);
            // 
            // tcPartesDisponibles
            // 
            this.tcPartesDisponibles.Controls.Add(this.tpPartesDisponibles);
            this.tcPartesDisponibles.Controls.Add(this.tpMPDisponibles);
            this.tcPartesDisponibles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcPartesDisponibles.Location = new System.Drawing.Point(3, 16);
            this.tcPartesDisponibles.Name = "tcPartesDisponibles";
            this.tcPartesDisponibles.SelectedIndex = 0;
            this.tcPartesDisponibles.Size = new System.Drawing.Size(427, 413);
            this.tcPartesDisponibles.TabIndex = 4;
            // 
            // tpPartesDisponibles
            // 
            this.tpPartesDisponibles.Controls.Add(this.cboFiltroTipoParte);
            this.tpPartesDisponibles.Controls.Add(this.label18);
            this.tpPartesDisponibles.Controls.Add(this.txtFiltroNombreParte);
            this.tpPartesDisponibles.Controls.Add(this.label16);
            this.tpPartesDisponibles.Controls.Add(this.dgvPartesDisponibles);
            this.tpPartesDisponibles.Location = new System.Drawing.Point(4, 22);
            this.tpPartesDisponibles.Name = "tpPartesDisponibles";
            this.tpPartesDisponibles.Padding = new System.Windows.Forms.Padding(3);
            this.tpPartesDisponibles.Size = new System.Drawing.Size(419, 387);
            this.tpPartesDisponibles.TabIndex = 0;
            this.tpPartesDisponibles.Text = "Partes";
            this.tpPartesDisponibles.UseVisualStyleBackColor = true;
            // 
            // cboFiltroTipoParte
            // 
            this.cboFiltroTipoParte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroTipoParte.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboFiltroTipoParte.FormattingEnabled = true;
            this.cboFiltroTipoParte.Location = new System.Drawing.Point(266, 361);
            this.cboFiltroTipoParte.Name = "cboFiltroTipoParte";
            this.cboFiltroTipoParte.Size = new System.Drawing.Size(147, 21);
            this.cboFiltroTipoParte.TabIndex = 4;
            this.cboFiltroTipoParte.SelectionChangeCommitted += new System.EventHandler(this.cboFiltroTipoParte_SelectionChangeCommitted);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(229, 364);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Tipo:";
            // 
            // txtFiltroNombreParte
            // 
            this.txtFiltroNombreParte.Location = new System.Drawing.Point(59, 361);
            this.txtFiltroNombreParte.Name = "txtFiltroNombreParte";
            this.txtFiltroNombreParte.Size = new System.Drawing.Size(160, 20);
            this.txtFiltroNombreParte.TabIndex = 2;
            this.txtFiltroNombreParte.TextChanged += new System.EventHandler(this.txtFiltroNombreParte_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 364);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Nombre:";
            // 
            // dgvPartesDisponibles
            // 
            this.dgvPartesDisponibles.AllowUserToAddRows = false;
            this.dgvPartesDisponibles.AllowUserToDeleteRows = false;
            this.dgvPartesDisponibles.AllowUserToOrderColumns = true;
            this.dgvPartesDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartesDisponibles.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPartesDisponibles.Location = new System.Drawing.Point(3, 3);
            this.dgvPartesDisponibles.MultiSelect = false;
            this.dgvPartesDisponibles.Name = "dgvPartesDisponibles";
            this.dgvPartesDisponibles.ReadOnly = true;
            this.dgvPartesDisponibles.RowHeadersVisible = false;
            this.dgvPartesDisponibles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartesDisponibles.Size = new System.Drawing.Size(413, 352);
            this.dgvPartesDisponibles.TabIndex = 0;
            this.dgvPartesDisponibles.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPartesDisponibles_ColumnHeaderMouseClick);
            this.dgvPartesDisponibles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPartesDisponibles_CellFormatting);
            // 
            // tpMPDisponibles
            // 
            this.tpMPDisponibles.Controls.Add(this.txtFiltroNombreMP);
            this.tpMPDisponibles.Controls.Add(this.label19);
            this.tpMPDisponibles.Controls.Add(this.dgvMPDisponibles);
            this.tpMPDisponibles.Location = new System.Drawing.Point(4, 22);
            this.tpMPDisponibles.Name = "tpMPDisponibles";
            this.tpMPDisponibles.Padding = new System.Windows.Forms.Padding(3);
            this.tpMPDisponibles.Size = new System.Drawing.Size(419, 387);
            this.tpMPDisponibles.TabIndex = 1;
            this.tpMPDisponibles.Text = "Materia Prima";
            this.tpMPDisponibles.UseVisualStyleBackColor = true;
            // 
            // txtFiltroNombreMP
            // 
            this.txtFiltroNombreMP.Location = new System.Drawing.Point(59, 361);
            this.txtFiltroNombreMP.Name = "txtFiltroNombreMP";
            this.txtFiltroNombreMP.Size = new System.Drawing.Size(160, 20);
            this.txtFiltroNombreMP.TabIndex = 4;
            this.txtFiltroNombreMP.TextChanged += new System.EventHandler(this.txtFiltroNombreMP_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 364);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(48, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Nombre:";
            // 
            // dgvMPDisponibles
            // 
            this.dgvMPDisponibles.AllowUserToAddRows = false;
            this.dgvMPDisponibles.AllowUserToDeleteRows = false;
            this.dgvMPDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMPDisponibles.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvMPDisponibles.Location = new System.Drawing.Point(3, 3);
            this.dgvMPDisponibles.MultiSelect = false;
            this.dgvMPDisponibles.Name = "dgvMPDisponibles";
            this.dgvMPDisponibles.ReadOnly = true;
            this.dgvMPDisponibles.RowHeadersVisible = false;
            this.dgvMPDisponibles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMPDisponibles.Size = new System.Drawing.Size(413, 352);
            this.dgvMPDisponibles.TabIndex = 0;
            this.dgvMPDisponibles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMPDisponibles_CellFormatting);
            // 
            // tsMenu
            // 
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnConsultar,
            this.btnModificar,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(792, 50);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.New_25;
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
            this.btnConsultar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Find_25;
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
            this.btnModificar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Text_Editor_25;
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
            this.btnEliminar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Delete_25;
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
            this.btnSalir.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // cmsGrillaOrdenesProduccion
            // 
            this.cmsGrillaOrdenesProduccion.AllowMerge = false;
            this.cmsGrillaOrdenesProduccion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBloquearColumna,
            this.tsmiDesbloquearColumna});
            this.cmsGrillaOrdenesProduccion.Name = "cmsGrillaOrdenesTrabajo";
            this.cmsGrillaOrdenesProduccion.Size = new System.Drawing.Size(188, 48);
            // 
            // tsmiBloquearColumna
            // 
            this.tsmiBloquearColumna.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiBloquearColumna.Name = "tsmiBloquearColumna";
            this.tsmiBloquearColumna.Size = new System.Drawing.Size(187, 22);
            this.tsmiBloquearColumna.Text = "Bloquear columna";
            this.tsmiBloquearColumna.Click += new System.EventHandler(this.tsmiBloquearColumna_Click);
            // 
            // tsmiDesbloquearColumna
            // 
            this.tsmiDesbloquearColumna.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiDesbloquearColumna.Name = "tsmiDesbloquearColumna";
            this.tsmiDesbloquearColumna.Size = new System.Drawing.Size(187, 22);
            this.tsmiDesbloquearColumna.Text = "Desbloquear columna";
            this.tsmiDesbloquearColumna.Click += new System.EventHandler(this.tsmiDesbloquearColumna_Click);
            // 
            // frmEstructuraCocina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmEstructuraCocina";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Estructura de Producto";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcEstructuraProducto.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstructuras)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbVer.ResumeLayout(false);
            this.gbPartes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartesEstructura)).EndInit();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudcosto)).EndInit();
            this.gbGuardarVolver.ResumeLayout(false);
            this.tpPartes.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.gbArbolEstructura.ResumeLayout(false);
            this.panelAccionesArbol.ResumeLayout(false);
            this.panelAccionesArbol.PerformLayout();
            this.gbAgregarParteMP.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadAgregar)).EndInit();
            this.tcPartesDisponibles.ResumeLayout(false);
            this.tpPartesDisponibles.ResumeLayout(false);
            this.tpPartesDisponibles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartesDisponibles)).EndInit();
            this.tpMPDisponibles.ResumeLayout(false);
            this.tpMPDisponibles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMPDisponibles)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.cmsGrillaOrdenesProduccion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcEstructuraProducto;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvEstructuras;
        private System.Windows.Forms.GroupBox groupBox1;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaAltaBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbResponsableBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanoBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCocinaBuscar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.Button btnDatos;
        private System.Windows.Forms.Button btnConjuntos;
        private System.Windows.Forms.GroupBox gbVer;
        private System.Windows.Forms.Button btnArbol;
        private System.Windows.Forms.GroupBox gbPartes;
        private System.Windows.Forms.DataGridView dgvPartesEstructura;
        private System.Windows.Forms.GroupBox gbDatos;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaModificacion;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaAlta;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbResponsable;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlano;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCocina;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbGuardarVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nudcosto;
        private System.Windows.Forms.Label label15;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEstado;
        private System.Windows.Forms.CheckBox chkFijo;
        private System.Windows.Forms.TabPage tpPartes;
        private System.Windows.Forms.GroupBox gbAgregarParteMP;
        private System.Windows.Forms.Button btnDiagramaEstructura;
        private System.Windows.Forms.GroupBox gbArbolEstructura;
        private System.Windows.Forms.DataGridView dgvPartesDisponibles;
        private System.Windows.Forms.TreeView tvEstructura;
        private System.Windows.Forms.TabControl tcPartesDisponibles;
        private System.Windows.Forms.TabPage tpPartesDisponibles;
        private System.Windows.Forms.TabPage tpMPDisponibles;
        private System.Windows.Forms.Button btnAgregarParte;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nudCantidadAgregar;
        private System.Windows.Forms.DataGridView dgvMPDisponibles;
        private System.Windows.Forms.Panel panelAccionesArbol;
        private System.Windows.Forms.Button btnRestarParte;
        private System.Windows.Forms.Button btnSumarParte;
        private System.Windows.Forms.Button btnDeleteParte;
        private System.Windows.Forms.Button btnVolverDePartes;
        private System.Windows.Forms.Panel panel1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboActivoBuscar;
        private System.Windows.Forms.GroupBox groupBox5;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboFiltroTipoParte;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtFiltroNombreParte;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtFiltroNombreMP;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ContextMenuStrip cmsGrillaOrdenesProduccion;
        private System.Windows.Forms.ToolStripMenuItem tsmiBloquearColumna;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesbloquearColumna;
    }
}