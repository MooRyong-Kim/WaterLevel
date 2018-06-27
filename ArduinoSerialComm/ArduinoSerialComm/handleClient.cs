using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ArduinoSerialComm
{
    class handleClient
    {
        public TcpClient clientSocket;
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

                while (true)
                {
                    try
                    {
                        stream = clientSocket.GetStream();
                        // 앞서 생성했던 Client에 의한 부하 발생 시 사용
                        //stream.ReadTimeout = 1000;

                        bytes = stream.Read(buffer, 0, buffer.Length);
                        msg = Encoding.ASCII.GetString(buffer, 0, bytes);

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
                            else
                            {
                                if (!dict_hClient[id].Equals(this))
                                {
                                    dict_hClient[id] = this;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
/*
                        // 앞서 생성했던 Client에 의한 부하 발생 시 사용
                        if(removeID != CLIENT_NO)
                        {
                            if(dict_hClient.ContainsKey(removeID))
                            {
                                dict_hClient.Remove(removeID);
                            }
                        }
*/
                    }

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
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("doChat - Exception : {0}", ex.Message));

                if (clientSocket != null)
                {
                    clientSocket.Close();
                    stream.Close();
                }
            }
        }

        public void sendMSG(string msg)
        {
            if(clientSocket.Connected)
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
}
