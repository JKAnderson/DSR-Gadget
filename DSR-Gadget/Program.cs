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

            if (settings.AllowStatEditing  && !settings.Warned)
            {
                MessageBox.Show("DS1 had no anticheat whatsoever. DSR does. "
                    + "You currently have stat editing enabled.\n\n" 
                    + "THIS CAN GET YOU BANNNED!\n\n"
                    + "Do not use Gadget while online, and revert your save(s) after use "
                    + "If you get banned, it's not my fault.",
                    "WARNING",
                    MessageBoxButtons.Ok,
                    MessageBoxIcon.Exclamation
                );
                settings.Warned = true;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            settings.Save();
        }
    }
}
