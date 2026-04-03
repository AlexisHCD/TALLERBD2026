using Entidad;
using Negocio;
using Presentacion.AAClases;
using System;
using System.Data;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace Presentacion.Cliente
{
    public partial class PCli_Ing : Form
    {
        ValidaRut Rut = new ValidaRut();
        ECliente Ent = new ECliente();
        NLocCom NegCom = new NLocCom();
        NLocPro NegPro = new NLocPro();
        NLocReg NegReg = new NLocReg();
        public PCli_Ing()
        {
            InitializeComponent();
        }

        private void PCli_Ing_Load(object sender, EventArgs e)
        {

        }
        public void LleComReg()
        {
            CBReg.DisplayMember = "Nombre";
            CBReg.ValueMember = "IdReg";
            CBReg.DataSource = NegReg.Listar();
        }

        private void CargaCBPro()
        {
            int IdReg = Convert.ToInt32(CBReg.SelectedValue);
            DataTable dt = NegPro.Filtrar(IdReg);
            CBPro.DisplayMember = "Nombre";
            CBPro.ValueMember = "IdPro";
            CBPro.DataSource = dt;
        }

        private void CargaCBCom()
        {
            int IdPro = Convert.ToInt32(CBPro.SelectedValue);
            DataTable dt = NegCom.Filtrar(IdPro);
            CBCom.DisplayMember = "Nombre";
            CBCom.ValueMember = "IdCom";
            CBCom.DataSource = dt;
        }

        public void HabBotIng() // Habilitar Boton de Ingerso
        {
            if ((TextRut.Text.Trim() != "") && (TextNom.Text.Trim() != "") && (CBReg.Text.Trim() != "") && (CBPro.Text.Trim() != "") && (CBCom.Text.Trim() != "") && (TextDire.Text.Trim() != "") && (TextTel.Text.Trim() != "") && (TextEma.Text.Trim() != "") && (TextGir.Text.Trim() != "")) // "Y" Distinto a vacio 
            {
                ButIng.Enabled = true;
            }
            else
            {
                ButIng.Enabled = false;
            }
        }
        public void HabBotLim() // Habilitar Boton de Ingreso
        {
            if ((TextRut.Text.Trim() != "") || (TextNom.Text.Trim() != "") || (CBReg.Text.Trim() != "") || (CBPro.Text.Trim() != "") || (CBCom.Text.Trim() != "") || (TextDire.Text.Trim() != "") || (TextTel.Text.Trim() != "") || (TextEma.Text.Trim() != "") || (TextGir.Text.Trim() != "")) // "O" Distinto a vacio
            {
                ButLim.Enabled = true;
            }
            else
            {
                ButLim.Enabled = false;
            }
        }

        private void TextRut_Leave(object sender, EventArgs e)
        {
            Ent.Rut = TextRut.Text;
            bool respuesta = false;
            respuesta = Rut.validarRut(TextRut.Text);

            if (respuesta == false)
            {
                TextRut.Clear();
                MessageBox.Show("Rut Malo", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Rut Bueno", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HabBotLim();
                HabBotIng();
            }

        }
        private void TextRut_TextChanged(object sender, EventArgs e)
        {
            if (TextRut.Text.Trim() != "")
            {
                TextNom.TabStop = true;
                TextRut.TabStop = false;
            }
            else
            {
                TextNom.TabStop = false;
                TextRut.TabStop = true;
            }
            HabBotIng();
        }

        private void TextNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))                                                                                                    // Ciclo para bloquear numeros
            {
                e.Handled = true;                                                                                                           // Permitir cualquier valor menos la condición anterior
                MessageBox.Show("Solo se permiten letras.", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);                        // Mensaje de OK con error que dice que solo permite letras 
            }
        }

        private void TextNom_TextChanged(object sender, EventArgs e)
        {
            TextNom.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TextNom.Text);
            TextNom.SelectionStart = TextNom.Text.Length;
            if (TextNom.Text.Trim() != "")
            {
                CBReg.TabStop = true;
                TextNom.TabStop = false;
                LleComReg();
            }
            else
            {
                CBReg.TabStop = false;
                TextNom.TabStop = true;
            }
            HabBotIng();
        }

        private void CBReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBReg.Text.Trim() != "")
            {
                CBPro.TabStop = true;
                CBReg.TabStop = false;
                CargaCBPro();
            }
            else
            {
                CBReg.TabStop = false;
                CBPro.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void CBPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBPro.Text.Trim() != "")
            {
                CBCom.TabStop = true;
                CBPro.TabStop = false;
                CargaCBCom();
            }
            else
            {
                CBPro.TabStop = false;
                CBCom.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void CBCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBCom.Text.Trim() != "")
            {
                TextDire.TabStop = true;
                CBCom.TabStop = false;
            }
            else
            {
                TextDire.TabStop = false;
                CBCom.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void TextDire_TextChanged(object sender, EventArgs e)
        {
            if (TextDire.Text.Trim() != "")
            {
                TextTel.TabStop = true;
                TextDire.TabStop = false;
            }
            else
            {
                TextTel.TabStop = false;
                TextDire.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void TextTel_TextChanged(object sender, EventArgs e)
        {
            if (TextTel.Text.Trim() != "")
            {
                TextEma.TabStop = true;
                TextTel.TabStop = false;
            }
            else
            {
                TextEma.TabStop = false;
                TextTel.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void TextEma_TextChanged(object sender, EventArgs e)
        {
            if (TextEma.Text.Trim() != "")
            {
                TextGir.TabStop = true;
                TextEma.TabStop = false;
            }
            else
            {
                TextGir.TabStop = false;
                TextEma.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void TextGir_TextChanged(object sender, EventArgs e)
        {
            if (TextGir.Text.Trim() != "")
            {
                ButIng.TabStop = true;
                TextGir.TabStop = false;
            }
            else
            {
                ButIng.TabStop = false;
                TextGir.TabStop = true;
            }
            HabBotLim();
            HabBotIng();
        }

        private void ButLim_Click(object sender, EventArgs e)
        {
            TextRut.Clear();
            TextNom.Clear();
            TextDire.Clear();
            TextTel.Clear();
            TextEma.Clear();
            TextGir.Clear();
            CBReg.DataSource = null;
            CBPro.DataSource = null;
            CBCom.DataSource = null;
            HabBotIng();
        }

        private void ButIng_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Esta seguro de la acción a realizar?", "Sistema.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            string Mensaje = string.Empty;
            Ent.Nombre = TextNom.Text;
            Ent.Rut = TextRut.Text;
            Ent.IdCom = Convert.ToInt32(CBCom.SelectedValue);
            Ent.Direccion = TextDire.Text;
            Ent.Tel = TextTel.Text;
            Ent.Email = TextEma.Text;
            Ent.Giro = TextGir.Text;
            if (res == DialogResult.Yes)
            {
                Respuesta<bool> resultado = NCliente.Ingresar(Ent);

                if (resultado.estado)
                {
                    MessageBox.Show("Ingreso fue realizado correctamente", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ButLim.PerformClick();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }
            }
            else if (res == DialogResult.No)
            {
                ButVol.Focus();
            }
            else if (res == DialogResult.Cancel)
            {
                ButSal.Focus();
            }
            HabBotIng();
            HabBotLim();
        }

        private void ButVol_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButSal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
