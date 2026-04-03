using Entidad;
using Negocio;
using Presentacion.AAClases;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace Presentacion.Proveedor
{
    public partial class PProv_Con : Form
    {
        public PProv_Con()
        {
            InitializeComponent();
        }

        EProv Ent = new EProv();
        NProv Neg = new NProv();
        NLocCom NegCom = new NLocCom();
        NLocPro NegPro = new NLocPro();
        NLocReg NegReg = new NLocReg();

        private void PProv_Con_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in Grilla.Columns)
            {

                if (columna.Visible == true && columna.Name != "Selec")
                {
                    ComboBus.Items.Add(new Filtrar() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            ComboBus.DisplayMember = "Texto";
            ComboBus.ValueMember = "Valor";
            ComboBus.SelectedIndex = 0;
            CarDat();
        }

        public void CarDat()
        {
            try
            {
                Grilla.Rows.Clear();
                List<EProv> Listar = new NProv().Listar();
                foreach (EProv item in Listar)
                {
                    Grilla.Rows.Add(new object[] { "", item.IdProv, item.Nombre, item.Rut, item.IdCom, item.Com.Nombre, item.Direccion, item.Tel, item.Email, item.Giro, item.Descr });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Grilla.ClearSelection();
        }

        public void ResetGrid()
        {
            foreach (DataGridViewRow row in Grilla.Rows)
            {
                row.Visible = true;
            }
        }

        private void TextBus_TextChanged(object sender, EventArgs e)
        {
            TextBus.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TextBus.Text);
            TextBus.SelectionStart = TextBus.Text.Length;
        }

        private void ButBus_Click(object sender, EventArgs e)
        {
            TextBus.Text = "";
            CarDat();
        }

        private void Grilla_DoubleClick(object sender, EventArgs e)
        {
            ButMod.Enabled = true;
            ButEli.Enabled = true;
            TextBox1.Text = this.Grilla.CurrentRow.Cells[1].Value.ToString();
        }

        private void ButMod_Click(object sender, EventArgs e)
        {
            PProv_Act pasar = new PProv_Act();
            pasar.TextIdCli.Text = this.Grilla.CurrentRow.Cells[1].Value.ToString();
            pasar.TextNomF.Text = this.Grilla.CurrentRow.Cells[2].Value.ToString();
            pasar.TextNomI.Text = this.Grilla.CurrentRow.Cells[2].Value.ToString();
            pasar.TextRutF.Text = this.Grilla.CurrentRow.Cells[3].Value.ToString();
            pasar.labelRtI.Text = this.Grilla.CurrentRow.Cells[3].Value.ToString();
            pasar.TextComIdeF.Text = this.Grilla.CurrentRow.Cells[4].Value.ToString();
            pasar.TextComIdeI.Text = this.Grilla.CurrentRow.Cells[4].Value.ToString();
            pasar.TextComI.Text = this.Grilla.CurrentRow.Cells[5].Value.ToString();
            pasar.TextComF.Text = this.Grilla.CurrentRow.Cells[5].Value.ToString();
            pasar.TextDireF.Text = this.Grilla.CurrentRow.Cells[6].Value.ToString();
            pasar.TextDireI.Text = this.Grilla.CurrentRow.Cells[6].Value.ToString();
            pasar.TextTelF.Text = this.Grilla.CurrentRow.Cells[7].Value.ToString();
            pasar.TextTelI.Text = this.Grilla.CurrentRow.Cells[7].Value.ToString();
            pasar.TextEmaF.Text = this.Grilla.CurrentRow.Cells[8].Value.ToString();
            pasar.TextEmaI.Text = this.Grilla.CurrentRow.Cells[8].Value.ToString();
            pasar.TextGirF.Text = this.Grilla.CurrentRow.Cells[9].Value.ToString();
            pasar.TextGirI.Text = this.Grilla.CurrentRow.Cells[9].Value.ToString();
            pasar.TextDesF.Text = this.Grilla.CurrentRow.Cells[10].Value.ToString();
            pasar.TextDesI.Text = this.Grilla.CurrentRow.Cells[10].Value.ToString();
            pasar.LabelRut.Enabled = true;
            pasar.LabelNom.Enabled = true;
            pasar.labelActCom.Enabled = true;
            pasar.LabelDir.Enabled = true;
            pasar.LabelTel.Enabled = true;
            pasar.LabelEma.Enabled = true;
            pasar.LabelGir.Enabled = true;
            pasar.Show();
            ButMod.Enabled = false;
            ButEli.Enabled = false;
            this.Close();
        }

        private void ButEli_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("¿Está seguro de la acción a realizar?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                int Id;
                if (int.TryParse(TextBox1.Text, out Id))
                {
                    Respuesta<bool> resultado = NProv.Eliminar(Id);
                    if (resultado.estado)
                    {
                        MessageBox.Show("La eliminación se realizó correctamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un registro válido", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Grilla.Rows.Clear();
            CarDat();
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
