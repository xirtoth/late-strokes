using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{
    private Camera cam;
    public float speed = 10f;

    private GameObject canvas;

    public GameObject gc;

    Vector3 movingDirection;


    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        movingDirection = Vector3.up;
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        //move the bike y axis with speed
        transform.position += movingDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "RoadBump")
        {
            Debug.Log("RoadBump");
            cam.GetComponent<CameraController>().ShakeCamera(2f, 0.2f);
            AudioManager.Instance.PlayAudio(Sound.HitBump);
            gc.GetComponent<Spawner>().SpawnEnemies();
        }
        else if (other.gameObject.tag == "Rainbow")
        {
            Debug.Log("Rainbow");
            canvas.GetComponent<ShaderTurner>().TurnRainbow();
            //increase time by double
            Time.timeScale = 3;
            canvas.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if (other.gameObject.tag == "RightTurn")
        {
            //rotate bike to right
            //get old z rotation and -90 from it
            var oldRotation = transform.rotation.z;


            transform.Rotate(0, 0, oldRotation - 90);
            movingDirection = transform.up;
            //load next level

        }

        //  cam.GetComponent<CameraController>().ShakeCamera(2f, 0.5f);
        //  AudioManager.Instance.PlayAudio(Sound.HitBump);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rainbow")
        {
            canvas.GetComponent<ShaderTurner>().TurnDefault();
            canvas.GetComponent<SpriteRenderer>().sortingOrder = -20;
            Time.timeScale = 1;
        }
    }
}