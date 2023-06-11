using Bookstore.Interfaces;
using System;
using System.Xml;
using NLog;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;

namespace Bookstore.Classes
{
    /// <summary>
    /// Represents a file manager for reading and writing JSON data.
    /// </summary>
    public class FileManager : IFileManager
    {
        private readonly string _jsonFilePath;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        // Initializes a new instance of the FileManager class with the specified JSON file path.
        public FileManager(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        // Reads data from a JSON file and deserializes it to the specified type.
        public T ReadFromJson<T>()
        {
            try
            {
                if (File.Exists(_jsonFilePath))
                {
                    string json = File.ReadAllText(_jsonFilePath);
                    var data = JsonConvert.DeserializeObject<T>(json);
                    if (data != null)
                    {
                        return data;
                    }
                    else
                    {
                        Console.WriteLine(ErrorMessages.EmptyFileError);
                    }
                }
                else
                {
                    Console.WriteLine(ErrorMessages.FileNotFoundError);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ErrorMessages.FileNotFoundError);
                logger.Error(ex);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ErrorMessages.JsonDeserializationError);
                logger.Error(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessages.UnhandledExceptionError);
                logger.Error(ex);
            }

            return default(T);
        }

        // Writes the specified data to a JSON file.
        public void WriteToJson<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_jsonFilePath, json);
        }
    }
}
