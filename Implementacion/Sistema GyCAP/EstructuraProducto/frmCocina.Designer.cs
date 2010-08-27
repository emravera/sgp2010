namespace GyCAP.UI.EstructuraProducto
{
    partial class frmCocina
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.cbEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbTerminacion = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbColor = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbDesignacion = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbMarca = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbModelo = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.gbImagen = new System.Windows.Forms.GroupBox();
            this.btnQuitarImagen = new System.Windows.Forms.Button();
            this.btnAbrirImagen = new System.Windows.Forms.Button();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.nudCosto = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvListaCocina = new System.Windows.Forms.DataGridView();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMarcaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbEstadoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbTerminacionBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtCodigoBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tcCocina = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ofdImagen = new System.Windows.Forms.OpenFileDialog();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.tsMenu.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.gbImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCosto)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCocina)).BeginInit();
            this.tpBuscar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tcCocina.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 297);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Costo $:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Designación:";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(509, 20);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 18;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(439, 20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(123, 43);
            this.txtCodigo.MaxLength = 80;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(201, 20);
            this.txtCodigo.TabIndex = 7;
            this.txtCodigo.Enter += new System.EventHandler(this.txtCodigo_Enter);
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
            this.tsMenu.Size = new System.Drawing.Size(593, 50);
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
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 355);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(579, 57);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(585, 415);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.cbEstado);
            this.gbDatos.Controls.Add(this.cbTerminacion);
            this.gbDatos.Controls.Add(this.cbColor);
            this.gbDatos.Controls.Add(this.cbDesignacion);
            this.gbDatos.Controls.Add(this.cbMarca);
            this.gbDatos.Controls.Add(this.cbModelo);
            this.gbDatos.Controls.Add(this.gbImagen);
            this.gbDatos.Controls.Add(this.nudCosto);
            this.gbDatos.Controls.Add(this.label10);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.label8);
            this.gbDatos.Controls.Add(this.label7);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label6);
            this.gbDatos.Controls.Add(this.label5);
            this.gbDatos.Controls.Add(this.txtCodigo);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(579, 351);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la Cocina";
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(123, 260);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(201, 21);
            this.cbEstado.TabIndex = 13;
            // 
            // cbTerminacion
            // 
            this.cbTerminacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTerminacion.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbTerminacion.FormattingEnabled = true;
            this.cbTerminacion.Location = new System.Drawing.Point(123, 224);
            this.cbTerminacion.Name = "cbTerminacion";
            this.cbTerminacion.Size = new System.Drawing.Size(201, 21);
            this.cbTerminacion.TabIndex = 12;
            // 
            // cbColor
            // 
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(123, 189);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(201, 21);
            this.cbColor.TabIndex = 11;
            // 
            // cbDesignacion
            // 
            this.cbDesignacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDesignacion.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbDesignacion.FormattingEnabled = true;
            this.cbDesignacion.Location = new System.Drawing.Point(123, 152);
            this.cbDesignacion.Name = "cbDesignacion";
            this.cbDesignacion.Size = new System.Drawing.Size(201, 21);
            this.cbDesignacion.TabIndex = 10;
            // 
            // cbMarca
            // 
            this.cbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarca.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(123, 114);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(201, 21);
            this.cbMarca.TabIndex = 9;
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Location = new System.Drawing.Point(123, 78);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(201, 21);
            this.cbModelo.TabIndex = 8;
            // 
            // gbImagen
            // 
            this.gbImagen.Controls.Add(this.btnZoomOut);
            this.gbImagen.Controls.Add(this.btnZoomIn);
            this.gbImagen.Controls.Add(this.btnQuitarImagen);
            this.gbImagen.Controls.Add(this.btnAbrirImagen);
            this.gbImagen.Controls.Add(this.pbImagen);
            this.gbImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbImagen.Location = new System.Drawing.Point(353, 43);
            this.gbImagen.Name = "gbImagen";
            this.gbImagen.Size = new System.Drawing.Size(214, 272);
            this.gbImagen.TabIndex = 21;
            this.gbImagen.TabStop = false;
            this.gbImagen.Text = "Imagen";
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
            this.btnQuitarImagen.Location = new System.Drawing.Point(42, 231);
            this.btnQuitarImagen.Name = "btnQuitarImagen";
            this.btnQuitarImagen.Size = new System.Drawing.Size(31, 31);
            this.btnQuitarImagen.TabIndex = 16;
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
            this.btnAbrirImagen.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Abrir_25;
            this.btnAbrirImagen.Location = new System.Drawing.Point(6, 231);
            this.btnAbrirImagen.Name = "btnAbrirImagen";
            this.btnAbrirImagen.Size = new System.Drawing.Size(31, 31);
            this.btnAbrirImagen.TabIndex = 15;
            this.btnAbrirImagen.UseVisualStyleBackColor = true;
            this.btnAbrirImagen.Click += new System.EventHandler(this.btnImagen_Click);
            this.btnAbrirImagen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnAbrirImagen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // pbImagen
            // 
            this.pbImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImagen.Location = new System.Drawing.Point(6, 25);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(200, 200);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagen.TabIndex = 0;
            this.pbImagen.TabStop = false;
            this.pbImagen.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbImagen_LoadCompleted);
            // 
            // nudCosto
            // 
            this.nudCosto.DecimalPlaces = 2;
            this.nudCosto.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudCosto.Location = new System.Drawing.Point(123, 295);
            this.nudCosto.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCosto.Name = "nudCosto";
            this.nudCosto.Size = new System.Drawing.Size(201, 20);
            this.nudCosto.TabIndex = 14;
            this.nudCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCosto.Enter += new System.EventHandler(this.nudCosto_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 227);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Terminación horno:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Marca:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Modelo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Estado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Código:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvListaCocina);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(9);
            this.groupBox2.Size = new System.Drawing.Size(579, 323);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Cocinas";
            // 
            // dgvListaCocina
            // 
            this.dgvListaCocina.AllowUserToAddRows = false;
            this.dgvListaCocina.AllowUserToDeleteRows = false;
            this.dgvListaCocina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaCocina.Location = new System.Drawing.Point(9, 22);
            this.dgvListaCocina.MultiSelect = false;
            this.dgvListaCocina.Name = "dgvListaCocina";
            this.dgvListaCocina.ReadOnly = true;
            this.dgvListaCocina.RowHeadersVisible = false;
            this.dgvListaCocina.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaCocina.Size = new System.Drawing.Size(561, 292);
            this.dgvListaCocina.TabIndex = 6;
            this.dgvListaCocina.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaCocina_RowEnter);
            this.dgvListaCocina.DoubleClick += new System.EventHandler(this.dgvListaCocina_DoubleClick);
            this.dgvListaCocina.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvListaCocina_CellFormatting);
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(585, 415);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMarcaBuscar);
            this.groupBox1.Controls.Add(this.cbEstadoBuscar);
            this.groupBox1.Controls.Add(this.cbTerminacionBuscar);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtCodigoBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // cbMarcaBuscar
            // 
            this.cbMarcaBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarcaBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMarcaBuscar.FormattingEnabled = true;
            this.cbMarcaBuscar.Location = new System.Drawing.Point(319, 22);
            this.cbMarcaBuscar.Name = "cbMarcaBuscar";
            this.cbMarcaBuscar.Size = new System.Drawing.Size(143, 21);
            this.cbMarcaBuscar.TabIndex = 10;
            // 
            // cbEstadoBuscar
            // 
            this.cbEstadoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEstadoBuscar.FormattingEnabled = true;
            this.cbEstadoBuscar.Location = new System.Drawing.Point(319, 53);
            this.cbEstadoBuscar.Name = "cbEstadoBuscar";
            this.cbEstadoBuscar.Size = new System.Drawing.Size(143, 21);
            this.cbEstadoBuscar.TabIndex = 9;
            // 
            // cbTerminacionBuscar
            // 
            this.cbTerminacionBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTerminacionBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbTerminacionBuscar.FormattingEnabled = true;
            this.cbTerminacionBuscar.Location = new System.Drawing.Point(92, 53);
            this.cbTerminacionBuscar.Name = "cbTerminacionBuscar";
            this.cbTerminacionBuscar.Size = new System.Drawing.Size(151, 21);
            this.cbTerminacionBuscar.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 26);
            this.label12.TabIndex = 7;
            this.label12.Text = "Terminación\r\nde horno:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(263, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Estado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Marca:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(492, 34);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCodigoBuscar
            // 
            this.txtCodigoBuscar.Location = new System.Drawing.Point(92, 22);
            this.txtCodigoBuscar.MaxLength = 80;
            this.txtCodigoBuscar.Name = "txtCodigoBuscar";
            this.txtCodigoBuscar.Size = new System.Drawing.Size(151, 20);
            this.txtCodigoBuscar.TabIndex = 1;
            this.txtCodigoBuscar.Enter += new System.EventHandler(this.txtCodigoBuscar_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código:";
            // 
            // tcCocina
            // 
            this.tcCocina.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcCocina.Controls.Add(this.tpBuscar);
            this.tcCocina.Controls.Add(this.tpDatos);
            this.tcCocina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCocina.ItemSize = new System.Drawing.Size(0, 1);
            this.tcCocina.Location = new System.Drawing.Point(2, 54);
            this.tcCocina.Margin = new System.Windows.Forms.Padding(0);
            this.tcCocina.Multiline = true;
            this.tcCocina.Name = "tcCocina";
            this.tcCocina.Padding = new System.Drawing.Point(0, 0);
            this.tcCocina.SelectedIndex = 0;
            this.tcCocina.Size = new System.Drawing.Size(593, 424);
            this.tcCocina.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcCocina.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcCocina, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(597, 480);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // ofdImagen
            // 
            this.ofdImagen.InitialDirectory = "d:\\";
            this.ofdImagen.Title = "Seleccione una imagen";
            this.ofdImagen.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdImagen_FileOk);
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
            this.btnZoomIn.Location = new System.Drawing.Point(140, 231);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(31, 31);
            this.btnZoomIn.TabIndex = 17;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            this.btnZoomIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnZoomIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
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
            this.btnZoomOut.Location = new System.Drawing.Point(170, 231);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(31, 31);
            this.btnZoomOut.TabIndex = 18;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            this.btnZoomOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnZoomOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // frmCocina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 480);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCocina";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Cocina";
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.gbGuardarCancelar.ResumeLayout(false);
            this.tpDatos.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.gbImagen.ResumeLayout(false);
            this.gbImagen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCosto)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCocina)).EndInit();
            this.tpBuscar.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcCocina.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvListaCocina;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtCodigoBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tcCocina;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbImagen;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.NumericUpDown nudCosto;
        private System.Windows.Forms.Button btnQuitarImagen;
        private System.Windows.Forms.Button btnAbrirImagen;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEstado;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbTerminacion;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbColor;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbDesignacion;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMarca;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbModelo;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMarcaBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEstadoBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbTerminacionBuscar;
        private System.Windows.Forms.OpenFileDialog ofdImagen;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
    }
}