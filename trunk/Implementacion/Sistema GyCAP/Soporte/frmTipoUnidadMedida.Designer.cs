namespace GyCAP.UI.Soporte
{
    partial class frmTipoUnidadMedida
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
            this.tcTipoUnidadMedida = new System.Windows.Forms.TabControl();
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tcTipoUnidadMedida.SuspendLayout();
            this.tpBuscar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcTipoUnidadMedida, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 364);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tcTipoUnidadMedida
            // 
            this.tcTipoUnidadMedida.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcTipoUnidadMedida.Controls.Add(this.tpBuscar);
            this.tcTipoUnidadMedida.Controls.Add(this.tpDatos);
            this.tcTipoUnidadMedida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTipoUnidadMedida.ItemSize = new System.Drawing.Size(0, 1);
            this.tcTipoUnidadMedida.Location = new System.Drawing.Point(2, 54);
            this.tcTipoUnidadMedida.Margin = new System.Windows.Forms.Padding(0);
            this.tcTipoUnidadMedida.Multiline = true;
            this.tcTipoUnidadMedida.Name = "tcTipoUnidadMedida";
            this.tcTipoUnidadMedida.Padding = new System.Drawing.Point(0, 0);
            this.tcTipoUnidadMedida.SelectedIndex = 0;
            this.tcTipoUnidadMedida.Size = new System.Drawing.Size(511, 308);
            this.tcTipoUnidadMedida.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcTipoUnidadMedida.TabIndex = 8;
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.groupBox2);
            this.tpBuscar.Controls.Add(this.groupBox1);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(503, 299);
            this.tpBuscar.TabIndex = 0;
            this.tpBuscar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvLista);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(497, 213);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Tipo de Unidad de Medida";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(10, 27);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(477, 176);
            this.dgvLista.TabIndex = 0;
            this.dgvLista.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_RowEnter);
            this.dgvLista.DoubleClick += new System.EventHandler(this.dgvLista_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtNombreBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterios de búsqueda";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.Soporte.Properties.Resources.lupa_25;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(399, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombreBuscar
            // 
            this.txtNombreBuscar.Location = new System.Drawing.Point(83, 28);
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(224, 24);
            this.txtNombreBuscar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(503, 299);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbGuardarCancelar.Location = new System.Drawing.Point(3, 230);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(497, 66);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(250, 23);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(148, 23);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.txtCodigo);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatos.Location = new System.Drawing.Point(3, 3);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(497, 225);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos Tipo Unidad de Medida";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(100, 119);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(170, 24);
            this.txtNombre.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(100, 60);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(170, 24);
            this.txtCodigo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Código:";
            // 
            // tsMenu
            // 
            this.tsMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMenu.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tsMenu.Size = new System.Drawing.Size(511, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.Soporte.Properties.Resources.New_25;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(45, 47);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = global::GyCAP.UI.Soporte.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(65, 47);
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::GyCAP.UI.Soporte.Properties.Resources.Text_Editor_25;
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(66, 47);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.Soporte.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(58, 47);
            this.btnEliminar.Text = "Eliminar";
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
            this.btnSalir.Image = global::GyCAP.UI.Soporte.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(37, 47);
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmTipoUnidadMedida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 370);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTipoUnidadMedida";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Tipo de Unidad de Medida";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTipoUnidadMedida_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcTipoUnidadMedida.ResumeLayout(false);
            this.tpBuscar.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpDatos.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcTipoUnidadMedida;
        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox gbGuardarCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnConsultar;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;



    }
}