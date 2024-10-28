using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSelect : BaseFacility
{
    [Header("View")]
    public WaveDataSO[] waveDatas;
    [SerializeField] WaveDataSO selectedWave;

    [Header("Model")]
    [SerializeField] Transform waveButtonsTransform;
    [SerializeField] GameObject buttonPrefab;

    public override void Awake()
    {
        base.Awake();

        CreateWaveButtons();
        int waveindex = 0;
        WaveClearData waveClearData = MyUtil.JsonLoad<WaveClearData>(MyUtil.JsonFileName.WaveClear);

        waveindex = (waveClearData != null) ? waveClearData.WaveClear : 0;

        int waveClear = waveindex + 1;
        for (int i = 0; i < waveClear; i++)
        {
            waveButtonsTransform.GetChild(i).GetComponentInChildren<Button>().interactable = true;
        }
    }

    // ���̺� ���� ��ư�� �����ϰ� �� ��ư�� ���̺� ������ ����
    private void CreateWaveButtons()
    {

        for (int i = 0; i < waveDatas.Length; i++)
        {
            int index = i;
            GameObject buttonObj = Instantiate(buttonPrefab, waveButtonsTransform);
            buttonObj.name = waveDatas[i].name + "Button";
            Button button = buttonObj.GetComponent<Button>();
            TMP_Text buttonTxt = buttonObj.GetComponentInChildren<TMP_Text>();
            buttonTxt.text = waveDatas[i].name;
            button.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX(SoundType.WaveSelect);
                SelectWave(index);
            });
            button.interactable = false;
        }
    }

    // ���õ� ���̺� �����͸� �����ϰ� 'Battle' ������ �̵�
    private void SelectWave(int index)
    {
        CopyWaveData(waveDatas[index], selectedWave);
        Debug.Log("���̺꼱��");
        SceneMove.Instance.LoadScene("Battle");
    }
    private void CopyWaveData(WaveDataSO origin, WaveDataSO copy)
    {
        copy.waveIndex = origin.waveIndex;
        copy.rewardMoney = origin.rewardMoney;
        copy.rewardGem = origin.rewardGem;
        copy.enemyCastleHealth = origin.enemyCastleHealth;

        copy.enemies = new List<WaveDataSO.EnemyData>();
        foreach (var enemy in origin.enemies)
        {
            WaveDataSO.EnemyData enemyCopy = new WaveDataSO.EnemyData
            {
                name = enemy.name,
                multiplier = enemy.multiplier,
                spawnCount = enemy.spawnCount,
                spawnTime = enemy.spawnTime,
                spawnDelay = enemy.spawnDelay
            };
            copy.enemies.Add(enemyCopy);
        }
    }
}
