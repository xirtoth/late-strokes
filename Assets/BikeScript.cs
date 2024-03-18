using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{
    private Camera cam;
    public float speed = 10f;

    private GameObject canvas;


    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        //move the bike y axis with speed
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "RoadBump")
        {
            Debug.Log("RoadBump");
            cam.GetComponent<CameraController>().ShakeCamera(2f, 0.5f);
            AudioManager.Instance.PlayAudio(Sound.HitBump);
        }
        else if (other.gameObject.tag == "Rainbow")
        {
            Debug.Log("Rainbow");
            canvas.GetComponent<ShaderTurner>().TurnRainbow();
            //change order in layer to 1
            canvas.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        //  cam.GetComponent<CameraController>().ShakeCamera(2f, 0.5f);
        //  AudioManager.Instance.PlayAudio(Sound.HitBump);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rainbow")
        {
            canvas.GetComponent<ShaderTurner>().TurnDefault();
            canvas.GetComponent<SpriteRenderer>().sortingOrder = -400;
        }
    }
}