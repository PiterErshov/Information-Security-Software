using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Lab1_Gamming_Srammbling.Models;

namespace Lab1_Gamming_Srammbling.Utilitiets
{
    public static class FileUtility
    {
        public static string RootDirectory = Directory.GetCurrentDirectory();
               

        public static DataModel DeserializeString(string filename) => JsonSerializer.Deserialize<DataModel>(filename);
        public static string Serialize(DataModel Data) => JsonSerializer.Serialize(Data);

        public static void JSONSave(string filename, string text) => File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Resources\\{filename}", text);
        public static string JSONSrt(string filename) => File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Resources\\{filename}");
    }
}
