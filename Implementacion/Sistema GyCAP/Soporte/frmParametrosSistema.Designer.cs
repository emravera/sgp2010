namespace GyCAP.UI.Soporte
{
    partial class frmParametrosSistema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParametrosSistema));
            this.tpBuscar = new System.Windows.Forms.TabPage();
            this.gbListadoBusqueda = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.txtNombreBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tcParametros = new System.Windows.Forms.TabControl();
            this.tpDatos = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbGuardarCancelar = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnConsultar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValorBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpBuscar.SuspendLayout();
            this.gbListadoBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.gbBusqueda.SuspendLayout();
            this.tcParametros.SuspendLayout();
            this.tpDatos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbGuardarCancelar.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tpBuscar
            // 
            this.tpBuscar.Controls.Add(this.gbListadoBusqueda);
            this.tpBuscar.Controls.Add(this.gbBusqueda);
            this.tpBuscar.Location = new System.Drawing.Point(4, 5);
            this.tpBuscar.Name = "tpBuscar";
            this.tpBuscar.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuscar.Size = new System.Drawing.Size(566, 295);
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
            this.gbListadoBusqueda.Size = new System.Drawing.Size(560, 228);
            this.gbListadoBusqueda.TabIndex = 1;
            this.gbListadoBusqueda.TabStop = false;
            this.gbListadoBusqueda.Text = "Listado de Parámetros del Sistema";
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(9, 22);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(542, 197);
            this.dgvLista.TabIndex = 0;
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBusqueda.Controls.Add(this.txtValorBuscar);
            this.gbBusqueda.Controls.Add(this.label2);
            this.gbBusqueda.Controls.Add(this.btnBuscar);
            this.gbBusqueda.Controls.Add(this.txtNombreBuscar);
            this.gbBusqueda.Controls.Add(this.label1);
            this.gbBusqueda.Location = new System.Drawing.Point(3, 3);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(560, 55);
            this.gbBusqueda.TabIndex = 0;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Criterios de búsqueda";
            // 
            // txtNombreBuscar
            // 
            this.txtNombreBuscar.CausesValidation = false;
            this.txtNombreBuscar.Location = new System.Drawing.Point(83, 23);
            this.txtNombreBuscar.MaxLength = 80;
            this.txtNombreBuscar.Name = "txtNombreBuscar";
            this.txtNombreBuscar.Size = new System.Drawing.Size(149, 20);
            this.txtNombreBuscar.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre:";
            // 
            // tcParametros
            // 
            this.tcParametros.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcParametros.Controls.Add(this.tpBuscar);
            this.tcParametros.Controls.Add(this.tpDatos);
            this.tcParametros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcParametros.ItemSize = new System.Drawing.Size(0, 1);
            this.tcParametros.Location = new System.Drawing.Point(2, 54);
            this.tcParametros.Margin = new System.Windows.Forms.Padding(0);
            this.tcParametros.Multiline = true;
            this.tcParametros.Name = "tcParametros";
            this.tcParametros.Padding = new System.Drawing.Point(0, 0);
            this.tcParametros.SelectedIndex = 0;
            this.tcParametros.Size = new System.Drawing.Size(574, 304);
            this.tcParametros.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcParametros.TabIndex = 8;
            // 
            // tpDatos
            // 
            this.tpDatos.Controls.Add(this.groupBox3);
            this.tpDatos.Controls.Add(this.gbGuardarCancelar);
            this.tpDatos.Controls.Add(this.gbDatos);
            this.tpDatos.Location = new System.Drawing.Point(4, 5);
            this.tpDatos.Margin = new System.Windows.Forms.Padding(1);
            this.tpDatos.Name = "tpDatos";
            this.tpDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatos.Size = new System.Drawing.Size(566, 295);
            this.tpDatos.TabIndex = 1;
            this.tpDatos.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 289);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // gbGuardarCancelar
            // 
            this.gbGuardarCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGuardarCancelar.Controls.Add(this.btnVolver);
            this.gbGuardarCancelar.Controls.Add(this.btnGuardar);
            this.gbGuardarCancelar.Location = new System.Drawing.Point(177, 235);
            this.gbGuardarCancelar.Margin = new System.Windows.Forms.Padding(1);
            this.gbGuardarCancelar.Name = "gbGuardarCancelar";
            this.gbGuardarCancelar.Size = new System.Drawing.Size(386, 57);
            this.gbGuardarCancelar.TabIndex = 1;
            this.gbGuardarCancelar.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(306, 19);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(64, 26);
            this.btnVolver.TabIndex = 11;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardar.Location = new System.Drawing.Point(236, 19);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(64, 26);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // gbDatos
            // 
            this.gbDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatos.Controls.Add(this.txtValor);
            this.gbDatos.Controls.Add(this.lblValor);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Controls.Add(this.label3);
            this.gbDatos.Location = new System.Drawing.Point(177, 3);
            this.gbDatos.Margin = new System.Windows.Forms.Padding(1);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(386, 230);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del Parámetro del Sistema";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(103, 35);
            this.txtNombre.MaxLength = 80;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(179, 20);
            this.txtNombre.TabIndex = 7;
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
            this.tsMenu.Size = new System.Drawing.Size(574, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcParametros, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 360);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Image = global::GyCAP.UI.Soporte.Properties.Resources.lupa_20;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(460, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 26);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox2.Image = global::GyCAP.UI.Soporte.Properties.Resources.System_Preferences_128;
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(15, 140);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(139, 137);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox1.Image = global::GyCAP.UI.Soporte.Properties.Resources.System_128;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(139, 144);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::GyCAP.UI.Soporte.Properties.Resources.New_25;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 47);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = global::GyCAP.UI.Soporte.Properties.Resources.Find_25;
            this.btnConsultar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(57, 47);
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::GyCAP.UI.Soporte.Properties.Resources.Text_Editor_25;
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(54, 47);
            this.btnModificar.Text = "&Modificar";
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::GyCAP.UI.Soporte.Properties.Resources.Delete_25;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 47);
            this.btnEliminar.Text = "&Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.Soporte.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(103, 67);
            this.txtValor.MaxLength = 80;
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(179, 20);
            this.txtValor.TabIndex = 9;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(44, 70);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(34, 13);
            this.lblValor.TabIndex = 8;
            this.lblValor.Text = "Valor:";
            // 
            // txtValorBuscar
            // 
            this.txtValorBuscar.CausesValidation = false;
            this.txtValorBuscar.Location = new System.Drawing.Point(291, 23);
            this.txtValorBuscar.MaxLength = 80;
            this.txtValorBuscar.Name = "txtValorBuscar";
            this.txtValorBuscar.Size = new System.Drawing.Size(149, 20);
            this.txtValorBuscar.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Valor:";
            // 
            // frmParametrosSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmParametrosSistema";
            this.Text = "Parámetros del Sistema";
            this.tpBuscar.ResumeLayout(false);
            this.gbListadoBusqueda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.tcParametros.ResumeLayout(false);
            this.tpDatos.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.gbGuardarCancelar.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpBuscar;
        private System.Windows.Forms.GroupBox gbListadoBusqueda;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombreBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tcParametros;
        private System.Windows.Forms.TabPage tpDatos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtValorBuscar;
        private System.Windows.Forms.Label label2;
    }
}