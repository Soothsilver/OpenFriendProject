using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace WinformsFriend
{
    public partial class FriendSelectForm : Form
    {
        public FriendSelectForm()
        {
            InitializeComponent();
        }

        private void FriendSelectForm_Load(object sender, EventArgs e)
        {
            ReloadFriends();
            if (this.lbFriends.Items.Count > 0)
            {
                this.lbFriends.SelectedIndex = 0;
            }
        }
        

        private void lblOpenFriends_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string folder =
                Path.Combine(

                    System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetEntryAssembly().Location
                    )
                    
                    , MemoryStorage.FolderName);
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            Process.Start(folder);

        }

        private void lblRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("This will close all connections to all friends. Do you want to refresh the list of friends anyway?", "Refresh friends from disk?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Server.Overseer.Persons.Reload();
                ReloadFriends();
            }
        }

        private void ReloadFriends()
        {
            this.lbFriends.Items.Clear();
            foreach(var fr in Server.Overseer.Persons.GetAllFriends())
            {
                this.lbFriends.Items.Add(fr);
            }
        }

        private void bCreateNewFriend_Click(object sender, EventArgs e)
        {
            Friend f = new Friend(Server.Overseer);
            Server.Overseer.Persons.CreateFriend(f);
            ReloadFriends();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.lbFriends.SelectedItem != null)
            {
                Friend f = (Friend) this.lbFriends.SelectedItem;
                MainForm mainForm = new WinformsFriend.MainForm(f);
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("You must first select a friend.", "Selection missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
