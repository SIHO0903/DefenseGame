using System.IO;
using System.Text;
using UnityEngine;

public static class MyUtil
{
    public static float MoveSpeed(float moveSpeed)
    {
        return moveSpeed * Time.deltaTime * 10f;
    }
    public static Vector3 SpawnRandomPos(float minX, float maxX, float minY, float maxY)
    {
        float randX;
        float randY;
        randX = Random.Range(minX, maxX);
        randY = Random.Range(minY, maxY);

        return new Vector3(randX, randY, 0);
    }
    public static void JsonSave<T>(T data,JsonFileName fileName)
    {
        Debug.Log(data);
        string json = JsonUtility.ToJson(data);
        Debug.Log("JsonSave : " + json);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.json", json);
        Debug.Log("Application.persistentDataPath : " + Application.persistentDataPath);
    }
    public static T JsonLoad<T>(JsonFileName fileName)
    {
        string path = Application.persistentDataPath + $"/{fileName}.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log("JsonLoad : " + json);
            return data;
        }
        else
        {
            Debug.Log("Statue_Save : null");
            return default(T);
        }
    }
    public static string StringBuilderTxt(params object[] values)
    {
        StringBuilder sb = new();
        sb.Clear();
        foreach (var value in values)
        {
            sb.Append(value);
        }
        return sb.ToString();
    }
    public enum JsonFileName
    {
        SelectedUnitName,
        Resources,
        WaveClear,
        Volum,
    } 
}
