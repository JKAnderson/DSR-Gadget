using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initItems()
        {
            foreach (DSRItemCategory category in DSRItemCategory.All)
                cmbCategory.Items.Add(category);
            cmbCategory.SelectedIndex = 0;
        }

        private void saveItems() { }

        private void resetItems() { }

        private void reloadItems() { }

        private void updateItems() { }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxItems.Items.Clear();
            DSRItemCategory category = cmbCategory.SelectedItem as DSRItemCategory;
            foreach (DSRItem item in category.Items)
                lbxItems.Items.Add(item);
            lbxItems.SelectedIndex = 0;
        }

        private void cmbInfusion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSRInfusion infusion = cmbInfusion.SelectedItem as DSRInfusion;
            nudUpgrade.Maximum = infusion.MaxUpgrade;
        }

        private void lbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSRItem item = lbxItems.SelectedItem as DSRItem;
            if (cbxRestrict.Checked)
            {
                if (item.StackLimit == 1)
                    nudQuantity.Enabled = false;
                else
                    nudQuantity.Enabled = true;
                nudQuantity.Maximum = item.StackLimit;
            }
            switch (item.UpgradeType)
            {
                case DSRItem.Upgrade.None:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Enabled = false;
                    nudUpgrade.Maximum = 0;
                    break;
                case DSRItem.Upgrade.Unique:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.Armor:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 10;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.Infusable:
                    cmbInfusion.Items.Clear();
                    foreach (DSRInfusion infusion in DSRInfusion.All)
                        cmbInfusion.Items.Add(infusion);
                    cmbInfusion.SelectedIndex = 0;
                    cmbInfusion.Enabled = true;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.InfusableRestricted:
                    cmbInfusion.Items.Clear();
                    foreach (DSRInfusion infusion in DSRInfusion.All)
                        if (!infusion.Restricted)
                            cmbInfusion.Items.Add(infusion);
                    cmbInfusion.SelectedIndex = 0;
                    cmbInfusion.Enabled = true;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.PyroFlame:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 15;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.PyroFlameAscended:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.Enabled = true;
                    break;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (loaded)
                createItem();
        }

        private void lbxItems_DoubleClick(object sender, EventArgs e)
        {
            if (loaded)
                createItem();
        }

        private void createItem()
        {
            DSRItemCategory category = cmbCategory.SelectedItem as DSRItemCategory;
            DSRItem item = lbxItems.SelectedItem as DSRItem;
            int id = item.ID;
            if (item.UpgradeType == DSRItem.Upgrade.PyroFlame || item.UpgradeType == DSRItem.Upgrade.PyroFlameAscended)
                id += (int)nudUpgrade.Value * 100;
            else
                id += (int)nudUpgrade.Value;
            if (item.UpgradeType == DSRItem.Upgrade.Infusable || item.UpgradeType == DSRItem.Upgrade.InfusableRestricted)
            {
                DSRInfusion infusion = cmbInfusion.SelectedItem as DSRInfusion;
                id += infusion.Value;
            }
            dsrProcess.GetItem(category.ID, id, (int)nudQuantity.Value);
        }

        private void cbxRestrict_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbxRestrict.Checked)
            {
                nudQuantity.Enabled = true;
                nudQuantity.Maximum = Int32.MaxValue;
            }
            else if (lbxItems.SelectedIndex != -1)
            {
                DSRItem item = lbxItems.SelectedItem as DSRItem;
                nudQuantity.Maximum = item.StackLimit;
                if (item.StackLimit == 1)
                    nudQuantity.Enabled = false;
            }
        }
    }
}
