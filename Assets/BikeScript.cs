using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{
    private Camera cam;
    public float speed = 10f;

    private GameObject canvas;

    public GameObject gc;

    public float speedMultiplier40 = 1f;

    public float speedMulplier60 = 1.4f;

    public float speedMulplier80 = 2f;

    Vector3 movingDirection;
    public List<Sprite> dieSpash = new List<Sprite>();


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
            gc.GetComponent<Spawner>().SpawnBalls();
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
        else if (other.gameObject.tag == "Speed40")
        {
            Debug.Log("Speed40");
            //get parent of the object
            if (other.transform.parent != null)
            {
                //get parent of the object
                var parent = other.transform.parent.gameObject;
                //destroy parent
                if (parent.GetComponent<TrafficSignScript>() != null)
                    parent.GetComponent<TrafficSignScript>().ZoomEffect();
            }
            Time.timeScale = speedMultiplier40;
        }

        else if (other.gameObject.tag == "Speed60")
        {
            Debug.Log("Speed60");
            Time.timeScale = speedMulplier60;
            //get parent of the object
            var parent = other.transform.parent.gameObject;
            //destroy parent
            if (parent.GetComponent<TrafficSignScript>() != null)
                parent.GetComponent<TrafficSignScript>().ZoomEffect();

        }

        else if (other.gameObject.tag == "Speed80")
        {
            Debug.Log("Speed80");
            Time.timeScale = speedMulplier80;
            //get parent of the object
            var parent = other.transform.parent.gameObject;
            //destroy parent
            if (parent.GetComponent<TrafficSignScript>() != null)
                parent.GetComponent<TrafficSignScript>().ZoomEffect();
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
        else if (other.gameObject.tag == "Speed40")
        {
            /* Time.timeScale = 1;
             Debug.Log("Speed40 eneded");*/
        }
    }
}