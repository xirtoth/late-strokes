using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScreenshotCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("loading screenshot");
            StartCoroutine(LoadScreenShot());
        }
    }

    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png");
        Debug.Log("took screenshot");
    }

    public IEnumerator LoadScreenShot()
    {
        string filePath = "file://" + Application.dataPath + "/../Screenshot.png";
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load screenshot: " + www.error);
            }
            else
            {
                Texture2D picture = DownloadHandlerTexture.GetContent(www);
                Debug.Log("Loaded picture with width: " + picture.width + " and height: " + picture.height);

                //calculate how many % of pixels are white
                int whitePixels = 0;
                for (int x = 0; x < picture.width; x++)
                {
                    for (int y = 0; y < picture.height; y++)
                    {
                        if (picture.GetPixel(x, y) == Color.white)
                        {
                            whitePixels++;
                        }
                    }
                }

                Debug.Log("White pixels: " + whitePixels + " of " + picture.width * picture.height + " total pixels. That is " + (whitePixels * 100 / (picture.width * picture.height)) + "%");

                //create new gamecomponent in middle of screen pause game and display it
                GameObject screen = new GameObject();
                screen.AddComponent<SpriteRenderer>();
                screen.GetComponent<SpriteRenderer>().sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f), 100.0f);
                screen.transform.position = new Vector3(0, 0, 0);
                Time.timeScale = 0;
            }
        }
    }

}
