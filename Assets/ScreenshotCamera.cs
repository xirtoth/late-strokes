using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            LoadScreenShot();
        }
    }

    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png");
        Debug.Log("took screenshot");
    }

    public void LoadScreenShot()
    {
        var picture = Resources.Load("Screenshot.png") as Texture2D;
        //create new gamecomponent in middle of screen pause game and display it
        GameObject screen = new GameObject();
        screen.AddComponent<SpriteRenderer>();
        screen.GetComponent<SpriteRenderer>().sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f), 100.0f);
        screen.transform.position = new Vector3(0, 0, 0);
        Time.timeScale = 0;

    }

}
