using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeLoader : MonoBehaviour
{
    public Slider volumeSlider;
    void Start()
    {
        //load volume from player prefs
        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set volume from slider
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
