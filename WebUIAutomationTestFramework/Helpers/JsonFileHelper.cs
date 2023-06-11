using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebUIAutomationTestFramework.Helpers
{
    public class JsonFileHelper
    {
        private readonly string _jsonFilePath;

        public JsonFileHelper(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public T GetJsonConfig<T>()
        {
            var jsonFile = ReadJsonFile();

            var jsonSerializeOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializeOptions.Converters.Add(new JsonStringEnumConverter());

            var jsonConfig = JsonSerializer.Deserialize<T>(jsonFile, jsonSerializeOptions);

            return jsonConfig;
        }

        private string ReadJsonFile()
        {
            return File.ReadAllText(_jsonFilePath);
        }
    }
}
