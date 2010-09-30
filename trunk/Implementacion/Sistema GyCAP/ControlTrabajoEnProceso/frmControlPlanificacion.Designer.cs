namespace GyCAP.UI.ControlTrabajoEnProceso
{
    partial class frmControlPlanificacion
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tcPlanes = new System.Windows.Forms.TabControl();
            this.tpPlanSemanal = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartDemanda = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCargaPlanSemanal = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tpPlanMensual = new System.Windows.Forms.TabPage();
            this.tpPlanAnual = new System.Windows.Forms.TabPage();
            this.cbPlanSemanal = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlanMensual = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.cbPlanAnual = new GyCAP.UI.Sistema.ControlesUsuarios.DropDownList();
            this.tcDetalle = new System.Windows.Forms.TabControl();
            this.tpDias = new System.Windows.Forms.TabPage();
            this.tpDetalleDiario = new System.Windows.Forms.TabPage();
            this.dgvDetallePlan = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.tcPlanes.SuspendLayout();
            this.tpPlanSemanal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDemanda)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbDatosPrincipales.SuspendLayout();
            this.tcDetalle.SuspendLayout();
            this.tpDias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePlan)).BeginInit();
            this.SuspendLayout();
            // 
            // tcPlanes
            // 
            this.tcPlanes.Controls.Add(this.tpPlanSemanal);
            this.tcPlanes.Controls.Add(this.tpPlanMensual);
            this.tcPlanes.Controls.Add(this.tpPlanAnual);
            this.tcPlanes.Location = new System.Drawing.Point(3, 5);
            this.tcPlanes.Name = "tcPlanes";
            this.tcPlanes.SelectedIndex = 0;
            this.tcPlanes.Size = new System.Drawing.Size(738, 409);
            this.tcPlanes.TabIndex = 0;
            // 
            // tpPlanSemanal
            // 
            this.tpPlanSemanal.Controls.Add(this.groupBox2);
            this.tpPlanSemanal.Controls.Add(this.groupBox1);
            this.tpPlanSemanal.Controls.Add(this.gbDatosPrincipales);
            this.tpPlanSemanal.Location = new System.Drawing.Point(4, 22);
            this.tpPlanSemanal.Name = "tpPlanSemanal";
            this.tpPlanSemanal.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlanSemanal.Size = new System.Drawing.Size(730, 383);
            this.tpPlanSemanal.TabIndex = 0;
            this.tpPlanSemanal.Text = "Plan Semanal";
            this.tpPlanSemanal.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chartDemanda);
            this.groupBox2.Location = new System.Drawing.Point(384, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 298);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gráfica de Avance del Plan";
            // 
            // chartDemanda
            // 
            chartArea3.Name = "ChartArea1";
            this.chartDemanda.ChartAreas.Add(chartArea3);
            this.chartDemanda.Location = new System.Drawing.Point(11, 19);
            this.chartDemanda.Name = "chartDemanda";
            series3.ChartArea = "ChartArea1";
            series3.Name = "Series1";
            this.chartDemanda.Series.Add(series3);
            this.chartDemanda.Size = new System.Drawing.Size(316, 252);
            this.chartDemanda.TabIndex = 1;
            this.chartDemanda.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tcDetalle);
            this.groupBox1.Location = new System.Drawing.Point(3, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 298);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle de Plan";
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.Controls.Add(this.cbPlanSemanal);
            this.gbDatosPrincipales.Controls.Add(this.cbPlanMensual);
            this.gbDatosPrincipales.Controls.Add(this.cbPlanAnual);
            this.gbDatosPrincipales.Controls.Add(this.label12);
            this.gbDatosPrincipales.Controls.Add(this.btnCargaPlanSemanal);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Controls.Add(this.label4);
            this.gbDatosPrincipales.Location = new System.Drawing.Point(3, 17);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(721, 57);
            this.gbDatosPrincipales.TabIndex = 14;
            this.gbDatosPrincipales.TabStop = false;
            this.gbDatosPrincipales.Text = "Datos Principales";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(456, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Semana:";
            // 
            // btnCargaPlanSemanal
            // 
            this.btnCargaPlanSemanal.Location = new System.Drawing.Point(626, 20);
            this.btnCargaPlanSemanal.Name = "btnCargaPlanSemanal";
            this.btnCargaPlanSemanal.Size = new System.Drawing.Size(82, 23);
            this.btnCargaPlanSemanal.TabIndex = 4;
            this.btnCargaPlanSemanal.Text = "Ver Datos ";
            this.btnCargaPlanSemanal.UseVisualStyleBackColor = true;
            this.btnCargaPlanSemanal.Click += new System.EventHandler(this.btnCargaDetalle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Plan Anual Planificación:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mes Planificación:";
            // 
            // tpPlanMensual
            // 
            this.tpPlanMensual.Location = new System.Drawing.Point(4, 22);
            this.tpPlanMensual.Name = "tpPlanMensual";
            this.tpPlanMensual.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlanMensual.Size = new System.Drawing.Size(730, 374);
            this.tpPlanMensual.TabIndex = 1;
            this.tpPlanMensual.Text = "Plan Mensual";
            this.tpPlanMensual.UseVisualStyleBackColor = true;
            // 
            // tpPlanAnual
            // 
            this.tpPlanAnual.Location = new System.Drawing.Point(4, 22);
            this.tpPlanAnual.Name = "tpPlanAnual";
            this.tpPlanAnual.Size = new System.Drawing.Size(730, 374);
            this.tpPlanAnual.TabIndex = 2;
            this.tpPlanAnual.Text = "Plan Anual";
            this.tpPlanAnual.UseVisualStyleBackColor = true;
            // 
            // cbPlanSemanal
            // 
            this.cbPlanSemanal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanSemanal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanSemanal.FormattingEnabled = true;
            this.cbPlanSemanal.Location = new System.Drawing.Point(511, 22);
            this.cbPlanSemanal.Name = "cbPlanSemanal";
            this.cbPlanSemanal.Size = new System.Drawing.Size(95, 21);
            this.cbPlanSemanal.TabIndex = 30;
            // 
            // cbPlanMensual
            // 
            this.cbPlanMensual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanMensual.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanMensual.FormattingEnabled = true;
            this.cbPlanMensual.Location = new System.Drawing.Point(355, 22);
            this.cbPlanMensual.Name = "cbPlanMensual";
            this.cbPlanMensual.Size = new System.Drawing.Size(95, 21);
            this.cbPlanMensual.TabIndex = 29;
            this.cbPlanMensual.DropDownClosed += new System.EventHandler(this.cbPlanMensual_DropDownClosed);
            // 
            // cbPlanAnual
            // 
            this.cbPlanAnual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanAnual.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cbPlanAnual.FormattingEnabled = true;
            this.cbPlanAnual.Location = new System.Drawing.Point(144, 21);
            this.cbPlanAnual.Name = "cbPlanAnual";
            this.cbPlanAnual.Size = new System.Drawing.Size(95, 21);
            this.cbPlanAnual.TabIndex = 28;
            this.cbPlanAnual.DropDownClosed += new System.EventHandler(this.cbPlanAnual_DropDownClosed);
            // 
            // tcDetalle
            // 
            this.tcDetalle.Controls.Add(this.tpDias);
            this.tcDetalle.Controls.Add(this.tpDetalleDiario);
            this.tcDetalle.Location = new System.Drawing.Point(9, 19);
            this.tcDetalle.Name = "tcDetalle";
            this.tcDetalle.SelectedIndex = 0;
            this.tcDetalle.Size = new System.Drawing.Size(360, 273);
            this.tcDetalle.TabIndex = 6;
            // 
            // tpDias
            // 
            this.tpDias.Controls.Add(this.button1);
            this.tpDias.Controls.Add(this.dgvDetallePlan);
            this.tpDias.Location = new System.Drawing.Point(4, 22);
            this.tpDias.Name = "tpDias";
            this.tpDias.Padding = new System.Windows.Forms.Padding(3);
            this.tpDias.Size = new System.Drawing.Size(352, 247);
            this.tpDias.TabIndex = 0;
            this.tpDias.Text = "Dias Plan";
            this.tpDias.UseVisualStyleBackColor = true;
            // 
            // tpDetalleDiario
            // 
            this.tpDetalleDiario.Location = new System.Drawing.Point(4, 22);
            this.tpDetalleDiario.Name = "tpDetalleDiario";
            this.tpDetalleDiario.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetalleDiario.Size = new System.Drawing.Size(352, 247);
            this.tpDetalleDiario.TabIndex = 1;
            this.tpDetalleDiario.Text = "Detalle Diario";
            this.tpDetalleDiario.UseVisualStyleBackColor = true;
            // 
            // dgvDetallePlan
            // 
            this.dgvDetallePlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePlan.Location = new System.Drawing.Point(4, 6);
            this.dgvDetallePlan.Name = "dgvDetallePlan";
            this.dgvDetallePlan.Size = new System.Drawing.Size(342, 207);
            this.dgvDetallePlan.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ver Detalle";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmControlPlanificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 417);
            this.Controls.Add(this.tcPlanes);
            this.Name = "frmControlPlanificacion";
            this.Text = "Control Planificacion";
            this.tcPlanes.ResumeLayout(false);
            this.tpPlanSemanal.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDemanda)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.tcDetalle.ResumeLayout(false);
            this.tpDias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePlan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcPlanes;
        private System.Windows.Forms.TabPage tpPlanSemanal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDemanda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanSemanal;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanMensual;
        private GyCAP.UI.Sistema.ControlesUsuarios.DropDownList cbPlanAnual;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCargaPlanSemanal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpPlanMensual;
        private System.Windows.Forms.TabPage tpPlanAnual;
        private System.Windows.Forms.TabControl tcDetalle;
        private System.Windows.Forms.TabPage tpDias;
        private System.Windows.Forms.DataGridView dgvDetallePlan;
        private System.Windows.Forms.TabPage tpDetalleDiario;
        private System.Windows.Forms.Button button1;


    }
}