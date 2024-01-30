using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Защита_Информаций
{

    internal class Lab1
    {
        private string _inputString = string.Empty;
        private string _encodeString = string.Empty;
        private string _decodeString = string.Empty;
        private string _key = string.Empty;
        private readonly string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        Dictionary<char, string> _data = new Dictionary<char, string>() { };

        public Lab1(string inputString, string key)
        {
            _inputString = inputString.ToLower();
            _key = key.ToLower();
            CreateAlphabet();
            Encode();
            Decode();
        }
        private void Encode()
        {
            string encodeString = string.Empty;
            foreach(var i in _inputString)
            {
                if (_data.ContainsKey(i))
                {
                    encodeString = encodeString + _data[i];
                }
                else
                {
                    encodeString = encodeString + i;
                }

            }
            _encodeString = encodeString;
        }

        private void Decode()
        {
            Dictionary<string, char> reversedData = new Dictionary<string, char>();
            foreach (var item in _data)
            {
                // Проверка на уникальность значений
                if (!reversedData.ContainsKey(item.Value))
                {
                    reversedData.Add(item.Value, item.Key);
                }
            }
            string decodeString = string.Empty;
            foreach (var i in _encodeString)
            {
                if (_data.ContainsValue(i.ToString()))
                {
                        decodeString = decodeString + reversedData[i.ToString()].ToString();
                }
                else
                {
                    decodeString = decodeString + i;
                }
                
            }
            _decodeString = decodeString;
        }

        private void CreateAlphabet()
        {
            string key = _key + _alphabet;
            List<char> k = key.Distinct().ToList();
            Dictionary<char, string> data = new Dictionary<char, string>() { };
            for (int i = 0; i < k.Count; i++)
            {
                data.Add(k[i], _alphabet[i].ToString());
            }
            _data = data;
        }
        
        public string GetEncodeString()
        {
            return _encodeString;
        }

        public string GetDecodeSring()
        {
            return _decodeString;
        }
    }
}