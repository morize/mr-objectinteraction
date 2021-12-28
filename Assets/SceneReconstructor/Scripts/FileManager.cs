using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static void StoreSerializedData(string fileName, string data)
    {
        WriteFile(GetPath(fileName), data);
    }

    public static T ReadSerializedData<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));
        
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default;
        }

        return JsonUtility.FromJson<T>(content);
    }

    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}
