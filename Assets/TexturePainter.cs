using UnityEngine;

public class TexturePainter : MonoBehaviour
{
    public Texture2D textureToPaintOn; // Assign the texture you want to paint on in the inspector

    void Start()
    {
        textureToPaintOn = GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture as Texture2D;
        // Set up initial texture properties
        textureToPaintOn.filterMode = FilterMode.Point; // Set filter mode to Point for pixel-perfect rendering
    }

    void Paint(Vector2 pixelUV)
    {
        // Convert UV coordinates to pixel coordinates
        int x = (int)(pixelUV.x * textureToPaintOn.width);
        int y = (int)(pixelUV.y * textureToPaintOn.height);

        // Example: paint a red pixel at the specified coordinates
        textureToPaintOn.SetPixel(x, y, Color.red);
        textureToPaintOn.Apply(); // Apply changes to the texture
    }

    void Update()
    {
        // Example: paint at the center of the texture when the left mouse button is clicked
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 pixelUV = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);
            Paint(pixelUV);
        }
    }
}
