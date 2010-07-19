namespace GyCAP.UI.EstructuraProducto
{
    partial class frmEstructuraProducto
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
            this.scDown = new System.Windows.Forms.SplitContainer();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCocina = new System.Windows.Forms.Button();
            this.panelCocina = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.panelColor = new System.Windows.Forms.Panel();
            this.btnConjunto = new System.Windows.Forms.Button();
            this.panelConjunto = new System.Windows.Forms.Panel();
            this.btnDesignacion = new System.Windows.Forms.Button();
            this.panelDesignacion = new System.Windows.Forms.Panel();
            this.btnEstructuraProducto = new System.Windows.Forms.Button();
            this.panelEstructuraProducto = new System.Windows.Forms.Panel();
            this.btnMPPrincipal = new System.Windows.Forms.Button();
            this.panelMPPrincipal = new System.Windows.Forms.Panel();
            this.btnModeloCocina = new System.Windows.Forms.Button();
            this.panelModeloCocina = new System.Windows.Forms.Panel();
            this.btnPieza = new System.Windows.Forms.Button();
            this.panelPieza = new System.Windows.Forms.Panel();
            this.btnSubconjunto = new System.Windows.Forms.Button();
            this.panelSubconjunto = new System.Windows.Forms.Panel();
            this.btnTerminacion = new System.Windows.Forms.Button();
            this.panelTerminacion = new System.Windows.Forms.Panel();
            this.btnUnidadMedida = new System.Windows.Forms.Button();
            this.panelUnidadMedida = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.scUp = new System.Windows.Forms.SplitContainer();
            this.btnMenu = new System.Windows.Forms.Button();
            this.scDown.Panel1.SuspendLayout();
            this.scDown.Panel2.SuspendLayout();
            this.scDown.SuspendLayout();
            this.flpMenu.SuspendLayout();
            this.panel12.SuspendLayout();
            this.scUp.Panel1.SuspendLayout();
            this.scUp.SuspendLayout();
            this.SuspendLayout();
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
            this.scDown.Size = new System.Drawing.Size(794, 572);
            this.scDown.SplitterDistance = 161;
            this.scDown.TabIndex = 1;
            // 
            // flpMenu
            // 
            this.flpMenu.AutoScroll = true;
            this.flpMenu.Controls.Add(this.btnCocina);
            this.flpMenu.Controls.Add(this.panelCocina);
            this.flpMenu.Controls.Add(this.btnColor);
            this.flpMenu.Controls.Add(this.panelColor);
            this.flpMenu.Controls.Add(this.btnConjunto);
            this.flpMenu.Controls.Add(this.panelConjunto);
            this.flpMenu.Controls.Add(this.btnDesignacion);
            this.flpMenu.Controls.Add(this.panelDesignacion);
            this.flpMenu.Controls.Add(this.btnEstructuraProducto);
            this.flpMenu.Controls.Add(this.panelEstructuraProducto);
            this.flpMenu.Controls.Add(this.btnMPPrincipal);
            this.flpMenu.Controls.Add(this.panelMPPrincipal);
            this.flpMenu.Controls.Add(this.btnModeloCocina);
            this.flpMenu.Controls.Add(this.panelModeloCocina);
            this.flpMenu.Controls.Add(this.btnPieza);
            this.flpMenu.Controls.Add(this.panelPieza);
            this.flpMenu.Controls.Add(this.btnSubconjunto);
            this.flpMenu.Controls.Add(this.panelSubconjunto);
            this.flpMenu.Controls.Add(this.btnTerminacion);
            this.flpMenu.Controls.Add(this.panelTerminacion);
            this.flpMenu.Controls.Add(this.btnUnidadMedida);
            this.flpMenu.Controls.Add(this.panelUnidadMedida);
            this.flpMenu.Controls.Add(this.panel12);
            this.flpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(157, 568);
            this.flpMenu.TabIndex = 0;
            // 
            // btnCocina
            // 
            this.btnCocina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCocina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCocina.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCocina.Location = new System.Drawing.Point(0, 0);
            this.btnCocina.Margin = new System.Windows.Forms.Padding(0);
            this.btnCocina.Name = "btnCocina";
            this.btnCocina.Size = new System.Drawing.Size(158, 25);
            this.btnCocina.TabIndex = 0;
            this.btnCocina.Text = "Cocina";
            this.btnCocina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCocina.UseVisualStyleBackColor = true;
            this.btnCocina.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelCocina
            // 
            this.panelCocina.BackColor = System.Drawing.Color.Silver;
            this.panelCocina.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelCocina.Location = new System.Drawing.Point(0, 25);
            this.panelCocina.Margin = new System.Windows.Forms.Padding(0);
            this.panelCocina.Name = "panelCocina";
            this.panelCocina.Size = new System.Drawing.Size(158, 100);
            this.panelCocina.TabIndex = 1;
            this.panelCocina.Visible = false;
            // 
            // btnColor
            // 
            this.btnColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColor.Location = new System.Drawing.Point(0, 125);
            this.btnColor.Margin = new System.Windows.Forms.Padding(0);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(158, 25);
            this.btnColor.TabIndex = 0;
            this.btnColor.Text = "Color";
            this.btnColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelColor
            // 
            this.panelColor.BackColor = System.Drawing.Color.Silver;
            this.panelColor.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelColor.Location = new System.Drawing.Point(0, 150);
            this.panelColor.Margin = new System.Windows.Forms.Padding(0);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(158, 100);
            this.panelColor.TabIndex = 2;
            this.panelColor.Visible = false;
            // 
            // btnConjunto
            // 
            this.btnConjunto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConjunto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConjunto.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConjunto.Location = new System.Drawing.Point(0, 250);
            this.btnConjunto.Margin = new System.Windows.Forms.Padding(0);
            this.btnConjunto.Name = "btnConjunto";
            this.btnConjunto.Size = new System.Drawing.Size(158, 25);
            this.btnConjunto.TabIndex = 3;
            this.btnConjunto.Text = "Conjunto";
            this.btnConjunto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConjunto.UseVisualStyleBackColor = true;
            this.btnConjunto.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelConjunto
            // 
            this.panelConjunto.BackColor = System.Drawing.Color.Silver;
            this.panelConjunto.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelConjunto.Location = new System.Drawing.Point(0, 275);
            this.panelConjunto.Margin = new System.Windows.Forms.Padding(0);
            this.panelConjunto.Name = "panelConjunto";
            this.panelConjunto.Size = new System.Drawing.Size(158, 100);
            this.panelConjunto.TabIndex = 4;
            this.panelConjunto.Visible = false;
            // 
            // btnDesignacion
            // 
            this.btnDesignacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesignacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesignacion.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesignacion.Location = new System.Drawing.Point(0, 375);
            this.btnDesignacion.Margin = new System.Windows.Forms.Padding(0);
            this.btnDesignacion.Name = "btnDesignacion";
            this.btnDesignacion.Size = new System.Drawing.Size(158, 25);
            this.btnDesignacion.TabIndex = 5;
            this.btnDesignacion.Text = "Designación";
            this.btnDesignacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesignacion.UseVisualStyleBackColor = true;
            this.btnDesignacion.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelDesignacion
            // 
            this.panelDesignacion.BackColor = System.Drawing.Color.Silver;
            this.panelDesignacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelDesignacion.Location = new System.Drawing.Point(0, 400);
            this.panelDesignacion.Margin = new System.Windows.Forms.Padding(0);
            this.panelDesignacion.Name = "panelDesignacion";
            this.panelDesignacion.Size = new System.Drawing.Size(158, 100);
            this.panelDesignacion.TabIndex = 6;
            this.panelDesignacion.Visible = false;
            // 
            // btnEstructuraProducto
            // 
            this.btnEstructuraProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstructuraProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstructuraProducto.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstructuraProducto.Location = new System.Drawing.Point(0, 500);
            this.btnEstructuraProducto.Margin = new System.Windows.Forms.Padding(0);
            this.btnEstructuraProducto.Name = "btnEstructuraProducto";
            this.btnEstructuraProducto.Size = new System.Drawing.Size(158, 25);
            this.btnEstructuraProducto.TabIndex = 7;
            this.btnEstructuraProducto.Text = "Estructura de Producto";
            this.btnEstructuraProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstructuraProducto.UseVisualStyleBackColor = true;
            this.btnEstructuraProducto.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelEstructuraProducto
            // 
            this.panelEstructuraProducto.BackColor = System.Drawing.Color.Silver;
            this.panelEstructuraProducto.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelEstructuraProducto.Location = new System.Drawing.Point(158, 0);
            this.panelEstructuraProducto.Margin = new System.Windows.Forms.Padding(0);
            this.panelEstructuraProducto.Name = "panelEstructuraProducto";
            this.panelEstructuraProducto.Size = new System.Drawing.Size(158, 100);
            this.panelEstructuraProducto.TabIndex = 8;
            this.panelEstructuraProducto.Visible = false;
            // 
            // btnMPPrincipal
            // 
            this.btnMPPrincipal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMPPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMPPrincipal.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMPPrincipal.Location = new System.Drawing.Point(158, 100);
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
            this.panelMPPrincipal.BackColor = System.Drawing.Color.Silver;
            this.panelMPPrincipal.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelMPPrincipal.Location = new System.Drawing.Point(158, 125);
            this.panelMPPrincipal.Margin = new System.Windows.Forms.Padding(0);
            this.panelMPPrincipal.Name = "panelMPPrincipal";
            this.panelMPPrincipal.Size = new System.Drawing.Size(158, 100);
            this.panelMPPrincipal.TabIndex = 10;
            this.panelMPPrincipal.Visible = false;
            // 
            // btnModeloCocina
            // 
            this.btnModeloCocina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModeloCocina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModeloCocina.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModeloCocina.Location = new System.Drawing.Point(158, 225);
            this.btnModeloCocina.Margin = new System.Windows.Forms.Padding(0);
            this.btnModeloCocina.Name = "btnModeloCocina";
            this.btnModeloCocina.Size = new System.Drawing.Size(158, 25);
            this.btnModeloCocina.TabIndex = 11;
            this.btnModeloCocina.Text = "Modelo de Cocina";
            this.btnModeloCocina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModeloCocina.UseVisualStyleBackColor = true;
            this.btnModeloCocina.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelModeloCocina
            // 
            this.panelModeloCocina.BackColor = System.Drawing.Color.Silver;
            this.panelModeloCocina.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelModeloCocina.Location = new System.Drawing.Point(158, 250);
            this.panelModeloCocina.Margin = new System.Windows.Forms.Padding(0);
            this.panelModeloCocina.Name = "panelModeloCocina";
            this.panelModeloCocina.Size = new System.Drawing.Size(158, 100);
            this.panelModeloCocina.TabIndex = 12;
            this.panelModeloCocina.Visible = false;
            // 
            // btnPieza
            // 
            this.btnPieza.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPieza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPieza.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPieza.Location = new System.Drawing.Point(158, 350);
            this.btnPieza.Margin = new System.Windows.Forms.Padding(0);
            this.btnPieza.Name = "btnPieza";
            this.btnPieza.Size = new System.Drawing.Size(158, 25);
            this.btnPieza.TabIndex = 13;
            this.btnPieza.Text = "Pieza";
            this.btnPieza.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPieza.UseVisualStyleBackColor = true;
            this.btnPieza.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelPieza
            // 
            this.panelPieza.BackColor = System.Drawing.Color.Silver;
            this.panelPieza.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelPieza.Location = new System.Drawing.Point(158, 375);
            this.panelPieza.Margin = new System.Windows.Forms.Padding(0);
            this.panelPieza.Name = "panelPieza";
            this.panelPieza.Size = new System.Drawing.Size(158, 100);
            this.panelPieza.TabIndex = 14;
            this.panelPieza.Visible = false;
            // 
            // btnSubconjunto
            // 
            this.btnSubconjunto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubconjunto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubconjunto.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubconjunto.Location = new System.Drawing.Point(158, 475);
            this.btnSubconjunto.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubconjunto.Name = "btnSubconjunto";
            this.btnSubconjunto.Size = new System.Drawing.Size(158, 25);
            this.btnSubconjunto.TabIndex = 15;
            this.btnSubconjunto.Text = "Subconjunto";
            this.btnSubconjunto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubconjunto.UseVisualStyleBackColor = true;
            this.btnSubconjunto.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelSubconjunto
            // 
            this.panelSubconjunto.BackColor = System.Drawing.Color.Silver;
            this.panelSubconjunto.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelSubconjunto.Location = new System.Drawing.Point(316, 0);
            this.panelSubconjunto.Margin = new System.Windows.Forms.Padding(0);
            this.panelSubconjunto.Name = "panelSubconjunto";
            this.panelSubconjunto.Size = new System.Drawing.Size(158, 100);
            this.panelSubconjunto.TabIndex = 16;
            this.panelSubconjunto.Visible = false;
            // 
            // btnTerminacion
            // 
            this.btnTerminacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminacion.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminacion.Location = new System.Drawing.Point(316, 100);
            this.btnTerminacion.Margin = new System.Windows.Forms.Padding(0);
            this.btnTerminacion.Name = "btnTerminacion";
            this.btnTerminacion.Size = new System.Drawing.Size(158, 25);
            this.btnTerminacion.TabIndex = 17;
            this.btnTerminacion.Text = "Terminación";
            this.btnTerminacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTerminacion.UseVisualStyleBackColor = true;
            this.btnTerminacion.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelTerminacion
            // 
            this.panelTerminacion.BackColor = System.Drawing.Color.Silver;
            this.panelTerminacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTerminacion.Location = new System.Drawing.Point(316, 125);
            this.panelTerminacion.Margin = new System.Windows.Forms.Padding(0);
            this.panelTerminacion.Name = "panelTerminacion";
            this.panelTerminacion.Size = new System.Drawing.Size(158, 100);
            this.panelTerminacion.TabIndex = 18;
            this.panelTerminacion.Visible = false;
            // 
            // btnUnidadMedida
            // 
            this.btnUnidadMedida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnidadMedida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnidadMedida.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnidadMedida.Location = new System.Drawing.Point(316, 225);
            this.btnUnidadMedida.Margin = new System.Windows.Forms.Padding(0);
            this.btnUnidadMedida.Name = "btnUnidadMedida";
            this.btnUnidadMedida.Size = new System.Drawing.Size(158, 25);
            this.btnUnidadMedida.TabIndex = 19;
            this.btnUnidadMedida.Text = "Unidad de Medida";
            this.btnUnidadMedida.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnidadMedida.UseVisualStyleBackColor = true;
            this.btnUnidadMedida.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelUnidadMedida
            // 
            this.panelUnidadMedida.BackColor = System.Drawing.Color.Silver;
            this.panelUnidadMedida.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelUnidadMedida.Location = new System.Drawing.Point(316, 250);
            this.panelUnidadMedida.Margin = new System.Windows.Forms.Padding(0);
            this.panelUnidadMedida.Name = "panelUnidadMedida";
            this.panelUnidadMedida.Size = new System.Drawing.Size(158, 100);
            this.panelUnidadMedida.TabIndex = 20;
            this.panelUnidadMedida.Visible = false;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.btnSalir);
            this.panel12.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpMenu.SetFlowBreak(this.panel12, true);
            this.panel12.Location = new System.Drawing.Point(316, 350);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(158, 100);
            this.panel12.TabIndex = 21;
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = true;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Image = global::GyCAP.UI.EstructuraProducto.Properties.Resources.Salir_25;
            this.btnSalir.Location = new System.Drawing.Point(37, 23);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 62);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
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
            this.scUp.Size = new System.Drawing.Size(625, 568);
            this.scUp.SplitterDistance = 20;
            this.scUp.TabIndex = 0;
            // 
            // btnMenu
            // 
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMenu.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(15, 568);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // frmEstructuraProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.scDown);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmEstructuraProducto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GyCAP - Estructura del Producto";
            this.Load += new System.EventHandler(this.frmEstructuraProducto_Load);
            this.scDown.Panel1.ResumeLayout(false);
            this.scDown.Panel2.ResumeLayout(false);
            this.scDown.ResumeLayout(false);
            this.flpMenu.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.scUp.Panel1.ResumeLayout(false);
            this.scUp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scDown;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;
        
        private System.Windows.Forms.SplitContainer scUp;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnCocina;
        private System.Windows.Forms.Panel panelCocina;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Button btnConjunto;
        private System.Windows.Forms.Panel panelConjunto;
        private System.Windows.Forms.Button btnDesignacion;
        private System.Windows.Forms.Panel panelDesignacion;
        private System.Windows.Forms.Button btnEstructuraProducto;
        private System.Windows.Forms.Panel panelEstructuraProducto;
        private System.Windows.Forms.Button btnMPPrincipal;
        private System.Windows.Forms.Panel panelMPPrincipal;
        private System.Windows.Forms.Button btnModeloCocina;
        private System.Windows.Forms.Panel panelModeloCocina;
        private System.Windows.Forms.Button btnPieza;
        private System.Windows.Forms.Panel panelPieza;
        private System.Windows.Forms.Button btnSubconjunto;
        private System.Windows.Forms.Panel panelSubconjunto;
        private System.Windows.Forms.Button btnTerminacion;
        private System.Windows.Forms.Panel panelTerminacion;
        private System.Windows.Forms.Button btnUnidadMedida;
        private System.Windows.Forms.Panel panelUnidadMedida;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btnSalir;

    }
}