using UnityEngine;
using UnityEngine.UI;

//사운드이름 enum타입으로 선언
public enum SoundType
{
    ClickBtn,
    Die,
    Explode,
    GetHit,
    Heal,
    MagicAttack,
    MeleeAttack,
    RangeAttack,
    UnitInstansiate,
    Upgrade,
    WaveSelect,
}
public enum BGMType
{
    Battle,
    Prepare,
    Start,
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] GameObject soundUI;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    public AudioClip[] BGM;
    public AudioClip[] SFX;

    private void Start()
    {
        (float bgm, float sfx) volum = MyUtil.JsonLoad<(float, float)>(MyUtil.JsonFileName.Volum);
        BGMSlider.value = volum.bgm;
        SFXSlider.value = volum.sfx;
    }
    private void Update()
    {
        SoundUI();
        BGMVolume();
        SFXVolume();
    }
    private void SoundUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !soundUI.activeSelf)
        {
            soundUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && soundUI.activeSelf)
        {
            MyUtil.JsonSave((BGMSlider.value, SFXSlider.value), MyUtil.JsonFileName.Volum);
            soundUI.SetActive(false);
        }
    }
    public void PlaySFX(SoundType sound)
    {
        sfxAudioSource.PlayOneShot(SFX[(int)sound]);
    }
    public void PlayBGM(BGMType sound)
    {
        bgmAudioSource.clip = BGM[(int)sound];
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = BGMSlider.value;
        bgmAudioSource.Play();
    }
    void BGMVolume()
    {
        bgmAudioSource.volume = BGMSlider.value;
    }
    void SFXVolume()
    {
        sfxAudioSource.volume = SFXSlider.value;
    }
}
