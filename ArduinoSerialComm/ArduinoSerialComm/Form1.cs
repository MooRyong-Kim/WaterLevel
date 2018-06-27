using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace ArduinoSerialComm
{
    public partial class Form1 : Form
    {
        Dictionary<string, BindingList<Record>> dict_ReceiveData = new Dictionary<string, BindingList<Record>>();

        public delegate void MessageSendHandler(string text);
        public event MessageSendHandler OnSend;

        TcpListener server = null;
        TcpClient client = null;
        string msgStack = "";

        struct controlMSG
        {
            //Arduino DataManagement 사용시
            //public const string Stop = "S\r\n";
            //public const string Go = "G\r\n";
            //public static readonly string[] LowCal = { "L", "W" };
            //public static readonly string[] HighCal = { "H", "W" };
            //public static readonly string[] LowInfo = { "L", "R" };
            //public static readonly string[] HighInfo = { "H", "R" };

            //Arduino DataManagement_Improve 사용시
            public const string Stop = "S";
            public const string Go = "G";
            public const string LowCal = "M";
            public const string HighCal = "D";
            public const string LowInfo = "L";
            public const string HighInfo = "H";
        }

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            //Set the range for Y-Axis
            chartControl1.Series.Add(new Series());
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Second;
            diagram.AxisX.Label.TextPattern = "{A:HH:mm:ss}";
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.SetMinMaxValues(-5, 110);

            rg_ClientLIst.SelectedIndexChanged += Rg_ClientLIst_SelectedIndexChanged;
                
            // socket start
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
        }

        private void Rg_ClientLIst_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_ReceiveCal.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(var it in handleClient.dict_hClient.Values)
            {
                if(it != null && it.clientSocket != null)
                {
                    it.clientSocket.Close();
                    it.clientSocket = null;
                }
            }

/*
            if (client != null)
            {
                client.Close();
                client = null;
            }
*/

            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }

        private void InitSocket()
        {
            server = new TcpListener(IPAddress.Any, 7777);
            client = default(TcpClient);
            server.Start();
            DisplayText("[Server Started]");

            while (true)
            {
                try
                {
                    client = server.AcceptTcpClient();
                    //string str = IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()));
                    DisplayText("[Accept connection from client]");

                    handleClient h_client = new handleClient();
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(DisplayText);
                    h_client.startClient(client);
                }
                catch (SocketException se)
                {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                }
            }
        }

        private void DisplayText(string text)
        {
            msgStack += text;
            check_String();
        }


        private void setSeries(string id, BindingList<Record> bList)
        {
            Series se = new Series(id, ViewType.Line);
            se.DataSource = bList;
            chartControl1.Series.Add(se);

            // Set the scale type for the series' arguments and values.
            se.ArgumentScaleType = ScaleType.DateTime;
            se.ValueScaleType = ScaleType.Numerical;

            se.ArgumentDataMember = "TIMESTAMP";
            se.ValueDataMembers.AddRange(new string[] { "DATA" });
        }

        List<DataSet> ds_list;
        private void check_String()
        {
            while(true)
            {
                string str = "";
                Regex rg = new Regex("\\[.*\\]");
                Match m = rg.Match(msgStack);

                if (m.Success)
                {
                    Group g = m.Groups[0];
                    msgStack = msgStack.Replace(g.ToString(), "");
                    str = g.ToString();
                }
                else if (msgStack.Contains("\r"))
                {
                    msgStack = msgStack.Replace("\n", "");

                    str = msgStack.Split('\r')[0];
                    msgStack = msgStack.Replace(str + "\r", "");
                }
                else return;

                if ("" != str)
                {
                        DataSet ds = null;
                        if (DataSet.TryParse(str, out ds))
                        {
                            tb_Receive.Invoke(new MethodInvoker(delegate()
                            {
                                string id = ds.Pos.ToString();
                                if (dict_ReceiveData.ContainsKey(id))
                                {
                                    dict_ReceiveData[id].Add(new Record(ds, ds.WLev));
                                    if (50 < dict_ReceiveData[id].Count)
                                    {
                                        dict_ReceiveData[id].RemoveAt(0);
                                    }

                                    tb_Receive.AppendText("- " + ds.FullData + "\r\n");
                                }
                                else
                                {
                                    BindingList<Record> bList_Record = new BindingList<Record>();
                                    dict_ReceiveData.Add(id, bList_Record);
                                    setSeries(id, bList_Record);

                                    var rbtn = new RadioGroupItem();
                                    rbtn.Description = id;

                                    rg_ClientLIst.Properties.Items.Add(rbtn);
                                }

                                string str_ID = rg_ClientLIst.Properties.Items[rg_ClientLIst.SelectedIndex].Description;
                                if (!temp_rflag && record_flag)
                                {
                                    ds_list = new List<DataSet>();
                                }

                                if(record_flag && ds.Pos.ToString() == str_ID)
                                {
                                    ds_list.Add(ds);
                                }

                                if(temp_rflag && !record_flag)
                                {
                                    save_CSV(str_ID, ds_list);
                                    ds_list.Clear();
                                    ds_list = null;
                                }

                                temp_rflag = record_flag;
                            }));
                    }
                    else
                    {
                        tb_Receive.Invoke(new MethodInvoker(delegate()
                        {
                            tb_Receive.AppendText(str + "\r\n");
                        }));
                    }
                }
            }
        }

        private void save_CSV(string id, List<DataSet> save_Data)
        {
            string csv_filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Record\\" + id + ".csv";
            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetDirectoryName(csv_filePath));
            if (di.Exists == false)
            {
                di.Create();
            }
            StreamWriter sw = File.CreateText(csv_filePath);

            // Low, High Data
            //sw.WriteLine("LOW Clock Count : ," + LCnt);
            //sw.WriteLine("LOW Depth(cm) : ," + LCm);
            //sw.WriteLine("HIGH Clock Count : ," + HCnt);
            //sw.WriteLine("HIGH Depth(cm) : ," + HCm);

            foreach(var it in save_Data)
            {
                string cCount = "";
                string depthCm = it.WLev.ToString();
                sw.WriteLine("Clock Count : ," + cCount + ",Depth(cm) : ," + depthCm);
            }

            sw.Flush();
            sw.Close();
        }

        private void btn_GraphClear_Click(object sender, EventArgs e)
        {
            foreach(var it in dict_ReceiveData.Values)
            {
                it.Clear();
            }
            tb_Receive.Clear();
        }

        private void sendClientMSG(string str)
        {
            handleClient.dict_hClient[rg_ClientLIst.Properties.Items[rg_ClientLIst.SelectedIndex].Description].sendMSG(str);
        }

        private void sendClientMSG(string[] str)
        {
            foreach(var it in str)
            {
                handleClient.dict_hClient[rg_ClientLIst.Properties.Items[rg_ClientLIst.SelectedIndex].Description].sendMSG(it);
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            int tempI = -1;
            if(int.TryParse(tb_Send.Text, out tempI))
            {
                sendClientMSG(tb_Send.Text);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            sendClientMSG(controlMSG.Stop);
        }

        private void btn_Go_Click(object sender, EventArgs e)
        {
            sendClientMSG(controlMSG.Go);
        }

        private void btn_LowCal_Click(object sender, EventArgs e)
        {
            sendClientMSG(controlMSG.LowCal);
        }

        private void btn_HighCal_Click(object sender, EventArgs e)
        {
            sendClientMSG(controlMSG.HighCal);
        }

        private void btn_LowInfo_Click(object sender, EventArgs e)
        {
            sendClientMSG(controlMSG.LowInfo);
        }

        private void btn_HighInfo_Click(object sender, EventArgs e)
        {
            sendClientMSG(controlMSG.HighInfo);
        }

        bool record_flag = false;
        bool temp_rflag = false;
        private void btn_Record_Click(object sender, EventArgs e)
        {
            record_flag = !record_flag;

            if(record_flag)
            {
                btn_Record.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                btn_Record.BackColor = System.Drawing.Color.Red;
            }
        }
    }

    public class Record
    {
        public Record(DataSet ds, float data)
        {
            TIMESTAMP = ds.TimeStampDT;
            DATA = data;
        }

        public DateTime TIMESTAMP { get; set; }
        public float DATA { get; set; }
    }
}
