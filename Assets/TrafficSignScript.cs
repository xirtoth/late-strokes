using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ZoomEffect()
    {
        StartCoroutine(Zoom());
        AudioManager.Instance.PlayAudio(Sound.Blob);
    }

    private IEnumerator Zoom()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 10; i++)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
