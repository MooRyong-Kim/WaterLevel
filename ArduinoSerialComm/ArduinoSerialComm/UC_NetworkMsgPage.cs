using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace ArduinoSerialComm
{
    public partial class UC_NetworkMsgPage : UserControl
    {
        public UC_NetworkMsgPage()
        {
            InitializeComponent();

            tc_NetworkMsg.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            tc_NetworkMsg.CloseButtonClick += Tc_NetworkMsg_CloseButtonClick;

            //rg_ClientLIst.Click += Rg_ClientLIst_Click;
            //rg_ClientLIst.MouseClick += Rg_ClientLIst_MouseC lick;
            rg_ClientLIst.MouseUp += Rg_ClientLIst_MouseUp;
            rg_ClientLIst.MouseCaptureChanged += Rg_ClientLIst_MouseCaptureChanged;

            AddChannel("1234");
            AddChannel("5678");
        }

        private void Rg_ClientLIst_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        private void Rg_ClientLIst_MouseUp(object sender, MouseEventArgs e)
        {
            int temp_idx = (sender as DevExpress.XtraEditors.RadioGroup).SelectedIndex;
            string temp_des = (sender as DevExpress.XtraEditors.RadioGroup).Properties.Items[temp_idx].Description;

            if (dict_tp.ContainsKey(temp_des))
            {
                dict_tp[temp_des].PageVisible = true;
            }
        }

        private void Rg_ClientLIst_MouseClick(object sender, MouseEventArgs e)
        {
            int temp_idx = (sender as DevExpress.XtraEditors.RadioGroup).SelectedIndex;
            string temp_des = (sender as DevExpress.XtraEditors.RadioGroup).Properties.Items[temp_idx].Description;

            if (dict_tp.ContainsKey(temp_des))
            {
                dict_tp[temp_des].PageVisible = true;
            }
        }

        private void Rg_ClientLIst_Click(object sender, EventArgs e)
        {
            int temp_idx = (sender as DevExpress.XtraEditors.RadioGroup).SelectedIndex;
            string temp_des = (sender as DevExpress.XtraEditors.RadioGroup).Properties.Items[temp_idx].Description;

            if(dict_tp.ContainsKey(temp_des))
            {
                dict_tp[temp_des].PageVisible = true;
            }
        }

        Dictionary<string, XtraTabPage> dict_tp = new Dictionary<string, XtraTabPage>();
        public void AddChannel(string id = "")
        {
            var rbtn = new RadioGroupItem();
            rbtn.Description = id;

            rg_ClientLIst.Properties.Items.Add(rbtn);

            var rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;

            var tp = new XtraTabPage();
            tp.Text = id;
            tp.Controls.Add(rtb);

            dict_tp.Add(id, tp);

            tc_NetworkMsg.TabPages.Add(tp);
        }

        private void Tc_NetworkMsg_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
        }
    }
}
