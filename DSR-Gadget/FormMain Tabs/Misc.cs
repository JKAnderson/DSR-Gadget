using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initMisc() { }

        private void saveMisc() { }

        private void resetMisc() { }

        private void reloadMisc() { }

        private void updateMisc() { }

        private void btnEventRead_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtEventFlag.Text, out int id))
                cbxEventFlag.Checked = Hook.ReadEventFlag(id);
        }

        private void btnEventWrite_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtEventFlag.Text, out int id))
                Hook.WriteEventFlag(id, cbxEventFlag.Checked);
        }
    }
}
