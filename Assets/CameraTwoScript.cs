using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject followTarget;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z - 10f);
    }
}
