using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class WaveClearData
{
    public int WaveClear;
}
public class GameManager : MonoBehaviour, ICastleObserver
{
    [Header("View")]
    [SerializeField] Transform resultUI;
    [SerializeField] TMP_Text winLoseTxt;
    [SerializeField] TMP_Text moneyTxt;
    [SerializeField] TMP_Text gemTxt;
    [SerializeField] Button okBtn;
    [SerializeField] TMP_Text waveTxt;
    [Header("Model")]
    [SerializeField] Castle friendlyCastle;
    [SerializeField] Castle enemyCastle;
    [SerializeField] WaveDataSO waveData;

    [HideInInspector] public float money;
    [HideInInspector] public float gem;

    private void Awake()
    {
        okBtn.onClick.AddListener(() =>
        {
            SceneMove.Instance.LoadScene("Prepare");
        });

        friendlyCastle.CastleDestroy += OnCastleDestroyed;
        enemyCastle.CastleDestroy += OnCastleDestroyed;
        waveTxt.text = "Wave " + waveData.waveIndex;
        SoundManager.Instance.PlayBGM(BGMType.Battle);
    }


    public void OnCastleDestroyed(Castle castle)
    {
        resultUI.gameObject.SetActive(true);
        if (castle == enemyCastle)
        {
            Win();
        }
        else if (castle == friendlyCastle)
        {
            Lose();
        }
    }

    private void Win()
    {
        winLoseTxt.text = "승리";
        moneyTxt.text = waveData.rewardMoney.ToString();
        gemTxt.text = waveData.rewardGem.ToString();

        (float money,float gem) resource = MyUtil.JsonLoad<(float,float)>(MyUtil.JsonFileName.Resources);
        money = resource.money;
        gem = resource.gem;

        money += waveData.rewardMoney;
        gem += waveData.rewardGem;

        MyUtil.JsonSave((money, gem), MyUtil.JsonFileName.Resources);

        WaveClearData waveClearData = new WaveClearData { WaveClear = waveData.waveIndex };
        MyUtil.JsonSave(waveClearData, MyUtil.JsonFileName.WaveClear);

        PoolManager.Instance.DeactivateAllChildren();
    }
    private void Lose()
    {
        winLoseTxt.text = "패배";
        moneyTxt.text = "0";
        gemTxt.text = "0";
    }

}