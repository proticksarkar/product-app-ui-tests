using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebUIAutomationTestFramework.Helpers
{
    public class PropertyFileHelper
    {
        private Dictionary<string, string> _dict;
        private readonly string _filePath;

        public PropertyFileHelper(string filePath)
        {
            _filePath = filePath;
            LoadFile();
        }

        public string GetPropertyValue(string property)
        {
            return _dict.ContainsKey(property) ? _dict[property] : null;
        }

        public void SetPropertyValue(string property, Object value)
        {
            if (!_dict.ContainsKey(property))
                _dict.Add(property, value.ToString());
            else
                _dict[property] = value.ToString();
        }

        public void SaveFile()
        {
            if (!File.Exists(_filePath))
                File.Create(_filePath);
            SaveDataToFile(_filePath);
        }

        private void SaveDataToFile(string filePath)
        {
            var file = new StreamWriter(filePath);

            foreach (string prop in _dict.Keys.ToArray())
                if (!string.IsNullOrWhiteSpace(_dict[prop]))
                    file.WriteLine(prop + "=" + _dict[prop]);

            file.Close();
        }

        public void LoadFile()
        {
            _dict = new Dictionary<string, string>();

            if (File.Exists(_filePath))
                LoadDataFromFile(_filePath);
            else
                File.Create(_filePath);
        }

        private void LoadDataFromFile(string filePath)
        {
            foreach (string line in File.ReadAllLines(filePath))
            {
                if ((!string.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    string key = line.Substring(0, index).Trim();
                    string value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    try
                    {
                        //ignore duplicates
                        _dict.Add(key, value);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.ToString());
                    }
                }
            }
        }
    }
}
