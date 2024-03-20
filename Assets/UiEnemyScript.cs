using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move randomly in canvas smoothly
        transform.position = new Vector3(Mathf.PingPong(Time.time, 8) + 10f, Mathf.PingPong(Time.time, 5) + 5, 0);

    }
}
