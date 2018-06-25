using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace ArduinoSerialComm
{
    class handleClient
    {
        TcpClient clientSocket;
        private int client_No = -1;
        public int CLIENT_NO { get { return client_No; } }

        public static Dictionary<string, handleClient> dict_hClient = new Dictionary<string, handleClient>();

        public void startClient(TcpClient ClientSocket)
        {
            this.clientSocket = ClientSocket;

            Thread t_hanlder = new Thread(doChat);
            t_hanlder.IsBackground = true;
            t_hanlder.Start();
        }

        public delegate void MessageDisplayHandler(string text);
        public event MessageDisplayHandler OnReceived;

        //public delegate void CalculateClientCounter();
        //public event CalculateClientCounter OnCalculated;

        public delegate void ConnectClient(string id, handleClient hClient);
        public event ConnectClient OnConnClient;

        private void doChat()
        {
            NetworkStream stream = null;
            try
            {
                byte[] buffer = new byte[1024];
                string msg = string.Empty;
                int bytes = 0;
                int MessageCount = 0;

                while (true)
                {
                    MessageCount++;
                    stream = clientSocket.GetStream();

                    //var pi = stream.GetType().GetProperty("Socket", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    //var socketIp = ((Socket)pi.GetValue(stream, null)).RemoteEndPoint.ToString();
                    //this.ip = socketIp;

                    bytes = stream.Read(buffer, 0, buffer.Length);
                    msg = Encoding.ASCII.GetString(buffer, 0, bytes);
                    //msg = msg.Substring(0, msg.IndexOf("$"));
                    //msg = "Data Received : " + msg;

                    if (msg.Contains("[ID : "))
                    {
                        Regex rg = new Regex("[\\d]{4}");
                        Match match = rg.Match(msg);

                        if (match.Success)
                        {
                            Group g = match.Groups[0];
                            msg = g.ToString();
                            int.TryParse(msg, out client_No);

                            if (OnConnClient != null)
                                OnConnClient(CLIENT_NO.ToString(), this);
                        }
                    }

                    if (OnReceived != null)
                        OnReceived(msg);

                    DataSet ds = null;
                    if (DataSet.TryParse(msg, out ds))
                    {
                        client_No = ds.Pos;
                        string id = CLIENT_NO.ToString();
                        if (!dict_hClient.ContainsKey(id))
                        {
                            dict_hClient.Add(id, this);
                        }
                    }


/*
                    msg = "Server to client(" + clientNo.ToString() + ") " + MessageCount.ToString();
                    if (OnReceived != null)
                        OnReceived(msg);

                    byte[] sbuffer = Encoding.Unicode.GetBytes(msg);
                    stream.Write(sbuffer, 0, sbuffer.Length);
                    stream.Flush();


                    msg = " >> " + msg;
                    if (OnReceived != null)
                    {
                        OnReceived(msg);
                        OnReceived("");
                    }
*/
                }
            }
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("doChat - SocketException : {0}", se.Message));

                if (clientSocket != null)
                {
                    clientSocket.Close();
                    stream.Close();
                }

                //if (OnCalculated != null)
                //    OnCalculated();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("doChat - Exception : {0}", ex.Message));

                if (clientSocket != null)
                {
                    clientSocket.Close();
                    stream.Close();
                }

                //if (OnCalculated != null)
                //    OnCalculated();
            }
        }

        public void sendMSG(string msg)
        {
            NetworkStream stream = null;
            try
            {
                stream = clientSocket.GetStream();
                byte[] buffer = Encoding.ASCII.GetBytes(msg);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {

            }
        }
    }
}
