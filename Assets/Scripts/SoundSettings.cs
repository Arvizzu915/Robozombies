using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField]int soundIndex = 0;
    private void Start()
    {
        switch (soundIndex)
        {
            case 3:
               SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
                break;
            case 1:
                SetVolumeEffects(PlayerPrefs.GetFloat("SavedSfxVolume", 100));
                break;
            case 2:
                SetVolumeMusic(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
                break;

        }

            
        
    }

    public void SetVolume(float _value)
    {
        if (_value < 1)
        {
            _value = .001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }
    public void SetVolumeEffects(float _value)
    {
        if (_value < 1)
        {
            _value = .001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedSfxVolume", _value);
        masterMixer.SetFloat("SfxVolume", Mathf.Log10(_value / 100) * 20f);
    }
    public void SetVolumeMusic(float _value)
    {
        if (_value < 1)
        {
            _value = .001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMusicVolume", _value);
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }
    public void SetFXVolumeFromSlider()
    {
        SetVolumeEffects(soundSlider.value);
    }
    public void SetMuVolumeFromSlider()
    {
        SetVolumeMusic(soundSlider.value);
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }
}