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
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.cbAñoHistorico = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.rbHistorico = new System.Windows.Forms.RadioButton();
            this.rbNuevo = new System.Windows.Forms.RadioButton();
            this.cbAñoInventario = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.btnGenerarInventario = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gbDatosCocinas = new System.Windows.Forms.GroupBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).BeginInit();
            this.gbCargaPorcentaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentaje)).BeginInit();
            this.gbMateriasPrimas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMP)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Controls.Add(this.cbAñoHistorico);
            this.gbDatosPrincipales.Controls.Add(this.rbHistorico);
            this.gbDatosPrincipales.Controls.Add(this.rbNuevo);
            this.gbDatosPrincipales.Controls.Add(this.cbAñoInventario);
            this.gbDatosPrincipales.Controls.Add(this.btnGenerarInventario);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 9);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(760, 53);
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
            this.cbAñoHistorico.Size = new System.Drawing.Size(95, 21);
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
            this.btnGenerarInventario.Location = new System.Drawing.Point(670, 19);
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
            this.gbDatosCocinas.Controls.Add(this.btnVolver);
            this.gbDatosCocinas.Controls.Add(this.btnCalcularABC);
            this.gbDatosCocinas.Controls.Add(this.dgvModelos);
            this.gbDatosCocinas.Controls.Add(this.gbCargaPorcentaje);
            this.gbDatosCocinas.Controls.Add(this.txtCantAnual);
            this.gbDatosCocinas.Controls.Add(this.label1);
            this.gbDatosCocinas.Location = new System.Drawing.Point(3, 68);
            this.gbDatosCocinas.Name = "gbDatosCocinas";
            this.gbDatosCocinas.Size = new System.Drawing.Size(350, 330);
            this.gbDatosCocinas.TabIndex = 17;
            this.gbDatosCocinas.TabStop = false;
            this.gbDatosCocinas.Text = "Datos Cocinas a Producir";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(156, 301);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(91, 23);
            this.btnVolver.TabIndex = 30;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCalcularABC
            // 
            this.btnCalcularABC.Location = new System.Drawing.Point(253, 301);
            this.btnCalcularABC.Name = "btnCalcularABC";
            this.btnCalcularABC.Size = new System.Drawing.Size(91, 23);
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
            this.dgvModelos.Location = new System.Drawing.Point(9, 136);
            this.dgvModelos.Name = "dgvModelos";
            this.dgvModelos.ReadOnly = true;
            this.dgvModelos.RowHeadersVisible = false;
            this.dgvModelos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModelos.Size = new System.Drawing.Size(335, 159);
            this.dgvModelos.TabIndex = 28;
            this.dgvModelos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvModelos_CellFormatting);
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
            this.gbCargaPorcentaje.Size = new System.Drawing.Size(338, 79);
            this.gbCargaPorcentaje.TabIndex = 27;
            this.gbCargaPorcentaje.TabStop = false;
            this.gbCargaPorcentaje.Text = "Carga Porcentajes Anuales";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Porcentaje (%)";
            // 
            // numPorcentaje
            // 
            this.numPorcentaje.Location = new System.Drawing.Point(113, 47);
            this.numPorcentaje.Name = "numPorcentaje";
            this.numPorcentaje.Size = new System.Drawing.Size(99, 20);
            this.numPorcentaje.TabIndex = 15;
            this.numPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(256, 46);
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
            this.cbCocinas.Location = new System.Drawing.Point(113, 18);
            this.cbCocinas.Name = "cbCocinas";
            this.cbCocinas.Size = new System.Drawing.Size(219, 21);
            this.cbCocinas.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cocina a Producir:";
            // 
            // txtCantAnual
            // 
            this.txtCantAnual.Location = new System.Drawing.Point(139, 25);
            this.txtCantAnual.Name = "txtCantAnual";
            this.txtCantAnual.Size = new System.Drawing.Size(100, 20);
            this.txtCantAnual.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Cantidad Total a Producir";
            // 
            // gbMateriasPrimas
            // 
            this.gbMateriasPrimas.Controls.Add(this.dgvMP);
            this.gbMateriasPrimas.Location = new System.Drawing.Point(359, 68);
            this.gbMateriasPrimas.Name = "gbMateriasPrimas";
            this.gbMateriasPrimas.Size = new System.Drawing.Size(404, 330);
            this.gbMateriasPrimas.TabIndex = 18;
            this.gbMateriasPrimas.TabStop = false;
            this.gbMateriasPrimas.Text = "Materias Primas";
            // 
            // dgvMP
            // 
            this.dgvMP.AllowUserToAddRows = false;
            this.dgvMP.AllowUserToDeleteRows = false;
            this.dgvMP.AllowUserToResizeRows = false;
            this.dgvMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMP.Location = new System.Drawing.Point(13, 19);
            this.dgvMP.Name = "dgvMP";
            this.dgvMP.ReadOnly = true;
            this.dgvMP.RowHeadersVisible = false;
            this.dgvMP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMP.Size = new System.Drawing.Size(383, 294);
            this.dgvMP.TabIndex = 29;
            // 
            // frmInventarioABC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 404);
            this.Controls.Add(this.gbMateriasPrimas);
            this.Controls.Add(this.gbDatosCocinas);
            this.Controls.Add(this.gbDatosPrincipales);
            this.Name = "frmInventarioABC";
            this.Text = "Inventario ABC";
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.gbDatosCocinas.ResumeLayout(false);
            this.gbDatosCocinas.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgvModelos;
        private System.Windows.Forms.GroupBox gbMateriasPrimas;
        private System.Windows.Forms.DataGridView dgvMP;
        private System.Windows.Forms.Button btnVolver;

    }
}