using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using DevExpress.XtraCharts;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace ArduinoSerialComm
{
    public partial class Form1 : Form
    {
        Dictionary<string, Tuple<handleClient, BindingList<Record>>> dict_client = new Dictionary<string, Tuple<handleClient, BindingList<Record>>>();

        TcpListener server = null;
        TcpClient client = null;
        int cnt = 0;
        string msgStack = "";

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
        }

        BindingList<Record> graph_Data = new BindingList<Record>();
        void Form1_Load(object sender, EventArgs e)
        {
            setSeries("Test Chart", graph_Data);

            //Set the range for Y-Axis
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.SetMinMaxValues(-5, 110);
            
            // socket start
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
//                byte[] dgram = Encoding.ASCII.GetBytes("<Disconnect>\r\n");
//                client.GetStream().Write(dgram, 0, dgram.Length);
                client.Close();
                client = null;
            }

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
                    //                    string str = IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()));
                    DisplayText("[Accept connection from client]");

                    handleClient h_client = new handleClient();
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(DisplayText);
                    //h_client.OnCalculated += new handleClient.CalculateClientCounter(CalculateCounter);
                    h_client.OnConnClient += new handleClient.ConnectClient(MakeClientDictionary);
                    h_client.startClient(client);


//                     dict_client.Add
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

        private void CalculateCounter()
        {
            //counter--;
        }

        private void DisplayText(string text)
        {
            msgStack += text;
            check_String();
        }
        private void MakeClientDictionary(string id, handleClient hClient)
        {
            if(!dict_client.ContainsKey(id))
            {
                BindingList<Record> bList_Record = new BindingList<Record>();
                dict_client.Add(id, new Tuple<handleClient, BindingList<Record>>(hClient, bList_Record));
                setSeries(id, bList_Record);
            }
        }


        private void setSeries(string id, BindingList<Record> bList)
        {
            Series se = new Series(id, ViewType.Line);
            se.DataSource = bList;
            chartControl1.Series.Add(se);

            se.ArgumentDataMember = "TIMESTAMP";
            se.ValueDataMembers.AddRange(new string[] { "DATA" });
        }

        /*
                TcpListener srv_TCP = new TcpListener(IPAddress.Any, 7777);
                void tdTCPFunc()
                {
                    srv_TCP.Start();
                    TcpClient tc = srv_TCP.AcceptTcpClient();
                    NetworkStream net_stream = tc.GetStream();
                    while (true)
                    {
                        // (3) 데이타 송신
                        string msg = "Hello world";
        //                byte[] dgram = Encoding.ASCII.GetBytes(msg);
                        byte[] dgram = new byte[1024];
                        int nbytes = net_stream.Read(dgram, 0, dgram.Length);

                        if(0 < nbytes)
                        {
                            net_stream.Write(dgram, 0, nbytes);
                            msgStack += Encoding.Default.GetString(dgram);
                            check_String();
                        }
                    }
                    net_stream.Close();
                    tc.Close();
                }
        */

        DataSet ds;
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
                    if (DataSet.TryParse(str, out ds))
                    {
                        tb_Receive.Invoke(new MethodInvoker(delegate()
                        {
                            if(dict_client.ContainsKey(ds.Pos.ToString()))
                            {
                                dict_client[ds.Pos.ToString()].Item2.Add(new Record(ds.TimeStamp, ds.WLev));
                                if (5 < dict_client[ds.Pos.ToString()].Item2.Count)
                                {
                                    dict_client[ds.Pos.ToString()].Item2.RemoveAt(0);
                                }

                                tb_Receive.AppendText("- " + ds.FullData + "\r\n");
                            }
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

        private void btn_GraphClear_Click(object sender, EventArgs e)
        {
            cnt = 0;
            foreach(var it in dict_client.Values)
            {
                it.Item2.Clear();
            }
            tb_Receive.Clear();
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
//             byte[] dgram = Encoding.UTF8.GetBytes(tb_Send.Text);
//             srv_UDP.Send(dgram, dgram.Length, remoteEP);
        }
    }

    public class Record
    {
        public Record(long timeStamp, float data)
        {
            TIMESTAMP = timeStamp;
            DATA = data;
        }

        public long TIMESTAMP { get; set; }
        public float DATA { get; set; }
    }
}
