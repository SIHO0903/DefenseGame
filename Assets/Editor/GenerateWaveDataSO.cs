using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class GenerateWaveDataSO : EditorWindow
{
    private int waveIndex = 1;
    private int rewardMoney;
    private int rewardGem;
    private int enemyCastleHealth;

    private List<EnemyData> enemies = new List<EnemyData>();

    private class EnemyData
    {
        public EnemyName name;
        public float multiplier;
        public int spawnCount;
        public int spawnTime;
        public int spawnDelay;
    }
    [MenuItem("MyUtilities/Generate Wave SO")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow(typeof(GenerateWaveDataSO), false, "Wave Data Generator", true);
        window.minSize = new Vector2(1000, 400);
        window.maxSize = new Vector2(1000, 400);
    }
    private void OnGUI()
    {
        GUILayout.Label("Wave Data", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Enemy", GUILayout.Width(100), GUILayout.Height(30)))
        {
            enemies.Add(new EnemyData());
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("웨이브", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("보상 골드", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("보상 젬", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("적 성체력", EditorStyles.boldLabel, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();

        // Wave Data Inputs
        EditorGUILayout.BeginHorizontal();
        waveIndex = EditorGUILayout.IntField( waveIndex, GUILayout.Width(150));
        rewardMoney = EditorGUILayout.IntField(rewardMoney, GUILayout.Width(150));
        rewardGem = EditorGUILayout.IntField(rewardGem, GUILayout.Width(150));
        enemyCastleHealth = EditorGUILayout.IntField(enemyCastleHealth, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        // Table-like headers for enemy data
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("적 캐릭터", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("배율(%)", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("출현수", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("출현 시기(성체력(%))", EditorStyles.boldLabel, GUILayout.Width(150));
        GUILayout.Label("출현 딜레이(sec)", EditorStyles.boldLabel, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();

        // Display enemy data in a table-like format
        for (int i = 0; i < enemies.Count; i++)
        {
            EditorGUILayout.BeginHorizontal(); // Row start
            enemies[i].name = (EnemyName)EditorGUILayout.EnumPopup(enemies[i].name, GUILayout.Width(150));
            //enemies[i].name = EditorGUILayout.TextField(enemies[i].name, GUILayout.Width(150));
            enemies[i].multiplier = EditorGUILayout.FloatField(enemies[i].multiplier, GUILayout.Width(150));
            enemies[i].spawnCount = EditorGUILayout.IntField(enemies[i].spawnCount, GUILayout.Width(150));
            enemies[i].spawnTime = EditorGUILayout.IntField(enemies[i].spawnTime, GUILayout.Width(150));
            enemies[i].spawnDelay = EditorGUILayout.IntField(enemies[i].spawnDelay, GUILayout.Width(150));
            EditorGUILayout.EndHorizontal(); // Row end
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Generate SO", GUILayout.Width(100), GUILayout.Height(30)))
        {
            GenerateSO();
        }
    }
    private void GenerateSO()
    {
        WaveDataSO waveData = ScriptableObject.CreateInstance<WaveDataSO>();

        // Set WaveData fields
        waveData.waveIndex = waveIndex;
        waveData.rewardMoney = rewardMoney;
        waveData.rewardGem = rewardGem;
        waveData.enemyCastleHealth = enemyCastleHealth;

        // Set enemy data
        foreach (var enemy in enemies)
        {
            WaveDataSO.EnemyData enemyData = new WaveDataSO.EnemyData();
            enemyData.name = enemy.name;
            enemyData.multiplier = enemy.multiplier;
            enemyData.spawnCount = enemy.spawnCount;
            enemyData.spawnTime = enemy.spawnTime;
            enemyData.spawnDelay = enemy.spawnDelay;
            waveData.enemies.Add(enemyData);
        }

        // Save ScriptableObject to specified folder
        string path = $"Assets/Data/Wave/Wave{waveIndex}.asset";
        AssetDatabase.CreateAsset(waveData, path);
        AssetDatabase.SaveAssets();

        Debug.Log($"ScriptableObject generated at: {path}");
    }
}