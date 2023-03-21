using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    public Slider _musicSlider;
    private void Update()
    {
        if (_musicSlider != null)
            _musicSlider.value = AudioManager.instance.musicSources.volume;
    }
    public void ToggleMusic()
    {
        Debug.Log("Pause Music");
        AudioManager.instance.ToggleMusic();
    }
    public void MusicVolume()
    {
        _musicSlider.onValueChanged.AddListener(AudioManager.instance.MusicVolume);
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }
}
