namespace GyCAP.UI.EstructuraProducto
{
    partial class frmListadoEstructura
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
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbOrdenPor = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbOrdenForma = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.chkMateriaPrima = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPartes = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCocinaBuscar = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvEstructuras = new System.Windows.Forms.DataGridView();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstructuras)).BeginInit();
            this.SuspendLayout();
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
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(2, 2);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0);
            this.tsMenu.Size = new System.Drawing.Size(613, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
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
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 470);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbOrdenPor);
            this.groupBox3.Controls.Add(this.cbOrdenForma);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnGenerar);
            this.groupBox3.Controls.Add(this.chkMateriaPrima);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.chkPartes);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(5, 377);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(607, 82);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones de listado";
            // 
            // cbOrdenPor
            // 
            this.cbOrdenPor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrdenPor.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbOrdenPor.FormattingEnabled = true;
            this.cbOrdenPor.Location = new System.Drawing.Point(95, 50);
            this.cbOrdenPor.Name = "cbOrdenPor";
            this.cbOrdenPor.Size = new System.Drawing.Size(136, 21);
            this.cbOrdenPor.TabIndex = 20;
            // 
            // cbOrdenForma
            // 
            this.cbOrdenForma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrdenForma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbOrdenForma.FormattingEnabled = true;
            this.cbOrdenForma.Location = new System.Drawing.Point(297, 50);
            this.cbOrdenForma.Name = "cbOrdenForma";
            this.cbOrdenForma.Size = new System.Drawing.Size(121, 21);
            this.cbOrdenForma.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "En forma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Ordenar por:";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Apply_25;
            this.btnGenerar.Location = new System.Drawing.Point(486, 17);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(93, 56);
            this.btnGenerar.TabIndex = 16;
            this.btnGenerar.Text = "Generar\r\nlistado";
            this.btnGenerar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // chkMateriaPrima
            // 
            this.chkMateriaPrima.AutoSize = true;
            this.chkMateriaPrima.Checked = true;
            this.chkMateriaPrima.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMateriaPrima.Location = new System.Drawing.Point(174, 24);
            this.chkMateriaPrima.Name = "chkMateriaPrima";
            this.chkMateriaPrima.Size = new System.Drawing.Size(101, 17);
            this.chkMateriaPrima.TabIndex = 15;
            this.chkMateriaPrima.Text = "Materias Primas";
            this.chkMateriaPrima.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mostrar:";
            // 
            // chkPartes
            // 
            this.chkPartes.AutoSize = true;
            this.chkPartes.Checked = true;
            this.chkPartes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPartes.Location = new System.Drawing.Point(95, 24);
            this.chkPartes.Name = "chkPartes";
            this.chkPartes.Size = new System.Drawing.Size(57, 17);
            this.chkPartes.TabIndex = 12;
            this.chkPartes.Text = "Partes";
            this.chkPartes.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCocinaBuscar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 50);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione la cocina";
            // 
            // cbCocinaBuscar
            // 
            this.cbCocinaBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCocinaBuscar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCocinaBuscar.FormattingEnabled = true;
            this.cbCocinaBuscar.Location = new System.Drawing.Point(95, 18);
            this.cbCocinaBuscar.Name = "cbCocinaBuscar";
            this.cbCocinaBuscar.Size = new System.Drawing.Size(268, 21);
            this.cbCocinaBuscar.TabIndex = 10;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(449, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(130, 30);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "&Buscar estructuras";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cocina:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvEstructuras);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(5, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 254);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de estructuras";
            // 
            // dgvEstructuras
            // 
            this.dgvEstructuras.AllowUserToAddRows = false;
            this.dgvEstructuras.AllowUserToDeleteRows = false;
            this.dgvEstructuras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstructuras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEstructuras.Location = new System.Drawing.Point(3, 16);
            this.dgvEstructuras.MultiSelect = false;
            this.dgvEstructuras.Name = "dgvEstructuras";
            this.dgvEstructuras.ReadOnly = true;
            this.dgvEstructuras.RowHeadersVisible = false;
            this.dgvEstructuras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstructuras.Size = new System.Drawing.Size(601, 235);
            this.dgvEstructuras.TabIndex = 0;
            this.dgvEstructuras.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEstructuras_CellFormatting);
            // 
            // frmListadoEstructura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 470);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmListadoEstructura";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Listado de Partes de Estructura de Cocina";
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstructuras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCocinaBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvEstructuras;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.CheckBox chkMateriaPrima;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkPartes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbOrdenPor;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbOrdenForma;
    }
}