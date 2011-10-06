namespace GyCAP.UI.PlanificacionProduccion
{
    partial class frmPlanSemanal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanSemanal));
            this.gbCargaDetalle = new System.Windows.Forms.GroupBox();
            this.gbPlanMensual = new System.Windows.Forms.GroupBox();
            this.dgvPlanMensual = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numUnidades = new System.Windows.Forms.NumericUpDown();
            this.numPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.rbPorcentaje = new System.Windows.Forms.RadioButton();
            this.rbUnidades = new System.Windows.Forms.RadioButton();
            this.dtpFechaDia = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSemana = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnSumar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.cbSemanaDatos = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCargaDetalle = new System.Windows.Forms.Button();
            this.cbPlanAnual = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMesDatos = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.gbDetalleGrillaDatos = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.gbGrillaDetalle = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcPlanAnual = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gbGrillaDemanda = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAnio = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbSemana = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.cbMes = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbBotones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCapDiaria = new System.Windows.Forms.TextBox();
            this.gbCargaDetalle.SuspendLayout();
            this.gbPlanMensual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanMensual)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).BeginInit();
            this.panelAcciones.SuspendLayout();
            this.gbDatosPrincipales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.gbDetalleGrillaDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.gbGrillaDetalle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcPlanAnual.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.gbGrillaDemanda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbBotones.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCargaDetalle
            // 
            this.gbCargaDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbCargaDetalle.Controls.Add(this.txtCapDiaria);
            this.gbCargaDetalle.Controls.Add(this.label9);
            this.gbCargaDetalle.Controls.Add(this.btnAgregar);
            this.gbCargaDetalle.Controls.Add(this.gbPlanMensual);
            this.gbCargaDetalle.Controls.Add(this.groupBox3);
            this.gbCargaDetalle.Controls.Add(this.dtpFechaDia);
            this.gbCargaDetalle.Controls.Add(this.label8);
            this.gbCargaDetalle.Controls.Add(this.txtSemana);
            this.gbCargaDetalle.Controls.Add(this.label5);
            this.gbCargaDetalle.Location = new System.Drawing.Point(3, 70);
            this.gbCargaDetalle.Name = "gbCargaDetalle";
            this.gbCargaDetalle.Size = new System.Drawing.Size(392, 387);
            this.gbCargaDetalle.TabIndex = 16;
            this.gbCargaDetalle.TabStop = false;
            this.gbCargaDetalle.Text = "Carga de Día del Plan Semanal";
            // 
            // gbPlanMensual
            // 
            this.gbPlanMensual.Controls.Add(this.dgvPlanMensual);
            this.gbPlanMensual.Location = new System.Drawing.Point(4, 74);
            this.gbPlanMensual.Name = "gbPlanMensual";
            this.gbPlanMensual.Size = new System.Drawing.Size(379, 206);
            this.gbPlanMensual.TabIndex = 16;
            this.gbPlanMensual.TabStop = false;
            this.gbPlanMensual.Text = "Plan Mensual";
            // 
            // dgvPlanMensual
            // 
            this.dgvPlanMensual.AllowUserToAddRows = false;
            this.dgvPlanMensual.AllowUserToDeleteRows = false;
            this.dgvPlanMensual.AllowUserToResizeColumns = false;
            this.dgvPlanMensual.AllowUserToResizeRows = false;
            this.dgvPlanMensual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanMensual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlanMensual.Location = new System.Drawing.Point(3, 17);
            this.dgvPlanMensual.Name = "dgvPlanMensual";
            this.dgvPlanMensual.ReadOnly = true;
            this.dgvPlanMensual.RowHeadersVisible = false;
            this.dgvPlanMensual.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPlanMensual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanMensual.Size = new System.Drawing.Size(373, 186);
            this.dgvPlanMensual.TabIndex = 7;
            this.dgvPlanMensual.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPlanMensual_CellFormatting);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.Location = new System.Drawing.Point(308, 355);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 26);
            this.btnAgregar.TabIndex = 9;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numUnidades);
            this.groupBox3.Controls.Add(this.numPorcentaje);
            this.groupBox3.Controls.Add(this.rbPorcentaje);
            this.groupBox3.Controls.Add(this.rbUnidades);
            this.groupBox3.Location = new System.Drawing.Point(4, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 67);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ingreso de Cantidades";
            // 
            // numUnidades
            // 
            this.numUnidades.Location = new System.Drawing.Point(178, 14);
            this.numUnidades.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numUnidades.Name = "numUnidades";
            this.numUnidades.Size = new System.Drawing.Size(120, 21);
            this.numUnidades.TabIndex = 7;
            this.numUnidades.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUnidades.Enter += new System.EventHandler(this.numUnidades_Enter);
            // 
            // numPorcentaje
            // 
            this.numPorcentaje.Location = new System.Drawing.Point(178, 40);
            this.numPorcentaje.Name = "numPorcentaje";
            this.numPorcentaje.Size = new System.Drawing.Size(120, 21);
            this.numPorcentaje.TabIndex = 8;
            this.numPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPorcentaje.Enter += new System.EventHandler(this.numPorcentaje_Enter);
            // 
            // rbPorcentaje
            // 
            this.rbPorcentaje.AutoSize = true;
            this.rbPorcentaje.Location = new System.Drawing.Point(73, 40);
            this.rbPorcentaje.Name = "rbPorcentaje";
            this.rbPorcentaje.Size = new System.Drawing.Size(99, 17);
            this.rbPorcentaje.TabIndex = 1;
            this.rbPorcentaje.TabStop = true;
            this.rbPorcentaje.Text = "Porcentaje (%)";
            this.rbPorcentaje.UseVisualStyleBackColor = true;
            this.rbPorcentaje.CheckedChanged += new System.EventHandler(this.rbPorcentaje_CheckedChanged);
            // 
            // rbUnidades
            // 
            this.rbUnidades.AutoSize = true;
            this.rbUnidades.Location = new System.Drawing.Point(73, 16);
            this.rbUnidades.Name = "rbUnidades";
            this.rbUnidades.Size = new System.Drawing.Size(103, 17);
            this.rbUnidades.TabIndex = 0;
            this.rbUnidades.TabStop = true;
            this.rbUnidades.Text = "Unidades Físicas";
            this.rbUnidades.UseVisualStyleBackColor = true;
            this.rbUnidades.CheckedChanged += new System.EventHandler(this.rbUnidades_CheckedChanged);
            // 
            // dtpFechaDia
            // 
            this.dtpFechaDia.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaDia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDia.Location = new System.Drawing.Point(291, 20);
            this.dtpFechaDia.Name = "dtpFechaDia";
            this.dtpFechaDia.Size = new System.Drawing.Size(89, 21);
            this.dtpFechaDia.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Día Planificado:";
            // 
            // txtSemana
            // 
            this.txtSemana.Location = new System.Drawing.Point(128, 21);
            this.txtSemana.Name = "txtSemana";
            this.txtSemana.Size = new System.Drawing.Size(81, 21);
            this.txtSemana.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Semana Planificada:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(235, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Cantidades";
            // 
            // panelAcciones
            // 
            this.panelAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.label7);
            this.panelAcciones.Controls.Add(this.btnRestar);
            this.panelAcciones.Controls.Add(this.btnSumar);
            this.panelAcciones.Controls.Add(this.label6);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Location = new System.Drawing.Point(3, 282);
            this.panelAcciones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(346, 57);
            this.panelAcciones.TabIndex = 12;
            // 
            // btnRestar
            // 
            this.btnRestar.FlatAppearance.BorderSize = 0;
            this.btnRestar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestar.Image = ((System.Drawing.Image)(resources.GetObject("btnRestar.Image")));
            this.btnRestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestar.Location = new System.Drawing.Point(275, 18);
            this.btnRestar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(30, 30);
            this.btnRestar.TabIndex = 19;
            this.btnRestar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            this.btnRestar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnRestar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // btnSumar
            // 
            this.btnSumar.FlatAppearance.BorderSize = 0;
            this.btnSumar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumar.Image = ((System.Drawing.Image)(resources.GetObject("btnSumar.Image")));
            this.btnSumar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSumar.Location = new System.Drawing.Point(227, 18);
            this.btnSumar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSumar.Name = "btnSumar";
            this.btnSumar.Size = new System.Drawing.Size(30, 30);
            this.btnSumar.TabIndex = 18;
            this.btnSumar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSumar.UseVisualStyleBackColor = true;
            this.btnSumar.Click += new System.EventHandler(this.btnSumar_Click);
            this.btnSumar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnSumar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Eliminar Seleccionado";
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(66, 16);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.btnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatosPrincipales.Controls.Add(this.cbSemanaDatos);
            this.gbDatosPrincipales.Controls.Add(this.label12);
            this.gbDatosPrincipales.Controls.Add(this.btnCargaDetalle);
            this.gbDatosPrincipales.Controls.Add(this.cbPlanAnual);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Controls.Add(this.cbMesDatos);
            this.gbDatosPrincipales.Controls.Add(this.label4);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 3);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(753, 61);
            this.gbDatosPrincipales.TabIndex = 10;
            this.gbDatosPrincipales.TabStop = false;
            this.gbDatosPrincipales.Text = "Datos Principales";
            // 
            // cbSemanaDatos
            // 
            this.cbSemanaDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemanaDatos.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbSemanaDatos.FormattingEnabled = true;
            this.cbSemanaDatos.Location = new System.Drawing.Point(520, 20);
            this.cbSemanaDatos.Name = "cbSemanaDatos";
            this.cbSemanaDatos.Size = new System.Drawing.Size(85, 21);
            this.cbSemanaDatos.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(474, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Semana:";
            // 
            // btnCargaDetalle
            // 
            this.btnCargaDetalle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCargaDetalle.Location = new System.Drawing.Point(657, 16);
            this.btnCargaDetalle.Name = "btnCargaDetalle";
            this.btnCargaDetalle.Size = new System.Drawing.Size(75, 26);
            this.btnCargaDetalle.TabIndex = 4;
            this.btnCargaDetalle.Text = "Cargar Detalle";
            this.btnCargaDetalle.UseVisualStyleBackColor = true;
            this.btnCargaDetalle.Click += new System.EventHandler(this.btnCargaDetalle_Click);
            // 
            // cbPlanAnual
            // 
            this.cbPlanAnual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanAnual.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanAnual.FormattingEnabled = true;
            this.cbPlanAnual.Location = new System.Drawing.Point(132, 21);
            this.cbPlanAnual.Name = "cbPlanAnual";
            this.cbPlanAnual.Size = new System.Drawing.Size(94, 21);
            this.cbPlanAnual.TabIndex = 1;
            this.cbPlanAnual.DropDownClosed += new System.EventHandler(this.cbPlanAnual_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Plan Anual Planificación:";
            // 
            // cbMesDatos
            // 
            this.cbMesDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMesDatos.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMesDatos.FormattingEnabled = true;
            this.cbMesDatos.Location = new System.Drawing.Point(333, 20);
            this.cbMesDatos.Name = "cbMesDatos";
            this.cbMesDatos.Size = new System.Drawing.Size(121, 21);
            this.cbMesDatos.TabIndex = 2;
            this.cbMesDatos.DropDownClosed += new System.EventHandler(this.cbMesDatos_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mes Planificación:";
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDatos.Location = new System.Drawing.Point(3, 17);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(349, 260);
            this.dgvDatos.TabIndex = 5;
            this.dgvDatos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDatos_CellFormatting);
            this.dgvDatos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDatos_DataBindingComplete);
            // 
            // gbDetalleGrillaDatos
            // 
            this.gbDetalleGrillaDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetalleGrillaDatos.Controls.Add(this.panelAcciones);
            this.gbDetalleGrillaDatos.Controls.Add(this.dgvDatos);
            this.gbDetalleGrillaDatos.Location = new System.Drawing.Point(401, 70);
            this.gbDetalleGrillaDatos.Name = "gbDetalleGrillaDatos";
            this.gbDetalleGrillaDatos.Size = new System.Drawing.Size(355, 343);
            this.gbDetalleGrillaDatos.TabIndex = 17;
            this.gbDetalleGrillaDatos.TabStop = false;
            this.gbDetalleGrillaDatos.Text = "Detalle Día Plan Semanal";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.Enabled = false;
            this.dgvDetalle.Location = new System.Drawing.Point(3, 17);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(353, 362);
            this.dgvDetalle.TabIndex = 1;
            this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetalle_CellFormatting);
            this.dgvDetalle.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDetalle_DataBindingComplete);
            // 
            // gbGrillaDetalle
            // 
            this.gbGrillaDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGrillaDetalle.Controls.Add(this.dgvDetalle);
            this.gbGrillaDetalle.Location = new System.Drawing.Point(397, 75);
            this.gbGrillaDetalle.Name = "gbGrillaDetalle";
            this.gbGrillaDetalle.Size = new System.Drawing.Size(359, 382);
            this.gbGrillaDetalle.TabIndex = 2;
            this.gbGrillaDetalle.TabStop = false;
            this.gbGrillaDetalle.Text = "Detalle del día del Plan Semanal";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcPlanAnual, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(774, 528);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tcPlanAnual
            // 
            this.tcPlanAnual.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcPlanAnual.Controls.Add(this.tpBuscar);
            this.tcPlanAnual.Controls.Add(this.tpDatos);
            this.tcPlanAnual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPlanAnual.ItemSize = new System.Drawing.Size(0, 1);
            this.tcPlanAnual.Location = new System.Drawing.Point(2, 54);
            this.tcPlanAnual.Margin = new System.Windows.Forms.Padding(0);
            this.tcPlanAnual.Multiline = true;
            this.tcPlanAnual.Name = "tcPlanAnual";
            this.tcPlanAnual.Padding = new System.Drawing.Point(0, 0);
            this.tcPlanAnual.SelectedIndex = 0;
            this.tcPlanAnual.Size = new System.Drawing.Size(770, 472);
            this.tcPlanAnual.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcPlanAnual.TabIndex = 8;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.gbGrillaDetalle);
            this.tpBuscar.Controls.Add(this.gbGrillaDemanda);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(762, 463);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // gbGrillaDemanda
            // 
            this.gbGrillaDemanda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbGrillaDemanda.Controls.Add(this.dgvLista);
            this.gbGrillaDemanda.Location = new System.Drawing.Point(6, 75);
            this.gbGrillaDemanda.Name = "gbGrillaDemanda";
            this.gbGrillaDemanda.Size = new System.Drawing.Size(385, 382);
            this.gbGrillaDemanda.TabIndex = 1;
            this.gbGrillaDemanda.TabStop = false;
            this.gbGrillaDemanda.Text = "Días Planificados en el  Plan Semanal";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(3, 17);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(379, 362);
            this.dgvLista.TabIndex = 1;
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting);
            this.dgvLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_CellClick);
            this.dgvLista.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLista_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAnio);
            this.groupBox1.Controls.Add(this.cbSemana);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cbMes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // cbAnio
            // 
            this.cbAnio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnio.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbAnio.FormattingEnabled = true;
            this.cbAnio.Location = new System.Drawing.Point(65, 25);
            this.cbAnio.Name = "cbAnio";
            this.cbAnio.Size = new System.Drawing.Size(115, 21);
            this.cbAnio.TabIndex = 9;
            this.cbAnio.DropDownClosed += new System.EventHandler(this.cbAnio_DropDownClosed);
            // 
            // cbSemana
            // 
            this.cbSemana.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemana.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbSemana.FormattingEnabled = true;
            this.cbSemana.Location = new System.Drawing.Point(467, 25);
            this.cbSemana.Name = "cbSemana";
            this.cbSemana.Size = new System.Drawing.Size(84, 21);
            this.cbSemana.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(419, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Semana:";
            // 
            // cbMes
            // 
            this.cbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMes.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMes.FormattingEnabled = true;
            this.cbMes.Location = new System.Drawing.Point(257, 26);
            this.cbMes.Name = "cbMes";
            this.cbMes.Size = new System.Drawing.Size(128, 21);
            this.cbMes.TabIndex = 3;
            this.cbMes.DropDownClosed += new System.EventHandler(this.cbMes_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mes:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBuscar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(650, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(73, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Año:";
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbDetalleGrillaDatos);
            this.tpDatos.Controls.Add(this.gbCargaDetalle);
            this.tpDatos.Controls.Add(this.gbBotones);
            this.tpDatos.Controls.Add(this.gbDatosPrincipales);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(762, 463);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbBotones
            // 
            this.gbBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBotones.Controls.Add(this.btnVolver);
            this.gbBotones.Controls.Add(this.btnGuardar);
            this.gbBotones.Location = new System.Drawing.Point(401, 414);
            this.gbBotones.Name = "gbBotones";
            this.gbBotones.Size = new System.Drawing.Size(355, 43);
            this.gbBotones.TabIndex = 13;
            this.gbBotones.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(268, 11);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 26);
            this.btnVolver.TabIndex = 13;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardar.Location = new System.Drawing.Point(187, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 26);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar";
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
            this.tsMenu.Size = new System.Drawing.Size(770, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
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
            this.btnConsultar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.lupa_25;
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
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
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
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
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
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Capacidad Diaria Planta:";
            // 
            // txtCapDiaria
            // 
            this.txtCapDiaria.Enabled = false;
            this.txtCapDiaria.Location = new System.Drawing.Point(128, 47);
            this.txtCapDiaria.Name = "txtCapDiaria";
            this.txtCapDiaria.Size = new System.Drawing.Size(81, 21);
            this.txtCapDiaria.TabIndex = 18;
            // 
            // frmPlanSemanal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 528);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPlanSemanal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Plan Semanal";
            this.gbCargaDetalle.ResumeLayout(false);
            this.gbCargaDetalle.PerformLayout();
            this.gbPlanMensual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanMensual)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.gbDetalleGrillaDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.gbGrillaDetalle.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcPlanAnual.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.gbGrillaDemanda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbBotones.ResumeLayout(false);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCargaDetalle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnSumar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private System.Windows.Forms.Button btnCargaDetalle;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanAnual;
        private System.Windows.Forms.Label label3;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMesDatos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox gbDetalleGrillaDatos;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.GroupBox gbGrillaDetalle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcPlanAnual;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox gbGrillaDemanda;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.GroupBox groupBox1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbBotones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ToolStrip tsMenu;
        public System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbSemana;
        private System.Windows.Forms.Label label11;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbSemanaDatos;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaDia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSemana;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbPlanMensual;
        private System.Windows.Forms.DataGridView dgvPlanMensual;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numUnidades;
        private System.Windows.Forms.NumericUpDown numPorcentaje;
        private System.Windows.Forms.RadioButton rbPorcentaje;
        private System.Windows.Forms.RadioButton rbUnidades;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbAnio;
        private System.Windows.Forms.TextBox txtCapDiaria;
        private System.Windows.Forms.Label label9;
    }
}