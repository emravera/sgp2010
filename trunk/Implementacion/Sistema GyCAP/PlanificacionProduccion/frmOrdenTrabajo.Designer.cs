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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcOrdenTrabajo = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.tcBusqueda = new System.Windows.Forms.TabControl();
            this.tpBuscarPorOtro = new System.Windows.Forms.TabPage();
            this.gbBuscarOtros = new System.Windows.Forms.GroupBox();
            this.dropDownList7 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label9 = new System.Windows.Forms.Label();
            this.dropDownList6 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label8 = new System.Windows.Forms.Label();
            this.dropDownList5 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.tpBuscarPorPlan = new System.Windows.Forms.TabPage();
            this.gbBuscarPlan = new System.Windows.Forms.GroupBox();
            this.dropDownList4 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label6 = new System.Windows.Forms.Label();
            this.dropDownList3 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dropDownList2 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dropDownList1 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.tvPlan = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvListaOrdenTrabajo = new System.Windows.Forms.DataGridView();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.slideControl = new SlickInterface.SlideControl();
            this.slideBusquedaOtro = new SlickInterface.Slide();
            this.slideBuscarPlan = new SlickInterface.Slide();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.seleccionadorFecha1 = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.seleccionadorFecha2 = new GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dropDownList8 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.dropDownList9 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dropDownList10 = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcOrdenTrabajo.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.tcBusqueda.SuspendLayout();
            this.tpBuscarPorOtro.SuspendLayout();
            this.gbBuscarOtros.SuspendLayout();
            this.tpBuscarPorPlan.SuspendLayout();
            this.gbBuscarPlan.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaOrdenTrabajo)).BeginInit();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(87, 34);
            this.txtNombre.MaxLength = 80;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(248, 21);
            this.txtNombre.TabIndex = 3;
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
            this.tpBuscar.Controls.Add(this.tcBusqueda);
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(780, 507);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // tcBusqueda
            // 
            this.tcBusqueda.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcBusqueda.Controls.Add(this.tpBuscarPorOtro);
            this.tcBusqueda.Controls.Add(this.tpBuscarPorPlan);
            this.tcBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcBusqueda.ItemSize = new System.Drawing.Size(0, 1);
            this.tcBusqueda.Location = new System.Drawing.Point(3, 3);
            this.tcBusqueda.Name = "tcBusqueda";
            this.tcBusqueda.SelectedIndex = 0;
            this.tcBusqueda.Size = new System.Drawing.Size(774, 182);
            this.tcBusqueda.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcBusqueda.TabIndex = 3;
            // 
            // tpBuscarPorOtro
            // 
            this.tpBuscarPorOtro.Controls.Add(this.groupBox1);
            this.tpBuscarPorOtro.Controls.Add(this.gbBuscarOtros);
            this.tpBuscarPorOtro.Controls.Add(this.slideBusquedaOtro);
            this.tpBuscarPorOtro.Controls.Add(this.slideControl);
            this.tpBuscarPorOtro.Location = new System.Drawing.Point(4, 5);
            this.tpBuscarPorOtro.Name = "tpBuscarPorOtro";
            this.tpBuscarPorOtro.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscarPorOtro.Size = new System.Drawing.Size(766, 173);
            this.tpBuscarPorOtro.TabIndex = 0;
            this.tpBuscarPorOtro.Text = "tabPage1";
            this.tpBuscarPorOtro.UseVisualStyleBackColor = true;
            // 
            // gbBuscarOtros
            // 
            this.gbBuscarOtros.Controls.Add(this.dropDownList10);
            this.gbBuscarOtros.Controls.Add(this.label15);
            this.gbBuscarOtros.Controls.Add(this.label14);
            this.gbBuscarOtros.Controls.Add(this.dropDownList9);
            this.gbBuscarOtros.Controls.Add(this.dropDownList8);
            this.gbBuscarOtros.Controls.Add(this.label13);
            this.gbBuscarOtros.Controls.Add(this.label12);
            this.gbBuscarOtros.Controls.Add(this.seleccionadorFecha2);
            this.gbBuscarOtros.Controls.Add(this.seleccionadorFecha1);
            this.gbBuscarOtros.Controls.Add(this.label11);
            this.gbBuscarOtros.Controls.Add(this.textBox1);
            this.gbBuscarOtros.Controls.Add(this.label10);
            this.gbBuscarOtros.Controls.Add(this.dropDownList7);
            this.gbBuscarOtros.Controls.Add(this.label9);
            this.gbBuscarOtros.Controls.Add(this.dropDownList6);
            this.gbBuscarOtros.Controls.Add(this.label8);
            this.gbBuscarOtros.Controls.Add(this.dropDownList5);
            this.gbBuscarOtros.Controls.Add(this.label7);
            this.gbBuscarOtros.Location = new System.Drawing.Point(6, 6);
            this.gbBuscarOtros.Name = "gbBuscarOtros";
            this.gbBuscarOtros.Size = new System.Drawing.Size(626, 161);
            this.gbBuscarOtros.TabIndex = 0;
            this.gbBuscarOtros.TabStop = false;
            this.gbBuscarOtros.Text = "Búsqueda individual";
            // 
            // dropDownList7
            // 
            this.dropDownList7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList7.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList7.FormattingEnabled = true;
            this.dropDownList7.Location = new System.Drawing.Point(469, 98);
            this.dropDownList7.Name = "dropDownList7";
            this.dropDownList7.Size = new System.Drawing.Size(121, 21);
            this.dropDownList7.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(366, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Empleado:";
            // 
            // dropDownList6
            // 
            this.dropDownList6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList6.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList6.FormattingEnabled = true;
            this.dropDownList6.Location = new System.Drawing.Point(57, 97);
            this.dropDownList6.Name = "dropDownList6";
            this.dropDownList6.Size = new System.Drawing.Size(121, 21);
            this.dropDownList6.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Modo:";
            // 
            // dropDownList5
            // 
            this.dropDownList5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList5.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList5.FormattingEnabled = true;
            this.dropDownList5.Location = new System.Drawing.Point(57, 70);
            this.dropDownList5.Name = "dropDownList5";
            this.dropDownList5.Size = new System.Drawing.Size(121, 21);
            this.dropDownList5.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Estado:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(24, 114);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // tpBuscarPorPlan
            // 
            this.tpBuscarPorPlan.Controls.Add(this.gbBuscarPlan);
            this.tpBuscarPorPlan.Controls.Add(this.slideBuscarPlan);
            this.tpBuscarPorPlan.Location = new System.Drawing.Point(4, 5);
            this.tpBuscarPorPlan.Name = "tpBuscarPorPlan";
            this.tpBuscarPorPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscarPorPlan.Size = new System.Drawing.Size(766, 173);
            this.tpBuscarPorPlan.TabIndex = 1;
            this.tpBuscarPorPlan.Text = "tabPage2";
            this.tpBuscarPorPlan.UseVisualStyleBackColor = true;
            // 
            // gbBuscarPlan
            // 
            this.gbBuscarPlan.Controls.Add(this.dropDownList4);
            this.gbBuscarPlan.Controls.Add(this.label6);
            this.gbBuscarPlan.Controls.Add(this.dropDownList3);
            this.gbBuscarPlan.Controls.Add(this.dropDownList2);
            this.gbBuscarPlan.Controls.Add(this.label5);
            this.gbBuscarPlan.Controls.Add(this.label2);
            this.gbBuscarPlan.Controls.Add(this.dropDownList1);
            this.gbBuscarPlan.Controls.Add(this.label1);
            this.gbBuscarPlan.Controls.Add(this.tvPlan);
            this.gbBuscarPlan.Location = new System.Drawing.Point(3, 3);
            this.gbBuscarPlan.Name = "gbBuscarPlan";
            this.gbBuscarPlan.Size = new System.Drawing.Size(630, 167);
            this.gbBuscarPlan.TabIndex = 1;
            this.gbBuscarPlan.TabStop = false;
            this.gbBuscarPlan.Text = "Búsqueda por Planificación";
            // 
            // dropDownList4
            // 
            this.dropDownList4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList4.FormattingEnabled = true;
            this.dropDownList4.Location = new System.Drawing.Point(75, 138);
            this.dropDownList4.Name = "dropDownList4";
            this.dropDownList4.Size = new System.Drawing.Size(158, 21);
            this.dropDownList4.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Día:";
            // 
            // dropDownList3
            // 
            this.dropDownList3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList3.FormattingEnabled = true;
            this.dropDownList3.Location = new System.Drawing.Point(75, 100);
            this.dropDownList3.Name = "dropDownList3";
            this.dropDownList3.Size = new System.Drawing.Size(158, 21);
            this.dropDownList3.TabIndex = 15;
            // 
            // dropDownList2
            // 
            this.dropDownList2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList2.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList2.FormattingEnabled = true;
            this.dropDownList2.Location = new System.Drawing.Point(75, 57);
            this.dropDownList2.Name = "dropDownList2";
            this.dropDownList2.Size = new System.Drawing.Size(158, 21);
            this.dropDownList2.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Mes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Año:";
            // 
            // dropDownList1
            // 
            this.dropDownList1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList1.FormattingEnabled = true;
            this.dropDownList1.Location = new System.Drawing.Point(75, 17);
            this.dropDownList1.Name = "dropDownList1";
            this.dropDownList1.Size = new System.Drawing.Size(158, 21);
            this.dropDownList1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Semana:";
            // 
            // tvPlan
            // 
            this.tvPlan.Location = new System.Drawing.Point(242, 16);
            this.tvPlan.Name = "tvPlan";
            this.tvPlan.Size = new System.Drawing.Size(382, 143);
            this.tvPlan.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvListaOrdenTrabajo);
            this.groupBox2.Location = new System.Drawing.Point(3, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 310);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Órdenes de Trabajo";
            // 
            // dgvListaOrdenTrabajo
            // 
            this.dgvListaOrdenTrabajo.AllowUserToAddRows = false;
            this.dgvListaOrdenTrabajo.AllowUserToDeleteRows = false;
            this.dgvListaOrdenTrabajo.AllowUserToResizeRows = false;
            this.dgvListaOrdenTrabajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaOrdenTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaOrdenTrabajo.Location = new System.Drawing.Point(3, 17);
            this.dgvListaOrdenTrabajo.MultiSelect = false;
            this.dgvListaOrdenTrabajo.Name = "dgvListaOrdenTrabajo";
            this.dgvListaOrdenTrabajo.ReadOnly = true;
            this.dgvListaOrdenTrabajo.RowHeadersVisible = false;
            this.dgvListaOrdenTrabajo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaOrdenTrabajo.Size = new System.Drawing.Size(765, 290);
            this.dgvListaOrdenTrabajo.TabIndex = 0;
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
            this.btnVolver.Location = new System.Drawing.Point(350, 18);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(280, 18);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.txtDescripcion);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(774, 197);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del Modelo de Cocina";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Location = new System.Drawing.Point(87, 84);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(248, 90);
            this.txtDescripcion.TabIndex = 4;
            this.txtDescripcion.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // tsMenu
            // 
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
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
            // 
            // slideControl
            // 
            this.slideControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slideControl.Location = new System.Drawing.Point(3, 3);
            this.slideControl.Name = "slideControl";
            this.slideControl.Selected = null;
            this.slideControl.Size = new System.Drawing.Size(760, 167);
            this.slideControl.SlideSpeed = 250;
            this.slideControl.TabIndex = 0;
            // 
            // slideBusquedaOtro
            // 
            this.slideBusquedaOtro.Dock = System.Windows.Forms.DockStyle.Left;
            this.slideBusquedaOtro.Location = new System.Drawing.Point(3, 3);
            this.slideBusquedaOtro.Name = "slideBusquedaOtro";
            this.slideBusquedaOtro.Size = new System.Drawing.Size(630, 167);
            this.slideBusquedaOtro.SlideControl = null;
            this.slideBusquedaOtro.TabIndex = 1;
            // 
            // slideBuscarPlan
            // 
            this.slideBuscarPlan.Dock = System.Windows.Forms.DockStyle.Left;
            this.slideBuscarPlan.Location = new System.Drawing.Point(3, 3);
            this.slideBuscarPlan.Name = "slideBuscarPlan";
            this.slideBuscarPlan.Size = new System.Drawing.Size(630, 167);
            this.slideBuscarPlan.SlideControl = null;
            this.slideBuscarPlan.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Código:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 21);
            this.textBox1.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(184, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Mes:";
            // 
            // seleccionadorFecha1
            // 
            this.seleccionadorFecha1.CustomFormat = " MM/yyyy";
            this.seleccionadorFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.seleccionadorFecha1.Location = new System.Drawing.Point(239, 42);
            this.seleccionadorFecha1.Name = "seleccionadorFecha1";
            this.seleccionadorFecha1.ShowUpDown = true;
            this.seleccionadorFecha1.Size = new System.Drawing.Size(121, 21);
            this.seleccionadorFecha1.TabIndex = 19;
            // 
            // seleccionadorFecha2
            // 
            this.seleccionadorFecha2.CustomFormat = " ";
            this.seleccionadorFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.seleccionadorFecha2.Location = new System.Drawing.Point(239, 96);
            this.seleccionadorFecha2.Name = "seleccionadorFecha2";
            this.seleccionadorFecha2.Size = new System.Drawing.Size(121, 21);
            this.seleccionadorFecha2.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(184, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Día:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(184, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Semana:";
            // 
            // dropDownList8
            // 
            this.dropDownList8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList8.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList8.FormattingEnabled = true;
            this.dropDownList8.Location = new System.Drawing.Point(239, 70);
            this.dropDownList8.Name = "dropDownList8";
            this.dropDownList8.Size = new System.Drawing.Size(121, 21);
            this.dropDownList8.TabIndex = 24;
            // 
            // dropDownList9
            // 
            this.dropDownList9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList9.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList9.FormattingEnabled = true;
            this.dropDownList9.Location = new System.Drawing.Point(469, 70);
            this.dropDownList9.Name = "dropDownList9";
            this.dropDownList9.Size = new System.Drawing.Size(121, 21);
            this.dropDownList9.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(366, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Centro de trabajo:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(366, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 27;
            this.label15.Text = "Sector:";
            // 
            // dropDownList10
            // 
            this.dropDownList10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDownList10.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropDownList10.FormattingEnabled = true;
            this.dropDownList10.Location = new System.Drawing.Point(469, 44);
            this.dropDownList10.Name = "dropDownList10";
            this.dropDownList10.Size = new System.Drawing.Size(121, 21);
            this.dropDownList10.TabIndex = 28;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Location = new System.Drawing.Point(638, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 161);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(20, 35);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Planificación";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(20, 65);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Individual";
            this.radioButton2.UseVisualStyleBackColor = true;
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
            this.Text = "Orden de Trabajo";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcOrdenTrabajo.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.tcBusqueda.ResumeLayout(false);
            this.tpBuscarPorOtro.ResumeLayout(false);
            this.gbBuscarOtros.ResumeLayout(false);
            this.gbBuscarOtros.PerformLayout();
            this.tpBuscarPorPlan.ResumeLayout(false);
            this.gbBuscarPlan.ResumeLayout(false);
            this.gbBuscarPlan.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaOrdenTrabajo)).EndInit();
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcOrdenTrabajo;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvListaOrdenTrabajo;
        private System.Windows.Forms.GroupBox gbBuscarOtros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox gbBuscarPlan;
        private System.Windows.Forms.TreeView tvPlan;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList6;
        private System.Windows.Forms.Label label8;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tcBusqueda;
        private System.Windows.Forms.TabPage tpBuscarPorOtro;
        private System.Windows.Forms.TabPage tpBuscarPorPlan;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList4;
        private System.Windows.Forms.Label label6;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList3;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList1;
        private System.Windows.Forms.Label label1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList7;
        private System.Windows.Forms.Label label9;
        private SlickInterface.Slide slideBusquedaOtro;
        private SlickInterface.SlideControl slideControl;
        private SlickInterface.Slide slideBuscarPlan;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha seleccionadorFecha2;
        private GyCAP.UI.Sistema.ControlesUsuarios.seleccionadorFecha seleccionadorFecha1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList9;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList dropDownList8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ToolStripButton btnNuevo;
    }
}