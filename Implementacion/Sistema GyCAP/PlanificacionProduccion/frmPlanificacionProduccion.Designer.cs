namespace GyCAP.UI.PlanificacionProduccion
{
    partial class frmPlanificacionProduccion
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
            this.btnDemanda = new System.Windows.Forms.Button();
            this.btnConsultarPlanAnual = new System.Windows.Forms.Button();
            this.btnNuevoPlanAnual = new System.Windows.Forms.Button();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDemanda = new System.Windows.Forms.Panel();
            this.btnConsultarDemanda = new System.Windows.Forms.Button();
            this.btnNuevoDemanda = new System.Windows.Forms.Button();
            this.btnPlanAnual = new System.Windows.Forms.Button();
            this.panelPlanAnual = new System.Windows.Forms.Panel();
            this.btnMPPrincipal = new System.Windows.Forms.Button();
            this.panelMPPrincipal = new System.Windows.Forms.Panel();
            this.btnNuevoMPPrincipal = new System.Windows.Forms.Button();
            this.panelSalir = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.btnConsultarMPPrincipal = new System.Windows.Forms.Button();
            this.flpMenu.SuspendLayout();
            this.panelDemanda.SuspendLayout();
            this.panelPlanAnual.SuspendLayout();
            this.panelMPPrincipal.SuspendLayout();
            this.panelSalir.SuspendLayout();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDemanda
            // 
            this.btnDemanda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDemanda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemanda.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemanda.Location = new System.Drawing.Point(0, 0);
            this.btnDemanda.Margin = new System.Windows.Forms.Padding(0);
            this.btnDemanda.Name = "btnDemanda";
            this.btnDemanda.Size = new System.Drawing.Size(158, 25);
            this.btnDemanda.TabIndex = 0;
            this.btnDemanda.Text = "Demanda";
            this.btnDemanda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDemanda.UseVisualStyleBackColor = true;
            this.btnDemanda.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnConsultarPlanAnual
            // 
            this.btnConsultarPlanAnual.AutoSize = true;
            this.btnConsultarPlanAnual.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarPlanAnual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarPlanAnual.FlatAppearance.BorderSize = 0;
            this.btnConsultarPlanAnual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnConsultarPlanAnual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarPlanAnual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarPlanAnual.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Find_48;
            this.btnConsultarPlanAnual.Location = new System.Drawing.Point(48, 74);
            this.btnConsultarPlanAnual.Name = "btnConsultarPlanAnual";
            this.btnConsultarPlanAnual.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarPlanAnual.TabIndex = 1;
            this.btnConsultarPlanAnual.Text = "Consultar";
            this.btnConsultarPlanAnual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarPlanAnual.UseVisualStyleBackColor = true;
            this.btnConsultarPlanAnual.Click += new System.EventHandler(this.btnConsultarPlanAnual_Click);
            // 
            // btnNuevoPlanAnual
            // 
            this.btnNuevoPlanAnual.AutoSize = true;
            this.btnNuevoPlanAnual.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoPlanAnual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoPlanAnual.FlatAppearance.BorderSize = 0;
            this.btnNuevoPlanAnual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNuevoPlanAnual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoPlanAnual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoPlanAnual.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.New_48;
            this.btnNuevoPlanAnual.Location = new System.Drawing.Point(52, 3);
            this.btnNuevoPlanAnual.Name = "btnNuevoPlanAnual";
            this.btnNuevoPlanAnual.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoPlanAnual.TabIndex = 0;
            this.btnNuevoPlanAnual.Text = " Nuevo";
            this.btnNuevoPlanAnual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoPlanAnual.UseVisualStyleBackColor = true;
            this.btnNuevoPlanAnual.Click += new System.EventHandler(this.btnNuevoPlanAnual_Click);
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnDemanda);
            this.flpMenu.Controls.Add(this.panelDemanda);
            this.flpMenu.Controls.Add(this.btnPlanAnual);
            this.flpMenu.Controls.Add(this.panelPlanAnual);
            this.flpMenu.Controls.Add(this.btnMPPrincipal);
            this.flpMenu.Controls.Add(this.panelMPPrincipal);
            this.flpMenu.Controls.Add(this.panelSalir);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 566);
            this.flpMenu.TabIndex = 0;
            // 
            // panelDemanda
            // 
            this.panelDemanda.AutoSize = true;
            this.panelDemanda.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDemanda.BackColor = System.Drawing.Color.Silver;
            this.panelDemanda.Controls.Add(this.btnConsultarDemanda);
            this.panelDemanda.Controls.Add(this.btnNuevoDemanda);
            this.panelDemanda.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelDemanda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDemanda.Location = new System.Drawing.Point(0, 25);
            this.panelDemanda.Margin = new System.Windows.Forms.Padding(0);
            this.panelDemanda.Name = "panelDemanda";
            this.panelDemanda.Size = new System.Drawing.Size(158, 148);
            this.panelDemanda.TabIndex = 1;
            this.panelDemanda.Visible = false;
            // 
            // btnConsultarDemanda
            // 
            this.btnConsultarDemanda.AutoSize = true;
            this.btnConsultarDemanda.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarDemanda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarDemanda.FlatAppearance.BorderSize = 0;
            this.btnConsultarDemanda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnConsultarDemanda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarDemanda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarDemanda.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Find_48;
            this.btnConsultarDemanda.Location = new System.Drawing.Point(48, 74);
            this.btnConsultarDemanda.Name = "btnConsultarDemanda";
            this.btnConsultarDemanda.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarDemanda.TabIndex = 1;
            this.btnConsultarDemanda.Text = "Consultar";
            this.btnConsultarDemanda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarDemanda.UseVisualStyleBackColor = true;
            this.btnConsultarDemanda.Click += new System.EventHandler(this.btnConsultarDemanda_Click);
            // 
            // btnNuevoDemanda
            // 
            this.btnNuevoDemanda.AutoSize = true;
            this.btnNuevoDemanda.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoDemanda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoDemanda.FlatAppearance.BorderSize = 0;
            this.btnNuevoDemanda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNuevoDemanda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoDemanda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoDemanda.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.New_48;
            this.btnNuevoDemanda.Location = new System.Drawing.Point(52, 3);
            this.btnNuevoDemanda.Name = "btnNuevoDemanda";
            this.btnNuevoDemanda.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoDemanda.TabIndex = 0;
            this.btnNuevoDemanda.Text = " Nuevo";
            this.btnNuevoDemanda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoDemanda.UseVisualStyleBackColor = true;
            this.btnNuevoDemanda.Click += new System.EventHandler(this.btnNuevoDemanda_Click);
            // 
            // btnPlanAnual
            // 
            this.btnPlanAnual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlanAnual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanAnual.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanAnual.Location = new System.Drawing.Point(0, 173);
            this.btnPlanAnual.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlanAnual.Name = "btnPlanAnual";
            this.btnPlanAnual.Size = new System.Drawing.Size(158, 25);
            this.btnPlanAnual.TabIndex = 3;
            this.btnPlanAnual.Text = "Plan Anual";
            this.btnPlanAnual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanAnual.UseVisualStyleBackColor = true;
            this.btnPlanAnual.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelPlanAnual
            // 
            this.panelPlanAnual.AutoSize = true;
            this.panelPlanAnual.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelPlanAnual.BackColor = System.Drawing.Color.Silver;
            this.panelPlanAnual.Controls.Add(this.btnConsultarPlanAnual);
            this.panelPlanAnual.Controls.Add(this.btnNuevoPlanAnual);
            this.panelPlanAnual.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelPlanAnual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlanAnual.Location = new System.Drawing.Point(0, 198);
            this.panelPlanAnual.Margin = new System.Windows.Forms.Padding(0);
            this.panelPlanAnual.Name = "panelPlanAnual";
            this.panelPlanAnual.Size = new System.Drawing.Size(158, 148);
            this.panelPlanAnual.TabIndex = 4;
            this.panelPlanAnual.Visible = false;
            // 
            // btnMPPrincipal
            // 
            this.btnMPPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMPPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMPPrincipal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMPPrincipal.Location = new System.Drawing.Point(0, 346);
            this.btnMPPrincipal.Margin = new System.Windows.Forms.Padding(0);
            this.btnMPPrincipal.Name = "btnMPPrincipal";
            this.btnMPPrincipal.Size = new System.Drawing.Size(158, 25);
            this.btnMPPrincipal.TabIndex = 9;
            this.btnMPPrincipal.Text = "Materia Prima Principal";
            this.btnMPPrincipal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMPPrincipal.UseVisualStyleBackColor = true;
            this.btnMPPrincipal.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelMPPrincipal
            // 
            this.panelMPPrincipal.AutoSize = true;
            this.panelMPPrincipal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMPPrincipal.BackColor = System.Drawing.Color.Silver;
            this.panelMPPrincipal.Controls.Add(this.btnConsultarMPPrincipal);
            this.panelMPPrincipal.Controls.Add(this.btnNuevoMPPrincipal);
            this.panelMPPrincipal.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelMPPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMPPrincipal.Location = new System.Drawing.Point(0, 371);
            this.panelMPPrincipal.Margin = new System.Windows.Forms.Padding(0);
            this.panelMPPrincipal.Name = "panelMPPrincipal";
            this.panelMPPrincipal.Size = new System.Drawing.Size(158, 152);
            this.panelMPPrincipal.TabIndex = 10;
            this.panelMPPrincipal.Visible = false;
            // 
            // btnNuevoMPPrincipal
            // 
            this.btnNuevoMPPrincipal.AutoSize = true;
            this.btnNuevoMPPrincipal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNuevoMPPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoMPPrincipal.FlatAppearance.BorderSize = 0;
            this.btnNuevoMPPrincipal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNuevoMPPrincipal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevoMPPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoMPPrincipal.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.New_48;
            this.btnNuevoMPPrincipal.Location = new System.Drawing.Point(52, 5);
            this.btnNuevoMPPrincipal.Name = "btnNuevoMPPrincipal";
            this.btnNuevoMPPrincipal.Size = new System.Drawing.Size(54, 71);
            this.btnNuevoMPPrincipal.TabIndex = 0;
            this.btnNuevoMPPrincipal.Text = "Nuevo";
            this.btnNuevoMPPrincipal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoMPPrincipal.UseVisualStyleBackColor = true;
            this.btnNuevoMPPrincipal.Click += new System.EventHandler(this.btnNuevoMPPrincipal_Click);
            // 
            // panelSalir
            // 
            this.panelSalir.Controls.Add(this.button1);
            this.panelSalir.Location = new System.Drawing.Point(158, 0);
            this.panelSalir.Margin = new System.Windows.Forms.Padding(0);
            this.panelSalir.Name = "panelSalir";
            this.panelSalir.Size = new System.Drawing.Size(158, 87);
            this.panelSalir.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Exit_48;
            this.button1.Location = new System.Drawing.Point(48, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 71);
            this.button1.TabIndex = 2;
            this.button1.Text = " Salir";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMenu.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(13, 566);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // scUp
            // 
            this.scUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scUp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scUp.IsSplitterFixed = true;
            this.scUp.Location = new System.Drawing.Point(0, 0);
            this.scUp.Name = "scUp";
            // 
            // scUp.Panel1
            // 
            this.scUp.Panel1.AutoScroll = true;
            this.scUp.Panel1.Controls.Add(this.btnMenu);
            this.scUp.Panel1MinSize = 20;
            // 
            // scUp.Panel2
            // 
            this.scUp.Panel2.AutoScroll = true;
            this.scUp.Panel2.AutoScrollMargin = new System.Drawing.Size(1000, 1000);
            this.scUp.Size = new System.Drawing.Size(624, 566);
            this.scUp.SplitterDistance = 20;
            this.scUp.SplitterWidth = 3;
            this.scUp.TabIndex = 0;
            // 
            // scDown
            // 
            this.scDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDown.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scDown.IsSplitterFixed = true;
            this.scDown.Location = new System.Drawing.Point(0, 0);
            this.scDown.Name = "scDown";
            // 
            // scDown.Panel1
            // 
            this.scDown.Panel1.AutoScroll = true;
            this.scDown.Panel1.Controls.Add(this.flpMenu);
            // 
            // scDown.Panel2
            // 
            this.scDown.Panel2.AutoScroll = true;
            this.scDown.Panel2.Controls.Add(this.scUp);
            this.scDown.Size = new System.Drawing.Size(792, 570);
            this.scDown.SplitterDistance = 161;
            this.scDown.SplitterWidth = 3;
            this.scDown.TabIndex = 4;
            // 
            // btnConsultarMPPrincipal
            // 
            this.btnConsultarMPPrincipal.AutoSize = true;
            this.btnConsultarMPPrincipal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConsultarMPPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarMPPrincipal.FlatAppearance.BorderSize = 0;
            this.btnConsultarMPPrincipal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnConsultarMPPrincipal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnConsultarMPPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarMPPrincipal.Image = global::GyCAP.UI.PlanificacionProduccion.Properties.Resources.Find_48;
            this.btnConsultarMPPrincipal.Location = new System.Drawing.Point(48, 78);
            this.btnConsultarMPPrincipal.Name = "btnConsultarMPPrincipal";
            this.btnConsultarMPPrincipal.Size = new System.Drawing.Size(63, 71);
            this.btnConsultarMPPrincipal.TabIndex = 2;
            this.btnConsultarMPPrincipal.Text = "Consultar";
            this.btnConsultarMPPrincipal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConsultarMPPrincipal.UseVisualStyleBackColor = true;
            this.btnConsultarMPPrincipal.Click += new System.EventHandler(this.btnConsultarMPPrincipal_Click);
            // 
            // frmPlanificacionProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPlanificacionProduccion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Planificación de la Producción";
            this.Load += new System.EventHandler(this.frmPlanificacionProduccion_Load);
            this.flpMenu.ResumeLayout(false);
            this.flpMenu.PerformLayout();
            this.panelDemanda.ResumeLayout(false);
            this.panelDemanda.PerformLayout();
            this.panelPlanAnual.ResumeLayout(false);
            this.panelPlanAnual.PerformLayout();
            this.panelMPPrincipal.ResumeLayout(false);
            this.panelMPPrincipal.PerformLayout();
            this.panelSalir.ResumeLayout(false);
            this.panelSalir.PerformLayout();
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDemanda;
        private System.Windows.Forms.Button btnConsultarPlanAnual;
        private System.Windows.Forms.Button btnNuevoPlanAnual;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        private System.Windows.Forms.Panel panelDemanda;
        private System.Windows.Forms.Button btnConsultarDemanda;
        private System.Windows.Forms.Button btnNuevoDemanda;
        private System.Windows.Forms.Button btnPlanAnual;
        private System.Windows.Forms.Panel panelPlanAnual;
        private System.Windows.Forms.Button btnMPPrincipal;
        private System.Windows.Forms.Panel panelMPPrincipal;
        private System.Windows.Forms.Button btnNuevoMPPrincipal;
        private System.Windows.Forms.Panel panelSalir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.Button btnConsultarMPPrincipal;
    }
}