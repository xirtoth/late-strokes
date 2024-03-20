using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{

    public GameObject canvas;
    public Camera secondCamera;
    private int numOfScreenshots = 0;
    private List<Texture2D> screenshots = new List<Texture2D>();

    private GameController gameController;

    private int width = 800;
    private int height = 600;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //every 5 seconds take a screenshot
        if (Time.time % 5 < 0.1)
        {
            StartCoroutine(CaptureScreenshotAsync());
            Debug.Log("Screenshot taken");
            numOfScreenshots++;
        }
    }

    private IEnumerator CaptureScreenshotAsync()
    {
        // Create a new RenderTexture
        RenderTexture rt = new RenderTexture(width, height, 24);
        secondCamera.targetTexture = rt;

        // Render the second camera's view to the RenderTexture
        secondCamera.Render();

        // Yield to the next frame to spread the work over multiple frames
        yield return null;

        // Read the pixels from the RenderTexture into a new Texture2D
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = null;
        secondCamera.targetTexture = null;

        // Add the screenshot to the list
        screenshots.Add(tex);

        // Save screenshot to file

        //File.WriteAllBytes(Application.dataPath + "/../Screenshot" + numOfScreenshots + ".png", bytes);
        gameController.GetComponent<GameController>().AddScreenshot(tex);
    }
    public void ShakeCamera(float shakeDuration, float shakeAmount)
    {
        StartCoroutine(Shake(shakeDuration, shakeAmount));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}