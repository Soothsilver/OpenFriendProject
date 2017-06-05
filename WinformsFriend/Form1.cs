using System;
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
    public partial class Form1 : Form
    {
        private Overseer overseer;
        public Form1()
        {
            InitializeComponent();
        }

        private async void InitOfp()
        {
            overseer = new Overseer();
            overseer.Speaking.DebugMessage += Speaking_DebugMessage;
            overseer.Speaking.HomeMessage += Speaking_HomeMessage;
            overseer.Speaking.SetQuickReplies += Speaking_SetQuickReplies;
            MetaCommunicator mc = new MetaCommunicator(overseer);
            await mc.StartLoop();
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

        private async void B_Click(object sender, EventArgs e)
        {
            await SendChat((sender as Button).Text);
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

            this.Invoke((MethodInvoker)delegate
            {
                action();
            });
        }

        private void Speaking_DebugMessage(string obj)
        {
            Ui(() => { 
                this.lbSystemLog.Items.Add(obj);
                this.lbSystemLog.TopIndex = this.lbSystemLog.Items.Count - 1;
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitOfp();
        }

        private async void tbChatTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                string txt = this.tbChatTextBox.Text;
                this.tbChatTextBox.Text = "";
                await SendChat(txt);
            }
        }

        private async Task SendChat(string text)
        {
            this.tbChatHistory.AppendText(Environment.NewLine + "You: " + text);
            await overseer.Senses.IncomingTextMessage("home", text);
        }

        private async void bSendChat_Click(object sender, EventArgs e)
        {
            string txt = this.tbChatTextBox.Text;
            this.tbChatTextBox.Text = "";
            this.tbChatTextBox.Focus();
            await SendChat(txt);
        }
    }
}
