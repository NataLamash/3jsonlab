using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;    
using System.IO;

namespace _3jsonlab
{
    public class JsonService
    {
        public static SportsFaculty DeserializeJson(string jsonFilePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                SportsFaculty sportsFaculty = JsonSerializer.Deserialize<SportsFaculty>(jsonContent);
                return sportsFaculty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return null;
            }
        }

        public static void SerializeToJson(SportsFaculty sportsFaculty, string outputFilePath)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(sportsFaculty, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputFilePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error serializing to JSON: {ex.Message}");
            }
        }
    }

}
