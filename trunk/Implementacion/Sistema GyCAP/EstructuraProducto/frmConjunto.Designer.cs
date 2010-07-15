namespace GyCAP.UI.EstructuraProducto
{
    partial class frmConjunto
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcConjunto = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvListaConjuntos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTerminacionBuscar = new System.Windows.Forms.ComboBox();
            this.rbTerminacion = new System.Windows.Forms.RadioButton();
            this.rbNombre = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.slideAgregar = new SlickInterface.Slide();
            this.slideDatos = new SlickInterface.Slide();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnSumar = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panelAgregar = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnHecho = new System.Windows.Forms.Button();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvSCDisponibles = new System.Windows.Forms.DataGridView();
            this.panelDatos = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTerminacion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.slideControl = new SlickInterface.SlideControl();
            this.dgvSCActuales = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcConjunto.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaConjuntos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelAgregar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSCDisponibles)).BeginInit();
            this.panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSCActuales)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(98, 63);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(201, 24);
            this.txtNombre.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(98, 6);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(201, 24);
            this.txtCodigo.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcConjunto, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(590, 570);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tcConjunto
            // 
            this.tcConjunto.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcConjunto.Controls.Add(this.tpBuscar);
            this.tcConjunto.Controls.Add(this.tpDatos);
            this.tcConjunto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcConjunto.ItemSize = new System.Drawing.Size(0, 1);
            this.tcConjunto.Location = new System.Drawing.Point(2, 60);
            this.tcConjunto.Margin = new System.Windows.Forms.Padding(0);
            this.tcConjunto.Multiline = true;
            this.tcConjunto.Name = "tcConjunto";
            this.tcConjunto.Padding = new System.Drawing.Point(0, 0);
            this.tcConjunto.SelectedIndex = 0;
            this.tcConjunto.Size = new System.Drawing.Size(586, 508);
            this.tcConjunto.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcConjunto.TabIndex = 8;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(578, 499);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvListaConjuntos);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(572, 355);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Conjuntos";
            // 
            // dgvListaConjuntos
            // 
            this.dgvListaConjuntos.AllowUserToAddRows = false;
            this.dgvListaConjuntos.AllowUserToDeleteRows = false;
            this.dgvListaConjuntos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvListaConjuntos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaConjuntos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaConjuntos.Location = new System.Drawing.Point(10, 27);
            this.dgvListaConjuntos.MultiSelect = false;
            this.dgvListaConjuntos.Name = "dgvListaConjuntos";
            this.dgvListaConjuntos.ReadOnly = true;
            this.dgvListaConjuntos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaConjuntos.Size = new System.Drawing.Size(552, 318);
            this.dgvListaConjuntos.TabIndex = 0;
            this.dgvListaConjuntos.DoubleClick += new System.EventHandler(this.dgvListaConjuntos_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTerminacionBuscar);
            this.groupBox1.Controls.Add(this.rbTerminacion);
            this.groupBox1.Controls.Add(this.rbNombre);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtNombreBuscar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // cbTerminacionBuscar
            // 
            this.cbTerminacionBuscar.Enabled = false;
            this.cbTerminacionBuscar.FormattingEnabled = true;
            this.cbTerminacionBuscar.Location = new System.Drawing.Point(128, 75);
            this.cbTerminacionBuscar.Name = "cbTerminacionBuscar";
            this.cbTerminacionBuscar.Size = new System.Drawing.Size(224, 23);
            this.cbTerminacionBuscar.TabIndex = 5;
            // 
            // rbTerminacion
            // 
            this.rbTerminacion.AutoSize = true;
            this.rbTerminacion.Location = new System.Drawing.Point(22, 77);
            this.rbTerminacion.Name = "rbTerminacion";
            this.rbTerminacion.Size = new System.Drawing.Size(100, 21);
            this.rbTerminacion.TabIndex = 4;
            this.rbTerminacion.Text = "Terminación:";
            this.rbTerminacion.UseVisualStyleBackColor = true;
            this.rbTerminacion.CheckedChanged += new System.EventHandler(this.rbTerminacion_CheckedChanged);
            // 
            // rbNombre
            // 
            this.rbNombre.AutoSize = true;
            this.rbNombre.Checked = true;
            this.rbNombre.Location = new System.Drawing.Point(22, 32);
            this.rbNombre.Name = "rbNombre";
            this.rbNombre.Size = new System.Drawing.Size(76, 21);
            this.rbNombre.TabIndex = 3;
            this.rbNombre.TabStop = true;
            this.rbNombre.Text = "Nombre:";
            this.rbNombre.UseVisualStyleBackColor = true;
            this.rbNombre.CheckedChanged += new System.EventHandler(this.rbNombre_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.lupa_25;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(430, 42);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombreBuscar
            // 
            this.txtNombreBuscar.Location = new System.Drawing.Point(128, 29);
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(224, 24);
            this.txtNombreBuscar.TabIndex = 1;
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(578, 499);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.slideAgregar);
            this.gbGuardarCancelar.Controls.Add(this.slideDatos);
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 430);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(572, 66);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // slideAgregar
            // 
            this.slideAgregar.Location = new System.Drawing.Point(56, 18);
            this.slideAgregar.Name = "slideAgregar";
            this.slideAgregar.Size = new System.Drawing.Size(49, 42);
            this.slideAgregar.SlideControl = null;
            this.slideAgregar.TabIndex = 9;
            // 
            // slideDatos
            // 
            this.slideDatos.Location = new System.Drawing.Point(6, 23);
            this.slideDatos.Name = "slideDatos";
            this.slideDatos.Size = new System.Drawing.Size(44, 33);
            this.slideDatos.SlideControl = null;
            this.slideDatos.TabIndex = 9;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(250, 23);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(148, 23);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.panelAcciones);
            this.gbDatos.Controls.Add(this.panelAgregar);
            this.gbDatos.Controls.Add(this.panelDatos);
            this.gbDatos.Controls.Add(this.slideControl);
            this.gbDatos.Controls.Add(this.dgvSCActuales);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(572, 425);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos Tipo Unidad de Medida";
            // 
            // panelAcciones
            // 
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.btnRestar);
            this.panelAcciones.Controls.Add(this.btnSumar);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Controls.Add(this.btnNew);
            this.panelAcciones.Location = new System.Drawing.Point(463, 249);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(85, 170);
            this.panelAcciones.TabIndex = 11;
            // 
            // btnRestar
            // 
            this.btnRestar.FlatAppearance.BorderSize = 0;
            this.btnRestar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Menos_25;
            this.btnRestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestar.Location = new System.Drawing.Point(23, 127);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(35, 35);
            this.btnRestar.TabIndex = 3;
            this.btnRestar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestar.UseVisualStyleBackColor = true;
            // 
            // btnSumar
            // 
            this.btnSumar.FlatAppearance.BorderSize = 0;
            this.btnSumar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Mas_25;
            this.btnSumar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSumar.Location = new System.Drawing.Point(23, 84);
            this.btnSumar.Name = "btnSumar";
            this.btnSumar.Size = new System.Drawing.Size(35, 35);
            this.btnSumar.TabIndex = 2;
            this.btnSumar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSumar.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Delete_25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(23, 43);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(35, 35);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.New_25;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(23, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(35, 35);
            this.btnNew.TabIndex = 0;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // panelAgregar
            // 
            this.panelAgregar.Controls.Add(this.panel1);
            this.panelAgregar.Controls.Add(this.label9);
            this.panelAgregar.Controls.Add(this.dgvSCDisponibles);
            this.panelAgregar.Location = new System.Drawing.Point(34, 24);
            this.panelAgregar.Name = "panelAgregar";
            this.panelAgregar.Size = new System.Drawing.Size(532, 202);
            this.panelAgregar.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnAgregar);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnHecho);
            this.panel1.Controls.Add(this.nudCantidad);
            this.panel1.Location = new System.Drawing.Point(429, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 146);
            this.panel1.TabIndex = 6;
            // 
            // btnAgregar
            // 
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Location = new System.Drawing.Point(3, 73);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 25);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Location = new System.Drawing.Point(0, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Cantidad";
            // 
            // btnHecho
            // 
            this.btnHecho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHecho.Location = new System.Drawing.Point(3, 104);
            this.btnHecho.Name = "btnHecho";
            this.btnHecho.Size = new System.Drawing.Size(75, 25);
            this.btnHecho.TabIndex = 4;
            this.btnHecho.Text = "Hecho";
            this.btnHecho.UseVisualStyleBackColor = true;
            this.btnHecho.Click += new System.EventHandler(this.btnHecho_Click);
            // 
            // nudCantidad
            // 
            this.nudCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudCantidad.Location = new System.Drawing.Point(3, 35);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(75, 24);
            this.nudCantidad.TabIndex = 2;
            this.nudCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Listado de Subconjuntos disponibles";
            // 
            // dgvSCDisponibles
            // 
            this.dgvSCDisponibles.AllowUserToAddRows = false;
            this.dgvSCDisponibles.AllowUserToDeleteRows = false;
            this.dgvSCDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSCDisponibles.Location = new System.Drawing.Point(3, 20);
            this.dgvSCDisponibles.MultiSelect = false;
            this.dgvSCDisponibles.Name = "dgvSCDisponibles";
            this.dgvSCDisponibles.ReadOnly = true;
            this.dgvSCDisponibles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSCDisponibles.Size = new System.Drawing.Size(423, 146);
            this.dgvSCDisponibles.TabIndex = 0;
            // 
            // panelDatos
            // 
            this.panelDatos.Controls.Add(this.txtNombre);
            this.panelDatos.Controls.Add(this.label2);
            this.panelDatos.Controls.Add(this.label3);
            this.panelDatos.Controls.Add(this.txtCodigo);
            this.panelDatos.Controls.Add(this.cbTerminacion);
            this.panelDatos.Controls.Add(this.label1);
            this.panelDatos.Location = new System.Drawing.Point(28, 33);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Size = new System.Drawing.Size(317, 183);
            this.panelDatos.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Código:";
            // 
            // cbTerminacion
            // 
            this.cbTerminacion.FormattingEnabled = true;
            this.cbTerminacion.Location = new System.Drawing.Point(98, 116);
            this.cbTerminacion.Name = "cbTerminacion";
            this.cbTerminacion.Size = new System.Drawing.Size(201, 23);
            this.cbTerminacion.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Terminación:";
            // 
            // slideControl
            // 
            this.slideControl.Location = new System.Drawing.Point(28, 22);
            this.slideControl.Name = "slideControl";
            this.slideControl.Selected = null;
            this.slideControl.Size = new System.Drawing.Size(541, 204);
            this.slideControl.SlideSpeed = 250;
            this.slideControl.TabIndex = 8;
            // 
            // dgvSCActuales
            // 
            this.dgvSCActuales.AllowUserToAddRows = false;
            this.dgvSCActuales.AllowUserToDeleteRows = false;
            this.dgvSCActuales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSCActuales.Location = new System.Drawing.Point(37, 249);
            this.dgvSCActuales.MultiSelect = false;
            this.dgvSCActuales.Name = "dgvSCActuales";
            this.dgvSCActuales.ReadOnly = true;
            this.dgvSCActuales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSCActuales.Size = new System.Drawing.Size(423, 170);
            this.dgvSCActuales.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Subconjuntos";
            // 
            // tsMenu
            // 
            this.tsMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMenu.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tsMenu.Size = new System.Drawing.Size(586, 56);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.New_25;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(45, 53);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(65, 53);
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Text_Editor_25;
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(66, 53);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(58, 53);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(37, 53);
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmConjunto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 570);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmConjunto";
            this.Text = "Conjuntos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConjunto_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcConjunto.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaConjuntos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.panelAgregar.ResumeLayout(false);
            this.panelAgregar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSCDisponibles)).EndInit();
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSCActuales)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcConjunto;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvListaConjuntos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.RadioButton rbTerminacion;
        private System.Windows.Forms.RadioButton rbNombre;
        private System.Windows.Forms.ComboBox cbTerminacionBuscar;
        private System.Windows.Forms.DataGridView dgvSCActuales;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTerminacion;
        private System.Windows.Forms.Label label1;
        private SlickInterface.Slide slideAgregar;
        private SlickInterface.Slide slideDatos;
        private SlickInterface.SlideControl slideControl;
        private System.Windows.Forms.Panel panelAgregar;
        private System.Windows.Forms.Button btnHecho;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvSCDisponibles;
        private System.Windows.Forms.Panel panelDatos;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnSumar;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
    }
}