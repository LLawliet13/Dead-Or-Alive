using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    public Slider _musicSlider;
    public Button muteButton;
    bool setValue = false;
    private void Update()
    {
        // lay gia tri tu man truoc
        if (_musicSlider != null && !setValue)
        {
            setValue = true;
            _musicSlider.value = AudioManager.instance.musicSources.volume;
            if (AudioManager.instance.musicSources.mute == true)
            {
                muteButton.image.color = Color.red;

            }
            else
            {
                muteButton.image.color = Color.white;
            }
        }

    }
    public void ToggleMusic()
    {
        Debug.Log("Pause Music");
        AudioManager.instance.ToggleMusic();
        if (AudioManager.instance.musicSources.mute == true)
        {
            muteButton.image.color = Color.red;

        }
        else
        {
            muteButton.image.color = Color.white;
        }
    }
    public void MusicVolume()
    {
        _musicSlider.onValueChanged.AddListener(AudioManager.instance.MusicVolume);
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }
}
