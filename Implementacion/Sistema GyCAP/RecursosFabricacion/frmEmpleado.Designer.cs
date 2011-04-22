namespace GyCAP.UI.RecursosFabricacion
{
    partial class frmEmpleado
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
            this.tcABM = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gpbLista = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboSectorBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboBuscarPor = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboBuscarEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.dgvCapacidades = new System.Windows.Forms.DataGridView();
            this.cboSector = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.sfFechaNac = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLegajo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.slideControl1 = new SlickInterface.SlideControl();
            this.slide1 = new SlickInterface.Slide();
            this.tpAgregarCapacidad = new System.Windows.Forms.TabPage();
            this.btnHecho = new System.Windows.Forms.Button();
            this.btnAgregarCapacidad = new System.Windows.Forms.Button();
            this.dgvListaCapacidadesAgregar = new System.Windows.Forms.DataGridView();
            this.slide2 = new SlickInterface.Slide();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcABM.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.gpbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacidades)).BeginInit();
            this.tpAgregarCapacidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCapacidadesAgregar)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcABM
            // 
            this.tcABM.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcABM.Controls.Add(this.tpBuscar);
            this.tcABM.Controls.Add(this.tpDatos);
            this.tcABM.Controls.Add(this.tpAgregarCapacidad);
            this.tcABM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcABM.ItemSize = new System.Drawing.Size(0, 1);
            this.tcABM.Location = new System.Drawing.Point(2, 54);
            this.tcABM.Margin = new System.Windows.Forms.Padding(0);
            this.tcABM.Multiline = true;
            this.tcABM.Name = "tcABM";
            this.tcABM.Padding = new System.Drawing.Point(0, 0);
            this.tcABM.SelectedIndex = 0;
            this.tcABM.Size = new System.Drawing.Size(558, 386);
            this.tcABM.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcABM.TabIndex = 8;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.gpbLista);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpBuscar.Size = new System.Drawing.Size(550, 377);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // gpbLista
            // 
            this.gpbLista.Controls.Add(this.dgvLista);
            this.gpbLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpbLista.Location = new System.Drawing.Point(3, 90);
            this.gpbLista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbLista.Name = "gpbLista";
            this.gpbLista.Padding = new System.Windows.Forms.Padding(9);
            this.gpbLista.Size = new System.Drawing.Size(544, 282);
            this.gpbLista.TabIndex = 1;
            this.gpbLista.TabStop = false;
            this.gpbLista.Text = "Listado";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 22);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(526, 251);
            this.dgvLista.TabIndex = 1;
            this.dgvLista.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_RowEnter);
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboSectorBuscar);
            this.groupBox1.Controls.Add(this.cboBuscarPor);
            this.groupBox1.Controls.Add(this.cboBuscarEstado);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtNombreBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(544, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Sector:";
            // 
            // cboSectorBuscar
            // 
            this.cboSectorBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSectorBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboSectorBuscar.FormattingEnabled = true;
            this.cboSectorBuscar.Location = new System.Drawing.Point(288, 52);
            this.cboSectorBuscar.Name = "cboSectorBuscar";
            this.cboSectorBuscar.Size = new System.Drawing.Size(121, 21);
            this.cboSectorBuscar.TabIndex = 7;
            // 
            // cboBuscarPor
            // 
            this.cboBuscarPor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscarPor.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboBuscarPor.FormattingEnabled = true;
            this.cboBuscarPor.Location = new System.Drawing.Point(86, 23);
            this.cboBuscarPor.Name = "cboBuscarPor";
            this.cboBuscarPor.Size = new System.Drawing.Size(148, 21);
            this.cboBuscarPor.TabIndex = 6;
            // 
            // cboBuscarEstado
            // 
            this.cboBuscarEstado.CausesValidation = false;
            this.cboBuscarEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscarEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboBuscarEstado.FormattingEnabled = true;
            this.cboBuscarEstado.Location = new System.Drawing.Point(86, 52);
            this.cboBuscarEstado.Name = "cboBuscarEstado";
            this.cboBuscarEstado.Size = new System.Drawing.Size(148, 21);
            this.cboBuscarEstado.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Estado:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(453, 36);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.txtNombreBuscar.Location = new System.Drawing.Point(240, 23);
            this.txtNombreBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(169, 20);
            this.txtNombreBuscar.TabIndex = 4;
            this.txtNombreBuscar.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar por:";
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpDatos.Size = new System.Drawing.Size(550, 377);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 318);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbGuardarCancelar.Size = new System.Drawing.Size(544, 57);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(472, 20);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 15;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(402, 20);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.dgvCapacidades);
            this.gbDatos.Controls.Add(this.cboSector);
            this.gbDatos.Controls.Add(this.cboEstado);
            this.gbDatos.Controls.Add(this.sfFechaNac);
            this.gbDatos.Controls.Add(this.label6);
            this.gbDatos.Controls.Add(this.txtLegajo);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.label7);
            this.gbDatos.Controls.Add(this.txtApellido);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label5);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Controls.Add(this.slideControl1);
            this.gbDatos.Controls.Add(this.slide1);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 2);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatos.Size = new System.Drawing.Size(544, 314);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos Empleado";
            // 
            // dgvCapacidades
            // 
            this.dgvCapacidades.AllowUserToAddRows = false;
            this.dgvCapacidades.AllowUserToDeleteRows = false;
            this.dgvCapacidades.Location = new System.Drawing.Point(16, 174);
            this.dgvCapacidades.MultiSelect = false;
            this.dgvCapacidades.Name = "dgvCapacidades";
            this.dgvCapacidades.ReadOnly = true;
            this.dgvCapacidades.RowHeadersVisible = false;
            this.dgvCapacidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCapacidades.Size = new System.Drawing.Size(353, 135);
            this.dgvCapacidades.TabIndex = 19;
            this.dgvCapacidades.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCapacidades_CellFormatting);
            // 
            // cboSector
            // 
            this.cboSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSector.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboSector.FormattingEnabled = true;
            this.cboSector.Location = new System.Drawing.Point(320, 90);
            this.cboSector.Name = "cboSector";
            this.cboSector.Size = new System.Drawing.Size(146, 21);
            this.cboSector.TabIndex = 11;
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(337, 56);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(146, 21);
            this.cboEstado.TabIndex = 12;
            // 
            // sfFechaNac
            // 
            this.sfFechaNac.CustomFormat = " ";
            this.sfFechaNac.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaNac.Location = new System.Drawing.Point(337, 28);
            this.sfFechaNac.Name = "sfFechaNac";
            this.sfFechaNac.Size = new System.Drawing.Size(146, 20);
            this.sfFechaNac.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(266, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Estado:";
            // 
            // txtLegajo
            // 
            this.txtLegajo.Location = new System.Drawing.Point(83, 28);
            this.txtLegajo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.Size = new System.Drawing.Size(146, 20);
            this.txtLegajo.TabIndex = 6;
            this.txtLegajo.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Legajo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(266, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Fecha Nac.:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(83, 85);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(146, 20);
            this.txtApellido.TabIndex = 7;
            this.txtApellido.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Apellido:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(266, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Sector:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(83, 57);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(146, 20);
            this.txtNombre.TabIndex = 8;
            this.txtNombre.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // slideControl1
            // 
            this.slideControl1.Location = new System.Drawing.Point(3, 18);
            this.slideControl1.Name = "slideControl1";
            this.slideControl1.Selected = null;
            this.slideControl1.Size = new System.Drawing.Size(532, 150);
            this.slideControl1.SlideSpeed = 250;
            this.slideControl1.TabIndex = 20;
            // 
            // slide1
            // 
            this.slide1.Location = new System.Drawing.Point(6, 18);
            this.slide1.Name = "slide1";
            this.slide1.Size = new System.Drawing.Size(532, 150);
            this.slide1.SlideControl = null;
            this.slide1.TabIndex = 21;
            // 
            // tpAgregarCapacidad
            // 
            this.tpAgregarCapacidad.Controls.Add(this.btnHecho);
            this.tpAgregarCapacidad.Controls.Add(this.btnAgregarCapacidad);
            this.tpAgregarCapacidad.Controls.Add(this.dgvListaCapacidadesAgregar);
            this.tpAgregarCapacidad.Controls.Add(this.slide2);
            this.tpAgregarCapacidad.Location = new System.Drawing.Point(4, 5);
            this.tpAgregarCapacidad.Name = "tpAgregarCapacidad";
            this.tpAgregarCapacidad.Size = new System.Drawing.Size(550, 377);
            this.tpAgregarCapacidad.TabIndex = 2;
            this.tpAgregarCapacidad.UseVisualStyleBackColor = true;
            // 
            // btnHecho
            // 
            this.btnHecho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHecho.Location = new System.Drawing.Point(439, 76);
            this.btnHecho.Name = "btnHecho";
            this.btnHecho.Size = new System.Drawing.Size(75, 23);
            this.btnHecho.TabIndex = 3;
            this.btnHecho.Text = "Finalizar";
            this.btnHecho.UseVisualStyleBackColor = true;
            // 
            // btnAgregarCapacidad
            // 
            this.btnAgregarCapacidad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarCapacidad.Location = new System.Drawing.Point(439, 31);
            this.btnAgregarCapacidad.Name = "btnAgregarCapacidad";
            this.btnAgregarCapacidad.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarCapacidad.TabIndex = 2;
            this.btnAgregarCapacidad.Text = "Agregar";
            this.btnAgregarCapacidad.UseVisualStyleBackColor = true;
            // 
            // dgvListaCapacidadesAgregar
            // 
            this.dgvListaCapacidadesAgregar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaCapacidadesAgregar.Location = new System.Drawing.Point(16, 3);
            this.dgvListaCapacidadesAgregar.Name = "dgvListaCapacidadesAgregar";
            this.dgvListaCapacidadesAgregar.Size = new System.Drawing.Size(398, 150);
            this.dgvListaCapacidadesAgregar.TabIndex = 1;
            this.dgvListaCapacidadesAgregar.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvListaCapacidadesAgregar_CellFormatting);
            // 
            // slide2
            // 
            this.slide2.Location = new System.Drawing.Point(6, 3);
            this.slide2.Name = "slide2";
            this.slide2.Size = new System.Drawing.Size(538, 150);
            this.slide2.SlideControl = null;
            this.slide2.TabIndex = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // tsMenu
            // 
            this.tsMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tsMenu.Size = new System.Drawing.Size(558, 50);
            this.tsMenu.TabIndex = 0;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.New_25;
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
            this.btnConsultar.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Find_25;
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
            this.btnModificar.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Text_Editor_25;
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
            this.btnEliminar.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 47);
            this.btnEliminar.Text = "&Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcABM, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(562, 442);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // frmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 442);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmEmpleado";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Empleados";
            this.tcABM.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.gpbLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacidades)).EndInit();
            this.tpAgregarCapacidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCapacidadesAgregar)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcABM;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox gpbLista;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLegajo;
        private System.Windows.Forms.Label label9;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboBuscarEstado;
        private System.Windows.Forms.DataGridView dgvLista;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha sfFechaNac;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstado;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboSector;
        private System.Windows.Forms.DataGridView dgvCapacidades;
        private SlickInterface.SlideControl slideControl1;
        private SlickInterface.Slide slide1;
        private System.Windows.Forms.TabPage tpAgregarCapacidad;
        private System.Windows.Forms.Button btnHecho;
        private System.Windows.Forms.Button btnAgregarCapacidad;
        private System.Windows.Forms.DataGridView dgvListaCapacidadesAgregar;
        private SlickInterface.Slide slide2;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboBuscarPor;
        private System.Windows.Forms.Label label8;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboSectorBuscar;
    }
}