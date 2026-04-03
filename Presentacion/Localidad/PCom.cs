using Entidad;
using Negocio;
using Presentacion.AAClases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Presentacion.Localidad
{
    public partial class PCom : Form
    {
        ELocCom Ent = new ELocCom();
        NLocPro NegPro = new NLocPro();
        NLocReg NegReg = new NLocReg();
        public PCom()
        {
            InitializeComponent();
        }

        private void PCom_Load(object sender, EventArgs e)
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
            LleComReg();
        }

        public void CarDat()
        {
            try
            {
                Grilla.Rows.Clear();
                List<ELocCom> Listar = new NLocCom().Listar();
                foreach (ELocCom item in Listar)
                {
                    Grilla.Rows.Add(new object[] { "", item.IdCom, item.Nombre, item.IdPro, item.Pro.Nombre, item.IdReg, item.Reg.Nombre });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Grilla.ClearSelection();
        }

        public void Validar()
        {
            if (TextIngMod.Text.Trim() != "")
            {
                if ((TextIngMod.Text == textConCom.Text) && (ComboIngModPro.Text == textConPro.Text))
                {
                    ButMod.Enabled = false;
                    ButIng.Enabled = false;
                }
                else
                {
                    ButMod.Enabled = true;
                    ButIng.Enabled = true;
                }
            }
            else
            {
                ButIng.Enabled = false;
                ButMod.Enabled = false;
                ButEli.Enabled = false;
            }
        }

        public void LleComReg()
        {
            ComboIngModReg.DisplayMember = "Nombre";
            ComboIngModReg.ValueMember = "IdReg";
            ComboIngModReg.DataSource = NegReg.Listar();
        }

        public void ResetGrid()
        {
            foreach (DataGridViewRow row in Grilla.Rows)
            {
                row.Visible = true;
            }
        }

        public void Check_funciones()
        {
            CheckIng.CheckState = CheckState.Unchecked;
            CheckMod.CheckState = CheckState.Unchecked;
            CheckEli.CheckState = CheckState.Unchecked;
            CheckIng.Enabled = true;
            CheckMod.Enabled = false;
            CheckEli.Enabled = false;
        }

        private void ButBus_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((Filtrar)ComboBus.SelectedItem).Valor.ToString();
            if (Grilla.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in Grilla.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(TextBus.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }
        private void TextBus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten letras.", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TextBus_TextChanged(object sender, EventArgs e)
        {
            TextBus.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TextBus.Text);
            TextBus.SelectionStart = TextBus.Text.Length;
            Validar();
        }

        private void ButLimBus_Click(object sender, EventArgs e)
        {
            TextBus.Text = "";
            CarDat();
        }

        private void Grilla_DoubleClick(object sender, EventArgs e)
        {
            textId.Clear();
            TextIngMod.Clear();
            CheckMod.Enabled = true;
            CheckEli.Enabled = true;
            CheckIng.CheckState = CheckState.Unchecked;
            CheckIng.Enabled = false;
            CheckMod.CheckState = CheckState.Unchecked;
            CheckEli.CheckState = CheckState.Unchecked;
            textId.Text = this.Grilla.CurrentRow.Cells[1].Value.ToString();
            textConCom.Text = this.Grilla.CurrentRow.Cells[2].Value.ToString();
            TextIngMod.Text = this.Grilla.CurrentRow.Cells[2].Value.ToString();
            textConPro.Text = this.Grilla.CurrentRow.Cells[4].Value.ToString();
            ComboIngModPro.Text = this.Grilla.CurrentRow.Cells[4].Value.ToString();
            ComboIngModReg.Text = this.Grilla.CurrentRow.Cells[6].Value.ToString();
            TextIngMod.Enabled = false;
            ComboIngModPro.Enabled = false;
        }

        private void CheckIng_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckIng.CheckState == CheckState.Checked)
            {
                CheckMod.CheckState = CheckState.Unchecked;
                CheckEli.CheckState = CheckState.Unchecked;
                label7.Text = "Ingresar Comuna:";
                TextIngMod.Enabled = true;
                ComboIngModPro.Enabled = true;
                ComboIngModReg.Enabled = true;
                TextIngMod.Clear();
                textId.Clear();
                ButIng.Visible = true;
            }
            else
            {
                label7.Text = "";
                ButIng.Visible = false;
            }
            Validar();
        }

        private void CheckMod_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMod.CheckState == CheckState.Checked)
            {
                CheckIng.CheckState = CheckState.Unchecked;
                CheckEli.CheckState = CheckState.Unchecked;
                label7.Text = "Actualizar Comuna:";
                TextIngMod.Enabled = true;
                ComboIngModReg.Enabled = true;
                ComboIngModPro.Enabled = true;
                ButMod.Visible = true;
            }
            else
            {
                label7.Text = "";
                ButMod.Visible = false;
            }
            Validar();
        }

        private void CheckEli_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckEli.CheckState == CheckState.Checked)
            {
                CheckIng.CheckState = CheckState.Unchecked;
                CheckMod.CheckState = CheckState.Unchecked;
                label7.Text = "Eliminar Región:";
                TextIngMod.Enabled = false;
                ButEli.Visible = true;
                ButEli.Enabled = true;
            }
            else
            {
                label7.Text = "";
                ButEli.Visible = false;
                ButEli.Enabled = false;
            }
        }

        private void ComboIngModReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdPro = Convert.ToInt32(ComboIngModReg.SelectedValue);
            DataTable dt = NegPro.Filtrar(IdPro);
            ComboIngModPro.DisplayMember = "Nombre";
            ComboIngModPro.ValueMember = "IdPro";
            ComboIngModPro.DataSource = dt;
        }

        private void ComboIngModPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void TextIngMod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten letras.", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextIngMod_TextChanged(object sender, EventArgs e)
        {
            TextIngMod.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TextIngMod.Text);
            TextIngMod.SelectionStart = TextIngMod.Text.Length;
            Validar();
        }

        private void ButIng_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Esta seguro de la acción a realizar?", "Sistema.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            string Mensaje = string.Empty;
            Ent.Nombre = TextIngMod.Text;
            Ent.IdPro = Convert.ToInt32(ComboIngModPro.SelectedValue);
            if (res == DialogResult.Yes)
            {
                Respuesta<bool> resultado = NLocCom.Ingresar(Ent);

                if (resultado.estado)
                {
                    MessageBox.Show("Ingreso fue realizado correctamente", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ButLim.PerformClick();
                }
                else
                {
                    MessageBox.Show("Seleccione un registro valido", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ButMod_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Esta seguro de la acción a realizar?", "Sistema.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            string Mensaje = string.Empty;
            Ent.IdCom = Convert.ToInt32(textId.Text);
            Ent.Nombre = TextIngMod.Text;
            Ent.IdPro = Convert.ToInt32(ComboIngModPro.SelectedValue);
            if (res == DialogResult.Yes)
            {
                Respuesta<bool> resultado = NLocCom.Actualizar(Ent);
                if (resultado.estado)
                {
                    MessageBox.Show("Actualización fue realizado correctamente", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ButLim.PerformClick();
                }
                else
                {
                    MessageBox.Show("Seleccione un registro valido", "Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ButEli_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("¿Está seguro de la acción a realizar?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                int Id;
                if (int.TryParse(textId.Text, out Id))
                {
                    Respuesta<bool> resultado = NLocCom.Eliminar(Id);
                    if (resultado.estado)
                    {
                        MessageBox.Show("La eliminación se realizó correctamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ButLim.PerformClick(); // Assuming this clears the form or refreshes the data grid
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

        private void ButLim_Click(object sender, EventArgs e)
        {
            textId.Clear();
            TextIngMod.Clear();
            textConCom.Clear();
            textConPro.Clear();
            TextIngMod.Enabled = false;
            ComboIngModPro.Enabled = false;
            ResetGrid();
            Check_funciones();
            CarDat();
            Validar();
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
