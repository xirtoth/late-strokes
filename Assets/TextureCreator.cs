using UnityEngine;

public class QuadTextureCreator : MonoBehaviour
{
    public int textureWidth = 256; // Width of the texture
    public int textureHeight = 256; // Height of the texture
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
                texture.SetPixel(x, y, color);
            }
        }

        // Apply the changes to the texture
        texture.Apply();

        // Set the texture to the quad's material
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}