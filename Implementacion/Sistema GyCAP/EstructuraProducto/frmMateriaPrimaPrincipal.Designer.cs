namespace GyCAP.UI.EstructuraProducto
{
    partial class frmMateriaPrimaPrincipal
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
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gbListadoBusqueda = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.rbTodosBuscar = new System.Windows.Forms.RadioButton();
            this.rbNOPcipalBuscar = new System.Windows.Forms.RadioButton();
            this.rbPcipalBuscar = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tcMateriaPrima = new System.Windows.Forms.TabControl();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.cbUnidadMedida = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label9 = new System.Windows.Forms.Label();
            this.cbUbicacionStock = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label8 = new System.Windows.Forms.Label();
            this.numCosto = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTipoUnMedida = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.gbEsPrincipal = new System.Windows.Forms.GroupBox();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.rbNOPcipalDatos = new System.Windows.Forms.RadioButton();
            this.rbPcipalDatos = new System.Windows.Forms.RadioButton();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tpBuscar.SuspendLayout();
            this.gbListadoBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.gbBusqueda.SuspendLayout();
            this.tcMateriaPrima.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).BeginInit();
            this.gbEsPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.gbListadoBusqueda);
            this.tpBuscar.Controls.Add(this.gbBusqueda);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(622, 353);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // gbListadoBusqueda
            // 
            this.gbListadoBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbListadoBusqueda.Controls.Add(this.dgvLista);
            this.gbListadoBusqueda.Location = new System.Drawing.Point(3, 64);
            this.gbListadoBusqueda.Name = "gbListadoBusqueda";
            this.gbListadoBusqueda.Padding = new System.Windows.Forms.Padding(9);
            this.gbListadoBusqueda.Size = new System.Drawing.Size(616, 286);
            this.gbListadoBusqueda.TabIndex = 1;
            this.gbListadoBusqueda.TabStop = false;
            this.gbListadoBusqueda.Text = "Listado de Materias Primas";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.CausesValidation = false;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 23);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(598, 254);
            this.dgvLista.TabIndex = 0;
            this.dgvLista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLista_CellFormatting_1);
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBusqueda.Controls.Add(this.rbTodosBuscar);
            this.gbBusqueda.Controls.Add(this.rbNOPcipalBuscar);
            this.gbBusqueda.Controls.Add(this.rbPcipalBuscar);
            this.gbBusqueda.Controls.Add(this.label4);
            this.gbBusqueda.Controls.Add(this.btnBuscar);
            this.gbBusqueda.Controls.Add(this.txtNombreBuscar);
            this.gbBusqueda.Controls.Add(this.label1);
            this.gbBusqueda.Location = new System.Drawing.Point(3, 3);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(616, 55);
            this.gbBusqueda.TabIndex = 0;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Criterios de búsqueda";
            // 
            // rbTodosBuscar
            // 
            this.rbTodosBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbTodosBuscar.AutoSize = true;
            this.rbTodosBuscar.CausesValidation = false;
            this.rbTodosBuscar.Location = new System.Drawing.Point(408, 23);
            this.rbTodosBuscar.Name = "rbTodosBuscar";
            this.rbTodosBuscar.Size = new System.Drawing.Size(54, 17);
            this.rbTodosBuscar.TabIndex = 8;
            this.rbTodosBuscar.TabStop = true;
            this.rbTodosBuscar.Text = "Todas";
            this.rbTodosBuscar.UseVisualStyleBackColor = true;
            // 
            // rbNOPcipalBuscar
            // 
            this.rbNOPcipalBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbNOPcipalBuscar.AutoSize = true;
            this.rbNOPcipalBuscar.CausesValidation = false;
            this.rbNOPcipalBuscar.Location = new System.Drawing.Point(367, 23);
            this.rbNOPcipalBuscar.Name = "rbNOPcipalBuscar";
            this.rbNOPcipalBuscar.Size = new System.Drawing.Size(38, 17);
            this.rbNOPcipalBuscar.TabIndex = 7;
            this.rbNOPcipalBuscar.TabStop = true;
            this.rbNOPcipalBuscar.Text = "No";
            this.rbNOPcipalBuscar.UseVisualStyleBackColor = true;
            // 
            // rbPcipalBuscar
            // 
            this.rbPcipalBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbPcipalBuscar.AutoSize = true;
            this.rbPcipalBuscar.CausesValidation = false;
            this.rbPcipalBuscar.Location = new System.Drawing.Point(326, 23);
            this.rbPcipalBuscar.Name = "rbPcipalBuscar";
            this.rbPcipalBuscar.Size = new System.Drawing.Size(33, 17);
            this.rbPcipalBuscar.TabIndex = 6;
            this.rbPcipalBuscar.TabStop = true;
            this.rbPcipalBuscar.Text = "Si";
            this.rbPcipalBuscar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Principal:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(516, 19);
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
            this.txtNombreBuscar.Location = new System.Drawing.Point(65, 21);
            this.txtNombreBuscar.MaxLength = 80;
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(144, 21);
            this.txtNombreBuscar.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Unidad Medida:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // tcMateriaPrima
            // 
            this.tcMateriaPrima.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcMateriaPrima.Controls.Add(this.tpBuscar);
            this.tcMateriaPrima.Controls.Add(this.tpDatos);
            this.tcMateriaPrima.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMateriaPrima.ItemSize = new System.Drawing.Size(0, 1);
            this.tcMateriaPrima.Location = new System.Drawing.Point(2, 54);
            this.tcMateriaPrima.Margin = new System.Windows.Forms.Padding(0);
            this.tcMateriaPrima.Multiline = true;
            this.tcMateriaPrima.Name = "tcMateriaPrima";
            this.tcMateriaPrima.Padding = new System.Drawing.Point(0, 0);
            this.tcMateriaPrima.SelectedIndex = 0;
            this.tcMateriaPrima.Size = new System.Drawing.Size(630, 362);
            this.tcMateriaPrima.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcMateriaPrima.TabIndex = 8;
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(622, 353);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Location = new System.Drawing.Point(0, 302);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(617, 46);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(537, 14);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 11;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardar.Location = new System.Drawing.Point(467, 14);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gbDatos
            // 
            this.gbDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatos.Controls.Add(this.cbUnidadMedida);
            this.gbDatos.Controls.Add(this.label9);
            this.gbDatos.Controls.Add(this.cbUbicacionStock);
            this.gbDatos.Controls.Add(this.label8);
            this.gbDatos.Controls.Add(this.numCosto);
            this.gbDatos.Controls.Add(this.label7);
            this.gbDatos.Controls.Add(this.txtDescripcion);
            this.gbDatos.Controls.Add(this.label6);
            this.gbDatos.Controls.Add(this.cbTipoUnMedida);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label5);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Controls.Add(this.gbEsPrincipal);
            this.gbDatos.Location = new System.Drawing.Point(4, 4);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(613, 297);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la Materia Prima";
            // 
            // cbUnidadMedida
            // 
            this.cbUnidadMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnidadMedida.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbUnidadMedida.FormattingEnabled = true;
            this.cbUnidadMedida.Location = new System.Drawing.Point(396, 117);
            this.cbUnidadMedida.Name = "cbUnidadMedida";
            this.cbUnidadMedida.Size = new System.Drawing.Size(155, 21);
            this.cbUnidadMedida.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(98, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Principal:";
            // 
            // cbUbicacionStock
            // 
            this.cbUbicacionStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUbicacionStock.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbUbicacionStock.FormattingEnabled = true;
            this.cbUbicacionStock.Location = new System.Drawing.Point(153, 172);
            this.cbUbicacionStock.Name = "cbUbicacionStock";
            this.cbUbicacionStock.Size = new System.Drawing.Size(155, 21);
            this.cbUbicacionStock.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Ubicación Stock:";
            // 
            // numCosto
            // 
            this.numCosto.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCosto.Location = new System.Drawing.Point(153, 145);
            this.numCosto.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numCosto.Name = "numCosto";
            this.numCosto.Size = new System.Drawing.Size(79, 21);
            this.numCosto.TabIndex = 5;
            this.numCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Costo/Unidad:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(153, 58);
            this.txtDescripcion.MaxLength = 80;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(398, 51);
            this.txtDescripcion.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Descripción:";
            // 
            // cbTipoUnMedida
            // 
            this.cbTipoUnMedida.CausesValidation = false;
            this.cbTipoUnMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoUnMedida.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbTipoUnMedida.FormattingEnabled = true;
            this.cbTipoUnMedida.Location = new System.Drawing.Point(153, 116);
            this.cbTipoUnMedida.Name = "cbTipoUnMedida";
            this.cbTipoUnMedida.Size = new System.Drawing.Size(155, 21);
            this.cbTipoUnMedida.TabIndex = 3;
            this.cbTipoUnMedida.DropDownClosed += new System.EventHandler(this.cbTipoUnMedida_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tipo Unidad Medida:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(153, 32);
            this.txtNombre.MaxLength = 80;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(398, 21);
            this.txtNombre.TabIndex = 1;
            // 
            // gbEsPrincipal
            // 
            this.gbEsPrincipal.Controls.Add(this.numCantidad);
            this.gbEsPrincipal.Controls.Add(this.label10);
            this.gbEsPrincipal.Controls.Add(this.rbNOPcipalDatos);
            this.gbEsPrincipal.Controls.Add(this.rbPcipalDatos);
            this.gbEsPrincipal.Location = new System.Drawing.Point(152, 191);
            this.gbEsPrincipal.Name = "gbEsPrincipal";
            this.gbEsPrincipal.Size = new System.Drawing.Size(399, 43);
            this.gbEsPrincipal.TabIndex = 22;
            this.gbEsPrincipal.TabStop = false;
            // 
            // numCantidad
            // 
            this.numCantidad.CausesValidation = false;
            this.numCantidad.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCantidad.Location = new System.Drawing.Point(314, 15);
            this.numCantidad.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(79, 21);
            this.numCantidad.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(257, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Cantidad:";
            // 
            // rbNOPcipalDatos
            // 
            this.rbNOPcipalDatos.AutoSize = true;
            this.rbNOPcipalDatos.CausesValidation = false;
            this.rbNOPcipalDatos.Location = new System.Drawing.Point(63, 16);
            this.rbNOPcipalDatos.Name = "rbNOPcipalDatos";
            this.rbNOPcipalDatos.Size = new System.Drawing.Size(38, 17);
            this.rbNOPcipalDatos.TabIndex = 8;
            this.rbNOPcipalDatos.TabStop = true;
            this.rbNOPcipalDatos.Text = "No";
            this.rbNOPcipalDatos.UseVisualStyleBackColor = true;
            this.rbNOPcipalDatos.CheckedChanged += new System.EventHandler(this.rbNOPcipalDatos_CheckedChanged);
            // 
            // rbPcipalDatos
            // 
            this.rbPcipalDatos.AutoSize = true;
            this.rbPcipalDatos.CausesValidation = false;
            this.rbPcipalDatos.Location = new System.Drawing.Point(22, 16);
            this.rbPcipalDatos.Name = "rbPcipalDatos";
            this.rbPcipalDatos.Size = new System.Drawing.Size(35, 17);
            this.rbPcipalDatos.TabIndex = 7;
            this.rbPcipalDatos.TabStop = true;
            this.rbPcipalDatos.Text = "SI";
            this.rbPcipalDatos.UseVisualStyleBackColor = true;
            this.rbPcipalDatos.CheckedChanged += new System.EventHandler(this.rbPcipalDatos_CheckedChanged);
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
            this.tsMenu.Size = new System.Drawing.Size(630, 50);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcMateriaPrima, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 418);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // frmMateriaPrimaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 418);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMateriaPrimaPrincipal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Materia Prima";
            this.tpBuscar.ResumeLayout(false);
            this.gbListadoBusqueda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.tcMateriaPrima.ResumeLayout(false);
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).EndInit();
            this.gbEsPrincipal.ResumeLayout(false);
            this.gbEsPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox gbListadoBusqueda;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tcMateriaPrima;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton rbNOPcipalBuscar;
        private System.Windows.Forms.RadioButton rbPcipalBuscar;
        private System.Windows.Forms.RadioButton rbTodosBuscar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label6;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbTipoUnMedida;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCosto;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbUbicacionStock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbEsPrincipal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbNOPcipalDatos;
        private System.Windows.Forms.RadioButton rbPcipalDatos;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbUnidadMedida;

    }
}