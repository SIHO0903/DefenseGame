using UnityEngine;
using UnityEngine.UI;

public class BaseFacility : MonoBehaviour
{
    [SerializeField] protected GameObject ui;
    [SerializeField] protected Button openButton;
    public virtual void Awake()
    {
        openButton.onClick.AddListener(() =>
        {
            ui.SetActive(true);
            SoundManager.Instance.PlaySFX(SoundType.ClickBtn);
        });
    }

    public virtual void HideUI()
    {
        SoundManager.Instance.PlaySFX(SoundType.ClickBtn);
        ui.SetActive(false);
    }
}