namespace GyCAP.UI.GestionStock
{
    partial class frmInventarioABC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventarioABC));
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.cbAñoHistorico = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.rbHistorico = new System.Windows.Forms.RadioButton();
            this.rbNuevo = new System.Windows.Forms.RadioButton();
            this.cbAñoInventario = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.btnGenerarInventario = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gbDatosCocinas = new System.Windows.Forms.GroupBox();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnSumar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCalcularABC = new System.Windows.Forms.Button();
            this.dgvModelos = new System.Windows.Forms.DataGridView();
            this.gbCargaPorcentaje = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.cbCocinas = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCantAnual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMateriasPrimas = new System.Windows.Forms.GroupBox();
            this.dgvMP = new System.Windows.Forms.DataGridView();
            this.gbDatosPrincipales.SuspendLayout();
            this.gbDatosCocinas.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).BeginInit();
            this.gbCargaPorcentaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).BeginInit();
            this.gbMateriasPrimas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMP)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatosPrincipales.Controls.Add(this.cbAñoHistorico);
            this.gbDatosPrincipales.Controls.Add(this.rbHistorico);
            this.gbDatosPrincipales.Controls.Add(this.rbNuevo);
            this.gbDatosPrincipales.Controls.Add(this.cbAñoInventario);
            this.gbDatosPrincipales.Controls.Add(this.btnGenerarInventario);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 9);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(807, 53);
            this.gbDatosPrincipales.TabIndex = 16;
            this.gbDatosPrincipales.TabStop = false;
            this.gbDatosPrincipales.Text = "Datos Principales";
            // 
            // cbAñoHistorico
            // 
            this.cbAñoHistorico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAñoHistorico.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbAñoHistorico.FormattingEnabled = true;
            this.cbAñoHistorico.Location = new System.Drawing.Point(495, 20);
            this.cbAñoHistorico.Name = "cbAñoHistorico";
            this.cbAñoHistorico.Size = new System.Drawing.Size(109, 21);
            this.cbAñoHistorico.TabIndex = 31;
            // 
            // rbHistorico
            // 
            this.rbHistorico.AutoSize = true;
            this.rbHistorico.Location = new System.Drawing.Point(369, 22);
            this.rbHistorico.Name = "rbHistorico";
            this.rbHistorico.Size = new System.Drawing.Size(120, 17);
            this.rbHistorico.TabIndex = 30;
            this.rbHistorico.TabStop = true;
            this.rbHistorico.Text = "Basado en Histórico";
            this.rbHistorico.UseVisualStyleBackColor = true;
            this.rbHistorico.CheckedChanged += new System.EventHandler(this.rbHistorico_CheckedChanged);
            // 
            // rbNuevo
            // 
            this.rbNuevo.AutoSize = true;
            this.rbNuevo.Location = new System.Drawing.Point(247, 22);
            this.rbNuevo.Name = "rbNuevo";
            this.rbNuevo.Size = new System.Drawing.Size(107, 17);
            this.rbNuevo.TabIndex = 29;
            this.rbNuevo.TabStop = true;
            this.rbNuevo.Text = "Nuevo Inventario";
            this.rbNuevo.UseVisualStyleBackColor = true;
            this.rbNuevo.CheckedChanged += new System.EventHandler(this.rbNuevo_CheckedChanged);
            // 
            // cbAñoInventario
            // 
            this.cbAñoInventario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAñoInventario.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbAñoInventario.FormattingEnabled = true;
            this.cbAñoInventario.Location = new System.Drawing.Point(123, 20);
            this.cbAñoInventario.Name = "cbAñoInventario";
            this.cbAñoInventario.Size = new System.Drawing.Size(95, 21);
            this.cbAñoInventario.TabIndex = 28;
            // 
            // btnGenerarInventario
            // 
            this.btnGenerarInventario.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGenerarInventario.Location = new System.Drawing.Point(705, 19);
            this.btnGenerarInventario.Name = "btnGenerarInventario";
            this.btnGenerarInventario.Size = new System.Drawing.Size(82, 23);
            this.btnGenerarInventario.TabIndex = 4;
            this.btnGenerarInventario.Text = "Generar";
            this.btnGenerarInventario.UseVisualStyleBackColor = true;
            this.btnGenerarInventario.Click += new System.EventHandler(this.btnGenerarInventario_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Año Inventario ABC:";
            // 
            // gbDatosCocinas
            // 
            this.gbDatosCocinas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbDatosCocinas.Controls.Add(this.panelAcciones);
            this.gbDatosCocinas.Controls.Add(this.btnVolver);
            this.gbDatosCocinas.Controls.Add(this.btnCalcularABC);
            this.gbDatosCocinas.Controls.Add(this.dgvModelos);
            this.gbDatosCocinas.Controls.Add(this.gbCargaPorcentaje);
            this.gbDatosCocinas.Controls.Add(this.txtCantAnual);
            this.gbDatosCocinas.Controls.Add(this.label1);
            this.gbDatosCocinas.Location = new System.Drawing.Point(3, 68);
            this.gbDatosCocinas.Name = "gbDatosCocinas";
            this.gbDatosCocinas.Size = new System.Drawing.Size(342, 384);
            this.gbDatosCocinas.TabIndex = 17;
            this.gbDatosCocinas.TabStop = false;
            this.gbDatosCocinas.Text = "Datos Cocinas a Producir";
            // 
            // panelAcciones
            // 
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAcciones.Controls.Add(this.label7);
            this.panelAcciones.Controls.Add(this.btnRestar);
            this.panelAcciones.Controls.Add(this.btnSumar);
            this.panelAcciones.Controls.Add(this.label6);
            this.panelAcciones.Controls.Add(this.btnDelete);
            this.panelAcciones.Location = new System.Drawing.Point(6, 295);
            this.panelAcciones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(325, 47);
            this.panelAcciones.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(170, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Porcentaje";
            // 
            // btnRestar
            // 
            this.btnRestar.FlatAppearance.BorderSize = 0;
            this.btnRestar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestar.Image = ((System.Drawing.Image)(resources.GetObject("btnRestar.Image")));
            this.btnRestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestar.Location = new System.Drawing.Point(200, 1);
            this.btnRestar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(30, 30);
            this.btnRestar.TabIndex = 19;
            this.btnRestar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnSumar
            // 
            this.btnSumar.FlatAppearance.BorderSize = 0;
            this.btnSumar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSumar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSumar.Image = ((System.Drawing.Image)(resources.GetObject("btnSumar.Image")));
            this.btnSumar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSumar.Location = new System.Drawing.Point(164, 1);
            this.btnSumar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSumar.Name = "btnSumar";
            this.btnSumar.Size = new System.Drawing.Size(30, 30);
            this.btnSumar.TabIndex = 18;
            this.btnSumar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSumar.UseVisualStyleBackColor = true;
            this.btnSumar.Click += new System.EventHandler(this.btnSumar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Eliminar";
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Delete_25;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(28, -2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(144, 347);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(90, 25);
            this.btnVolver.TabIndex = 30;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCalcularABC
            // 
            this.btnCalcularABC.Location = new System.Drawing.Point(241, 347);
            this.btnCalcularABC.Name = "btnCalcularABC";
            this.btnCalcularABC.Size = new System.Drawing.Size(90, 25);
            this.btnCalcularABC.TabIndex = 29;
            this.btnCalcularABC.Text = "Calcular ABC";
            this.btnCalcularABC.UseVisualStyleBackColor = true;
            this.btnCalcularABC.Click += new System.EventHandler(this.btnCalcularABC_Click);
            // 
            // dgvModelos
            // 
            this.dgvModelos.AllowUserToAddRows = false;
            this.dgvModelos.AllowUserToDeleteRows = false;
            this.dgvModelos.AllowUserToResizeRows = false;
            this.dgvModelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModelos.Location = new System.Drawing.Point(6, 136);
            this.dgvModelos.Name = "dgvModelos";
            this.dgvModelos.ReadOnly = true;
            this.dgvModelos.RowHeadersVisible = false;
            this.dgvModelos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModelos.Size = new System.Drawing.Size(325, 154);
            this.dgvModelos.TabIndex = 28;
            this.dgvModelos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvModelos_CellFormatting);
            this.dgvModelos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvModelos_DataBindingComplete);
            // 
            // gbCargaPorcentaje
            // 
            this.gbCargaPorcentaje.Controls.Add(this.label2);
            this.gbCargaPorcentaje.Controls.Add(this.numPorcentaje);
            this.gbCargaPorcentaje.Controls.Add(this.btnAgregar);
            this.gbCargaPorcentaje.Controls.Add(this.cbCocinas);
            this.gbCargaPorcentaje.Controls.Add(this.label5);
            this.gbCargaPorcentaje.Location = new System.Drawing.Point(6, 51);
            this.gbCargaPorcentaje.Name = "gbCargaPorcentaje";
            this.gbCargaPorcentaje.Size = new System.Drawing.Size(325, 79);
            this.gbCargaPorcentaje.TabIndex = 27;
            this.gbCargaPorcentaje.TabStop = false;
            this.gbCargaPorcentaje.Text = "Carga Porcentajes Anuales";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Porcentaje (%)";
            // 
            // numPorcentaje
            // 
            this.numPorcentaje.Location = new System.Drawing.Point(106, 45);
            this.numPorcentaje.Name = "numPorcentaje";
            this.numPorcentaje.Size = new System.Drawing.Size(99, 20);
            this.numPorcentaje.TabIndex = 15;
            this.numPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(240, 44);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(76, 23);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // cbCocinas
            // 
            this.cbCocinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCocinas.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbCocinas.FormattingEnabled = true;
            this.cbCocinas.Location = new System.Drawing.Point(106, 17);
            this.cbCocinas.Name = "cbCocinas";
            this.cbCocinas.Size = new System.Drawing.Size(210, 21);
            this.cbCocinas.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cocina a Producir:";
            // 
            // txtCantAnual
            // 
            this.txtCantAnual.Location = new System.Drawing.Point(195, 25);
            this.txtCantAnual.Name = "txtCantAnual";
            this.txtCantAnual.Size = new System.Drawing.Size(136, 20);
            this.txtCantAnual.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Cantidad Total a Producir en el Año:";
            // 
            // gbMateriasPrimas
            // 
            this.gbMateriasPrimas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMateriasPrimas.Controls.Add(this.dgvMP);
            this.gbMateriasPrimas.Location = new System.Drawing.Point(351, 68);
            this.gbMateriasPrimas.Name = "gbMateriasPrimas";
            this.gbMateriasPrimas.Size = new System.Drawing.Size(459, 384);
            this.gbMateriasPrimas.TabIndex = 18;
            this.gbMateriasPrimas.TabStop = false;
            this.gbMateriasPrimas.Text = "Clasificación Inventario ABC";
            // 
            // dgvMP
            // 
            this.dgvMP.AllowUserToAddRows = false;
            this.dgvMP.AllowUserToDeleteRows = false;
            this.dgvMP.AllowUserToResizeRows = false;
            this.dgvMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMP.Location = new System.Drawing.Point(3, 16);
            this.dgvMP.Name = "dgvMP";
            this.dgvMP.RowHeadersVisible = false;
            this.dgvMP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMP.Size = new System.Drawing.Size(453, 365);
            this.dgvMP.TabIndex = 29;
            this.dgvMP.DataMemberChanged += new System.EventHandler(this.dgvMP_DataBindingComplete);
            this.dgvMP.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMP_CellFormatting);
            // 
            // frmInventarioABC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 464);
            this.Controls.Add(this.gbMateriasPrimas);
            this.Controls.Add(this.gbDatosCocinas);
            this.Controls.Add(this.gbDatosPrincipales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmInventarioABC";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Inventario ABC";
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.gbDatosCocinas.ResumeLayout(false);
            this.gbDatosCocinas.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).EndInit();
            this.gbCargaPorcentaje.ResumeLayout(false);
            this.gbCargaPorcentaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).EndInit();
            this.gbMateriasPrimas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbAñoHistorico;
        private System.Windows.Forms.RadioButton rbHistorico;
        private System.Windows.Forms.RadioButton rbNuevo;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbAñoInventario;
        private System.Windows.Forms.Button btnGenerarInventario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbDatosCocinas;
        private System.Windows.Forms.TextBox txtCantAnual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbCargaPorcentaje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPorcentaje;
        private System.Windows.Forms.Button btnAgregar;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbCocinas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCalcularABC;
        private System.Windows.Forms.GroupBox gbMateriasPrimas;
        private System.Windows.Forms.DataGridView dgvMP;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnSumar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvModelos;

    }
}