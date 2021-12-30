using System.IO;
using System.Text.Json;

namespace Lab1_Gamming_Srammbling.Utilitiets
{
    public static class FileUtility
    {
        public static string RootDirectory = Directory.GetCurrentDirectory();


        public static TestModel DeserializeString<TestModel>(string filename) => JsonSerializer.Deserialize<TestModel>(filename);
        public static string Serialize<TestModel>(TestModel Data) => JsonSerializer.Serialize(Data);

        public static void JSONSave(string filename, string text) => File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Resources\\{filename}", text);
        public static string JSONSrt(string filename) => File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Resources\\{filename}");
    }
}
