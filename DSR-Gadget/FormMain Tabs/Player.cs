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
                    dsrProcess.SetNoGravity(false);
                if (!cbxCollision.Checked)
                    dsrProcess.SetNoCollision(false);
                if (cbxSpeed.Checked)
                    dsrProcess.SetAnimSpeed(1);
            }
        }

        private void reloadPlayer()
        {
            if (!cbxGravity.Checked)
                dsrProcess.SetNoGravity(true);
            if (!cbxCollision.Checked)
                dsrProcess.SetNoCollision(true);
        }

        private void updatePlayer()
        {
            nudHealth.Value = dsrProcess.GetHealth();
            nudHealthMax.Value = dsrProcess.GetHealthMax();
            nudStamina.Value = dsrProcess.GetStamina();
            nudStaminaMax.Value = dsrProcess.GetStaminaMax();

            try
            {
                dsrProcess.GetPosition(out float x, out float y, out float z, out float angle);
                nudPosX.Value = (decimal)x;
                nudPosY.Value = (decimal)y;
                nudPosZ.Value = (decimal)z;
                nudPosAngle.Value = angleToDegree(angle);

                dsrProcess.GetStablePosition(out x, out y, out z, out angle);
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

            cbxDeathCam.Checked = dsrProcess.GetDeathCam();
            if (cbxSpeed.Checked)
                dsrProcess.SetAnimSpeed((float)nudSpeed.Value);

            int bonfireID = dsrProcess.GetLastBonfire();
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
                dsrProcess.SetHealth((int)nudHealth.Value);
        }

        private void nudStamina_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                dsrProcess.SetStamina((int)nudStamina.Value);
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
            playerState.FollowCam = dsrProcess.DumpFollowCam();
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
            dsrProcess.PosWarp(x, y, z, angle);

            if (playerState.Set)
            {
                // Two frames for safety, wait until after warp
                System.Threading.Thread.Sleep(1000 / 15);
                dsrProcess.UndumpFollowCam(playerState.FollowCam);

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
                dsrProcess.SetNoGravity(!cbxGravity.Checked);
        }

        private void cbxCollision_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.SetNoCollision(!cbxCollision.Checked);
        }

        private void cbxDeathCam_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                dsrProcess.SetDeathCam(cbxDeathCam.Checked);
        }

        private void cmbBonfire_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRBonfire bonfire = cmbBonfire.SelectedItem as DSRBonfire;
                dsrProcess.SetLastBonfire(bonfire.ID);
            }
        }

        private void btnWarp_Click(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.BonfireWarp();
        }

        private void cbxSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded && !cbxSpeed.Checked)
                dsrProcess.SetAnimSpeed(1);
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
