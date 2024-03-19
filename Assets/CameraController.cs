using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{

    public GameObject canvas;
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // If the user presses the 'C' key
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Start the CaptureScreenshot coroutine
            StartCoroutine(CaptureScreenshot());
        }
    }

    private IEnumerator CaptureScreenshot()
    {
        // Wait until the end of the frame
        yield return new WaitForEndOfFrame();
        //disable all enemies and player
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }


        // Get the width and height of the screen in pixels
        int width = Screen.width;
        int height = Screen.height;

        // Create a new texture
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read the pixels from the screen into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode the texture into PNG format
        byte[] bytes = tex.EncodeToPNG();

        // Save the PNG file to disk
        File.WriteAllBytes(Application.dataPath + "/Testi.png", bytes);
        Debug.Log("Screenshot saved to " + Application.dataPath + "/Testi.png");
        //enable all enemies and player
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }

        }
        player.SetActive(true);
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
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}