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
    }


    private IEnumerator StartGameCoroutine()
    {
        Debug.Log("AudioListener.volume: " + AudioListener.volume);
        while (AudioListener.volume > 0.01f)
        {
            AudioListener.volume -= 0.1f;
            Debug.Log("AudioListener.volume: " + AudioListener.volume);
            yield return new WaitForSeconds(0.3f);
        }
        // load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
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
