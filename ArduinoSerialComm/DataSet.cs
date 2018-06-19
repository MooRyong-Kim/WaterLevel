using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoSerialComm
{
    class DataSet
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
                    if(it is float)
                    {
                        tempS += String.Format("{000.00}", it);
                    }
                    else
                    {
                        tempS += DateFormat(Convert.ToInt32(it));
                    }
                }

                return tempS;
            }
        }

        private string DateFormat(int num)
        {
            string str = "";
            if (num < 10)
            {
                return str = "0" + num.ToString();
            }

            return num.ToString();
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

            for (int i = 0; i < 8; i++)
            {
                char[] tempC = new char[10];
                fullData.CopyTo(startIdx, tempC, 0, readCnt[i]);
                ParsingData((DataIdx)i, new string(tempC));
                startIdx += readCnt[i];
            }
        }

        private void ParsingData(DataIdx target, string origin)
        {

            if (DataIdx.WLev != target)
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
