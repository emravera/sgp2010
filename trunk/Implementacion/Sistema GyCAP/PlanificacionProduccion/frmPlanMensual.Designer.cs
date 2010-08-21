namespace GyCAP.UI.PlanificacionProduccion
{
    partial class frmPlanMensual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanMensual));
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.btnCargaDetalle = new System.Windows.Forms.Button();
            this.cbPlanAnual = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMesDatos = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gbGrillaDemanda = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMes = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtAnioBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbCantidades = new System.Windows.Forms.GroupBox();
            this.txtRestaPlanificar = new System.Windows.Forms.TextBox();
            this.txtCantPlanificada = new System.Windows.Forms.TextBox();
            this.txtCantAPlanificar = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gbDetalleGrilla = new System.Windows.Forms.GroupBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnSumar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gbCargaDetalle = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numUnidades = new System.Windows.Forms.NumericUpDown();
            this.numPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.rbPorcentaje = new System.Windows.Forms.RadioButton();
            this.rbUnidades = new System.Windows.Forms.RadioButton();
            this.cbCocinas = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label5 = new System.Windows.Forms.Label();
            this.gbBotones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.gbGrillaDetalle = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcPlanAnual = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gbDatosPrincipales.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.gbGrillaDemanda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbCantidades.SuspendLayout();
            this.gbDetalleGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.panelAcciones.SuspendLayout();
            this.gbCargaDetalle.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).BeginInit();
            this.gbBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.gbGrillaDetalle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcPlanAnual.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Controls.Add(this.btnCargaDetalle);
            this.gbDatosPrincipales.Controls.Add(this.cbPlanAnual);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Controls.Add(this.cbMesDatos);
            this.gbDatosPrincipales.Controls.Add(this.label4);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 3);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(721, 61);
            this.gbDatosPrincipales.TabIndex = 10;
            this.gbDatosPrincipales.TabStop = false;
            this.gbDatosPrincipales.Text = "Datos Principales";
            // 
            // btnCargaDetalle
            // 
            this.btnCargaDetalle.Location = new System.Drawing.Point(625, 19);
            this.btnCargaDetalle.Name = "btnCargaDetalle";
            this.btnCargaDetalle.Size = new System.Drawing.Size(82, 23);
            this.btnCargaDetalle.TabIndex = 25;
            this.btnCargaDetalle.Text = "Cargar Detalle";
            this.btnCargaDetalle.UseVisualStyleBackColor = true;
            this.btnCargaDetalle.Click += new System.EventHandler(this.btnCargaDetalle_Click);
            // 
            // cbPlanAnual
            // 
            this.cbPlanAnual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanAnual.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanAnual.FormattingEnabled = true;
            this.cbPlanAnual.Location = new System.Drawing.Point(166, 20);
            this.cbPlanAnual.Name = "cbPlanAnual";
            this.cbPlanAnual.Size = new System.Drawing.Size(94, 21);
            this.cbPlanAnual.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Plan Anual Planificación:";
            // 
            // cbMesDatos
            // 
            this.cbMesDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMesDatos.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMesDatos.FormattingEnabled = true;
            this.cbMesDatos.Location = new System.Drawing.Point(445, 20);
            this.cbMesDatos.Name = "cbMesDatos";
            this.cbMesDatos.Size = new System.Drawing.Size(137, 21);
            this.cbMesDatos.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mes Planificación:";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(43, 47);
            this.btnConsultar.Text = "&Buscar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.New_25;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 47);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
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
            this.tsMenu.Size = new System.Drawing.Size(736, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Text_Editor_25;
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(54, 47);
            this.btnModificar.Text = "&Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Delete_25;
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
            // gbGrillaDemanda
            // 
            this.gbGrillaDemanda.Controls.Add(this.dgvLista);
            this.gbGrillaDemanda.Location = new System.Drawing.Point(6, 75);
            this.gbGrillaDemanda.Name = "gbGrillaDemanda";
            this.gbGrillaDemanda.Size = new System.Drawing.Size(404, 320);
            this.gbGrillaDemanda.TabIndex = 1;
            this.gbGrillaDemanda.TabStop = false;
            this.gbGrillaDemanda.Text = "Listado de Planes Mensuales";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.Location = new System.Drawing.Point(10, 19);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(376, 290);
            this.dgvLista.TabIndex = 1;
            this.dgvLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtAnioBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // cbMes
            // 
            this.cbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMes.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMes.FormattingEnabled = true;
            this.cbMes.Location = new System.Drawing.Point(247, 20);
            this.cbMes.Name = "cbMes";
            this.cbMes.Size = new System.Drawing.Size(128, 21);
            this.cbMes.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mes:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.lupa_25;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(471, 18);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtAnioBuscar
            // 
            this.txtAnioBuscar.Location = new System.Drawing.Point(65, 22);
            this.txtAnioBuscar.Name = "txtAnioBuscar";
            this.txtAnioBuscar.Size = new System.Drawing.Size(97, 20);
            this.txtAnioBuscar.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Año:";
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbCantidades);
            this.tpDatos.Controls.Add(this.gbDetalleGrilla);
            this.tpDatos.Controls.Add(this.gbCargaDetalle);
            this.tpDatos.Controls.Add(this.gbBotones);
            this.tpDatos.Controls.Add(this.gbDatosPrincipales);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(728, 394);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbCantidades
            // 
            this.gbCantidades.Controls.Add(this.txtRestaPlanificar);
            this.gbCantidades.Controls.Add(this.txtCantPlanificada);
            this.gbCantidades.Controls.Add(this.txtCantAPlanificar);
            this.gbCantidades.Controls.Add(this.label10);
            this.gbCantidades.Controls.Add(this.label9);
            this.gbCantidades.Controls.Add(this.label8);
            this.gbCantidades.Location = new System.Drawing.Point(9, 72);
            this.gbCantidades.Name = "gbCantidades";
            this.gbCantidades.Size = new System.Drawing.Size(350, 98);
            this.gbCantidades.TabIndex = 18;
            this.gbCantidades.TabStop = false;
            this.gbCantidades.Text = "Cantidades a Planificar";
            // 
            // txtRestaPlanificar
            // 
            this.txtRestaPlanificar.Location = new System.Drawing.Point(142, 71);
            this.txtRestaPlanificar.Name = "txtRestaPlanificar";
            this.txtRestaPlanificar.Size = new System.Drawing.Size(135, 20);
            this.txtRestaPlanificar.TabIndex = 9;
            // 
            // txtCantPlanificada
            // 
            this.txtCantPlanificada.Location = new System.Drawing.Point(142, 45);
            this.txtCantPlanificada.Name = "txtCantPlanificada";
            this.txtCantPlanificada.Size = new System.Drawing.Size(135, 20);
            this.txtCantPlanificada.TabIndex = 8;
            // 
            // txtCantAPlanificar
            // 
            this.txtCantAPlanificar.Location = new System.Drawing.Point(142, 19);
            this.txtCantAPlanificar.Name = "txtCantAPlanificar";
            this.txtCantAPlanificar.Size = new System.Drawing.Size(135, 20);
            this.txtCantAPlanificar.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Resta Planificar:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Cantidad Planificada:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Cantidad a Planificar:";
            // 
            // gbDetalleGrilla
            // 
            this.gbDetalleGrilla.Controls.Add(this.dgvDatos);
            this.gbDetalleGrilla.Controls.Add(this.panelAcciones);
            this.gbDetalleGrilla.Location = new System.Drawing.Point(372, 70);
            this.gbDetalleGrilla.Name = "gbDetalleGrilla";
            this.gbDetalleGrilla.Size = new System.Drawing.Size(350, 271);
            this.gbDetalleGrilla.TabIndex = 17;
            this.gbDetalleGrilla.TabStop = false;
            this.gbDetalleGrilla.Text = "Detalle";
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(5, 24);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.Size = new System.Drawing.Size(268, 220);
            this.dgvDatos.TabIndex = 5;
            this.dgvDatos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDatos_CellFormatting);
            // 
            // panelAcciones
            // 
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.label7);
            this.panelAcciones.Controls.Add(this.btnRestar);
            this.panelAcciones.Controls.Add(this.btnSumar);
            this.panelAcciones.Controls.Add(this.label6);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Location = new System.Drawing.Point(279, 23);
            this.panelAcciones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(65, 221);
            this.panelAcciones.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Cantidades";
            // 
            // btnRestar
            // 
            this.btnRestar.FlatAppearance.BorderSize = 0;
            this.btnRestar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestar.Image = ((System.Drawing.Image)(resources.GetObject("btnRestar.Image")));
            this.btnRestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestar.Location = new System.Drawing.Point(19, 172);
            this.btnRestar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(30, 30);
            this.btnRestar.TabIndex = 19;
            this.btnRestar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestar.UseVisualStyleBackColor = true;
            // 
            // btnSumar
            // 
            this.btnSumar.FlatAppearance.BorderSize = 0;
            this.btnSumar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumar.Image = ((System.Drawing.Image)(resources.GetObject("btnSumar.Image")));
            this.btnSumar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSumar.Location = new System.Drawing.Point(19, 131);
            this.btnSumar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSumar.Name = "btnSumar";
            this.btnSumar.Size = new System.Drawing.Size(30, 30);
            this.btnSumar.TabIndex = 18;
            this.btnSumar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSumar.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Acciones";
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Delete_25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(19, 28);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // gbCargaDetalle
            // 
            this.gbCargaDetalle.Controls.Add(this.btnAgregar);
            this.gbCargaDetalle.Controls.Add(this.groupBox3);
            this.gbCargaDetalle.Controls.Add(this.cbCocinas);
            this.gbCargaDetalle.Controls.Add(this.label5);
            this.gbCargaDetalle.Location = new System.Drawing.Point(3, 176);
            this.gbCargaDetalle.Name = "gbCargaDetalle";
            this.gbCargaDetalle.Size = new System.Drawing.Size(359, 165);
            this.gbCargaDetalle.TabIndex = 16;
            this.gbCargaDetalle.TabStop = false;
            this.gbCargaDetalle.Text = "Carga Detalle del Plan Mensual";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(249, 136);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(104, 23);
            this.btnAgregar.TabIndex = 13;
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
            this.groupBox3.Location = new System.Drawing.Point(24, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 88);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cantidad";
            // 
            // numUnidades
            // 
            this.numUnidades.Location = new System.Drawing.Point(125, 19);
            this.numUnidades.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numUnidades.Name = "numUnidades";
            this.numUnidades.Size = new System.Drawing.Size(120, 20);
            this.numUnidades.TabIndex = 3;
            this.numUnidades.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numPorcentaje
            // 
            this.numPorcentaje.Location = new System.Drawing.Point(125, 51);
            this.numPorcentaje.Name = "numPorcentaje";
            this.numPorcentaje.Size = new System.Drawing.Size(120, 20);
            this.numPorcentaje.TabIndex = 2;
            this.numPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rbPorcentaje
            // 
            this.rbPorcentaje.AutoSize = true;
            this.rbPorcentaje.Location = new System.Drawing.Point(15, 51);
            this.rbPorcentaje.Name = "rbPorcentaje";
            this.rbPorcentaje.Size = new System.Drawing.Size(93, 17);
            this.rbPorcentaje.TabIndex = 1;
            this.rbPorcentaje.TabStop = true;
            this.rbPorcentaje.Text = "Porcentaje (%)";
            this.rbPorcentaje.UseVisualStyleBackColor = true;
            this.rbPorcentaje.CheckedChanged += new System.EventHandler(this.rbPorcentaje_CheckedChanged);
            // 
            // rbUnidades
            // 
            this.rbUnidades.AutoSize = true;
            this.rbUnidades.Location = new System.Drawing.Point(15, 20);
            this.rbUnidades.Name = "rbUnidades";
            this.rbUnidades.Size = new System.Drawing.Size(105, 17);
            this.rbUnidades.TabIndex = 0;
            this.rbUnidades.TabStop = true;
            this.rbUnidades.Text = "Unidades Fisicas";
            this.rbUnidades.UseVisualStyleBackColor = true;
            this.rbUnidades.CheckedChanged += new System.EventHandler(this.rbUnidades_CheckedChanged);
            // 
            // cbCocinas
            // 
            this.cbCocinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCocinas.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCocinas.FormattingEnabled = true;
            this.cbCocinas.Location = new System.Drawing.Point(105, 19);
            this.cbCocinas.Name = "cbCocinas";
            this.cbCocinas.Size = new System.Drawing.Size(212, 21);
            this.cbCocinas.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cocina a Producir:";
            // 
            // gbBotones
            // 
            this.gbBotones.Controls.Add(this.btnVolver);
            this.gbBotones.Controls.Add(this.btnGuardar);
            this.gbBotones.Location = new System.Drawing.Point(372, 347);
            this.gbBotones.Name = "gbBotones";
            this.gbBotones.Size = new System.Drawing.Size(350, 40);
            this.gbBotones.TabIndex = 13;
            this.gbBotones.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(279, 12);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 22);
            this.btnVolver.TabIndex = 22;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(209, 12);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 22);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.Enabled = false;
            this.dgvDetalle.Location = new System.Drawing.Point(11, 22);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(291, 290);
            this.dgvDetalle.TabIndex = 1;
            this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetalle_CellFormatting);
            // 
            // gbGrillaDetalle
            // 
            this.gbGrillaDetalle.Controls.Add(this.dgvDetalle);
            this.gbGrillaDetalle.Location = new System.Drawing.Point(416, 75);
            this.gbGrillaDetalle.Name = "gbGrillaDetalle";
            this.gbGrillaDetalle.Size = new System.Drawing.Size(308, 320);
            this.gbGrillaDetalle.TabIndex = 2;
            this.gbGrillaDetalle.TabStop = false;
            this.gbGrillaDetalle.Text = "Listado Detalle Planes Mensual";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(740, 459);
            this.tableLayoutPanel1.TabIndex = 14;
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
            this.tcPlanAnual.Size = new System.Drawing.Size(736, 403);
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
            this.tpBuscar.Size = new System.Drawing.Size(728, 394);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // frmPlanMensual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 459);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmPlanMensual";
            this.Text = "Plan Mensual";
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.gbGrillaDemanda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbCantidades.ResumeLayout(false);
            this.gbCantidades.PerformLayout();
            this.gbDetalleGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.gbCargaDetalle.ResumeLayout(false);
            this.gbCargaDetalle.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).EndInit();
            this.gbBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.gbGrillaDetalle.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcPlanAnual.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMesDatos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        public System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox gbGrillaDemanda;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtAnioBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbBotones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.GroupBox gbGrillaDetalle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcPlanAnual;
        private System.Windows.Forms.TabPage tpBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanAnual;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCargaDetalle;
        private System.Windows.Forms.GroupBox gbCargaDetalle;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCocinas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numPorcentaje;
        private System.Windows.Forms.RadioButton rbPorcentaje;
        private System.Windows.Forms.RadioButton rbUnidades;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnSumar;
        private System.Windows.Forms.GroupBox gbDetalleGrilla;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox gbCantidades;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMes;
        private System.Windows.Forms.TextBox txtRestaPlanificar;
        private System.Windows.Forms.TextBox txtCantPlanificada;
        private System.Windows.Forms.TextBox txtCantAPlanificar;
        private System.Windows.Forms.NumericUpDown numUnidades;
    }
}