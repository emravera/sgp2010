namespace GyCAP.UI.ControlTrabajoEnProceso
{
    partial class frmControlPlanificacion
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tcPlanes = new System.Windows.Forms.TabControl();
            this.tpPlanSemanal = new System.Windows.Forms.TabPage();
            this.gbGraficaSemanal = new System.Windows.Forms.GroupBox();
            this.chartAvance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.cbPlanSemanal = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlanMensual = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlanAnual = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCargaPlanSemanal = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tpPlanMensual = new System.Windows.Forms.TabPage();
            this.gbDetallePlanAnual = new System.Windows.Forms.GroupBox();
            this.tcPlanAnual = new System.Windows.Forms.TabControl();
            this.tpMesesPlanAnual = new System.Windows.Forms.TabPage();
            this.btnGraficaPA = new System.Windows.Forms.Button();
            this.chTodosMeses = new System.Windows.Forms.CheckBox();
            this.btnDetalleMeses = new System.Windows.Forms.Button();
            this.dgvMesesPlanAnual = new System.Windows.Forms.DataGridView();
            this.tpDetalleMeses = new System.Windows.Forms.TabPage();
            this.btnVolver2 = new System.Windows.Forms.Button();
            this.dgvDetalleMensual = new System.Windows.Forms.DataGridView();
            this.gbDatosPlanAnual = new System.Windows.Forms.GroupBox();
            this.cbPlanAnual2 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.btnDatosPlanAnual = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gbGraficaPlanAnual = new System.Windows.Forms.GroupBox();
            this.btnVolverGrafico = new System.Windows.Forms.Button();
            this.chartPlanAnual = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.btnVolverPlanSemanal = new System.Windows.Forms.Button();
            this.gbDetallePlanSemanal = new System.Windows.Forms.GroupBox();
            this.tcDetalle = new System.Windows.Forms.TabControl();
            this.tpDias = new System.Windows.Forms.TabPage();
            this.chDetallesDiarios = new System.Windows.Forms.CheckBox();
            this.btnAvanceDiario = new System.Windows.Forms.Button();
            this.btnDetalleDiario = new System.Windows.Forms.Button();
            this.dgvDetallePlan = new System.Windows.Forms.DataGridView();
            this.tpDetalleDiario = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvDetalleDiario = new System.Windows.Forms.DataGridView();
            this.tcPlanes.SuspendLayout();
            this.tpPlanSemanal.SuspendLayout();
            this.gbGraficaSemanal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAvance)).BeginInit();
            this.gbDatosPrincipales.SuspendLayout();
            this.tpPlanMensual.SuspendLayout();
            this.gbDetallePlanAnual.SuspendLayout();
            this.tcPlanAnual.SuspendLayout();
            this.tpMesesPlanAnual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesesPlanAnual)).BeginInit();
            this.tpDetalleMeses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleMensual)).BeginInit();
            this.gbDatosPlanAnual.SuspendLayout();
            this.gbGraficaPlanAnual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPlanAnual)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.gbDetallePlanSemanal.SuspendLayout();
            this.tcDetalle.SuspendLayout();
            this.tpDias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePlan)).BeginInit();
            this.tpDetalleDiario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleDiario)).BeginInit();
            this.SuspendLayout();
            // 
            // tcPlanes
            // 
            this.tcPlanes.Controls.Add(this.tpPlanSemanal);
            this.tcPlanes.Controls.Add(this.tpPlanMensual);
            this.tcPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPlanes.Location = new System.Drawing.Point(3, 53);
            this.tcPlanes.Name = "tcPlanes";
            this.tcPlanes.SelectedIndex = 0;
            this.tcPlanes.Size = new System.Drawing.Size(786, 440);
            this.tcPlanes.TabIndex = 0;
            // 
            // tpPlanSemanal
            // 
            this.tpPlanSemanal.BackColor = System.Drawing.SystemColors.Control;
            this.tpPlanSemanal.Controls.Add(this.gbDetallePlanSemanal);
            this.tpPlanSemanal.Controls.Add(this.gbDatosPrincipales);
            this.tpPlanSemanal.Controls.Add(this.gbGraficaSemanal);
            this.tpPlanSemanal.Location = new System.Drawing.Point(4, 22);
            this.tpPlanSemanal.Name = "tpPlanSemanal";
            this.tpPlanSemanal.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlanSemanal.Size = new System.Drawing.Size(778, 414);
            this.tpPlanSemanal.TabIndex = 0;
            this.tpPlanSemanal.Text = "Plan Semanal";
            // 
            // gbGraficaSemanal
            // 
            this.gbGraficaSemanal.Controls.Add(this.btnVolverPlanSemanal);
            this.gbGraficaSemanal.Controls.Add(this.chartAvance);
            this.gbGraficaSemanal.Location = new System.Drawing.Point(3, 80);
            this.gbGraficaSemanal.Name = "gbGraficaSemanal";
            this.gbGraficaSemanal.Size = new System.Drawing.Size(769, 328);
            this.gbGraficaSemanal.TabIndex = 16;
            this.gbGraficaSemanal.TabStop = false;
            this.gbGraficaSemanal.Text = "Gráfica de Avance del Plan";
            // 
            // chartAvance
            // 
            chartArea7.Name = "ChartArea1";
            this.chartAvance.ChartAreas.Add(chartArea7);
            legend7.Name = "LeyendaPS";
            this.chartAvance.Legends.Add(legend7);
            this.chartAvance.Location = new System.Drawing.Point(32, 19);
            this.chartAvance.Name = "chartAvance";
            this.chartAvance.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series7.ChartArea = "ChartArea1";
            series7.Legend = "LeyendaPS";
            series7.Name = "Series1";
            this.chartAvance.Series.Add(series7);
            this.chartAvance.Size = new System.Drawing.Size(719, 270);
            this.chartAvance.TabIndex = 1;
            this.chartAvance.Text = "chart1";
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Controls.Add(this.cbPlanSemanal);
            this.gbDatosPrincipales.Controls.Add(this.cbPlanMensual);
            this.gbDatosPrincipales.Controls.Add(this.cbPlanAnual);
            this.gbDatosPrincipales.Controls.Add(this.label12);
            this.gbDatosPrincipales.Controls.Add(this.btnCargaPlanSemanal);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Controls.Add(this.label4);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 17);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(769, 57);
            this.gbDatosPrincipales.TabIndex = 14;
            this.gbDatosPrincipales.TabStop = false;
            this.gbDatosPrincipales.Text = "Datos Principales";
            // 
            // cbPlanSemanal
            // 
            this.cbPlanSemanal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanSemanal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanSemanal.FormattingEnabled = true;
            this.cbPlanSemanal.Location = new System.Drawing.Point(511, 22);
            this.cbPlanSemanal.Name = "cbPlanSemanal";
            this.cbPlanSemanal.Size = new System.Drawing.Size(95, 21);
            this.cbPlanSemanal.TabIndex = 30;
            // 
            // cbPlanMensual
            // 
            this.cbPlanMensual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanMensual.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanMensual.FormattingEnabled = true;
            this.cbPlanMensual.Location = new System.Drawing.Point(355, 22);
            this.cbPlanMensual.Name = "cbPlanMensual";
            this.cbPlanMensual.Size = new System.Drawing.Size(95, 21);
            this.cbPlanMensual.TabIndex = 29;
            this.cbPlanMensual.DropDownClosed += new System.EventHandler(this.cbPlanMensual_DropDownClosed);
            // 
            // cbPlanAnual
            // 
            this.cbPlanAnual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanAnual.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanAnual.FormattingEnabled = true;
            this.cbPlanAnual.Location = new System.Drawing.Point(144, 21);
            this.cbPlanAnual.Name = "cbPlanAnual";
            this.cbPlanAnual.Size = new System.Drawing.Size(95, 21);
            this.cbPlanAnual.TabIndex = 28;
            this.cbPlanAnual.DropDownClosed += new System.EventHandler(this.cbPlanAnual_DropDownClosed);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(456, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Semana:";
            // 
            // btnCargaPlanSemanal
            // 
            this.btnCargaPlanSemanal.Location = new System.Drawing.Point(681, 20);
            this.btnCargaPlanSemanal.Name = "btnCargaPlanSemanal";
            this.btnCargaPlanSemanal.Size = new System.Drawing.Size(82, 23);
            this.btnCargaPlanSemanal.TabIndex = 4;
            this.btnCargaPlanSemanal.Text = "Ver Datos ";
            this.btnCargaPlanSemanal.UseVisualStyleBackColor = true;
            this.btnCargaPlanSemanal.Click += new System.EventHandler(this.btnCargaDetalle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Plan Anual Planificación:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mes Planificación:";
            // 
            // tpPlanMensual
            // 
            this.tpPlanMensual.BackColor = System.Drawing.SystemColors.Control;
            this.tpPlanMensual.Controls.Add(this.gbDetallePlanAnual);
            this.tpPlanMensual.Controls.Add(this.gbDatosPlanAnual);
            this.tpPlanMensual.Controls.Add(this.gbGraficaPlanAnual);
            this.tpPlanMensual.Location = new System.Drawing.Point(4, 22);
            this.tpPlanMensual.Name = "tpPlanMensual";
            this.tpPlanMensual.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlanMensual.Size = new System.Drawing.Size(778, 414);
            this.tpPlanMensual.TabIndex = 1;
            this.tpPlanMensual.Text = "Plan Mensual y Anual";
            // 
            // gbDetallePlanAnual
            // 
            this.gbDetallePlanAnual.Controls.Add(this.tcPlanAnual);
            this.gbDetallePlanAnual.Location = new System.Drawing.Point(6, 74);
            this.gbDetallePlanAnual.Name = "gbDetallePlanAnual";
            this.gbDetallePlanAnual.Size = new System.Drawing.Size(375, 334);
            this.gbDetallePlanAnual.TabIndex = 20;
            this.gbDetallePlanAnual.TabStop = false;
            this.gbDetallePlanAnual.Text = "Detalle de Plan Anual";
            // 
            // tcPlanAnual
            // 
            this.tcPlanAnual.Controls.Add(this.tpMesesPlanAnual);
            this.tcPlanAnual.Controls.Add(this.tpDetalleMeses);
            this.tcPlanAnual.Location = new System.Drawing.Point(9, 19);
            this.tcPlanAnual.Name = "tcPlanAnual";
            this.tcPlanAnual.SelectedIndex = 0;
            this.tcPlanAnual.Size = new System.Drawing.Size(360, 304);
            this.tcPlanAnual.TabIndex = 6;
            // 
            // tpMesesPlanAnual
            // 
            this.tpMesesPlanAnual.Controls.Add(this.btnGraficaPA);
            this.tpMesesPlanAnual.Controls.Add(this.chTodosMeses);
            this.tpMesesPlanAnual.Controls.Add(this.btnDetalleMeses);
            this.tpMesesPlanAnual.Controls.Add(this.dgvMesesPlanAnual);
            this.tpMesesPlanAnual.Location = new System.Drawing.Point(4, 22);
            this.tpMesesPlanAnual.Name = "tpMesesPlanAnual";
            this.tpMesesPlanAnual.Padding = new System.Windows.Forms.Padding(3);
            this.tpMesesPlanAnual.Size = new System.Drawing.Size(352, 278);
            this.tpMesesPlanAnual.TabIndex = 0;
            this.tpMesesPlanAnual.Text = "Meses Plan";
            this.tpMesesPlanAnual.UseVisualStyleBackColor = true;
            // 
            // btnGraficaPA
            // 
            this.btnGraficaPA.Location = new System.Drawing.Point(176, 245);
            this.btnGraficaPA.Name = "btnGraficaPA";
            this.btnGraficaPA.Size = new System.Drawing.Size(82, 23);
            this.btnGraficaPA.TabIndex = 9;
            this.btnGraficaPA.Text = "Ver Gráfica";
            this.btnGraficaPA.UseVisualStyleBackColor = true;
            this.btnGraficaPA.Click += new System.EventHandler(this.btnGraficaPA_Click);
            // 
            // chTodosMeses
            // 
            this.chTodosMeses.AutoSize = true;
            this.chTodosMeses.Location = new System.Drawing.Point(6, 247);
            this.chTodosMeses.Name = "chTodosMeses";
            this.chTodosMeses.Size = new System.Drawing.Size(105, 17);
            this.chTodosMeses.TabIndex = 8;
            this.chTodosMeses.Text = "Todos los meses";
            this.chTodosMeses.UseVisualStyleBackColor = true;
            // 
            // btnDetalleMeses
            // 
            this.btnDetalleMeses.Location = new System.Drawing.Point(264, 245);
            this.btnDetalleMeses.Name = "btnDetalleMeses";
            this.btnDetalleMeses.Size = new System.Drawing.Size(82, 23);
            this.btnDetalleMeses.TabIndex = 5;
            this.btnDetalleMeses.Text = "Ver Detalle";
            this.btnDetalleMeses.UseVisualStyleBackColor = true;
            this.btnDetalleMeses.Click += new System.EventHandler(this.btnDetalleMeses_Click);
            // 
            // dgvMesesPlanAnual
            // 
            this.dgvMesesPlanAnual.AllowUserToAddRows = false;
            this.dgvMesesPlanAnual.AllowUserToDeleteRows = false;
            this.dgvMesesPlanAnual.AllowUserToResizeRows = false;
            this.dgvMesesPlanAnual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMesesPlanAnual.Location = new System.Drawing.Point(4, 6);
            this.dgvMesesPlanAnual.MultiSelect = false;
            this.dgvMesesPlanAnual.Name = "dgvMesesPlanAnual";
            this.dgvMesesPlanAnual.RowHeadersVisible = false;
            this.dgvMesesPlanAnual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMesesPlanAnual.Size = new System.Drawing.Size(342, 233);
            this.dgvMesesPlanAnual.TabIndex = 1;
            // 
            // tpDetalleMeses
            // 
            this.tpDetalleMeses.Controls.Add(this.btnVolver2);
            this.tpDetalleMeses.Controls.Add(this.dgvDetalleMensual);
            this.tpDetalleMeses.Location = new System.Drawing.Point(4, 22);
            this.tpDetalleMeses.Name = "tpDetalleMeses";
            this.tpDetalleMeses.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetalleMeses.Size = new System.Drawing.Size(352, 278);
            this.tpDetalleMeses.TabIndex = 1;
            this.tpDetalleMeses.Text = "Detalle Meses";
            this.tpDetalleMeses.UseVisualStyleBackColor = true;
            // 
            // btnVolver2
            // 
            this.btnVolver2.Location = new System.Drawing.Point(264, 249);
            this.btnVolver2.Name = "btnVolver2";
            this.btnVolver2.Size = new System.Drawing.Size(82, 23);
            this.btnVolver2.TabIndex = 6;
            this.btnVolver2.Text = "Volver";
            this.btnVolver2.UseVisualStyleBackColor = true;
            this.btnVolver2.Click += new System.EventHandler(this.btnVolver2_Click);
            // 
            // dgvDetalleMensual
            // 
            this.dgvDetalleMensual.AllowUserToAddRows = false;
            this.dgvDetalleMensual.AllowUserToDeleteRows = false;
            this.dgvDetalleMensual.AllowUserToResizeColumns = false;
            this.dgvDetalleMensual.AllowUserToResizeRows = false;
            this.dgvDetalleMensual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleMensual.Location = new System.Drawing.Point(4, 6);
            this.dgvDetalleMensual.MultiSelect = false;
            this.dgvDetalleMensual.Name = "dgvDetalleMensual";
            this.dgvDetalleMensual.RowHeadersVisible = false;
            this.dgvDetalleMensual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleMensual.Size = new System.Drawing.Size(342, 237);
            this.dgvDetalleMensual.TabIndex = 2;
            // 
            // gbDatosPlanAnual
            // 
            this.gbDatosPlanAnual.Controls.Add(this.cbPlanAnual2);
            this.gbDatosPlanAnual.Controls.Add(this.btnDatosPlanAnual);
            this.gbDatosPlanAnual.Controls.Add(this.label2);
            this.gbDatosPlanAnual.Location = new System.Drawing.Point(5, 11);
            this.gbDatosPlanAnual.Name = "gbDatosPlanAnual";
            this.gbDatosPlanAnual.Size = new System.Drawing.Size(721, 57);
            this.gbDatosPlanAnual.TabIndex = 17;
            this.gbDatosPlanAnual.TabStop = false;
            this.gbDatosPlanAnual.Text = "Datos Principales";
            // 
            // cbPlanAnual2
            // 
            this.cbPlanAnual2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanAnual2.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanAnual2.FormattingEnabled = true;
            this.cbPlanAnual2.Location = new System.Drawing.Point(144, 21);
            this.cbPlanAnual2.Name = "cbPlanAnual2";
            this.cbPlanAnual2.Size = new System.Drawing.Size(95, 21);
            this.cbPlanAnual2.TabIndex = 28;
            // 
            // btnDatosPlanAnual
            // 
            this.btnDatosPlanAnual.Location = new System.Drawing.Point(626, 20);
            this.btnDatosPlanAnual.Name = "btnDatosPlanAnual";
            this.btnDatosPlanAnual.Size = new System.Drawing.Size(82, 23);
            this.btnDatosPlanAnual.TabIndex = 4;
            this.btnDatosPlanAnual.Text = "Ver Datos ";
            this.btnDatosPlanAnual.UseVisualStyleBackColor = true;
            this.btnDatosPlanAnual.Click += new System.EventHandler(this.btnDatosPlanAnual_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Plan Anual Planificación:";
            // 
            // gbGraficaPlanAnual
            // 
            this.gbGraficaPlanAnual.Controls.Add(this.btnVolverGrafico);
            this.gbGraficaPlanAnual.Controls.Add(this.chartPlanAnual);
            this.gbGraficaPlanAnual.Location = new System.Drawing.Point(6, 74);
            this.gbGraficaPlanAnual.Name = "gbGraficaPlanAnual";
            this.gbGraficaPlanAnual.Size = new System.Drawing.Size(794, 335);
            this.gbGraficaPlanAnual.TabIndex = 19;
            this.gbGraficaPlanAnual.TabStop = false;
            this.gbGraficaPlanAnual.Text = "Gráfica de Avance del Plan";
            // 
            // btnVolverGrafico
            // 
            this.btnVolverGrafico.Location = new System.Drawing.Point(684, 306);
            this.btnVolverGrafico.Name = "btnVolverGrafico";
            this.btnVolverGrafico.Size = new System.Drawing.Size(82, 23);
            this.btnVolverGrafico.TabIndex = 20;
            this.btnVolverGrafico.Text = "Volver";
            this.btnVolverGrafico.UseVisualStyleBackColor = true;
            this.btnVolverGrafico.Click += new System.EventHandler(this.btnVolverGrafico_Click);
            // 
            // chartPlanAnual
            // 
            chartArea8.Name = "ChartArea1";
            this.chartPlanAnual.ChartAreas.Add(chartArea8);
            legend8.Name = "LeyendaPA";
            this.chartPlanAnual.Legends.Add(legend8);
            this.chartPlanAnual.Location = new System.Drawing.Point(11, 19);
            this.chartPlanAnual.Name = "chartPlanAnual";
            this.chartPlanAnual.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series8.ChartArea = "ChartArea1";
            series8.Legend = "LeyendaPA";
            series8.Name = "Series1";
            this.chartPlanAnual.Series.Add(series8);
            this.chartPlanAnual.Size = new System.Drawing.Size(755, 281);
            this.chartPlanAnual.TabIndex = 1;
            this.chartPlanAnual.Text = "chart1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tcPlanes, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 496);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tsMenu
            // 
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(792, 50);
            this.tsMenu.TabIndex = 2;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.ControlTrabajoEnProceso.Properties.Resources.New_25;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 47);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
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
            // btnVolverPlanSemanal
            // 
            this.btnVolverPlanSemanal.Location = new System.Drawing.Point(669, 295);
            this.btnVolverPlanSemanal.Name = "btnVolverPlanSemanal";
            this.btnVolverPlanSemanal.Size = new System.Drawing.Size(82, 23);
            this.btnVolverPlanSemanal.TabIndex = 16;
            this.btnVolverPlanSemanal.Text = "Volver";
            this.btnVolverPlanSemanal.UseVisualStyleBackColor = true;
            this.btnVolverPlanSemanal.Click += new System.EventHandler(this.btnVolverPlanSemanal_Click);
            // 
            // gbDetallePlanSemanal
            // 
            this.gbDetallePlanSemanal.Controls.Add(this.tcDetalle);
            this.gbDetallePlanSemanal.Location = new System.Drawing.Point(3, 80);
            this.gbDetallePlanSemanal.Name = "gbDetallePlanSemanal";
            this.gbDetallePlanSemanal.Size = new System.Drawing.Size(375, 328);
            this.gbDetallePlanSemanal.TabIndex = 16;
            this.gbDetallePlanSemanal.TabStop = false;
            this.gbDetallePlanSemanal.Text = "Detalle de Plan";
            // 
            // tcDetalle
            // 
            this.tcDetalle.Controls.Add(this.tpDias);
            this.tcDetalle.Controls.Add(this.tpDetalleDiario);
            this.tcDetalle.Location = new System.Drawing.Point(9, 19);
            this.tcDetalle.Name = "tcDetalle";
            this.tcDetalle.SelectedIndex = 0;
            this.tcDetalle.Size = new System.Drawing.Size(360, 303);
            this.tcDetalle.TabIndex = 6;
            // 
            // tpDias
            // 
            this.tpDias.Controls.Add(this.chDetallesDiarios);
            this.tpDias.Controls.Add(this.btnAvanceDiario);
            this.tpDias.Controls.Add(this.btnDetalleDiario);
            this.tpDias.Controls.Add(this.dgvDetallePlan);
            this.tpDias.Location = new System.Drawing.Point(4, 22);
            this.tpDias.Name = "tpDias";
            this.tpDias.Padding = new System.Windows.Forms.Padding(3);
            this.tpDias.Size = new System.Drawing.Size(352, 277);
            this.tpDias.TabIndex = 0;
            this.tpDias.Text = "Dias Plan";
            this.tpDias.UseVisualStyleBackColor = true;
            // 
            // chDetallesDiarios
            // 
            this.chDetallesDiarios.AutoSize = true;
            this.chDetallesDiarios.Location = new System.Drawing.Point(29, 249);
            this.chDetallesDiarios.Name = "chDetallesDiarios";
            this.chDetallesDiarios.Size = new System.Drawing.Size(96, 17);
            this.chDetallesDiarios.TabIndex = 8;
            this.chDetallesDiarios.Text = "Todos los Dias";
            this.chDetallesDiarios.UseVisualStyleBackColor = true;
            // 
            // btnAvanceDiario
            // 
            this.btnAvanceDiario.Location = new System.Drawing.Point(176, 248);
            this.btnAvanceDiario.Name = "btnAvanceDiario";
            this.btnAvanceDiario.Size = new System.Drawing.Size(82, 23);
            this.btnAvanceDiario.TabIndex = 7;
            this.btnAvanceDiario.Text = "Ver Grafica";
            this.btnAvanceDiario.UseVisualStyleBackColor = true;
            this.btnAvanceDiario.Click += new System.EventHandler(this.btnAvanceDiario_Click);
            // 
            // btnDetalleDiario
            // 
            this.btnDetalleDiario.Location = new System.Drawing.Point(264, 248);
            this.btnDetalleDiario.Name = "btnDetalleDiario";
            this.btnDetalleDiario.Size = new System.Drawing.Size(82, 23);
            this.btnDetalleDiario.TabIndex = 5;
            this.btnDetalleDiario.Text = "Ver Detalle";
            this.btnDetalleDiario.UseVisualStyleBackColor = true;
            this.btnDetalleDiario.Click += new System.EventHandler(this.btnDetalleDiario_Click);
            // 
            // dgvDetallePlan
            // 
            this.dgvDetallePlan.AllowUserToAddRows = false;
            this.dgvDetallePlan.AllowUserToDeleteRows = false;
            this.dgvDetallePlan.AllowUserToResizeRows = false;
            this.dgvDetallePlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePlan.Location = new System.Drawing.Point(4, 6);
            this.dgvDetallePlan.MultiSelect = false;
            this.dgvDetallePlan.Name = "dgvDetallePlan";
            this.dgvDetallePlan.RowHeadersVisible = false;
            this.dgvDetallePlan.RowTemplate.ReadOnly = true;
            this.dgvDetallePlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetallePlan.Size = new System.Drawing.Size(342, 236);
            this.dgvDetallePlan.TabIndex = 1;
            // 
            // tpDetalleDiario
            // 
            this.tpDetalleDiario.Controls.Add(this.button1);
            this.tpDetalleDiario.Controls.Add(this.dgvDetalleDiario);
            this.tpDetalleDiario.Location = new System.Drawing.Point(4, 22);
            this.tpDetalleDiario.Name = "tpDetalleDiario";
            this.tpDetalleDiario.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetalleDiario.Size = new System.Drawing.Size(352, 277);
            this.tpDetalleDiario.TabIndex = 1;
            this.tpDetalleDiario.Text = "Detalle Diario";
            this.tpDetalleDiario.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dgvDetalleDiario
            // 
            this.dgvDetalleDiario.AllowUserToAddRows = false;
            this.dgvDetalleDiario.AllowUserToDeleteRows = false;
            this.dgvDetalleDiario.AllowUserToResizeColumns = false;
            this.dgvDetalleDiario.AllowUserToResizeRows = false;
            this.dgvDetalleDiario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleDiario.Location = new System.Drawing.Point(4, 6);
            this.dgvDetalleDiario.MultiSelect = false;
            this.dgvDetalleDiario.Name = "dgvDetalleDiario";
            this.dgvDetalleDiario.RowHeadersVisible = false;
            this.dgvDetalleDiario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleDiario.Size = new System.Drawing.Size(342, 236);
            this.dgvDetalleDiario.TabIndex = 2;
            // 
            // frmControlPlanificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 496);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmControlPlanificacion";
            this.Text = "Control Planificacion";
            this.tcPlanes.ResumeLayout(false);
            this.tpPlanSemanal.ResumeLayout(false);
            this.gbGraficaSemanal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAvance)).EndInit();
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.tpPlanMensual.ResumeLayout(false);
            this.gbDetallePlanAnual.ResumeLayout(false);
            this.tcPlanAnual.ResumeLayout(false);
            this.tpMesesPlanAnual.ResumeLayout(false);
            this.tpMesesPlanAnual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesesPlanAnual)).EndInit();
            this.tpDetalleMeses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleMensual)).EndInit();
            this.gbDatosPlanAnual.ResumeLayout(false);
            this.gbDatosPlanAnual.PerformLayout();
            this.gbGraficaPlanAnual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPlanAnual)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.gbDetallePlanSemanal.ResumeLayout(false);
            this.tcDetalle.ResumeLayout(false);
            this.tpDias.ResumeLayout(false);
            this.tpDias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePlan)).EndInit();
            this.tpDetalleDiario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleDiario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcPlanes;
        private System.Windows.Forms.TabPage tpPlanSemanal;
        private System.Windows.Forms.GroupBox gbGraficaSemanal;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAvance;
        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanSemanal;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanMensual;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanAnual;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCargaPlanSemanal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpPlanMensual;
        private System.Windows.Forms.GroupBox gbGraficaPlanAnual;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPlanAnual;
        private System.Windows.Forms.GroupBox gbDatosPlanAnual;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanAnual2;
        private System.Windows.Forms.Button btnDatosPlanAnual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Button btnVolverGrafico;
        private System.Windows.Forms.GroupBox gbDetallePlanAnual;
        private System.Windows.Forms.TabControl tcPlanAnual;
        private System.Windows.Forms.TabPage tpMesesPlanAnual;
        private System.Windows.Forms.Button btnGraficaPA;
        private System.Windows.Forms.CheckBox chTodosMeses;
        private System.Windows.Forms.Button btnDetalleMeses;
        private System.Windows.Forms.DataGridView dgvMesesPlanAnual;
        private System.Windows.Forms.TabPage tpDetalleMeses;
        private System.Windows.Forms.Button btnVolver2;
        private System.Windows.Forms.DataGridView dgvDetalleMensual;
        private System.Windows.Forms.Button btnVolverPlanSemanal;
        private System.Windows.Forms.GroupBox gbDetallePlanSemanal;
        private System.Windows.Forms.TabControl tcDetalle;
        private System.Windows.Forms.TabPage tpDias;
        private System.Windows.Forms.CheckBox chDetallesDiarios;
        private System.Windows.Forms.Button btnAvanceDiario;
        private System.Windows.Forms.Button btnDetalleDiario;
        private System.Windows.Forms.DataGridView dgvDetallePlan;
        private System.Windows.Forms.TabPage tpDetalleDiario;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvDetalleDiario;


    }
}