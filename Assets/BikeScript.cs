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
            var parent = other.transform.parent.gameObject;
            if (parent.GetComponent<TrafficSignScript>() != null)
            {
                if (parent.GetComponent<TrafficSignScript>().hasEntered)
                {
                    return;
                }
            }
            Debug.Log("RoadBump triggered by " + other.gameObject.name + " at " + Time.time);
            cam.GetComponent<CameraController>().ShakeCamera(2f, 0.2f);
            AudioManager.Instance.PlayAudio(Sound.HitBump);
            gc.GetComponent<Spawner>().SpawnBalls();
            cam.GetComponent<CameraController>().TakeSCreenshot();
            if (other.transform.parent != null)
            {
                //get parent of the object

                //destroy parent
                if (parent.GetComponent<TrafficSignScript>() != null)
                {
                    parent.GetComponent<TrafficSignScript>().ZoomEffect();
                    parent.GetComponent<TrafficSignScript>().hasEntered = true;
                }
            }
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

            var parent = other.transform.parent.gameObject;
            if (parent.GetComponent<TrafficSignScript>() != null)
            {
                if (parent.GetComponent<TrafficSignScript>().hasEntered)
                {
                    return;
                }
            }
            if (other.transform.parent != null)
            {
                //get parent of the object

                //destroy parent
                if (parent.GetComponent<TrafficSignScript>() != null)
                    parent.GetComponent<TrafficSignScript>().ZoomEffect();
            }
            parent.GetComponent<TrafficSignScript>().hasEntered = true;
            Time.timeScale = speedMultiplier40;
        }

        else if (other.gameObject.tag == "Speed60")
        {
            var parent = other.transform.parent.gameObject;
            if (parent.GetComponent<TrafficSignScript>() != null)
            {
                if (parent.GetComponent<TrafficSignScript>().hasEntered)
                {
                    return;
                }
            }
            Debug.Log("Speed60");
            cam.GetComponent<CameraController>().TakeSCreenshot();
            Time.timeScale = speedMulplier60;
            //get parent of the object

            //destroy parent
            if (parent.GetComponent<TrafficSignScript>() != null)
                parent.GetComponent<TrafficSignScript>().ZoomEffect();
            parent.GetComponent<TrafficSignScript>().hasEntered = true;

        }

        else if (other.gameObject.tag == "Speed80")
        {
            var parent = other.transform.parent.gameObject;
            if (parent.GetComponent<TrafficSignScript>() != null)
            {
                if (parent.GetComponent<TrafficSignScript>().hasEntered)
                {
                    return;
                }
            }
            Debug.Log("Speed80");
            Time.timeScale = speedMulplier80;
            cam.GetComponent<CameraController>().TakeSCreenshot();
            parent.GetComponent<TrafficSignScript>().hasEntered = true;
            //get parent of the object

            //destroy parent
            if (parent.GetComponent<TrafficSignScript>() != null)
                parent.GetComponent<TrafficSignScript>().ZoomEffect();
        }
        else if (other.gameObject.tag == "Finish")
        {

            Debug.Log("Finish");
            //take screenshot
            cam.GetComponent<CameraController>().TakeSCreenshot();
            StartCoroutine("Finish");

        }

        //  cam.GetComponent<CameraController>().ShakeCamera(2f, 0.5f);
        //  AudioManager.Instance.PlayAudio(Sound.HitBump);
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(2);
        gc.GetComponent<GameController>().Finish();
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