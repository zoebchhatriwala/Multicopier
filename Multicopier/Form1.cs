using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multicopier
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CenterToScreen();
        }
        
        

        private void copy_Click(object sender, EventArgs e)
        {
             try
             {
                 if (tabControl1.SelectedIndex == 0)
                 {
                     Clipboard.SetText(items.SelectedItem.ToString());
                 }
                 else
                 {
                     StringCollection file = new StringCollection();
                     foreach (string x in items2.SelectedItems)
                     {
                         file.Add(x);
                     }
                    Clipboard.SetFileDropList(file);
                 }
             }
             catch (Exception) { MessageBox.Show("No Item Selected","Error");}
        }

        private void del_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                items.Items.Remove(items.SelectedItem);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                items2.Items.Remove(items2.SelectedItem);
            }
        }

        private void clr_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                items.Items.Clear();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                items2.Items.Clear();
            }
        }

        private void hide_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           if (items.Items.Contains(Clipboard.GetText())==false && Clipboard.GetText()!="")
            {
              items.Items.Insert(0, Clipboard.GetText());
            }
           else if (Clipboard.ContainsFileDropList()==true)
           {
            StringCollection file = new StringCollection();
            file = Clipboard.GetFileDropList();
            foreach (string x in file)
                {
                    if (items2.Items.Contains(x) == false)
                    { items2.Items.Insert(0, x); }
                }
            }
            
        }

        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            Home.ActiveForm.Text = "Multicopier [Running]";
            stop.Enabled = true;
            timer1.Enabled = true;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            start.Enabled = true;
            Home.ActiveForm.Text = "Multicopier";
            stop.Enabled = false;
            timer1.Enabled = false;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Opacity = 1;
            this.ShowInTaskbar = true;
        }

        private void about_Click(object sender, EventArgs e)
        {
          MessageBox.Show("Multicopier is a Application That can be Used for Copying Multiple Data From Multiple Sources,\nMulticopier Tracks Your Copy Record for Future Refrence Until Application Closes.\nApplication Developed By: Zoeb Chhatriwala");
        }
    }
}
