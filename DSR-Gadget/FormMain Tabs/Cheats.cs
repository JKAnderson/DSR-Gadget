using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initCheats() { }

        private void saveCheats() { }

        private void resetCheats()
        {
            if (cbxAllNoMagicQty.Checked)
                dsrProcess.SetAllNoMagicQty(false);
            if (cbxPlayerNoDead.Checked)
                dsrProcess.SetPlayerNoDead(false);
            if (cbxPlayerExterminate.Checked)
                dsrProcess.SetPlayerExterminate(false);
            if (cbxAllNoStamina.Checked)
                dsrProcess.SetAllNoStamina(false);
            if (cbxAllNoArrow.Checked)
                dsrProcess.SetAllNoArrow(false);
            if (cbxPlayerHide.Checked)
                dsrProcess.SetPlayerHide(false);
            if (cbxPlayerSilence.Checked)
                dsrProcess.SetPlayerSilence(false);
            if (cbxAllNoDead.Checked)
                dsrProcess.SetAllNoDead(false);
            if (cbxAllNoDamage.Checked)
                dsrProcess.SetAllNoDamage(false);
            if (cbxAllNoHit.Checked)
                dsrProcess.SetAllNoHit(false);
            if (cbxAllNoAttack.Checked)
                dsrProcess.SetAllNoAttack(false);
            if (cbxAllNoMove.Checked)
                dsrProcess.SetAllNoMove(false);
            if (cbxAllNoUpdateAI.Checked)
                dsrProcess.SetAllNoUpdateAI(false);

            if (loaded)
            {
                if (cbxPlayerDeadMode.Checked)
                    dsrProcess.SetPlayerDeadMode(false);
                if (cbxPlayerDisableDamage.Checked)
                    dsrProcess.SetPlayerDisableDamage(false);
                if (cbxPlayerNoHit.Checked)
                    dsrProcess.SetPlayerNoHit(false);
                if (cbxPlayerNoStamina.Checked)
                    dsrProcess.SetPlayerNoStamina(false);
                if (cbxPlayerSuperArmor.Checked)
                    dsrProcess.SetPlayerSuperArmor(false);
                if (cbxPlayerNoGoods.Checked)
                    dsrProcess.SetPlayerNoGoods(false);
            }
        }

        private void reloadCheats()
        {
            if (cbxPlayerDeadMode.Checked)
                dsrProcess.SetPlayerDeadMode(true);
            if (cbxPlayerDisableDamage.Checked)
                dsrProcess.SetPlayerDisableDamage(true);
            if (cbxPlayerNoHit.Checked)
                dsrProcess.SetPlayerNoHit(true);
            if (cbxPlayerNoStamina.Checked)
                dsrProcess.SetPlayerNoStamina(true);
            if (cbxPlayerSuperArmor.Checked)
                dsrProcess.SetPlayerSuperArmor(true);
            if (cbxPlayerNoGoods.Checked)
                dsrProcess.SetPlayerNoGoods(true);
            if (cbxAllNoMagicQty.Checked)
                dsrProcess.SetAllNoMagicQty(true);
            if (cbxPlayerNoDead.Checked)
                dsrProcess.SetPlayerNoDead(true);
            if (cbxPlayerExterminate.Checked)
                dsrProcess.SetPlayerExterminate(true);
            if (cbxAllNoStamina.Checked)
                dsrProcess.SetAllNoStamina(true);
            if (cbxAllNoArrow.Checked)
                dsrProcess.SetAllNoArrow(true);
            if (cbxPlayerHide.Checked)
                dsrProcess.SetPlayerHide(true);
            if (cbxPlayerSilence.Checked)
                dsrProcess.SetPlayerSilence(true);
            if (cbxAllNoDead.Checked)
                dsrProcess.SetAllNoDead(true);
            if (cbxAllNoDamage.Checked)
                dsrProcess.SetAllNoDamage(true);
            if (cbxAllNoHit.Checked)
                dsrProcess.SetAllNoHit(true);
            if (cbxAllNoAttack.Checked)
                dsrProcess.SetAllNoAttack(true);
            if (cbxAllNoMove.Checked)
                dsrProcess.SetAllNoMove(true);
            if (cbxAllNoUpdateAI.Checked)
                dsrProcess.SetAllNoUpdateAI(true);
        }

        private void updateCheats()
        {
            if (cbxPlayerDeadMode.Checked && !dsrProcess.GetPlayerDeadMode())
                dsrProcess.SetPlayerDeadMode(true);
        }

        private void cbxPlayerDeadMode_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.SetPlayerDeadMode(cbxPlayerDeadMode.Checked);
        }

        private void cbxPlayerNoDead_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetPlayerNoDead(cbxPlayerNoDead.Checked);
        }

        private void cbxPlayerDisableDamage_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.SetPlayerDisableDamage(cbxPlayerDisableDamage.Checked);
        }

        private void cbxPlayerNoHit_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.SetPlayerNoHit(cbxPlayerNoHit.Checked);
        }

        private void cbxPlayerNoStamina_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetPlayerNoStamina(cbxPlayerNoStamina.Checked);
        }

        private void cbxPlayerSuperArmor_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.SetPlayerSuperArmor(cbxPlayerSuperArmor.Checked);
        }

        private void cbxPlayerHide_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetPlayerHide(cbxPlayerHide.Checked);
        }

        private void cbxPlayerSilence_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetPlayerSilence(cbxPlayerSilence.Checked);
        }

        private void cbxPlayerExterminate_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetPlayerExterminate(cbxPlayerExterminate.Checked);
        }

        private void cbxPlayerNoGoods_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                dsrProcess.SetPlayerNoGoods(cbxPlayerNoGoods.Checked);
        }

        private void cbxAllNoArrow_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoArrow(cbxAllNoArrow.Checked);
        }

        private void cbxAllNoMagicQty_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoMagicQty(cbxAllNoMagicQty.Checked);
        }

        private void cbxAllNoDead_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoDead(cbxAllNoDead.Checked);
        }

        private void cbxAllNoDamage_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoDamage(cbxAllNoDamage.Checked);
        }

        private void cbxAllNoHit_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoHit(cbxAllNoHit.Checked);
        }

        private void cbxAllNoStamina_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoStamina(cbxAllNoStamina.Checked);
        }

        private void cbxAllNoAttack_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoAttack(cbxAllNoAttack.Checked);
        }

        private void cbxAllNoMove_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoMove(cbxAllNoMove.Checked);
        }

        private void cbxAllNoUpdateAI_CheckedChanged(object sender, EventArgs e)
        {
            dsrProcess.SetAllNoUpdateAI(cbxAllNoUpdateAI.Checked);
        }
    }
}
