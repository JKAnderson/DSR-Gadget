namespace DSR_Gadget
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tclMain = new System.Windows.Forms.TabControl();
            this.tpgPlayer = new System.Windows.Forms.TabPage();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblVersionValue = new System.Windows.Forms.Label();
            this.lblLoaded = new System.Windows.Forms.Label();
            this.lblLoadedValue = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cbxGravity = new System.Windows.Forms.CheckBox();
            this.tclMain.SuspendLayout();
            this.tpgPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tclMain
            // 
            this.tclMain.Controls.Add(this.tpgPlayer);
            this.tclMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tclMain.Location = new System.Drawing.Point(0, 46);
            this.tclMain.Name = "tclMain";
            this.tclMain.SelectedIndex = 0;
            this.tclMain.Size = new System.Drawing.Size(740, 460);
            this.tclMain.TabIndex = 0;
            // 
            // tpgPlayer
            // 
            this.tpgPlayer.Controls.Add(this.cbxGravity);
            this.tpgPlayer.Location = new System.Drawing.Point(4, 22);
            this.tpgPlayer.Name = "tpgPlayer";
            this.tpgPlayer.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlayer.Size = new System.Drawing.Size(732, 434);
            this.tpgPlayer.TabIndex = 1;
            this.tpgPlayer.Text = "Player";
            this.tpgPlayer.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(12, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(75, 13);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Game version:";
            // 
            // lblVersionValue
            // 
            this.lblVersionValue.AutoSize = true;
            this.lblVersionValue.Location = new System.Drawing.Point(93, 9);
            this.lblVersionValue.Name = "lblVersionValue";
            this.lblVersionValue.Size = new System.Drawing.Size(33, 13);
            this.lblVersionValue.TabIndex = 2;
            this.lblVersionValue.Text = "None";
            // 
            // lblLoaded
            // 
            this.lblLoaded.AutoSize = true;
            this.lblLoaded.Location = new System.Drawing.Point(12, 22);
            this.lblLoaded.Name = "lblLoaded";
            this.lblLoaded.Size = new System.Drawing.Size(73, 13);
            this.lblLoaded.TabIndex = 3;
            this.lblLoaded.Text = "Game loaded:";
            // 
            // lblLoadedValue
            // 
            this.lblLoadedValue.AutoSize = true;
            this.lblLoadedValue.Location = new System.Drawing.Point(93, 22);
            this.lblLoadedValue.Name = "lblLoadedValue";
            this.lblLoadedValue.Size = new System.Drawing.Size(21, 13);
            this.lblLoadedValue.TabIndex = 4;
            this.lblLoadedValue.Text = "No";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 16;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // cbxGravity
            // 
            this.cbxGravity.AutoSize = true;
            this.cbxGravity.Checked = true;
            this.cbxGravity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGravity.Location = new System.Drawing.Point(6, 6);
            this.cbxGravity.Name = "cbxGravity";
            this.cbxGravity.Size = new System.Drawing.Size(59, 17);
            this.cbxGravity.TabIndex = 0;
            this.cbxGravity.Text = "Gravity";
            this.cbxGravity.UseVisualStyleBackColor = true;
            this.cbxGravity.CheckedChanged += new System.EventHandler(this.cbxGravity_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 506);
            this.Controls.Add(this.lblLoadedValue);
            this.Controls.Add(this.lblLoaded);
            this.Controls.Add(this.lblVersionValue);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.tclMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "DSR Gadget <version>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tclMain.ResumeLayout(false);
            this.tpgPlayer.ResumeLayout(false);
            this.tpgPlayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tclMain;
        private System.Windows.Forms.TabPage tpgPlayer;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblVersionValue;
        private System.Windows.Forms.Label lblLoaded;
        private System.Windows.Forms.Label lblLoadedValue;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.CheckBox cbxGravity;
    }
}

