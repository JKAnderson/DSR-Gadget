using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private bool syncing = false;

        private void initGraphics()
        {
            nudBrightnessR.Value = settings.FilterBrightnessR;
            nudBrightnessG.Value = settings.FilterBrightnessG;
            nudBrightnessB.Value = settings.FilterBrightnessB;
            cbxBrightnessSync.Checked = settings.FilterBrightnessSync;
            nudContrastR.Value = settings.FilterContrastR;
            nudContrastG.Value = settings.FilterContrastG;
            nudContrastB.Value = settings.FilterContrastB;
            cbxContrastSync.Checked = settings.FilterContrastSync;
            nudSaturation.Value = settings.FilterSaturation;
            nudHue.Value = settings.FilterHue;
            cbxFilter.Checked = settings.FilterOverride;
        }

        private void saveGraphics()
        {
            settings.FilterOverride = cbxFilter.Checked;
            settings.FilterBrightnessSync = cbxBrightnessSync.Checked;
            settings.FilterBrightnessR = nudBrightnessR.Value;
            settings.FilterBrightnessG = nudBrightnessG.Value;
            settings.FilterBrightnessB = nudBrightnessB.Value;
            settings.FilterContrastSync = cbxContrastSync.Checked;
            settings.FilterContrastR = nudContrastR.Value;
            settings.FilterContrastG = nudContrastG.Value;
            settings.FilterContrastB = nudContrastB.Value;
            settings.FilterSaturation = nudSaturation.Value;
            settings.FilterHue = nudHue.Value;
        }

        private void resetGraphics()
        {
            if (!cbxDrawMap.Checked)
                Hook.DrawMap = true;
            if (!cbxDrawObjects.Checked)
                Hook.DrawObjects = true;
            if (!cbxDrawCharacters.Checked)
                Hook.DrawCharacters = true;
            if (!cbxDrawSFX.Checked)
                Hook.DrawSFX = true;
            if (!cbxDrawCutscenes.Checked)
                Hook.DrawCutscenes = true;

            if (loaded && cbxFilter.Checked)
                Hook.FilterOverride = false;
        }

        private void reloadGraphics()
        {
            if (!cbxDrawMap.Checked)
                Hook.DrawMap = false;
            if (!cbxDrawObjects.Checked)
                Hook.DrawObjects = false;
            if (!cbxDrawCharacters.Checked)
                Hook.DrawCharacters = false;
            if (!cbxDrawSFX.Checked)
                Hook.DrawSFX = false;
            if (!cbxDrawCutscenes.Checked)
                Hook.DrawCutscenes = false;

            if (cbxFilter.Checked)
            {
                Hook.FilterOverride = true;
                setFilterValues();
            }
        }

        private void updateGraphics() { }

        private void cbxDrawMap_CheckedChanged(object sender, EventArgs e)
        {
            Hook.DrawMap = cbxDrawMap.Checked;
        }

        private void cbxDrawObjects_CheckedChanged(object sender, EventArgs e)
        {
            Hook.DrawObjects = cbxDrawObjects.Checked;
        }

        private void cbxDrawCharacters_CheckedChanged(object sender, EventArgs e)
        {
            Hook.DrawCharacters = cbxDrawCharacters.Checked;
        }

        private void cbxDrawSFX_CheckedChanged(object sender, EventArgs e)
        {
            Hook.DrawSFX = cbxDrawSFX.Checked;
        }

        private void cbxDrawCutscenes_CheckedChanged(object sender, EventArgs e)
        {
            Hook.DrawCutscenes = cbxDrawCutscenes.Checked;
        }

        private void cbxFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (cbxFilter.Checked)
                    setFilterValues();
                Hook.FilterOverride = cbxFilter.Checked;
            }
        }

        private void cbxBrightnessSync_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxBrightnessSync.Checked)
            {
                nudBrightnessG.Enabled = false;
                nudBrightnessG.Value = nudBrightnessR.Value;
                nudBrightnessB.Enabled = false;
                nudBrightnessB.Value = nudBrightnessR.Value;
            }
            else
            {
                nudBrightnessG.Enabled = true;
                nudBrightnessB.Enabled = true;
            }
        }

        private void cbxContrastSync_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxContrastSync.Checked)
            {
                nudContrastG.Enabled = false;
                nudContrastG.Value = nudContrastR.Value;
                nudContrastB.Enabled = false;
                nudContrastB.Value = nudContrastR.Value;
            }
            else
            {
                nudContrastG.Enabled = true;
                nudContrastB.Enabled = true;
            }
        }

        private void nudBrightnessR_ValueChanged(object sender, EventArgs e)
        {
            if (cbxBrightnessSync.Checked)
            {
                syncing = true;
                nudBrightnessG.Value = nudBrightnessR.Value;
                nudBrightnessB.Value = nudBrightnessR.Value;
                syncing = false;
            }
            setFilterValues();
        }

        private void nudContrastR_ValueChanged(object sender, EventArgs e)
        {
            if (cbxContrastSync.Checked)
            {
                syncing = true;
                nudContrastG.Value = nudContrastR.Value;
                nudContrastB.Value = nudContrastR.Value;
                syncing = false;
            }
            setFilterValues();
        }

        private void nudFilter_ValueChanged(object sender, EventArgs e)
        {
            if (!syncing)
                setFilterValues();
        }

        private void setFilterValues()
        {
            if (loaded && cbxFilter.Checked)
            {
                float brightR = (float)nudBrightnessR.Value;
                float brightG = (float)nudBrightnessG.Value;
                float brightB = (float)nudBrightnessB.Value;
                float contR = (float)nudContrastR.Value;
                float contG = (float)nudContrastG.Value;
                float contB = (float)nudContrastB.Value;
                float saturation = (float)nudSaturation.Value;
                float hue = (float)nudHue.Value;
                Hook.SetFilterValues(brightR, brightG, brightB, contR, contG, contB, saturation, hue);
            }
        }
    }
}
