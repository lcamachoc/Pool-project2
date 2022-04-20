using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider.value = AudioManager.Instance.sounds[0].volume;
    }
    public void setVolume()
    {
        float volume = slider.value;
        AudioManager.Instance.setVolume(volume);
    }
}
