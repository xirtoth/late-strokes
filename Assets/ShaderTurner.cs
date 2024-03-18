using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTurner : MonoBehaviour
{
    public Material defaultMaterial;
    public Material rainbowMaterial;
    void Start()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnRainbow()
    {
        Debug.Log("TurnRainbow");
        GetComponent<SpriteRenderer>().material = rainbowMaterial;
    }

    public void TurnDefault()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
}
