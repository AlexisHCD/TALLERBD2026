using Entidad;
using Negocio;
using Presentacion.AAClases;
using System;
using System.Data;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace Presentacion.Proveedor
{
    public partial class PProv_Act : Form
    {
        public PProv_Act()
        {
            InitializeComponent();
        }
        ValidaRut Rut = new ValidaRut();
        EProv Ent = new EProv();
        NProv Neg = new NProv();
        NLocCom NegCom = new NLocCom();
        NLocPro NegPro = new NLocPro();
        NLocReg NegReg = new NLocReg();

        private void PProv_Act_Load(object sender, EventArgs e)
        {
            Validar();
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

        public void Validar()
        {
            if ((TextNomF.Text.Trim() != TextNomI.Text.Trim()) || (TextRutF.Text.Trim() != labelRtI.Text.Trim()) || (TextComIdeF.Text.Trim() != TextComIdeI.Text.Trim()) || (TextComI.Text.Trim() != TextComF.Text.Trim()) || (TextDireF.Text.Trim() != TextDireI.Text.Trim()) || (TextGirF.Text.Trim() != TextGirI.Text.Trim()) || (TextDesF.Text.Trim() != TextDesI.Text.Trim()) || (TextTelF.Text.Trim() != TextTelI.Text.Trim()) || (TextEmaF.Text.Trim() != TextEmaI.Text.Trim()))
            {
                ButMod.Enabled = true;
                LabelRut.Enabled = false;
                LabelNom.Enabled = false;
                labelActCom.Enabled = false;
                LabelDir.Enabled = false;
                LabelTel.Enabled = false;
                LabelEma.Enabled = false;
                LabelGir.Enabled = false;
                LabelDes.Enabled = false;
            }
            else
            {
                ButMod.Enabled = false;
                LabelRut.Enabled = true;
                LabelNom.Enabled = true;
                labelActCom.Enabled = true;
                LabelDir.Enabled = true;
                LabelTel.Enabled = true;
                LabelEma.Enabled = true;
                LabelGir.Enabled = true;
                LabelDes.Enabled = true;
            }
        }

        private void LabelRut_Click(object sender, EventArgs e)
        {
            TextRutF.Enabled = true;
        }

        private void TextRutF_Leave(object sender, EventArgs e)
        {
            Ent.Rut = TextRutF.Text;
            bool respuesta = false;
            respuesta = Rut.validarRut(TextRutF.Text);
            if (respuesta == false)
            {
                TextRutF.Clear();
                MessageBox.Show("Rut Malo", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Rut Bueno", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Validar();
            }
        }

        private void LabelNom_Click(object sender, EventArgs e)
        {
            TextNomF.Enabled = true;
        }

        private void TextNomF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("solo se permiten letras");
            }
        }

        private void TextNomF_TextChanged(object sender, EventArgs e)
        {
            TextNomF.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TextNomF.Text);
            TextNomF.SelectionStart = TextNomF.Text.Length;
            Validar();
        }

        private void LabelTel_Click(object sender, EventArgs e)
        {
            TextTelF.Enabled = true;
        }

        private void TextTelF_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void labelActCom_Click(object sender, EventArgs e)
        {
            LleComReg();
            labelActReg.Enabled = true;
            CBReg.Enabled = true;
            labelAcPro.Enabled = true;
            CBPro.Enabled = true;
            LabelAcCom.Enabled = true;
            CBCom.Enabled = true;
        }

        private void CBReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCBPro();
        }

        private void CBPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCBCom();
        }

        private void CBCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextComF.Text = CBCom.Text;
            TextComIdeF.Text = Convert.ToString(CBCom.SelectedValue);
            Validar();
        }

        private void LabelDir_Click(object sender, EventArgs e)
        {
            TextDireF.Enabled = true;
        }

        private void TextDireF_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void LabelEma_Click(object sender, EventArgs e)
        {
            Validar();
        }

        private void TextEmaF_TextChanged(object sender, EventArgs e)
        {
            TextEmaF.Enabled = true;
        }

        private void LabelGir_Click(object sender, EventArgs e)
        {
            TextGirF.Enabled = true;
        }

        private void TextGirF_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void LabelDes_Click(object sender, EventArgs e)
        {
            TextDesF.Enabled = true;
        }

        private void TextDesF_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }
        public void Cambio()
        {
            LabelRut.Enabled = true;
            LabelNom.Enabled = true;
            labelActCom.Enabled = true;
            LabelDir.Enabled = true;
            LabelTel.Enabled = true;
            LabelEma.Enabled = true;
            LabelGir.Enabled = true;
            LabelDes.Enabled = true;
            TextNomF.Enabled = false;
            TextRutF.Enabled = false;
            TextDireF.Enabled = false;
            TextTelF.Enabled = false;
            TextEmaF.Enabled = false;
            TextGirF.Enabled = false;
            TextDesF.Enabled = false;
            ButMod.Enabled = false;
            labelActReg.Enabled = false;
            CBReg.Enabled = false;
            labelAcPro.Enabled = false;
            CBPro.Enabled = false;
            LabelAcCom.Enabled = false;
            CBCom.Enabled = false;
        }

        private void ButMod_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Esta seguro de la acción a realizar?", "Sistema.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            string Mensaje = string.Empty;
            Ent.IdProv = Convert.ToInt32(TextIdCli.Text);
            Ent.Nombre = TextNomF.Text;
            Ent.Rut = TextRutF.Text;
            Ent.IdCom = Convert.ToInt32(TextComIdeF.Text);
            Ent.Direccion = TextDireF.Text;
            Ent.Tel = TextTelF.Text;
            Ent.Email = TextEmaF.Text;
            Ent.Giro = TextGirF.Text;
            Ent.Descr = TextDesF.Text;
            if (res == DialogResult.Yes)
            {
                Respuesta<bool> resultado = NProv.Actualizar(Ent);
                if (resultado.estado)
                {
                    MessageBox.Show("Actualización fue realizado correctamente", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextNomI.Text = TextNomF.Text;
                    labelRtI.Text = TextRutF.Text;
                    TextComIdeI.Text = TextComIdeF.Text;
                    TextComI.Text = TextComF.Text;
                    TextDireI.Text = TextDireF.Text;
                    TextTelI.Text = TextTelF.Text;
                    TextEmaI.Text = TextEmaF.Text;
                    TextGirI.Text = TextGirF.Text;
                    TextDesI.Text = TextDesF.Text;
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
            Cambio();
            Validar();
        }

        private void ButAnu_Click(object sender, EventArgs e)
        {
            TextNomF.Text = TextNomI.Text;
            TextRutF.Text = labelRtI.Text;
            TextComIdeF.Text = TextComIdeI.Text;
            TextComF.Text = TextComI.Text;
            TextDireF.Text = TextDireI.Text;
            TextTelF.Text = TextTelI.Text;
            TextEmaF.Text = TextEmaI.Text;
            TextGirF.Text = TextGirI.Text;
            TextDesF.Text = TextDesI.Text;
            Cambio();
        }

        private void ButVol_Click(object sender, EventArgs e)
        {
            PProv_Con ver = new PProv_Con();
            ver.ButMod.Visible = true;
            this.Close();
        }

        private void ButSal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
