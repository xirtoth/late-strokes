using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugScript : MonoBehaviour
{
    public float amplitude = 0.0005f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move up and down a little bit
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * amplitude, transform.position.z);
    }
}
