namespace WinformsFriend
{
    partial class FriendSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendSelectForm));
            this.lbFriends = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bCreateNewFriend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRefresh = new System.Windows.Forms.LinkLabel();
            this.lblOpenFriends = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lbFriends
            // 
            this.lbFriends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFriends.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbFriends.FormattingEnabled = true;
            this.lbFriends.ItemHeight = 20;
            this.lbFriends.Location = new System.Drawing.Point(12, 32);
            this.lbFriends.Name = "lbFriends";
            this.lbFriends.Size = new System.Drawing.Size(511, 344);
            this.lbFriends.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(12, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(315, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect to this friend";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bCreateNewFriend
            // 
            this.bCreateNewFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreateNewFriend.Location = new System.Drawing.Point(334, 384);
            this.bCreateNewFriend.Name = "bCreateNewFriend";
            this.bCreateNewFriend.Size = new System.Drawing.Size(189, 43);
            this.bCreateNewFriend.TabIndex = 2;
            this.bCreateNewFriend.Text = "Spawn a new friend";
            this.bCreateNewFriend.UseVisualStyleBackColor = true;
            this.bCreateNewFriend.Click += new System.EventHandler(this.bCreateNewFriend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Friends:";
            // 
            // lblRefresh
            // 
            this.lblRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefresh.AutoSize = true;
            this.lblRefresh.Location = new System.Drawing.Point(366, 13);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(44, 13);
            this.lblRefresh.TabIndex = 4;
            this.lblRefresh.TabStop = true;
            this.lblRefresh.Text = "Refresh";
            this.lblRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblRefresh_LinkClicked);
            // 
            // lblOpenFriends
            // 
            this.lblOpenFriends.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOpenFriends.AutoSize = true;
            this.lblOpenFriends.Location = new System.Drawing.Point(416, 13);
            this.lblOpenFriends.Name = "lblOpenFriends";
            this.lblOpenFriends.Size = new System.Drawing.Size(108, 13);
            this.lblOpenFriends.TabIndex = 5;
            this.lblOpenFriends.TabStop = true;
            this.lblOpenFriends.Text = "Open folder with data";
            this.lblOpenFriends.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpenFriends_LinkClicked);
            // 
            // FriendSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 439);
            this.Controls.Add(this.lblOpenFriends);
            this.Controls.Add(this.lblRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCreateNewFriend);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbFriends);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FriendSelectForm";
            this.Text = "Open Friend Project -- Friend Selection";
            this.Load += new System.EventHandler(this.FriendSelectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFriends;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bCreateNewFriend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblRefresh;
        private System.Windows.Forms.LinkLabel lblOpenFriends;
    }
}