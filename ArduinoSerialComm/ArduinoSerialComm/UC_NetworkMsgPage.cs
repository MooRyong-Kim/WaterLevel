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
using DevExpress.XtraEditors.ViewInfo;

namespace ArduinoSerialComm
{
    public partial class UC_NetworkMsgPage : UserControl
    {
        public string now_SelID { get { return rg_ClientLIst.Properties.Items[rg_ClientLIst.SelectedIndex].Description; } }

        public UC_NetworkMsgPage()
        {
            InitializeComponent();

            tc_NetworkMsg.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            tc_NetworkMsg.CloseButtonClick += Tc_NetworkMsg_CloseButtonClick;

            rg_ClientLIst.SelectedIndexChanged += Rg_ClientLIst_SelectedIndexChanged;
            rg_ClientLIst.MouseClick += Rg_ClientLIst_MouseClick;
            rtb_CalMsg.TextChanged += Rtb_TextChanged;
//             AddChannel("1234");
//             AddChannel("5678");
//             AddChannel("3457");
//             AddChannel("6789");
//             AddChannel("1020");
        }

        private void Rg_ClientLIst_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtb_CalMsg.Clear();
        }

        public void DisplayMsg(object obj)
        {
            if(obj is DataSet)
            {
                var ds = (obj as DataSet);

                string temp_id = ds.Pos.ToString();
                if (dict_tp.ContainsKey(temp_id))
                {
                    if (dict_tp[temp_id].Controls["rtb_" + temp_id] is RichTextBox)
                    {
                        var rTextBox = (dict_tp[temp_id].Controls["rtb_" + temp_id] as RichTextBox);

                        rTextBox.AppendText(ds.FullData + "\n");
                    }
                }
            }
            else if(obj is string)
            {
                var str = (obj as string);

                rtb_CalMsg.AppendText(str);
            }
        }

        private void Rg_ClientLIst_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var temp = (sender as DevExpress.XtraEditors.RadioGroup);

            RadioGroupViewInfo cInfo = (RadioGroupViewInfo)temp.GetViewInfo();

            for(int i = 0; i < temp.Properties.Items.Count; i++)
            {
                Rectangle rC = ((RadioGroupItemViewInfo)cInfo.ItemsInfo[i]).CaptionRect;
                Rectangle rG = ((RadioGroupItemViewInfo)cInfo.ItemsInfo[i]).GlyphRect;

                if (rC.Contains(e.Location) || rG.Contains(e.Location))
                {
                    string temp_des = temp.Properties.Items[i].Description;

                    if (dict_tp.ContainsKey(temp_des))
                    {
                        dict_tp[temp_des].PageVisible = true;
                    }
                    break;
                }
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
            rtb.Name = "rtb_" + id;
            rtb.TextChanged += Rtb_TextChanged;

            var tp = new XtraTabPage();
            tp.Text = id;
            tp.Controls.Add(rtb);

            dict_tp.Add(id, tp);

            tc_NetworkMsg.TabPages.Add(tp);
        }

        private void Rtb_TextChanged(object sender, EventArgs e)
        {
            if(sender is RichTextBox)
            {
                var rtb = sender as RichTextBox;

                rtb.SelectionStart = rtb.Text.Length;
                rtb.ScrollToCaret();
            }
        }

        private void Tc_NetworkMsg_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
//             DataSet temp_ds = new DataSet("20180705005959100.001234");
            DataSet temp_ds = new DataSet("20180705005959100.00" + rg_ClientLIst.Properties.Items[rg_ClientLIst.SelectedIndex].Description);
            DisplayMsg(temp_ds);

            string temp_str = "[Calibration Msg]\n";
            DisplayMsg(temp_str);
        }
    }
}
