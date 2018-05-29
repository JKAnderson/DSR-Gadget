using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Properties.Settings settings = Properties.Settings.Default;
            if (settings.UpgradeRequired)
            {
                settings.Upgrade();
                settings.UpgradeRequired = false;
            }

            if (!settings.Warned)
            {
                MessageBox.Show("DS1 had no anticheat whatsoever. DSR does.\n\n"
                    + "Do not use Gadget while online, and revert your save after use "
                    + "if you do anything even remotely sketchy and plan to go online in the future.\n\n"
                    + "If you get bunned, it's not my fault.",
                    "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                settings.Warned = true;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            settings.Save();
        }
    }
}
