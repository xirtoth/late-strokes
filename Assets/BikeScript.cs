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

    public Texture2D speed40;
    public Texture2D speed60;
    public Texture2D speed80;
    public Texture2D roadBump;


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
            cam.GetComponent<CameraController>().ShakeCamera(1f, 0.1f);
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
            ShowRoadSign("RoadBump");
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
            StartCoroutine("TurnCamera");
            movingDirection = transform.up;
            //flip whole scene 90 degrees

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

            ShowRoadSign("Speed40");
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


            ShowRoadSign("Speed60");

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

            ShowRoadSign("Speed80");

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


    private void ShowRoadSign(string sign)
    {
        //instantiate road sign to middle of screen with transparency and fade it out
        GameObject roadSign = new GameObject();
        roadSign.AddComponent<SpriteRenderer>();
        switch (sign)
        {
            case "Speed40":
                roadSign.GetComponent<SpriteRenderer>().sprite = Sprite.Create(speed40, new Rect(0, 0, speed40.width, speed40.height), new Vector2(0.5f, 0.5f), 100.0f);
                break;
            case "Speed60":
                roadSign.GetComponent<SpriteRenderer>().sprite = Sprite.Create(speed60, new Rect(0, 0, speed60.width, speed60.height), new Vector2(0.5f, 0.5f), 100.0f);
                break;
            case "Speed80":
                roadSign.GetComponent<SpriteRenderer>().sprite = Sprite.Create(speed80, new Rect(0, 0, speed80.width, speed80.height), new Vector2(0.5f, 0.5f), 100.0f);
                break;
            case "RoadBump":
                roadSign.GetComponent<SpriteRenderer>().sprite = Sprite.Create(roadBump, new Rect(0, 0, roadBump.width, roadBump.height), new Vector2(0.5f, 0.5f), 100.0f);
                break;
        }
        roadSign.transform.position = new Vector3(-2f, 0, 0);
        //roadSign.AddComponent<FadeInScript>();
        roadSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
        roadSign.GetComponent<SpriteRenderer>().sortingOrder = 1;
        roadSign.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        //scale it up
        roadSign.transform.localScale = new Vector3(10f, 10f, 1);
        Destroy(roadSign, 4);
        //fadeout

    }


    private IEnumerator TurnCamera()
    {
        Camera cam = Camera.main;
        float duration = 12f; // Duration of the turn
        float halfDuration = duration / 2f; // Halfway point
        float startRotation = cam.transform.eulerAngles.z; // Starting rotation
        float endRotation = startRotation + 45f; // Ending rotation

        // Rotate to 45 degrees
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / halfDuration);
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, zRotation);
            yield return null;
        }

        // Ensure the rotation is exactly 45 degrees
        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, endRotation);

        // Rotate back to original rotation
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            float zRotation = Mathf.Lerp(endRotation, startRotation, t / halfDuration);
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, zRotation);
            yield return null;
        }

        // Ensure the rotation is exactly the original rotation
        transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, startRotation);
    }
}