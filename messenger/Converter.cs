using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger
{
    class Converter
    {
        public byte[] ConvertToBytes<T>(T data)
        {
            if (data is string)
                return Encoding.UTF8.GetBytes(data as string);
            else if (data is int)
                return BitConverter.GetBytes((int)(object)data);
            else
                throw new ArgumentException("Unsupported data type");
        }

        public byte[] ConvertToBytes<T>(T[] data)
        {
            if (data is byte[])
                throw new ArgumentException("Unsupported data type");
            else // 배열인지 확인
            {
                if (data.GetType().GetElementType() == typeof(string)) // 문자열 배열인지 확인
                {
                    string[] stringArray = data as string[];
                    List<byte> byteArray = new List<byte>();
                    foreach (string str in stringArray)
                    {
                        byteArray.AddRange(Encoding.UTF8.GetBytes(str));
                    }
                    return byteArray.ToArray();
                }
                else if (data.GetType().GetElementType() == typeof(int)) // int 배열인지 확인
                {
                    int[] intArray = data as int[];
                    List<byte> byteArray = new List<byte>();
                    foreach (int num in intArray)
                    {
                        byteArray.AddRange(BitConverter.GetBytes(num));
                    }
                    return byteArray.ToArray();
                }
                // 다른 배열 타입에 대한 처리 추가
            }
            return data as byte[];
        }

        public string StringFromBytes(byte[] data)
        {
            string receivedString = Encoding.UTF8.GetString(data);
            return receivedString;
        }

        public int IntFromBytes(byte[] data)
        {
            int receivedInt = BitConverter.ToInt32(data, 0);
            return receivedInt;
        }

        public string DataTableToJson(DataTable dataTable)
        {
            string json = JsonConvert.SerializeObject(dataTable);
            return json;
        }

        public DataTable JsonToDataTable(string json)
        {
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);
            return dataTable;
        }

        public string DataToJson<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        public string ConvertSize(int size)
        {
            string sizeString = size.ToString();
            int cnt = 4 - sizeString.Length;
            string result = new string('0', cnt) + sizeString;
            return result;
        }
    }
}
