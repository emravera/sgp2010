﻿namespace GyCAP.UI.GestionStock
{
    partial class frmEstructuraStock
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
            this.tcEstructuraStock = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvcEstructura = new TreeViewColumnsProject.TreeViewColumns();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tcEstructuraStock.SuspendLayout();
            this.tpLista.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.tcEstructuraStock.Size = new System.Drawing.Size(788, 514);
            this.tcEstructuraStock.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcEstructuraStock.TabIndex = 8;
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.groupBox1);
            this.tpLista.Location = new System.Drawing.Point(4, 5);
            this.tpLista.Name = "tpLista";
            this.tpLista.Padding = new System.Windows.Forms.Padding(3);
            this.tpLista.Size = new System.Drawing.Size(780, 505);
            this.tpLista.TabIndex = 0;
            this.tpLista.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvcEstructura);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 499);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estructura de Stock";
            // 
            // tvcEstructura
            // 
            this.tvcEstructura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(166)))), ((int)(((byte)(170)))));
            this.tvcEstructura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvcEstructura.Location = new System.Drawing.Point(3, 17);
            this.tvcEstructura.Name = "tvcEstructura";
            this.tvcEstructura.Padding = new System.Windows.Forms.Padding(1);
            this.tvcEstructura.Size = new System.Drawing.Size(768, 479);
            this.tvcEstructura.TabIndex = 0;
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
            this.btnActualizar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tsMenu.Location = new System.Drawing.Point(2, 2);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Padding = new System.Windows.Forms.Padding(0);
            this.tsMenu.Size = new System.Drawing.Size(788, 50);
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
            this.tableLayoutPanel1.Controls.Add(this.tcEstructuraStock, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsMenu, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 570);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Refresh_25;
            this.btnActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(58, 47);
            this.btnActualizar.Text = "&Actualizar";
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::GyCAP.UI.GestionStock.Properties.Resources.Salir_25;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(31, 47);
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmEstructuraStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmEstructuraStock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Estructura de Stock";
            this.tcEstructuraStock.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcEstructuraStock;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TreeViewColumnsProject.TreeViewColumns tvcEstructura;
    }
}