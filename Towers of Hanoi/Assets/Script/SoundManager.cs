using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip ButtionClickSound,levelclick;
    [Space]
    [SerializeField]
    AudioSource BackGroundSource, SFXSource;
    public static SoundManager Instance;
    public Slider MusicSlider, SoundSlider;
    public string SFXvolume = "sfxvolume", BGvolume = "bgvolume";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        MusicSlider.value = SoundManager.Instance.Getbgvolme();
        SoundSlider.value = SoundManager.Instance.Getsfxvolme();
        MusicSlider.onValueChanged.AddListener(setBgVolume);
        SoundSlider.onValueChanged.AddListener(setSfxVolume);
    }
    public void setBgVolume(float a)
    {
        SoundManager.Instance.setBGvolume(a);
    }
    public void setSfxVolume(float a)
    {
        SoundManager.Instance.setSFXvolume(a);
    }
    public void setBGVolume(float value)
    {
        SoundManager.Instance.setBGvolume(value);
    }
    public void setSFXVolume(float value)
    {
        SoundManager.Instance.setSFXvolume(value);
    }

    public void ButttonClick()
    {
        SFXSource.PlayOneShot(ButtionClickSound);
    }
    public void LevelSound()
    {
        SFXSource.PlayOneShot(levelclick);
    }

    public void setBGvolume(float volume)
    {
        BackGroundSource.volume = volume;
        PlayerPrefs.SetFloat(BGvolume, volume);
    }
    public void setSFXvolume(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat(SFXvolume, volume);
    }
    public float Getbgvolme()
    {
        return PlayerPrefs.GetFloat(BGvolume, 1);
    }
    public float Getsfxvolme()
    {
        return PlayerPrefs.GetFloat(SFXvolume, 1);
    }
}
