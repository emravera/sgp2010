namespace GyCAP.UI.PlanificacionProduccion
{
    partial class frmOrdenTrabajo
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
            this.tcOrdenTrabajo = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gbBuscarOtros = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtCodigoBuscar = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvOrdenesProduccion = new System.Windows.Forms.DataGridView();
            this.tpOrdenesTrabajo = new System.Windows.Forms.TabPage();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.dgvOrdenesTrabajo = new System.Windows.Forms.DataGridView();
            this.tpCierreParcial = new System.Windows.Forms.TabPage();
            this.gbAgregarCierreParcial = new System.Windows.Forms.GroupBox();
            this.btnCancelarCierre = new System.Windows.Forms.Button();
            this.btnGuardarCierre = new System.Windows.Forms.Button();
            this.txtObservacionesCierre = new System.Windows.Forms.RichTextBox();
            this.nudCantidadCierre = new System.Windows.Forms.NumericUpDown();
            this.dtpHoraCierre = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEliminarCierre = new System.Windows.Forms.Button();
            this.btnModificarCierre = new System.Windows.Forms.Button();
            this.dgvCierresParciales = new System.Windows.Forms.DataGridView();
            this.btnAgregarCierre = new System.Windows.Forms.Button();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnIniciar = new System.Windows.Forms.ToolStripButton();
            this.btnCierreParcial = new System.Windows.Forms.ToolStripButton();
            this.btnFinalizar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.dtpFechaHastaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaDesdeBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.dtpFechaGeneracionBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.cboModoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboEstadoBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboMaquinaCierre = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cboEmpleadoCierre = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dtpFechaCierre = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcOrdenTrabajo.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.gbBuscarOtros.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesProduccion)).BeginInit();
            this.tpOrdenesTrabajo.SuspendLayout();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesTrabajo)).BeginInit();
            this.tpCierreParcial.SuspendLayout();
            this.gbAgregarCierreParcial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadCierre)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCierresParciales)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
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
            // tcOrdenTrabajo
            // 
            this.tcOrdenTrabajo.Controls.Add(this.tpBuscar);
            this.tcOrdenTrabajo.Controls.Add(this.tpOrdenesTrabajo);
            this.tcOrdenTrabajo.Controls.Add(this.tpCierreParcial);
            this.tcOrdenTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOrdenTrabajo.ItemSize = new System.Drawing.Size(150, 18);
            this.tcOrdenTrabajo.Location = new System.Drawing.Point(3, 53);
            this.tcOrdenTrabajo.Multiline = true;
            this.tcOrdenTrabajo.Name = "tcOrdenTrabajo";
            this.tcOrdenTrabajo.SelectedIndex = 0;
            this.tcOrdenTrabajo.Size = new System.Drawing.Size(788, 516);
            this.tcOrdenTrabajo.TabIndex = 0;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.gbBuscarOtros);
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Location = new System.Drawing.Point(4, 22);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(780, 490);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.Text = "Búsqueda";
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // gbBuscarOtros
            // 
            this.gbBuscarOtros.Controls.Add(this.dtpFechaHastaBuscar);
            this.gbBuscarOtros.Controls.Add(this.dtpFechaDesdeBuscar);
            this.gbBuscarOtros.Controls.Add(this.label5);
            this.gbBuscarOtros.Controls.Add(this.label2);
            this.gbBuscarOtros.Controls.Add(this.label1);
            this.gbBuscarOtros.Controls.Add(this.dtpFechaGeneracionBuscar);
            this.gbBuscarOtros.Controls.Add(this.btnBuscar);
            this.gbBuscarOtros.Controls.Add(this.txtCodigoBuscar);
            this.gbBuscarOtros.Controls.Add(this.label10);
            this.gbBuscarOtros.Controls.Add(this.cboModoBuscar);
            this.gbBuscarOtros.Controls.Add(this.label8);
            this.gbBuscarOtros.Controls.Add(this.cboEstadoBuscar);
            this.gbBuscarOtros.Controls.Add(this.label7);
            this.gbBuscarOtros.Location = new System.Drawing.Point(6, 6);
            this.gbBuscarOtros.Name = "gbBuscarOtros";
            this.gbBuscarOtros.Size = new System.Drawing.Size(765, 127);
            this.gbBuscarOtros.TabIndex = 3;
            this.gbBuscarOtros.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Fecha inicio hasta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Fecha inicio desde:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Fecha generación:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(649, 52);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCodigoBuscar
            // 
            this.txtCodigoBuscar.Location = new System.Drawing.Point(72, 25);
            this.txtCodigoBuscar.Name = "txtCodigoBuscar";
            this.txtCodigoBuscar.Size = new System.Drawing.Size(193, 21);
            this.txtCodigoBuscar.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Código:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Modo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Estado:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvOrdenesProduccion);
            this.groupBox2.Location = new System.Drawing.Point(3, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 345);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Órdenes de Producción";
            // 
            // dgvOrdenesProduccion
            // 
            this.dgvOrdenesProduccion.AllowUserToAddRows = false;
            this.dgvOrdenesProduccion.AllowUserToDeleteRows = false;
            this.dgvOrdenesProduccion.AllowUserToResizeRows = false;
            this.dgvOrdenesProduccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesProduccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenesProduccion.Location = new System.Drawing.Point(3, 17);
            this.dgvOrdenesProduccion.MultiSelect = false;
            this.dgvOrdenesProduccion.Name = "dgvOrdenesProduccion";
            this.dgvOrdenesProduccion.ReadOnly = true;
            this.dgvOrdenesProduccion.RowHeadersVisible = false;
            this.dgvOrdenesProduccion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenesProduccion.Size = new System.Drawing.Size(765, 325);
            this.dgvOrdenesProduccion.TabIndex = 0;
            this.dgvOrdenesProduccion.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrdenesProduccion_CellFormatting);
            // 
            // tpOrdenesTrabajo
            // 
            this.tpOrdenesTrabajo.Controls.Add(this.gbDatos);
            this.tpOrdenesTrabajo.Location = new System.Drawing.Point(4, 22);
            this.tpOrdenesTrabajo.Name = "tpOrdenesTrabajo";
            this.tpOrdenesTrabajo.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrdenesTrabajo.Size = new System.Drawing.Size(780, 490);
            this.tpOrdenesTrabajo.TabIndex = 1;
            this.tpOrdenesTrabajo.Text = "Órdenes de Trabajo";
            this.tpOrdenesTrabajo.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.dgvOrdenesTrabajo);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(774, 481);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Listado de Órdenes de Trabajo";
            // 
            // dgvOrdenesTrabajo
            // 
            this.dgvOrdenesTrabajo.AllowUserToAddRows = false;
            this.dgvOrdenesTrabajo.AllowUserToDeleteRows = false;
            this.dgvOrdenesTrabajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenesTrabajo.Location = new System.Drawing.Point(3, 17);
            this.dgvOrdenesTrabajo.MultiSelect = false;
            this.dgvOrdenesTrabajo.Name = "dgvOrdenesTrabajo";
            this.dgvOrdenesTrabajo.ReadOnly = true;
            this.dgvOrdenesTrabajo.RowHeadersVisible = false;
            this.dgvOrdenesTrabajo.Size = new System.Drawing.Size(768, 461);
            this.dgvOrdenesTrabajo.TabIndex = 0;
            this.dgvOrdenesTrabajo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrdenesTrabajo_CellFormatting);
            // 
            // tpCierreParcial
            // 
            this.tpCierreParcial.Controls.Add(this.gbAgregarCierreParcial);
            this.tpCierreParcial.Controls.Add(this.groupBox1);
            this.tpCierreParcial.Location = new System.Drawing.Point(4, 22);
            this.tpCierreParcial.Name = "tpCierreParcial";
            this.tpCierreParcial.Size = new System.Drawing.Size(780, 490);
            this.tpCierreParcial.TabIndex = 2;
            this.tpCierreParcial.Text = "Cierres Parciales";
            this.tpCierreParcial.UseVisualStyleBackColor = true;
            // 
            // gbAgregarCierreParcial
            // 
            this.gbAgregarCierreParcial.Controls.Add(this.btnCancelarCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.btnGuardarCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.txtObservacionesCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.nudCantidadCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.cboMaquinaCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.dtpHoraCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.cboEmpleadoCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.dtpFechaCierre);
            this.gbAgregarCierreParcial.Controls.Add(this.label12);
            this.gbAgregarCierreParcial.Controls.Add(this.label11);
            this.gbAgregarCierreParcial.Controls.Add(this.label9);
            this.gbAgregarCierreParcial.Controls.Add(this.label6);
            this.gbAgregarCierreParcial.Controls.Add(this.label4);
            this.gbAgregarCierreParcial.Controls.Add(this.label3);
            this.gbAgregarCierreParcial.Location = new System.Drawing.Point(5, 294);
            this.gbAgregarCierreParcial.Name = "gbAgregarCierreParcial";
            this.gbAgregarCierreParcial.Size = new System.Drawing.Size(770, 191);
            this.gbAgregarCierreParcial.TabIndex = 3;
            this.gbAgregarCierreParcial.TabStop = false;
            this.gbAgregarCierreParcial.Text = "Agregar cierre parcial";
            // 
            // btnCancelarCierre
            // 
            this.btnCancelarCierre.Location = new System.Drawing.Point(672, 117);
            this.btnCancelarCierre.Name = "btnCancelarCierre";
            this.btnCancelarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnCancelarCierre.TabIndex = 29;
            this.btnCancelarCierre.Text = "Cancelar";
            this.btnCancelarCierre.UseVisualStyleBackColor = true;
            // 
            // btnGuardarCierre
            // 
            this.btnGuardarCierre.Location = new System.Drawing.Point(672, 71);
            this.btnGuardarCierre.Name = "btnGuardarCierre";
            this.btnGuardarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnGuardarCierre.TabIndex = 28;
            this.btnGuardarCierre.Text = "Guardar";
            this.btnGuardarCierre.UseVisualStyleBackColor = true;
            // 
            // txtObservacionesCierre
            // 
            this.txtObservacionesCierre.Location = new System.Drawing.Point(347, 46);
            this.txtObservacionesCierre.Name = "txtObservacionesCierre";
            this.txtObservacionesCierre.Size = new System.Drawing.Size(281, 129);
            this.txtObservacionesCierre.TabIndex = 27;
            this.txtObservacionesCierre.Text = "";
            // 
            // nudCantidadCierre
            // 
            this.nudCantidadCierre.Location = new System.Drawing.Point(84, 92);
            this.nudCantidadCierre.Name = "nudCantidadCierre";
            this.nudCantidadCierre.Size = new System.Drawing.Size(200, 21);
            this.nudCantidadCierre.TabIndex = 25;
            // 
            // dtpHoraCierre
            // 
            this.dtpHoraCierre.Location = new System.Drawing.Point(84, 154);
            this.dtpHoraCierre.Name = "dtpHoraCierre";
            this.dtpHoraCierre.Size = new System.Drawing.Size(200, 21);
            this.dtpHoraCierre.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(344, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Observaciones";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Hora:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Fecha:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Cantidad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Máquina:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Empleado:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEliminarCierre);
            this.groupBox1.Controls.Add(this.btnModificarCierre);
            this.groupBox1.Controls.Add(this.dgvCierresParciales);
            this.groupBox1.Controls.Add(this.btnAgregarCierre);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(770, 285);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Cierres Parciales";
            // 
            // btnEliminarCierre
            // 
            this.btnEliminarCierre.Location = new System.Drawing.Point(689, 254);
            this.btnEliminarCierre.Name = "btnEliminarCierre";
            this.btnEliminarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnEliminarCierre.TabIndex = 28;
            this.btnEliminarCierre.Text = "Eliminar";
            this.btnEliminarCierre.UseVisualStyleBackColor = true;
            // 
            // btnModificarCierre
            // 
            this.btnModificarCierre.Location = new System.Drawing.Point(608, 254);
            this.btnModificarCierre.Name = "btnModificarCierre";
            this.btnModificarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnModificarCierre.TabIndex = 27;
            this.btnModificarCierre.Text = "Modificar";
            this.btnModificarCierre.UseVisualStyleBackColor = true;
            // 
            // dgvCierresParciales
            // 
            this.dgvCierresParciales.AllowUserToAddRows = false;
            this.dgvCierresParciales.AllowUserToDeleteRows = false;
            this.dgvCierresParciales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCierresParciales.Location = new System.Drawing.Point(6, 20);
            this.dgvCierresParciales.MultiSelect = false;
            this.dgvCierresParciales.Name = "dgvCierresParciales";
            this.dgvCierresParciales.ReadOnly = true;
            this.dgvCierresParciales.RowHeadersVisible = false;
            this.dgvCierresParciales.Size = new System.Drawing.Size(758, 228);
            this.dgvCierresParciales.TabIndex = 1;
            this.dgvCierresParciales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCierresParciales_CellFormatting);
            // 
            // btnAgregarCierre
            // 
            this.btnAgregarCierre.Location = new System.Drawing.Point(527, 254);
            this.btnAgregarCierre.Name = "btnAgregarCierre";
            this.btnAgregarCierre.Size = new System.Drawing.Size(75, 25);
            this.btnAgregarCierre.TabIndex = 26;
            this.btnAgregarCierre.Text = "Agregar";
            this.btnAgregarCierre.UseVisualStyleBackColor = true;
            // 
            // tsMenu
            // 
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIniciar,
            this.btnCierreParcial,
            this.btnFinalizar,
            this.btnCancelar,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(794, 50);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Iniciar_25;
            this.btnIniciar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(40, 47);
            this.btnIniciar.Text = "&Iniciar";
            this.btnIniciar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnCierreParcial
            // 
            this.btnCierreParcial.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Cierre_Parcial_25;
            this.btnCierreParcial.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCierreParcial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCierreParcial.Name = "btnCierreParcial";
            this.btnCierreParcial.Size = new System.Drawing.Size(74, 47);
            this.btnCierreParcial.Text = "&Cierre parcial";
            this.btnCierreParcial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Finalizar_25;
            this.btnFinalizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFinalizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(50, 47);
            this.btnFinalizar.Text = "&Finalizar";
            this.btnFinalizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Cancelar_25;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(53, 47);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 47);
            this.btnEliminar.Text = "Eliminar";
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
            // dtpFechaHastaBuscar
            // 
            this.dtpFechaHastaBuscar.CustomFormat = " ";
            this.dtpFechaHastaBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHastaBuscar.Location = new System.Drawing.Point(405, 86);
            this.dtpFechaHastaBuscar.Name = "dtpFechaHastaBuscar";
            this.dtpFechaHastaBuscar.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaHastaBuscar.TabIndex = 23;
            // 
            // dtpFechaDesdeBuscar
            // 
            this.dtpFechaDesdeBuscar.CustomFormat = " ";
            this.dtpFechaDesdeBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDesdeBuscar.Location = new System.Drawing.Point(405, 55);
            this.dtpFechaDesdeBuscar.Name = "dtpFechaDesdeBuscar";
            this.dtpFechaDesdeBuscar.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaDesdeBuscar.TabIndex = 22;
            // 
            // dtpFechaGeneracionBuscar
            // 
            this.dtpFechaGeneracionBuscar.CustomFormat = " ";
            this.dtpFechaGeneracionBuscar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaGeneracionBuscar.Location = new System.Drawing.Point(405, 24);
            this.dtpFechaGeneracionBuscar.Name = "dtpFechaGeneracionBuscar";
            this.dtpFechaGeneracionBuscar.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaGeneracionBuscar.TabIndex = 18;
            // 
            // cboModoBuscar
            // 
            this.cboModoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboModoBuscar.FormattingEnabled = true;
            this.cboModoBuscar.Location = new System.Drawing.Point(72, 87);
            this.cboModoBuscar.Name = "cboModoBuscar";
            this.cboModoBuscar.Size = new System.Drawing.Size(193, 21);
            this.cboModoBuscar.TabIndex = 13;
            // 
            // cboEstadoBuscar
            // 
            this.cboEstadoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEstadoBuscar.FormattingEnabled = true;
            this.cboEstadoBuscar.Location = new System.Drawing.Point(72, 56);
            this.cboEstadoBuscar.Name = "cboEstadoBuscar";
            this.cboEstadoBuscar.Size = new System.Drawing.Size(193, 21);
            this.cboEstadoBuscar.TabIndex = 11;
            // 
            // cboMaquinaCierre
            // 
            this.cboMaquinaCierre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaquinaCierre.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboMaquinaCierre.FormattingEnabled = true;
            this.cboMaquinaCierre.Location = new System.Drawing.Point(84, 59);
            this.cboMaquinaCierre.Name = "cboMaquinaCierre";
            this.cboMaquinaCierre.Size = new System.Drawing.Size(200, 21);
            this.cboMaquinaCierre.TabIndex = 24;
            // 
            // cboEmpleadoCierre
            // 
            this.cboEmpleadoCierre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpleadoCierre.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboEmpleadoCierre.FormattingEnabled = true;
            this.cboEmpleadoCierre.Location = new System.Drawing.Point(84, 27);
            this.cboEmpleadoCierre.Name = "cboEmpleadoCierre";
            this.cboEmpleadoCierre.Size = new System.Drawing.Size(200, 21);
            this.cboEmpleadoCierre.TabIndex = 22;
            // 
            // dtpFechaCierre
            // 
            this.dtpFechaCierre.CustomFormat = " ";
            this.dtpFechaCierre.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCierre.Location = new System.Drawing.Point(84, 122);
            this.dtpFechaCierre.Name = "dtpFechaCierre";
            this.dtpFechaCierre.Size = new System.Drawing.Size(200, 21);
            this.dtpFechaCierre.TabIndex = 21;
            // 
            // frmOrdenTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmOrdenTrabajo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Control de Órdenes de Producción";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcOrdenTrabajo.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.gbBuscarOtros.ResumeLayout(false);
            this.gbBuscarOtros.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesProduccion)).EndInit();
            this.tpOrdenesTrabajo.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesTrabajo)).EndInit();
            this.tpCierreParcial.ResumeLayout(false);
            this.gbAgregarCierreParcial.ResumeLayout(false);
            this.gbAgregarCierreParcial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadCierre)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCierresParciales)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcOrdenTrabajo;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvOrdenesProduccion;
        private System.Windows.Forms.TabPage tpOrdenesTrabajo;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnCierreParcial;
        private System.Windows.Forms.ToolStripButton btnFinalizar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnIniciar;
        private System.Windows.Forms.GroupBox gbBuscarOtros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtCodigoBuscar;
        private System.Windows.Forms.Label label10;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboModoBuscar;
        private System.Windows.Forms.Label label8;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEstadoBuscar;
        private System.Windows.Forms.Label label7;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaHastaBuscar;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaDesdeBuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaGeneracionBuscar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.TabPage tpCierreParcial;
        private System.Windows.Forms.DataGridView dgvOrdenesTrabajo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCierresParciales;
        private System.Windows.Forms.GroupBox gbAgregarCierreParcial;
        private System.Windows.Forms.Button btnCancelarCierre;
        private System.Windows.Forms.Button btnGuardarCierre;
        private System.Windows.Forms.RichTextBox txtObservacionesCierre;
        private System.Windows.Forms.NumericUpDown nudCantidadCierre;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboMaquinaCierre;
        private System.Windows.Forms.DateTimePicker dtpHoraCierre;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboEmpleadoCierre;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha dtpFechaCierre;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEliminarCierre;
        private System.Windows.Forms.Button btnModificarCierre;
        private System.Windows.Forms.Button btnAgregarCierre;
    }
}