namespace WinformsFriend
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbChatHistory = new System.Windows.Forms.TextBox();
            this.panelChatButtons = new System.Windows.Forms.Panel();
            this.bSendChat = new System.Windows.Forms.Button();
            this.tbChatTextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbSystemLog = new System.Windows.Forms.ListBox();
            this.lblCommonName = new System.Windows.Forms.Label();
            this.progressTyping = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinformsFriend.Properties.Resources.Anime_Girl_svg;
            this.pictureBox1.Location = new System.Drawing.Point(-223, -22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(614, 474);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 190);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(270, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(460, 600);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbChatHistory);
            this.tabPage1.Controls.Add(this.panelChatButtons);
            this.tabPage1.Controls.Add(this.bSendChat);
            this.tabPage1.Controls.Add(this.tbChatTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(452, 574);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dialogue";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbChatHistory
            // 
            this.tbChatHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbChatHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbChatHistory.Location = new System.Drawing.Point(4, 7);
            this.tbChatHistory.Multiline = true;
            this.tbChatHistory.Name = "tbChatHistory";
            this.tbChatHistory.ReadOnly = true;
            this.tbChatHistory.Size = new System.Drawing.Size(442, 338);
            this.tbChatHistory.TabIndex = 4;
            this.tbChatHistory.Text = "Open Friend Project is now online.";
            // 
            // panelChatButtons
            // 
            this.panelChatButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChatButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelChatButtons.Location = new System.Drawing.Point(4, 384);
            this.panelChatButtons.Name = "panelChatButtons";
            this.panelChatButtons.Size = new System.Drawing.Size(442, 184);
            this.panelChatButtons.TabIndex = 3;
            // 
            // bSendChat
            // 
            this.bSendChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSendChat.Location = new System.Drawing.Point(391, 351);
            this.bSendChat.Name = "bSendChat";
            this.bSendChat.Size = new System.Drawing.Size(55, 26);
            this.bSendChat.TabIndex = 2;
            this.bSendChat.Text = "Send";
            this.bSendChat.UseVisualStyleBackColor = true;
            this.bSendChat.Click += new System.EventHandler(this.bSendChat_Click);
            // 
            // tbChatTextBox
            // 
            this.tbChatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChatTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbChatTextBox.Location = new System.Drawing.Point(3, 351);
            this.tbChatTextBox.Name = "tbChatTextBox";
            this.tbChatTextBox.Size = new System.Drawing.Size(381, 26);
            this.tbChatTextBox.TabIndex = 1;
            this.tbChatTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbChatTextBox_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbSystemLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(452, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbSystemLog
            // 
            this.lbSystemLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSystemLog.FormattingEnabled = true;
            this.lbSystemLog.Location = new System.Drawing.Point(3, 3);
            this.lbSystemLog.Name = "lbSystemLog";
            this.lbSystemLog.Size = new System.Drawing.Size(446, 568);
            this.lbSystemLog.TabIndex = 0;
            // 
            // lblCommonName
            // 
            this.lblCommonName.AutoSize = true;
            this.lblCommonName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCommonName.Location = new System.Drawing.Point(29, 219);
            this.lblCommonName.Name = "lblCommonName";
            this.lblCommonName.Size = new System.Drawing.Size(86, 31);
            this.lblCommonName.TabIndex = 3;
            this.lblCommonName.Text = "Name";
            // 
            // progressTyping
            // 
            this.progressTyping.Location = new System.Drawing.Point(163, 347);
            this.progressTyping.Name = "progressTyping";
            this.progressTyping.Size = new System.Drawing.Size(100, 23);
            this.progressTyping.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressTyping.TabIndex = 5;
            this.progressTyping.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML files|*.xml";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(734, 607);
            this.Controls.Add(this.progressTyping);
            this.Controls.Add(this.lblCommonName);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Open Friend Project Home";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lbSystemLog;
        private System.Windows.Forms.Button bSendChat;
        private System.Windows.Forms.TextBox tbChatTextBox;
        private System.Windows.Forms.Panel panelChatButtons;
        private System.Windows.Forms.TextBox tbChatHistory;
        private System.Windows.Forms.Label lblCommonName;
        private System.Windows.Forms.ProgressBar progressTyping;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

