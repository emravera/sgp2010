namespace GyCAP.UI.EstructuraProducto
{
    partial class frmParte
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
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboProveedor = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboUnidadMedida = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label13 = new System.Windows.Forms.Label();
            this.panelImagen = new System.Windows.Forms.Panel();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnQuitarImagen = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAbrirImagen = new System.Windows.Forms.Button();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.nudCosto = new System.Windows.Forms.NumericUpDown();
            this.cboHojaRuta = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboTerminacion = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboTipo = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboPlano = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tcParte = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEstadoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboPlanoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboTerminacionBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboTipoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.txtCodigoBuscar = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ofdImagen = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.gbDatos.SuspendLayout();
            this.panelImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCosto)).BeginInit();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.tcParte.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.CausesValidation = false;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 23);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(583, 352);
            this.dgvLista.TabIndex = 8;
            this.dgvLista.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_RowEnter);
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting);
            this.dgvLista.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.label15);
            this.gbDatos.Controls.Add(this.cboProveedor);
            this.gbDatos.Controls.Add(this.cboUnidadMedida);
            this.gbDatos.Controls.Add(this.label13);
            this.gbDatos.Controls.Add(this.panelImagen);
            this.gbDatos.Controls.Add(this.nudCosto);
            this.gbDatos.Controls.Add(this.cboHojaRuta);
            this.gbDatos.Controls.Add(this.cboTerminacion);
            this.gbDatos.Controls.Add(this.cboEstado);
            this.gbDatos.Controls.Add(this.cboTipo);
            this.gbDatos.Controls.Add(this.cboPlano);
            this.gbDatos.Controls.Add(this.txtCodigo);
            this.gbDatos.Controls.Add(this.label10);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.txtDescripcion);
            this.gbDatos.Controls.Add(this.label7);
            this.gbDatos.Controls.Add(this.label6);
            this.gbDatos.Controls.Add(this.label5);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label8);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(601, 441);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la parte";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 260);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 39;
            this.label15.Text = "Proveedor:";
            // 
            // cboProveedor
            // 
            this.cboProveedor.CausesValidation = false;
            this.cboProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProveedor.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboProveedor.FormattingEnabled = true;
            this.cboProveedor.Location = new System.Drawing.Point(102, 257);
            this.cboProveedor.Name = "cboProveedor";
            this.cboProveedor.Size = new System.Drawing.Size(251, 21);
            this.cboProveedor.TabIndex = 17;
            // 
            // cboUnidadMedida
            // 
            this.cboUnidadMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidadMedida.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboUnidadMedida.FormattingEnabled = true;
            this.cboUnidadMedida.Location = new System.Drawing.Point(102, 197);
            this.cboUnidadMedida.Name = "cboUnidadMedida";
            this.cboUnidadMedida.Size = new System.Drawing.Size(251, 21);
            this.cboUnidadMedida.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 200);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Unidad medida:";
            // 
            // panelImagen
            // 
            this.panelImagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelImagen.Controls.Add(this.btnZoomOut);
            this.panelImagen.Controls.Add(this.btnZoomIn);
            this.panelImagen.Controls.Add(this.btnQuitarImagen);
            this.panelImagen.Controls.Add(this.label12);
            this.panelImagen.Controls.Add(this.btnAbrirImagen);
            this.panelImagen.Controls.Add(this.pbImagen);
            this.panelImagen.Location = new System.Drawing.Point(375, 37);
            this.panelImagen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelImagen.Name = "panelImagen";
            this.panelImagen.Size = new System.Drawing.Size(221, 238);
            this.panelImagen.TabIndex = 35;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.AutoSize = true;
            this.btnZoomOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZoomOut.FlatAppearance.BorderSize = 0;
            this.btnZoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Zoom_Out_25;
            this.btnZoomOut.Location = new System.Drawing.Point(180, 198);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(31, 31);
            this.btnZoomOut.TabIndex = 23;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            this.btnZoomOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnZoomOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.AutoSize = true;
            this.btnZoomIn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZoomIn.FlatAppearance.BorderSize = 0;
            this.btnZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Zoom_In_25;
            this.btnZoomIn.Location = new System.Drawing.Point(143, 198);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(31, 31);
            this.btnZoomIn.TabIndex = 22;
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
            this.btnQuitarImagen.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Delete_25;
            this.btnQuitarImagen.Location = new System.Drawing.Point(43, 198);
            this.btnQuitarImagen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuitarImagen.Name = "btnQuitarImagen";
            this.btnQuitarImagen.Size = new System.Drawing.Size(31, 31);
            this.btnQuitarImagen.TabIndex = 21;
            this.btnQuitarImagen.UseVisualStyleBackColor = true;
            this.btnQuitarImagen.Click += new System.EventHandler(this.btnQuitarImagen_Click);
            this.btnQuitarImagen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnQuitarImagen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Imagen";
            // 
            // btnAbrirImagen
            // 
            this.btnAbrirImagen.AutoSize = true;
            this.btnAbrirImagen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAbrirImagen.FlatAppearance.BorderSize = 0;
            this.btnAbrirImagen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnAbrirImagen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnAbrirImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirImagen.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Abrir_25;
            this.btnAbrirImagen.Location = new System.Drawing.Point(6, 199);
            this.btnAbrirImagen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAbrirImagen.Name = "btnAbrirImagen";
            this.btnAbrirImagen.Size = new System.Drawing.Size(31, 31);
            this.btnAbrirImagen.TabIndex = 20;
            this.btnAbrirImagen.UseVisualStyleBackColor = true;
            this.btnAbrirImagen.Click += new System.EventHandler(this.btnImagen_Click);
            this.btnAbrirImagen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnAbrirImagen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // pbImagen
            // 
            this.pbImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImagen.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.sinimagen;
            this.pbImagen.Location = new System.Drawing.Point(5, 16);
            this.pbImagen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(206, 177);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagen.TabIndex = 0;
            this.pbImagen.TabStop = false;
            this.pbImagen.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbImagen_LoadCompleted);
            // 
            // nudCosto
            // 
            this.nudCosto.DecimalPlaces = 2;
            this.nudCosto.Location = new System.Drawing.Point(102, 286);
            this.nudCosto.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudCosto.Name = "nudCosto";
            this.nudCosto.Size = new System.Drawing.Size(251, 21);
            this.nudCosto.TabIndex = 18;
            this.nudCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboHojaRuta
            // 
            this.cboHojaRuta.CausesValidation = false;
            this.cboHojaRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojaRuta.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboHojaRuta.FormattingEnabled = true;
            this.cboHojaRuta.Location = new System.Drawing.Point(102, 227);
            this.cboHojaRuta.Name = "cboHojaRuta";
            this.cboHojaRuta.Size = new System.Drawing.Size(251, 21);
            this.cboHojaRuta.TabIndex = 16;
            // 
            // cboTerminacion
            // 
            this.cboTerminacion.CausesValidation = false;
            this.cboTerminacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminacion.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboTerminacion.FormattingEnabled = true;
            this.cboTerminacion.Location = new System.Drawing.Point(102, 166);
            this.cboTerminacion.Name = "cboTerminacion";
            this.cboTerminacion.Size = new System.Drawing.Size(251, 21);
            this.cboTerminacion.TabIndex = 14;
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(102, 135);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(251, 21);
            this.cboEstado.TabIndex = 13;
            // 
            // cboTipo
            // 
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(102, 77);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(251, 21);
            this.cboTipo.TabIndex = 11;
            this.cboTipo.SelectedValueChanged += new System.EventHandler(this.cboTipo_SelectedValueChanged);
            // 
            // cboPlano
            // 
            this.cboPlano.CausesValidation = false;
            this.cboPlano.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlano.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboPlano.FormattingEnabled = true;
            this.cboPlano.Location = new System.Drawing.Point(102, 106);
            this.cboPlano.Name = "cboPlano";
            this.cboPlano.Size = new System.Drawing.Size(251, 21);
            this.cboPlano.TabIndex = 12;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(102, 48);
            this.txtCodigo.MaxLength = 80;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(251, 21);
            this.txtCodigo.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 290);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Costo $:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 230);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Hoja de ruta:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CausesValidation = false;
            this.txtDescripcion.Location = new System.Drawing.Point(102, 325);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(493, 110);
            this.txtDescripcion.TabIndex = 19;
            this.txtDescripcion.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Terminación:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Estado:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Tipo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Plano:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Código:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 325);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Descripción:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(102, 21);
            this.txtNombre.MaxLength = 80;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(251, 21);
            this.txtNombre.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(519, 20);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 25;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(607, 505);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 444);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(601, 56);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(449, 20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 24;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
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
            this.tsMenu.Size = new System.Drawing.Size(615, 50);
            this.tsMenu.TabIndex = 7;
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
            // tcParte
            // 
            this.tcParte.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcParte.Controls.Add(this.tpBuscar);
            this.tcParte.Controls.Add(this.tpDatos);
            this.tcParte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcParte.ItemSize = new System.Drawing.Size(0, 1);
            this.tcParte.Location = new System.Drawing.Point(2, 54);
            this.tcParte.Margin = new System.Windows.Forms.Padding(0);
            this.tcParte.Multiline = true;
            this.tcParte.Name = "tcParte";
            this.tcParte.Padding = new System.Drawing.Point(0, 0);
            this.tcParte.SelectedIndex = 0;
            this.tcParte.Size = new System.Drawing.Size(615, 514);
            this.tcParte.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcParte.TabIndex = 8;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(607, 505);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvLista);
            this.groupBox2.Location = new System.Drawing.Point(3, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(9);
            this.groupBox2.Size = new System.Drawing.Size(601, 384);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado partes";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboEstadoBuscar);
            this.groupBox1.Controls.Add(this.cboPlanoBuscar);
            this.groupBox1.Controls.Add(this.cboTerminacionBuscar);
            this.groupBox1.Controls.Add(this.cboTipoBuscar);
            this.groupBox1.Controls.Add(this.txtCodigoBuscar);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtNombreBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // cboEstadoBuscar
            // 
            this.cboEstadoBuscar.CausesValidation = false;
            this.cboEstadoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstadoBuscar.FormattingEnabled = true;
            this.cboEstadoBuscar.Location = new System.Drawing.Point(310, 50);
            this.cboEstadoBuscar.Name = "cboEstadoBuscar";
            this.cboEstadoBuscar.Size = new System.Drawing.Size(167, 21);
            this.cboEstadoBuscar.TabIndex = 5;
            // 
            // cboPlanoBuscar
            // 
            this.cboPlanoBuscar.CausesValidation = false;
            this.cboPlanoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlanoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboPlanoBuscar.FormattingEnabled = true;
            this.cboPlanoBuscar.Location = new System.Drawing.Point(310, 76);
            this.cboPlanoBuscar.Name = "cboPlanoBuscar";
            this.cboPlanoBuscar.Size = new System.Drawing.Size(167, 21);
            this.cboPlanoBuscar.TabIndex = 6;
            // 
            // cboTerminacionBuscar
            // 
            this.cboTerminacionBuscar.CausesValidation = false;
            this.cboTerminacionBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminacionBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboTerminacionBuscar.FormattingEnabled = true;
            this.cboTerminacionBuscar.Location = new System.Drawing.Point(86, 76);
            this.cboTerminacionBuscar.Name = "cboTerminacionBuscar";
            this.cboTerminacionBuscar.Size = new System.Drawing.Size(168, 21);
            this.cboTerminacionBuscar.TabIndex = 3;
            // 
            // cboTipoBuscar
            // 
            this.cboTipoBuscar.CausesValidation = false;
            this.cboTipoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboTipoBuscar.FormattingEnabled = true;
            this.cboTipoBuscar.Location = new System.Drawing.Point(310, 22);
            this.cboTipoBuscar.Name = "cboTipoBuscar";
            this.cboTipoBuscar.Size = new System.Drawing.Size(167, 21);
            this.cboTipoBuscar.TabIndex = 4;
            // 
            // txtCodigoBuscar
            // 
            this.txtCodigoBuscar.CausesValidation = false;
            this.txtCodigoBuscar.Location = new System.Drawing.Point(86, 50);
            this.txtCodigoBuscar.MaxLength = 80;
            this.txtCodigoBuscar.Name = "txtCodigoBuscar";
            this.txtCodigoBuscar.Size = new System.Drawing.Size(168, 21);
            this.txtCodigoBuscar.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(260, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 13);
            this.label19.TabIndex = 11;
            this.label19.Text = "Tipo:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(260, 53);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Estado:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Terminación:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(260, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Plano:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Código:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(506, 46);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombreBuscar
            // 
            this.txtNombreBuscar.CausesValidation = false;
            this.txtNombreBuscar.Location = new System.Drawing.Point(86, 22);
            this.txtNombreBuscar.MaxLength = 80;
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(168, 21);
            this.txtNombreBuscar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcParte, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(619, 570);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // ofdImagen
            // 
            this.ofdImagen.Title = "Seleccione una imagen";
            this.ofdImagen.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdImagen_FileOk);
            // 
            // frmParte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 570);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmParte";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Parte";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.panelImagen.ResumeLayout(false);
            this.panelImagen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCosto)).EndInit();
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tcParte.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TabControl tcParte;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtCodigoBuscar;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudCosto;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboHojaRuta;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboTerminacion;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstado;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboTipo;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboPlano;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label10;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstadoBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboPlanoBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboTerminacionBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboTipoBuscar;
        private System.Windows.Forms.Panel panelImagen;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnQuitarImagen;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnAbrirImagen;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.OpenFileDialog ofdImagen;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboUnidadMedida;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboProveedor;
    }
}