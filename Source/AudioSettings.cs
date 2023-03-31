using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectSlider;
    [SerializeField] private Slider _uiSlider;
    [SerializeField] private AudioMixerGroup _mixer;

    private float _startMasterVolume, _startMusicVolume, _StartEffectVolume, _startUiVolume = 1;
    private float _minValue = -80;
    private string _musicVolume = "Music";
    private string _effectVolume = "Effects";
    private string _UIVolume = "UI";
    private string _volume = "Volume";

    public void ChangeMasterVolume()
    {
        _startMasterVolume = _masterSlider.value;
        AudioListener.volume = _startMasterVolume;
        PlayerPrefs.SetFloat(_volume, AudioListener.volume);
    }

    public void ChangeMusicVolume()
    {
        _mixer.audioMixer.SetFloat(_musicVolume, Mathf.Lerp(_minValue, 0, _musicSlider.value));
        _startMusicVolume = _musicSlider.value;
    }

    public void ChangeEffectVolume()
    {
        _mixer.audioMixer.SetFloat(_effectVolume, Mathf.Lerp(_minValue, 0, _effectSlider.value));
        _StartEffectVolume = _effectSlider.value;
    }

    public void ChangeUIVolume()
    {
        _mixer.audioMixer.SetFloat(_UIVolume, Mathf.Lerp(_minValue, 0, _uiSlider.value));
        _startUiVolume = _uiSlider.value;
    }
}
