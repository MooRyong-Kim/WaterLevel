using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
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
        TcpListener server = null;
        TcpClient client = null;
        static int counter = 0;
        int cnt = 0;
        string msgStack = "";

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
            graph_Data.ListChanged += Graph_Data_ListChanged;
        }

        private void Graph_Data_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(50 < graph_Data.Count)
            {
                graph_Data.RemoveAt(0);
            }
        }

        BindingList<Record> graph_Data = new BindingList<Record>();
        void Form1_Load(object sender, EventArgs e)
        {
//            chartControl1.DataSource = graph_Data;
            Series s1 = new Series("Test Chart", ViewType.Line);
            s1.DataSource = graph_Data;
            chartControl1.Series.Add(s1);

            s1.ArgumentDataMember = "id";
            s1.ValueDataMembers.AddRange(new string[] { "Data" });

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
                    counter++;
                    client = server.AcceptTcpClient();
                    DisplayText("[Accept connection from client]");

                    handleClient h_client = new handleClient();
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(DisplayText);
                    h_client.OnCalculated += new handleClient.CalculateClientCounter(CalculateCounter);
                    h_client.startClient(client, counter);
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
            counter--;
        }

        private void DisplayText(string text)
        {
            msgStack += text;
            check_String();

/*
            if (tb_Receive.InvokeRequired)
            {
                tb_Receive.BeginInvoke(new MethodInvoker(delegate
                {
                    tb_Receive.AppendText(text + Environment.NewLine);
                }));
            }
            else
                tb_Receive.AppendText(text + Environment.NewLine);
*/
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
                            graph_Data.Add(new Record(cnt++, "1", ds.WLev));
                            tb_Receive.AppendText("- " + ds.FullData + "\r\n");
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
            graph_Data.Clear();
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
        public Record(int id, string name, float data)
        {
            ID = id;
            Name = name;
            Data = data;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public float Data { get; set; }
    }
}
