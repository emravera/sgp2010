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
            this.gbDatos = new System.Windows.Forms.GroupBox();
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
            this.gbImagen = new System.Windows.Forms.GroupBox();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnQuitarImagen = new System.Windows.Forms.Button();
            this.btnAbrirImagen = new System.Windows.Forms.Button();
            this.slideDatos = new SlickInterface.Slide();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dgvCapacidades = new System.Windows.Forms.DataGridView();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.slideControl = new SlickInterface.SlideControl();
            this.tpAgregarCapacidad = new System.Windows.Forms.TabPage();
            this.gbAgregar = new System.Windows.Forms.GroupBox();
            this.dgvListaCapacidadesAgregar = new System.Windows.Forms.DataGridView();
            this.btnAgregarCapacidad = new System.Windows.Forms.Button();
            this.btnHecho = new System.Windows.Forms.Button();
            this.slideAgregar = new SlickInterface.Slide();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ofdImagen = new System.Windows.Forms.OpenFileDialog();
            this.tcABM.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.gpbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.gbImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacidades)).BeginInit();
            this.gbGuardarCancelar.SuspendLayout();
            this.tpAgregarCapacidad.SuspendLayout();
            this.gbAgregar.SuspendLayout();
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
            this.tcABM.Size = new System.Drawing.Size(493, 432);
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
            this.tpBuscar.Size = new System.Drawing.Size(485, 423);
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
            this.gpbLista.Size = new System.Drawing.Size(479, 328);
            this.gpbLista.TabIndex = 1;
            this.gpbLista.TabStop = false;
            this.gpbLista.Text = "Listado";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.CausesValidation = false;
            this.dgvLista.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 22);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(461, 297);
            this.dgvLista.TabIndex = 6;
            this.dgvLista.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_RowEnter);
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting);
            this.dgvLista.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
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
            this.groupBox1.Size = new System.Drawing.Size(479, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(231, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Sector:";
            // 
            // cboSectorBuscar
            // 
            this.cboSectorBuscar.CausesValidation = false;
            this.cboSectorBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSectorBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboSectorBuscar.FormattingEnabled = true;
            this.cboSectorBuscar.Location = new System.Drawing.Point(279, 52);
            this.cboSectorBuscar.Name = "cboSectorBuscar";
            this.cboSectorBuscar.Size = new System.Drawing.Size(113, 21);
            this.cboSectorBuscar.TabIndex = 4;
            // 
            // cboBuscarPor
            // 
            this.cboBuscarPor.CausesValidation = false;
            this.cboBuscarPor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscarPor.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboBuscarPor.FormattingEnabled = true;
            this.cboBuscarPor.Location = new System.Drawing.Point(77, 23);
            this.cboBuscarPor.Name = "cboBuscarPor";
            this.cboBuscarPor.Size = new System.Drawing.Size(148, 21);
            this.cboBuscarPor.TabIndex = 1;
            // 
            // cboBuscarEstado
            // 
            this.cboBuscarEstado.CausesValidation = false;
            this.cboBuscarEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscarEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboBuscarEstado.FormattingEnabled = true;
            this.cboBuscarEstado.Location = new System.Drawing.Point(77, 52);
            this.cboBuscarEstado.Name = "cboBuscarEstado";
            this.cboBuscarEstado.Size = new System.Drawing.Size(148, 21);
            this.cboBuscarEstado.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Estado:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(398, 32);
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
            this.txtNombreBuscar.Location = new System.Drawing.Point(231, 23);
            this.txtNombreBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(161, 20);
            this.txtNombreBuscar.TabIndex = 2;
            this.txtNombreBuscar.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar por:";
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Controls.Add(this.groupBox2);
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.slideControl);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpDatos.Size = new System.Drawing.Size(485, 423);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
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
            this.gbDatos.Controls.Add(this.gbImagen);
            this.gbDatos.Controls.Add(this.slideDatos);
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatos.Size = new System.Drawing.Size(477, 217);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos Empleado";
            // 
            // cboSector
            // 
            this.cboSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSector.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboSector.FormattingEnabled = true;
            this.cboSector.Location = new System.Drawing.Point(88, 183);
            this.cboSector.Name = "cboSector";
            this.cboSector.Size = new System.Drawing.Size(182, 21);
            this.cboSector.TabIndex = 12;
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(88, 149);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(182, 21);
            this.cboEstado.TabIndex = 11;
            // 
            // sfFechaNac
            // 
            this.sfFechaNac.CustomFormat = " ";
            this.sfFechaNac.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sfFechaNac.Location = new System.Drawing.Point(88, 116);
            this.sfFechaNac.Name = "sfFechaNac";
            this.sfFechaNac.Size = new System.Drawing.Size(182, 20);
            this.sfFechaNac.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Estado:";
            // 
            // txtLegajo
            // 
            this.txtLegajo.Location = new System.Drawing.Point(88, 22);
            this.txtLegajo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.Size = new System.Drawing.Size(182, 20);
            this.txtLegajo.TabIndex = 7;
            this.txtLegajo.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Legajo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Fecha Nac.:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(88, 84);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(182, 20);
            this.txtApellido.TabIndex = 9;
            this.txtApellido.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Apellido:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Sector:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(88, 53);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(182, 20);
            this.txtNombre.TabIndex = 8;
            this.txtNombre.Enter += new System.EventHandler(this.control_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // gbImagen
            // 
            this.gbImagen.Controls.Add(this.btnZoomOut);
            this.gbImagen.Controls.Add(this.pbImagen);
            this.gbImagen.Controls.Add(this.btnZoomIn);
            this.gbImagen.Controls.Add(this.btnQuitarImagen);
            this.gbImagen.Controls.Add(this.btnAbrirImagen);
            this.gbImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbImagen.Location = new System.Drawing.Point(300, 12);
            this.gbImagen.Name = "gbImagen";
            this.gbImagen.Size = new System.Drawing.Size(171, 196);
            this.gbImagen.TabIndex = 22;
            this.gbImagen.TabStop = false;
            this.gbImagen.Text = "Foto";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.AutoSize = true;
            this.btnZoomOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZoomOut.FlatAppearance.BorderSize = 0;
            this.btnZoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Zoom_Out_25;
            this.btnZoomOut.Location = new System.Drawing.Point(128, 160);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(31, 31);
            this.btnZoomOut.TabIndex = 16;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            this.btnZoomOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnZoomOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // pbImagen
            // 
            this.pbImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImagen.Location = new System.Drawing.Point(6, 16);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(153, 138);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagen.TabIndex = 0;
            this.pbImagen.TabStop = false;
            this.pbImagen.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbImagen_LoadCompleted);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.AutoSize = true;
            this.btnZoomIn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZoomIn.FlatAppearance.BorderSize = 0;
            this.btnZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Zoom_In_25;
            this.btnZoomIn.Location = new System.Drawing.Point(95, 160);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(31, 31);
            this.btnZoomIn.TabIndex = 15;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            this.btnZoomIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnZoomIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnQuitarImagen
            // 
            this.btnQuitarImagen.AutoSize = true;
            this.btnQuitarImagen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuitarImagen.FlatAppearance.BorderSize = 0;
            this.btnQuitarImagen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnQuitarImagen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnQuitarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarImagen.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Delete_25;
            this.btnQuitarImagen.Location = new System.Drawing.Point(43, 160);
            this.btnQuitarImagen.Name = "btnQuitarImagen";
            this.btnQuitarImagen.Size = new System.Drawing.Size(31, 31);
            this.btnQuitarImagen.TabIndex = 14;
            this.btnQuitarImagen.UseVisualStyleBackColor = true;
            this.btnQuitarImagen.Click += new System.EventHandler(this.btnQuitar_Click);
            this.btnQuitarImagen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnQuitarImagen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnAbrirImagen
            // 
            this.btnAbrirImagen.AutoSize = true;
            this.btnAbrirImagen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAbrirImagen.FlatAppearance.BorderSize = 0;
            this.btnAbrirImagen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnAbrirImagen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnAbrirImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirImagen.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Abrir_25;
            this.btnAbrirImagen.Location = new System.Drawing.Point(6, 160);
            this.btnAbrirImagen.Name = "btnAbrirImagen";
            this.btnAbrirImagen.Size = new System.Drawing.Size(31, 31);
            this.btnAbrirImagen.TabIndex = 13;
            this.btnAbrirImagen.UseVisualStyleBackColor = true;
            this.btnAbrirImagen.Click += new System.EventHandler(this.btnImagen_Click);
            this.btnAbrirImagen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnAbrirImagen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // slideDatos
            // 
            this.slideDatos.Location = new System.Drawing.Point(-3, 2);
            this.slideDatos.Name = "slideDatos";
            this.slideDatos.Size = new System.Drawing.Size(488, 221);
            this.slideDatos.SlideControl = null;
            this.slideDatos.TabIndex = 21;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelAcciones);
            this.groupBox2.Controls.Add(this.dgvCapacidades);
            this.groupBox2.Location = new System.Drawing.Point(5, 227);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 135);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Capacidades del empleado";
            // 
            // panelAcciones
            // 
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Controls.Add(this.btnNew);
            this.panelAcciones.Location = new System.Drawing.Point(403, 25);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(65, 97);
            this.panelAcciones.TabIndex = 23;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.Delete_25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(15, 52);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDeleteCapacidad_Click);
            this.btnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnNew
            // 
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = global::GyCAP.UI.RecursosFabricacion.Properties.Resources.New_25;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(14, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(30, 30);
            this.btnNew.TabIndex = 17;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNewCapacidad_Click);
            this.btnNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // dgvCapacidades
            // 
            this.dgvCapacidades.AllowUserToAddRows = false;
            this.dgvCapacidades.AllowUserToDeleteRows = false;
            this.dgvCapacidades.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvCapacidades.Location = new System.Drawing.Point(3, 16);
            this.dgvCapacidades.MultiSelect = false;
            this.dgvCapacidades.Name = "dgvCapacidades";
            this.dgvCapacidades.ReadOnly = true;
            this.dgvCapacidades.RowHeadersVisible = false;
            this.dgvCapacidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCapacidades.Size = new System.Drawing.Size(394, 116);
            this.dgvCapacidades.TabIndex = 19;
            this.dgvCapacidades.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCapacidades_CellFormatting);
            this.dgvCapacidades.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 364);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbGuardarCancelar.Size = new System.Drawing.Size(482, 57);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(412, 20);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 23;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(342, 20);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 22;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // slideControl
            // 
            this.slideControl.Location = new System.Drawing.Point(3, 5);
            this.slideControl.Name = "slideControl";
            this.slideControl.Selected = null;
            this.slideControl.Size = new System.Drawing.Size(553, 221);
            this.slideControl.SlideSpeed = 250;
            this.slideControl.TabIndex = 20;
            // 
            // tpAgregarCapacidad
            // 
            this.tpAgregarCapacidad.Controls.Add(this.gbAgregar);
            this.tpAgregarCapacidad.Controls.Add(this.slideAgregar);
            this.tpAgregarCapacidad.Location = new System.Drawing.Point(4, 5);
            this.tpAgregarCapacidad.Name = "tpAgregarCapacidad";
            this.tpAgregarCapacidad.Size = new System.Drawing.Size(485, 423);
            this.tpAgregarCapacidad.TabIndex = 2;
            this.tpAgregarCapacidad.UseVisualStyleBackColor = true;
            // 
            // gbAgregar
            // 
            this.gbAgregar.Controls.Add(this.dgvListaCapacidadesAgregar);
            this.gbAgregar.Controls.Add(this.btnAgregarCapacidad);
            this.gbAgregar.Controls.Add(this.btnHecho);
            this.gbAgregar.Location = new System.Drawing.Point(3, 3);
            this.gbAgregar.Name = "gbAgregar";
            this.gbAgregar.Size = new System.Drawing.Size(479, 217);
            this.gbAgregar.TabIndex = 4;
            this.gbAgregar.TabStop = false;
            this.gbAgregar.Text = "Seleccione una capacidad";
            // 
            // dgvListaCapacidadesAgregar
            // 
            this.dgvListaCapacidadesAgregar.AllowUserToAddRows = false;
            this.dgvListaCapacidadesAgregar.AllowUserToDeleteRows = false;
            this.dgvListaCapacidadesAgregar.AllowUserToResizeRows = false;
            this.dgvListaCapacidadesAgregar.CausesValidation = false;
            this.dgvListaCapacidadesAgregar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaCapacidadesAgregar.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvListaCapacidadesAgregar.Location = new System.Drawing.Point(3, 16);
            this.dgvListaCapacidadesAgregar.MultiSelect = false;
            this.dgvListaCapacidadesAgregar.Name = "dgvListaCapacidadesAgregar";
            this.dgvListaCapacidadesAgregar.ReadOnly = true;
            this.dgvListaCapacidadesAgregar.RowHeadersVisible = false;
            this.dgvListaCapacidadesAgregar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaCapacidadesAgregar.Size = new System.Drawing.Size(370, 198);
            this.dgvListaCapacidadesAgregar.TabIndex = 19;
            this.dgvListaCapacidadesAgregar.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
            // 
            // btnAgregarCapacidad
            // 
            this.btnAgregarCapacidad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarCapacidad.Location = new System.Drawing.Point(382, 53);
            this.btnAgregarCapacidad.Name = "btnAgregarCapacidad";
            this.btnAgregarCapacidad.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarCapacidad.TabIndex = 20;
            this.btnAgregarCapacidad.Text = "Agregar";
            this.btnAgregarCapacidad.UseVisualStyleBackColor = true;
            this.btnAgregarCapacidad.Click += new System.EventHandler(this.btnAgregarCapacidad_Click);
            // 
            // btnHecho
            // 
            this.btnHecho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHecho.Location = new System.Drawing.Point(382, 82);
            this.btnHecho.Name = "btnHecho";
            this.btnHecho.Size = new System.Drawing.Size(75, 23);
            this.btnHecho.TabIndex = 21;
            this.btnHecho.Text = "Hecho";
            this.btnHecho.UseVisualStyleBackColor = true;
            this.btnHecho.Click += new System.EventHandler(this.btnHecho_Click);
            // 
            // slideAgregar
            // 
            this.slideAgregar.Location = new System.Drawing.Point(6, 3);
            this.slideAgregar.Name = "slideAgregar";
            this.slideAgregar.Size = new System.Drawing.Size(476, 181);
            this.slideAgregar.SlideControl = null;
            this.slideAgregar.TabIndex = 0;
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
            this.tsMenu.Size = new System.Drawing.Size(493, 50);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(497, 488);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // ofdImagen
            // 
            this.ofdImagen.InitialDirectory = "d:\\";
            this.ofdImagen.Title = "Seleccione una imagen";
            this.ofdImagen.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdImagen_FileOk);
            // 
            // frmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 488);
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
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.gbImagen.ResumeLayout(false);
            this.gbImagen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacidades)).EndInit();
            this.gbGuardarCancelar.ResumeLayout(false);
            this.tpAgregarCapacidad.ResumeLayout(false);
            this.gbAgregar.ResumeLayout(false);
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
        private SlickInterface.SlideControl slideControl;
        private SlickInterface.Slide slideDatos;
        private System.Windows.Forms.TabPage tpAgregarCapacidad;
        private System.Windows.Forms.Button btnHecho;
        private System.Windows.Forms.Button btnAgregarCapacidad;
        private System.Windows.Forms.DataGridView dgvListaCapacidadesAgregar;
        private SlickInterface.Slide slideAgregar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboBuscarPor;
        private System.Windows.Forms.Label label8;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboSectorBuscar;
        private System.Windows.Forms.GroupBox gbImagen;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.OpenFileDialog ofdImagen;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnQuitarImagen;
        private System.Windows.Forms.Button btnAbrirImagen;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox gbAgregar;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}