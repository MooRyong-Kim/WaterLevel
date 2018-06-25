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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;

namespace VirtualClient
{
    public partial class Form1 : Form
    {
        string id = "";
        TcpClient clientSocket = new TcpClient();

        public Form1()
        {
            InitializeComponent();

            FormClosing += Form1_FormClosing;

            id = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location);

            Regex rg = new Regex("[\\d]{4}");
            Match match = rg.Match(id);

            if (!match.Success)
            {
                id = "9999";
            }

            MessageBox.Show(id);

            new Thread(delegate ()
            {
                InitSocket();
            }).Start();

            Thread.Sleep(100);

            Thread td = new Thread(new ThreadStart(td_getData));
            td.Start();
        }

        private void InitSocket()
        {
            try
            {
                clientSocket.Connect("192.168.0.2", 7777);
                DisplayText("Client Started");
                label1.Invoke(new MethodInvoker(delegate ()
                {
                    label1.Text = "Client Socket Program - Server Connected ...";
                }));
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientSocket != null)
                clientSocket.Close();
            if (td != null)
                td.Abort();
        }

        private void td_getData()
        {
            while(true)
            {
                stream = clientSocket.GetStream();
                byte[] sbuffer = new byte[1024];
                stream.Read(sbuffer, 0, sbuffer.Length);
                string text = Encoding.ASCII.GetString(sbuffer, 0, sbuffer.Length);

                if (textBox1.InvokeRequired)
                {
                    textBox1.BeginInvoke(new MethodInvoker(delegate
                    {
                        textBox1.AppendText(Environment.NewLine + " >> " + text);
                    }));
                }
                else
                    textBox1.AppendText(Environment.NewLine + " >> " + text);

                Thread.Sleep(10);
            }
        }

        private void DisplayText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    textBox1.AppendText(Environment.NewLine + " >> " + text);
                }));
            }
            //else
                //textBox1.AppendText(Environment.NewLine + " >> " + text);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            stream = clientSocket.GetStream();
            byte[] sbuffer = Encoding.ASCII.GetBytes(textBox2.Text);
            stream.Write(sbuffer, 0, sbuffer.Length);
            stream.Flush();

/*
            byte[] rbuffer = new byte[1024];
            stream.Read(rbuffer, 0, rbuffer.Length);
            string msg = Encoding.ASCII.GetString(rbuffer);
            msg = "Data from Server : " + msg;
            DisplayText(msg);
*/

            textBox2.Text = "";
            textBox2.Focus();
        }

        NetworkStream stream;
        Thread td;
        private void btn_Test_Click(object sender, EventArgs e)
        {
            try
            {
                stream = clientSocket.GetStream();

                Regex rg = new Regex("[\\d]{4}");
                Match match = rg.Match(id);

                if (match.Success)
                {
                    byte[] sbuffer = Encoding.ASCII.GetBytes("[ID : " + id + "]");
                    stream.Write(sbuffer, 0, sbuffer.Length);
                }
            }
            catch
            {

            }

            td = new Thread(new ThreadStart(td_Test));
            td.Start();
        }

        private void td_Test()
        {
            Random rd = new Random();
            stream = clientSocket.GetStream();
            while (true)
            {
                string str = DateTime.Now.ToString("yyyyMMddHHmmss") + String.Format("{0:000.00}", rd.Next(0, 100)) + id + "\r\n";
                byte[] sbuffer = Encoding.ASCII.GetBytes(str);
                stream.Write(sbuffer, 0, sbuffer.Length);

                Thread.Sleep(1000);
            }
        }
    }
}
