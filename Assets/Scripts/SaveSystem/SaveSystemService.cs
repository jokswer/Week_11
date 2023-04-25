using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveSystemService
    {
        private static readonly string _path = $"{Application.persistentDataPath}/progress";

        public static void Save(Progress progress)
        {
            var fileStream = new FileStream(_path, FileMode.Create);
            try
            {
                var binaryFormatter = new BinaryFormatter();
                var progressData = new ProgressData(progress);
                binaryFormatter.Serialize(fileStream, progressData);
            }
            catch (SerializationException exception)
            {
                Debug.Log(exception.Message);
            }
            finally
            {
                fileStream.Close();
            }
        }

        public static ProgressData Load()
        {
            if (File.Exists(_path))
            {
                var fileStream = new FileStream(_path, FileMode.Open);
                var binaryFormatter = new BinaryFormatter();
                var data = (ProgressData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}