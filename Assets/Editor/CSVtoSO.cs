using UnityEditor;
using System.IO;
using UnityEngine;
using System;


public class CSVtoSO
{
    //Editor폴더에 있는 .csv파일위치
    //public static string enemyCSVPath = "/Editor/CSVs/Enemy.csv";

    [MenuItem("MyUtilities/GenerateUnitSO")]
    public static void GenerateUnitSO()
    {
        string path = EditorUtility.OpenFilePanel("Select CSV file", Application.dataPath + "/Editor/CSVs", "csv");

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("No file selected");
            return;
        }

        string[] data = File.ReadAllLines(path);

        for (int i = 1; i < data.Length; i++)
        {
            string[] splitData = data[i].Split(',');

            UnitData target = ScriptableObject.CreateInstance<UnitData>();
            target.UnitKorName = splitData[0];
            target.UnitName = splitData[1];
            target.Level = int.Parse(splitData[2]);
            target.Health = float.Parse(splitData[3]);
            target.Damage = float.Parse(splitData[4]);
            target.AttackSpeed = float.Parse(splitData[5]);
            target.MoveSpeed = float.Parse(splitData[6]);
            target.DetectRange = float.Parse(splitData[7]);
            target.AttackRange = float.Parse(splitData[8]);
            target.Cost = int.Parse(splitData[9]);
            target.MoveDir = Enum.Parse<MoveDir>(splitData[10]);

            string folderName = Path.GetFileNameWithoutExtension(path).Contains("Enemy") ? "Enemy" : "Friendly";
            AssetDatabase.CreateAsset(target, $"Assets/Data/{folderName}/{target.UnitName}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("MyUtilities/GenerateUnitUpgradeSO")]
    public static void GenerateUnitUpgradeSO()
    {
        string path = EditorUtility.OpenFilePanel("Select CSV file", Application.dataPath + "/Editor/CSVs/UnitUpgrade", "csv");


        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("No file selected");
            return;
        }

        string[] data = File.ReadAllLines(path);


        UnitUpgradeDataSO target = ScriptableObject.CreateInstance<UnitUpgradeDataSO>();
        target.unitUpgrade = new ResearchUpgrade[data.Length - 1]; 


        for (int i = 1; i < data.Length; i++)
        {
            string[] splitData = data[i].Split(',');


            ResearchUpgrade upgradeData = new ResearchUpgrade
            (
                int.Parse(splitData[0]),
                float.Parse(splitData[1]),
                float.Parse(splitData[2]),
                float.Parse(splitData[3]),
                float.Parse(splitData[4])
            );

            target.unitUpgrade[i - 1] = upgradeData;
        }

        string fullString = path;
        int lastSpaceIndex = fullString.LastIndexOf(' ');
        string fileName = fullString.Substring(lastSpaceIndex + 1);
        fileName = fileName.Replace(".csv", "");

        AssetDatabase.CreateAsset(target, $"Assets/Data/Friendly/UpgradeData/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }
}
