using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeIn()
    {

        var maxScale = 7f;
        var randomScale = Random.Range(1f, maxScale);
        var scaleAmount = 0.9f;
        //scale from 0 to 1
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = f;
            GetComponent<Renderer>().material.color = c;
            transform.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
            if (transform.localScale.x > randomScale)
                transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            yield return new WaitForSeconds(0.01f);
        }
        // also use localScale for scaling


    }

    public void StartFadeOut()
    {
        //start fade out
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        //fade out
        for (float f = 1f; f >= -0.05f; f -= 0.01f) // Decrease f by a smaller amount to make the fade out slower
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = f;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.3f); // Increase the wait time to make the fade out slower
        }
        //destroy the gameobject
        Destroy(gameObject);
    }
}
