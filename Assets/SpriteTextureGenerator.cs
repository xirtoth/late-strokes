using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTextureGenerator : MonoBehaviour
{
    public int textureWidth = 256;
    public int textureHeight = 256;
    private Texture2D texture;

    private void Start()
    {
        // Create an empty texture
        Texture2D texture = new Texture2D(textureWidth, textureHeight);

        // Iterate over each pixel
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // Generate a random color
                Color color = new Color(Random.value, Random.value, Random.value);

                // Set the pixel to the random color
                texture.SetPixel(x, y, Color.white);
            }
        }

        AddColorSplash(Color.red, 5);
        // Apply the changes to the texture
        texture.Apply();

        // Create a new sprite using the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, textureWidth, textureHeight), new Vector2(0.5f, 0.5f), 100.0f);

        // Set the sprite to the object's SpriteRenderer
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddColorSplash(Color splashColor, int splashRadius)
    {
        Debug.Log("Called AddColorSplash");
        // Generate a random point for the center of the splash
        int centerX = Random.Range(0, texture.width);
        int centerY = Random.Range(0, texture.height);

        // Iterate over each pixel in the splash radius
        for (int y = centerY - splashRadius; y <= centerY + splashRadius; y++)
        {
            for (int x = centerX - splashRadius; x <= centerX + splashRadius; x++)
            {
                // Check if the pixel is within the texture bounds
                if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                {
                    // Set the pixel to the splash color
                    texture.SetPixel(x, y, splashColor);
                }
            }
        }

        // Apply the changes to the texture
        texture.Apply();
    }
}