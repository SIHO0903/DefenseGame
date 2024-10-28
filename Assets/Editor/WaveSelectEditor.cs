using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveSelect))]
public class WaveSelectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WaveSelect waveSelect = (WaveSelect)target; 

        if (GUILayout.Button("Load WaveData SO"))
        {
            LoadWaveDataFiles(waveSelect);
        }
    }

    private void LoadWaveDataFiles(WaveSelect waveSelect)
    {

        string[] assetGuids = AssetDatabase.FindAssets("t:WaveDataSO", new[] { "Assets/Data/Wave" });

        waveSelect.waveDatas = new WaveDataSO[assetGuids.Length];

        for (int i = 0; i < assetGuids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
            waveSelect.waveDatas[i] = AssetDatabase.LoadAssetAtPath<WaveDataSO>(assetPath);

            //if (waveSelect.waveDatas[i] != null)
            //{
            //    Debug.Log($"Loaded WaveData: {waveSelect.waveDatas[i].name}");
            //}
        }
    }
}
