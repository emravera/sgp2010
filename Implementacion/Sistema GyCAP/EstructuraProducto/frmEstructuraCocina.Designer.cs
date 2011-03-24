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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcEstructuraCocina = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvEstructuras = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaAltaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cbResponsableBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlanoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbCocinaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbActivoBuscar = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbCSCPMP = new System.Windows.Forms.GroupBox();
            this.btnDatos = new System.Windows.Forms.Button();
            this.btnConjuntos = new System.Windows.Forms.Button();
            this.btnPiezas = new System.Windows.Forms.Button();
            this.gbVer = new System.Windows.Forms.GroupBox();
            this.btnArbol = new System.Windows.Forms.Button();
            this.gbPartes = new System.Windows.Forms.GroupBox();
            this.dgvPartes = new System.Windows.Forms.DataGridView();
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
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcEstructuraCocina.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstructuras)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbCSCPMP.SuspendLayout();
            this.gbVer.SuspendLayout();
            this.gbPartes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartes)).BeginInit();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudcosto)).BeginInit();
            this.gbGuardarVolver.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcEstructuraCocina, 0, 1);
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
            // tcEstructuraCocina
            // 
            this.tcEstructuraCocina.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcEstructuraCocina.Controls.Add(this.tpBuscar);
            this.tcEstructuraCocina.Controls.Add(this.tpDatos);
            this.tcEstructuraCocina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEstructuraCocina.ItemSize = new System.Drawing.Size(0, 1);
            this.tcEstructuraCocina.Location = new System.Drawing.Point(3, 53);
            this.tcEstructuraCocina.Multiline = true;
            this.tcEstructuraCocina.Name = "tcEstructuraCocina";
            this.tcEstructuraCocina.SelectedIndex = 0;
            this.tcEstructuraCocina.Size = new System.Drawing.Size(786, 514);
            this.tcEstructuraCocina.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcEstructuraCocina.TabIndex = 0;
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
            this.groupBox1.Controls.Add(this.dtpFechaAltaBuscar);
            this.groupBox1.Controls.Add(this.cbResponsableBuscar);
            this.groupBox1.Controls.Add(this.cbPlanoBuscar);
            this.groupBox1.Controls.Add(this.cbCocinaBuscar);
            this.groupBox1.Controls.Add(this.cbActivoBuscar);
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
            // dtpFechaAltaBuscar
            // 
            this.dtpFechaAltaBuscar.CustomFormat = " ";
            this.dtpFechaAltaBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaAltaBuscar.Location = new System.Drawing.Point(574, 20);
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
            // cbActivoBuscar
            // 
            this.cbActivoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActivoBuscar.FormattingEnabled = true;
            this.cbActivoBuscar.Location = new System.Drawing.Point(574, 60);
            this.cbActivoBuscar.Name = "cbActivoBuscar";
            this.cbActivoBuscar.Size = new System.Drawing.Size(95, 21);
            this.cbActivoBuscar.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(517, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Activo:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(517, 15);
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
            this.tpDatos.Controls.Add(this.gbCSCPMP);
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
            // gbCSCPMP
            // 
            this.gbCSCPMP.Controls.Add(this.btnDatos);
            this.gbCSCPMP.Controls.Add(this.btnConjuntos);
            this.gbCSCPMP.Controls.Add(this.btnPiezas);
            this.gbCSCPMP.Location = new System.Drawing.Point(6, 453);
            this.gbCSCPMP.Name = "gbCSCPMP";
            this.gbCSCPMP.Size = new System.Drawing.Size(606, 50);
            this.gbCSCPMP.TabIndex = 12;
            this.gbCSCPMP.TabStop = false;
            // 
            // btnDatos
            // 
            this.btnDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatos.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.arriba1_15;
            this.btnDatos.Location = new System.Drawing.Point(77, 15);
            this.btnDatos.Name = "btnDatos";
            this.btnDatos.Size = new System.Drawing.Size(115, 28);
            this.btnDatos.TabIndex = 10;
            this.btnDatos.Text = "Datos";
            this.btnDatos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDatos.UseVisualStyleBackColor = true;
            this.btnDatos.Click += new System.EventHandler(this.btnDatos_Click);
            // 
            // btnConjuntos
            // 
            this.btnConjuntos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConjuntos.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.derecha1_15;
            this.btnConjuntos.Location = new System.Drawing.Point(234, 15);
            this.btnConjuntos.Name = "btnConjuntos";
            this.btnConjuntos.Size = new System.Drawing.Size(115, 28);
            this.btnConjuntos.TabIndex = 4;
            this.btnConjuntos.Text = "Conjuntos";
            this.btnConjuntos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnConjuntos.UseVisualStyleBackColor = true;
            this.btnConjuntos.Click += new System.EventHandler(this.btnConjuntos_Click);
            // 
            // btnPiezas
            // 
            this.btnPiezas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPiezas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPiezas.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.derecha1_15;
            this.btnPiezas.Location = new System.Drawing.Point(389, 15);
            this.btnPiezas.Name = "btnPiezas";
            this.btnPiezas.Size = new System.Drawing.Size(115, 28);
            this.btnPiezas.TabIndex = 7;
            this.btnPiezas.Text = "Piezas";
            this.btnPiezas.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnPiezas.UseVisualStyleBackColor = true;
            this.btnPiezas.Click += new System.EventHandler(this.btnPiezas_Click);
            // 
            // gbVer
            // 
            this.gbVer.Controls.Add(this.btnArbol);
            this.gbVer.Location = new System.Drawing.Point(618, 177);
            this.gbVer.Name = "gbVer";
            this.gbVer.Size = new System.Drawing.Size(156, 274);
            this.gbVer.TabIndex = 10;
            this.gbVer.TabStop = false;
            this.gbVer.Text = "Ver";
            // 
            // btnArbol
            // 
            this.btnArbol.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnArbol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArbol.Location = new System.Drawing.Point(28, 173);
            this.btnArbol.Name = "btnArbol";
            this.btnArbol.Size = new System.Drawing.Size(92, 47);
            this.btnArbol.TabIndex = 4;
            this.btnArbol.Text = "Árbol de\r\nEstructura";
            this.btnArbol.UseVisualStyleBackColor = true;
            this.btnArbol.Visible = false;
            // 
            // gbPartes
            // 
            this.gbPartes.Controls.Add(this.dgvPartes);
            this.gbPartes.Location = new System.Drawing.Point(3, 177);
            this.gbPartes.Name = "gbPartes";
            this.gbPartes.Size = new System.Drawing.Size(609, 277);
            this.gbPartes.TabIndex = 8;
            this.gbPartes.TabStop = false;
            this.gbPartes.Text = "Listado de partes";
            // 
            // dgvPartes
            // 
            this.dgvPartes.AllowUserToAddRows = false;
            this.dgvPartes.AllowUserToDeleteRows = false;
            this.dgvPartes.AllowUserToResizeRows = false;
            this.dgvPartes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPartes.Location = new System.Drawing.Point(3, 16);
            this.dgvPartes.MultiSelect = false;
            this.dgvPartes.Name = "dgvPartes";
            this.dgvPartes.ReadOnly = true;
            this.dgvPartes.RowHeadersVisible = false;
            this.dgvPartes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartes.Size = new System.Drawing.Size(603, 258);
            this.dgvPartes.TabIndex = 0;
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
            this.gbDatos.Location = new System.Drawing.Point(3, 6);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(772, 169);
            this.gbDatos.TabIndex = 5;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la Estructura de Cocina";
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
            this.Text = "Estructura de Cocina";
            this.Activated += new System.EventHandler(this.frmEstructuraCocina_Activated);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcEstructuraCocina.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstructuras)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbCSCPMP.ResumeLayout(false);
            this.gbVer.ResumeLayout(false);
            this.gbPartes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartes)).EndInit();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudcosto)).EndInit();
            this.gbGuardarVolver.ResumeLayout(false);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcEstructuraCocina;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvEstructuras;
        private System.Windows.Forms.GroupBox groupBox1;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaAltaBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbResponsableBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanoBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCocinaBuscar;
        private System.Windows.Forms.ComboBox cbActivoBuscar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbCSCPMP;
        private System.Windows.Forms.Button btnDatos;
        private System.Windows.Forms.Button btnConjuntos;
        private System.Windows.Forms.Button btnPiezas;
        private System.Windows.Forms.GroupBox gbVer;
        private System.Windows.Forms.Button btnArbol;
        private System.Windows.Forms.GroupBox gbPartes;
        private System.Windows.Forms.DataGridView dgvPartes;
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
    }
}