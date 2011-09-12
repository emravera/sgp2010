namespace GyCAP.UI.Costos
{
    partial class frmCostoProducto
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
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tcEstructuraStock = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tvcCostosCentros = new TreeViewColumnsProject.TreeViewColumns();
            this.cboCocina = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.tvcCostosMateriales = new TreeViewColumnsProject.TreeViewColumns();
            this.txtCostoTotal = new System.Windows.Forms.Label();
            this.tcEstructuraStock.SuspendLayout();
            this.tpLista.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.Costos.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // tcEstructuraStock
            // 
            this.tcEstructuraStock.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tcEstructuraStock.Controls.Add(this.tpLista);
            this.tcEstructuraStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEstructuraStock.ItemSize = new System.Drawing.Size(0, 1);
            this.tcEstructuraStock.Location = new System.Drawing.Point(2, 54);
            this.tcEstructuraStock.Margin = new System.Windows.Forms.Padding(0);
            this.tcEstructuraStock.Multiline = true;
            this.tcEstructuraStock.Name = "tcEstructuraStock";
            this.tcEstructuraStock.Padding = new System.Drawing.Point(0, 0);
            this.tcEstructuraStock.SelectedIndex = 0;
            this.tcEstructuraStock.Size = new System.Drawing.Size(790, 516);
            this.tcEstructuraStock.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcEstructuraStock.TabIndex = 8;
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.groupBox4);
            this.tpLista.Controls.Add(this.groupBox3);
            this.tpLista.Controls.Add(this.groupBox2);
            this.tpLista.Controls.Add(this.groupBox1);
            this.tpLista.Location = new System.Drawing.Point(4, 5);
            this.tpLista.Name = "tpLista";
            this.tpLista.Padding = new System.Windows.Forms.Padding(3);
            this.tpLista.Size = new System.Drawing.Size(782, 507);
            this.tpLista.TabIndex = 0;
            this.tpLista.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCostoTotal);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(494, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(283, 54);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Costo total $:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tvcCostosCentros);
            this.groupBox3.Location = new System.Drawing.Point(6, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(771, 210);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Costos de Proceso";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.cboCocina);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(485, 54);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccione la cocina";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::GyCAP.UI.Costos.Properties.Resources.System_25;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(379, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 30);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "&Calcular";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cocina:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvcCostosMateriales);
            this.groupBox1.Location = new System.Drawing.Point(3, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Costos de Materiales";
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
            this.tableLayoutPanel1.Controls.Add(this.tcEstructuraStock, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 572);
            this.tableLayoutPanel1.TabIndex = 12;
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
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(2, 2);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0);
            this.tsMenu.Size = new System.Drawing.Size(790, 50);
            this.tsMenu.TabIndex = 7;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tvcCostosCentros
            // 
            this.tvcCostosCentros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(166)))), ((int)(((byte)(170)))));
            this.tvcCostosCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvcCostosCentros.Location = new System.Drawing.Point(3, 17);
            this.tvcCostosCentros.Name = "tvcCostosCentros";
            this.tvcCostosCentros.Padding = new System.Windows.Forms.Padding(1);
            this.tvcCostosCentros.Size = new System.Drawing.Size(765, 190);
            this.tvcCostosCentros.TabIndex = 0;
            // 
            // cboCocina
            // 
            this.cboCocina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCocina.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboCocina.FormattingEnabled = true;
            this.cboCocina.Location = new System.Drawing.Point(55, 20);
            this.cboCocina.Name = "cboCocina";
            this.cboCocina.Size = new System.Drawing.Size(318, 21);
            this.cboCocina.TabIndex = 1;
            // 
            // tvcCostosMateriales
            // 
            this.tvcCostosMateriales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(166)))), ((int)(((byte)(170)))));
            this.tvcCostosMateriales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvcCostosMateriales.Location = new System.Drawing.Point(3, 17);
            this.tvcCostosMateriales.Name = "tvcCostosMateriales";
            this.tvcCostosMateriales.Padding = new System.Windows.Forms.Padding(1);
            this.tvcCostosMateriales.Size = new System.Drawing.Size(768, 197);
            this.tvcCostosMateriales.TabIndex = 0;
            // 
            // txtCostoTotal
            // 
            this.txtCostoTotal.AutoSize = true;
            this.txtCostoTotal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostoTotal.Location = new System.Drawing.Point(100, 20);
            this.txtCostoTotal.Name = "txtCostoTotal";
            this.txtCostoTotal.Size = new System.Drawing.Size(0, 19);
            this.txtCostoTotal.TabIndex = 1;
            // 
            // frmCostoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCostoProducto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Costo del Producto";
            this.tcEstructuraStock.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TabControl tcEstructuraStock;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.GroupBox groupBox2;
        private TreeViewColumnsProject.TreeViewColumns tvcCostosMateriales;
        private System.Windows.Forms.Label label1;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cboCocina;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox3;
        private TreeViewColumnsProject.TreeViewColumns tvcCostosCentros;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtCostoTotal;
    }
}