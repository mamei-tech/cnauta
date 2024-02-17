namespace cnauta.view
{
    partial class VFormConfigs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VFormConfigs));
            this.buttonIncrement = new System.Windows.Forms.Button();
            this.labelIncrement = new System.Windows.Forms.Label();
            this.lbl_defaultUser = new System.Windows.Forms.Label();
            this.txb_defaultUser = new System.Windows.Forms.TextBox();
            this.txb_defaultUserPass = new System.Windows.Forms.TextBox();
            this.lbl_defaultPass = new System.Windows.Forms.Label();
            this.btn_ConfigSave = new System.Windows.Forms.Button();
            this.btn_configCancel = new System.Windows.Forms.Button();
            this.pn_CfgDefaultAccoun = new System.Windows.Forms.Panel();
            this.btn_revealDefaultPass = new System.Windows.Forms.Button();
            this.lbl_panDefaultUser = new System.Windows.Forms.Label();
            this.lbl_panAlternativeAUser = new System.Windows.Forms.Label();
            this.pn_CfgAlternativeAccoun = new System.Windows.Forms.Panel();
            this.btn_revealAltAPass = new System.Windows.Forms.Button();
            this.txb_alternativeAUser = new System.Windows.Forms.TextBox();
            this.txb_alternativeAUserPass = new System.Windows.Forms.TextBox();
            this.lbl_alternativeAUser = new System.Windows.Forms.Label();
            this.lbl_alternativeAPass = new System.Windows.Forms.Label();
            this.lbl_panAlternativeBUser = new System.Windows.Forms.Label();
            this.pn_CfgAlternativeBAccoun = new System.Windows.Forms.Panel();
            this.btn_revealAltBPass = new System.Windows.Forms.Button();
            this.txb_alternativeBUser = new System.Windows.Forms.TextBox();
            this.txb_alternativeBUserPass = new System.Windows.Forms.TextBox();
            this.lbl_alternativeBUser = new System.Windows.Forms.Label();
            this.lbl_alternativeBPass = new System.Windows.Forms.Label();
            this.pn_CfgDefaultAccoun.SuspendLayout();
            this.pn_CfgAlternativeAccoun.SuspendLayout();
            this.pn_CfgAlternativeBAccoun.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonIncrement
            // 
            this.buttonIncrement.Location = new System.Drawing.Point(68, 159);
            this.buttonIncrement.Name = "buttonIncrement";
            this.buttonIncrement.Size = new System.Drawing.Size(66, 30);
            this.buttonIncrement.TabIndex = 0;
            this.buttonIncrement.Text = "Increment";
            this.buttonIncrement.UseVisualStyleBackColor = true;
            this.buttonIncrement.Click += new System.EventHandler(this.buttonIncrement_Click);
            // 
            // labelIncrement
            // 
            this.labelIncrement.Location = new System.Drawing.Point(29, 168);
            this.labelIncrement.Name = "labelIncrement";
            this.labelIncrement.Size = new System.Drawing.Size(33, 16);
            this.labelIncrement.TabIndex = 1;
            this.labelIncrement.Text = "0";
            // 
            // lbl_defaultUser
            // 
            this.lbl_defaultUser.Location = new System.Drawing.Point(12, 31);
            this.lbl_defaultUser.Name = "lbl_defaultUser";
            this.lbl_defaultUser.Size = new System.Drawing.Size(53, 18);
            this.lbl_defaultUser.TabIndex = 2;
            this.lbl_defaultUser.Text = "user";
            // 
            // txb_defaultUser
            // 
            this.txb_defaultUser.Location = new System.Drawing.Point(47, 28);
            this.txb_defaultUser.MaxLength = 26;
            this.txb_defaultUser.Name = "txb_defaultUser";
            this.txb_defaultUser.Size = new System.Drawing.Size(117, 20);
            this.txb_defaultUser.TabIndex = 3;
            // 
            // txb_defaultUserPass
            // 
            this.txb_defaultUserPass.Location = new System.Drawing.Point(47, 54);
            this.txb_defaultUserPass.MaxLength = 16;
            this.txb_defaultUserPass.Name = "txb_defaultUserPass";
            this.txb_defaultUserPass.PasswordChar = '*';
            this.txb_defaultUserPass.Size = new System.Drawing.Size(117, 20);
            this.txb_defaultUserPass.TabIndex = 4;
            // 
            // lbl_defaultPass
            // 
            this.lbl_defaultPass.Location = new System.Drawing.Point(12, 57);
            this.lbl_defaultPass.Name = "lbl_defaultPass";
            this.lbl_defaultPass.Size = new System.Drawing.Size(53, 18);
            this.lbl_defaultPass.TabIndex = 5;
            this.lbl_defaultPass.Text = "pass";
            // 
            // btn_ConfigSave
            // 
            this.btn_ConfigSave.Location = new System.Drawing.Point(12, 273);
            this.btn_ConfigSave.Name = "btn_ConfigSave";
            this.btn_ConfigSave.Size = new System.Drawing.Size(75, 23);
            this.btn_ConfigSave.TabIndex = 6;
            this.btn_ConfigSave.Text = "✔ Save";
            this.btn_ConfigSave.UseVisualStyleBackColor = true;
            this.btn_ConfigSave.Click += new System.EventHandler(this.btn_ConfigSave_Click);
            // 
            // btn_configCancel
            // 
            this.btn_configCancel.Location = new System.Drawing.Point(101, 273);
            this.btn_configCancel.Name = "btn_configCancel";
            this.btn_configCancel.Size = new System.Drawing.Size(75, 23);
            this.btn_configCancel.TabIndex = 7;
            this.btn_configCancel.Text = "❌ Cancel";
            this.btn_configCancel.UseVisualStyleBackColor = true;
            // 
            // pn_CfgDefaultAccoun
            // 
            this.pn_CfgDefaultAccoun.AccessibleName = "";
            this.pn_CfgDefaultAccoun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_CfgDefaultAccoun.Controls.Add(this.btn_revealDefaultPass);
            this.pn_CfgDefaultAccoun.Controls.Add(this.txb_defaultUser);
            this.pn_CfgDefaultAccoun.Controls.Add(this.txb_defaultUserPass);
            this.pn_CfgDefaultAccoun.Controls.Add(this.lbl_defaultUser);
            this.pn_CfgDefaultAccoun.Controls.Add(this.lbl_defaultPass);
            this.pn_CfgDefaultAccoun.Location = new System.Drawing.Point(20, 23);
            this.pn_CfgDefaultAccoun.Name = "pn_CfgDefaultAccoun";
            this.pn_CfgDefaultAccoun.Size = new System.Drawing.Size(208, 100);
            this.pn_CfgDefaultAccoun.TabIndex = 8;
            this.pn_CfgDefaultAccoun.Tag = "";
            // 
            // btn_revealDefaultPass
            // 
            this.btn_revealDefaultPass.Location = new System.Drawing.Point(170, 52);
            this.btn_revealDefaultPass.Name = "btn_revealDefaultPass";
            this.btn_revealDefaultPass.Size = new System.Drawing.Size(24, 24);
            this.btn_revealDefaultPass.TabIndex = 10;
            this.btn_revealDefaultPass.Text = "👁";
            this.btn_revealDefaultPass.UseVisualStyleBackColor = true;
            this.btn_revealDefaultPass.Click += new System.EventHandler(this.btn_revealDefaultPass_Click);
            // 
            // lbl_panDefaultUser
            // 
            this.lbl_panDefaultUser.AutoSize = true;
            this.lbl_panDefaultUser.Location = new System.Drawing.Point(36, 16);
            this.lbl_panDefaultUser.Name = "lbl_panDefaultUser";
            this.lbl_panDefaultUser.Size = new System.Drawing.Size(66, 13);
            this.lbl_panDefaultUser.TabIndex = 9;
            this.lbl_panDefaultUser.Text = "Default User";
            // 
            // lbl_panAlternativeAUser
            // 
            this.lbl_panAlternativeAUser.AutoSize = true;
            this.lbl_panAlternativeAUser.Location = new System.Drawing.Point(255, 16);
            this.lbl_panAlternativeAUser.Name = "lbl_panAlternativeAUser";
            this.lbl_panAlternativeAUser.Size = new System.Drawing.Size(39, 13);
            this.lbl_panAlternativeAUser.TabIndex = 11;
            this.lbl_panAlternativeAUser.Text = "A User";
            // 
            // pn_CfgAlternativeAccoun
            // 
            this.pn_CfgAlternativeAccoun.AccessibleName = "";
            this.pn_CfgAlternativeAccoun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_CfgAlternativeAccoun.Controls.Add(this.btn_revealAltAPass);
            this.pn_CfgAlternativeAccoun.Controls.Add(this.txb_alternativeAUser);
            this.pn_CfgAlternativeAccoun.Controls.Add(this.txb_alternativeAUserPass);
            this.pn_CfgAlternativeAccoun.Controls.Add(this.lbl_alternativeAUser);
            this.pn_CfgAlternativeAccoun.Controls.Add(this.lbl_alternativeAPass);
            this.pn_CfgAlternativeAccoun.Location = new System.Drawing.Point(237, 23);
            this.pn_CfgAlternativeAccoun.Name = "pn_CfgAlternativeAccoun";
            this.pn_CfgAlternativeAccoun.Size = new System.Drawing.Size(208, 100);
            this.pn_CfgAlternativeAccoun.TabIndex = 10;
            this.pn_CfgAlternativeAccoun.Tag = "";
            // 
            // btn_revealAltAPass
            // 
            this.btn_revealAltAPass.Location = new System.Drawing.Point(170, 52);
            this.btn_revealAltAPass.Name = "btn_revealAltAPass";
            this.btn_revealAltAPass.Size = new System.Drawing.Size(24, 24);
            this.btn_revealAltAPass.TabIndex = 10;
            this.btn_revealAltAPass.Text = "👁";
            this.btn_revealAltAPass.UseVisualStyleBackColor = true;
            this.btn_revealAltAPass.Click += new System.EventHandler(this.btn_revealAltAPass_Click);
            // 
            // txb_alternativeAUser
            // 
            this.txb_alternativeAUser.Location = new System.Drawing.Point(47, 28);
            this.txb_alternativeAUser.MaxLength = 26;
            this.txb_alternativeAUser.Name = "txb_alternativeAUser";
            this.txb_alternativeAUser.Size = new System.Drawing.Size(117, 20);
            this.txb_alternativeAUser.TabIndex = 3;
            // 
            // txb_alternativeAUserPass
            // 
            this.txb_alternativeAUserPass.Location = new System.Drawing.Point(47, 54);
            this.txb_alternativeAUserPass.MaxLength = 16;
            this.txb_alternativeAUserPass.Name = "txb_alternativeAUserPass";
            this.txb_alternativeAUserPass.PasswordChar = '*';
            this.txb_alternativeAUserPass.Size = new System.Drawing.Size(117, 20);
            this.txb_alternativeAUserPass.TabIndex = 4;
            // 
            // lbl_alternativeAUser
            // 
            this.lbl_alternativeAUser.Location = new System.Drawing.Point(12, 31);
            this.lbl_alternativeAUser.Name = "lbl_alternativeAUser";
            this.lbl_alternativeAUser.Size = new System.Drawing.Size(53, 18);
            this.lbl_alternativeAUser.TabIndex = 2;
            this.lbl_alternativeAUser.Text = "user";
            // 
            // lbl_alternativeAPass
            // 
            this.lbl_alternativeAPass.Location = new System.Drawing.Point(12, 57);
            this.lbl_alternativeAPass.Name = "lbl_alternativeAPass";
            this.lbl_alternativeAPass.Size = new System.Drawing.Size(53, 18);
            this.lbl_alternativeAPass.TabIndex = 5;
            this.lbl_alternativeAPass.Text = "pass";
            // 
            // lbl_panAlternativeBUser
            // 
            this.lbl_panAlternativeBUser.AutoSize = true;
            this.lbl_panAlternativeBUser.Location = new System.Drawing.Point(471, 16);
            this.lbl_panAlternativeBUser.Name = "lbl_panAlternativeBUser";
            this.lbl_panAlternativeBUser.Size = new System.Drawing.Size(39, 13);
            this.lbl_panAlternativeBUser.TabIndex = 13;
            this.lbl_panAlternativeBUser.Text = "B User";
            // 
            // pn_CfgAlternativeBAccoun
            // 
            this.pn_CfgAlternativeBAccoun.AccessibleName = "";
            this.pn_CfgAlternativeBAccoun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_CfgAlternativeBAccoun.Controls.Add(this.btn_revealAltBPass);
            this.pn_CfgAlternativeBAccoun.Controls.Add(this.txb_alternativeBUser);
            this.pn_CfgAlternativeBAccoun.Controls.Add(this.txb_alternativeBUserPass);
            this.pn_CfgAlternativeBAccoun.Controls.Add(this.lbl_alternativeBUser);
            this.pn_CfgAlternativeBAccoun.Controls.Add(this.lbl_alternativeBPass);
            this.pn_CfgAlternativeBAccoun.Location = new System.Drawing.Point(454, 23);
            this.pn_CfgAlternativeBAccoun.Name = "pn_CfgAlternativeBAccoun";
            this.pn_CfgAlternativeBAccoun.Size = new System.Drawing.Size(208, 100);
            this.pn_CfgAlternativeBAccoun.TabIndex = 12;
            this.pn_CfgAlternativeBAccoun.Tag = "";
            // 
            // btn_revealAltBPass
            // 
            this.btn_revealAltBPass.Location = new System.Drawing.Point(170, 52);
            this.btn_revealAltBPass.Name = "btn_revealAltBPass";
            this.btn_revealAltBPass.Size = new System.Drawing.Size(24, 24);
            this.btn_revealAltBPass.TabIndex = 10;
            this.btn_revealAltBPass.Text = "👁";
            this.btn_revealAltBPass.UseVisualStyleBackColor = true;
            this.btn_revealAltBPass.Click += new System.EventHandler(this.btn_revealAltBPass_Click);
            // 
            // txb_alternativeBUser
            // 
            this.txb_alternativeBUser.Location = new System.Drawing.Point(47, 28);
            this.txb_alternativeBUser.MaxLength = 26;
            this.txb_alternativeBUser.Name = "txb_alternativeBUser";
            this.txb_alternativeBUser.Size = new System.Drawing.Size(117, 20);
            this.txb_alternativeBUser.TabIndex = 3;
            // 
            // txb_alternativeBUserPass
            // 
            this.txb_alternativeBUserPass.Location = new System.Drawing.Point(47, 54);
            this.txb_alternativeBUserPass.MaxLength = 16;
            this.txb_alternativeBUserPass.Name = "txb_alternativeBUserPass";
            this.txb_alternativeBUserPass.PasswordChar = '*';
            this.txb_alternativeBUserPass.Size = new System.Drawing.Size(117, 20);
            this.txb_alternativeBUserPass.TabIndex = 4;
            // 
            // lbl_alternativeBUser
            // 
            this.lbl_alternativeBUser.Location = new System.Drawing.Point(12, 31);
            this.lbl_alternativeBUser.Name = "lbl_alternativeBUser";
            this.lbl_alternativeBUser.Size = new System.Drawing.Size(53, 18);
            this.lbl_alternativeBUser.TabIndex = 2;
            this.lbl_alternativeBUser.Text = "user";
            // 
            // lbl_alternativeBPass
            // 
            this.lbl_alternativeBPass.Location = new System.Drawing.Point(12, 57);
            this.lbl_alternativeBPass.Name = "lbl_alternativeBPass";
            this.lbl_alternativeBPass.Size = new System.Drawing.Size(53, 18);
            this.lbl_alternativeBPass.TabIndex = 5;
            this.lbl_alternativeBPass.Text = "pass";
            // 
            // VFormConfigs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 308);
            this.Controls.Add(this.lbl_panAlternativeBUser);
            this.Controls.Add(this.pn_CfgAlternativeBAccoun);
            this.Controls.Add(this.lbl_panAlternativeAUser);
            this.Controls.Add(this.pn_CfgAlternativeAccoun);
            this.Controls.Add(this.btn_configCancel);
            this.Controls.Add(this.lbl_panDefaultUser);
            this.Controls.Add(this.btn_ConfigSave);
            this.Controls.Add(this.labelIncrement);
            this.Controls.Add(this.buttonIncrement);
            this.Controls.Add(this.pn_CfgDefaultAccoun);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VFormConfigs";
            this.Text = "CNauta | Settings";
            this.pn_CfgDefaultAccoun.ResumeLayout(false);
            this.pn_CfgDefaultAccoun.PerformLayout();
            this.pn_CfgAlternativeAccoun.ResumeLayout(false);
            this.pn_CfgAlternativeAccoun.PerformLayout();
            this.pn_CfgAlternativeBAccoun.ResumeLayout(false);
            this.pn_CfgAlternativeBAccoun.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txb_defaultUser;
        private System.Windows.Forms.Label lbl_defaultUser;
        private System.Windows.Forms.TextBox txb_defaultUserPass;
        private System.Windows.Forms.Label lbl_defaultPass;

        private System.Windows.Forms.Button buttonIncrement;
        private System.Windows.Forms.Label labelIncrement;

        #endregion

        private System.Windows.Forms.Button btn_ConfigSave;
        private System.Windows.Forms.Button btn_configCancel;
        private System.Windows.Forms.Panel pn_CfgDefaultAccoun;
        private System.Windows.Forms.Label lbl_panDefaultUser;
        private System.Windows.Forms.Button btn_revealDefaultPass;
        private System.Windows.Forms.Label lbl_panAlternativeAUser;
        private System.Windows.Forms.Panel pn_CfgAlternativeAccoun;
        private System.Windows.Forms.TextBox txb_alternativeAUser;
        private System.Windows.Forms.TextBox txb_alternativeAUserPass;
        private System.Windows.Forms.Label lbl_alternativeAUser;
        private System.Windows.Forms.Label lbl_alternativeAPass;
        private System.Windows.Forms.Label lbl_panAlternativeBUser;
        private System.Windows.Forms.Panel pn_CfgAlternativeBAccoun;
        private System.Windows.Forms.Button btn_revealAltBPass;
        private System.Windows.Forms.TextBox txb_alternativeBUser;
        private System.Windows.Forms.TextBox txb_alternativeBUserPass;
        private System.Windows.Forms.Label lbl_alternativeBUser;
        private System.Windows.Forms.Label lbl_alternativeBPass;
        private System.Windows.Forms.Button btn_revealAltAPass;
    }
}