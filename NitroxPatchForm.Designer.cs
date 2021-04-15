
namespace NitroxPatch
{
    partial class NitroxPatchForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NitroxPatchForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.subnauticaPathGroupBox = new System.Windows.Forms.GroupBox();
            this.subnauticaPathLabel = new System.Windows.Forms.Label();
            this.subnauticaPathSelectButton = new System.Windows.Forms.Button();
            this.nitroxPathGroupBox = new System.Windows.Forms.GroupBox();
            this.nitroxPathLabel = new System.Windows.Forms.Label();
            this.nitroxPathSelectButton = new System.Windows.Forms.Button();
            this.setupButton = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.imitateSubnauticaCheckBox = new System.Windows.Forms.CheckBox();
            this.imitationPathGroupBox = new System.Windows.Forms.GroupBox();
            this.imitationPathLabel = new System.Windows.Forms.Label();
            this.imitationPathSelectButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.subnauticaPathGroupBox.SuspendLayout();
            this.nitroxPathGroupBox.SuspendLayout();
            this.imitationPathGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 46);
            this.panel1.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.closeButton.Location = new System.Drawing.Point(374, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(19, 20);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 30F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "NitroxPatch";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // subnauticaPathGroupBox
            // 
            this.subnauticaPathGroupBox.Controls.Add(this.subnauticaPathLabel);
            this.subnauticaPathGroupBox.Controls.Add(this.subnauticaPathSelectButton);
            this.subnauticaPathGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.subnauticaPathGroupBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.subnauticaPathGroupBox.Location = new System.Drawing.Point(0, 46);
            this.subnauticaPathGroupBox.Name = "subnauticaPathGroupBox";
            this.subnauticaPathGroupBox.Size = new System.Drawing.Size(394, 60);
            this.subnauticaPathGroupBox.TabIndex = 2;
            this.subnauticaPathGroupBox.TabStop = false;
            this.subnauticaPathGroupBox.Text = "Subnautica Path";
            // 
            // subnauticaPathLabel
            // 
            this.subnauticaPathLabel.AutoEllipsis = true;
            this.subnauticaPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subnauticaPathLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.subnauticaPathLabel.Location = new System.Drawing.Point(3, 25);
            this.subnauticaPathLabel.Name = "subnauticaPathLabel";
            this.subnauticaPathLabel.Size = new System.Drawing.Size(315, 32);
            this.subnauticaPathLabel.TabIndex = 0;
            this.subnauticaPathLabel.Text = "...";
            this.subnauticaPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subnauticaPathSelectButton
            // 
            this.subnauticaPathSelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.subnauticaPathSelectButton.Location = new System.Drawing.Point(318, 25);
            this.subnauticaPathSelectButton.Name = "subnauticaPathSelectButton";
            this.subnauticaPathSelectButton.Size = new System.Drawing.Size(73, 32);
            this.subnauticaPathSelectButton.TabIndex = 1;
            this.subnauticaPathSelectButton.Text = "Select";
            this.subnauticaPathSelectButton.UseVisualStyleBackColor = true;
            this.subnauticaPathSelectButton.Click += new System.EventHandler(this.subnauticaPathSelectButton_Click);
            // 
            // nitroxPathGroupBox
            // 
            this.nitroxPathGroupBox.Controls.Add(this.nitroxPathLabel);
            this.nitroxPathGroupBox.Controls.Add(this.nitroxPathSelectButton);
            this.nitroxPathGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nitroxPathGroupBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.nitroxPathGroupBox.Location = new System.Drawing.Point(0, 106);
            this.nitroxPathGroupBox.Name = "nitroxPathGroupBox";
            this.nitroxPathGroupBox.Size = new System.Drawing.Size(394, 61);
            this.nitroxPathGroupBox.TabIndex = 3;
            this.nitroxPathGroupBox.TabStop = false;
            this.nitroxPathGroupBox.Text = "Nitrox Path";
            // 
            // nitroxPathLabel
            // 
            this.nitroxPathLabel.AutoEllipsis = true;
            this.nitroxPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nitroxPathLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.nitroxPathLabel.Location = new System.Drawing.Point(3, 25);
            this.nitroxPathLabel.Name = "nitroxPathLabel";
            this.nitroxPathLabel.Size = new System.Drawing.Size(315, 33);
            this.nitroxPathLabel.TabIndex = 0;
            this.nitroxPathLabel.Text = "...";
            this.nitroxPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nitroxPathSelectButton
            // 
            this.nitroxPathSelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.nitroxPathSelectButton.Location = new System.Drawing.Point(318, 25);
            this.nitroxPathSelectButton.Name = "nitroxPathSelectButton";
            this.nitroxPathSelectButton.Size = new System.Drawing.Size(73, 33);
            this.nitroxPathSelectButton.TabIndex = 1;
            this.nitroxPathSelectButton.Text = "Select";
            this.nitroxPathSelectButton.UseVisualStyleBackColor = true;
            this.nitroxPathSelectButton.Click += new System.EventHandler(this.nitroxPathSelectButton_Click);
            // 
            // setupButton
            // 
            this.setupButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setupButton.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.setupButton.Location = new System.Drawing.Point(0, 246);
            this.setupButton.Name = "setupButton";
            this.setupButton.Size = new System.Drawing.Size(394, 63);
            this.setupButton.TabIndex = 4;
            this.setupButton.Text = "Setup";
            this.setupButton.UseVisualStyleBackColor = true;
            this.setupButton.Click += new System.EventHandler(this.setupButton_Click);
            // 
            // imitateSubnauticaCheckBox
            // 
            this.imitateSubnauticaCheckBox.AutoSize = true;
            this.imitateSubnauticaCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.imitateSubnauticaCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.imitateSubnauticaCheckBox.Location = new System.Drawing.Point(0, 167);
            this.imitateSubnauticaCheckBox.Name = "imitateSubnauticaCheckBox";
            this.imitateSubnauticaCheckBox.Size = new System.Drawing.Size(394, 21);
            this.imitateSubnauticaCheckBox.TabIndex = 5;
            this.imitateSubnauticaCheckBox.Text = "imitate subnauitica in different folder (windows 10)";
            this.imitateSubnauticaCheckBox.UseVisualStyleBackColor = true;
            // 
            // imitationPathGroupBox
            // 
            this.imitationPathGroupBox.Controls.Add(this.imitationPathLabel);
            this.imitationPathGroupBox.Controls.Add(this.imitationPathSelectButton);
            this.imitationPathGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.imitationPathGroupBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.imitationPathGroupBox.Location = new System.Drawing.Point(0, 188);
            this.imitationPathGroupBox.Name = "imitationPathGroupBox";
            this.imitationPathGroupBox.Size = new System.Drawing.Size(394, 58);
            this.imitationPathGroupBox.TabIndex = 4;
            this.imitationPathGroupBox.TabStop = false;
            this.imitationPathGroupBox.Text = "Installation Path";
            this.imitationPathGroupBox.Visible = false;
            // 
            // imitationPathLabel
            // 
            this.imitationPathLabel.AutoEllipsis = true;
            this.imitationPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imitationPathLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.imitationPathLabel.Location = new System.Drawing.Point(3, 25);
            this.imitationPathLabel.Name = "imitationPathLabel";
            this.imitationPathLabel.Size = new System.Drawing.Size(315, 30);
            this.imitationPathLabel.TabIndex = 0;
            this.imitationPathLabel.Text = "...";
            this.imitationPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imitationPathSelectButton
            // 
            this.imitationPathSelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.imitationPathSelectButton.Location = new System.Drawing.Point(318, 25);
            this.imitationPathSelectButton.Name = "imitationPathSelectButton";
            this.imitationPathSelectButton.Size = new System.Drawing.Size(73, 30);
            this.imitationPathSelectButton.TabIndex = 1;
            this.imitationPathSelectButton.Text = "Select";
            this.imitationPathSelectButton.UseVisualStyleBackColor = true;
            this.imitationPathSelectButton.Click += new System.EventHandler(this.imitationPathSelectButton_Click);
            // 
            // NitroxPatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 309);
            this.Controls.Add(this.setupButton);
            this.Controls.Add(this.imitationPathGroupBox);
            this.Controls.Add(this.imitateSubnauticaCheckBox);
            this.Controls.Add(this.nitroxPathGroupBox);
            this.Controls.Add(this.subnauticaPathGroupBox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NitroxPatchForm";
            this.Text = "NitroxPatch";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.subnauticaPathGroupBox.ResumeLayout(false);
            this.nitroxPathGroupBox.ResumeLayout(false);
            this.imitationPathGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox subnauticaPathGroupBox;
        private System.Windows.Forms.Button subnauticaPathSelectButton;
        private System.Windows.Forms.Label subnauticaPathLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.GroupBox nitroxPathGroupBox;
        private System.Windows.Forms.Button nitroxPathSelectButton;
        private System.Windows.Forms.Label nitroxPathLabel;
        private System.Windows.Forms.Button setupButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.CheckBox imitateSubnauticaCheckBox;
        private System.Windows.Forms.GroupBox imitationPathGroupBox;
        private System.Windows.Forms.Label imitationPathLabel;
        private System.Windows.Forms.Button imitationPathSelectButton;
    }
}

