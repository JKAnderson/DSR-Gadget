using LowLevelHooking;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private GlobalKeyboardHook keyboardHook = new GlobalKeyboardHook();
        private List<GadgetHotkey> hotkeys = new List<GadgetHotkey>();

        private void initHotkeys()
        {
            cbxHotkeysEnable.Checked = settings.HotkeysEnable;
            cbxHotkeysHandle.Checked = settings.HotkeysHandle;

            hotkeys.Add(new GadgetHotkey("HotkeyGravity", txtHotkeyGravity, tpgHotkeys, () =>
            {
                cbxGravity.Checked = !cbxGravity.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyCollision", txtHotkeyCollision, tpgHotkeys, () =>
            {
                cbxCollision.Checked = !cbxCollision.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeySpeed", txtHotkeySpeed, tpgHotkeys, () =>
            {
                cbxSpeed.Checked = !cbxSpeed.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyStore", txtHotkeyStore, tpgHotkeys, () =>
            {
                storePosition();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyRestore", txtHotkeyRestore, tpgHotkeys, () =>
            {
                restorePosition();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyUp", txtHotkeyUp, tpgHotkeys, () =>
            {
                dsrProcess.GetPosition(out float x, out float y, out float z, out float angle);
                dsrProcess.PosWarp(x, y + 10, z, angle);
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyDown", txtHotkeyDown, tpgHotkeys, () =>
            {
                dsrProcess.GetPosition(out float x, out float y, out float z, out float angle);
                dsrProcess.PosWarp(x, y - 10, z, angle);
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyMenu", txtHotkeyMenu, tpgHotkeys, () =>
            {
                dsrProcess.MenuKick();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyFilter", txtHotkeyFilter, tpgHotkeys, () =>
            {
                cbxFilter.Checked = !cbxFilter.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyDeadMode", txtHotkeyDeadMode, tpgHotkeys, () =>
            {
                cbxPlayerDeadMode.Checked = !cbxPlayerDeadMode.Checked;
            }));

            /*
            hotkeys.Add(new GadgetHotkey("Hotkey", txtHotkey, tpgHotkeys, () =>
            {
                
            }));
            */

#if DEBUG
            hotkeys.Add(new GadgetHotkey("HotkeyTest1", txtHotkeyTest1, tpgHotkeys, () =>
            {
                dsrProcess.HotkeyTest1();
            }));
            hotkeys.Add(new GadgetHotkey("HotkeyTest2", txtHotkeyTest2, tpgHotkeys, () =>
            {
                dsrProcess.HotkeyTest2();
            }));
#else
            txtHotkeyTest1.Visible = false;
            lblHotkeyTest1.Visible = false;
            txtHotkeyTest2.Visible = false;
            lblHotkeyTest2.Visible = false;
#endif

            keyboardHook.KeyDownOrUp += GlobalKeyboardHook_KeyDownOrUp;
        }

        private void saveHotkeys()
        {
            settings.HotkeysEnable = cbxHotkeysEnable.Checked;
            settings.HotkeysHandle = cbxHotkeysHandle.Checked;
            foreach (GadgetHotkey hotkey in hotkeys)
                hotkey.Save();
            keyboardHook.Dispose();
        }

        private void resetHotkeys() { }

        private void reloadHotkeys() { }

        private void updateHotkeys() { }

        private void GlobalKeyboardHook_KeyDownOrUp(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (cbxHotkeysEnable.Checked && loaded && dsrProcess.Focused() && !e.IsUp)
            {
                foreach (GadgetHotkey hotkey in hotkeys)
                {
                    if (hotkey.Trigger(e.KeyCode) && cbxHotkeysHandle.Checked)
                        e.Handled = true;
                }
            }
        }
    }
}
