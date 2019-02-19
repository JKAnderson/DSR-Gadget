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

            hotkeys.Add(new GadgetHotkey("HotkeyGravity", "Toggle Gravity", flpHotkeyControls, () =>
            {
                cbxGravity.Checked = !cbxGravity.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyCollision", "Toggle Collision", flpHotkeyControls, () =>
            {
                cbxCollision.Checked = !cbxCollision.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeySpeed", "Toggle Speed", flpHotkeyControls, () =>
            {
                cbxSpeed.Checked = !cbxSpeed.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyStore", "Store Position", flpHotkeyControls, () =>
            {
                storePosition();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyRestore", "Restore Position", flpHotkeyControls, () =>
            {
                restorePosition();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyUp", "Move Up", flpHotkeyControls, () =>
            {
                Hook.GetPosition(out float x, out float y, out float z, out float angle);
                Hook.PosWarp(x, y + 10, z, angle);
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyDown", "Move Down", flpHotkeyControls, () =>
            {
                Hook.GetPosition(out float x, out float y, out float z, out float angle);
                Hook.PosWarp(x, y - 10, z, angle);
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyMenu", "Quit to Menu", flpHotkeyControls, () =>
            {
                Hook.MenuKick();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyFilter", "Toggle Filter", flpHotkeyControls, () =>
            {
                cbxFilter.Checked = !cbxFilter.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyDeadMode", "Toggle No Death", flpHotkeyControls, () =>
            {
                cbxPlayerDeadMode.Checked = !cbxPlayerDeadMode.Checked;
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyCreateItem", "Create Selected Item", flpHotkeyControls, () =>
            {
                createItem();
            }));

            /*
            hotkeys.Add(new GadgetHotkey("Hotkey", "", flpHotkeyControls, () =>
            {
                
            }));
            */

#if DEBUG
            hotkeys.Add(new GadgetHotkey("HotkeyTest1", "Test 1", flpHotkeyControls, () =>
            {
                Hook.HotkeyTest1();
            }));

            hotkeys.Add(new GadgetHotkey("HotkeyTest2", "Test 2", flpHotkeyControls, () =>
            {
                Hook.HotkeyTest2();
            }));
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
            if (cbxHotkeysEnable.Checked && loaded && Hook.Focused && !e.IsUp)
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
