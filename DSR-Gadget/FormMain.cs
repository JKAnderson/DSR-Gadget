using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private static Properties.Settings settings = Properties.Settings.Default;

        private DSRProcess dsrProcess = null;
        private bool loaded = false;
        private bool reading = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void enableTabs(bool enable)
        {
            foreach (TabPage tab in tclMain.TabPages)
                tab.Enabled = enable;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "DSR Gadget " + System.Windows.Forms.Application.ProductVersion;
            enableTabs(false);
            initializeAll();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveAll();
            resetAll();
            if (dsrProcess != null)
                dsrProcess.Close();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (dsrProcess == null)
            {
                DSRProcess result = DSRProcess.GetProcess();
                if (result != null)
                {
                    lblVersionValue.Text = result.Version;
                    lblVersionValue.ForeColor = result.Valid ? Color.DarkGreen : Color.DarkRed;
                    dsrProcess = result;
                }
            }
            else
            {
                if (dsrProcess.Alive())
                {
                    if (dsrProcess.Loaded())
                    {
                        if (!loaded)
                        {
                            lblLoadedValue.Text = "Yes";
                            reloadAll();
                            enableTabs(true);
                            loaded = true;
                        }
                        else
                        {
                            updateAll();
                        }
                    }
                    else if (loaded)
                    {
                        lblLoadedValue.Text = "No";
                        enableTabs(false);
                        loaded = false;
                    }
                }
                else
                {
                    dsrProcess.Close();
                    dsrProcess = null;
                    lblVersionValue.Text = "None";
                    lblVersionValue.ForeColor = Color.Black;
                    lblLoadedValue.Text = "No";
                    enableTabs(false);
                    loaded = false;
                }
            }
        }

        private void initializeAll()
        {

        }

        private void reloadAll()
        {
            reading = true;

            reading = false;
        }

        private void updateAll()
        {
            reading = true;

            reading = false;
        }

        private void saveAll()
        {

        }

        private void resetAll()
        {

        }

        private void cbxGravity_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess?.SetNoGravity(!cbxGravity.Checked);
        }
    }
}
