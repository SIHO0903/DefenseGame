using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Initialize Sound Lists"))
        {
            LoadSFX();
            LoadBGM();
        }
    }

    // SFX 파일을 지정된 경로에서 로드하여 SoundManager의 SFX 배열에 할당
    private void LoadSFX()
    {
        SoundManager soundManager = (SoundManager)target;
        string[] assetGuids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/CustomAsset/Sound/SFX" });

        soundManager.SFX = new AudioClip[assetGuids.Length];

        for (int i = 0; i < assetGuids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
            soundManager.SFX[i] = AssetDatabase.LoadAssetAtPath<AudioClip>(assetPath);
        }

        // 에디터 상태를 변경하여 데이터가 저장되도록 만듭니다.
        EditorUtility.SetDirty(soundManager);
    }

    // BGM 파일을 지정된 경로에서 로드하여 SoundManager의 BGM 배열에 할당
    private void LoadBGM()
    {
        SoundManager soundManager = (SoundManager)target;
        string[] assetGuids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/CustomAsset/Sound/BGM" });

        soundManager.BGM = new AudioClip[assetGuids.Length];

        for (int i = 0; i < assetGuids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
            soundManager.BGM[i] = AssetDatabase.LoadAssetAtPath<AudioClip>(assetPath);
        }

        // 에디터 상태를 변경하여 데이터가 저장되도록 만듭니다.
        EditorUtility.SetDirty(soundManager);
    }
}