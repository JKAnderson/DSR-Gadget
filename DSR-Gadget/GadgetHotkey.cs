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
        private Action hotkeyAction;

        public VirtualKey Key;

        public GadgetHotkey(string setSettingsName, string labelText, FlowLayoutPanel flowLayoutPanel, Action setAction)
        {
            settingsName = setSettingsName;
            FlowLayoutPanel container = new FlowLayoutPanel() { AutoSize = true };
            hotkeyTextBox = new TextBox() { Width = 80, Margin = new Padding(0, 0, 20, 5) };
            Label hotkeyLabel = new Label() { Text = labelText };
            hotkeyAction = setAction;

            Key = (VirtualKey)(int)Properties.Settings.Default[settingsName];
            if (Key == VirtualKey.Escape)
                hotkeyTextBox.Text = "Unbound";
            else
                hotkeyTextBox.Text = Key.ToString();
            hotkeyTextBox.Enter += new EventHandler(enter);
            hotkeyTextBox.Leave += new EventHandler(leave);
            hotkeyTextBox.KeyUp += new KeyEventHandler(keyUp);
            container.Controls.Add(hotkeyTextBox);
            container.Controls.Add(hotkeyLabel);
            flowLayoutPanel.Controls.Add(container);
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
            var selectedTextBox = sender as TextBox;
            Key = (VirtualKey)e.KeyValue;
            if (Key == VirtualKey.Escape)
                hotkeyTextBox.Text = "Unbound";
            else
                hotkeyTextBox.Text = Key.ToString();
            e.Handled = true;
            selectedTextBox.Parent.Focus();
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
