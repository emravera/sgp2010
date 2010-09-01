namespace GyCAP.UI.PlanificacionProduccion
{
    partial class frmGenerarOrdenTrabajo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerarOrdenTrabajo));
            this.tcOrdenTrabajo = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.tvDetallePlan = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelAccionesOrdenTrabajo = new System.Windows.Forms.Panel();
            this.dgvListaOrdenTrabajo = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAnioBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbMesBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSemanaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.cbUnidadMedida = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpTiempoTotal = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.txtOrigen = new System.Windows.Forms.TextBox();
            this.cbHojaRuta = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbEstructura = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpHoraFin = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpHoraInicio = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaFin = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaInicio = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cbTipoParte = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.cbParte = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbEmpleado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbCentroTrabajo = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpFechaAlta = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cbEstado = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbNavegador = new System.Windows.Forms.GroupBox();
            this.bnNavegador = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcOrdenTrabajo.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaOrdenTrabajo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.gbNavegador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnNavegador)).BeginInit();
            this.bnNavegador.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcOrdenTrabajo
            // 
            this.tcOrdenTrabajo.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcOrdenTrabajo.Controls.Add(this.tpBuscar);
            this.tcOrdenTrabajo.Controls.Add(this.tpDatos);
            this.tcOrdenTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOrdenTrabajo.ItemSize = new System.Drawing.Size(0, 1);
            this.tcOrdenTrabajo.Location = new System.Drawing.Point(3, 53);
            this.tcOrdenTrabajo.Multiline = true;
            this.tcOrdenTrabajo.Name = "tcOrdenTrabajo";
            this.tcOrdenTrabajo.SelectedIndex = 0;
            this.tcOrdenTrabajo.Size = new System.Drawing.Size(788, 516);
            this.tcOrdenTrabajo.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcOrdenTrabajo.TabIndex = 0;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox3);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(780, 507);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGenerar);
            this.groupBox3.Controls.Add(this.tvDetallePlan);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(774, 165);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalle del plan de producción";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.System_25;
            this.btnGenerar.Location = new System.Drawing.Point(676, 65);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(82, 36);
            this.btnGenerar.TabIndex = 8;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // tvDetallePlan
            // 
            this.tvDetallePlan.CheckBoxes = true;
            this.tvDetallePlan.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvDetallePlan.Location = new System.Drawing.Point(3, 17);
            this.tvDetallePlan.Name = "tvDetallePlan";
            this.tvDetallePlan.Size = new System.Drawing.Size(595, 145);
            this.tvDetallePlan.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelAccionesOrdenTrabajo);
            this.groupBox1.Controls.Add(this.dgvListaOrdenTrabajo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 276);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de órdenes de trabajo";
            // 
            // panelAccionesOrdenTrabajo
            // 
            this.panelAccionesOrdenTrabajo.Location = new System.Drawing.Point(615, 20);
            this.panelAccionesOrdenTrabajo.Name = "panelAccionesOrdenTrabajo";
            this.panelAccionesOrdenTrabajo.Size = new System.Drawing.Size(143, 250);
            this.panelAccionesOrdenTrabajo.TabIndex = 1;
            // 
            // dgvListaOrdenTrabajo
            // 
            this.dgvListaOrdenTrabajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaOrdenTrabajo.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvListaOrdenTrabajo.Location = new System.Drawing.Point(3, 17);
            this.dgvListaOrdenTrabajo.Name = "dgvListaOrdenTrabajo";
            this.dgvListaOrdenTrabajo.Size = new System.Drawing.Size(595, 256);
            this.dgvListaOrdenTrabajo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbAnioBuscar);
            this.groupBox2.Controls.Add(this.cbMesBuscar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbSemanaBuscar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(774, 54);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccione el plan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Año:";
            // 
            // cbAnioBuscar
            // 
            this.cbAnioBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnioBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbAnioBuscar.FormattingEnabled = true;
            this.cbAnioBuscar.Location = new System.Drawing.Point(50, 20);
            this.cbAnioBuscar.Name = "cbAnioBuscar";
            this.cbAnioBuscar.Size = new System.Drawing.Size(125, 21);
            this.cbAnioBuscar.TabIndex = 8;
            this.cbAnioBuscar.SelectedIndexChanged += new System.EventHandler(this.cbAnioBuscar_SelectedIndexChanged);
            // 
            // cbMesBuscar
            // 
            this.cbMesBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMesBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbMesBuscar.FormattingEnabled = true;
            this.cbMesBuscar.Location = new System.Drawing.Point(256, 20);
            this.cbMesBuscar.Name = "cbMesBuscar";
            this.cbMesBuscar.Size = new System.Drawing.Size(125, 21);
            this.cbMesBuscar.TabIndex = 7;
            this.cbMesBuscar.SelectedIndexChanged += new System.EventHandler(this.cbMesBuscar_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mes:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.lupa_20;
            this.btnBuscar.Location = new System.Drawing.Point(683, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Semana:";
            // 
            // cbSemanaBuscar
            // 
            this.cbSemanaBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemanaBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbSemanaBuscar.FormattingEnabled = true;
            this.cbSemanaBuscar.Location = new System.Drawing.Point(473, 20);
            this.cbSemanaBuscar.Name = "cbSemanaBuscar";
            this.cbSemanaBuscar.Size = new System.Drawing.Size(125, 21);
            this.cbSemanaBuscar.TabIndex = 3;
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(780, 507);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 451);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(774, 53);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.Location = new System.Drawing.Point(704, 18);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(634, 18);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.cbUnidadMedida);
            this.gbDatos.Controls.Add(this.dtpTiempoTotal);
            this.gbDatos.Controls.Add(this.txtOrigen);
            this.gbDatos.Controls.Add(this.cbHojaRuta);
            this.gbDatos.Controls.Add(this.cbEstructura);
            this.gbDatos.Controls.Add(this.dtpHoraFin);
            this.gbDatos.Controls.Add(this.dtpHoraInicio);
            this.gbDatos.Controls.Add(this.dtpFechaFin);
            this.gbDatos.Controls.Add(this.dtpFechaInicio);
            this.gbDatos.Controls.Add(this.cbTipoParte);
            this.gbDatos.Controls.Add(this.nudCantidad);
            this.gbDatos.Controls.Add(this.cbParte);
            this.gbDatos.Controls.Add(this.cbEmpleado);
            this.gbDatos.Controls.Add(this.cbCentroTrabajo);
            this.gbDatos.Controls.Add(this.dtpFechaAlta);
            this.gbDatos.Controls.Add(this.cbEstado);
            this.gbDatos.Controls.Add(this.txtCodigo);
            this.gbDatos.Controls.Add(this.label24);
            this.gbDatos.Controls.Add(this.label23);
            this.gbDatos.Controls.Add(this.label22);
            this.gbDatos.Controls.Add(this.label21);
            this.gbDatos.Controls.Add(this.label20);
            this.gbDatos.Controls.Add(this.label19);
            this.gbDatos.Controls.Add(this.label18);
            this.gbDatos.Controls.Add(this.label17);
            this.gbDatos.Controls.Add(this.label16);
            this.gbDatos.Controls.Add(this.label15);
            this.gbDatos.Controls.Add(this.label11);
            this.gbDatos.Controls.Add(this.label10);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.label8);
            this.gbDatos.Controls.Add(this.label7);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Controls.Add(this.gbNavegador);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(774, 442);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la Orden de Trabajo";
            // 
            // cbUnidadMedida
            // 
            this.cbUnidadMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnidadMedida.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbUnidadMedida.FormattingEnabled = true;
            this.cbUnidadMedida.Location = new System.Drawing.Point(140, 293);
            this.cbUnidadMedida.Name = "cbUnidadMedida";
            this.cbUnidadMedida.Size = new System.Drawing.Size(200, 21);
            this.cbUnidadMedida.TabIndex = 39;
            // 
            // dtpTiempoTotal
            // 
            this.dtpTiempoTotal.CustomFormat = " ";
            this.dtpTiempoTotal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTiempoTotal.Location = new System.Drawing.Point(487, 259);
            this.dtpTiempoTotal.Name = "dtpTiempoTotal";
            this.dtpTiempoTotal.Size = new System.Drawing.Size(200, 21);
            this.dtpTiempoTotal.TabIndex = 38;
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(487, 37);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(200, 21);
            this.txtOrigen.TabIndex = 37;
            // 
            // cbHojaRuta
            // 
            this.cbHojaRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHojaRuta.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbHojaRuta.FormattingEnabled = true;
            this.cbHojaRuta.Location = new System.Drawing.Point(487, 102);
            this.cbHojaRuta.Name = "cbHojaRuta";
            this.cbHojaRuta.Size = new System.Drawing.Size(200, 21);
            this.cbHojaRuta.TabIndex = 36;
            // 
            // cbEstructura
            // 
            this.cbEstructura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstructura.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEstructura.FormattingEnabled = true;
            this.cbEstructura.Location = new System.Drawing.Point(487, 69);
            this.cbEstructura.Name = "cbEstructura";
            this.cbEstructura.Size = new System.Drawing.Size(200, 21);
            this.cbEstructura.TabIndex = 35;
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.CustomFormat = " ";
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraFin.Location = new System.Drawing.Point(487, 228);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.Size = new System.Drawing.Size(200, 21);
            this.dtpHoraFin.TabIndex = 34;
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.CustomFormat = " ";
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraInicio.Location = new System.Drawing.Point(487, 196);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.Size = new System.Drawing.Size(200, 21);
            this.dtpHoraInicio.TabIndex = 33;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = " ";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(487, 164);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaFin.TabIndex = 32;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = " ";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(487, 132);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaInicio.TabIndex = 31;
            // 
            // cbTipoParte
            // 
            this.cbTipoParte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoParte.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbTipoParte.FormattingEnabled = true;
            this.cbTipoParte.Location = new System.Drawing.Point(140, 229);
            this.cbTipoParte.Name = "cbTipoParte";
            this.cbTipoParte.Size = new System.Drawing.Size(200, 21);
            this.cbTipoParte.TabIndex = 30;
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(140, 261);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(200, 21);
            this.nudCantidad.TabIndex = 29;
            // 
            // cbParte
            // 
            this.cbParte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParte.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbParte.FormattingEnabled = true;
            this.cbParte.Location = new System.Drawing.Point(140, 197);
            this.cbParte.Name = "cbParte";
            this.cbParte.Size = new System.Drawing.Size(200, 21);
            this.cbParte.TabIndex = 28;
            // 
            // cbEmpleado
            // 
            this.cbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmpleado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEmpleado.FormattingEnabled = true;
            this.cbEmpleado.Location = new System.Drawing.Point(140, 165);
            this.cbEmpleado.Name = "cbEmpleado";
            this.cbEmpleado.Size = new System.Drawing.Size(200, 21);
            this.cbEmpleado.TabIndex = 27;
            // 
            // cbCentroTrabajo
            // 
            this.cbCentroTrabajo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCentroTrabajo.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCentroTrabajo.FormattingEnabled = true;
            this.cbCentroTrabajo.Location = new System.Drawing.Point(140, 133);
            this.cbCentroTrabajo.Name = "cbCentroTrabajo";
            this.cbCentroTrabajo.Size = new System.Drawing.Size(200, 21);
            this.cbCentroTrabajo.TabIndex = 26;
            // 
            // dtpFechaAlta
            // 
            this.dtpFechaAlta.CustomFormat = " ";
            this.dtpFechaAlta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaAlta.Location = new System.Drawing.Point(140, 101);
            this.dtpFechaAlta.Name = "dtpFechaAlta";
            this.dtpFechaAlta.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaAlta.TabIndex = 25;
            // 
            // cbEstado
            // 
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.Location = new System.Drawing.Point(140, 69);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(200, 21);
            this.cbEstado.TabIndex = 24;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(140, 37);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(200, 21);
            this.txtCodigo.TabIndex = 23;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(386, 168);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(95, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "Fecha finalización:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(386, 136);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 13);
            this.label23.TabIndex = 21;
            this.label23.Text = "Fecha inicio:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(36, 232);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 13);
            this.label22.TabIndex = 20;
            this.label22.Text = "Tipo parte:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(36, 263);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 19;
            this.label21.Text = "Cantidad:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(36, 200);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 13);
            this.label20.TabIndex = 18;
            this.label20.Text = "Parte:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(36, 168);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "Empleado:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(36, 136);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Centro de trabajo:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(36, 105);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 13);
            this.label17.TabIndex = 15;
            this.label17.Text = "Fecha alta:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(36, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Estado:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Código:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 296);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Unidad medida:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(386, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Tiempo total:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(386, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Origen:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(386, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Hoja de ruta:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(386, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Estructura:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Hora finalización:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Hora inicio:";
            // 
            // gbNavegador
            // 
            this.gbNavegador.Controls.Add(this.bnNavegador);
            this.gbNavegador.Location = new System.Drawing.Point(265, 398);
            this.gbNavegador.Name = "gbNavegador";
            this.gbNavegador.Size = new System.Drawing.Size(256, 38);
            this.gbNavegador.TabIndex = 2;
            this.gbNavegador.TabStop = false;
            // 
            // bnNavegador
            // 
            this.bnNavegador.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnNavegador.CountItem = this.bindingNavigatorCountItem;
            this.bnNavegador.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnNavegador.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnNavegador.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnNavegador.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bnNavegador.Location = new System.Drawing.Point(3, 10);
            this.bnNavegador.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnNavegador.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnNavegador.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnNavegador.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnNavegador.Name = "bnNavegador";
            this.bnNavegador.PositionItem = this.bindingNavigatorPositionItem;
            this.bnNavegador.Size = new System.Drawing.Size(250, 25);
            this.bnNavegador.TabIndex = 0;
            this.bnNavegador.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsMenu
            // 
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConsultar,
            this.btnModificar,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(794, 50);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(57, 47);
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcOrdenTrabajo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 572);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // frmGenerarOrdenTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmGenerarOrdenTrabajo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Generar Orden de Trabajo";
            this.tcOrdenTrabajo.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaOrdenTrabajo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.gbNavegador.ResumeLayout(false);
            this.gbNavegador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnNavegador)).EndInit();
            this.bnNavegador.ResumeLayout(false);
            this.bnNavegador.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcOrdenTrabajo;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.TreeView tvDetallePlan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelAccionesOrdenTrabajo;
        private System.Windows.Forms.DataGridView dgvListaOrdenTrabajo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbAnioBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbMesBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label5;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbSemanaBuscar;
        private System.Windows.Forms.BindingNavigator bnNavegador;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.GroupBox gbNavegador;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpHoraInicio;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaFin;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaInicio;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbTipoParte;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbParte;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEmpleado;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCentroTrabajo;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaAlta;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEstado;
        private System.Windows.Forms.TextBox txtCodigo;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbUnidadMedida;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpTiempoTotal;
        private System.Windows.Forms.TextBox txtOrigen;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbHojaRuta;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbEstructura;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpHoraFin;

    }
}