using TMPro;
using UnityEngine;

public class MoneyGem : MonoBehaviour
{
    [SerializeField] TMP_Text moneyTxt;
    [SerializeField] TMP_Text gemTxt;
    public float Money { get; private set; }
    public float Gem { get; private set; }

    public void UseMoney(float amount)
    {
        Money -= amount;
        moneyTxt.text = Money.ToString();
    }

    public void UseGem(float amount)
    {
        Gem -= amount;
        gemTxt.text = Gem.ToString();
    }
    
    public void LoadResource()
    {
        Debug.Log("MoneyGem : LoadResource");
        (float money,float gem) resource = MyUtil.JsonLoad<(float,float)>(MyUtil.JsonFileName.Resources);
        Money = resource.money;
        Gem = resource.gem;

        moneyTxt.text = Money.ToString();
        gemTxt.text = Gem.ToString();
    }
    public void SaveResource()
    {
        Debug.Log("MoneyGem : SaveResource");
        MyUtil.JsonSave((Money, Gem), MyUtil.JsonFileName.Resources);
    }

    //나중에 지우셈
    public void TmpBtn()
    {
        Money += 1000;
        moneyTxt.text = Money.ToString();

        Gem += 10;
        gemTxt.text = Gem.ToString();
    }
}
