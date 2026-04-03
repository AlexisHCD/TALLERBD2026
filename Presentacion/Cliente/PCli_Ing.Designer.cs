namespace Presentacion.Cliente
{
    partial class PCli_Ing
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
            this.ButLim = new System.Windows.Forms.Button();
            this.ButIng = new System.Windows.Forms.Button();
            this.ButSal = new System.Windows.Forms.Button();
            this.ButVol = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextGir = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.TextTel = new System.Windows.Forms.TextBox();
            this.TextEma = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextDire = new System.Windows.Forms.TextBox();
            this.TextNom = new System.Windows.Forms.TextBox();
            this.CBCom = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.CBPro = new System.Windows.Forms.ComboBox();
            this.CBReg = new System.Windows.Forms.ComboBox();
            this.TextRut = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButLim
            // 
            this.ButLim.Enabled = false;
            this.ButLim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButLim.Location = new System.Drawing.Point(245, 411);
            this.ButLim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButLim.Name = "ButLim";
            this.ButLim.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButLim.Size = new System.Drawing.Size(71, 49);
            this.ButLim.TabIndex = 100;
            this.ButLim.TabStop = false;
            this.ButLim.Text = "Limpiar";
            this.ButLim.UseVisualStyleBackColor = true;
            this.ButLim.Click += new System.EventHandler(this.ButLim_Click);
            // 
            // ButIng
            // 
            this.ButIng.Enabled = false;
            this.ButIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButIng.Location = new System.Drawing.Point(420, 411);
            this.ButIng.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButIng.Name = "ButIng";
            this.ButIng.Size = new System.Drawing.Size(107, 49);
            this.ButIng.TabIndex = 99;
            this.ButIng.TabStop = false;
            this.ButIng.Text = "Ingresar";
            this.ButIng.UseVisualStyleBackColor = true;
            this.ButIng.Click += new System.EventHandler(this.ButIng_Click);
            // 
            // ButSal
            // 
            this.ButSal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButSal.Location = new System.Drawing.Point(732, 411);
            this.ButSal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButSal.Name = "ButSal";
            this.ButSal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButSal.Size = new System.Drawing.Size(71, 49);
            this.ButSal.TabIndex = 98;
            this.ButSal.TabStop = false;
            this.ButSal.Text = "Salir";
            this.ButSal.UseVisualStyleBackColor = true;
            this.ButSal.Click += new System.EventHandler(this.ButSal_Click);
            // 
            // ButVol
            // 
            this.ButVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButVol.Location = new System.Drawing.Point(619, 411);
            this.ButVol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButVol.Name = "ButVol";
            this.ButVol.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButVol.Size = new System.Drawing.Size(71, 49);
            this.ButVol.TabIndex = 97;
            this.ButVol.TabStop = false;
            this.ButVol.Text = "Volver";
            this.ButVol.UseVisualStyleBackColor = true;
            this.ButVol.Click += new System.EventHandler(this.ButVol_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(405, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 16);
            this.label1.TabIndex = 96;
            this.label1.Text = "Ingreso Nuevo Cliente:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextGir);
            this.groupBox1.Controls.Add(this.Label3);
            this.groupBox1.Controls.Add(this.TextTel);
            this.groupBox1.Controls.Add(this.TextEma);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextDire);
            this.groupBox1.Controls.Add(this.TextNom);
            this.groupBox1.Controls.Add(this.CBCom);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.CBPro);
            this.groupBox1.Controls.Add(this.CBReg);
            this.groupBox1.Controls.Add(this.TextRut);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(157, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 342);
            this.groupBox1.TabIndex = 95;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Personales";
            // 
            // TextGir
            // 
            this.TextGir.Location = new System.Drawing.Point(259, 300);
            this.TextGir.Name = "TextGir";
            this.TextGir.Size = new System.Drawing.Size(374, 22);
            this.TextGir.TabIndex = 42;
            this.TextGir.TabStop = false;
            this.TextGir.TextChanged += new System.EventHandler(this.TextGir_TextChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(118, 303);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(40, 16);
            this.Label3.TabIndex = 41;
            this.Label3.Text = "Giro:";
            // 
            // TextTel
            // 
            this.TextTel.Location = new System.Drawing.Point(63, 250);
            this.TextTel.Name = "TextTel";
            this.TextTel.Size = new System.Drawing.Size(161, 22);
            this.TextTel.TabIndex = 14;
            this.TextTel.TabStop = false;
            this.TextTel.TextChanged += new System.EventHandler(this.TextTel_TextChanged);
            // 
            // TextEma
            // 
            this.TextEma.Location = new System.Drawing.Point(308, 250);
            this.TextEma.Name = "TextEma";
            this.TextEma.Size = new System.Drawing.Size(374, 22);
            this.TextEma.TabIndex = 15;
            this.TextEma.TabStop = false;
            this.TextEma.TextChanged += new System.EventHandler(this.TextEma_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(106, 220);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 16);
            this.label20.TabIndex = 40;
            this.label20.Text = "Telefono:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label21.Location = new System.Drawing.Point(470, 220);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 16);
            this.label21.TabIndex = 39;
            this.label21.Text = "Email:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(9, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Rut:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(64, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 16);
            this.label19.TabIndex = 38;
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(256, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // TextDire
            // 
            this.TextDire.Location = new System.Drawing.Point(308, 181);
            this.TextDire.Name = "TextDire";
            this.TextDire.Size = new System.Drawing.Size(374, 22);
            this.TextDire.TabIndex = 13;
            this.TextDire.TabStop = false;
            this.TextDire.TextChanged += new System.EventHandler(this.TextDire_TextChanged);
            // 
            // TextNom
            // 
            this.TextNom.Location = new System.Drawing.Point(339, 40);
            this.TextNom.Name = "TextNom";
            this.TextNom.Size = new System.Drawing.Size(343, 22);
            this.TextNom.TabIndex = 2;
            this.TextNom.TextChanged += new System.EventHandler(this.TextNom_TextChanged);
            this.TextNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNom_KeyPress);
            // 
            // CBCom
            // 
            this.CBCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBCom.FormattingEnabled = true;
            this.CBCom.Location = new System.Drawing.Point(12, 180);
            this.CBCom.Name = "CBCom";
            this.CBCom.Size = new System.Drawing.Size(263, 24);
            this.CBCom.TabIndex = 12;
            this.CBCom.TabStop = false;
            this.CBCom.SelectedIndexChanged += new System.EventHandler(this.CBCom_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(106, 150);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 16);
            this.label18.TabIndex = 30;
            this.label18.Text = "Comuna:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(456, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "Dirección:";
            // 
            // CBPro
            // 
            this.CBPro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPro.FormattingEnabled = true;
            this.CBPro.Items.AddRange(new object[] {
            "Selecione Provincia"});
            this.CBPro.Location = new System.Drawing.Point(419, 110);
            this.CBPro.Name = "CBPro";
            this.CBPro.Size = new System.Drawing.Size(263, 24);
            this.CBPro.TabIndex = 11;
            this.CBPro.TabStop = false;
            this.CBPro.SelectedIndexChanged += new System.EventHandler(this.CBPro_SelectedIndexChanged);
            // 
            // CBReg
            // 
            this.CBReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBReg.FormattingEnabled = true;
            this.CBReg.Items.AddRange(new object[] {
            "Selecione Región"});
            this.CBReg.Location = new System.Drawing.Point(9, 110);
            this.CBReg.Name = "CBReg";
            this.CBReg.Size = new System.Drawing.Size(374, 24);
            this.CBReg.TabIndex = 10;
            this.CBReg.TabStop = false;
            this.CBReg.SelectedIndexChanged += new System.EventHandler(this.CBReg_SelectedIndexChanged);
            // 
            // TextRut
            // 
            this.TextRut.Location = new System.Drawing.Point(63, 40);
            this.TextRut.Name = "TextRut";
            this.TextRut.Size = new System.Drawing.Size(100, 22);
            this.TextRut.TabIndex = 1;
            this.TextRut.TextChanged += new System.EventHandler(this.TextRut_TextChanged);
            this.TextRut.Leave += new System.EventHandler(this.TextRut_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(512, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 16);
            this.label17.TabIndex = 29;
            this.label17.Text = "Provincia:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(165, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "Región:";
            // 
            // PCli_Ing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 480);
            this.ControlBox = false;
            this.Controls.Add(this.ButLim);
            this.Controls.Add(this.ButIng);
            this.Controls.Add(this.ButSal);
            this.Controls.Add(this.ButVol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PCli_Ing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCli_Ing";
            this.Load += new System.EventHandler(this.PCli_Ing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButLim;
        private System.Windows.Forms.Button ButIng;
        public System.Windows.Forms.Button ButSal;
        public System.Windows.Forms.Button ButVol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TextGir;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.TextBox TextTel;
        private System.Windows.Forms.TextBox TextEma;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextDire;
        private System.Windows.Forms.TextBox TextNom;
        private System.Windows.Forms.ComboBox CBCom;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox CBPro;
        private System.Windows.Forms.ComboBox CBReg;
        private System.Windows.Forms.TextBox TextRut;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
    }
}