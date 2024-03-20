using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;



public class GameController : MonoBehaviour
{
    public List<GameObject> powerUps = new List<GameObject>();
    public GameObject canvas;
    public bool canSpawnPowerUp = true;
    private int enemiesKilled = 0;

    public ComputeShader computeShader;
    public List<Texture2D> screenShots = new List<Texture2D>();

    private bool doneCalculating = false;

    public GameObject UI;

    private float prosent;

    private float timeToCalculatePixels = 5;

    public GameObject finishSquare;

    private Camera cam;
    private int screenshots = 0;


    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = canvas.GetComponent<Renderer>().bounds;

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        cam = Camera.main;



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Time.time > timeToCalculatePixels)
        {
            timeToCalculatePixels += 5;
            StartCoroutine(CountWhitePixels(screenShots[screenShots.Count - 1]));
            if (prosent > 90)
            {

                Debug.Log("Spawning balls");

            }
            else
            {
                Debug.Log(prosent);
            }
        }

        //every 5 second take latest screenshot from gamecontroller


    }
    private void StartCountingWhitePixels(Texture2D screenshot)
    {

        int kernelHandle = computeShader.FindKernel("CSMain");


        ComputeBuffer resultBuffer = new ComputeBuffer(1, sizeof(int));
        int[] resultData = new int[1] { 0 };


        resultBuffer.SetData(resultData);


        computeShader.SetTexture(kernelHandle, "inputTexture", screenshot);
        computeShader.SetBuffer(kernelHandle, "result", resultBuffer);


        int threadGroupSizeX = 8;
        int threadGroupSizeY = 8;
        int dispatchSizeX = (screenshot.width + threadGroupSizeX - 1) / threadGroupSizeX;
        int dispatchSizeY = (screenshot.height + threadGroupSizeY - 1) / threadGroupSizeY;


        computeShader.Dispatch(kernelHandle, dispatchSizeX, dispatchSizeY, 1);


        resultBuffer.GetData(resultData);
        Debug.Log("White pixel percentage: " + (resultData[0] / (float)(screenshot.width * screenshot.height)) * 100 + "%");
        prosent = (resultData[0] / (float)(screenshot.width * screenshot.height)) * 100;
        GetComponent<UIcontroller>().precentangeText.text = prosent.ToString() + "%";

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
        prosent = (whitePixels / (float)(pixels.Length)) * 100;
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("Time taken to calculate white pixels: " + (Time.realtimeSinceStartup - starTime) + " seconds");
        doneCalculating = true;
    }
    private IEnumerator DisplayScreenshots()
    {
        Time.timeScale = 0;
        Debug.Log("total screenshots: " + screenShots.Count);
        foreach (var screenshot in screenShots)
        {
            InstantiateScreenshot(screenshot);
            yield return new WaitForSecondsRealtime(1);
            Debug.Log("displayed screenshot " + screenShots.IndexOf(screenshot) + " of " + screenShots.Count);
        }
        //calculate white pixels of last screenshot
        StartCoroutine(CountWhitePixels(screenShots[screenShots.Count - 1]));
        Debug.Log("done displaying screenshots last screenshot has " + prosent + "% white pixels");
        StartCoroutine(TextTyper());


    }

    private IEnumerator TextTyper()
    {
        string textToType = "Last screenshot has " + prosent + "% white pixels";
        foreach (char letter in textToType.ToCharArray())
        {
            GetComponent<UIcontroller>().myText.text += letter;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return new WaitForSecondsRealtime(3);
        switch (prosent)
        {
            case float n when (n < 20):
                GetComponent<UIcontroller>().myText.text += "Your grade is A";
                break;
            case float n when (n < 30):
                GetComponent<UIcontroller>().myText.text += "Your grade is B";
                break;
            case float n when (n < 60):
                GetComponent<UIcontroller>().myText.text += "Your grade is C";
                break;
            case float n when (n < 70):
                GetComponent<UIcontroller>().myText.text += "Your grade is D";
                break;
            default:
                GetComponent<UIcontroller>().myText.text += "Your grade is F";
                break;
        }
    }

    private void InstantiateScreenshot(Texture2D screenshot)
    {
        screenshots++;
        GameObject screen = new GameObject();
        screen.AddComponent<SpriteRenderer>();
        screen.GetComponent<SpriteRenderer>().sprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f), 100.0f);
        screen.transform.position = finishSquare.transform.position;
        screen.transform.name = "screenshot" + screenShots.IndexOf(screenshot);
        //order in layer
        screen.GetComponent<SpriteRenderer>().sortingOrder = screenshots;

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

    public void Finish()
    {
        cam.transform.position = finishSquare.transform.position;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
        Time.timeScale = 0;
        Debug.Log("gc finish");
        StartCoroutine(DisplayScreenshots());
    }
}
