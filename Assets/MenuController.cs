using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject howToPlayPanel;
    public GameObject optionsPanel;

    public Slider volumeSlider;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        SaveVolume();
    }

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }
    public void SaveVolume()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }


}
