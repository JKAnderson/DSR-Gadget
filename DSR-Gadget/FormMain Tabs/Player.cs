using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private struct PlayerState
        {
            public bool Set;
            public decimal Health, Stamina;
            public bool DeathCam;
            public byte[] FollowCam;
        }

        private List<int> unknownBonfires = new List<int>();
        private PlayerState playerState;

        private void initPlayer()
        {
            cbxRestoreState.Checked = settings.RestoreState;
            foreach (DSRBonfire bonfire in DSRBonfire.All)
                cmbBonfire.Items.Add(bonfire);
            cmbBonfire.SelectedIndex = 0;
            nudSpeed.Value = settings.AnimSpeed;
        }

        private void savePlayer()
        {
            settings.RestoreState = cbxRestoreState.Checked;
            settings.AnimSpeed = nudSpeed.Value;
        }

        private void resetPlayer()
        {
            if (loaded)
            {
                if (!cbxGravity.Checked)
                    Hook.NoGravity = false;
                if (!cbxCollision.Checked)
                    Hook.NoCollision = false;
                if (cbxSpeed.Checked)
                    Hook.AnimSpeed = 1;
            }
        }

        private void reloadPlayer()
        {
            if (!cbxGravity.Checked)
                Hook.NoGravity = true;
            if (!cbxCollision.Checked)
                Hook.NoCollision = true;
        }

        private void updatePlayer()
        {
            nudHealth.Value = Hook.Health;
            nudHealthMax.Value = Hook.HealthMax;
            nudStamina.Value = Hook.Stamina;
            nudStaminaMax.Value = Hook.StaminaMax;

            try
            {
                Hook.GetPosition(out float x, out float y, out float z, out float angle);
                nudPosX.Value = (decimal)x;
                nudPosY.Value = (decimal)y;
                nudPosZ.Value = (decimal)z;
                nudPosAngle.Value = angleToDegree(angle);

                Hook.GetStablePosition(out x, out y, out z, out angle);
                nudStableX.Value = (decimal)x;
                nudStableY.Value = (decimal)y;
                nudStableZ.Value = (decimal)z;
                nudStableAngle.Value = angleToDegree(angle);
            }
            catch (OverflowException)
            {
                nudPosX.Value = 0;
                nudPosY.Value = 0;
                nudPosZ.Value = 0;
                nudPosAngle.Value = 0;

                nudStableX.Value = 0;
                nudStableY.Value = 0;
                nudStableZ.Value = 0;
                nudStableAngle.Value = 0;
            }

            cbxDeathCam.Checked = Hook.DeathCam;
            if (cbxSpeed.Checked)
                Hook.AnimSpeed = (float)nudSpeed.Value;

            int bonfireID = Hook.LastBonfire;
            DSRBonfire lastBonfire = cmbBonfire.SelectedItem as DSRBonfire;
            if (!cmbBonfire.DroppedDown && bonfireID != lastBonfire.ID && !unknownBonfires.Contains(bonfireID))
            {
                DSRBonfire thisBonfire = null;
                foreach (object item in cmbBonfire.Items)
                {
                    DSRBonfire bonfire = item as DSRBonfire;
                    if (bonfireID == bonfire.ID)
                    {
                        thisBonfire = bonfire;
                        break;
                    }
                }

                if (thisBonfire == null)
                {
                    unknownBonfires.Add(bonfireID);
                    MessageBox.Show("Unknown bonfire ID, please report me: " + bonfireID, "Unknown Bonfire");
                }
                else
                    cmbBonfire.SelectedItem = thisBonfire;
            }
        }

        private void nudHealth_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.Health = (int)nudHealth.Value;
        }

        private void nudStamina_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.Stamina = (int)nudStamina.Value;
        }

        private void btnPosStore_Click(object sender, EventArgs e)
        {
            if (loaded)
                storePosition();
        }

        private void storePosition()
        {
            nudStoredX.Value = nudPosX.Value;
            nudStoredY.Value = nudPosY.Value;
            nudStoredZ.Value = nudPosZ.Value;
            nudStoredAngle.Value = nudPosAngle.Value;

            playerState.Health = nudHealth.Value;
            playerState.Stamina = nudStamina.Value;
            playerState.FollowCam = Hook.DumpFollowCam();
            playerState.DeathCam = cbxDeathCam.Checked;
            playerState.Set = true;
        }

        private void btnPosRestore_Click(object sender, EventArgs e)
        {
            if (loaded)
                restorePosition();
        }

        private void restorePosition()
        {
            float x = (float)nudStoredX.Value;
            float y = (float)nudStoredY.Value;
            float z = (float)nudStoredZ.Value;
            float angle = degreeToAngle(nudStoredAngle.Value);
            Hook.PosWarp(x, y, z, angle);

            if (playerState.Set)
            {
                // Two frames for safety, wait until after warp
                System.Threading.Thread.Sleep(1000 / 15);
                Hook.UndumpFollowCam(playerState.FollowCam);

                if (cbxRestoreState.Checked)
                {
                    nudHealth.Value = playerState.Health;
                    nudStamina.Value = playerState.Stamina;
                    cbxDeathCam.Checked = playerState.DeathCam;
                }
            }
        }

        private void cbxGravity_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.NoGravity = !cbxGravity.Checked;
        }

        private void cbxCollision_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.NoCollision = !cbxCollision.Checked;
        }

        private void cbxDeathCam_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.DeathCam = cbxDeathCam.Checked;
        }

        private void cmbBonfire_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRBonfire bonfire = cmbBonfire.SelectedItem as DSRBonfire;
                Hook.LastBonfire = bonfire.ID;
            }
        }

        private void btnWarp_Click(object sender, EventArgs e)
        {
            if (loaded)
                Hook.BonfireWarp();
        }

        private void cbxSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded && !cbxSpeed.Checked)
                Hook.AnimSpeed = 1;
        }

        private decimal angleToDegree(float angle)
        {
            return (decimal)((angle + Math.PI) / (Math.PI * 2) * 360);
        }

        private float degreeToAngle(decimal degree)
        {
            return (float)((double)degree / 360 * (Math.PI * 2) - Math.PI);
        }
    }
}
