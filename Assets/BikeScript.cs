using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{
    private Camera cam;
    public float speed = 10f;


    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
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
        cam.GetComponent<CameraController>().ShakeCamera(2f, 0.5f);
        AudioManager.Instance.PlayAudio(Sound.HitBump);
    }
}