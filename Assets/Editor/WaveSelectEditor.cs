using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveSelectMenu))]
public class WaveSelectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WaveSelectMenu waveSelect = (WaveSelectMenu)target; 

        if (GUILayout.Button("Load WaveData SO"))
        {
            LoadWaveDataFiles(waveSelect);
        }
    }

    private void LoadWaveDataFiles(WaveSelectMenu waveSelect)
    {

        string[] assetGuids = AssetDatabase.FindAssets("t:WaveDataSO", new[] { "Assets/Data/Wave" });

        waveSelect.waveDatas = new WaveDataSO[assetGuids.Length];

        for (int i = 0; i < assetGuids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
            waveSelect.waveDatas[i] = AssetDatabase.LoadAssetAtPath<WaveDataSO>(assetPath);

        }
    }
}
