using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Interfaces
{
    /// <summary>
    /// Represents a file manager for reading and writing JSON data.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Reads data from a JSON file and deserializes it to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON data into.</typeparam>
        /// <returns>The deserialized object of type <typeparamref name="T"/>.</returns>
        T ReadFromJson<T>();

        /// <summary>
        /// Writes the specified data to a JSON file.
        /// </summary>
        /// <typeparam name="T">The type of the data to write.</typeparam>
        /// <param name="data">The data to write to the JSON file.</param>
        void WriteToJson<T>(T data);
    }
}
