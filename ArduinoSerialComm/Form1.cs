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

namespace ArduinoSerialComm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        BindingList<Record> graph_Data = new BindingList<Record>();
        void Form1_Load(object sender, EventArgs e)
        {
            chartControl1.DataSource = graph_Data;
            Series s1 = new Series("Test Chart", ViewType.Line);
            chartControl1.Series.Add(s1);

            s1.ArgumentDataMember = "id";
            s1.ValueDataMembers.AddRange(new string[] { "Data" });

            //Set the range for Y-Axis
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYScrolling = true;
            //diagram.AxisX.WholeRange.Auto = false;
            //diagram.AxisX.WholeRange.SetMinMaxValues(-10, 10);
            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.SetMinMaxValues(-5, 110);
            
            //((BarSeriesView)s1.View).ColorEach = true;

            //Thread tdBT = new Thread(new ThreadStart(tdBTFunc));
            //tdBT.Start();
            Thread tdUDP = new Thread(new ThreadStart(tdUDPFunc));
            tdUDP.Start();
        }

        void BTC()
        {
            //BlueToothConnection.BaudRate = (115200);
            //BlueToothConnection.PortName = "COM4";
            BlueToothConnection.BaudRate = (9600);
            BlueToothConnection.PortName = "COM6";
            BlueToothConnection.Parity = Parity.None;
            BlueToothConnection.StopBits = StopBits.One;
            BlueToothConnection.DataBits = 8;
        }

        SerialPort BlueToothConnection = new SerialPort();
        int cnt = 0;
        void tdBTFunc()
        {
            while (true)
            {
                return;
                try
                {
                    if (BlueToothConnection.IsOpen)
                    {
                        string str = BlueToothConnection.ReadLine();
                        Regex rex = new Regex("[0-9]{3}p");
//                         string[] divide_data = BlueToothConnection.ReadLine().Split(' ');
//                         string str = "";

//                         foreach(var it in divide_data)
//                         {
//                             str = rex.Match(it).Value;
//                             if("" != str) break;
//                             if ("Button" == it) { str = it; break; }
//                         }

                        if(str != "")
                        {   
                            tb_Receive.Invoke(new MethodInvoker(delegate()
                            {
                                tb_Receive.AppendText("- " + str + "\r\n");
                            }));
                        }

                        if("Button" !=str )
                            graph_Data.Add(new Record(cnt++, "1", Convert.ToSingle(str.Remove(4))));
                        System.Threading.Thread.Sleep(5);
                    }
                }
                catch(Exception e)
                {

                }
            }
        }

        string msgStack = "";
        UdpClient srv = new UdpClient(7777);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Broadcast, 7777);
        void tdUDPFunc()
        {
            while (true)
            {
                // (3) 데이타 송신
                byte[] dgram;

                //srv.Send(dgram, dgram.Length, remoteEP);
                //Console.WriteLine("[Send] {0} 로 {1} 바이트 송신", remoteEP.ToString(), dgram.Length);

                dgram = srv.Receive(ref remoteEP);

                //if (DataSet.TryParse(Encoding.Default.GetString(dgram), out ds))
                msgStack += Encoding.Default.GetString(dgram);
                check_String();
            }
        }

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

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender is Button)
            {
                var it = sender as Button;
                if("btn_Connect" == it.Name)
                {
                    try
                    {
                        BlueToothConnection.Open();
                    }
                    catch(Exception ex)
                    {

                    }
                }
                else if ("btn_Disconnect" == it.Name)
                {
                    try
                    {
                        BlueToothConnection.Close();
                    }
                    catch (Exception ex)
                    {

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
            byte[] dgram = Encoding.UTF8.GetBytes(tb_Send.Text);
            srv.Send(dgram, dgram.Length, remoteEP);
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

    public class DataSet
    {
        enum DataIdx
        {
            Y,
            M,
            D,
            h,
            m,
            s,
            WLev,
            Pos,
        }

        Dictionary<DataIdx, object> dict_var = new Dictionary<DataIdx, object>();
        public DataSet(string fullData)
        {
            int idx = 0;
            for (int i = 0; i < 8; i++)
            {
                dict_var.Add((DataIdx)i, -1);
            }

            DivideDataSet(fullData);
        }

        public int Y { get { return (int)dict_var[DataIdx.Y]; } set { dict_var[DataIdx.Y] = value; } }
        public int M { get { return (int)dict_var[DataIdx.M]; } set { dict_var[DataIdx.M] = value; } }
        public int D { get { return (int)dict_var[DataIdx.D]; } set { dict_var[DataIdx.D] = value; } }
        public int h { get { return (int)dict_var[DataIdx.h]; } set { dict_var[DataIdx.h] = value; } }
        public int m { get { return (int)dict_var[DataIdx.m]; } set { dict_var[DataIdx.m] = value; } }
        public int s { get { return (int)dict_var[DataIdx.s]; } set { dict_var[DataIdx.s] = value; } }
        public float WLev { get { return (float)dict_var[DataIdx.WLev]; } set { dict_var[DataIdx.WLev] = value; } }
        public int Pos { get { return (int)dict_var[DataIdx.Pos]; } set { dict_var[DataIdx.Pos] = value; } }

        public string FullData
        {
            get
            {
                string tempS = "";

                foreach (var it in dict_var.Values)
                {
                    tempS += it;
                }

                return tempS;
            }
        }

        private string WaterLevel_Format(float wl)
        {
            String str = "";
            if (wl < 100 && wl >= 10)
            {
//                str = "0" + (roundf(wl * 100.0) / 100.0);
            }
            else if (wl < 10 && wl >= 0)
            {
//                str = "00" + (roundf(wl * 100.0) / 100.0);
            }
            else
            {
                str = "000.00";
            }

            return str;
        }

        private DataSet self
        {
            get
            {
                return this;
            }
        }

        public static bool TryParse(string str, out DataSet result)
        {
            try
            {
                result = new DataSet(str);
            }
            catch (Exception ex)
            {
                result = null;
                return false;
            }

            return true;
        }

        public void DivideDataSet(string fullData)
        {
            int startIdx = 0;
            int[] readCnt = { 4, 2, 2, 2, 2, 2, 6, 4 };

            for(int i = 0; i < 8; i++)
            {
                char[] tempC = new char[10];
                fullData.CopyTo(startIdx, tempC, 0, readCnt[i]);
                ParsingData((DataIdx)i, new string(tempC));
                startIdx += readCnt[i];
            }
        }

        private void ParsingData(DataIdx target, string origin)
        {
            
            if(DataIdx.WLev != target)
            {

                int tempI = -1;
                int.TryParse(origin, out tempI);
                dict_var[target] = tempI;
            }
            else if (DataIdx.WLev == target)
            {
                float tempD = -1;
                float.TryParse(origin, out tempD);
                dict_var[target] = tempD;
            }
        }
    }
}
