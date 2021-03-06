﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace WinformsFriend
{
    public partial class MainForm : Form
    {
        private Overseer overseer;
        private Friend friend;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Friend friend)
        {
            this.friend = friend;
            InitializeComponent();
        }

        private void Speaking_SetQuickReplies(Core.Conversation.QuickReply[] obj)
        {
            Ui(() =>
            {
                this.panelChatButtons.Controls.Clear();
                if (obj != null)
                {
                    foreach(var reply in obj)
                    {
                        Button b = new Button()
                        {
                            Dock = DockStyle.Top,
                            Text = reply.Title,
                            Margin = new Padding(5),
                            Tag = reply.Payload
                        };
                        b.Click += B_Click;
                        this.panelChatButtons.Controls.Add(b);
                    }
                }
            });
        }

        private void B_Click(object sender, EventArgs e)
        {
            this.panelChatButtons.Controls.Clear();
            SendChat((sender as Button).Text);
        }

        private void Speaking_HomeMessage(string obj)
        {
            Ui(() =>
            {
                this.tbChatHistory.AppendText(Environment.NewLine + "Friend: " + obj);
            });
        }

        private void Ui(Action action)
        {
            if (IsHandleCreated)
            {
                this.Invoke((MethodInvoker) delegate
                {
                    action();
                });
            }
        }

        private void Speaking_DebugMessage(string obj)
        {
            Ui(() => { 
                this.lbSystemLog.Items.Add(obj);
                this.lbSystemLog.TopIndex = this.lbSystemLog.Items.Count - 1;
                UpdateParatext();
            });
        }

        private void UpdateParatext()
        {
            this.Text = this.friend.Memory.Persistent.CommonName + " - Open Friend Project Home";
            this.lblCommonName.Text = this.friend.Memory.Persistent.CommonName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            overseer = Server.Overseer;
            overseer.Speaking.DebugMessage += Speaking_DebugMessage;
            friend.Speaking.Message += Speaking_HomeMessage;
            friend.Speaking.QuickRepliesSet += Speaking_SetQuickReplies;
            friend.Speaking.TypingBegan += Speaking_TypingBegan;
            friend.Speaking.TypingEnded += Speaking_TypingEnded;
            friend.Speaking.FilenameSent += Speaking_FilenameSent;
            UpdateParatext();
        }

        private void Speaking_FilenameSent(string obj)
        {
            Ui(() =>
            {
                this.tbChatHistory.AppendText(Environment.NewLine + "File: " + obj);
            });
        }

        private void Speaking_TypingEnded()
        {
            Ui(() =>
                this.progressTyping.Visible = false);
        }

        private void Speaking_TypingBegan()
        {
            Ui(() => this.progressTyping.Visible = true);
        }

        private void tbChatTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                string txt = this.tbChatTextBox.Text;
                this.tbChatTextBox.Text = "";
                SendChat(txt);
            }
        }

        private void SendChat(string text)
        {
            this.tbChatHistory.AppendText(Environment.NewLine + "You: " + text);
            overseer.Home.SendMessage(friend, text);
        }

        private void bSendChat_Click(object sender, EventArgs e)
        {
            string txt = this.tbChatTextBox.Text;
            this.tbChatTextBox.Text = "";
            this.tbChatTextBox.Focus();
            SendChat(txt);
        }
    }
}
