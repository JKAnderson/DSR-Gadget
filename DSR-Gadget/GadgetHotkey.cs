using LowLevelHooking;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSR_Gadget
{
    class GadgetHotkey
    {
        private string settingsName;
        private TextBox hotkeyTextBox;
        private TabPage hotkeyTabPage;
        private Action hotkeyAction;

        public VirtualKey Key;

        public GadgetHotkey(string setSettingsName, TextBox setTextBox, TabPage setTabPage, Action setAction)
        {
            settingsName = setSettingsName;
            hotkeyTextBox = setTextBox;
            hotkeyTabPage = setTabPage;
            hotkeyAction = setAction;

            Key = (VirtualKey)(int)Properties.Settings.Default[settingsName];
            if (Key == VirtualKey.Escape)
                hotkeyTextBox.Text = "Unbound";
            else
                hotkeyTextBox.Text = Key.ToString();
            hotkeyTextBox.Enter += new EventHandler(enter);
            hotkeyTextBox.Leave += new EventHandler(leave);
            hotkeyTextBox.KeyUp += new KeyEventHandler(keyUp);
        }

        private void enter(object sender, EventArgs e)
        {
            hotkeyTextBox.BackColor = Color.LightGreen;
        }

        private void leave(object sender, EventArgs e)
        {
            hotkeyTextBox.BackColor = SystemColors.Window;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            Key = (VirtualKey)e.KeyValue;
            if (Key == VirtualKey.Escape)
                hotkeyTextBox.Text = "Unbound";
            else
                hotkeyTextBox.Text = Key.ToString();
            e.Handled = true;
            hotkeyTabPage.Focus();
        }

        public bool Trigger(VirtualKey pressed)
        {
            bool result = false;
            if (Key != VirtualKey.Escape && pressed == Key)
            {
                hotkeyAction();
                result = true;
            }
            return result;
        }

        public void Save()
        {
            Properties.Settings.Default[settingsName] = (int)Key;
        }
    }
}
