using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public List<GameObject> powerUps = new List<GameObject>();
    public GameObject canvas;
    public bool canSpawnPowerUp = true;
    private int enemiesKilled = 0;

    public ComputeShader computeShader;
    public List<Texture2D> screenShots = new List<Texture2D>();

    private bool doneCalculating = false;

    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = canvas.GetComponent<Renderer>().bounds;

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (screenShots.Count > 0)
            {
                StartCountingWhitePixels(screenShots[screenShots.Count - 1]);
            }

        }

        //every 5 second take latest screenshot from gamecontroller


    }
    private void StartCountingWhitePixels(Texture2D screenshot)
    {
        // Find the kernel in the compute shader
        int kernelHandle = computeShader.FindKernel("CSMain");

        // Create a buffer to store the result
        ComputeBuffer resultBuffer = new ComputeBuffer(1, sizeof(int));
        int[] resultData = new int[1];

        // Set the input texture and result buffer for the compute shader
        computeShader.SetTexture(kernelHandle, "inputTexture", screenshot);
        computeShader.SetBuffer(kernelHandle, "result", resultBuffer);

        // Calculate the number of thread groups
        int threadGroupSizeX = 8;
        int threadGroupSizeY = 8;
        int dispatchSizeX = (screenshot.width + threadGroupSizeX - 1) / threadGroupSizeX;
        int dispatchSizeY = (screenshot.height + threadGroupSizeY - 1) / threadGroupSizeY;

        // Dispatch the compute shader
        computeShader.Dispatch(kernelHandle, dispatchSizeX, dispatchSizeY, 1);

        // Read the result back from the buffer
        resultBuffer.GetData(resultData);
        Debug.Log("White pixel percentage: " + (resultData[0] / (float)(screenshot.width * screenshot.height)) * 100 + "%");

        // Release the buffer
        resultBuffer.Release();
    }
    private IEnumerator CountWhitePixels(Texture2D screenshot)
    {
        var starTime = Time.realtimeSinceStartup;
        var whitePixels = 0;
        float tolerance = 0.01f; // Define a small tolerance value

        Color[] pixels = screenshot.GetPixels(); // Get all pixels at once

        for (int i = 0; i < pixels.Length; i++)
        {
            Color pixelColor = pixels[i];
            if (Mathf.Abs(pixelColor.r - 1) < tolerance &&
                Mathf.Abs(pixelColor.g - 1) < tolerance &&
                Mathf.Abs(pixelColor.b - 1) < tolerance)
            {
                whitePixels++;
            }

            // Yield every 1000 pixels to spread the work over multiple frames
            if (i % 1000 == 0)
            {
                yield return null;
            }
        }

        Debug.Log("Screenshot has " + whitePixels + " white pixels, which is " + (whitePixels / (float)(pixels.Length)) * 100 + "% of the total pixels");
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("Time taken to calculate white pixels: " + (Time.realtimeSinceStartup - starTime) + " seconds");
        doneCalculating = true;
    }
    private IEnumerator DisplayScreenshots()
    {
        Time.timeScale = 0;
        foreach (var screenshot in screenShots)
        {
            //read each white pixels of screenshot and what %
            var whitePixels = 0;
            float tolerance = 0.01f; // Define a small tolerance value
            for (int x = 0; x < screenshot.width; x++)
            {
                for (int y = 0; y < screenshot.height; y++)
                {
                    Color pixelColor = screenshot.GetPixel(x, y);
                    if (Mathf.Abs(pixelColor.r - 1) < tolerance &&
                        Mathf.Abs(pixelColor.g - 1) < tolerance &&
                        Mathf.Abs(pixelColor.b - 1) < tolerance)
                    {
                        whitePixels++;
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
            Debug.Log("Screenshot has " + whitePixels + " white pixels, which is " + (whitePixels / (float)(screenshot.width * screenshot.height)) * 100 + "% of the total pixels");
            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled == 10)
        {
            if (canSpawnPowerUp)
            {
                canSpawnPowerUp = false;
                SpawnPowerUp();
            }

        }
    }

    private void SpawnPowerUp()
    {
        Bounds bounds = canvas.GetComponent<Renderer>().bounds;

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), 0);
        Instantiate(powerUps[UnityEngine.Random.Range(0, powerUps.Count)], randomPosition, Quaternion.identity);
        Debug.Log("instantiated at location " + randomPosition.ToString());
    }

    public void AddScreenshot(Texture2D tex)
    {
        screenShots.Add(tex);
    }
}
