namespace Presentacion.Localidad
{
    partial class PCom
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
            this.ButLimBus = new System.Windows.Forms.Button();
            this.ButBus = new System.Windows.Forms.Button();
            this.TextBus = new System.Windows.Forms.TextBox();
            this.ComboBus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textConPro = new System.Windows.Forms.TextBox();
            this.textConCom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboIngModPro = new System.Windows.Forms.ComboBox();
            this.textId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ComboIngModReg = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ButEli = new System.Windows.Forms.Button();
            this.ButMod = new System.Windows.Forms.Button();
            this.ButIng = new System.Windows.Forms.Button();
            this.TextIngMod = new System.Windows.Forms.TextBox();
            this.Grilla = new System.Windows.Forms.DataGridView();
            this.Selec = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdCom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comuna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdPro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Provincia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Región = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckEli = new System.Windows.Forms.CheckBox();
            this.CheckMod = new System.Windows.Forms.CheckBox();
            this.CheckIng = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButSal = new System.Windows.Forms.Button();
            this.ButLim = new System.Windows.Forms.Button();
            this.ButVol = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grilla)).BeginInit();
            this.SuspendLayout();
            // 
            // ButLimBus
            // 
            this.ButLimBus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButLimBus.Location = new System.Drawing.Point(712, 109);
            this.ButLimBus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButLimBus.Name = "ButLimBus";
            this.ButLimBus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButLimBus.Size = new System.Drawing.Size(175, 23);
            this.ButLimBus.TabIndex = 301;
            this.ButLimBus.Text = "Limpiar Busqueda";
            this.ButLimBus.UseVisualStyleBackColor = true;
            this.ButLimBus.Click += new System.EventHandler(this.ButLimBus_Click);
            // 
            // ButBus
            // 
            this.ButBus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButBus.Location = new System.Drawing.Point(629, 109);
            this.ButBus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButBus.Name = "ButBus";
            this.ButBus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButBus.Size = new System.Drawing.Size(71, 23);
            this.ButBus.TabIndex = 300;
            this.ButBus.Text = "Buscar";
            this.ButBus.UseVisualStyleBackColor = true;
            this.ButBus.Click += new System.EventHandler(this.ButBus_Click);
            // 
            // TextBus
            // 
            this.TextBus.Location = new System.Drawing.Point(282, 109);
            this.TextBus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBus.Name = "TextBus";
            this.TextBus.Size = new System.Drawing.Size(335, 22);
            this.TextBus.TabIndex = 299;
            this.TextBus.TabStop = false;
            this.TextBus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBus.TextChanged += new System.EventHandler(this.TextBus_TextChanged);
            this.TextBus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBus_KeyPress);
            // 
            // ComboBus
            // 
            this.ComboBus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBus.FormattingEnabled = true;
            this.ComboBus.Location = new System.Drawing.Point(128, 108);
            this.ComboBus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboBus.Name = "ComboBus";
            this.ComboBus.Size = new System.Drawing.Size(142, 24);
            this.ComboBus.TabIndex = 298;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 297;
            this.label4.Text = "Buscar Por:";
            // 
            // textConPro
            // 
            this.textConPro.Enabled = false;
            this.textConPro.Location = new System.Drawing.Point(108, 570);
            this.textConPro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textConPro.Name = "textConPro";
            this.textConPro.Size = new System.Drawing.Size(162, 22);
            this.textConPro.TabIndex = 296;
            this.textConPro.TabStop = false;
            this.textConPro.Visible = false;
            // 
            // textConCom
            // 
            this.textConCom.Enabled = false;
            this.textConCom.Location = new System.Drawing.Point(108, 518);
            this.textConCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textConCom.Name = "textConCom";
            this.textConCom.Size = new System.Drawing.Size(162, 22);
            this.textConCom.TabIndex = 295;
            this.textConCom.TabStop = false;
            this.textConCom.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(487, 420);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 294;
            this.label2.Text = "Provincia:";
            // 
            // ComboIngModPro
            // 
            this.ComboIngModPro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboIngModPro.Enabled = false;
            this.ComboIngModPro.FormattingEnabled = true;
            this.ComboIngModPro.Location = new System.Drawing.Point(569, 416);
            this.ComboIngModPro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboIngModPro.Name = "ComboIngModPro";
            this.ComboIngModPro.Size = new System.Drawing.Size(240, 24);
            this.ComboIngModPro.TabIndex = 293;
            this.ComboIngModPro.SelectedIndexChanged += new System.EventHandler(this.ComboIngModPro_SelectedIndexChanged);
            // 
            // textId
            // 
            this.textId.Enabled = false;
            this.textId.Location = new System.Drawing.Point(107, 544);
            this.textId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textId.Name = "textId";
            this.textId.Size = new System.Drawing.Size(162, 22);
            this.textId.TabIndex = 292;
            this.textId.TabStop = false;
            this.textId.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(252, 467);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 16);
            this.label7.TabIndex = 291;
            // 
            // ComboIngModReg
            // 
            this.ComboIngModReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboIngModReg.Enabled = false;
            this.ComboIngModReg.FormattingEnabled = true;
            this.ComboIngModReg.Location = new System.Drawing.Point(130, 416);
            this.ComboIngModReg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboIngModReg.Name = "ComboIngModReg";
            this.ComboIngModReg.Size = new System.Drawing.Size(353, 24);
            this.ComboIngModReg.TabIndex = 290;
            this.ComboIngModReg.SelectedIndexChanged += new System.EventHandler(this.ComboIngModReg_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 289;
            this.label3.Text = "Región:";
            // 
            // ButEli
            // 
            this.ButEli.Enabled = false;
            this.ButEli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButEli.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButEli.Location = new System.Drawing.Point(892, 402);
            this.ButEli.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButEli.Name = "ButEli";
            this.ButEli.Size = new System.Drawing.Size(107, 49);
            this.ButEli.TabIndex = 288;
            this.ButEli.TabStop = false;
            this.ButEli.Text = "Eliminar";
            this.ButEli.UseVisualStyleBackColor = true;
            this.ButEli.Visible = false;
            this.ButEli.Click += new System.EventHandler(this.ButEli_Click);
            // 
            // ButMod
            // 
            this.ButMod.Enabled = false;
            this.ButMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButMod.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButMod.Location = new System.Drawing.Point(892, 402);
            this.ButMod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButMod.Name = "ButMod";
            this.ButMod.Size = new System.Drawing.Size(107, 49);
            this.ButMod.TabIndex = 287;
            this.ButMod.TabStop = false;
            this.ButMod.Text = "Modificar";
            this.ButMod.UseVisualStyleBackColor = true;
            this.ButMod.Visible = false;
            this.ButMod.Click += new System.EventHandler(this.ButMod_Click);
            // 
            // ButIng
            // 
            this.ButIng.Enabled = false;
            this.ButIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButIng.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButIng.Location = new System.Drawing.Point(892, 402);
            this.ButIng.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButIng.Name = "ButIng";
            this.ButIng.Size = new System.Drawing.Size(107, 49);
            this.ButIng.TabIndex = 283;
            this.ButIng.TabStop = false;
            this.ButIng.Text = "Ingresar";
            this.ButIng.UseVisualStyleBackColor = true;
            this.ButIng.Visible = false;
            this.ButIng.Click += new System.EventHandler(this.ButIng_Click);
            // 
            // TextIngMod
            // 
            this.TextIngMod.Enabled = false;
            this.TextIngMod.Location = new System.Drawing.Point(487, 462);
            this.TextIngMod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextIngMod.Name = "TextIngMod";
            this.TextIngMod.Size = new System.Drawing.Size(205, 22);
            this.TextIngMod.TabIndex = 282;
            this.TextIngMod.TabStop = false;
            this.TextIngMod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextIngMod.TextChanged += new System.EventHandler(this.TextIngMod_TextChanged);
            this.TextIngMod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextIngMod_KeyPress);
            // 
            // Grilla
            // 
            this.Grilla.AllowUserToAddRows = false;
            this.Grilla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Grilla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grilla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grilla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selec,
            this.IdCom,
            this.Comuna,
            this.IdPro,
            this.Provincia,
            this.IdReg,
            this.Región});
            this.Grilla.EnableHeadersVisualStyles = false;
            this.Grilla.Location = new System.Drawing.Point(34, 149);
            this.Grilla.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Grilla.Name = "Grilla";
            this.Grilla.ReadOnly = true;
            this.Grilla.RowHeadersVisible = false;
            this.Grilla.Size = new System.Drawing.Size(853, 238);
            this.Grilla.TabIndex = 281;
            this.Grilla.DoubleClick += new System.EventHandler(this.Grilla_DoubleClick);
            // 
            // Selec
            // 
            this.Selec.HeaderText = "Selec";
            this.Selec.Name = "Selec";
            this.Selec.ReadOnly = true;
            this.Selec.Visible = false;
            // 
            // IdCom
            // 
            this.IdCom.HeaderText = "IdCom";
            this.IdCom.Name = "IdCom";
            this.IdCom.ReadOnly = true;
            this.IdCom.Visible = false;
            // 
            // Comuna
            // 
            this.Comuna.HeaderText = "Comuna";
            this.Comuna.Name = "Comuna";
            this.Comuna.ReadOnly = true;
            this.Comuna.Width = 240;
            // 
            // IdPro
            // 
            this.IdPro.HeaderText = "IdPro";
            this.IdPro.Name = "IdPro";
            this.IdPro.ReadOnly = true;
            this.IdPro.Visible = false;
            // 
            // Provincia
            // 
            this.Provincia.HeaderText = "Provincia";
            this.Provincia.Name = "Provincia";
            this.Provincia.ReadOnly = true;
            this.Provincia.Width = 240;
            // 
            // IdReg
            // 
            this.IdReg.HeaderText = "IdReg";
            this.IdReg.Name = "IdReg";
            this.IdReg.ReadOnly = true;
            this.IdReg.Visible = false;
            // 
            // Región
            // 
            this.Región.HeaderText = "Región";
            this.Región.Name = "Región";
            this.Región.ReadOnly = true;
            this.Región.Width = 354;
            // 
            // CheckEli
            // 
            this.CheckEli.AutoSize = true;
            this.CheckEli.Enabled = false;
            this.CheckEli.Location = new System.Drawing.Point(805, 67);
            this.CheckEli.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckEli.Name = "CheckEli";
            this.CheckEli.Size = new System.Drawing.Size(82, 20);
            this.CheckEli.TabIndex = 280;
            this.CheckEli.TabStop = false;
            this.CheckEli.Text = "Eliminar";
            this.CheckEli.UseVisualStyleBackColor = true;
            this.CheckEli.CheckedChanged += new System.EventHandler(this.CheckEli_CheckedChanged);
            // 
            // CheckMod
            // 
            this.CheckMod.AutoSize = true;
            this.CheckMod.Enabled = false;
            this.CheckMod.Location = new System.Drawing.Point(415, 67);
            this.CheckMod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckMod.Name = "CheckMod";
            this.CheckMod.Size = new System.Drawing.Size(90, 20);
            this.CheckMod.TabIndex = 279;
            this.CheckMod.TabStop = false;
            this.CheckMod.Text = "Modificar";
            this.CheckMod.UseVisualStyleBackColor = true;
            this.CheckMod.CheckedChanged += new System.EventHandler(this.CheckMod_CheckedChanged);
            // 
            // CheckIng
            // 
            this.CheckIng.AutoSize = true;
            this.CheckIng.Location = new System.Drawing.Point(34, 67);
            this.CheckIng.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckIng.Name = "CheckIng";
            this.CheckIng.Size = new System.Drawing.Size(83, 20);
            this.CheckIng.TabIndex = 278;
            this.CheckIng.TabStop = false;
            this.CheckIng.Text = "Ingresar";
            this.CheckIng.UseVisualStyleBackColor = true;
            this.CheckIng.CheckedChanged += new System.EventHandler(this.CheckIng_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 16);
            this.label1.TabIndex = 277;
            this.label1.Text = "Información sobre Comunas";
            // 
            // ButSal
            // 
            this.ButSal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButSal.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ButSal.Location = new System.Drawing.Point(910, 310);
            this.ButSal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButSal.Name = "ButSal";
            this.ButSal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButSal.Size = new System.Drawing.Size(71, 49);
            this.ButSal.TabIndex = 286;
            this.ButSal.Text = "Salir";
            this.ButSal.UseVisualStyleBackColor = true;
            this.ButSal.Click += new System.EventHandler(this.ButSal_Click);
            // 
            // ButLim
            // 
            this.ButLim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButLim.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ButLim.Location = new System.Drawing.Point(910, 166);
            this.ButLim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButLim.Name = "ButLim";
            this.ButLim.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButLim.Size = new System.Drawing.Size(71, 49);
            this.ButLim.TabIndex = 285;
            this.ButLim.Text = "Limpiar";
            this.ButLim.UseVisualStyleBackColor = true;
            this.ButLim.Click += new System.EventHandler(this.ButLim_Click);
            // 
            // ButVol
            // 
            this.ButVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButVol.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ButVol.Location = new System.Drawing.Point(910, 238);
            this.ButVol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButVol.Name = "ButVol";
            this.ButVol.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButVol.Size = new System.Drawing.Size(71, 49);
            this.ButVol.TabIndex = 284;
            this.ButVol.Text = "Volver";
            this.ButVol.UseVisualStyleBackColor = true;
            this.ButVol.Click += new System.EventHandler(this.ButVol_Click);
            // 
            // PCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 489);
            this.ControlBox = false;
            this.Controls.Add(this.ButLimBus);
            this.Controls.Add(this.ButBus);
            this.Controls.Add(this.TextBus);
            this.Controls.Add(this.ComboBus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textConPro);
            this.Controls.Add(this.textConCom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboIngModPro);
            this.Controls.Add(this.textId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ComboIngModReg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ButEli);
            this.Controls.Add(this.ButMod);
            this.Controls.Add(this.ButIng);
            this.Controls.Add(this.TextIngMod);
            this.Controls.Add(this.Grilla);
            this.Controls.Add(this.CheckEli);
            this.Controls.Add(this.CheckMod);
            this.Controls.Add(this.CheckIng);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButSal);
            this.Controls.Add(this.ButLim);
            this.Controls.Add(this.ButVol);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PCom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comunas";
            this.Load += new System.EventHandler(this.PCom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grilla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButLimBus;
        private System.Windows.Forms.Button ButBus;
        private System.Windows.Forms.TextBox TextBus;
        public System.Windows.Forms.ComboBox ComboBus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textConPro;
        private System.Windows.Forms.TextBox textConCom;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox ComboIngModPro;
        private System.Windows.Forms.TextBox textId;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox ComboIngModReg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ButEli;
        private System.Windows.Forms.Button ButMod;
        private System.Windows.Forms.Button ButIng;
        public System.Windows.Forms.TextBox TextIngMod;
        private System.Windows.Forms.DataGridView Grilla;
        private System.Windows.Forms.DataGridViewButtonColumn Selec;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comuna;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdPro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Provincia;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Región;
        public System.Windows.Forms.CheckBox CheckEli;
        public System.Windows.Forms.CheckBox CheckMod;
        public System.Windows.Forms.CheckBox CheckIng;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button ButSal;
        private System.Windows.Forms.Button ButLim;
        public System.Windows.Forms.Button ButVol;
    }
}